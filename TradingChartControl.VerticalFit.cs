namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private const int WmLButtonDblClk = 0x0203;
        private static bool verticalFitMessageFilterRegistered;
        private readonly bool verticalFitFilterInitialized = RegisterVerticalFitMessageFilter();

        private static bool RegisterVerticalFitMessageFilter()
        {
            if (!verticalFitMessageFilterRegistered)
            {
                Application.AddMessageFilter(new VerticalFitMessageFilter());
                verticalFitMessageFilterRegistered = true;
            }

            return true;
        }

        private void FitVerticalView()
        {
            if (points.Count == 0)
                return;

            ApplyConfiguredVerticalFit();
            Invalidate();
        }

        private void ApplyConfiguredVerticalFit()
        {
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
                verticalPanOffset = 0;
                return;
            }

            // range باید طوری بزرگ شود که data دقیقاً از پایین شروع شود
            // و درصد تنظیم‌شده از کل ارتفاع Plot در بالا خالی بماند.
            var factor = 1.0 - percent;
            verticalZoom = factor;
            verticalPanOffset = range * percent / (2.0 * factor);
        }

        private void FitVerticalRange()
        {
            ResetView();
        }

        private sealed class VerticalFitMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg != WmLButtonDblClk)
                    return false;

                if (Control.FromHandle(m.HWnd) is not TradingChartControl chart)
                    return false;

                var lParam = m.LParam.ToInt64();
                var x = (short)(lParam & 0xFFFF);
                var y = (short)((lParam >> 16) & 0xFFFF);

                if (x <= 55 && y >= 0 && y <= chart.Height - 35)
                {
                    chart.FitVerticalView();
                    return true;
                }

                return false;
            }
        }
    }
}
