using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private enum AdvancedDrawingTool
        {
            None,
            FibonacciRetracement,
            TextLabel
        }

        private sealed class AdvancedDrawing
        {
            public AdvancedDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public string? Text { get; set; }
        }

        private readonly List<AdvancedDrawing> advancedDrawings = new();
        private AdvancedDrawingTool activeAdvancedDrawingTool;
        private bool advancedDrawingInProgress;
        private Point advancedDrawingStartPoint;
        private Point advancedDrawingCurrentPoint;
        private int selectedAdvancedDrawingIndex = -1;
        private int advancedDataCount = -1;
        private DateTime advancedFirstDate;
        private DateTime advancedLastDate;
        private bool advancedDrawingEventsInitialized;
        private int draggingAdvancedDrawingIndex = -1;
        private Point draggingAdvancedLastPoint;

        public bool AdvancedDrawingActive => activeAdvancedDrawingTool != AdvancedDrawingTool.None;

        public void ActivateFibonacciRetracement()
        {
            EnsureAdvancedDrawingEvents();
            CancelDrawing();
            CancelExtraDrawing();
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement
                ? AdvancedDrawingTool.None
                : AdvancedDrawingTool.FibonacciRetracement;
            advancedDrawingInProgress = false;
            Cursor = activeAdvancedDrawingTool == AdvancedDrawingTool.None ? Cursors.Default : Cursors.Cross;
            Focus();
            Invalidate();
        }

        public void ActivateTextLabel()
        {
            EnsureAdvancedDrawingEvents();
            CancelDrawing();
            CancelExtraDrawing();
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == AdvancedDrawingTool.TextLabel
                ? AdvancedDrawingTool.None
                : AdvancedDrawingTool.TextLabel;
            advancedDrawingInProgress = false;
            Cursor = Cursors.Default;
            Focus();
            Invalidate();
        }

        public void CancelAdvancedDrawing()
        {
            activeAdvancedDrawingTool = AdvancedDrawingTool.None;
            advancedDrawingInProgress = false;
            advancedDrawingStartPoint = Point.Empty;
            advancedDrawingCurrentPoint = Point.Empty;
            draggingAdvancedDrawingIndex = -1;
            draggingAdvancedLastPoint = Point.Empty;
            Capture = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private void EnsureAdvancedDrawingEvents()
        {
            if (advancedDrawingEventsInitialized)
                return;

            advancedDrawingEventsInitialized = true;
            Paint += AdvancedDrawing_Paint;
            MouseDown += AdvancedDrawing_MouseDown;
            MouseMove += AdvancedDrawing_MouseMove;
            MouseUp += AdvancedDrawing_MouseUp;
            KeyDown += AdvancedDrawing_KeyDown;
        }

        private void SyncAdvancedDrawingData()
        {
            var count = points.Count;
            var first = count > 0 ? points[0].Date : DateTime.MinValue;
            var last = count > 0 ? points[^1].Date : DateTime.MinValue;

            if (count != advancedDataCount || first != advancedFirstDate || last != advancedLastDate)
            {
                advancedDrawings.Clear();
                selectedAdvancedDrawingIndex = -1;
                advancedDataCount = count;
                advancedFirstDate = first;
                advancedLastDate = last;
            }
        }

        private void AdvancedDrawing_MouseDown(object? sender, MouseEventArgs e)
        {
            SyncAdvancedDrawingData();

            if (e.Button == MouseButtons.Right)
            {
                if (AdvancedDrawingActive || advancedDrawingInProgress)
                    CancelAdvancedDrawing();
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            if (AdvancedDrawingActive)
            {
                if (!IsInsidePlot(e.Location))
                    return;

                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;

                if (activeAdvancedDrawingTool == AdvancedDrawingTool.TextLabel)
                {
                    var text = PromptAdvancedText();
                    if (!string.IsNullOrWhiteSpace(text))
                        AddAdvancedText(e.Location, text.Trim());
                    CancelAdvancedDrawing();
                    DeferAdvancedMouseState();
                    return;
                }

                if (!advancedDrawingInProgress)
                {
                    advancedDrawingStartPoint = e.Location;
                    advancedDrawingCurrentPoint = e.Location;
                    advancedDrawingInProgress = true;
                    Invalidate();
                    DeferAdvancedMouseState();
                    return;
                }

                advancedDrawingCurrentPoint = e.Location;
                AddAdvancedFibonacci(advancedDrawingStartPoint, advancedDrawingCurrentPoint);
                CancelAdvancedDrawing();
                DeferAdvancedMouseState();
                return;
            }

            var plot = GetPlotRectangle();
            var hit = HitTestAdvancedDrawing(e.Location, plot);
            if (hit >= 0)
            {
                selectedAdvancedDrawingIndex = hit;
                draggingAdvancedDrawingIndex = hit;
                draggingAdvancedLastPoint = e.Location;
                Focus();
                Invalidate();
                DeferAdvancedMouseState();
            }
        }

        private void AdvancedDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (draggingAdvancedDrawingIndex >= 0 && draggingAdvancedDrawingIndex < advancedDrawings.Count && e.Button == MouseButtons.Left)
            {
                if (TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                {
                    var previousX = ScreenToDataX(draggingAdvancedLastPoint.X, plot, visibleCountForDrawing);
                    var previousY = ScreenToPrice(draggingAdvancedLastPoint.Y, plot, min, max);
                    var currentX = ScreenToDataX(e.Location.X, plot, visibleCountForDrawing);
                    var currentY = ScreenToPrice(e.Location.Y, plot, min, max);
                    var dx = currentX - previousX;
                    var dy = currentY - previousY;
                    var d = advancedDrawings[draggingAdvancedDrawingIndex];
                    d.X1 += dx;
                    d.Y1 += dy;
                    if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                    {
                        d.X2 += dx;
                        d.Y2 += dy;
                    }
                    draggingAdvancedLastPoint = e.Location;
                    Invalidate();
                    DeferAdvancedMouseState();
                }
                return;
            }

            if (!AdvancedDrawingActive)
                return;

            panning = false;
            horizontalAxisDrag = false;
            verticalAxisDrag = false;

            if (advancedDrawingInProgress && activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement)
            {
                advancedDrawingCurrentPoint = e.Location;
                Invalidate();
                DeferAdvancedMouseState();
            }
        }

        private void AdvancedDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingAdvancedDrawingIndex = -1;
                draggingAdvancedLastPoint = Point.Empty;
                if (AdvancedDrawingActive || advancedDrawingInProgress)
                    DeferAdvancedMouseState();
            }
        }

        private void AdvancedDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedAdvancedDrawingIndex >= 0 && selectedAdvancedDrawingIndex < advancedDrawings.Count)
            {
                advancedDrawings.RemoveAt(selectedAdvancedDrawingIndex);
                selectedAdvancedDrawingIndex = -1;
                draggingAdvancedDrawingIndex = -1;
                draggingAdvancedLastPoint = Point.Empty;
                Invalidate();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.Escape && AdvancedDrawingActive)
            {
                CancelAdvancedDrawing();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DeferAdvancedMouseState()
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
                Cursor = activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement
                    ? Cursors.Cross
                    : Cursors.Default;
            }));
        }

        private void AddAdvancedFibonacci(Point start, Point end)
        {
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            var x1 = ScreenToDataX(start.X, plot, visibleCountForDrawing);
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, visibleCountForDrawing);
            var y2 = ScreenToPrice(end.Y, plot, min, max);

            if (Math.Abs(x2 - x1) < 0.001 || Math.Abs(y2 - y1) < 1e-12)
                return;

            advancedDrawings.Add(new AdvancedDrawing { Tool = AdvancedDrawingTool.FibonacciRetracement, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 });
            selectedAdvancedDrawingIndex = advancedDrawings.Count - 1;
        }

        private void AddAdvancedText(Point location, string text)
        {
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = AdvancedDrawingTool.TextLabel,
                X1 = ScreenToDataX(location.X, plot, visibleCountForDrawing),
                Y1 = ScreenToPrice(location.Y, plot, min, max),
                Text = text
            });
            selectedAdvancedDrawingIndex = -1;
        }

        private bool TryGetAdvancedContext(out Rectangle plot, out int visibleCountForDrawing, out double min, out double max)
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

        private void AdvancedDrawing_Paint(object? sender, PaintEventArgs e)
        {
            SyncAdvancedDrawingData();
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var normalPen = new Pen(Color.FromArgb(45, 105, 170), 1.2f);
            using var selectedPen = new Pen(Color.FromArgb(25, 75, 140), 2f);
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));
            using var previewPen = new Pen(Color.FromArgb(45, 105, 170), 1.2f) { DashStyle = DashStyle.Dash };

            for (var i = 0; i < advancedDrawings.Count; i++)
            {
                var d = advancedDrawings[i];
                var pen = i == selectedAdvancedDrawingIndex ? selectedPen : normalPen;
                if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                    DrawAdvancedFibonacci(e.Graphics, pen, labelBrush, d, plot, visibleCountForDrawing, min, max);
                else
                    DrawAdvancedText(e.Graphics, pen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }

            if (advancedDrawingInProgress && activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement && IsInsidePlot(advancedDrawingCurrentPoint))
                DrawFibonacciLevels(e.Graphics, previewPen, labelBrush, advancedDrawingStartPoint, advancedDrawingCurrentPoint);
        }

        private void DrawAdvancedTextLabels(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            SyncAdvancedDrawingData();

            using var normalPen = new Pen(Color.FromArgb(45, 105, 170), 1.2f);
            using var selectedPen = new Pen(Color.FromArgb(25, 75, 140), 2f);
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));

            for (var i = 0; i < advancedDrawings.Count; i++)
            {
                var d = advancedDrawings[i];
                if (d.Tool != AdvancedDrawingTool.TextLabel)
                    continue;

                var pen = i == selectedAdvancedDrawingIndex ? selectedPen : normalPen;
                DrawAdvancedText(g, pen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }
        }

        private void DrawAdvancedFibonacci(Graphics g, Pen pen, Brush labelBrush, AdvancedDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var start = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            var end = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
            DrawFibonacciLevels(g, pen, labelBrush, start, end);
            DrawAdvancedHandle(g, start);
            DrawAdvancedHandle(g, end);
        }

        private void DrawAdvancedText(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, AdvancedDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var point = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);
            var text = d.Text ?? string.Empty;
            var size = g.MeasureString(text, font);
            var rect = new RectangleF(point.X, point.Y - size.Height, size.Width + 8f, size.Height + 6f);
            g.FillRectangle(labelBack, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            g.DrawString(text, font, labelBrush, point.X + 4f, point.Y - size.Height + 3f);
        }

        private static void DrawFibonacciLevels(Graphics g, Pen pen, Brush labelBrush, PointF start, PointF end)
        {
            var left = Math.Min(start.X, end.X);
            var right = Math.Max(start.X, end.X);
            var height = end.Y - start.Y;
            var levels = new[] { 0f, 0.236f, 0.382f, 0.5f, 0.618f, 0.786f, 1f };
            foreach (var level in levels)
            {
                var y = start.Y + height * level;
                g.DrawLine(pen, left, y, right, y);
                var text = level switch { 0f => "0%", 0.236f => "23.6%", 0.382f => "38.2%", 0.5f => "50%", 0.618f => "61.8%", 0.786f => "78.6%", _ => "100%" };
                var size = g.MeasureString(text, SystemFonts.DefaultFont);
                var labelX = right + 4f;
                if (labelX + size.Width > g.VisibleClipBounds.Right)
                    labelX = left + 4f;
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
            }
        }

        private static void DrawAdvancedHandle(Graphics g, PointF point)
        {
            using var brush = new SolidBrush(Color.White);
            using var pen = new Pen(Color.FromArgb(45, 105, 170), 1.3f);
            const float r = 4f;
            g.FillEllipse(brush, point.X - r, point.Y - r, r * 2f, r * 2f);
            g.DrawEllipse(pen, point.X - r, point.Y - r, r * 2f, r * 2f);
        }

        private int HitTestAdvancedDrawing(Point location, Rectangle plot)
        {
            if (!TryGetAdvancedContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;

            for (var i = advancedDrawings.Count - 1; i >= 0; i--)
            {
                var d = advancedDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                if (DistanceToPoint(location, p1) <= 10f)
                    return i;

                if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                {
                    var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, p2) <= 10f || DistanceToSegment(location, p1, p2) <= 6f)
                        return i;
                }
                else
                {
                    using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);
                    var size = MeasureText(d.Text ?? string.Empty, font);
                    if (new RectangleF(p1.X, p1.Y - size.Height, size.Width + 8f, size.Height + 6f).Contains(location))
                        return i;
                }
            }
            return -1;
        }

        private static SizeF MeasureText(string text, Font font)
        {
            using var bmp = new Bitmap(1, 1);
            using var g = Graphics.FromImage(bmp);
            return g.MeasureString(text, font);
        }

        private string? PromptAdvancedText()
        {
            using var form = new Form { Width = 360, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, StartPosition = FormStartPosition.CenterParent, MinimizeBox = false, MaximizeBox = false, RightToLeft = RightToLeft.Yes, RightToLeftLayout = true, Text = "درج متن" };
            using var textBox = new TextBox { Dock = DockStyle.Top, Multiline = false };
            using var ok = new Button { Text = "تأیید", DialogResult = DialogResult.OK, Width = 80, Height = 30 };
            using var cancel = new Button { Text = "لغو", DialogResult = DialogResult.Cancel, Width = 80, Height = 30 };
            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 45, FlowDirection = FlowDirection.LeftToRight, RightToLeft = RightToLeft.Yes };
            buttons.Controls.Add(ok);
            buttons.Controls.Add(cancel);
            form.Controls.Add(textBox);
            form.Controls.Add(buttons);
            form.AcceptButton = ok;
            form.CancelButton = cancel;
            form.Shown += (_, _) => textBox.Focus();
            return form.ShowDialog(FindForm()) == DialogResult.OK ? textBox.Text : null;
        }
    }
}
