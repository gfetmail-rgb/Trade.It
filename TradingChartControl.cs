using Microsoft.VisualBasic.Devices;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        private int draggingAdvancedDrawingIndex = -1;
        private int draggingAdvancedHandle;
        private Point draggingAdvancedLastPoint;
        private bool advancedDrawingEventsInitialized;
        private int advancedDataCount = -1;
        private DateTime advancedFirstDate;
        private DateTime advancedLastDate;

        public bool AdvancedDrawingActive => activeAdvancedDrawingTool != AdvancedDrawingTool.None;

        public void ActivateFibonacciRetracement()
        {
            EnsureAdvancedDrawingEvents();
            CancelDrawing();
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement
                ? AdvancedDrawingTool.None
                : AdvancedDrawingTool.FibonacciRetracement;
            advancedDrawingInProgress = false;
            advancedDrawingStartPoint = Point.Empty;
            advancedDrawingCurrentPoint = Point.Empty;
            Cursor = activeAdvancedDrawingTool == AdvancedDrawingTool.None ? Cursors.Default : Cursors.Cross;
            Focus();
            Invalidate();
        }

        public void ActivateTextLabel()
        {
            EnsureAdvancedDrawingEvents();
            CancelDrawing();
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == AdvancedDrawingTool.TextLabel
                ? AdvancedDrawingTool.None
                : AdvancedDrawingTool.TextLabel;
            advancedDrawingInProgress = false;
            advancedDrawingStartPoint = Point.Empty;
            advancedDrawingCurrentPoint = Point.Empty;
            Cursor = activeAdvancedDrawingTool == AdvancedDrawingTool.None ? Cursors.Default : Cursors.Cross;
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
            draggingAdvancedHandle = 0;
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
                draggingAdvancedDrawingIndex = -1;
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

                if (activeAdvancedDrawingTool == AdvancedDrawingTool.TextLabel)
                {
                    var text = PromptForText();
                    if (!string.IsNullOrWhiteSpace(text))
                        AddAdvancedText(e.Location, text.Trim());
                    CancelAdvancedDrawing();
                    return;
                }

                if (!advancedDrawingInProgress)
                {
                    advancedDrawingStartPoint = e.Location;
                    advancedDrawingCurrentPoint = e.Location;
                    advancedDrawingInProgress = true;
                    Invalidate();
                    return;
                }

                advancedDrawingCurrentPoint = e.Location;
                AddAdvancedFibonacci(advancedDrawingStartPoint, advancedDrawingCurrentPoint);
                CancelAdvancedDrawing();
                return;
            }

            var plot = GetPlotRectangle();
            var hit = HitTestAdvancedHandle(e.Location, plot, out var handle);
            if (hit >= 0)
            {
                selectedAdvancedDrawingIndex = hit;
                draggingAdvancedDrawingIndex = hit;
                draggingAdvancedHandle = handle;
                draggingAdvancedLastPoint = e.Location;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            hit = HitTestAdvancedDrawing(e.Location, plot);
            if (hit >= 0)
            {
                selectedAdvancedDrawingIndex = hit;
                draggingAdvancedDrawingIndex = hit;
                draggingAdvancedHandle = 0;
                draggingAdvancedLastPoint = e.Location;
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = true;
                Cursor = Cursors.SizeAll;
                Invalidate();
                return;
            }

            selectedAdvancedDrawingIndex = -1;
            Invalidate();
        }

        private void AdvancedDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (advancedDrawingInProgress && activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement)
            {
                advancedDrawingCurrentPoint = e.Location;
                Invalidate();
                return;
            }

            if (draggingAdvancedDrawingIndex >= 0 && draggingAdvancedDrawingIndex < advancedDrawings.Count && Capture)
            {
                MoveOrResizeAdvancedDrawing(draggingAdvancedDrawingIndex, draggingAdvancedHandle, e.Location);
                draggingAdvancedLastPoint = e.Location;
                Invalidate();
            }
        }

        private void AdvancedDrawing_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (draggingAdvancedDrawingIndex >= 0)
            {
                draggingAdvancedDrawingIndex = -1;
                draggingAdvancedHandle = 0;
                Capture = false;
                Cursor = Cursors.Default;
            }
        }

        private void AdvancedDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedAdvancedDrawingIndex >= 0 && selectedAdvancedDrawingIndex < advancedDrawings.Count)
            {
                advancedDrawings.RemoveAt(selectedAdvancedDrawingIndex);
                selectedAdvancedDrawingIndex = -1;
                draggingAdvancedDrawingIndex = -1;
                draggingAdvancedHandle = 0;
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

        private void AddAdvancedFibonacci(Point start, Point end)
        {
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            var x1 = ScreenToDataX(start.X, plot, visibleCountForDrawing);
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, visibleCountForDrawing);
            var y2 = ScreenToPrice(end.Y, plot, min, max);

            if (Math.Abs(x2 - x1) < 0.001 || Math.Abs(y2 - y1) < 1e-12)
                return;

            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = AdvancedDrawingTool.FibonacciRetracement,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            });
            selectedAdvancedDrawingIndex = advancedDrawings.Count - 1;
        }

        private void AddAdvancedText(Point location, string text)
        {
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = AdvancedDrawingTool.TextLabel,
                X1 = ScreenToDataX(location.X, plot, visibleCountForDrawing),
                Y1 = ScreenToPrice(location.Y, plot, min, max),
                Text = text
            });
            selectedAdvancedDrawingIndex = advancedDrawings.Count - 1;
        }

        private bool TryGetDrawingContext(out Rectangle plot, out int visibleCountForDrawing, out double min, out double max)
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
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var normalPen = new Pen(Color.FromArgb(45, 105, 170), 1.2f);
            using var selectedPen = new Pen(Color.FromArgb(45, 105, 170), 2.2f);
            using var levelBrush = new SolidBrush(Color.FromArgb(65, 105, 160));
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));
            using var textFont = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);

            for (var i = 0; i < advancedDrawings.Count; i++)
            {
                var drawing = advancedDrawings[i];
                var pen = i == selectedAdvancedDrawingIndex ? selectedPen : normalPen;

                if (drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
                    DrawAdvancedFibonacci(e.Graphics, pen, levelBrush, labelBrush, drawing, plot, visibleCountForDrawing, min, max);
                else if (drawing.Tool == AdvancedDrawingTool.TextLabel)
                    DrawAdvancedText(e.Graphics, pen, labelBrush, labelBack, textFont, drawing, plot, visibleCountForDrawing, min, max);
            }

            if (advancedDrawingInProgress && activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement && IsInsidePlot(advancedDrawingCurrentPoint))
            {
                using var previewPen = new Pen(Color.FromArgb(45, 105, 170), 1.2f) { DashStyle = DashStyle.Dash };
                DrawAdvancedFibonacciPreview(e.Graphics, previewPen, levelBrush, labelBrush, advancedDrawingStartPoint, advancedDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
            }
        }

        private void DrawAdvancedFibonacci(Graphics g, Pen pen, Brush levelBrush, Brush labelBrush, AdvancedDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
            DrawFibonacciLevels(g, pen, labelBrush, start, end);
            DrawAdvancedHandle(g, start, selectedAdvancedDrawingIndex >= 0);
            DrawAdvancedHandle(g, end, selectedAdvancedDrawingIndex >= 0);
        }

        private void DrawAdvancedFibonacciPreview(Graphics g, Pen pen, Brush levelBrush, Brush labelBrush, Point start, Point end, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            DrawFibonacciLevels(g, pen, labelBrush, start, end);
        }

        private static void DrawFibonacciLevels(Graphics g, Pen pen, Brush labelBrush, PointF start, PointF end)
        {
            var left = Math.Min(start.X, end.X);
            var right = Math.Max(start.X, end.X);
            var height = end.Y - start.Y;
            var levels = new[] { 0f, 0.236f, 0.382f, 0.5f, 0.618f, 0.786f, 1f };

            foreach (var level in levels)
            {
                var y = end.Y + height * level;
                g.DrawLine(pen, left, y, right, y);
                var text = level switch
                {
                    0f => "0%",
                    0.236f => "23.6%",
                    0.382f => "38.2%",
                    0.5f => "50%",
                    0.618f => "61.8%",
                    0.786f => "78.6%",
                    _ => "100%"
                };
                var size = g.MeasureString(text, SystemFonts.DefaultFont);
                var labelX = right + 4;
                if (labelX + size.Width > g.VisibleClipBounds.Right)
                    labelX = left + 4;
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
            }
        }

        private void DrawAdvancedText(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, Font font, AdvancedDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var point = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var text = drawing.Text ?? string.Empty;
            var size = g.MeasureString(text, font);
            var rect = new RectangleF(point.X, point.Y - size.Height, size.Width + 6, size.Height + 4);
            g.FillRectangle(labelBack, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            g.DrawString(text, font, labelBrush, point.X + 3, point.Y - size.Height + 2);
        }

        private static void DrawAdvancedHandle(Graphics g, PointF point, bool selected)
        {
            if (!selected)
                return;

            using var brush = new SolidBrush(Color.White);
            using var pen = new Pen(Color.FromArgb(45, 105, 170), 1.3f);
            const float radius = 4f;
            g.FillEllipse(brush, point.X - radius, point.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(pen, point.X - radius, point.Y - radius, radius * 2, radius * 2);
        }

        private int HitTestAdvancedHandle(Point location, Rectangle plot, out int handle)
        {
            handle = 0;
            if (!TryGetDrawingContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;

            const double tolerance = 9;
            for (var i = advancedDrawings.Count - 1; i >= 0; i--)
            {
                var drawing = advancedDrawings[i];
                var first = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
                if (DistanceToPoint(location, first) <= tolerance)
                {
                    handle = 1;
                    return i;
                }

                if (drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
                {
                    var second = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, second) <= tolerance)
                    {
                        handle = 2;
                        return i;
                    }
                }
            }

            return -1;
        }

        private int HitTestAdvancedDrawing(Point location, Rectangle plot)
        {
            if (!TryGetDrawingContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;

            const double tolerance = 7;
            for (var i = advancedDrawings.Count - 1; i >= 0; i--)
            {
                var drawing = advancedDrawings[i];
                var first = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);

                if (drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
                {
                    var second = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
                    var left = Math.Min(first.X, second.X);
                    var right = Math.Max(first.X, second.X);
                    var height = second.Y - first.Y;
                    foreach (var level in new[] { 0f, 0.236f, 0.382f, 0.5f, 0.618f, 0.786f, 1f })
                    {
                        var y = second.Y + height * level;
                        if (DistanceToSegment(location, new PointF(left, y), new PointF(right, y)) <= tolerance)
                            return i;
                    }
                }
                else if (drawing.Tool == AdvancedDrawingTool.TextLabel)
                {
                    using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);
                    var text = drawing.Text ?? string.Empty;
                    var size = CreateGraphics().MeasureString(text, font);
                    var rect = new RectangleF(first.X, first.Y - size.Height, size.Width + 6, size.Height + 4);
                    if (rect.Contains(location))
                        return i;
                }
            }

            return -1;
        }

        private void MoveOrResizeAdvancedDrawing(int index, int handle, Point location)
        {
            if (index < 0 || index >= advancedDrawings.Count)
                return;
            if (!TryGetDrawingContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            var drawing = advancedDrawings[index];
            if (handle == 1)
            {
                drawing.X1 = ScreenToDataX(location.X, plot, visibleCountForDrawing);
                drawing.Y1 = ScreenToPrice(location.Y, plot, min, max);
                return;
            }

            if (handle == 2 && drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
            {
                drawing.X2 = ScreenToDataX(location.X, plot, visibleCountForDrawing);
                drawing.Y2 = ScreenToPrice(location.Y, plot, min, max);
                return;
            }

            var previousX = ScreenToDataX(draggingAdvancedLastPoint.X, plot, visibleCountForDrawing);
            var previousY = ScreenToPrice(draggingAdvancedLastPoint.Y, plot, min, max);
            var currentX = ScreenToDataX(location.X, plot, visibleCountForDrawing);
            var currentY = ScreenToPrice(location.Y, plot, min, max);
            var dx = currentX - previousX;
            var dy = currentY - previousY;

            drawing.X1 += dx;
            drawing.Y1 += dy;
            if (drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
            {
                drawing.X2 += dx;
                drawing.Y2 += dy;
            }
        }

        private string? PromptForText()
        {
            using var dialog = new Form
            {
                Text = "??? ???",
                Width = 360,
                Height = 145,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                RightToLeft = RightToLeft.Yes,
                RightToLeftLayout = true
            };

            var textBox = new TextBox
            {
                Location = new Point(15, 15),
                Width = 315,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var okButton = new Button
            {
                Text = "?????",
                DialogResult = DialogResult.OK,
                Location = new Point(170, 55),
                Width = 75
            };
            var cancelButton = new Button
            {
                Text = "??????",
                DialogResult = DialogResult.Cancel,
                Location = new Point(255, 55),
                Width = 75
            };

            dialog.Controls.Add(textBox);
            dialog.Controls.Add(okButton);
            dialog.Controls.Add(cancelButton);
            dialog.AcceptButton = okButton;
            dialog.CancelButton = cancelButton;
            dialog.Shown += (_, _) => textBox.Focus();

            return dialog.ShowDialog(this) == DialogResult.OK ? textBox.Text : null;
        }
    }
}
