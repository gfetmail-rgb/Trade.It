namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private bool initialTopMarginApplied;

        private void EnsureInitialTopMargin()
        {
            var percent = Math.Clamp(ChartTopEmptyPercent, 0.0, 50.0) / 100.0;
            if (percent <= 0.0001)
                return;

            // SetData() and ResetView() both restore verticalZoom=1 and
            // verticalPanOffset=0. Those exact defaults are used as the signal
            // that a fresh/reset view needs its initial top margin applied.
            if (Math.Abs(verticalZoom - 1.0) > 0.0001 || Math.Abs(verticalPanOffset) > 0.0001)
            {
                initialTopMarginApplied = false;
                return;
            }

            if (initialTopMarginApplied || points.Count == 0)
                return;

            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(Math.Max(1, endIndex - firstIndex)).ToList();
            if (visible.Count == 0)
                return;

            var dataMin = visible.Min(x => x.Low);
            var dataMax = visible.Max(x => x.High);
            var range = Math.Max(dataMax - dataMin, Math.Max(Math.Abs(dataMax), 1.0) * 0.01);
            var factor = 1.0 - percent;
            if (factor <= 0.0)
                return;

            // GetVerticalRange divides range by verticalZoom and centers it at
            // (min+max)/2 + verticalPanOffset. These values make dataMin remain
            // at the bottom while leaving exactly 'percent' of the plot above
            // dataMax.
            verticalZoom = factor;
            verticalPanOffset = range * percent / (2.0 * factor);
            initialTopMarginApplied = true;
        }
    }
}