namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {


        private void NoDateAxisLabel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ExtraDrawingActive)
                CancelExtraDrawing();
        }

        private void NoDateAxis_MouseMove(object? sender, MouseEventArgs e)
        {
            UpdateNoDateAxisLabel();
        }

        private void NoDateAxis_Resize(object? sender, EventArgs e)
        {
            UpdateNoDateAxisLabel();
        }

        private void UpdateNoDateAxisLabel()
        {
            if (noDateAxisLabel == null)
                return;

            if (!IsSyntheticNoDateAxis() || !showCrosshair || crosshairIndex < 0 || crosshairIndex >= points.Count)
            {
                noDateAxisLabel.Visible = false;
                return;
            }

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visibleCountForDrawing = Math.Max(1, endIndex - firstIndex);
            if (crosshairIndex >= visibleCountForDrawing)
            {
                noDateAxisLabel.Visible = false;
                return;
            }

            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var initialOffset = -plot.Width * 0.25;
            var x = plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset;
            // Keep covering the renderer's synthetic date label, but do not display a candle number.
            var text = string.Empty;

            // The renderer still has a legacy date-label path for the crosshair.
            // Make this no-date label wide enough to fully cover that synthetic
            // date so no fake calendar value can remain visible underneath it.
            var width = Math.Max(170, TextRenderer.MeasureText(text, Font).Width + 18);
            var height = Math.Max(20, Font.Height + 4);
            var left = (int)Math.Round(x - width / 2.0);
            left = Math.Clamp(left, plot.Left, Math.Max(plot.Left, plot.Right - width));
            var top = plot.Bottom + 2;

            noDateAxisLabel.Bounds = new Rectangle(left, top, width, height);
            noDateAxisLabel.Font = Font;
            noDateAxisLabel.Text = text;
            noDateAxisLabel.Visible = true;
            noDateAxisLabel.BringToFront();
        }
    }
}