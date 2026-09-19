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
        private Point extraDraggingLastPoint;
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
            extraDraggingLastPoint = Point.Empty;
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
            extraDraggingLastPoint = Point.Empty;
            extraInputHandled = false;
            Capture = false;
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

            if (count == extraDataCount && first == extraFirstDate && last == extraLastDate)
                return;

            extraDrawings.Clear();
            selectedExtraDrawingIndex = -1;
            extraDraggingHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandleActive = false;
            extraDraggingLastPoint = Point.Empty;
            extraDataCount = count;
            extraFirstDate = first;
            extraLastDate = last;
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
                {
                    CancelExtraDrawing();
                }
                else
                {
                    selectedExtraDrawingIndex = -1;
                    extraDraggingHandle = 0;
                    extraDraggingDrawingIndex = -1;
                    extraDraggingHandleActive = false;
                    Capture = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                }
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            // While a three-point tool is active, this handler is the only owner
            // of the left-click. The main chart must not pan or start another tool.
            if (ExtraDrawingActive)
            {
                if (!IsInsidePlot(e.Location))
                {
                    CancelExtraDrawing();
                    extraInputHandled = true;
                    return;
                }

                extraInputHandled = true;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = false;

                if (!extraDrawingInProgress)
                {
                    extraDrawingPoints.Clear();
                    extraDrawingPoints.Add(e.Location);
                    extraDrawingCurrentPoint = e.Location;
                    extraDrawingInProgress = true;
                    Invalidate();
                    return;
                }

                if (extraDrawingPoints.Count < RequiredExtraPoints)
                {
                    extraDrawingPoints.Add(e.Location);
                    extraDrawingCurrentPoint = e.Location;
                }

                if (extraDrawingPoints.Count == RequiredExtraPoints)
                {
                    if (activeExtraDrawingTool != ExtraDrawingTool.Measure)
                        AddExtraDrawing();
                    else
                        AddExtraDrawing();

                    // Completing a drawing also ends the tool, exactly once.
                    activeExtraDrawingTool = ExtraDrawingTool.None;
                    extraDrawingInProgress = false;
                    extraDrawingPoints.Clear();
                    extraDrawingCurrentPoint = Point.Empty;
                    extraDraggingHandle = 0;
                    extraDraggingDrawingIndex = -1;
                    extraDraggingHandleActive = false;
                    Cursor = Cursors.Default;
                }

                Invalidate();
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

                var handle = HitTestHandle(e.Location, p1, p2, p3);
                if (handle != 0)
                {
                    StartExtraDrag(i, handle, e.Location);
                    return;
                }

                if (IsExtraDrawingBodyHit(e.Location, d, plot, visibleCountForDrawing, min, max))
                {
                    // Handle 0 means move the whole drawing, not one anchor.
                    StartExtraDrag(i, 0, e.Location);
                    return;
                }
            }

            selectedExtraDrawingIndex = -1;
            extraDraggingHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandleActive = false;
            Capture = false;
            Cursor = Cursors.Default;
            extraInputHandled = extraDrawings.Count > 0;
            Invalidate();
        }

        private void StartExtraDrag(int drawingIndex, int handle, Point location)
        {
            selectedExtraDrawingIndex = drawingIndex;
            extraDraggingDrawingIndex = drawingIndex;
            extraDraggingHandle = handle;
            extraDraggingHandleActive = true;
            extraDraggingLastPoint = location;
            extraInputHandled = true;
            panning = false;
            horizontalAxisDrag = false;
            verticalAxisDrag = false;
            Capture = true;
            Cursor = Cursors.SizeAll;
            Focus();
            Invalidate();
        }

        private bool IsExtraDrawingBodyHit(Point location, ExtraDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
            var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);

            if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                return HitTestFibonacciLevel(location, d, plot, visibleCountForDrawing, min, max);

            if (d.Tool == ExtraDrawingTool.Pitchfork)
            {
                var midpoint = new PointF((p2.X + p3.X) / 2f, (p2.Y + p3.Y) / 2f);
                var dx = midpoint.X - p1.X;
                var dy = midpoint.Y - p1.Y;
                if (DistanceToSegment(location, p2, p3) <= 8f)
                    return true;
                if (DistanceToRay(location, p1, dx, dy, plot) <= 7f ||
                    DistanceToRay(location, p2, dx, dy, plot) <= 7f ||
                    DistanceToRay(location, p3, dx, dy, plot) <= 7f)
                    return true;
                return false;
            }

            return d.Tool == ExtraDrawingTool.Measure && DistanceToSegment(location, p1, p2) <= 7f;
        }

        private void ExtraDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (extraDraggingHandleActive && extraDraggingDrawingIndex >= 0 && extraDraggingDrawingIndex < extraDrawings.Count && Capture)
            {
                extraInputHandled = true;
                if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                    return;

                var d = extraDrawings[extraDraggingDrawingIndex];

                if (extraDraggingHandle == 0)
                {
                    var oldX = ScreenToDataX(extraDraggingLastPoint.X, plot, visibleCountForDrawing);
                    var oldY = ScreenToPrice(extraDraggingLastPoint.Y, plot, min, max);
                    var newX = ScreenToDataX(e.X, plot, visibleCountForDrawing);
                    var newY = ScreenToPrice(e.Y, plot, min, max);
                    var dx = newX - oldX;
                    var dy = newY - oldY;
                    d.X1 += dx; d.Y1 += dy;
                    d.X2 += dx; d.Y2 += dy;
                    d.X3 += dx; d.Y3 += dy;
                }
                else
                {
                    var x = ScreenToDataX(e.X, plot, visibleCountForDrawing);
                    var y = ScreenToPrice(e.Y, plot, min, max);
                    if (extraDraggingHandle == 1) { d.X1 = x; d.Y1 = y; }
                    else if (extraDraggingHandle == 2) { d.X2 = x; d.Y2 = y; }
                    else if (extraDraggingHandle == 3) { d.X3 = x; d.Y3 = y; }
                }

                extraDraggingLastPoint = e.Location;
                Invalidate();
                return;
            }

            if (!ExtraDrawingActive || !extraDrawingInProgress)
                return;

            extraInputHandled = true;
            panning = false;
            horizontalAxisDrag = false;
            verticalAxisDrag = false;
            extraDrawingCurrentPoint = e.Location;
            Invalidate();
        }

        private void ExtraDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (extraDraggingHandleActive)
            {
                extraInputHandled = true;
                extraDraggingHandleActive = false;
                extraDraggingDrawingIndex = -1;
                extraDraggingHandle = 0;
                extraDraggingLastPoint = Point.Empty;
                Capture = false;
                Cursor = Cursors.Default;
                Invalidate();
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
            if (location.X < leftX - 8f || location.X > rightX + 8f)
                return false;

            foreach (var level in FibonacciLevels)
            {
                var y = c.Y + dy * level.Value;
                if (Math.Abs(location.Y - y) <= 7f)
                    return true;
            }
            return false;
        }

        private static (float Value, string Text)[] FibonacciLevels => ChartAppearanceSettings.GetEnabledFibonacciLevels();

        private static float DistanceToRay(Point location, PointF start, float dx, float dy, Rectangle plot)
        {
            if (Math.Abs(dx) + Math.Abs(dy) < 0.001f)
                return float.MaxValue;

            var length = MathF.Sqrt(dx * dx + dy * dy);
            var ux = dx / length;
            var uy = dy / length;
            var vx = location.X - start.X;
            var vy = location.Y - start.Y;
            var t = vx * ux + vy * uy;
            if (t < 0)
                return DistanceToPoint(location, start);

            var px = start.X + ux * t;
            var py = start.Y + uy * t;
            return DistanceToPoint(location, new PointF(px, py));
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
                Capture = false;
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
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;
            if (extraDrawingPoints.Count < RequiredExtraPoints)
                return;

            var p1 = extraDrawingPoints[0];
            var p2 = extraDrawingPoints[1];
            var p3 = extraDrawingPoints.Count > 2 ? extraDrawingPoints[2] : p2;

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
            using var previewPen = new Pen(Color.FromArgb(155, 80, 45), 1.2f) { DashStyle = DashStyle.Dash };

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var d = extraDrawings[i];
                var pen = i == selectedExtraDrawingIndex ? selectedPen : normalPen;
                if (d.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(e.Graphics, pen, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawFibonacciExtension(e.Graphics, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
                else if (d.Tool == ExtraDrawingTool.Measure)
                    DrawMeasure(e.Graphics, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
            }

            if (!extraDrawingInProgress || !ExtraDrawingActive || extraDrawingPoints.Count == 0)
                return;

            if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                DrawMeasurePreview(e.Graphics, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
            else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                DrawPitchforkPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            else if (activeExtraDrawingTool == ExtraDrawingTool.FibonacciExtension)
                DrawFibonacciExtensionPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
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
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);

            foreach (var level in FibonacciLevels)
            {
                var y = c.Y + dy * level.Value;
                g.DrawLine(pen, leftX, y, rightX, y);
                var size = g.MeasureString(level.Text, SystemFonts.DefaultFont);
                var labelX = Math.Min(rightX + 5f, plot.Right - size.Width - 2f);
                if (labelX < leftX) labelX = leftX;
                g.DrawString(level.Text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
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

            foreach (var level in FibonacciLevels)
            {
                var y = c.Y + dy * level.Value;
                g.DrawLine(pen, leftX, y, rightX, y);
            }
        }

        private void DrawMeasure(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            g.DrawLine(pen, a, b);
            if (selected)
            {
                DrawHandle(g, a);
                DrawHandle(g, b);
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
                if (IsExtraDrawingBodyHit(location, d, plot, visibleCountForDrawing, min, max))
                    return i;
            }
            return -1;
        }

        private static float DistanceToPoint(Point location, PointF point)
        {
            var dx = location.X - point.X;
            var dy = location.Y - point.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        private static float DistanceToSegment(Point location, PointF a, PointF b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            if (Math.Abs(dx) + Math.Abs(dy) < 0.001f)
                return DistanceToPoint(location, a);
            var t = ((location.X - a.X) * dx + (location.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0f, 1f);
            return DistanceToPoint(location, new PointF(a.X + t * dx, a.Y + t * dy));
        }
    }
}