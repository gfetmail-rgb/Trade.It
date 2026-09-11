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
        private Point[] extraDrawingPoints = Array.Empty<Point>();
        private Point extraDrawingCurrentPoint;
        private int selectedExtraDrawingIndex = -1;
        private int draggingExtraDrawingIndex = -1;
        private int draggingExtraHandle;
        private Point draggingExtraLastPoint;
        private bool extraDrawingEventsInitialized;
        private int extraDataCount = -1;
        private DateTime extraFirstDate;
        private DateTime extraLastDate;

        public bool ExtraDrawingActive => activeExtraDrawingTool != ExtraDrawingTool.None;

        public void ActivatePitchfork()
        {
            ActivateExtraDrawingTool(ExtraDrawingTool.Pitchfork);
        }

        public void ActivateFibonacciExtension()
        {
            ActivateExtraDrawingTool(ExtraDrawingTool.FibonacciExtension);
        }

        public void ActivateMeasureTool()
        {
            ActivateExtraDrawingTool(ExtraDrawingTool.Measure);
        }

        public void CancelExtraDrawing()
        {
            activeExtraDrawingTool = ExtraDrawingTool.None;
            extraDrawingInProgress = false;
            extraDrawingPoints = Array.Empty<Point>();
            extraDrawingCurrentPoint = Point.Empty;
            draggingExtraDrawingIndex = -1;
            draggingExtraHandle = 0;
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
            extraDrawingPoints = Array.Empty<Point>();
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
                draggingExtraDrawingIndex = -1;
                extraDataCount = count;
                extraFirstDate = first;
                extraLastDate = last;
            }
        }

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

                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                {
                    if (!extraDrawingInProgress)
                    {
                        extraDrawingPoints = new[] { e.Location };
                        extraDrawingCurrentPoint = e.Location;
                        extraDrawingInProgress = true;
                        Invalidate();
                    }
                    else
                    {
                        extraDrawingCurrentPoint = e.Location;
                        AddExtraDrawing();
                        CancelExtraDrawing();
                    }
                    return;
                }

                var requiredPoints = activeExtraDrawingTool == ExtraDrawingTool.Pitchfork ? 3 : 3;
                if (!extraDrawingInProgress)
                {
                    extraDrawingPoints = new[] { e.Location };
                    extraDrawingCurrentPoint = e.Location;
                    extraDrawingInProgress = true;
                    Invalidate();
                    return;
                }

                var list = extraDrawingPoints.ToList();
                list.Add(e.Location);
                extraDrawingPoints = list.ToArray();
                extraDrawingCurrentPoint = e.Location;

                if (extraDrawingPoints.Length >= requiredPoints)
                {
                    AddExtraDrawing();
                    CancelExtraDrawing();
                }
                else
                {
                    Invalidate();
                }
                return;
            }

            var plot = GetPlotRectangle();
            var hit = HitTestExtraHandle(e.Location, plot, out var handle);
            if (hit >= 0)
            {
                selectedExtraDrawingIndex = hit;
                draggingExtraDrawingIndex = hit;
                draggingExtraHandle = handle;
                draggingExtraLastPoint = e.Location;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            hit = HitTestExtraDrawing(e.Location, plot);
            if (hit >= 0)
            {
                selectedExtraDrawingIndex = hit;
                draggingExtraDrawingIndex = hit;
                draggingExtraHandle = 0;
                draggingExtraLastPoint = e.Location;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            selectedExtraDrawingIndex = -1;
            Invalidate();
        }

        private void ExtraDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (extraDrawingInProgress && ExtraDrawingActive)
            {
                extraDrawingCurrentPoint = e.Location;
                Invalidate();
                return;
            }

            if (draggingExtraDrawingIndex >= 0 && draggingExtraDrawingIndex < extraDrawings.Count && Capture)
            {
                MoveOrResizeExtraDrawing(draggingExtraDrawingIndex, draggingExtraHandle, e.Location);
                draggingExtraLastPoint = e.Location;
                Invalidate();
            }
        }

        private void ExtraDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (draggingExtraDrawingIndex >= 0)
            {
                draggingExtraDrawingIndex = -1;
                draggingExtraHandle = 0;
                Capture = false;
                Cursor = Cursors.Default;
            }
        }

        private void ExtraDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedExtraDrawingIndex >= 0 && selectedExtraDrawingIndex < extraDrawings.Count)
            {
                extraDrawings.RemoveAt(selectedExtraDrawingIndex);
                selectedExtraDrawingIndex = -1;
                draggingExtraDrawingIndex = -1;
                draggingExtraHandle = 0;
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
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            if (extraDrawingPoints.Length < 2)
                return;

            var p1 = extraDrawingPoints[0];
            var p2 = extraDrawingPoints.Length > 1 ? extraDrawingPoints[1] : extraDrawingCurrentPoint;
            var p3 = extraDrawingPoints.Length > 2 ? extraDrawingPoints[2] : extraDrawingCurrentPoint;

            var drawing = new ExtraDrawing
            {
                Tool = activeExtraDrawingTool,
                X1 = ScreenToDataX(p1.X, plot, visibleCountForDrawing),
                Y1 = ScreenToPrice(p1.Y, plot, min, max),
                X2 = ScreenToDataX(p2.X, plot, visibleCountForDrawing),
                Y2 = ScreenToPrice(p2.Y, plot, min, max),
                X3 = ScreenToDataX(p3.X, plot, visibleCountForDrawing),
                Y3 = ScreenToPrice(p3.Y, plot, min, max)
            };

            if (drawing.Tool == ExtraDrawingTool.Measure)
            {
                drawing.X3 = drawing.X2;
                drawing.Y3 = drawing.Y2;
            }

            extraDrawings.Add(drawing);
            selectedExtraDrawingIndex = extraDrawings.Count - 1;
        }

        private void ExtraDrawing_Paint(object? sender, PaintEventArgs e)
        {
            SyncExtraDrawingData();
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var normalPen = new Pen(Color.FromArgb(155, 80, 45), 1.3f);
            using var selectedPen = new Pen(Color.FromArgb(190, 55, 35), 2f);
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(248, 248, 248));
            using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);

            var state = e.Graphics.Save();
            e.Graphics.SetClip(plot);

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var drawing = extraDrawings[i];
                var pen = i == selectedExtraDrawingIndex ? selectedPen : normalPen;

                if (drawing.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(e.Graphics, pen, drawing, plot, visibleCountForDrawing, min, max);
                else if (drawing.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawFibonacciExtension(e.Graphics, pen, labelBrush, drawing, plot, visibleCountForDrawing, min, max);
                else if (drawing.Tool == ExtraDrawingTool.Measure)
                    DrawMeasure(e.Graphics, pen, labelBrush, labelBack, font, drawing, plot, visibleCountForDrawing, min, max);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Length > 0)
            {
                using var previewPen = new Pen(Color.FromArgb(155, 80, 45), 1.2f) { DashStyle = DashStyle.Dash };
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                {
                    DrawMeasurePreview(e.Graphics, previewPen, labelBrush, labelBack, font, extraDrawingPoints[0], extraDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
                }
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                {
                    DrawPitchforkPreview(e.Graphics, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                }
                else if (activeExtraDrawingTool == ExtraDrawingTool.FibonacciExtension)
                {
                    DrawFibonacciExtensionPreview(e.Graphics, previewPen, labelBrush, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                }
            }

            e.Graphics.Restore(state);
        }

        private static void DrawPitchfork(Graphics g, Pen pen, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            DrawPitchforkGeometry(g, pen, p1, p2, p3, plot);
            DrawExtraHandle(g, p1, true);
            DrawExtraHandle(g, p2, true);
            DrawExtraHandle(g, p3, true);
        }

        private static void DrawPitchforkPreview(Graphics g, Pen pen, Point[] points, Point current, Rectangle plot)
        {
            if (points.Length == 1)
                return;
            var p1 = points[0];
            var p2 = points.Length > 1 ? points[1] : current;
            var p3 = points.Length > 2 ? points[2] : current;
            DrawPitchforkGeometry(g, pen, p1, p2, p3, plot);
        }

        private static void DrawPitchforkGeometry(Graphics g, Pen pen, PointF p1, PointF p2, PointF p3, Rectangle plot)
        {
            var midpoint = new PointF((p2.X + p3.X) / 2f, (p2.Y + p3.Y) / 2f);
            var dx = midpoint.X - p1.X;
            var dy = midpoint.Y - p1.Y;
            if (Math.Abs(dx) + Math.Abs(dy) < 0.001f)
                return;

            DrawInfiniteLine(g, pen, p1, dx, dy, plot);
            DrawInfiniteLine(g, pen, p2, dx, dy, plot);
            DrawInfiniteLine(g, pen, p3, dx, dy, plot);
            g.DrawLine(pen, p2, p3);
        }

        private static void DrawInfiniteLine(Graphics g, Pen pen, PointF point, float dx, float dy, Rectangle plot)
        {
            const float length = 5000f;
            g.DrawLine(pen, point.X - dx * length, point.Y - dy * length, point.X + dx * length, point.Y + dy * length);
        }

        private static void DrawFibonacciExtension(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f };

            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, c);
            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, c.X, y, plot.Right, y);
                var text = level switch
                {
                    0f => "0%",
                    0.382f => "38.2%",
                    0.618f => "61.8%",
                    1f => "100%",
                    1.272f => "127.2%",
                    1.618f => "161.8%",
                    _ => "261.8%"
                };
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, Math.Min(c.X + 4, plot.Right - 45), y - 8);
            }

            DrawExtraHandle(g, a, true);
            DrawExtraHandle(g, b, true);
            DrawExtraHandle(g, c, true);
        }

        private static void DrawFibonacciExtensionPreview(Graphics g, Pen pen, Brush labelBrush, Point[] points, Point current, Rectangle plot)
        {
            if (points.Length == 0)
                return;
            var a = points[0];
            var b = points.Length > 1 ? points[1] : current;
            var c = points.Length > 2 ? points[2] : current;
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f };
            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, c);
            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, c.X, y, plot.Right, y);
                g.DrawString(level switch
                {
                    0f => "0%",
                    0.382f => "38.2%",
                    0.618f => "61.8%",
                    1f => "100%",
                    1.272f => "127.2%",
                    1.618f => "161.8%",
                    _ => "261.8%"
                }, SystemFonts.DefaultFont, labelBrush, Math.Min(c.X + 4, plot.Right - 45), y - 8);
            }
        }

        private static void DrawMeasure(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, Font font, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var start = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var end = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            DrawMeasureLine(g, pen, labelBrush, labelBack, font, start, end, plot, visibleCount, min, max);
        }

        private static void DrawMeasurePreview(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, Font font, Point start, Point end, Rectangle plot, int visibleCount, double min, double max)
        {
            DrawMeasureLine(g, pen, labelBrush, labelBack, font, start, end, plot, visibleCount, min, max);
        }

        private static void DrawMeasureLine(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, Font font, PointF start, PointF end, Rectangle plot, int visibleCount, double min, double max)
        {
            g.DrawLine(pen, start, end);
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length > 0.1)
            {
                var ux = (float)(dx / length);
                var uy = (float)(dy / length);
                g.DrawLine(pen, start.X - uy * 6, start.Y + ux * 6, start.X + uy * 6, start.Y - ux * 6);
                g.DrawLine(pen, end.X - uy * 6, end.Y + ux * 6, end.X + uy * 6, end.Y - ux * 6);
            }

            var startPrice = ScreenToPrice(start.Y, plot, min, max);
            var endPrice = ScreenToPrice(end.Y, plot, min, max);
            var startX = ScreenToDataX(start.X, plot, visibleCount);
            var endX = ScreenToDataX(end.X, plot, visibleCount);
            var deltaPrice = endPrice - startPrice;
            var percent = Math.Abs(startPrice) > 1e-12 ? deltaPrice / startPrice * 100.0 : 0.0;
            var bars = Math.Abs(endX - startX);
            var text = $"{bars:0} کندل | {deltaPrice:0.##} | {percent:+0.##;-0.##;0}%";
            var size = g.MeasureString(text, font);
            var x = (start.X + end.X) / 2f - size.Width / 2f;
            var y = (start.Y + end.Y) / 2f - size.Height - 5;
            x = Math.Max(plot.Left + 2, Math.Min(x, plot.Right - size.Width - 2));
            y = Math.Max(plot.Top + 2, Math.Min(y, plot.Bottom - size.Height - 2));
            var rect = new RectangleF(x - 3, y - 2, size.Width + 6, size.Height + 4);
            g.FillRectangle(labelBack, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            g.DrawString(text, font, labelBrush, x, y);
        }

        private int HitTestExtraHandle(Point location, Rectangle plot, out int handle)
        {
            handle = 0;
            if (!TryGetDrawingContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;

            const double tolerance = 9;
            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                if (DistanceToPoint(location, p1) <= tolerance)
                {
                    handle = 1;
                    return i;
                }
                if (d.Tool != ExtraDrawingTool.Measure)
                {
                    var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, p2) <= tolerance)
                    {
                        handle = 2;
                        return i;
                    }
                    var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, p3) <= tolerance)
                    {
                        handle = 3;
                        return i;
                    }
                }
                else
                {
                    var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, p2) <= tolerance)
                    {
                        handle = 2;
                        return i;
                    }
                }
            }
            return -1;
        }

        private int HitTestExtraDrawing(Point location, Rectangle plot)
        {
            if (!TryGetDrawingContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;
            const double tolerance = 7;
            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var a = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                var b = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                if (d.Tool == ExtraDrawingTool.Measure)
                {
                    if (DistanceToSegment(location, a, b) <= tolerance)
                        return i;
                }
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                {
                    var c = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);
                    foreach (var level in new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f })
                    {
                        var y = c.Y + (b.Y - a.Y) * level;
                        if (DistanceToSegment(location, new PointF(c.X, y), new PointF(plot.Right, y)) <= tolerance)
                            return i;
                    }
                }
                else
                {
                    var c = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);
                    var midpoint = new PointF((b.X + c.X) / 2f, (b.Y + c.Y) / 2f);
                    var dx = midpoint.X - a.X;
                    var dy = midpoint.Y - a.Y;
                    if (Math.Abs(dx) + Math.Abs(dy) > 0.001f)
                    {
                        if (DistanceToInfiniteLine(location, a, dx, dy) <= tolerance ||
                            DistanceToInfiniteLine(location, b, dx, dy) <= tolerance ||
                            DistanceToInfiniteLine(location, c, dx, dy) <= tolerance)
                            return i;
                    }
                }
            }
            return -1;
        }

        private void MoveOrResizeExtraDrawing(int index, int handle, Point location)
        {
            if (index < 0 || index >= extraDrawings.Count)
                return;
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            var d = extraDrawings[index];
            if (handle == 1)
            {
                d.X1 = ScreenToDataX(location.X, plot, visibleCountForDrawing);
                d.Y1 = ScreenToPrice(location.Y, plot, min, max);
                return;
            }
            if (handle == 2)
            {
                d.X2 = ScreenToDataX(location.X, plot, visibleCountForDrawing);
                d.Y2 = ScreenToPrice(location.Y, plot, min, max);
                return;
            }
            if (handle == 3)
            {
                d.X3 = ScreenToDataX(location.X, plot, visibleCountForDrawing);
                d.Y3 = ScreenToPrice(location.Y, plot, min, max);
                return;
            }

            var previousX = ScreenToDataX(draggingExtraLastPoint.X, plot, visibleCountForDrawing);
            var previousY = ScreenToPrice(draggingExtraLastPoint.Y, plot, min, max);
            var currentX = ScreenToDataX(location.X, plot, visibleCountForDrawing);
            var currentY = ScreenToPrice(location.Y, plot, min, max);
            var dx = currentX - previousX;
            var dy = currentY - previousY;
            d.X1 += dx;
            d.Y1 += dy;
            d.X2 += dx;
            d.Y2 += dy;
            if (d.Tool != ExtraDrawingTool.Measure)
            {
                d.X3 += dx;
                d.Y3 += dy;
            }
        }

        private static void DrawExtraHandle(Graphics g, PointF point, bool selected)
        {
            if (!selected)
                return;
            using var brush = new SolidBrush(Color.White);
            using var pen = new Pen(Color.FromArgb(190, 55, 35), 1.3f);
            const float radius = 4f;
            g.FillEllipse(brush, point.X - radius, point.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(pen, point.X - radius, point.Y - radius, radius * 2, radius * 2);
        }

        private static double DistanceToInfiniteLine(Point p, PointF origin, float dx, float dy)
        {
            var px = p.X - origin.X;
            var py = p.Y - origin.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length < 1e-9)
                return double.MaxValue;
            return Math.Abs(px * dy - py * dx) / length;
        }
    }
}
