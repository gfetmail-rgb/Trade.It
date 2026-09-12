namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        internal static double ChartRightEmptyPercent
        {
            get => ChartAppearanceSettings.ChartRightEmptyPercent;
            set => ChartAppearanceSettings.SetChartRightEmptyPercent(value);
        }

        internal static void LoadChartAppearanceSettings() => ChartAppearanceSettings.Load();
        internal static void SaveChartAppearanceSettings() => ChartAppearanceSettings.Save();
    }
}
