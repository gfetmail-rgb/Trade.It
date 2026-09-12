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

            verticalPanOffset = 0;
            verticalZoom = 1.10;
            Invalidate();
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
