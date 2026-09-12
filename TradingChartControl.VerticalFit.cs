namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private const int WmLButtonDblClk = 0x0203;

        private void FitVerticalView()
        {
            if (points.Count == 0)
                return;

            verticalPanOffset = 0;
            verticalZoom = 1.10;
            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmLButtonDblClk)
            {
                var lParam = m.LParam.ToInt64();
                var x = (short)(lParam & 0xFFFF);
                var y = (short)((lParam >> 16) & 0xFFFF);

                if (x <= 55 && y >= 0 && y <= Height - 35)
                {
                    FitVerticalView();
                    return;
                }
            }

            base.WndProc(ref m);
        }
    }
}
