namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private void FitVerticalView()
        {
            if (points.Count == 0)
                return;

            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points
                .Skip(firstIndex)
                .Take(Math.Max(1, endIndex - firstIndex))
                .ToList();

            if (visible.Count == 0)
                return;

            var dataMin = visible.Min(x => x.Low);
            var dataMax = visible.Max(x => x.High);
            var range = Math.Max(
                dataMax - dataMin,
                Math.Max(Math.Abs(dataMax), 1.0) * 0.01);

            var percent = Math.Clamp(ChartTopEmptyPercent, 0.0, 50.0) / 100.0;

            if (percent <= 0.0001)
            {
                verticalZoom = 1.0;
                verticalPanOffset = 0.0;
            }
            else
            {
                // بعد از Fit:
                // - Low دقیقاً روی پایین Plot قرار می‌گیرد.
                // - High دقیقاً روی مرز فضای خالی بالای Plot قرار می‌گیرد.
                // verticalZoom در GetVerticalRange به صورت range / zoom
                // استفاده می‌شود، بنابراین برای اشغال (1-percent) از ارتفاع
                // باید zoom برابر همان ضریب باشد.
                var factor = 1.0 - percent;
                verticalZoom = factor;
                verticalPanOffset = range * percent / (2.0 * factor);
            }

            Invalidate();
        }

        private void FitVerticalRange()
        {
            FitVerticalView();
        }
    }
}
