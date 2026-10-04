using System.Drawing.Drawing2D;
using System.Globalization;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            base.OnPaint(e);
            if (points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var naturalEndIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(Math.Max(1, naturalEndIndex - firstIndex)).ToList();
            if (visible.Count == 0)
                return;

            // Test Mode فقط تعداد کندل‌های نمایش‌داده‌شده را محدود می‌کند؛
            // تمام تبدیل‌های مختصات و مقیاس‌بندی دقیقاً همان حالت عادی است.
            var displayedCount = testMode && testEndIndex >= firstIndex
                ? Math.Clamp(testEndIndex - firstIndex + 1, 0, visible.Count)
                : visible.Count;
            if (displayedCount <= 0)
                return;

            var scaledVisible = displayedCount == visible.Count
                ? visible
                : visible.Take(displayedCount).ToList();
            GetVerticalRange(scaledVisible, out var min, out var max);

            using var gridPen = new Pen(LineAppearanceSettings.GridColor, LineAppearanceSettings.GridLineWidth) { DashStyle = LineAppearanceSettings.GridLineStyle };
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var linePen = new Pen(ChartAppearanceSettings.LineChartColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);
            using var priceAxisTextFont = new Font(Font.FontFamily, Math.Max(6.0f, Font.Size - 3.0f), Font.Style);
            using var headerFont = new Font(Font.FontFamily, Math.Max(8.0f, Font.Size), FontStyle.Bold);

            if (showGrid)
            {
                for (var i = 1; i <= 5; i++)
                {
                    var y = plot.Top + plot.Height * i / 6f;