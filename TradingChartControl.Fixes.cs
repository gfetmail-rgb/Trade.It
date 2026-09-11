using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private double chartPanCompensation;

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_SIZE = 0x0005;

            if (m.Msg == WM_SIZE || m.Msg == WM_PAINT || m.Msg == WM_MOUSEMOVE ||
                m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONUP)
            {
                EnsureChartPanCompensation();
            }

            var detachedExtraMouseDown = false;
            if (m.Msg == WM_LBUTTONDOWN && !ExtraDrawingActive && !extraDraggingHandleActive &&
                !IsExtraDrawingHit(GetMousePointFromMessage(m)))
            {
                MouseDown -= ExtraDrawing_MouseDown;
                detachedExtraMouseDown = true;
                extraInputHandled = false;
            }

            base.WndProc(ref m);

            if (detachedExtraMouseDown)
                MouseDown += ExtraDrawing_MouseDown;

            if (m.Msg == WM_MOUSEMOVE && verticalAxisDrag && Capture && points.Count > 1)
            {
                var delta = Cursor.Position.Y - PointToScreen(verticalAxisStartPoint).Y;
                verticalZoom = Math.Clamp(
                    verticalAxisStartZoom * Math.Exp(-delta / 200.0),
                    0.1,
                    20.0);
                Invalidate();
            }

            if (m.Msg == WM_PAINT)
                DrawChartBoundaryAndAxisOverlay();
        }

        private Point GetMousePointFromMessage(Message m)
        {
            var x = (short)(long)m.LParam;
            var y = (short)((long)m.LParam >> 16);
            return new Point(x, y);
        }

        private bool IsExtraDrawingHit(Point location)
        {
            if (extraDrawings.Count == 0)
                return false;

            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return false;

            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);

                if (DistanceToPoint(location, p1) <= 10f ||
                    DistanceToPoint(location, p2) <= 10f ||
                    DistanceToPoint(location, p3) <= 10f)
                    return true;

                if (d.Tool == ExtraDrawingTool.FibonacciExtension &&
                    HitTestFibonacciLevel(location, d, plot, visibleCountForDrawing, min, max))
                    return true;

                if (d.Tool == ExtraDrawingTool.Measure &&
                    DistanceToSegment(location, p1, p2) <= 7f)
                    return true;
            }

            return false;
        }

        private void EnsureChartPanCompensation()
        {
            var plot = GetPlotRectangle();
            var desired = plot.Width * 0.25;
            var delta = desired - chartPanCompensation;
            if (Math.Abs(delta) < 0.01)
                return;

            horizontalPanOffset += delta;
            chartPanCompensation = desired;
        }

        private void DrawChartBoundaryAndAxisOverlay()
        {
            if (points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;

            GetVerticalRange(visible, out var min, out var max);

            using var g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using var backgroundBrush = new SolidBrush(BackColor);
            g.FillRectangle(backgroundBrush, 0, 0, Width, plot.Top);
            g.FillRectangle(backgroundBrush, 0, plot.Top, plot.Left, plot.Height);
            g.FillRectangle(backgroundBrush, plot.Right, plot.Top, Math.Max(0, Width - plot.Right), plot.Height);
            g.FillRectangle(backgroundBrush, 0, plot.Bottom, Width, Math.Max(0, Height - plot.Bottom));

            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);

            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var labelBack = new SolidBrush(Color.FromArgb(248, 248, 248));
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);

            for (var i = 0; i <= 5; i++)
            {
                var value = max - (max - min) * i / 5.0;
                var y = PriceToScreen(value, plot, min, max);
                var text = value.ToString("0.##");
                var size = g.MeasureString(text, axisTextFont);
                var x = plot.Right + 4f;
                g.FillRectangle(labelBack, x - 2f, y - size.Height / 2f - 1f, size.Width + 4f, size.Height + 2f);
                g.DrawString(text, axisTextFont, textBrush, x, y - size.Height / 2f);
            }

            var step = plot.Width / (double)Math.Max(1, visible.Count);
            var timeLabelCount = Math.Min(6, visible.Count);
            for (var n = 0; n < timeLabelCount; n++)
            {
                var index = timeLabelCount == 1
                    ? 0
                    : (int)Math.Round(n * (visible.Count - 1.0) / (timeLabelCount - 1.0));
                var x = (float)(plot.Left + step * (index + 0.5) - 0.25 * plot.Width + horizontalPanOffset);
                var text = visible[index].Date.ToString("yyyy/MM/dd");
                var size = g.MeasureString(text, axisTextFont);
                var left = Math.Clamp(x - size.Width / 2f, plot.Left, Math.Max(plot.Left, plot.Right - size.Width));
                var top = plot.Bottom + 4f;
                g.FillRectangle(labelBack, left - 2f, top - 1f, size.Width + 4f, size.Height + 2f);
                g.DrawString(text, axisTextFont, textBrush, left, top);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var x = (float)(plot.Left + step * (crosshairIndex + 0.5) - 0.25 * plot.Width + horizontalPanOffset);
                var y = Math.Clamp(crosshairPoint.Y, plot.Top, plot.Bottom);
                var priceText = ScreenToPrice(y, plot, min, max).ToString("0.##");
                var timeText = visible[crosshairIndex].Date.ToString("yyyy/MM/dd");

                var priceSize = g.MeasureString(priceText, axisTextFont);
                var priceX = plot.Right + 4f;
                g.FillRectangle(labelBack, priceX - 2f, y - priceSize.Height / 2f - 1f, priceSize.Width + 4f, priceSize.Height + 2f);
                g.DrawString(priceText, axisTextFont, textBrush, priceX, y - priceSize.Height / 2f);

                var timeSize = g.MeasureString(timeText, axisTextFont);
                var timeX = Math.Clamp(x - timeSize.Width / 2f, plot.Left, Math.Max(plot.Left, plot.Right - timeSize.Width));
                var timeY = plot.Bottom + 4f;
                g.FillRectangle(labelBack, timeX - 2f, timeY - 1f, timeSize.Width + 4f, timeSize.Height + 2f);
                g.DrawString(timeText, axisTextFont, textBrush, timeX, timeY);
            }
        }
    }
}
