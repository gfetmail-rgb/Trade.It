using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private enum AdvancedDrawingTool
        {
            None,
            FibonacciRetracement,
            TextLabel,
            QuestionMark,
            ExclamationMark
        }

        private sealed class AdvancedDrawing
        {
            public AdvancedDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public string? Text { get; set; }
            public DateTime? Date1 { get; set; }
            public DateTime? Date2 { get; set; }
            public Color? StrokeColor { get; set; }
            public float? StrokeWidth { get; set; }
            public string? StrokeStyle { get; set; }
            public float FontSize { get; set; } = 26f;
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
        private int draggingAdvancedHandle;

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
            // فعال‌سازی متن نباید وضعیت یا رندر Drawingهای تکمیل‌شده‌ی Extra
            // مثل Pitchfork را دستکاری کند. فقط ابزار Advanced فعلی تغییر می‌کند.
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == AdvancedDrawingTool.TextLabel
                ? AdvancedDrawingTool.None
                : AdvancedDrawingTool.TextLabel;
            advancedDrawingInProgress = false;
            Cursor = Cursors.Default;
            Focus();
            Invalidate();
        }

        public void ActivateQuestionMark() => ActivateAdvancedMarker(AdvancedDrawingTool.QuestionMark);
        public void ActivateExclamationMark() => ActivateAdvancedMarker(AdvancedDrawingTool.ExclamationMark);

        private void ActivateAdvancedMarker(AdvancedDrawingTool tool)
        {
            EnsureAdvancedDrawingEvents();
            CancelDrawing();
            CancelExtraDrawing();
            activeAdvancedDrawingTool = activeAdvancedDrawingTool == tool ? AdvancedDrawingTool.None : tool;
            advancedDrawingInProgress = false;
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
            draggingAdvancedLastPoint = Point.Empty;
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

            if (count == advancedDataCount && first == advancedFirstDate && last == advancedLastDate)
                return;

            // تغییر تایم‌فریم/داده نباید Drawingهای تکمیل‌شده را پاک کند.
            // مختصات آنها با Date1/Date2 به تایم‌فریم جدید نگاشت می‌شوند.
            advancedDataCount = count;
            advancedFirstDate = first;
            advancedLastDate = last;
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
                drawingSelectionInputHandled = true;
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

                if (activeAdvancedDrawingTool is AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark)
                {
                    AddAdvancedMarker(e.Location, activeAdvancedDrawingTool);
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
            var hit = HitTestAdvancedDrawing(e.Location, plot, out var hitHandle);
            var controlPressed = (ModifierKeys & Keys.Control) == Keys.Control;
            if (hit >= 0)
            {
                SelectDrawing(DrawingSelectionKind.Advanced, hit, controlPressed);
                drawingSelectionInputHandled = true;
                if (!controlPressed)
                {
                    draggingAdvancedDrawingIndex = hit;
                    draggingAdvancedHandle = hitHandle;
                    draggingAdvancedLastPoint = e.Location;
                    DeferAdvancedMouseState();
                }
                Focus();
                Invalidate();
            }
            else if (!controlPressed)
            {
                ClearDrawingSelection();
                Invalidate();
            }
        }

        private void AdvancedDrawing_MouseMove(object? sender, MouseEventArgs e)
        {
            if (draggingAdvancedDrawingIndex >= 0 && draggingAdvancedDrawingIndex < advancedDrawings.Count && e.Button == MouseButtons.Left)
            {
                if (TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                {
                    var previousX = ScreenToDataX(draggingAdvancedLastPoint.X, plot, visibleCountForDrawing) + firstIndex;
                    var previousY = ScreenToPrice(draggingAdvancedLastPoint.Y, plot, min, max);
                    var currentX = ScreenToDataX(e.Location.X, plot, visibleCountForDrawing) + firstIndex;
                    var currentY = ScreenToPrice(e.Location.Y, plot, min, max);
                    var dx = currentX - previousX;
                    var dy = currentY - previousY;
                    var d = advancedDrawings[draggingAdvancedDrawingIndex];

                    if (d.Tool is AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark && draggingAdvancedHandle > 0)
                    {
                        var resizeDelta = e.Location.X - draggingAdvancedLastPoint.X;
                        d.FontSize = Math.Clamp(d.FontSize + resizeDelta * 0.45f, 12f, 80f);
                    }
                    else if (d.Tool == AdvancedDrawingTool.FibonacciRetracement && draggingAdvancedHandle == 1)
                    {
                        d.X1 = currentX;
                        d.Y1 = currentY;
                    }
                    else if (d.Tool == AdvancedDrawingTool.FibonacciRetracement && draggingAdvancedHandle == 2)
                    {
                        d.X2 = currentX;
                        d.Y2 = currentY;
                    }
                    else
                    {
                        d.X1 += dx;
                        d.Y1 += dy;
                        if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                        {
                            d.X2 += dx;
                            d.Y2 += dy;
                        }
                    }

                    d.Date1 = DataXToDate(d.X1);
                    if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                        d.Date2 = DataXToDate(d.X2);

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
                var wasDragging = draggingAdvancedDrawingIndex >= 0;
                draggingAdvancedDrawingIndex = -1;
                draggingAdvancedLastPoint = Point.Empty;
                draggingAdvancedHandle = 0;
                if (wasDragging)
                    AnalysisChanged?.Invoke(this, EventArgs.Empty);
                if (AdvancedDrawingActive || advancedDrawingInProgress)
                    DeferAdvancedMouseState();
            }
        }

        private void AdvancedDrawing_KeyDown(object? sender, KeyEventArgs e)
        {
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

            var x1 = ScreenToDataX(start.X, plot, visibleCountForDrawing) + firstIndex;
            var y1 = ScreenToPrice(start.Y, plot, min, max);
            var x2 = ScreenToDataX(end.X, plot, visibleCountForDrawing) + firstIndex;
            var y2 = ScreenToPrice(end.Y, plot, min, max);

            if (Math.Abs(x2 - x1) < 0.001 || Math.Abs(y2 - y1) < 1e-12)
                return;

            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = AdvancedDrawingTool.FibonacciRetracement,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Date1 = DataXToDate(x1),
                Date2 = DataXToDate(x2),
                StrokeColor = ChartAppearanceSettings.FibonacciRetracementColor,
                StrokeWidth = LineAppearanceSettings.DrawingLineWidth,
                StrokeStyle = LineAppearanceSettings.DrawingLineStyle.ToString()
            });
            ClearDrawingSelection();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        private void AddAdvancedMarker(Point location, AdvancedDrawingTool tool)
        {
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            var x = ScreenToDataX(location.X, plot, visibleCountForDrawing) + firstIndex;
            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = tool,
                X1 = x,
                Y1 = ScreenToPrice(location.Y, plot, min, max),
                Text = tool == AdvancedDrawingTool.QuestionMark ? "؟" : "!",
                Date1 = DataXToDate(x),
                FontSize = 28f,
                StrokeColor = ChartAppearanceSettings.TextLabelColor,
                StrokeWidth = LineAppearanceSettings.DrawingLineWidth,
                StrokeStyle = LineAppearanceSettings.DrawingLineStyle.ToString()
            });
            ClearDrawingSelection();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        private void AddAdvancedText(Point location, string text)
        {
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            advancedDrawings.Add(new AdvancedDrawing
            {
                Tool = AdvancedDrawingTool.TextLabel,
                X1 = ScreenToDataX(location.X, plot, visibleCountForDrawing) + firstIndex,
                Y1 = ScreenToPrice(location.Y, plot, min, max),
                Text = text,
                Date1 = DataXToDate(ScreenToDataX(location.X, plot, visibleCountForDrawing) + firstIndex),
                StrokeColor = ChartAppearanceSettings.TextLabelColor,
                StrokeWidth = LineAppearanceSettings.DrawingLineWidth,
                StrokeStyle = LineAppearanceSettings.DrawingLineStyle.ToString()
            });
            ClearDrawingSelection();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool TryGetAdvancedContext(out Rectangle plot, out int visibleCountForDrawing, out double min, out double max)
        {
            plot = GetPlotRectangle();
            visibleCountForDrawing = GetDrawingLayoutCount();
            var visible = points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();
            if (visible.Count == 0)
            {
                min = max = 0;
                return false;
            }
            GetVerticalRange(visible, out min, out max);
            return true;
        }

        private void RenderAdvancedDrawings(Graphics g)
        {
            SyncAdvancedDrawingData();
            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));

            for (var i = 0; i < advancedDrawings.Count; i++)
            {
                var d = advancedDrawings[i];
                var isSelected = IsDrawingSelected(DrawingSelectionKind.Advanced, i);
                var color = d.StrokeColor ?? (d.Tool == AdvancedDrawingTool.FibonacciRetracement
                    ? ChartAppearanceSettings.FibonacciRetracementColor
                    : ChartAppearanceSettings.TextLabelColor);
                var baseWidth = d.StrokeWidth ?? LineAppearanceSettings.DrawingLineWidth;
                var width = isSelected ? baseWidth + 0.9f : baseWidth;
                using var pen = new Pen(color, width) { DashStyle = GetDrawingDashStyle(d.StrokeStyle) };

                if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                    DrawAdvancedFibonacci(g, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, isSelected);
                else if (d.Tool is AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark)
                    DrawAdvancedMarker(g, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, isSelected);
                else
                    if (d.Tool is AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark)
                    DrawAdvancedMarker(g, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, IsDrawingSelected(DrawingSelectionKind.Advanced, i));
                else
                    DrawAdvancedText(g, pen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }

            if (advancedDrawingInProgress && activeAdvancedDrawingTool == AdvancedDrawingTool.FibonacciRetracement && IsInsidePlot(advancedDrawingCurrentPoint))
            {
                using var previewPen = new Pen(ChartAppearanceSettings.FibonacciRetracementColor, LineAppearanceSettings.DrawingLineWidth) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                DrawFibonacciLevels(g, previewPen, labelBrush, advancedDrawingStartPoint, advancedDrawingCurrentPoint);
            }

        }

        private void DrawAdvancedTextLabels(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            SyncAdvancedDrawingData();

            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));

            for (var i = 0; i < advancedDrawings.Count; i++)
            {
                var d = advancedDrawings[i];
                if (d.Tool is not (AdvancedDrawingTool.TextLabel or AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark))
                    continue;

                var baseWidth = d.StrokeWidth ?? LineAppearanceSettings.DrawingLineWidth;
                var width = IsDrawingSelected(DrawingSelectionKind.Advanced, i) ? baseWidth + 0.9f : baseWidth;
                using var pen = new Pen(d.StrokeColor ?? ChartAppearanceSettings.TextLabelColor, width) { DashStyle = GetDrawingDashStyle(d.StrokeStyle) };
                DrawAdvancedText(g, pen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }
        }

        private void DrawAdvancedFibonacci(Graphics g, Pen pen, Brush labelBrush, AdvancedDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max, bool selected)
        {
            var start = DateToScreen(d.Date1, d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            var end = DateToScreen(d.Date2, d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
            DrawFibonacciLevels(g, pen, labelBrush, start, end);
            if (selected)
            {
                DrawAdvancedHandle(g, start);
                DrawAdvancedHandle(g, end);
            }
        }

        private void DrawAdvancedText(Graphics g, Pen pen, Brush labelBrush, Brush labelBack, AdvancedDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var point = DateToScreen(d.Date1, d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);
            var text = d.Text ?? string.Empty;
            var size = g.MeasureString(text, font);
            var rect = new RectangleF(point.X, point.Y - size.Height, size.Width + 8f, size.Height + 6f);
            g.FillRectangle(labelBack, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            g.DrawString(text, font, labelBrush, point.X + 4f, point.Y - size.Height + 3f);
        }

        private void DrawAdvancedMarker(Graphics g, Pen pen, Brush labelBrush, AdvancedDrawing d, Rectangle plot, int visibleCountForDrawing, double min, double max, bool selected)
        {
            var point = DateToScreen(d.Date1, d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
            using var font = new Font("Segoe UI", Math.Clamp(d.FontSize, 12f, 80f), FontStyle.Bold);
            var size = g.MeasureString(d.Text ?? string.Empty, font);
            var bounds = new RectangleF(point.X - size.Width / 2f - 2f, point.Y - size.Height / 2f - 2f, size.Width + 4f, size.Height + 4f);
            g.DrawString(d.Text ?? string.Empty, font, labelBrush, bounds.X + 2f, bounds.Y + 2f);
            if (selected)
            {
                DrawAdvancedHandle(g, new PointF(bounds.Left, bounds.Top));
                DrawAdvancedHandle(g, new PointF(bounds.Right, bounds.Top));
                DrawAdvancedHandle(g, new PointF(bounds.Right, bounds.Bottom));
                DrawAdvancedHandle(g, new PointF(bounds.Left, bounds.Bottom));
            }
        }

        private static void DrawFibonacciLevels(Graphics g, Pen pen, Brush labelBrush, PointF start, PointF end)
        {
            var left = Math.Min(start.X, end.X);
            var right = Math.Max(start.X, end.X);
            var height = end.Y - start.Y;

            // Point 1 is the 100% anchor and point 2 is the 0% anchor.
            // Values above 100% are Fibonacci extensions beyond point 1.
            var levels = ChartAppearanceSettings.GetEnabledFibonacciRetracementLevels();

            foreach (var level in levels)
            {
                var y = start.Y + height * (1f - level.Value);
                g.DrawLine(pen, left, y, right, y);

                var text = level.Text;

                var textSize = g.MeasureString(text, SystemFonts.DefaultFont);
                var textX = right + 4f;
                if (textX + textSize.Width > g.VisibleClipBounds.Right)
                    textX = left - textSize.Width - 4f;

                var textY = y - textSize.Height / 2f;
                if (textY < g.VisibleClipBounds.Top)
                    textY = g.VisibleClipBounds.Top;
                else if (textY + textSize.Height > g.VisibleClipBounds.Bottom)
                    textY = g.VisibleClipBounds.Bottom - textSize.Height;

                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, textX, textY);
            }
        }

        private static void DrawAdvancedHandle(Graphics g, PointF point)
        {
            using var brush = new SolidBrush(Color.White);
            using var pen = new Pen(ChartAppearanceSettings.FibonacciRetracementColor, 1.3f);
            const float r = 4f;
            g.FillEllipse(brush, point.X - r, point.Y - r, r * 2f, r * 2f);
            g.DrawEllipse(pen, point.X - r, point.Y - r, r * 2f, r * 2f);
        }

        private int HitTestAdvancedDrawing(Point location, Rectangle plot, out int handle)
        {
            handle = 0;
            if (!TryGetAdvancedContext(out _, out var visibleCountForDrawing, out var min, out var max))
                return -1;

            for (var i = advancedDrawings.Count - 1; i >= 0; i--)
            {
                var d = advancedDrawings[i];
                var p1 = DateToScreen(d.Date1, d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                if (d.Tool is AdvancedDrawingTool.QuestionMark or AdvancedDrawingTool.ExclamationMark)
                {
                    using var markerFont = new Font("Segoe UI", Math.Clamp(d.FontSize, 12f, 80f), FontStyle.Bold);
                    var markerSize = MeasureText(d.Text ?? string.Empty, markerFont);
                    var markerBounds = new RectangleF(p1.X - markerSize.Width / 2f - 2f, p1.Y - markerSize.Height / 2f - 2f, markerSize.Width + 4f, markerSize.Height + 4f);
                    var corners = new[] { new PointF(markerBounds.Left, markerBounds.Top), new PointF(markerBounds.Right, markerBounds.Top), new PointF(markerBounds.Right, markerBounds.Bottom), new PointF(markerBounds.Left, markerBounds.Bottom) };
                    for (var corner = 0; corner < corners.Length; corner++)
                    {
                        if (DistanceToPoint(location, corners[corner]) <= 9f)
                        {
                            handle = corner + 1;
                            return i;
                        }
                    }
                    if (markerBounds.Contains(location))
                        return i;
                    continue;
                }

                if (DistanceToPoint(location, p1) <= 10f)
                {
                    handle = d.Tool == AdvancedDrawingTool.FibonacciRetracement ? 1 : 0;
                    return i;
                }

                if (d.Tool == AdvancedDrawingTool.FibonacciRetracement)
                {
                    var p2 = DateToScreen(d.Date2, d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                    if (DistanceToPoint(location, p2) <= 10f)
                    {
                        handle = 2;
                        return i;
                    }
                    if (DistanceToSegment(location, p1, p2) <= 6f)
                    {
                        handle = 0;
                        return i;
                    }
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

        private DateTime? DataXToDate(double absoluteX)
        {
            if (points.Count == 0)
                return null;

            // مختصات فضای خالی سمت راست آخرین کندل نباید به آخرین تاریخ موجود
            // محدود شود؛ در این حالت null باعث می‌شود DateToDataX از X ذخیره‌شده استفاده کند.
            if (absoluteX > points.Count - 1)
                return null;

            var x = Math.Clamp(absoluteX, 0.0, points.Count - 1);
            var leftIndex = (int)Math.Floor(x);
            if (leftIndex >= points.Count - 1)
                return points[^1].Date;

            var fraction = x - leftIndex;
            var left = points[leftIndex].Date;
            var right = points[leftIndex + 1].Date;
            var spanTicks = right.Ticks - left.Ticks;
            if (spanTicks <= 0)
                return left;

            return new DateTime(left.Ticks + (long)Math.Round(spanTicks * fraction), left.Kind);
        }

        private static SizeF MeasureText(string text, Font font)
        {
            using var bmp = new Bitmap(1, 1);
            using var g = Graphics.FromImage(bmp);
            return g.MeasureString(text, font);
        }

        private string? PromptAdvancedText()
        {
            using var form = new AdvancedTextPromptForm();
            return form.ShowDialog(FindForm()) == DialogResult.OK
                ? form.InputText
                : null;
        }
    }
}