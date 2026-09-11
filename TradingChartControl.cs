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
        TrendChannel,
        HorizontalDoubleArrow,
        VerticalDoubleArrow,
        HorizontalRay,
        TrendLineWithArrow,
        Rectangle
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

        public void SetData(IEnumerable<TradingChartPoint> data)
        {
            points.Clear();
            points.AddRange(data.OrderBy(x => x.Date));
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0;
            horizontalPanOffset = 0;
            showCrosshair = true;
            crosshairIndex = -1;
            CancelDrawing();
            drawings.Clear();
            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
            Invalidate();
        }

        public void SetChartType(TradingChartType type) { chartType = type; Invalidate(); }
        public void ToggleGrid() { showGrid = !showGrid; Invalidate(); }
        public bool GridVisible => showGrid;
        public void ToggleCrosshair() { showCrosshair = !showCrosshair; Invalidate(); }
        public bool CrosshairVisible => showCrosshair;
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
            crosshairIndex = -1;
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
            if (e.Button == MouseButtons.Left && e.X <= 55 && e.Y <= Height - 35)
                ResetView();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (extraInputHandled)
            {
                extraInputHandled = false;
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                if (activeDrawingTool != ChartDrawingTool.None || drawingInProgress)
                    CancelDrawing();
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            if (activeDrawingTool != ChartDrawingTool.None)
            {
                BeginOrCompleteDrawing(e.Location);
                return;
            }

            var plot = GetPlotRectangle();
            var handleIndex = HitTestDrawingHandle(e.Location, plot, out var handle);
            if (handleIndex >= 0)
            {
                selectedDrawingIndex = handleIndex;
                draggingDrawingIndex = handleIndex;
                draggingHandle = handle;
                draggingLastPoint = e.Location;
                Focus();
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            var hitIndex = HitTestDrawing(e.Location, plot);
            if (hitIndex >= 0)
            {
                selectedDrawingIndex = hitIndex;
                draggingDrawingIndex = hitIndex;
                draggingHandle = 0;
                draggingLastPoint = e.Location;
                Focus();
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
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
                panStartVerticalRange = visible.Count > 0
                    ? Math.Max(visible.Max(x => x.High) - visible.Min(x => x.Low), 1e-9) / verticalZoom * 1.10
                    : 1.0;
                Capture = true;
                Cursor = Cursors.SizeAll;
            }

            Focus();
        }

        private void BeginOrCompleteDrawing(Point location)
        {
            if (!IsInsidePlot(location)) return;

            if (activeDrawingTool == ChartDrawingTool.HorizontalDoubleArrow ||
                activeDrawingTool == ChartDrawingTool.VerticalDoubleArrow)
            {
                AddDrawing(location, location);
                drawingInProgress = false;
                drawingStage = 0;
                drawingStartPoint = Point.Empty;
                drawingCurrentPoint = Point.Empty;
                activeDrawingTool = ChartDrawingTool.None;
                Cursor = Cursors.Default;
                Invalidate();
                return;
            }

            if (activeDrawingTool == ChartDrawingTool.TrendChannel)
            {
                if (!drawingInProgress)
                {
                    drawingStartPoint = location;
                    drawingCurrentPoint = location;
                    drawingSecondPoint = Point.Empty;
                    drawingStage = 1;
                    drawingInProgress = true;
                    Invalidate();
                    return;
                }

                if (drawingStage == 1)
                {
                    drawingSecondPoint = location;
                    drawingCurrentPoint = location;
                    drawingStage = 2;
                    Invalidate();
                    return;
                }

                AddDrawing(drawingStartPoint, drawingSecondPoint, location);
                drawingInProgress = false;
                drawingStage = 0;
                drawingStartPoint = Point.Empty;
                drawingCurrentPoint = Point.Empty;
                drawingSecondPoint = Point.Empty;
                activeDrawingTool = ChartDrawingTool.None;
                Cursor = Cursors.Default;
                Invalidate();
                return;
            }

            if (!drawingInProgress)
            {
                drawingStartPoint = location;
                drawingCurrentPoint = location;
                drawingStage = 1;
                drawingInProgress = true;
                Invalidate();
                return;
            }

            drawingCurrentPoint = location;
            AddDrawing(drawingStartPoint, drawingCurrentPoint);
            drawingInProgress = false;
            drawingStage = 0;
            drawingStartPoint = Point.Empty;
            drawingCurrentPoint = Point.Empty;
            activeDrawingTool = ChartDrawingTool.None;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private bool IsInsidePlot(Point point) => GetPlotRectangle().Contains(point);

        private void AddDrawing(Point start, Point end)
        {
            AddDrawing(start, end, Point.Empty);
        }

        private void AddDrawing(Point start, Point end, Point third)
        {
            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0) return;
            GetVerticalRange(visible, out var min, out var max);

            var x1 = ScreenToDataX(start.X, plot, visible.Count);
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, visible.Count);
            var y2 = ScreenToPrice(end.Y, plot, min, max);

            switch (activeDrawingTool)
            {
                case ChartDrawingTool.TrendLine:
                case ChartDrawingTool.TrendLineWithArrow:
                case ChartDrawingTool.Rectangle:
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 });
                    break;

                case ChartDrawingTool.TrendChannel:
                    if (third == Point.Empty) return;
                    var x3 = ScreenToDataX(third.X, plot, visible.Count);
                    var y3 = ScreenToPrice(third.Y, plot, min, max);
                    drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, X3 = x3, Y3 = y3 });
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

            if (extraInputHandled)
            {
                extraInputHandled = false;
                return;
            }

            if (showCrosshair)
            {
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

            if (draggingDrawingIndex >= 0 && draggingDrawingIndex < drawings.Count && Capture)
            {
                MoveOrResizeDrawing(draggingDrawingIndex, draggingHandle, e.Location);
                draggingLastPoint = e.Location;
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
                var delta = e.Y - verticalAxisStartPoint.Y;
                verticalZoom = Math.Clamp(verticalAxisStartZoom * Math.Exp(delta / 200.0), 0.1, 20.0);
                Invalidate();
                return;
            }

            if (panning && Capture && points.Count > 1)
            {
                var dx = e.X - panStartPoint.X;
                var dy = e.Y - panStartPoint.Y;
                var step = Math.Max(1.0, (Width - 70) / (double)Math.Max(1, visibleCount));
                var indexDelta = (int)Math.Round(-dx / step);
                firstIndex = Math.Clamp(panStartFirstIndex + indexDelta, 0, Math.Max(0, points.Count - visibleCount));
                verticalPanOffset = panStartVerticalPanOffset + dy / Math.Max(1.0, Height - 50) * panStartVerticalRange;
                horizontalPanOffset = panStartHorizontalOffset + dx;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (extraInputHandled)
            {
                extraInputHandled = false;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (draggingDrawingIndex >= 0)
                {
                    draggingDrawingIndex = -1;
                    draggingHandle = 0;
                    Capture = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                    return;
                }

                if (horizontalAxisDrag || verticalAxisDrag || panning)
                {
                    horizontalAxisDrag = false;
                    verticalAxisDrag = false;
                    panning = false;
                    Capture = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                }
            }
        }
    }
}