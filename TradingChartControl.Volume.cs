namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private bool IsVolumePanelSeparator(int y)
        {
            var plot = GetPlotRectangle();
            var separatorCenter = plot.Bottom + Math.Clamp(volumePanelGap, 2, 30) / 2;
            return Math.Abs(y - separatorCenter) <= 5;
        }

        public double VolumePanelRatio
        {
            get => volumePanelRatio;
            set
            {
                volumePanelRatio = Math.Clamp(value, 0.10, 0.45);
                Invalidate();
            }
        }

        public int VolumePanelGap
        {
            get => volumePanelGap;
            set
            {
                volumePanelGap = Math.Clamp(value, 2, 30);
                Invalidate();
            }
        }
    }
}