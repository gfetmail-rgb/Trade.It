namespace Trade.It
{
    internal sealed partial class OnFuMoradiSettingsForm : Form
    {
        internal double TrendWeight => (double)trendWeightNumeric.Value;
        internal double MomentumWeight => (double)momentumWeightNumeric.Value;
        internal double MeanReversionWeight => (double)meanReversionWeightNumeric.Value;
        internal double VolatilityLimit => (double)volatilityLimitNumeric.Value;
        internal double WickPatternWeight => (double)wickPatternWeightNumeric.Value;
        internal int ForecastCandleCount => (int)forecastCandleCountNumeric.Value;

        internal OnFuMoradiSettingsForm(ChartIndicator indicator)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            trendWeightNumeric.Value = Clamp(indicator.OnFuMoradiAlpha, 0m, 2m);
            momentumWeightNumeric.Value = Clamp(indicator.OnFuMoradiBeta, 0m, 2m);
            meanReversionWeightNumeric.Value = Clamp(indicator.OnFuMoradiGamma, 0m, 2m);
            volatilityLimitNumeric.Value = Clamp(indicator.OnFuMoradiDelta, 0.05m, 2m);
            wickPatternWeightNumeric.Value = Clamp(indicator.OnFuMoradiEpsilon, 0m, 2m);
            forecastCandleCountNumeric.Value = Math.Clamp(indicator.Period, 1, 100);

            defaultButton.Click += (_, _) => ApplyDefaults();
            okButton.Click += (_, _) => DialogResult = DialogResult.OK;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private static decimal Clamp(double value, decimal minimum, decimal maximum)
        {
            if (!double.IsFinite(value))
                return minimum;
            return Math.Clamp((decimal)value, minimum, maximum);
        }

        private void ApplyDefaults()
        {
            trendWeightNumeric.Value = 0.55m;
            momentumWeightNumeric.Value = 0.25m;
            meanReversionWeightNumeric.Value = 0.20m;
            volatilityLimitNumeric.Value = 0.35m;
            wickPatternWeightNumeric.Value = 0.15m;
            forecastCandleCountNumeric.Value = 13m;
        }
    }
}