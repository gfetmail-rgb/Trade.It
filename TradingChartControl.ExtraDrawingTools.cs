using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private enum ExtraDrawingTool
        {
            None,
            Pitchfork,
            FibonacciExtension,
            Measure
        }

        private sealed class ExtraDrawing
        {
            public ExtraDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public double X3 { get; set; }
            public double Y3 { get; set; }
        }

        private readonly List<ExtraDrawing> extraDrawings = new();
        private ExtraDrawingTool activeExtraDrawingTool;
        private bool extraDrawingInProgress;
        private readonly List<Point> extraDrawingPoints = new();
        private Point extraDrawingCurrentPoint;
        private int selectedExtraDrawingIndex = -1;
        private int extraDraggingHandle;
        private int extraDraggingDrawingIndex = -1;
        private bool extraDraggingHandleActive;
        private bool extraInputHandled;
        private int extraDataCount = -1;
        private DateTime extraFirstDate;
        private DateTime extraLastDate;
        private bool extraDrawingEventsInitialized;

        public bool ExtraDrawingActive => activeExtraDrawingTool != ExtraDrawingTool.None;

        public void ActivatePitchfork() => ActivateExtraDrawingTool(ExtraDrawingTool.Pitchfork);
        public void ActivateFibonacciExtension() => ActivateExtraDrawingTool(ExtraDrawingTool.FibonacciExtension);
        public void ActivateMeasureTool() => ActivateExtraDrawingTool(ExtraDrawingTool.Measure);

        public void CancelExtraDrawing()
        {
            activeExtraDrawingTool = ExtraDrawingTool.None;
            extraDrawingInProgress = false;
            extraDrawingPoints.Clear();
            extraDrawingCurrentPoint = Point.Empty;
            extraDraggingHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandleActive = false;
            extraInputHandled = false;
            Capture = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private void ActivateExtraDrawingTool(ExtraDrawingTool tool)
        {
            EnsureExtraDrawingEvents();
            CancelAdvancedDrawing();
            CancelDrawing();
            activeExtraDrawingTool = activeExtraDrawingTool == tool ? ExtraDrawingTool.None : tool;
            extraDrawingInProgress = false;
            extraDrawingPoints.Clear();
            extraDrawingCurrentPoint = Point.Empty;
            extraDraggingHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandleActive = false;
            extraInputHandled = false;
            Cursor = activeExtraDrawingTool == ExtraDrawingTool.None ? Cursors.Default : Cursors.Cross;
            Focus();
            Invalidate();
        }

        private void EnsureExtraDrawingEvents()
        {
            if (extraDrawingEventsInitialized)
                return;
            extraDrawingEventsInitialized = true;
            Paint += ExtraDrawing_Paint;
            MouseDown += ExtraDrawing_MouseDown;
            MouseMove += ExtraDrawing_MouseMove;
            MouseUp += ExtraDrawing_MouseUp;
            KeyDown += ExtraDrawing_KeyDown;
        }

        private void SyncExtraDrawingData()
        {
            var count = points.Count;
            var first = count > 0 ? points[0].Date : DateTime.MinValue;
            var last = count > 0 ? points[^1].Date : DateTime.MinValue;
            if (count != extraDataCount || first != extraFirstDate || last != extraLastDate)
            {
                extraDrawings.Clear();
                selectedExtraDrawingIndex = -1;
                extraDraggingHandle = 0;
                extraDraggingDrawingIndex = -1;
                extraDraggingHandleActive = false;
                extraDataCount = count;
                extraFirstDate = first;
                extraLastDate = last;
            }
        }

        private int RequiredExtraPoints => activeExtraDrawingTool == ExtraDrawingTool.Measure ? 2 : 3;

        private void ExtraDrawing_MouseDown(object? sender, MouseEventArgs e)
        {
            // WndProc uses extraInputHandled to mark a click that has already
            // cancelled an active extra tool (for example, a click in the blank
            // area outside the plotting rectangle). Consume that event here so
            // this handler cannot immediately revive the cancelled tool state.
            if (extraInputHandled)
            {
                extraInputHandled = false;
                return;
            }

            extraInputHandled = false;
            SyncExtraDrawingData();

            if (e.Button == MouseButtons.Right)
            {
                extraInputHandled = true;
                if (ExtraDrawingActive || extraDrawingInProgress)
                    CancelExtraDrawing();
                else
                {
                    selectedExtraDrawingIndex = -1;
                    extraDraggingHandle = 0;
                    extraDraggingDrawingIndex = -1;
                    extraDraggingHandleActive = false;
                    Invalidate();
                }
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            if (ExtraDrawingActive)
            {
                if (!IsInsidePlot(e.Location))
                    return;

                extraInputHandled = true;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;

                if (!extraDrawingInProgress)
                {
                    extraDrawingPoints.Clear();
                    extraDrawingPoints.Add(e.Location);
                    extraDrawingCurrentPoint = e.Location;
                    extraDrawingInProgress = true;
                    Invalidate();
                    DeferExtraMouseState();
                    return;
                }

                extraDrawingPoints.Add(e.Location);
                extraDrawingCurrentPoint = e.Location;
                if (extraDrawingPoints.Count >= RequiredExtraPoints)
                {
                    if (activeExtraDrawingTool != ExtraDrawingTool.Measure)
                        AddExtraDrawing();
                    CancelExtraDrawing();
                }
                Invalidate();
                DeferExtraMouseState();
                return;
            }

            var plot = GetPlotRectangle();
            if (!TryGetExtraContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return;

            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);

                if (d.Tool == ExtraDrawingTool.Pitchfork || d.Tool == ExtraDrawingTool.FibonacciExtension)
                {
                    var handle = HitTestHandle(e.Location, p1, p2, p3);
                    if (handle != 0)
                    {
                        extraInputHandled = true;
                        selectedExtraDrawingIndex = i;
                        extraDraggingDrawingIndex = i;
                        extraDraggingHandle = handle;
                        extraDraggingHandleActive = true;
                        Capture = true;
                        Cursor = Cursors.SizeAll;
                        Focus();
                        Invalidate();
                        return;
                    }
                }

                if (DistanceToPoint(e.Location, p1) <= 10f || DistanceToPoint(e.Location, p2) <= 10f || DistanceToPoint(e.Location, p3) <= 10f ||
                    (d.Tool == ExtraDrawingTool.FibonacciExtension && HitTestFibonacciLevel(e.Location, d, plot, visibleCountForDrawing, min, max)) ||
                    (d.Tool == ExtraDrawingTool.Measure && DistanceToSegment(e.Location, p1, p2) <= 7f))
                {
                    extraInputHandled = true;
                    selectedExtraDrawingIndex = i;
                    Invalidate();
                    DeferExtraMouseState();
                    return;
                }
            }

            if (extraDrawings.Count > 0)
                extraInputHandled = true;
            selectedExtraDrawingIndex = -1;
            extraDraggingHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandleActive = false;
            Capture = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private void ExtraDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (extraDraggingHandleActive && extraDraggingDrawingIndex >= 0 && extraDraggingDrawingIndex < extraDrawings.Count)
            {
                if (e.Button == MouseButtons.Left)
                {
                    var plot = GetPlotRectangle();
                    if (TryGetExtraContext(out _, out var visibleCountForDrawing, out var min, out var max))
                    {
                        var drawing = extraDrawings[extraDraggingDrawingIndex];
                        var point = ScreenToData(e.Location, plot, visibleCountForDrawing, min, max);
                        if (extraDraggingHandle == 1)
                        {
                            drawing.X1 = point.X;
                            drawing.Y1 = point.Y;
                        }
                        else if (extraDraggingHandle == 2)
                        {
                            drawing.X2 = point.X;
                            drawing.Y2 = point.Y;
                        }
                        else if (extraDraggingHandle == 3)
                        {
                            drawing.X3 = point.X;
                            drawing.Y3 = point.Y;
                        }
                        Invalidate();
                    }
                }
                return;
            }

            if (ExtraDrawingActive && extraDrawingInProgress)
            {
                extraDrawingCurrentPoint = e.Location;
                Invalidate();
            }
        }

        private void ExtraDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && extraDraggingHandleActive)
            {
                extraDraggingHandleActive = false;
                extraDraggingDrawingIndex = -1;
                extraDraggingHandle = 0;
                Capture = false;
                Cursor = ExtraDrawingActive ? Cursors.Cross : Cursors.Default;
                Invalidate();
            }
        }

        private void ExtraDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && (ExtraDrawingActive || extraDrawingInProgress || extraDraggingHandleActive))
            {
                CancelExtraDrawing();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void AddExtraDrawing()
        {
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;
            if (extraDrawingPoints.Count < RequiredExtraPoints)
                return;

            var pointsToUse = extraDrawingPoints.Take(RequiredExtraPoints).ToList();
            var p1 = ScreenToData(pointsToUse[0], plot, visibleCountForDrawing, min, max);
            var p2 = ScreenToData(pointsToUse[1], plot, visibleCountForDrawing, min, max);
            var p3 = RequiredExtraPoints >= 3
                ? ScreenToData(pointsToUse[2], plot, visibleCountForDrawing, min, max)
                : p2;

            extraDrawings.Add(new ExtraDrawing
            {
                Tool = activeExtraDrawingTool,
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                X3 = p3.X,
                Y3 = p3.Y
            });
            selectedExtraDrawingIndex = extraDrawings.Count - 1;
        }

        private void DeferExtraMouseState()
        {
            BeginInvoke(new Action(() =>
            {
                if (IsDisposed || !IsHandleCreated)
                    return;
                extraInputHandled = false;
                Invalidate();
            }));
        }

        private bool TryGetExtraContext(out Rectangle plot, out int visibleCountForDrawing, out double min, out double max)
        {
            plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            visibleCountForDrawing = Math.Max(1, endIndex - firstIndex);
            var visible = points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();
            if (visible.Count == 0)
            {
                min = max = 0;
                return false;
            }
            GetVerticalRange(visible, out min, out max);
            return true;
        }

        private PointF ScreenToData(Point point, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var x = (point.X - plot.Left - horizontalPanOffset - (-plot.Width * 0.25)) / Math.Max(0.0001, step) - 0.5 + firstIndex;
            var y = max - ((point.Y - plot.Top) / (double)Math.Max(1, plot.Height)) * (max - min);
            return new PointF((float)x, (float)y);
        }

        private PointF DataToScreen(double x, double y, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var localX = x - firstIndex;
            var screenX = plot.Left + step * (localX + 0.5) + (-plot.Width * 0.25) + horizontalPanOffset;
            var screenY = PriceToScreen(y, plot, min, max);
            return new PointF((float)screenX, (float)screenY);
        }

        private static int HitTestHandle(Point location, PointF p1, PointF p2, PointF p3)
        {
            if (DistanceToPoint(location, p1) <= 10f) return 1;
            if (DistanceToPoint(location, p2) <= 10f) return 2;
            if (DistanceToPoint(location, p3) <= 10f) return 3;
            return 0;
        }

        private static float DistanceToPoint(PointF p, PointF q)
        {
            var dx = p.X - q.X;
            var dy = p.Y - q.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private static float DistanceToPoint(Point p, PointF q)
        {
            return DistanceToPoint(new PointF(p.X, p.Y), q);
        }

        private static float DistanceToSegment(Point p, PointF a, PointF b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            if (Math.Abs(dx) < 0.001f && Math.Abs(dy) < 0.001f)
                return DistanceToPoint(p, a);
            var t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0f, 1f);
            var projection = new PointF(a.X + t * dx, a.Y + t * dy);
            return DistanceToPoint(p, projection);
        }

        private bool HitTestFibonacciLevel(Point location, ExtraDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var p1 = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var p2 = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
            var p3 = DataToScreen(drawing.X3, drawing.Y3, plot, visibleCountForDrawing, min, max);
            var priceRange = p1.Y - p2.Y;
            if (Math.Abs(priceRange) < 0.001f)
                return false;
            var levels = new[] { 0.0, 0.236, 0.382, 0.5, 0.618, 0.786, 1.0, 1.272, 1.618, 2.0 };
            foreach (var level in levels)
            {
                var y = p1.Y + (p2.Y - p1.Y) * level;
                if (Math.Abs(location.Y - y) <= 7f)
                    return true;
            }
            return false;
        }
    }
}
