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

        private static Color GetDrawingColor(ChartDrawingTool tool) => tool switch
        {
            ChartDrawingTool.TrendLine => ChartAppearanceSettings.TrendLineColor,
            ChartDrawingTool.TrendChannel => ChartAppearanceSettings.TrendChannelColor,
            ChartDrawingTool.HorizontalDoubleArrow => ChartAppearanceSettings.HorizontalDoubleArrowColor,
            ChartDrawingTool.VerticalDoubleArrow => ChartAppearanceSettings.VerticalDoubleArrowColor,
            ChartDrawingTool.HorizontalRay => ChartAppearanceSettings.HorizontalRayColor,
            ChartDrawingTool.TrendLineWithArrow => ChartAppearanceSettings.TrendLineWithArrowColor,
            ChartDrawingTool.Rectangle => ChartAppearanceSettings.RectangleColor,
            _ => ChartAppearanceSettings.TrendLineColor
        };

        private static Color DarkenColor(Color color, float factor = 0.72f)
        {
            return Color.FromArgb(
                color.A,
                (int)(color.R * factor),
                (int)(color.G * factor),
                (int)(color.B * factor));
        }
    }
}
