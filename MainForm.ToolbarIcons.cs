using System.Drawing.Drawing2D;

namespace Trade.It
{
    public partial class MainForm
    {
        private enum ToolbarIcon
        {
            Grid,
            Crosshair,
            ZoomIn,
            ZoomOut,
            Reset,
            HideChart,
            HideTools,
            Print,
            Snapshot,
            FullScreen,
            TrendLine,
            TrendChannel,
            HorizontalDouble,
            VerticalDouble,
            HorizontalRay,
            TrendLineArrow
        }

        private bool toolbarIconsInitialized;

        private void InitializeToolbarIcons()
        {
            if (toolbarIconsInitialized)
                return;

            toolbarIconsInitialized = true;

            ConfigureToolbarIcon(gridButton, ToolbarIcon.Grid);
            ConfigureToolbarIcon(crossButton, ToolbarIcon.Crosshair);
            ConfigureToolbarIcon(zoomInButton, ToolbarIcon.ZoomIn);
            ConfigureToolbarIcon(zoomOutButton, ToolbarIcon.ZoomOut);
            ConfigureToolbarIcon(resetChartButton, ToolbarIcon.Reset);
            ConfigureToolbarIcon(hideChartButton, ToolbarIcon.HideChart);
            ConfigureToolbarIcon(hideToolsButton, ToolbarIcon.HideTools);
            ConfigureToolbarIcon(printChartButton, ToolbarIcon.Print);
            ConfigureToolbarIcon(snapshotChartButton, ToolbarIcon.Snapshot);
            ConfigureToolbarIcon(fullScreenChartButton, ToolbarIcon.FullScreen);

            ConfigureToolbarIcon(drawTrendLineButton, ToolbarIcon.TrendLine);
            ConfigureToolbarIcon(drawTrendChannelButton, ToolbarIcon.TrendChannel);
            ConfigureToolbarIcon(drawHorizontalDoubleButton, ToolbarIcon.HorizontalDouble);
            ConfigureToolbarIcon(drawVerticalDoubleButton, ToolbarIcon.VerticalDouble);
            ConfigureToolbarIcon(drawHorizontalRayButton, ToolbarIcon.HorizontalRay);
            ConfigureToolbarIcon(drawTrendLineArrowButton, ToolbarIcon.TrendLineArrow);

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                ConfigureToolbarIcon(drawRectangleButton, ToolbarIcon.RectanglePlaceholder);
                ConfigureToolbarIcon(drawFibonacciButton, ToolbarIcon.FibonacciPlaceholder);
                ConfigureToolbarIcon(drawTextButton, ToolbarIcon.TextPlaceholder);
                ConfigureToolbarIcon(drawPitchforkButton, ToolbarIcon.PitchforkPlaceholder);
                ConfigureToolbarIcon(drawFibonacciExtensionButton, ToolbarIcon.FibonacciExtensionPlaceholder);
                ConfigureToolbarIcon(drawMeasureButton, ToolbarIcon.MeasurePlaceholder);
            }
        }

        private void ConfigureToolbarIcon(Button button, ToolbarIcon icon)
        {
            button.Text = string.Empty;
            button.Tag = icon;
            button.Paint += ToolbarIconButton_Paint;
        }

        private void ToolbarIconButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button button || button.Tag is not ToolbarIcon icon)
                return;

            var w = button.ClientSize.Width;
            var h = button.ClientSize.Height;
            var cx = w / 2f;
            var cy = h / 2f;
            var stroke = button.Enabled ? SystemColors.ControlText : SystemColors.GrayText;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(stroke, 2f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };
            using var thinPen = new Pen(stroke, 1.5f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            using var brush = new SolidBrush(stroke);

            switch (icon)
            {
                case ToolbarIcon.Grid:
                    DrawGridIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.Crosshair:
                    e.Graphics.DrawLine(pen, cx - 18, cy, cx + 18, cy);
                    e.Graphics.DrawLine(pen, cx, cy - 14, cx, cy + 14);
                    e.Graphics.DrawEllipse(pen, cx - 7, cy - 7, 14, 14);
                    break;
                case ToolbarIcon.ZoomIn:
                    DrawMagnifier(e.Graphics, pen, cx, cy, true);
                    break;
                case ToolbarIcon.ZoomOut:
                    DrawMagnifier(e.Graphics, pen, cx, cy, false);
                    break;
                case ToolbarIcon.Reset:
                    DrawResetIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.HideChart:
                    DrawEyeIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.HideTools:
                    DrawToolsIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.Print:
                    DrawPrintIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.Snapshot:
                    DrawCameraIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.FullScreen:
                    DrawFullScreenIcon(e.Graphics, pen, cx, cy);
                    break;
                case ToolbarIcon.TrendLine:
                    e.Graphics.DrawLine(pen, cx - 22, cy + 9, cx + 22, cy - 9);
                    e.Graphics.FillEllipse(brush, cx - 25, cy + 6, 6, 6);
                    e.Graphics.FillEllipse(brush, cx + 19, cy - 12, 6, 6);
                    break;
                case ToolbarIcon.TrendChannel:
                    e.Graphics.DrawLine(pen, cx - 22, cy + 9, cx + 22, cy - 9);
                    e.Graphics.DrawLine(pen, cx - 22, cy + 17, cx + 22, cy - 1);
                    e.Graphics.DrawLine(thinPen, cx - 22, cy + 9, cx - 22, cy + 17);
                    e.Graphics.DrawLine(thinPen, cx + 22, cy - 9, cx + 22, cy - 1);
                    break;
                case ToolbarIcon.HorizontalDouble:
                    e.Graphics.DrawLine(pen, cx - 22, cy, cx + 22, cy);
                    e.Graphics.DrawLine(pen, cx - 22, cy - 7, cx - 22, cy + 7);
                    e.Graphics.DrawLine(pen, cx + 22, cy - 7, cx + 22, cy + 7);
                    break;
                case ToolbarIcon.VerticalDouble:
                    e.Graphics.DrawLine(pen, cx, cy - 14, cx, cy + 14);
                    e.Graphics.DrawLine(pen, cx - 7, cy - 14, cx + 7, cy - 14);
                    e.Graphics.DrawLine(pen, cx - 7, cy + 14, cx + 7, cy + 14);
                    break;
                case ToolbarIcon.HorizontalRay:
                    e.Graphics.DrawLine(pen, cx - 20, cy, cx + 22, cy);
                    e.Graphics.DrawLine(pen, cx - 20, cy - 7, cx - 20, cy + 7);
                    break;
                case ToolbarIcon.TrendLineArrow:
                    e.Graphics.DrawLine(pen, cx - 22, cy + 9, cx + 17, cy - 7);
                    DrawArrowHead(e.Graphics, pen, cx + 17, cy - 7, -28f);
                    break;
                case ToolbarIcon.RectanglePlaceholder:
                    e.Graphics.DrawRectangle(pen, cx - 21, cy - 11, 42, 22);
                    break;
                case ToolbarIcon.FibonacciPlaceholder:
                    e.Graphics.DrawLine(pen, cx - 22, cy + 8, cx + 22, cy - 8);
                    e.Graphics.DrawLine(thinPen, cx - 18, cy - 7, cx + 22, cy - 7);
                    e.Graphics.DrawLine(thinPen, cx - 18, cy, cx + 22, cy);
                    e.Graphics.DrawLine(thinPen, cx - 18, cy + 7, cx + 22, cy + 7);
                    break;
                case ToolbarIcon.TextPlaceholder:
                    using (var font = new Font(button.Font.FontFamily, 15f, FontStyle.Bold))
                    {
                        const string text = "T";
                        var size = e.Graphics.MeasureString(text, font);
                        e.Graphics.DrawString(text, font, brush, cx - size.Width / 2f, cy - size.Height / 2f - 1);
                    }
                    break;
                case ToolbarIcon.PitchforkPlaceholder:
                    e.Graphics.DrawLine(pen, cx - 20, cy + 9, cx + 20, cy - 9);
                    e.Graphics.DrawLine(pen, cx - 12, cy + 5, cx + 18, cy + 12);
                    e.Graphics.DrawLine(pen, cx - 12, cy + 5, cx + 18, cy - 2);
                    e.Graphics.DrawLine(pen, cx - 20, cy + 9, cx - 12, cy + 5);
                    break;
                case ToolbarIcon.FibonacciExtensionPlaceholder:
                    e.Graphics.DrawLine(pen, cx - 20, cy + 9, cx - 4, cy - 8);
                    e.Graphics.DrawLine(pen, cx - 4, cy - 8, cx + 8, cy + 3);
                    e.Graphics.DrawLine(thinPen, cx + 8, cy - 8, cx + 21, cy - 8);
                    e.Graphics.DrawLine(thinPen, cx + 8, cy + 2, cx + 21, cy + 2);
                    e.Graphics.DrawLine(thinPen, cx + 8, cy + 10, cx + 21, cy + 10);
                    break;
                case ToolbarIcon.MeasurePlaceholder:
                    e.Graphics.DrawLine(pen, cx - 21, cy + 8, cx + 21, cy - 8);
                    e.Graphics.DrawLine(pen, cx - 18, cy + 3, cx - 24, cy + 13);
                    e.Graphics.DrawLine(pen, cx + 18, cy - 13, cx + 24, cy - 3);
                    break;
            }
        }

        private static void DrawGridIcon(Graphics g, Pen pen, float cx, float cy)
        {
            const float s = 18;
            g.DrawRectangle(pen, cx - s, cy - s, s * 2, s * 2);
            g.DrawLine(pen, cx, cy - s, cx, cy + s);
            g.DrawLine(pen, cx - s, cy, cx + s, cy);
        }

        private static void DrawMagnifier(Graphics g, Pen pen, float cx, float cy, bool plus)
        {
            g.DrawEllipse(pen, cx - 13, cy - 13, 22, 22);
            g.DrawLine(pen, cx + 5, cy + 5, cx + 17, cy + 17);
            g.DrawLine(pen, cx - 8, cy - 2, cx + 4, cy - 2);
            if (plus)
                g.DrawLine(pen, cx - 2, cy - 8, cx - 2, cy + 4);
        }

        private static void DrawResetIcon(Graphics g, Pen pen, float cx, float cy)
        {
            var rect = new RectangleF(cx - 15, cy - 15, 30, 30);
            g.DrawArc(pen, rect, 45, 285);
            var points = new[]
            {
                new PointF(cx + 14, cy - 9),
                new PointF(cx + 16, cy + 1),
                new PointF(cx + 7, cy - 2)
            };
            g.FillPolygon(pen.Brush, points);
        }

        private static void DrawEyeIcon(Graphics g, Pen pen, float cx, float cy)
        {
            var path = new GraphicsPath();
            path.AddBezier(cx - 22, cy, cx - 11, cy - 14, cx + 11, cy - 14, cx + 22, cy);
            path.AddBezier(cx + 22, cy, cx + 11, cy + 14, cx - 11, cy + 14, cx - 22, cy);
            g.DrawPath(pen, path);
            g.DrawEllipse(pen, cx - 6, cy - 6, 12, 12);
        }

        private static void DrawToolsIcon(Graphics g, Pen pen, float cx, float cy)
        {
            g.DrawRectangle(pen, cx - 16, cy - 13, 32, 26);
            g.DrawLine(pen, cx - 10, cy - 5, cx + 10, cy - 5);
            g.DrawLine(pen, cx - 10, cy + 3, cx + 7, cy + 3);
            g.DrawLine(pen, cx - 10, cy + 11, cx + 3, cy + 11);
        }

        private static void DrawPrintIcon(Graphics g, Pen pen, float cx, float cy)
        {
            g.DrawRectangle(pen, cx - 12, cy - 16, 24, 10);
            g.DrawRectangle(pen, cx - 17, cy - 6, 34, 18);
            g.DrawRectangle(pen, cx - 12, cy + 4, 24, 13);
            g.FillEllipse(pen.Brush, cx + 9, cy - 1, 3, 3);
        }

        private static void DrawCameraIcon(Graphics g, Pen pen, float cx, float cy)
        {
            g.DrawRectangle(pen, cx - 19, cy - 11, 38, 24);
            g.DrawLine(pen, cx - 10, cy - 11, cx - 6, cy - 16);
            g.DrawLine(pen, cx - 6, cy - 16, cx + 5, cy - 16);
            g.DrawLine(pen, cx + 5, cy - 16, cx + 9, cy - 11);
            g.DrawEllipse(pen, cx - 7, cy - 6, 14, 14);
        }

        private static void DrawFullScreenIcon(Graphics g, Pen pen, float cx, float cy)
        {
            const float d = 17;
            g.DrawLine(pen, cx - d, cy - d, cx - 6, cy - d);
            g.DrawLine(pen, cx - d, cy - d, cx - d, cy - 6);
            g.DrawLine(pen, cx + d, cy - d, cx + 6, cy - d);
            g.DrawLine(pen, cx + d, cy - d, cx + d, cy - 6);
            g.DrawLine(pen, cx - d, cy + d, cx - 6, cy + d);
            g.DrawLine(pen, cx - d, cy + d, cx - d, cy + 6);
            g.DrawLine(pen, cx + d, cy + d, cx + 6, cy + d);
            g.DrawLine(pen, cx + d, cy + d, cx + d, cy + 6);
        }

        private static void DrawArrowHead(Graphics g, Pen pen, float x, float y, float angleDegrees)
        {
            var radians = angleDegrees * Math.PI / 180.0;
            var a1 = radians + Math.PI * 0.82;
            var a2 = radians - Math.PI * 0.82;
            var p1 = new PointF(x + (float)(9 * Math.Cos(a1)), y + (float)(9 * Math.Sin(a1)));
            var p2 = new PointF(x + (float)(9 * Math.Cos(a2)), y + (float)(9 * Math.Sin(a2)));
            g.DrawLine(pen, x, y, p1.X, p1.Y);
            g.DrawLine(pen, x, y, p2.X, p2.Y);
        }
    }
}