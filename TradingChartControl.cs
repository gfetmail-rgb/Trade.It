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
        private double volumePanelRatio = 0.22;
        private int volumePanelGap = 8;
        private bool volumePanelResizeDrag;
        private int volumePanelResizeStartY;
        private double volumePanelResizeStartRatio;
        private bool volumePanelVisible = true;
        private bool testMode;
        private bool testStartSelected;
        private int testEndIndex = -1;
        private int testAnchorIndex = -1;
        private float testAnchorScreenX;

        private sealed class ChartDrawing
        {
            public ChartDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public double X3 { get; set; }
            public double Y3 { get; set; }
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

        public void SetData(IEnumerable<TradingChartPoint> data, string? symbol = null)
        {
            chartSymbol = symbol?.Trim() ?? string.Empty;
            points.Clear();
            points.AddRange(data.OrderBy(x => x.Date));

            // همگام‌سازی وضعیت داده‌های ابزارهای Extra با داده‌ی جدید.
            // این کار مانع می‌شود یک Paint/Refresh موقت (مثلاً هنگام باز شدن پنجره متن)
            // به اشتباه Extra Drawingهای موجود مثل Pitchfork را پاک کند.
            extraDataCount = points.Count;
            extraFirstDate = points.Count > 0 ? points[0].Date : DateTime.MinValue;
            extraLastDate = points.Count > 0 ? points[^1].Date : DateTime.MinValue;

            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            testEndIndex = -1;
            testAnchorIndex = -1;
            testAnchorScreenX = 0f;
            verticalZoom = 1.0;
            verticalPanOffset = 0;
            horizontalPanOffset = 0;
            chartPanCompensation = 0;
            showCrosshair = true;
            crosshairIndex = -1;
            CancelDrawing();
            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
            Invalidate();
        }

        public string ChartSymbol => chartSymbol;

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
                    X1 = firstIndex + drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = firstIndex + drawing.X2,
                    Y2 = drawing.Y2,
                    X3 = firstIndex + drawing.X3,
                    Y3 = drawing.Y3
                });
            }

            foreach (var drawing in advancedDrawings)
            {
                document.AdvancedDrawings.Add(new ChartAnalysisDrawing
                {
                    Tool = drawing.Tool.ToString(),
                    X1 = firstIndex + drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = firstIndex + drawing.X2,
                    Y2 = drawing.Y2,
                    Text = drawing.Text
                });
            }

            foreach (var drawing in extraDrawings)
            {
                document.ExtraDrawings.Add(new ChartAnalysisDrawing
                {
                    Tool = drawing.Tool.ToString(),
                    X1 = firstIndex + drawing.X1,
                    Y1 = drawing.Y1,
                    X2 = firstIndex + drawing.X2,
                    Y2 = drawing.Y2,
                    X3 = firstIndex + drawing.X3,
                    Y3 = drawing.Y3
                });
            }

            return document;
        }

        public void RestoreAnalysisDocument(ChartAnalysisDocument document)
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
                    X1 = item.X1 - document.FirstIndex,
                    Y1 = item.Y1,
                    X2 = item.X2 - document.FirstIndex,
                    Y2 = item.Y2,
                    X3 = item.X3 - document.FirstIndex,
                    Y3 = item.Y3
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
                    X1 = item.X1 - document.FirstIndex,
                    Y1 = item.Y1,
                    X2 = item.X2 - document.FirstIndex,
                    Y2 = item.Y2,
                    Text = item.Text
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
                    X1 = item.X1 - document.FirstIndex,
                    Y1 = item.Y1,
                    X2 = item.X2 - document.FirstIndex,
                    Y2 = item.Y2,
                    X3 = item.X3 - document.FirstIndex,
                    Y3 = item.Y3
                });
            }

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
                document.VolumePanelRatio > 0 ? document.VolumePanelRatio : 0.22,
                0.10,
                0.45);

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
            volumePanelResizeDrag = false;
            draggingDrawingIndex = -1;
            draggingHandle = 0;

            if (enabled)
                CancelDrawing();

            Cursor = Cursors.Default;
            Focus();
            Invalidate();
        }

        public void StepTest(int delta)
        {
            if (!testMode || points.Count == 0 || !testStartSelected)
                return;

            if (testEndIndex < 0 || testAnchorIndex < 0)
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
            if (!testMode || testAnchorIndex < 0 || testEndIndex < 0 || points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var count = Math.Max(2, visibleCount);
            var step = plot.Width / (double)count;
            var initialOffset = -plot.Width * 0.25;

            var relativeIndex = (int)Math.Round(
                (testAnchorScreenX - plot.Left - initialOffset - horizontalPanOffset) / step - 0.5);

            relativeIndex = Math.Clamp(relativeIndex, 0, count - 1);

            var desiredFirst = testEndIndex - relativeIndex;

            // در مد تست لازم است حتی در انتهای داده نیز فضای خالی سمت راست
            // حفظ شود تا کندل لنگر دقیقاً در همان مختصات صفحه بماند.
            // محدود کردن firstIndex به points.Count - count باعث می‌شد
            // در حرکت با کلید راست، لنگر به سمت راست سر بخورد.
            firstIndex = Math.Clamp(
                desiredFirst,
                0,
                Math.Max(0, points.Count - 1));
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
            ApplyConfiguredVerticalFit();
            chartPanCompensation = 0;
            crosshairIndex = -1;
            EnsureChartPanCompensation();
            Invalidate();
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
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedDrawingIndex >= 0 && selectedDrawingIndex < drawings.Count)
            {
                drawings.RemoveAt(selectedDrawingIndex);
                selectedDrawingIndex = -1;
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

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ZoomX(e.Delta > 0 ? 0.80 : 1.25);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left && GetPlotRectangle().Contains(e.Location))
                FitVerticalView();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right) { CancelDrawing(); return; }
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

            // محور افقی در پایین‌ترین نوار کنترل چارت قرار دارد؛ وقتی پنل حجم
            // فعال است، این نوار بعد از پنل حجم است. قبلاً محدوده زوم را بین
            // plot قیمت و volumePlot.Top گذاشته بودیم که عملاً فقط همان فاصله
            // باریکِ جداکننده را قابل گرفتن می‌کرد.
            var axisBandTop = Math.Max(plotBottom, Height - 35);

            // Drag معمولی روی محور همیشه Zoom است.
            // تغییر ارتفاع پنل حجم فقط با Shift + Drag روی جداکننده انجام می‌شود.
            var onVolumeSeparator = volumePanelVisible &&
                                    IsVolumePanelSeparator(e.Location.Y) &&
                                    (ModifierKeys & Keys.Shift) == Keys.Shift;

            if (onVolumeSeparator)
            {
                volumePanelResizeDrag = true;
                volumePanelResizeStartY = e.Location.Y;
                volumePanelResizeStartRatio = volumePanelRatio;
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
                volumePanelResizeDrag = false;
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
            var x1 = ScreenToDataX(start.X, plot, layoutCount);
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, layoutCount);
            var y2 = ScreenToPrice(end.Y, plot, min, max);
            switch (activeDrawingTool)
            {
                case ChartDrawingTool.TrendLine:
                case ChartDrawingTool.TrendLineWithArrow:
                case ChartDrawingTool.Rectangle:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 }); break;
                case ChartDrawingTool.TrendChannel:
                    if (third == Point.Empty) return;
                    var x3 = ScreenToDataX(third.X, plot, layoutCount);
                    var y3 = ScreenToPrice(third.Y, plot, min, max);
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, X3 = x3, Y3 = y3 }); break;
                case ChartDrawingTool.HorizontalLine:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 }); break;
                case ChartDrawingTool.VerticalLine:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x1, Y2 = y2 }); break;
                case ChartDrawingTool.HorizontalRay:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 }); break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (extraInputHandled) { extraInputHandled = false; return; }
            if (volumePanelResizeDrag && Capture)
            {
                var delta = e.Location.Y - volumePanelResizeStartY;
                var totalHeight = Math.Max(120, Height - 35 - 15);
                var deltaRatio = -delta / (double)totalHeight;
                volumePanelRatio = Math.Clamp(
                    volumePanelResizeStartRatio + deltaRatio,
                    0.10,
                    0.45);
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
                crosshairIndex = Math.Clamp(nearest, 0, Math.Max(0, visibleCount - 1));
                crosshairPoint = new Point((int)Math.Round(plotLeft + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset), Math.Clamp(e.Y, plotTop, plotBottom));
                Invalidate();
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

                // فضای خالی بالای نمودار سقف مجاز حرکت عمودی را تعیین می‌کند.
                // بنابراین Drag نمی‌تواند High را از مرز فضای خالی
                // تنظیم‌شده بالاتر ببرد.
                verticalPanOffset = ClampVerticalPanOffset(requestedVerticalPanOffset);

                // firstIndex جابه‌جایی افقی کندل‌ها را کنترل می‌کند.
                // نباید dx دوباره به مختصات صفحه اضافه شود؛ این کار باعث
                // خروج چارت از محدوده Plot و بریده شدن آن توسط Clip می‌شد.
                horizontalPanOffset = panStartHorizontalOffset;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (extraInputHandled) { extraInputHandled = false; return; }
            if (e.Button == MouseButtons.Left)
            {
                if (volumePanelResizeDrag)
                {
                    volumePanelResizeDrag = false;
                    Capture = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                    NotifyVolumeSettingsChanged();
                    return;
                }

                if (draggingDrawingIndex >= 0)
                {
                    draggingDrawingIndex = -1; draggingHandle = 0; Capture = false; Cursor = Cursors.Default; Invalidate(); return;
                }
                if (horizontalAxisDrag || verticalAxisDrag || panning)
                {
                    horizontalAxisDrag = false; verticalAxisDrag = false; panning = false; Capture = false; Cursor = Cursors.Default; Invalidate();
                }
            }
        }
    }
}