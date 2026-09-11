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
                extraDataCount = count;
                extraFirstDate = first;
                extraLastDate = last;
            }
        }

        private int RequiredExtraPoints => activeExtraDrawingTool == ExtraDrawingTool.Measure ? 2 : 3;

        private void ExtraDrawing_MouseDown(object? sender, MouseEventArgs e)
        {
            SyncExtraDrawingData();
            if (e.Button == MouseButtons.Right)
            {
                if (ExtraDrawingActive || extraDrawingInProgress)
                    CancelExtraDrawing();
                return;
            }
            if (e.Button != MouseButtons.Left)
                return;

            if (ExtraDrawingActive)
            {
                if (!IsInsidePlot(e.Location))
                    return;

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
                    AddExtraDrawing();
                    CancelExtraDrawing();
                }
                Invalidate();
                DeferExtraMouseState();
                return;
            }

            var hit = HitTestExtraDrawing(e.Location, GetPlotRectangle());
            if (hit >= 0)
            {
                selectedExtraDrawingIndex = hit;
                Invalidate();
                DeferExtraMouseState();
            }
        }

        private void ExtraDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!ExtraDrawingActive)
                return;
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
            if (e.Button == MouseButtons.Left && ExtraDrawingActive)
                DeferExtraMouseState();
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
            selectedExtraDrawingIndex = extraDrawings.Count - 1;
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
                    DrawPitchfork(e.Graphics, pen, d, plot, visibleCountForDrawing, min, max);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawFibonacciExtension(e.Graphics, pen, labelBrush, d, plot, visibleCountForDrawing, min, max);
                else
                    DrawMeasure(e.Graphics, pen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Count > 0)
            {
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                    DrawMeasurePreview(e.Graphics, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot);
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                    DrawPitchforkPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawFibonacciExtensionPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            }
        }

        private void DrawPitchfork(Graphics g, Pen pen, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            DrawPitchforkGeometry(g, pen, p1, p2, p3, plot);
            DrawHandle(g, p1); DrawHandle(g, p2); DrawHandle(g, p3);
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

            // Andrews Pitchfork: the median and its two parallel tines extend
            // from their anchor points toward the future (right side of chart).
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

            var targetTopX = start.X + dx * ((plot.Top - start.Y) / dy);
            if (Math.Abs(dy) > 0.001f && targetTopX >= start.X && targetTopX <= plot.Right)
            {
                g.DrawLine(pen, start, new PointF(targetTopX, plot.Top));
                return;
            }

            var targetBottomX = start.X + dx * ((plot.Bottom - start.Y) / dy);
            if (Math.Abs(dy) > 0.001f && targetBottomX >= start.X && targetBottomX <= plot.Right)
                g.DrawLine(pen, start, new PointF(targetBottomX, plot.Bottom));
        }

        private void DrawFibonacciExtension(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f };
            g.DrawLine(pen, a, b); g.DrawLine(pen, b, c);
            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, c.X, y, plot.Right, y);
                var text = level switch { 0f => "0%", 0.382f => "38.2%", 0.618f => "61.8%", 1f => "100%", 1.272f => "127.2%", 1.618f => "161.8%", _ => "261.8%" };
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, Math.Min(c.X + 4f, plot.Right - 48f), y - 8f);
            }
            DrawHandle(g, a); DrawHandle(g, b); DrawHandle(g, c);
        }

        private static void DrawFibonacciExtensionPreview(Graphics g, Pen pen, List<Point> points, Point current, Rectangle plot)
        {
            if (points.Count == 0) return;
            var a = points[0];
            var b = points.Count > 1 ? points[1] : current;
            var c = points.Count > 2 ? points[2] : current;
            var dy = b.Y - a.Y;
            g.DrawLine(pen, a, b); g.DrawLine(pen, b, c);
            foreach (var level in new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f })
                g.DrawLine(pen, c.X, c.Y + dy * level, plot.Right, c.Y + dy * level);
        }

        private void DrawMeasure(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            g.DrawLine(pen, a, b); DrawHandle(g, a); DrawHandle(g, b);
            var text = $"Δ قیمت: {d.Y2 - d.Y1:N2}    Δ کندل: {Math.Abs(d.X2 - d.X1):N1}";
            using var font = new Font(SystemFonts.DefaultFont.FontFamily, 9f);
            var size = g.MeasureString(text, font);
            var x = Math.Max(plot.Left + 4f, Math.Min((a.X + b.X) / 2f, plot.Right - size.Width - 8f));
            var y = Math.Max(plot.Top + 4f, Math.Min((a.Y + b.Y) / 2f - size.Height - 4f, plot.Bottom - size.Height - 4f));
            var rect = new RectangleF(x, y, size.Width + 8f, size.Height + 4f);
            g.FillRectangle(labelBack, rect); g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            g.DrawString(text, font, labelBrush, x + 4f, y + 2f);
        }

        private static void DrawMeasurePreview(Graphics g, Pen pen, Brush labelBrush, Point a, Point b, Rectangle plot)
        {
            g.DrawLine(pen, a, b);
            var dx = b.X - a.X; var dy = b.Y - a.Y;
            var text = $"فاصله: {Math.Sqrt(dx * dx + dy * dy):N0}px";
            using var font = new Font(SystemFonts.DefaultFont.FontFamily, 9f);
            g.DrawString(text, font, labelBrush, b.X + 6f, b.Y + 6f);
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
                if (d.Tool == ExtraDrawingTool.Measure && DistanceToSegment(location, p1, p2) <= 7f)
                    return i;
            }
            return -1;
        }
    }
}
