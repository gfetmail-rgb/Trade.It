using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal enum TradingChartType { Candlestick, Line, Bar }

    internal enum ChartDrawingTool
    {
        None, TrendLine, TrendChannel, HorizontalLine, VerticalLine,
        HorizontalRay, TrendLineWithArrow, Rectangle
    }

    internal sealed class TradingChartPoint
    {
        public DateTime Date { get; init; }
        public bool HasRealDate { get; init; }
        public double Open { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Close { get; init; }
        public double Volume { get; init; }
    }

    internal sealed partial class TradingChartControl : Control
    {
        private readonly List<TradingChartPoint> points = new();
        private readonly List<ChartDrawing> drawings = new();
        private TradingChartType chartType = TradingChartType.Candlestick;
        private int visibleCount;
        private int firstIndex;
        private int selectedDrawingIndex = -1;
        private bool panning;
        private Point panStartPoint;
        private int panStartFirstIndex;
        private double panStartVerticalPanOffset;
        private double panStartVerticalRange;
        private double panStartHorizontalOffset;
        private bool horizontalAxisDrag;
        private Point horizontalAxisStartPoint;
        private int horizontalAxisStartVisibleCount;
        private double horizontalAxisCenterIndex;
        private bool verticalAxisDrag;
        private Point verticalAxisStartPoint;
        private double verticalAxisStartZoom;
        private bool showGrid;
        private bool showCrosshair = true;
        private Point crosshairPoint;
        private int crosshairIndex = -1;
        private double crosshairPosition = -1.0;
        private DateTime? syncedCrosshairDate;
        private double verticalZoom = 1.0;
        private double verticalPanOffset;
        private double horizontalPanOffset;
        private ChartDrawingTool activeDrawingTool;
        private bool drawingInProgress;
        private int drawingStage;
        private Point drawingStartPoint;
        private Point drawingCurrentPoint;
        private Point drawingSecondPoint;
        private int draggingDrawingIndex = -1;
        private int draggingHandle = 0;
        private Point draggingLastPoint;
        private string chartSymbol = string.Empty;
        private string chartTimeFrame = string.Empty;
        private double volumePanelRatio = 0.10;
        private double rsiPanelRatio = 0.10;
        private double macdPanelRatio = 0.10;
        private double stochasticPanelRatio = 0.10;
        private int volumePanelGap = 8;
        private bool lowerPanelResizeDrag;
        private int lowerPanelResizeStartY;
        private double lowerPanelResizeStartRsiRatio;
        private double lowerPanelResizeStartVolumeRatio;
        private double lowerPanelResizeStartMacdRatio;
        private double lowerPanelResizeStartStochasticRatio;
        private enum LowerPanelSplitter
        {
            None,
            PriceRsi,
            PriceMacd,
            PriceVolume,
            RsiMacd,
            RsiVolume,
            MacdVolume,
            PriceStochastic,
            RsiStochastic,
            MacdStochastic,
            StochasticVolume
        }
        private LowerPanelSplitter activeLowerPanelSplitter;
        private bool volumePanelVisible = true;
        private bool testMode;
        private bool testStartSelected;
        private int testEndIndex = -1;
        private int testAnchorIndex = -1;
        private float testAnchorScreenX;
        private bool suppressSyncNotifications;

        public event EventHandler? ViewChanged;
        public event EventHandler? CrosshairDateChanged;
        public event EventHandler? AnalysisChanged;

        private sealed class ChartDrawing
        {
            public ChartDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public double X3 { get; set; }
            public double Y3 { get; set; }
            public DateTime? Date1 { get; set; }
            public DateTime? Date2 { get; set; }
            public DateTime? Date3 { get; set; }
        }

        public TradingChartControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ForeColor = Color.Black;
            ResizeRedraw = true;
            MinimumSize = new Size(200, 150);
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        public void SetData(IEnumerable<TradingChartPoint> data, string? symbol = null, string? timeFrame = null)
        {
            chartSymbol = symbol?.Trim() ?? string.Empty;
            chartTimeFrame = timeFrame?.Trim() ?? string.Empty;
            points.Clear();
            points.AddRange(data.OrderBy(x => x.Date));
            InvalidateIchimokuCache();

            // همگام‌سازی وضعیت داده‌های ابزارهای Extra با داده‌ی جدید.
            // این کار مانع می‌شود یک Paint/Refresh موقت (مثلاً هنگام باز شدن پنجره متن)
            // به اشتباه Extra Drawingهای موجود مثل Pitchfork را پاک کند.
            extraDataCount = points.Count;
            extraFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            extraLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;

            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            // نسبت اولیه پنل‌ها هنگام باز شدن هر چارت.
            volumePanelRatio = 0.10;
            rsiPanelRatio = 0.10;
            macdPanelRatio = 0.10;
            stochasticPanelRatio = 0.10;
            testEndIndex = -1;
            testAnchorIndex = -1;
            testAnchorScreenX = 0f;
            verticalZoom = 1.0;
            verticalPanOffset = 0;
            horizontalPanOffset = 0;
            chartPanCompensation = 0;
            showCrosshair = true;
            crosshairIndex = -1;
            crosshairPosition = -1.0;
            syncedCrosshairDate = null;
            CancelDrawing();
            selectedDrawingIndex = -1;
            selectedIndicatorIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
            Invalidate();
        }

        public string ChartSymbol => chartSymbol;
        public string ChartTimeFrame => chartTimeFrame;
        public IReadOnlyList<TradingChartPoint> Points => points;
        public DateTime? CrosshairDate =>
            syncedCrosshairDate ??
            (crosshairIndex >= 0 && crosshairIndex < visibleCount &&
             firstIndex + crosshairIndex < points.Count
                ? points[firstIndex + crosshairIndex].Date
                : null);

        public (DateTime Start, DateTime End)? GetVisibleDateRange()
        {
            if (points.Count == 0)
                return null;

            var startIndex = Math.Clamp(firstIndex, 0, points.Count - 1);
            var endIndex = Math.Clamp(firstIndex + Math.Max(1, visibleCount) - 1, startIndex, points.Count - 1);
            return (points[startIndex].Date, points[endIndex].Date);
        }

        public void SetVisibleDateRange(DateTime start, DateTime end)
        {
            if (points.Count == 0)
                return;

            if (end < start)
                (start, end) = (end, start);

            var startIndex = FindNearestPointIndex(start);
            var endIndex = FindNearestPointIndex(end);

            if (endIndex < startIndex)
                (startIndex, endIndex) = (endIndex, startIndex);

            var count = Math.Max(2, endIndex - startIndex + 1);
            count = Math.Min(count, points.Count);
            firstIndex = Math.Clamp(startIndex, 0, Math.Max(0, points.Count - count));
            visibleCount = count;
            crosshairIndex = -1;
            crosshairPosition = -1.0;
            syncedCrosshairDate = null;
            EnsureChartPanCompensation();
            Invalidate();
        }

        public void SetCrosshairDate(DateTime date)
        {
            if (!showCrosshair || points.Count == 0)
                return;

            var absoluteIndex = FindNearestPointIndex(date);
            if (absoluteIndex < firstIndex ||
                absoluteIndex >= firstIndex + visibleCount)
            {
                return;
            }

            crosshairIndex = absoluteIndex - firstIndex;
            syncedCrosshairDate = date;

            // جایگاه کراس بر اساس زمان واقعی محاسبه می‌شود، نه صرفاً نزدیک‌ترین کندل.
            // بنابراین مثلاً در چارت روزانه، کراس با حرکت چارت دقیقه‌ای
            // بین دو کندل روزانه نیز به‌صورت پیوسته حرکت می‌کند.
            double absolutePosition = absoluteIndex;
            if (absoluteIndex > 0 && absoluteIndex < points.Count)
            {
                var left = points[absoluteIndex - 1];
                var right = points[absoluteIndex];

                if (right.Date > left.Date && date >= left.Date && date <= right.Date)
                {
                    var fraction = (date - left.Date).TotalSeconds /
                                   Math.Max(1.0, (right.Date - left.Date).TotalSeconds);
                    absolutePosition = (absoluteIndex - 1) + Math.Clamp(fraction, 0.0, 1.0);
                }
            }

            crosshairPosition = absolutePosition - firstIndex;

            var plot = GetPlotRectangle();
            var step = plot.Width / (double)Math.Max(1, visibleCount);
            var initialOffset = -plot.Width * 0.25;
            crosshairPoint = new Point(
                (int)Math.Round(plot.Left + step * (crosshairPosition + 0.5) + initialOffset + horizontalPanOffset),
                plot.Top + plot.Height / 2);
            Invalidate();
        }

        private int FindNearestPointIndex(DateTime date)
        {
            if (points.Count == 0)
                return 0;

            var lo = 0;
            var hi = points.Count - 1;
            while (lo < hi)
            {
                var mid = lo + (hi - lo) / 2;
                if (points[mid].Date < date)
                    lo = mid + 1;
                else
                    hi = mid;
            }

            if (lo == 0)
                return 0;

            var previous = lo - 1;
            return Math.Abs((points[lo].Date - date).Ticks) <
                   Math.Abs((points[previous].Date - date).Ticks)
                ? lo
                : previous;
        }

        private void NotifyViewChanged()
        {
            if (!suppressSyncNotifications)
                ViewChanged?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyCrosshairDateChanged()
        {
            if (!suppressSyncNotifications)
                CrosshairDateChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void ApplySyncedDateRange(DateTime start, DateTime end)
        {
            if (points.Count == 0)
                return;

            if (end < start)
                (start, end) = (end, start);

            // اگر چارت از قبل دقیقاً همین بازه را دارد، هیچ کاری نکن.
            // این حالت هنگام ساخت Workspace مهم است: RestoreAnalysisDocument
            // قبلاً بازه را تنظیم کرده و اجرای دوباره‌ی Sync نباید مختصات ابزارها
            // را یک بار دیگر تبدیل و دچار رانش کند.
            var currentRange = GetVisibleDateRange();
            if (currentRange.HasValue &&
                currentRange.Value.Start == start &&
                currentRange.Value.End == end)
                return;

            // تاریخ نقاط اتصال را قبل از تغییر firstIndex نگه می‌داریم.
            // سپس بعد از تغییر View، X را از روی همان تاریخ‌ها بازسازی می‌کنیم.
            var drawingDates = drawings.Select(d => (d.Date1, d.Date2, d.Date3)).ToList();
            var advancedDates = advancedDrawings.Select(d => (d.Date1, d.Date2)).ToList();
            var extraDates = extraDrawings.Select(d => (d.Date1, d.Date2, d.Date3)).ToList();

            suppressSyncNotifications = true;
            try
            {
                SetVisibleDateRange(start, end);

                for (var i = 0; i < drawings.Count && i < drawingDates.Count; i++)
                {
                    var dates = drawingDates[i];
                    if (dates.Date1.HasValue) drawings[i].X1 = DateToDataX(dates.Date1.Value, drawings[i].X1);
                    if (dates.Date2.HasValue) drawings[i].X2 = DateToDataX(dates.Date2.Value, drawings[i].X2);
                    if (dates.Date3.HasValue) drawings[i].X3 = DateToDataX(dates.Date3.Value, drawings[i].X3);
                }

                for (var i = 0; i < advancedDrawings.Count && i < advancedDates.Count; i++)
                {
                    var dates = advancedDates[i];
                    if (dates.Date1.HasValue) advancedDrawings[i].X1 = DateToDataX(dates.Date1.Value, advancedDrawings[i].X1);
                    if (dates.Date2.HasValue) advancedDrawings[i].X2 = DateToDataX(dates.Date2.Value, advancedDrawings[i].X2);
                }

                for (var i = 0; i < extraDrawings.Count && i < extraDates.Count; i++)
                {
                    var dates = extraDates[i];
                    if (dates.Date1.HasValue) extraDrawings[i].X1 = DateToDataX(dates.Date1.Value, extraDrawings[i].X1);
                    if (dates.Date2.HasValue) extraDrawings[i].X2 = DateToDataX(dates.Date2.Value, extraDrawings[i].X2);
                    if (dates.Date3.HasValue) extraDrawings[i].X3 = DateToDataX(dates.Date3.Value, extraDrawings[i].X3);
                }
            }
            finally
            {
                suppressSyncNotifications = false;
            }

            Invalidate();
        }

        internal void ApplySyncedCrosshairDate(DateTime date)
        {
            suppressSyncNotifications = true;
            try { SetCrosshairDate(date); }
            finally { suppressSyncNotifications = false; }
        }

        public TradingChartType ChartType => chartType;

        public ChartAnalysisDocument CreateAnalysisDocument()
        {
            var document = new ChartAnalysisDocument
            {
                Symbol = chartSymbol,
                ChartType = chartType.ToString(),
                GridVisible = showGrid,
                CrosshairVisible = showCrosshair,
                VisibleCount = visibleCount,
                FirstIndex = firstIndex,
                VisibleStartDate = points.Count > 0 && firstIndex >= 0 && firstIndex < points.Count
                    ? points[firstIndex].Date
                    : null,
                VisibleEndDate = points.Count > 0
                    ? points[Math.Clamp(firstIndex + Math.Max(1, visibleCount) - 1, 0, points.Count - 1)].Date
                    : null,
                VerticalZoom = verticalZoom,
                VerticalPanOffset = verticalPanOffset,
                HorizontalPanOffset = horizontalPanOffset,
                ChartPanCompensation = chartPanCompensation,
                VolumePanelRatio = volumePanelRatio
            };

            foreach (var drawing in drawings)
            {
                document.Drawings.Add(new ChartAnalysisDrawing
                {
                    Tool = drawing.Tool.ToString(),
                    X1 = drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = drawing.X2,
                    Y2 = drawing.Y2,
                    X3 = drawing.X3,
                    Y3 = drawing.Y3,
                    Date1 = drawing.Date1 ?? GetPointDate((int)Math.Round(drawing.X1)),
                    Date2 = drawing.Date2 ?? GetPointDate((int)Math.Round(drawing.X2)),
                    Date3 = drawing.Date3 ?? GetPointDate((int)Math.Round(drawing.X3))
                });
            }

            foreach (var drawing in advancedDrawings)
            {
                document.AdvancedDrawings.Add(new ChartAnalysisDrawing
                {
                    Tool = drawing.Tool.ToString(),
                    X1 = drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = drawing.X2,
                    Y2 = drawing.Y2,
                    Date1 = drawing.Date1 ?? GetPointDate((int)Math.Round(drawing.X1)),
                    Date2 = drawing.Date2 ?? GetPointDate((int)Math.Round(drawing.X2)),
                    Text = drawing.Text
                });
            }

            foreach (var drawing in extraDrawings)
            {
                document.ExtraDrawings.Add(new ChartAnalysisDrawing
                {
                    Tool = drawing.Tool.ToString(),
                    X1 = drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = drawing.X2,
                    Y2 = drawing.Y2,
                    X3 = drawing.X3,
                    Y3 = drawing.Y3,
                    Date1 = drawing.Date1 ?? GetPointDate((int)Math.Round(drawing.X1)),
                    Date2 = drawing.Date2 ?? GetPointDate((int)Math.Round(drawing.X2)),
                    Date3 = drawing.Date3 ?? GetPointDate((int)Math.Round(drawing.X3))
                });
            }

            return document;
        }

        private void UpdateDrawingDates(ChartDrawing drawing)
        {
            drawing.Date1 = GetPointDate((int)Math.Round(drawing.X1));
            drawing.Date2 = GetPointDate((int)Math.Round(drawing.X2));
            drawing.Date3 = GetPointDate((int)Math.Round(drawing.X3));
        }

        private DateTime? GetPointDate(int absoluteIndex)
        {
            if (absoluteIndex < 0 || absoluteIndex >= points.Count)
                return null;
            return points[absoluteIndex].Date;
        }

        private int ResolveDrawingIndex(DateTime? date, double legacyIndex, int fallbackFirstIndex)
        {
            if (date.HasValue && points.Count > 0)
                return FindNearestPointIndex(date.Value);
            return (int)Math.Round(legacyIndex);
        }

        public void ApplySharedAnalysisDrawings(ChartAnalysisDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (!string.IsNullOrWhiteSpace(document.Symbol) &&
                !string.Equals(document.Symbol.Trim(), chartSymbol.Trim(), StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("این تحلیل مربوط به نماد دیگری است.");

            CancelDrawing();
            CancelAdvancedDrawing();
            CancelExtraDrawing();

            drawings.Clear();
            advancedDrawings.Clear();
            extraDrawings.Clear();

            foreach (var item in document.Drawings ?? new List<ChartAnalysisDrawing>())
            {
                var toolName = item.Tool switch
                {
                    "HorizontalDoubleArrow" => "HorizontalLine",
                    "VerticalDoubleArrow" => "VerticalLine",
                    _ => item.Tool
                };

                if (!Enum.TryParse<ChartDrawingTool>(toolName, true, out var tool) ||
                    tool == ChartDrawingTool.None)
                    continue;

                var x1 = ResolveDrawingIndex(item.Date1, item.X1, 0);
                var x2 = ResolveDrawingIndex(item.Date2, item.X2, 0);
                var x3 = ResolveDrawingIndex(item.Date3, item.X3, 0);

                drawings.Add(new ChartDrawing
                {
                    Tool = tool,
                    X1 = x1, Y1 = item.Y1,
                    X2 = x2, Y2 = item.Y2,
                    X3 = x3, Y3 = item.Y3,
                    Date1 = item.Date1 ?? GetPointDate(x1),
                    Date2 = item.Date2 ?? GetPointDate(x2),
                    Date3 = item.Date3 ?? GetPointDate(x3)
                });
            }

            foreach (var item in document.AdvancedDrawings ?? new List<ChartAnalysisDrawing>())
            {
                if (!Enum.TryParse<AdvancedDrawingTool>(item.Tool, true, out var tool) ||
                    tool == AdvancedDrawingTool.None)
                    continue;

                var x1 = ResolveDrawingIndex(item.Date1, item.X1, 0);
                var x2 = ResolveDrawingIndex(item.Date2, item.X2, 0);

                advancedDrawings.Add(new AdvancedDrawing
                {
                    Tool = tool,
                    X1 = x1, Y1 = item.Y1,
                    X2 = x2, Y2 = item.Y2,
                    Text = item.Text,
                    Date1 = item.Date1 ?? GetPointDate(x1),
                    Date2 = item.Date2 ?? GetPointDate(x2)
                });
            }

            foreach (var item in document.ExtraDrawings ?? new List<ChartAnalysisDrawing>())
            {
                if (!Enum.TryParse<ExtraDrawingTool>(item.Tool, true, out var tool) ||
                    tool == ExtraDrawingTool.None)
                    continue;

                var x1 = ResolveDrawingIndex(item.Date1, item.X1, 0);
                var x2 = ResolveDrawingIndex(item.Date2, item.X2, 0);
                var x3 = ResolveDrawingIndex(item.Date3, item.X3, 0);

                extraDrawings.Add(new ExtraDrawing
                {
                    Tool = tool,
                    X1 = x1, Y1 = item.Y1,
                    X2 = x2, Y2 = item.Y2,
                    X3 = x3, Y3 = item.Y3,
                    Date1 = item.Date1 ?? GetPointDate(x1),
                    Date2 = item.Date2 ?? GetPointDate(x2),
                    Date3 = item.Date3 ?? GetPointDate(x3)
                });
            }

            advancedDataCount = points.Count;
            advancedFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            advancedLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;
            extraDataCount = points.Count;
            extraFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            extraLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;

            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            selectedAdvancedDrawingIndex = -1;
            draggingAdvancedDrawingIndex = -1;
            selectedExtraDrawingIndex = -1;
            extraDraggingDrawingIndex = -1;

            Invalidate();
        }

        public void RestoreAnalysisDocument(ChartAnalysisDocument document, bool restoreView = true)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (!string.IsNullOrWhiteSpace(document.Symbol) &&
                !string.Equals(document.Symbol.Trim(), chartSymbol.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("این تحلیل مربوط به نماد دیگری است.");
            }

            CancelDrawing();
            CancelAdvancedDrawing();
            CancelExtraDrawing();

            // در حالت چندتایم‌فریمی ابتدا پنجره زمانی چارت مقصد را تعیین می‌کنیم.
            // سپس X ابزارها نسبت به firstIndex نهایی محاسبه می‌شود. اگر این ترتیب
            // برعکس باشد، SetVisibleDateRange بعداً firstIndex را تغییر می‌دهد و
            // مستطیل، خط عمودی و فلش‌دار در جای اشتباه ظاهر می‌شوند یا از دید خارج می‌شوند.
            if (!restoreView && document.VisibleStartDate.HasValue && document.VisibleEndDate.HasValue)
                SetVisibleDateRange(document.VisibleStartDate.Value, document.VisibleEndDate.Value);

            drawings.Clear();
            advancedDrawings.Clear();
            extraDrawings.Clear();

            foreach (var item in document.Drawings ?? new List<ChartAnalysisDrawing>())
            {
                // سازگاری با تحلیل‌های قدیمی که نام ابزارها در آنها DoubleArrow بوده است.
                var toolName = item.Tool switch
                {
                    "HorizontalDoubleArrow" => "HorizontalLine",
                    "VerticalDoubleArrow" => "VerticalLine",
                    _ => item.Tool
                };

                if (!Enum.TryParse<ChartDrawingTool>(toolName, true, out var tool) ||
                    tool == ChartDrawingTool.None)
                    continue;

                drawings.Add(new ChartDrawing
                {
                    Tool = tool,
                    X1 = ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex),
                    Y1 = item.Y1,
                    X2 = ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex),
                    Y2 = item.Y2,
                    X3 = ResolveDrawingIndex(item.Date3, item.X3, document.FirstIndex),
                    Y3 = item.Y3,
                    Date1 = item.Date1 ?? GetPointDate((int)ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex)),
                    Date2 = item.Date2 ?? GetPointDate((int)ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex)),
                    Date3 = item.Date3 ?? GetPointDate((int)ResolveDrawingIndex(item.Date3, item.X3, document.FirstIndex))
                });
            }

            foreach (var item in document.AdvancedDrawings ?? new List<ChartAnalysisDrawing>())
            {
                if (!Enum.TryParse<AdvancedDrawingTool>(item.Tool, true, out var tool) ||
                    tool == AdvancedDrawingTool.None)
                    continue;

                advancedDrawings.Add(new AdvancedDrawing
                {
                    Tool = tool,
                    X1 = ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex),
                    Y1 = item.Y1,
                    X2 = ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex),
                    Y2 = item.Y2,
                    Text = item.Text,
                    Date1 = item.Date1 ?? GetPointDate((int)ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex)),
                    Date2 = item.Date2 ?? GetPointDate((int)ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex))
                });
            }

            foreach (var item in document.ExtraDrawings ?? new List<ChartAnalysisDrawing>())
            {
                if (!Enum.TryParse<ExtraDrawingTool>(item.Tool, true, out var tool) ||
                    tool == ExtraDrawingTool.None)
                    continue;

                extraDrawings.Add(new ExtraDrawing
                {
                    Tool = tool,
                    X1 = ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex),
                    Y1 = item.Y1,
                    X2 = ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex),
                    Y2 = item.Y2,
                    X3 = ResolveDrawingIndex(item.Date3, item.X3, document.FirstIndex),
                    Y3 = item.Y3,
                    Date1 = item.Date1 ?? GetPointDate((int)ResolveDrawingIndex(item.Date1, item.X1, document.FirstIndex)),
                    Date2 = item.Date2 ?? GetPointDate((int)ResolveDrawingIndex(item.Date2, item.X2, document.FirstIndex)),
                    Date3 = item.Date3 ?? GetPointDate((int)ResolveDrawingIndex(item.Date3, item.X3, document.FirstIndex))
                });
            }

            if (restoreView)
            {
                visibleCount = Math.Clamp(
                    document.VisibleCount > 0 ? document.VisibleCount : Math.Min(200, Math.Max(1, points.Count)),
                    1,
                    Math.Max(1, points.Count));

                firstIndex = Math.Clamp(
                    document.FirstIndex,
                    0,
                    Math.Max(0, points.Count - visibleCount));

                verticalZoom = document.VerticalZoom > 0 ? document.VerticalZoom : 1.0;
                verticalPanOffset = document.VerticalPanOffset;
            horizontalPanOffset = document.HorizontalPanOffset;
            chartPanCompensation = document.ChartPanCompensation;
            volumePanelRatio = Math.Clamp(
                document.VolumePanelRatio > 0 ? document.VolumePanelRatio : 0.11,
                0.10,
                0.45);
            }

            if (Enum.TryParse<TradingChartType>(document.ChartType, true, out var restoredChartType))
                chartType = restoredChartType;

            showGrid = document.GridVisible;
            showCrosshair = document.CrosshairVisible;
            crosshairIndex = -1;

            advancedDataCount = points.Count;
            advancedFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            advancedLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;

            extraDataCount = points.Count;
            extraFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            extraLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;

            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            selectedAdvancedDrawingIndex = -1;
            draggingAdvancedDrawingIndex = -1;
            selectedExtraDrawingIndex = -1;
            extraDraggingDrawingIndex = -1;

            Invalidate();
        }

        public void SetGridVisible(bool visible)
        {
            showGrid = visible;
            Invalidate();
        }

        public void SetCrosshairVisible(bool visible)
        {
            showCrosshair = visible;
            if (!visible)
                crosshairIndex = -1;
            Invalidate();
        }

        public void SetChartType(TradingChartType type) { chartType = type; Invalidate(); }
        public void ToggleGrid() { showGrid = !showGrid; Invalidate(); }
        public bool GridVisible => showGrid;
        public void ToggleCrosshair() { showCrosshair = !showCrosshair; Invalidate(); }
        public bool CrosshairVisible => showCrosshair;
        public bool TestMode => testMode;

        public void SetTestMode(bool enabled)
        {
            testMode = enabled;
            testStartSelected = false;
            testEndIndex = -1;
            testAnchorIndex = -1;
            testAnchorScreenX = 0f;

            // با ورود/خروج از حالت تست، هیچ وضعیت نیمه‌کاره‌ای از
            // ورودی ماوس نباید به رسم ابزارهای معمولی منتقل شود.
            extraInputHandled = false;
            Capture = false;
            panning = false;
            horizontalAxisDrag = false;
            verticalAxisDrag = false;
            lowerPanelResizeDrag = false;
            activeLowerPanelSplitter = LowerPanelSplitter.None;
            draggingDrawingIndex = -1;
            draggingHandle = 0;

            if (enabled)
                CancelDrawing();
            else
                ResetView();

            Cursor = Cursors.Default;
            Focus();
            Invalidate();
        }

        public void StepTest(int delta)
        {
            if (!testMode || points.Count == 0 || !testStartSelected)
                return;

            if (testEndIndex < 0)
                return;

            var nextEnd = Math.Clamp(testEndIndex + delta, 0, points.Count - 1);
            if (nextEnd == testEndIndex)
                return;

            testEndIndex = nextEnd;
            KeepTestAnchorFixed();
            crosshairIndex = -1;
            Invalidate();
        }

        private void KeepTestAnchorFixed()
        {
            if (!testMode || testEndIndex < 0 || points.Count == 0)
                return;

            // در Test Mode، کندل آخرِ قابل مشاهده باید در همان موقعیت
            // افقی بماند و با هر Step فقط پنجره‌ی داده یک کندل جابه‌جا شود.
            // بنابراین با حرکت به راست، firstIndex هم به همان اندازه جلو می‌رود
            // و کندل‌های قبلی به سمت چپ کشیده می‌شوند.
            var count = Math.Max(1, visibleCount);
            var desiredFirst = testEndIndex - count + 1;

            firstIndex = Math.Clamp(
                desiredFirst,
                0,
                Math.Max(0, points.Count - count));
        }

        private void SetTestEndFromMouse(Point location)
        {
            if (!testMode || points.Count == 0 || !IsInsidePlot(location))
                return;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var count = Math.Max(1, endIndex - firstIndex);
            var step = plot.Width / (double)count;
            var initialOffset = -plot.Width * 0.25;
            var dataX = (location.X - plot.Left - initialOffset - horizontalPanOffset) / step - 0.5;
            var relativeIndex = Math.Clamp((int)Math.Round(dataX), 0, count - 1);

            // کندل انتخاب‌شده، آخرین کندل قابل مشاهده در Test Mode است.
            // موقعیت آن در صفحه ثابت می‌ماند و Step فقط داده‌های قبل از آن
            // را یک کندل به چپ/راست جابه‌جا می‌کند.
            testEndIndex = Math.Clamp(firstIndex + relativeIndex, 0, points.Count - 1);
            testAnchorIndex = testEndIndex;
            testAnchorScreenX = location.X;
            testStartSelected = true;
            KeepTestAnchorFixed();
            crosshairIndex = -1;
            Invalidate();
        }
        public ChartDrawingTool ActiveDrawingTool => activeDrawingTool;
        public bool DrawingInProgress => drawingInProgress;

        public void SetDrawingTool(ChartDrawingTool tool)
        {
            activeDrawingTool = tool;
            drawingInProgress = false;
            drawingStage = 0;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            drawingSecondPoint = Point.Empty;
            Focus();
            Cursor = tool == ChartDrawingTool.None ? Cursors.Default : Cursors.Cross;
            Invalidate();
        }

        public void CancelDrawing()
        {
            activeDrawingTool = ChartDrawingTool.None;
            drawingInProgress = false;
            drawingStage = 0;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            drawingSecondPoint = Point.Empty;
            Cursor = Cursors.Default;
            Invalidate();
        }

        public void ResetView()
        {
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1;
            verticalPanOffset = 0;
            horizontalPanOffset = 0;
            FitVerticalView();
            chartPanCompensation = 0;
            crosshairIndex = -1;
            EnsureChartPanCompensation();
            Invalidate();
            NotifyViewChanged();
        }

        public void ZoomX(double factor)
        {
            if (points.Count < 2) return;
            var oldCount = Math.Max(2, visibleCount);
            var newCount = Math.Clamp((int)Math.Round(oldCount * factor), 2, points.Count);
            if (newCount == oldCount) return;
            visibleCount = newCount;
            firstIndex = Math.Max(0, points.Count - newCount);
            crosshairIndex = -1;
            crosshairPosition = -1.0;
            syncedCrosshairDate = null;
            Invalidate();
            NotifyViewChanged();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedDrawingIndex >= 0 && selectedDrawingIndex < drawings.Count)
            {
                drawings.RemoveAt(selectedDrawingIndex);
                selectedDrawingIndex = -1;
                AnalysisChanged?.Invoke(this, EventArgs.Empty);
                draggingDrawingIndex = -1;
                draggingHandle = 0;
                Invalidate();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.Escape && activeDrawingTool != ChartDrawingTool.None)
            {
                CancelDrawing();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            base.OnKeyDown(e);
        }

        private LowerPanelSplitter GetLowerPanelSplitterAt(int y)
        {
            var layout = GetLowerPanelLayout();
            const int tolerance = 5;

            if (HasRsiIndicator && !layout.Rsi.IsEmpty &&
                Math.Abs(y - layout.Rsi.Top) <= tolerance)
                return LowerPanelSplitter.PriceRsi;

            if (!HasRsiIndicator && HasMacdIndicator && !layout.Macd.IsEmpty &&
                Math.Abs(y - layout.Macd.Top) <= tolerance)
                return LowerPanelSplitter.PriceMacd;

            if (!HasRsiIndicator && !HasMacdIndicator && volumePanelVisible &&
                !layout.Volume.IsEmpty &&
                Math.Abs(y - layout.Volume.Top) <= tolerance)
                return LowerPanelSplitter.PriceVolume;

            if (HasRsiIndicator && HasMacdIndicator &&
                !layout.Macd.IsEmpty &&
                Math.Abs(y - layout.Macd.Top) <= tolerance)
                return LowerPanelSplitter.RsiMacd;

            if (HasRsiIndicator && !HasMacdIndicator && volumePanelVisible &&
                !layout.Volume.IsEmpty &&
                Math.Abs(y - layout.Volume.Top) <= tolerance)
                return LowerPanelSplitter.RsiVolume;

            if (HasStochasticIndicator && !layout.Stochastic.IsEmpty &&
                Math.Abs(y - layout.Stochastic.Top) <= tolerance)
            {
                if (HasMacdIndicator) return LowerPanelSplitter.MacdStochastic;
                if (HasRsiIndicator) return LowerPanelSplitter.RsiStochastic;
                return LowerPanelSplitter.PriceStochastic;
            }

            if (HasStochasticIndicator && volumePanelVisible && !layout.Volume.IsEmpty &&
                Math.Abs(y - layout.Volume.Top) <= tolerance)
                return LowerPanelSplitter.StochasticVolume;

            if (!HasRsiIndicator && HasMacdIndicator && volumePanelVisible &&
                !layout.Volume.IsEmpty &&
                Math.Abs(y - layout.Volume.Top) <= tolerance)
                return LowerPanelSplitter.MacdVolume;

            return LowerPanelSplitter.None;
        }

        private void ResizeLowerPanels(int mouseY)
        {
            var layout = GetLowerPanelLayout();
            var totalHeight = Math.Max(1, layout.OverallBottom - layout.Price.Top);
            var delta = mouseY - lowerPanelResizeStartY;
            var deltaRatio = delta / (double)totalHeight;

            const int minimumPriceHeight = 80;
            const int minimumRsiHeight = 60;
            const int minimumMacdHeight = 60;
            const int minimumVolumeHeight = 45;
            var gapCount = (HasRsiIndicator ? 1 : 0) +
                           (HasMacdIndicator ? 1 : 0) +
                           (volumePanelVisible ? 1 : 0);
            var minPriceRatio = minimumPriceHeight / (double)totalHeight;
            var minRsiRatio = minimumRsiHeight / (double)totalHeight;
            var minMacdRatio = minimumMacdHeight / (double)totalHeight;
            var minVolumeRatio = minimumVolumeHeight / (double)totalHeight;
            var gapRatio = Math.Clamp(volumePanelGap, 2, 30) * Math.Max(0, gapCount) / (double)totalHeight;

            switch (activeLowerPanelSplitter)
            {
                case LowerPanelSplitter.PriceRsi when HasRsiIndicator:
                    rsiPanelRatio = Math.Clamp(
                        lowerPanelResizeStartRsiRatio - deltaRatio,
                        minRsiRatio,
                        Math.Max(minRsiRatio, 1.0 - minPriceRatio - gapRatio -
                            (HasMacdIndicator ? macdPanelRatio : 0) -
                            (volumePanelVisible ? volumePanelRatio : 0)));
                    break;

                case LowerPanelSplitter.PriceMacd when HasMacdIndicator && !HasRsiIndicator:
                    macdPanelRatio = Math.Clamp(
                        lowerPanelResizeStartMacdRatio - deltaRatio,
                        minMacdRatio,
                        Math.Max(minMacdRatio, 1.0 - minPriceRatio - gapRatio -
                            (volumePanelVisible ? volumePanelRatio : 0)));
                    break;

                case LowerPanelSplitter.PriceVolume when volumePanelVisible:
                    volumePanelRatio = Math.Clamp(
                        lowerPanelResizeStartVolumeRatio - deltaRatio,
                        minVolumeRatio,
                        Math.Max(minVolumeRatio, 0.45));
                    break;

                case LowerPanelSplitter.RsiMacd when HasRsiIndicator && HasMacdIndicator:
                {
                    var sum = lowerPanelResizeStartRsiRatio + lowerPanelResizeStartMacdRatio;
                    var rsi = lowerPanelResizeStartRsiRatio + deltaRatio;
                    var macd = sum - rsi;
                    var maxSum = 1.0 - minPriceRatio - gapRatio -
                                 (volumePanelVisible ? volumePanelRatio : 0);
                    rsi = Math.Clamp(rsi, minRsiRatio, Math.Max(minRsiRatio, maxSum - minMacdRatio));
                    macd = sum - rsi;
                    if (macd < minMacdRatio)
                    {
                        macd = minMacdRatio;
                        rsi = sum - macd;
                    }
                    rsiPanelRatio = rsi;
                    macdPanelRatio = macd;
                    break;
                }

                case LowerPanelSplitter.RsiVolume when HasRsiIndicator && volumePanelVisible:
                {
                    var sum = lowerPanelResizeStartRsiRatio + lowerPanelResizeStartVolumeRatio;
                    var rsi = lowerPanelResizeStartRsiRatio + deltaRatio;
                    var volume = sum - rsi;
                    var maxSum = 1.0 - minPriceRatio - gapRatio -
                                 (HasMacdIndicator ? macdPanelRatio : 0);
                    rsi = Math.Clamp(rsi, minRsiRatio, Math.Max(minRsiRatio, maxSum - minVolumeRatio));
                    volume = sum - rsi;
                    if (volume < minVolumeRatio)
                    {
                        volume = minVolumeRatio;
                        rsi = sum - volume;
                    }
                    rsiPanelRatio = rsi;
                    volumePanelRatio = volume;
                    break;
                }

                case LowerPanelSplitter.PriceStochastic when HasStochasticIndicator && !HasRsiIndicator && !HasMacdIndicator:
                    stochasticPanelRatio = Math.Clamp(lowerPanelResizeStartStochasticRatio - deltaRatio, minRsiRatio, Math.Max(minRsiRatio, 1.0 - minPriceRatio - gapRatio - (volumePanelVisible ? volumePanelRatio : 0)));
                    break;

                case LowerPanelSplitter.RsiStochastic when HasRsiIndicator && HasStochasticIndicator && !HasMacdIndicator:
                case LowerPanelSplitter.MacdStochastic when HasMacdIndicator && HasStochasticIndicator:
                {
                    var sum = (activeLowerPanelSplitter == LowerPanelSplitter.RsiStochastic ? lowerPanelResizeStartRsiRatio : lowerPanelResizeStartMacdRatio) + lowerPanelResizeStartStochasticRatio;
                    var first = (activeLowerPanelSplitter == LowerPanelSplitter.RsiStochastic ? lowerPanelResizeStartRsiRatio : lowerPanelResizeStartMacdRatio) + deltaRatio;
                    var second = sum - first;
                    first = Math.Max(0.06, first); second = Math.Max(0.06, second);
                    var scale = sum / (first + second); first *= scale; second *= scale;
                    if (activeLowerPanelSplitter == LowerPanelSplitter.RsiStochastic) { rsiPanelRatio = first; stochasticPanelRatio = second; }
                    else { macdPanelRatio = first; stochasticPanelRatio = second; }
                    break;
                }

                case LowerPanelSplitter.StochasticVolume when HasStochasticIndicator && volumePanelVisible:
                {
                    var sum = lowerPanelResizeStartStochasticRatio + lowerPanelResizeStartVolumeRatio;
                    var stochastic = Math.Max(0.06, lowerPanelResizeStartStochasticRatio + deltaRatio);
                    var volume = Math.Max(minVolumeRatio, sum - stochastic);
                    stochastic = sum - volume;
                    stochasticPanelRatio = stochastic;
                    volumePanelRatio = volume;
                    break;
                }

                case LowerPanelSplitter.MacdVolume when HasMacdIndicator && volumePanelVisible:
                {
                    var sum = lowerPanelResizeStartMacdRatio + lowerPanelResizeStartVolumeRatio;
                    var macd = lowerPanelResizeStartMacdRatio + deltaRatio;
                    var volume = sum - macd;
                    var maxSum = 1.0 - minPriceRatio - gapRatio -
                                 (HasRsiIndicator ? rsiPanelRatio : 0);
                    macd = Math.Clamp(macd, minMacdRatio, Math.Max(minMacdRatio, maxSum - minVolumeRatio));
                    volume = sum - macd;
                    if (volume < minVolumeRatio)
                    {
                        volume = minVolumeRatio;
                        macd = sum - volume;
                    }
                    macdPanelRatio = macd;
                    volumePanelRatio = volume;
                    break;
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ZoomX(e.Delta > 0 ? 0.80 : 1.25);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            var plot = GetPlotRectangle();
            if (e.Button == MouseButtons.Left &&
                e.Y >= plot.Top &&
                e.Y <= plot.Bottom &&
                (e.X <= plot.Left + 12 || plot.Contains(e.Location)))
            {
                FitVerticalView();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                var indicatorIndex = HitTestIndicator(e.Location);
                if (indicatorIndex >= 0)
                {
                    CancelDrawing();
                    Focus();
                    Invalidate();
                    return;
                }

                selectedIndicatorIndex = -1;
                CancelDrawing();
                return;
            }
            if (extraInputHandled) { extraInputHandled = false; return; }
            if (e.Button != MouseButtons.Left) return;
            if (testMode && !testStartSelected)
            {
                SetTestEndFromMouse(e.Location);
                Focus();
                return;
            }
            if (ExtraDrawingActive || extraDrawingInProgress || extraDraggingHandleActive)
            {
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = false;
                return;
            }
            if (activeDrawingTool != ChartDrawingTool.None) { BeginOrCompleteDrawing(e.Location); return; }

            var plot = GetPlotRectangle();
            var plotLeft = plot.Left;
            var plotBottom = plot.Bottom;

            // تمام نوار محور افقی، از داخل نمودار تا قبل از پنل حجم،
            // ناحیه زوم افقی است؛ لازم نیست ماوس دقیقاً روی خود خط محور باشد.
            // فقط نوار باریکِ وسط جداکننده برای تغییر ارتفاع پنل حجم محفوظ می‌ماند.
            var volumePlot = GetVolumePlotRectangle();

            // محور زمان واقعاً در پایین پنل حجم قرار دارد، نه در یک مختصات
            // ثابت نسبت به Height. بنابراین کل ناحیه محور زمان (خط محور و
            // برچسب‌های زیر آن) باید با کلیک چپ قابل Drag باشد.
            // این کار به‌خصوص بعد از تغییر ارتفاع پنل حجم ضروری است.
            var volumePlotForAxis = GetVolumePlotRectangle();
            var timeAxisBottom = volumePlotForAxis == Rectangle.Empty
                ? plotBottom
                : volumePlotForAxis.Bottom;
            var axisBandTop = Math.Max(0, timeAxisBottom);

            // همه‌ی پنل‌های پایین با Splitter قابل تغییر اندازه هستند.
            var splitter = GetLowerPanelSplitterAt(e.Location.Y);
            if (splitter != LowerPanelSplitter.None)
            {
                lowerPanelResizeDrag = true;
                activeLowerPanelSplitter = splitter;
                lowerPanelResizeStartY = e.Location.Y;
                lowerPanelResizeStartRsiRatio = rsiPanelRatio;
                lowerPanelResizeStartVolumeRatio = volumePanelRatio;
                lowerPanelResizeStartMacdRatio = macdPanelRatio;
                lowerPanelResizeStartStochasticRatio = stochasticPanelRatio;
                Capture = true;
                Cursor = Cursors.SizeNS;
                return;
            }

            horizontalAxisDrag =
                e.Y >= axisBandTop &&
                e.Y < Height &&
                e.X >= plotLeft;

            if (horizontalAxisDrag)
            {
                panning = false;
                lowerPanelResizeDrag = false;
                horizontalAxisStartPoint = e.Location;
                horizontalAxisStartVisibleCount = Math.Max(2, visibleCount);

                var axisStep = plot.Width / (double)Math.Max(2, visibleCount);
                var axisInitialOffset = -plot.Width * 0.25;
                var axisRelativeX =
                    (e.X - plotLeft - axisInitialOffset - horizontalPanOffset) / axisStep - 0.5;
                horizontalAxisCenterIndex = firstIndex + axisRelativeX;

                Capture = true;
                Cursor = Cursors.SizeWE;
                return;
            }

            var handleIndex = HitTestDrawingHandle(e.Location, plot, out var handle);
            if (handleIndex >= 0)
            {
                selectedDrawingIndex = handleIndex;
                draggingDrawingIndex = handleIndex;
                draggingHandle = handle;
                draggingLastPoint = e.Location;
                Focus(); Capture = true; Cursor = Cursors.SizeAll; Invalidate(); return;
            }
            var hitIndex = HitTestDrawing(e.Location, plot);
            if (hitIndex >= 0)
            {
                selectedDrawingIndex = hitIndex;
                draggingDrawingIndex = hitIndex;
                draggingHandle = 0;
                draggingLastPoint = e.Location;
                Focus(); Capture = true; Cursor = Cursors.SizeAll; Invalidate(); return;
            }

            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
            verticalAxisDrag = e.X <= plotLeft && e.Y <= plotBottom;
            if (verticalAxisDrag)
            {
                panning = false;
                verticalAxisStartPoint = e.Location;
                verticalAxisStartZoom = verticalZoom;
                Capture = true; Cursor = Cursors.SizeNS; return;
            }
            if (points.Count > 1)
            {
                panning = true;
                panStartPoint = e.Location;
                panStartFirstIndex = firstIndex;
                panStartVerticalPanOffset = verticalPanOffset;
                panStartHorizontalOffset = horizontalPanOffset;
                var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
                var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
                panStartVerticalRange = visible.Count > 0
                    ? Math.Max(visible.Max(x => x.High) - visible.Min(x => x.Low), 1e-9) / verticalZoom * 1.10
                    : 1.0;
                Capture = true; Cursor = Cursors.SizeAll;
            }
            Focus();
        }

        private void BeginOrCompleteDrawing(Point location)
        {
            if (!IsInsidePlot(location)) return;
            if (activeDrawingTool == ChartDrawingTool.HorizontalLine || activeDrawingTool == ChartDrawingTool.VerticalLine)
            {
                AddDrawing(location, location);
                drawingInProgress = false; drawingStage = 0; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty;
                activeDrawingTool = ChartDrawingTool.None; Cursor = Cursors.Default; Invalidate(); return;
            }
            if (activeDrawingTool == ChartDrawingTool.TrendChannel)
            {
                if (!drawingInProgress)
                {
                    drawingStartPoint = location; drawingCurrentPoint = location; drawingSecondPoint = Point.Empty; drawingStage = 1; drawingInProgress = true; Invalidate(); return;
                }
                if (drawingStage == 1)
                {
                    drawingSecondPoint = location; drawingCurrentPoint = location; drawingStage = 2; Invalidate(); return;
                }
                AddDrawing(drawingStartPoint, drawingSecondPoint, location);
                drawingInProgress = false; drawingStage = 0; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty; drawingSecondPoint = Point.Empty;
                activeDrawingTool = ChartDrawingTool.None; Cursor = Cursors.Default; Invalidate(); return;
            }
            if (!drawingInProgress)
            {
                drawingStartPoint = location; drawingCurrentPoint = location; drawingStage = 1; drawingInProgress = true; Invalidate(); return;
            }
            drawingCurrentPoint = location;
            AddDrawing(drawingStartPoint, drawingCurrentPoint);
            drawingInProgress = false; drawingStage = 0; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty;
            activeDrawingTool = ChartDrawingTool.None; Cursor = Cursors.Default; Invalidate();
        }

        private int GetDrawingLayoutCount()
        {
            var naturalEndIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            return Math.Max(1, naturalEndIndex - firstIndex);
        }

        private bool IsInsidePlot(Point point) => GetPlotRectangle().Contains(point);
        private void AddDrawing(Point start, Point end) => AddDrawing(start, end, Point.Empty);

        private void AddDrawing(Point start, Point end, Point third)
        {
            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0) return;
            GetVerticalRange(visible, out var min, out var max);
            var layoutCount = GetDrawingLayoutCount();
            var x1 = ScreenToDataX(start.X, plot, layoutCount) + firstIndex;
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, layoutCount) + firstIndex;
            var y2 = ScreenToPrice(end.Y, plot, min, max);
            switch (activeDrawingTool)
            {
                case ChartDrawingTool.TrendLine:
                case ChartDrawingTool.TrendLineWithArrow:
                case ChartDrawingTool.Rectangle:
                    drawings.Add(new ChartDrawing
                    {
                        Tool = activeDrawingTool,
                        X1 = x1, Y1 = y1,
                        X2 = x2, Y2 = y2,
                        Date1 = DataXToDate(x1),
                        Date2 = DataXToDate(x2)
                    });
                    break;
                case ChartDrawingTool.TrendChannel:
                    if (third == Point.Empty) return;
                    var x3 = ScreenToDataX(third.X, plot, layoutCount) + firstIndex;
                    var y3 = ScreenToPrice(third.Y, plot, min, max);
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, X3 = x3, Y3 = y3 }); break;
                case ChartDrawingTool.HorizontalLine:
                    drawings.Add(new ChartDrawing
                    {
                        Tool = activeDrawingTool,
                        X1 = x1, Y1 = y1, X2 = x2, Y2 = y1,
                        Date1 = DataXToDate(x1), Date2 = DataXToDate(x2)
                    }); break;
                case ChartDrawingTool.VerticalLine:
                    drawings.Add(new ChartDrawing
                    {
                        Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x1, Y2 = y2,
                        Date1 = DataXToDate(x1), Date2 = DataXToDate(x1)
                    }); break;
                case ChartDrawingTool.HorizontalRay:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 }); break;
            }

            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // روی تمام جداکننده‌های پنل‌ها شکل Splitter نشان داده می‌شود.
            if (!Capture && GetLowerPanelSplitterAt(e.Location.Y) != LowerPanelSplitter.None)
            {
                Cursor = Cursors.SizeNS;
            }
            else if (!Capture && !horizontalAxisDrag && !verticalAxisDrag && !panning)
            {
                Cursor = Cursors.Default;
            }
            if (extraInputHandled) { extraInputHandled = false; return; }
            if (lowerPanelResizeDrag && Capture)
            {
                ResizeLowerPanels(e.Location.Y);
                Invalidate();
                return;
            }

            if (showCrosshair)
            {
                var plot = GetPlotRectangle();
                var plotLeft = plot.Left;
                var plotRight = plot.Right;
                var plotTop = plot.Top;
                var plotBottom = plot.Bottom;
                var plotWidth = Math.Max(1, plotRight - plotLeft);
                var step = plotWidth / (double)Math.Max(1, visibleCount);
                var initialOffset = -plotWidth * 0.25;
                var relativeX = e.X - plotLeft - initialOffset - horizontalPanOffset;
                var nearest = (int)Math.Round(relativeX / step - 0.5);
                var oldCrosshairDate = CrosshairDate;
                syncedCrosshairDate = null;
                crosshairPosition = Math.Clamp(nearest, 0, Math.Max(0, visibleCount - 1));
                crosshairIndex = (int)Math.Round(crosshairPosition);
                crosshairPoint = new Point((int)Math.Round(plotLeft + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset), Math.Clamp(e.Y, plotTop, plotBottom));
                Invalidate();
                if (CrosshairDate.HasValue && CrosshairDate != oldCrosshairDate)
                    CrosshairDateChanged?.Invoke(this, EventArgs.Empty);
            }
            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None) { drawingCurrentPoint = e.Location; Invalidate(); return; }
            if (draggingDrawingIndex >= 0 && draggingDrawingIndex < drawings.Count && Capture)
            {
                MoveOrResizeDrawing(draggingDrawingIndex, draggingHandle, e.Location); draggingLastPoint = e.Location; Invalidate(); return;
            }
            if (horizontalAxisDrag && Capture && points.Count > 1)
            {
                var delta = e.X - horizontalAxisStartPoint.X;
                var factor = Math.Exp(-delta / 900.0);
                var newCount = Math.Clamp((int)Math.Round(horizontalAxisStartVisibleCount * factor), 2, points.Count);

                if (testMode && testStartSelected)
                {
                    visibleCount = newCount;
                    KeepTestAnchorFixed();
                }
                else
                {
                    // محور افقی فقط زوم است؛ نقطه‌ای که ماوس هنگام شروع
                    // Drag روی محور گرفته، لنگر زوم است. در نتیجه با حرکت ماوس
                    // فقط تعداد کندل‌ها تغییر می‌کند و آن نقطه روی همان مختصات
                    // صفحه باقی می‌ماند؛ هیچ Pan افقی انجام نمی‌شود.
                    visibleCount = newCount;

                    var zoomPlot = GetPlotRectangle();
                    var zoomStep = zoomPlot.Width / (double)Math.Max(2, newCount);
                    var zoomInitialOffset = -zoomPlot.Width * 0.25;

                    // لنگر همان نقطه‌ای است که Drag روی محور شروع شده است.
                    // نباید با حرکت بعدی ماوس، لنگر دوباره محاسبه شود؛
                    // وگرنه نمودار به‌جای زوم حول نقطه شروع، جابه‌جا می‌شود.
                    var anchorRelativeIndex =
                        (horizontalAxisStartPoint.X - zoomPlot.Left - zoomInitialOffset)
                        / zoomStep - 0.5;

                    firstIndex = Math.Clamp(
                        (int)Math.Round(horizontalAxisCenterIndex - anchorRelativeIndex),
                        0,
                        Math.Max(0, points.Count - newCount));

                    horizontalPanOffset = 0;
                }

                crosshairIndex = -1;
                Invalidate();
                return;
            }
            if (verticalAxisDrag && Capture && points.Count > 1)
            {
                var delta = e.Y - verticalAxisStartPoint.Y;
                verticalZoom = Math.Clamp(verticalAxisStartZoom * Math.Exp(delta / 200.0), 0.1, 20.0); Invalidate(); return;
            }
            if (panning && Capture && points.Count > 1)
            {
                var dx = e.X - panStartPoint.X;
                var dy = e.Y - panStartPoint.Y;
                var step = Math.Max(1.0, GetPlotRectangle().Width / (double)Math.Max(1, visibleCount));
                var indexDelta = (int)Math.Round(-dx / step);
                firstIndex = Math.Clamp(panStartFirstIndex + indexDelta, 0, Math.Max(0, points.Count - visibleCount));
                var requestedVerticalPanOffset =
                    panStartVerticalPanOffset + dy / Math.Max(1.0, GetPlotRectangle().Height) * panStartVerticalRange;

                // فضای خالی بالای نمودار فقط در حالت Fit تعیین‌کننده است.
                // هنگام Drag، چارت باید بتواند از لبه بالای Plot عبور کند
                // و توسط Clip بریده/پنهان شود؛ درست مانند حرکت از پایین.
                verticalPanOffset = requestedVerticalPanOffset;

                // firstIndex جابه‌جایی افقی کندل‌ها را کنترل می‌کند.
                // نباید dx دوباره به مختصات صفحه اضافه شود؛ این کار باعث
                // خروج چارت از محدوده Plot و بریده شدن آن توسط Clip می‌شد.
                horizontalPanOffset = panStartHorizontalOffset;
                Invalidate();
                NotifyViewChanged();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (extraInputHandled) { extraInputHandled = false; return; }
            if (e.Button == MouseButtons.Left)
            {
                if (lowerPanelResizeDrag)
                {
                    lowerPanelResizeDrag = false;
                    activeLowerPanelSplitter = LowerPanelSplitter.None;
                    Capture = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                    NotifyVolumeSettingsChanged();
                    return;
                }

                if (draggingDrawingIndex >= 0)
                {
                    draggingDrawingIndex = -1; draggingHandle = 0; Capture = false; Cursor = Cursors.Default; Invalidate();
                    AnalysisChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }
                if (horizontalAxisDrag || verticalAxisDrag || panning)
                {
                    horizontalAxisDrag = false; verticalAxisDrag = false; panning = false; Capture = false; Cursor = Cursors.Default; Invalidate();
                }
            }
        }
    }
}