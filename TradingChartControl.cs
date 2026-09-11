using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal enum TradingChartType
    {
        Candlestick,
        Line,
        Bar
    }

    internal enum ChartDrawingTool
    {
        None,
        TrendLine,
        HorizontalDoubleArrow,
        VerticalDoubleArrow,
        HorizontalRay,
        TrendLineWithArrow
    }

    internal sealed class TradingChartPoint
    {
        public DateTime Date { get; init; }
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
        private Point drawingStartPoint;
        private Point drawingCurrentPoint;

        private sealed class ChartDrawing
        {
            public ChartDrawingTool Tool { get; init; }
            public double X1 { get; init; }
            public double Y1 { get; init; }
            public double X2 { get; init; }
            public double Y2 { get; init; }
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

        public void SetData(IEnumerable<TradingChartPoint> data)
        {
            points.Clear();
            points.AddRange(data.OrderBy(x => x.Date));
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            horizontalPanOffset = 0.0;
            showCrosshair = true;
            crosshairIndex = -1;
            CancelDrawing();
            drawings.Clear();
            Invalidate();
        }

        public void SetChartType(TradingChartType type)
        {
            chartType = type;
            Invalidate();
        }

        public void ToggleGrid()
        {
            showGrid = !showGrid;
            Invalidate();
        }

        public bool GridVisible => showGrid;

        public void ToggleCrosshair()
        {
            showCrosshair = !showCrosshair;
            Invalidate();
        }

        public bool CrosshairVisible => showCrosshair;

        public ChartDrawingTool ActiveDrawingTool => activeDrawingTool;
        public bool DrawingInProgress => drawingInProgress;

        public void SetDrawingTool(ChartDrawingTool tool)
        {
            activeDrawingTool = tool;
            drawingInProgress = false;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            Focus();
            Cursor = tool == ChartDrawingTool.None ? Cursors.Default : Cursors.Cross;
            Invalidate();
        }

        public void CancelDrawing()
        {
            activeDrawingTool = ChartDrawingTool.None;
            drawingInProgress = false;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            Cursor = Cursors.Default;
            Invalidate();
        }

        public void ResetView()
        {
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            horizontalPanOffset = 0.0;
            crosshairIndex = -1;
            Invalidate();
        }

        public void ZoomX(double factor)
        {
            if (points.Count < 2)
                return;

            var oldCount = Math.Max(2, visibleCount);
            var newCount = Math.Clamp((int)Math.Round(oldCount * factor), 2, points.Count);
            if (newCount == oldCount)
                return;

            visibleCount = newCount;
            firstIndex = Math.Max(0, points.Count - newCount);
            crosshairIndex = -1;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
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
            var plotLeft = 55;
            var plotBottom = Height - 35;
            if (e.Button == MouseButtons.Left && e.X <= plotLeft && e.Y <= plotBottom)
                FitVerticalRange();
        }

        private void FitVerticalRange()
        {
            if (points.Count == 0)
                return;
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;
            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                if (activeDrawingTool != ChartDrawingTool.None || drawingInProgress)
                    CancelDrawing();
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            if (activeDrawingTool != ChartDrawingTool.None)
            {
                BeginOrCompleteDrawing(e.Location);
                return;
            }

            var plotLeft = 55;
            var plotBottom = Height - 35;
            horizontalAxisDrag = e.Y >= plotBottom && e.X >= plotLeft;
            verticalAxisDrag = e.X <= plotLeft && e.Y <= plotBottom;

            if (horizontalAxisDrag)
            {
                panning = false;
                horizontalAxisStartPoint = e.Location;
                horizontalAxisStartVisibleCount = Math.Max(2, visibleCount);
                horizontalAxisCenterIndex = firstIndex + horizontalAxisStartVisibleCount / 2.0;
                Capture = true;
                Cursor = Cursors.SizeWE;
                return;
            }

            if (verticalAxisDrag)
            {
                panning = false;
                verticalAxisStartPoint = e.Location;
                verticalAxisStartZoom = verticalZoom;
                Capture = true;
                Cursor = Cursors.SizeNS;
                return;
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
                if (visible.Count > 0)
                {
                    var rawRange = visible.Max(x => x.High) - visible.Min(x => x.Low);
                    panStartVerticalRange = Math.Max(rawRange, 1e-9) / verticalZoom * 1.10;
                }
                else
                {
                    panStartVerticalRange = 1.0;
                }
                Capture = true;
                Cursor = Cursors.SizeAll;
            }
        }

        private void BeginOrCompleteDrawing(Point location)
        {
            if (!IsInsidePlot(location))
                return;

            if (!drawingInProgress)
            {
                drawingStartPoint = location;
                drawingCurrentPoint = location;
                drawingInProgress = true;
                Invalidate();
                return;
            }

            drawingCurrentPoint = location;
            AddDrawing(drawingStartPoint, drawingCurrentPoint);
            drawingInProgress = false;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            activeDrawingTool = ChartDrawingTool.None;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private bool IsInsidePlot(Point point) => GetPlotRectangle().Contains(point);

        private void AddDrawing(Point start, Point end)
        {
            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;

            GetVerticalRange(visible, out var min, out var max);
            var x1 = ScreenToDataX(start.X, plot, visible.Count);
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, visible.Count);
            var y2 = ScreenToPrice(end.Y, plot, min, max);

            switch (activeDrawingTool)
            {
                case ChartDrawingTool.TrendLine:
                case ChartDrawingTool.TrendLineWithArrow:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 });
                    break;
                case ChartDrawingTool.HorizontalDoubleArrow:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 });
                    break;
                case ChartDrawingTool.VerticalDoubleArrow:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x1, Y2 = y2 });
                    break;
                case ChartDrawingTool.HorizontalRay:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 });
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (showCrosshair)
            {
                crosshairPoint = e.Location;
                var plotLeft = 55;
                var plotRight = Math.Max(plotLeft, Width - 15);
                var plotTop = 15;
                var plotBottom = Math.Max(plotTop, Height - 35);
                var plotWidth = Math.Max(1, plotRight - plotLeft);
                var step = plotWidth / (double)Math.Max(1, visibleCount);
                var initialOffset = -plotWidth * 0.25;
                var relativeX = e.X - plotLeft - initialOffset - horizontalPanOffset;
                var nearest = (int)Math.Round(relativeX / step - 0.5);
                crosshairIndex = Math.Clamp(nearest, 0, Math.Max(0, visibleCount - 1));
                crosshairPoint = new Point(
                    (int)Math.Round(plotLeft + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset),
                    Math.Clamp(e.Y, plotTop, plotBottom));
                Invalidate();
            }

            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None)
            {
                drawingCurrentPoint = e.Location;
                Invalidate();
                return;
            }

            if (horizontalAxisDrag && Capture && points.Count > 1)
            {
                var delta = e.X - horizontalAxisStartPoint.X;
                var factor = Math.Exp(-delta / 300.0);
                var newCount = Math.Clamp((int)Math.Round(horizontalAxisStartVisibleCount * factor), 2, points.Count);
                visibleCount = newCount;
                firstIndex = Math.Clamp((int)Math.Round(horizontalAxisCenterIndex - newCount / 2.0), 0, Math.Max(0, points.Count - newCount));
                crosshairIndex = -1;
                Invalidate();
                return;
            }

            if (verticalAxisDrag && Capture && points.Count > 1)
            {
                var delta = verticalAxisStartPoint.Y - e.Y;
                verticalZoom = Math.Clamp(verticalAxisStartZoom * Math.Exp(delta / 700.0), 0.15, 8.0);
                Invalidate();
                return;
            }

            if (panning && Capture && points.Count > 1)
            {
                var horizontalDelta = e.X - panStartPoint.X;
                horizontalPanOffset = panStartHorizontalOffset + horizontalDelta;
                var plotWidth = Math.Max(1, Width - 70);
                horizontalPanOffset = Math.Clamp(horizontalPanOffset, -plotWidth, plotWidth);
                var verticalDelta = e.Y - panStartPoint.Y;
                if (Math.Abs(verticalDelta) >= 0.5)
                {
                    var plotHeight = Math.Max(1, Height - 50);
                    verticalPanOffset = panStartVerticalPanOffset + verticalDelta * panStartVerticalRange / plotHeight;
                }
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = false;
                if (!drawingInProgress && activeDrawingTool == ChartDrawingTool.None)
                    Cursor = Cursors.Default;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            if (points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;

            GetVerticalRange(visible, out var min, out var max);

            using var gridPen = new Pen(Color.FromArgb(225, 225, 225), 1);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(Color.FromArgb(35, 150, 80));
            using var fallingBrush = new SolidBrush(Color.FromArgb(205, 70, 70));
            using var linePen = new Pen(Color.FromArgb(35, 90, 160), 1.6f);
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);
            using var crosshairLabelBackBrush = new SolidBrush(Color.FromArgb(45, 45, 45));
            using var crosshairLabelTextBrush = new SolidBrush(Color.White);

            if (showGrid)
            {
                for (var i = 0; i <= 5; i++)
                {
                    var y = plot.Top + plot.Height * i / 5f;
                    e.Graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                }
                var verticalGridCount = Math.Min(10, Math.Max(2, visible.Count));
                for (var i = 0; i <= verticalGridCount; i++)
                {
                    var x = plot.Left + plot.Width * i / (float)verticalGridCount;
                    e.Graphics.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
                }
            }

            for (var i = 0; i <= 5; i++)
            {
                var y = plot.Top + plot.Height * i / 5f;
                var value = max - (max - min) * i / 5.0;
                var text = value.ToString("0.##");
                var size = e.Graphics.MeasureString(text, axisTextFont);
                e.Graphics.DrawString(text, axisTextFont, textBrush, 4, y - size.Height / 2f);
            }

            e.Graphics.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            var step = plot.Width / (double)Math.Max(1, visible.Count);
            var initialOffset = -plot.Width * 0.25;
            double X(int index) => plot.Left + step * (index + 0.5) + initialOffset + horizontalPanOffset;
            double Y(double value) => plot.Bottom - (value - min) / (max - min) * plot.Height;

            if (chartType == TradingChartType.Line)
            {
                for (var i = 1; i < visible.Count; i++)
                    e.Graphics.DrawLine(linePen, (float)X(i - 1), (float)Y(visible[i - 1].Close), (float)X(i), (float)Y(visible[i].Close));
            }
            else if (chartType == TradingChartType.Bar)
            {
                var tick = Math.Max(2, (int)Math.Round(step * 0.25));
                using var upPen = new Pen(Color.FromArgb(35, 150, 80), 1.2f);
                using var downPen = new Pen(Color.FromArgb(205, 70, 70), 1.2f);
                for (var i = 0; i < visible.Count; i++)
                {
                    var p = visible[i];
                    var x = (float)X(i);
                    var pen = p.Close >= p.Open ? upPen : downPen;
                    e.Graphics.DrawLine(pen, x, (float)Y(p.High), x, (float)Y(p.Low));
                    e.Graphics.DrawLine(pen, x - tick, (float)Y(p.Open), x, (float)Y(p.Open));
                    e.Graphics.DrawLine(pen, x, (float)Y(p.Close), x + tick, (float)Y(p.Close));
                }
            }
            else
            {
                var bodyWidth = Math.Max(3, Math.Min(16, step * 0.65));
                using var wickUpPen = new Pen(Color.FromArgb(35, 150, 80), 1.2f);
                using var wickDownPen = new Pen(Color.FromArgb(205, 70, 70), 1.2f);
                for (var i = 0; i < visible.Count; i++)
                {
                    var p = visible[i];
                    var x = (float)X(i);
                    var rising = p.Close >= p.Open;
                    var yHigh = (float)Y(p.High);
                    var yLow = (float)Y(p.Low);
                    var yOpen = (float)Y(p.Open);
                    var yClose = (float)Y(p.Close);
                    var topBody = Math.Min(yOpen, yClose);
                    var bodyHeight = Math.Max(1, Math.Abs(yClose - yOpen));
                    var rect = new RectangleF((float)(x - bodyWidth / 2), topBody, (float)bodyWidth, bodyHeight);
                    e.Graphics.DrawLine(rising ? wickUpPen : wickDownPen, x, yHigh, x, yLow);
                    e.Graphics.FillRectangle(rising ? risingBrush : fallingBrush, rect);
                    e.Graphics.DrawRectangle(rising ? wickUpPen : wickDownPen, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }

            DrawDrawings(e.Graphics, plot, visible.Count, min, max);

            var labelCount = Math.Min(6, visible.Count);
            for (var i = 0; i < labelCount; i++)
            {
                var index = labelCount == 1 ? 0 : (int)Math.Round(i * (visible.Count - 1.0) / (labelCount - 1));
                var x = (float)X(index);
                var text = visible[index].Date == default ? string.Empty : visible[index].Date.ToString("yyyy/MM/dd");
                if (string.IsNullOrEmpty(text))
                    continue;
                var size = e.Graphics.MeasureString(text, axisTextFont);
                e.Graphics.DrawString(text, axisTextFont, textBrush, x - size.Width / 2, plot.Bottom + 7);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var p = visible[crosshairIndex];
                var x = (float)X(crosshairIndex);
                var y = Math.Clamp(crosshairPoint.Y, plot.Top, plot.Bottom);
                var price = min + (plot.Bottom - y) / (double)plot.Height * (max - min);
                using var crossPen = new Pen(Color.FromArgb(100, 80, 80, 80), 1) { DashStyle = DashStyle.Dash };
                e.Graphics.DrawLine(crossPen, plot.Left, y, plot.Right, y);
                e.Graphics.DrawLine(crossPen, x, plot.Top, x, plot.Bottom);
                var priceText = price.ToString("0.##");
                var priceSize = e.Graphics.MeasureString(priceText, axisTextFont);
                var priceRect = new RectangleF(2, y - priceSize.Height / 2f - 2, priceSize.Width + 6, priceSize.Height + 4);
                e.Graphics.FillRectangle(crosshairLabelBackBrush, priceRect);
                e.Graphics.DrawString(priceText, axisTextFont, crosshairLabelTextBrush, priceRect.X + 3, priceRect.Y + 2);
                if (p.Date != default)
                {
                    var dateText = p.Date.ToString("yyyy/MM/dd");
                    var dateSize = e.Graphics.MeasureString(dateText, axisTextFont);
                    var dateRect = new RectangleF(x - dateSize.Width / 2f - 3, plot.Bottom + 4, dateSize.Width + 6, dateSize.Height + 4);
                    e.Graphics.FillRectangle(crosshairLabelBackBrush, dateRect);
                    e.Graphics.DrawString(dateText, axisTextFont, crosshairLabelTextBrush, dateRect.X + 3, dateRect.Y + 2);
                }
                var infoText = $"Open: {p.Open:0.##}   High: {p.High:0.##}   Low: {p.Low:0.##}   Close: {p.Close:0.##}   Volume: {p.Volume:N0}";
                using var infoFont = new Font(Font.FontFamily, Math.Max(8.0f, Font.Size - 1.0f), Font.Style);
                var infoSize = e.Graphics.MeasureString(infoText, infoFont);
                var infoRect = new RectangleF(plot.Left + 6, plot.Top + 5, infoSize.Width + 10, infoSize.Height + 6);
                e.Graphics.FillRectangle(crosshairLabelBackBrush, infoRect);
                e.Graphics.DrawString(infoText, infoFont, crosshairLabelTextBrush, infoRect.X + 5, infoRect.Y + 3);
            }

            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None)
                DrawDrawingPreview(e.Graphics, plot, visible.Count, min, max);
        }

        private Rectangle GetPlotRectangle() => new(55, 15, Math.Max(1, Width - 70), Math.Max(1, Height - 50));

        private void GetVerticalRange(IReadOnlyList<TradingChartPoint> visible, out double min, out double max)
        {
            min = visible.Min(x => x.Low);
            max = visible.Max(x => x.High);
            if (max <= min)
            {
                max += 1;
                min -= 1;
            }
            var center = (max + min) / 2.0 + verticalPanOffset;
            var halfRange = (max - min) / 2.0 / verticalZoom;
            min = center - halfRange;
            max = center + halfRange;
            var margin = (max - min) * 0.05;
            min -= margin;
            max += margin;
        }

        private double ScreenToDataX(float screenX, Rectangle plot, int count)
        {
            var step = plot.Width / (double)Math.Max(1, count);
            var initialOffset = -plot.Width * 0.25;
            var local = (screenX - plot.Left - initialOffset - horizontalPanOffset) / step - 0.5;
            return firstIndex + local;
        }

        private double ScreenToPrice(float screenY, Rectangle plot, double min, double max)
        {
            return min + (plot.Bottom - screenY) / (double)plot.Height * (max - min);
        }

        private PointF DataToScreen(double dataX, double price, Rectangle plot, int count, double min, double max)
        {
            var step = plot.Width / (double)Math.Max(1, count);
            var initialOffset = -plot.Width * 0.25;
            var x = plot.Left + step * (dataX - firstIndex + 0.5) + initialOffset + horizontalPanOffset;
            var y = plot.Bottom - (price - min) / (max - min) * plot.Height;
            return new PointF((float)x, (float)y);
        }

        private void DrawDrawings(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            if (drawings.Count == 0)
                return;
            using var drawingPen = new Pen(Color.FromArgb(30, 90, 160), 1.8f);
            foreach (var drawing in drawings)
                DrawSingleDrawing(g, drawingPen, drawing, plot, visibleCountForDrawing, min, max);
        }

        private void DrawDrawingPreview(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var preview = new ChartDrawing
            {
                Tool = activeDrawingTool,
                X1 = ScreenToDataX(drawingStartPoint.X, plot, visibleCountForDrawing),
                Y1 = ScreenToPrice(drawingStartPoint.Y, plot, min, max),
                X2 = ScreenToDataX(drawingCurrentPoint.X, plot, visibleCountForDrawing),
                Y2 = ScreenToPrice(drawingCurrentPoint.Y, plot, min, max)
            };
            using var previewPen = new Pen(Color.FromArgb(110, 30, 90, 160), 1.6f) { DashStyle = DashStyle.Dash };
            DrawSingleDrawing(g, previewPen, preview, plot, visibleCountForDrawing, min, max);
        }

        private void DrawSingleDrawing(Graphics g, Pen pen, ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
            switch (drawing.Tool)
            {
                case ChartDrawingTool.TrendLine:
                    g.DrawLine(pen, start, end);
                    break;
                case ChartDrawingTool.TrendLineWithArrow:
                    g.DrawLine(pen, start, end);
                    DrawArrowHead(g, pen, start, end);
                    break;
                case ChartDrawingTool.HorizontalDoubleArrow:
                    end.Y = start.Y;
                    g.DrawLine(pen, start, end);
                    DrawArrowHead(g, pen, start, end);
                    DrawArrowHead(g, pen, end, start);
                    break;
                case ChartDrawingTool.VerticalDoubleArrow:
                    end.X = start.X;
                    g.DrawLine(pen, start, end);
                    DrawArrowHead(g, pen, start, end);
                    DrawArrowHead(g, pen, end, start);
                    break;
                case ChartDrawingTool.HorizontalRay:
                    end.Y = start.Y;
                    var direction = end.X >= start.X ? 1f : -1f;
                    var rayEnd = new PointF(direction > 0 ? plot.Right : plot.Left, start.Y);
                    g.DrawLine(pen, start, rayEnd);
                    break;
            }
        }

        private static void DrawArrowHead(Graphics g, Pen basePen, PointF tip, PointF from)
        {
            var dx = tip.X - from.X;
            var dy = tip.Y - from.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length < 0.5)
                return;
            const float size = 8f;
            var ux = (float)(dx / length);
            var uy = (float)(dy / length);
            var px = -uy;
            var py = ux;
            var left = new PointF(tip.X - ux * size + px * size * 0.55f, tip.Y - uy * size + py * size * 0.55f);
            var right = new PointF(tip.X - ux * size - px * size * 0.55f, tip.Y - uy * size - py * size * 0.55f);
            g.DrawLine(basePen, tip, left);
            g.DrawLine(basePen, tip, right);
        }
    }
}
