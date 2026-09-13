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

            // SetData() and ResetView() both restore these exact defaults. Expanding
            // the vertical range while shifting its center upward leaves the data
            // anchored at the bottom and reserves only the requested space above it.
            // Any subsequent vertical zoom/pan changes the values and therefore uses
            // the reserved area normally.
            if (Math.Abs(verticalZoom - 1.0) > 0.0001 || Math.Abs(verticalPanOffset) > 0.0001)
            {
                initialTopMarginApplied = false;
                return;
            }

            if (initialTopMarginApplied)
                return;

            var factor = 1.0 - percent;
            if (factor <= 0.0)
                return;

            verticalZoom = factor;
            verticalPanOffset = percent / (2.0 * factor);
            initialTopMarginApplied = true;
        }
    }
}