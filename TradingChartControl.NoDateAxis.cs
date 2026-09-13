namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private static readonly bool noDateAxisFixInitialized = InitializeNoDateAxisFix();
        private static readonly HashSet<TradingChartControl> noDateAxisFixControls = new();
        private Label? noDateAxisLabel;

        private static bool InitializeNoDateAxisFix()
        {
            Application.Idle += AttachNoDateAxisFixes;
            return true;
        }

        private static void AttachNoDateAxisFixes(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
                AttachNoDateAxisFixes(form);
        }

        private static void AttachNoDateAxisFixes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TradingChartControl chart && noDateAxisFixControls.Add(chart))
                {
                    chart.noDateAxisLabel = new Label
                    {
                        AutoSize = false,
                        BackColor = chart.BackColor,
                        ForeColor = Color.FromArgb(255, 255, 255),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false,
                        TabStop = false
                    };
                    chart.noDateAxisLabel.MouseDown += (_, args) =>
                    {
                        if (args.Button == MouseButtons.Left && chart.ExtraDrawingActive)
                            chart.CancelExtraDrawing();
                    };
                    chart.Controls.Add(chart.noDateAxisLabel);
                    chart.noDateAxisLabel.BringToFront();
                    chart.MouseMove += chart.NoDateAxis_MouseMove;
                    chart.Resize += chart.NoDateAxis_Resize;
                }

                if (control.HasChildren)
                    AttachNoDateAxisFixes(control);
            }
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
            var text = $"کندل {firstIndex + crosshairIndex + 1:N0}";
            var width = Math.Max(64, TextRenderer.MeasureText(text, Font).Width + 8);
            var height = Math.Max(18, Font.Height + 4);
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