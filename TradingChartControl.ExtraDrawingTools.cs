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
            extraInputHandled = false;

            if (extraDraggingHandleActive && extraDraggingDrawingIndex >= 0 && extraDraggingDrawingIndex < extraDrawings.Count)
            {
                extraInputHandled = true;
                if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                    return;

                var d = extraDrawings[extraDraggingDrawingIndex];
                var x = ScreenToDataX(e.X, plot, visibleCountForDrawing);
                var y = ScreenToPrice(e.Y, plot, min, max);

                if (extraDraggingHandle == 1)
                {
                    d.X1 = x;
                    d.Y1 = y;
                }
                else if (extraDraggingHandle == 2)
                {
                    d.X2 = x;
                    d.Y2 = y;
                }
                else if (extraDraggingHandle == 3)
                {
                    d.X3 = x;
                    d.Y3 = y;
                }

                Invalidate();
                return;
            }

            if (!ExtraDrawingActive)
                return;

            extraInputHandled = true;
            panning = false;
            horizontalAxisDrag = false;
            verticalAxisDrag = false;
            if (extraDrawingInProgress)
            {
                extraDrawingCurrentPoint = e.Location;
                Invalidate();
                DeferExtraMouseState();
            }
        }

        private void ExtraDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            extraInputHandled = false;
            if (e.Button == MouseButtons.Left && extraDraggingHandleActive)
            {
                extraInputHandled = true;
                extraDraggingHandleActive = false;
                extraDraggingDrawingIndex = -1;
                extraDraggingHandle = 0;
                Capture = false;
                Cursor = Cursors.Default;
                Invalidate();
                return;
            }

            if (e.Button == MouseButtons.Left && ExtraDrawingActive)
            {
                extraInputHandled = true;
                DeferExtraMouseState();
            }
        }

        private int HitTestHandle(Point location, PointF p1, PointF p2, PointF p3)
        {
            if (DistanceToPoint(location, p1) <= 10f) return 1;
            if (DistanceToPoint(location, p2) <= 10f) return 2;
            if (DistanceToPoint(location, p3) <= 10f) return 3;
            return 0;
        }

        private bool HitTestFibonacciLevel(Point location, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);
            if (location.X < leftX - 6f || location.X > rightX + 6f)
                return false;

            foreach (var level in new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2f, 2.618f })
            {
                var y = c.Y + dy * level;
                if (Math.Abs(location.Y - y) <= 7f)
                    return true;
            }
            return false;
        }

        private void DeferExtraMouseState()
        {
            if (!IsHandleCreated)
                return;
            BeginInvoke(new Action(() =>
            {
                if (IsDisposed)
                    return;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                draggingDrawingIndex = -1;
                draggingHandle = 0;
                Capture = false;
                if (ExtraDrawingActive)
                    Cursor = Cursors.Cross;
            }));
        }

        private void ExtraDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedExtraDrawingIndex >= 0 && selectedExtraDrawingIndex < extraDrawings.Count)
            {
                extraDrawings.RemoveAt(selectedExtraDrawingIndex);
                selectedExtraDrawingIndex = -1;
                extraDraggingHandle = 0;
                extraDraggingDrawingIndex = -1;
                extraDraggingHandleActive = false;
                Invalidate();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.Escape && ExtraDrawingActive)
            {
                CancelExtraDrawing();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void AddExtraDrawing()
        {
            if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                return;
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;
            if (extraDrawingPoints.Count < RequiredExtraPoints)
                return;

            var p1 = extraDrawingPoints[0];
            var p2 = extraDrawingPoints[1];
            var p3 = RequiredExtraPoints == 3 ? extraDrawingPoints[2] : p2;
            extraDrawings.Add(new ExtraDrawing
            {
                Tool = activeExtraDrawingTool,
                X1 = ScreenToDataX(p1.X, plot, visibleCountForDrawing),
                Y1 = ScreenToPrice(p1.Y, plot, min, max),
                X2 = ScreenToDataX(p2.X, plot, visibleCountForDrawing),
                Y2 = ScreenToPrice(p2.Y, plot, min, max),
                X3 = ScreenToDataX(p3.X, plot, visibleCountForDrawing),
                Y3 = ScreenToPrice(p3.Y, plot, min, max)
            });
            selectedExtraDrawingIndex = -1;
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

        private void ExtraDrawing_Paint(object? sender, PaintEventArgs e)
        {
            SyncExtraDrawingData();
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var normalPen = new Pen(Color.FromArgb(155, 80, 45), 1.3f);
            using var selectedPen = new Pen(Color.FromArgb(190, 55, 35), 2f);
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(248, 248, 248));
            using var previewPen = new Pen(Color.FromArgb(155, 80, 45), 1.2f) { DashStyle = DashStyle.Dash };

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var d = extraDrawings[i];
                var pen = i == selectedExtraDrawingIndex ? selectedPen : normalPen;
                if (d.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(e.Graphics, pen, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawFibonacciExtension(e.Graphics, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Count > 0)
            {
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                    DrawMeasurePreview(e.Graphics, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                    DrawPitchforkPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawFibonacciExtensionPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            }
        }

        private void DrawPitchfork(Graphics g, Pen pen, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            DrawPitchforkGeometry(g, pen, p1, p2, p3, plot);
            if (selected)
            {
                DrawHandle(g, p1);
                DrawHandle(g, p2);
                DrawHandle(g, p3);
            }
        }

        private static void DrawPitchforkPreview(Graphics g, Pen pen, List<Point> points, Point current, Rectangle plot)
        {
            if (points.Count == 0) return;
            var p1 = points[0];
            var p2 = points.Count > 1 ? points[1] : current;
            var p3 = points.Count > 2 ? points[2] : current;
            DrawPitchforkGeometry(g, pen, p1, p2, p3, plot);
        }

        private static void DrawPitchforkGeometry(Graphics g, Pen pen, PointF p1, PointF p2, PointF p3, Rectangle plot)
        {
            var midpoint = new PointF((p2.X + p3.X) / 2f, (p2.Y + p3.Y) / 2f);
            var dx = midpoint.X - p1.X;
            var dy = midpoint.Y - p1.Y;
            if (Math.Abs(dx) + Math.Abs(dy) < 0.001f) return;
            DrawRayToRight(g, pen, p1, dx, dy, plot);
            DrawRayToRight(g, pen, p2, dx, dy, plot);
            DrawRayToRight(g, pen, p3, dx, dy, plot);
            g.DrawLine(pen, p2, p3);
        }

        private static void DrawRayToRight(Graphics g, Pen pen, PointF start, float dx, float dy, Rectangle plot)
        {
            if (Math.Abs(dx) < 0.001f)
            {
                g.DrawLine(pen, start.X, start.Y, start.X, plot.Bottom);
                return;
            }
            var targetX = plot.Right;
            var targetY = start.Y + dy * ((targetX - start.X) / dx);
            if (targetY >= plot.Top && targetY <= plot.Bottom)
            {
                g.DrawLine(pen, start, new PointF(targetX, targetY));
                return;
            }
            if (Math.Abs(dy) > 0.001f)
            {
                var targetTopX = start.X + dx * ((plot.Top - start.Y) / dy);
                if (targetTopX >= start.X && targetTopX <= plot.Right)
                {
                    g.DrawLine(pen, start, new PointF(targetTopX, plot.Top));
                    return;
                }
                var targetBottomX = start.X + dx * ((plot.Bottom - start.Y) / dy);
                if (targetBottomX >= start.X && targetBottomX <= plot.Right)
                    g.DrawLine(pen, start, new PointF(targetBottomX, plot.Bottom));
            }
        }

        private void DrawFibonacciExtension(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2f, 2.618f };
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);
            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, c);
            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, leftX, y, rightX, y);
                var text = level switch
                {
                    0f => "0%",
                    0.382f => "38.2%",
                    0.618f => "61.8%",
                    1f => "100%",
                    1.272f => "127.2%",
                    1.618f => "161.8%",
                    2f => "200%",
                    _ => "261.8%"
                };
                var size = g.MeasureString(text, SystemFonts.DefaultFont);
                var labelX = Math.Min(rightX + 5f, plot.Right - size.Width - 2f);
                if (labelX < leftX)
                    labelX = leftX;
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
            }
            if (selected)
            {
                DrawHandle(g, a);
                DrawHandle(g, b);
                DrawHandle(g, c);
            }
        }

        private static void DrawFibonacciExtensionPreview(Graphics g, Pen pen, List<Point> points, Point current, Rectangle plot)
        {
            if (points.Count == 0) return;
            var a = points[0];
            var b = points.Count > 1 ? points[1] : current;
            var c = points.Count > 2 ? points[2] : current;
            var dy = b.Y - a.Y;
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);
            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, c);
            foreach (var level in new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2f, 2.618f })
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, leftX, y, rightX, y);
            }
        }

        private void DrawMeasurePreview(Graphics g, Pen pen, Brush labelBrush, Point a, Point b, Rectangle plot, int visibleCount, double min, double max)
        {
            g.DrawLine(pen, a, b);

            var price1 = ScreenToPrice(a.Y, plot, min, max);
            var price2 = ScreenToPrice(b.Y, plot, min, max);
            var percent = Math.Abs(price1) > double.Epsilon ? ((price2 - price1) / price1) * 100.0 : 0.0;

            var x1 = ScreenToDataX(a.X, plot, visibleCount);
            var x2 = ScreenToDataX(b.X, plot, visibleCount);
            var candleCount = Math.Abs((int)Math.Round(x2) - (int)Math.Round(x1)) + 1;

            var sign = percent > 0 ? "+" : string.Empty;
            var text = $"Δ قیمت: {sign}{percent:N2}%   |   تعداد کندل: {candleCount:N0}";
            using var font = new Font(SystemFonts.DefaultFont.FontFamily, 9f);
            var size = g.MeasureString(text, font);

            var x = Math.Min(b.X + 10f, plot.Right - size.Width - 10f);
            if (x < plot.Left + 5f) x = plot.Left + 5f;
            var y = Math.Min(b.Y + 10f, plot.Bottom - size.Height - 10f);
            if (y < plot.Top + 5f) y = plot.Top + 5f;

            using var back = new SolidBrush(Color.FromArgb(245, 245, 245));
            g.FillRectangle(back, x - 4f, y - 2f, size.Width + 8f, size.Height + 4f);
            g.DrawString(text, font, labelBrush, x, y);
        }

        private static void DrawHandle(Graphics g, PointF p)
        {
            using var brush = new SolidBrush(Color.White);
            using var pen = new Pen(Color.FromArgb(190, 55, 35), 1.2f);
            const float r = 4f;
            g.FillEllipse(brush, p.X - r, p.Y - r, r * 2f, r * 2f);
            g.DrawEllipse(pen, p.X - r, p.Y - r, r * 2f, r * 2f);
        }

        private int HitTestExtraDrawing(Point location, Rectangle plot)
        {
            if (!TryGetExtraContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;
            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);
                if (DistanceToPoint(location, p1) <= 10f || DistanceToPoint(location, p2) <= 10f || DistanceToPoint(location, p3) <= 10f)
                    return i;
                if (d.Tool == ExtraDrawingTool.FibonacciExtension && HitTestFibonacciLevel(location, d, plot, visibleCountForDrawing, min, max))
                    return i;
                if (d.Tool == ExtraDrawingTool.Measure && DistanceToSegment(location, p1, p2) <= 7f)
                    return i;
            }
            return -1;
        }
    }
}
