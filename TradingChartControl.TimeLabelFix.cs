using System.Globalization;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private CrosshairTimeOverlay? crosshairTimeOverlay;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (crosshairTimeOverlay == null)
            {
                crosshairTimeOverlay = new CrosshairTimeOverlay();
                Controls.Add(crosshairTimeOverlay);
                crosshairTimeOverlay.Visible = false;
                crosshairTimeOverlay.BringToFront();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            UpdateCrosshairTimeOverlay();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            UpdateCrosshairTimeOverlay();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateCrosshairTimeOverlay();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (crosshairTimeOverlay != null)
                crosshairTimeOverlay.Visible = false;
        }

        private void UpdateCrosshairTimeOverlay()
        {
            if (crosshairTimeOverlay == null || !IsHandleCreated || IsDisposed)
                return;

            if (!showCrosshair || crosshairIndex < 0 || crosshairIndex >= visibleCount || points.Count == 0)
            {
                crosshairTimeOverlay.Visible = false;
                return;
            }

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var count = Math.Max(0, endIndex - firstIndex);
            if (crosshairIndex >= count)
            {
                crosshairTimeOverlay.Visible = false;
                return;
            }

            var step = plot.Width / (double)Math.Max(1, count);
            var initialOffset = -plot.Width * 0.25;
            var crosshairX = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);

            var date = points[firstIndex + crosshairIndex].Date;
            var calendar = new PersianCalendar();
            var timeText = string.Create(
                CultureInfo.InvariantCulture,
                $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}");

            crosshairTimeOverlay.TextValue = timeText;
            crosshairTimeOverlay.Width = 120;
            crosshairTimeOverlay.Height = 25;
            var x = (int)Math.Clamp(crosshairX - crosshairTimeOverlay.Width / 2f, plot.Left, Math.Max(plot.Left, Width - crosshairTimeOverlay.Width));
            crosshairTimeOverlay.Location = new Point(x, plot.Bottom + 2);
            crosshairTimeOverlay.Visible = true;
            crosshairTimeOverlay.BringToFront();
            crosshairTimeOverlay.Invalidate();
        }

        private sealed class CrosshairTimeOverlay : Control
        {
            public string TextValue { get; set; } = string.Empty;

            public CrosshairTimeOverlay()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
                BackColor = LineAppearanceSettings.CrosshairColor;
                ForeColor = Color.White;
                Enabled = false;
                TabStop = false;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                using var back = new SolidBrush(LineAppearanceSettings.CrosshairColor);
                using var text = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(back, ClientRectangle);
                TextRenderer.DrawText(
                    e.Graphics,
                    TextValue,
                    Font,
                    ClientRectangle,
                    text.Color,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            }
        }
    }
}
