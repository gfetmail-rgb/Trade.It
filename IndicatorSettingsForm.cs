namespace Trade.It
{
    public partial class IndicatorSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public Color LineColor => lineColorButton.BackColor;
        public Color BackgroundColor => backgroundColorButton.BackColor;
        private readonly int defaultPeriod;
        private readonly ChartIndicatorType indicatorType;

        internal IndicatorSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            Text = indicator.Type == ChartIndicatorType.MovingAverage
                ? "تنظیمات MA"
                : indicator.Type == ChartIndicatorType.AverageTrueRange
                    ? "تنظیمات ATR"
                    : indicator.Type == ChartIndicatorType.DonchianChannel
                        ? "تنظیمات دونچیان"
                        : "تنظیمات EMA";

            indicatorType = indicator.Type;
            defaultPeriod = indicator.Type == ChartIndicatorType.AverageTrueRange ? 13 : 21;

            periodNumeric.Minimum = 2;
            periodNumeric.Maximum = Math.Max(2, maxPeriod);
            periodNumeric.Value = Math.Clamp(indicator.Period, 2, Math.Max(2, maxPeriod));

            SetColorButton(lineColorButton, indicator.LineColor);
            SetColorButton(backgroundColorButton, indicator.BackgroundColor);

            // رنگ زمینه فقط برای کانال دونچیان معنی دارد؛ برای اندیکاتورهای تک‌خطی نمایش داده نشود.
            if (indicator.Type != ChartIndicatorType.DonchianChannel)
            {
                backgroundColorLabel.Visible = false;
                backgroundColorButton.Visible = false;
            }

            lineColorButton.Click += (_, _) => PickColor(lineColorButton);
            if (indicator.Type == ChartIndicatorType.DonchianChannel)
                backgroundColorButton.Click += (_, _) => PickColor(backgroundColorButton);
            okButton.Click += (_, _) => DialogResult = DialogResult.OK;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
            defaultButton.Click += (_, _) => ApplyDefaults();
        }

        private void ApplyDefaults()
        {
            periodNumeric.Value = Math.Min(defaultPeriod, periodNumeric.Maximum);
            SetColorButton(lineColorButton, indicatorType == ChartIndicatorType.DonchianChannel ? Color.FromArgb(50, 160, 80) : Color.FromArgb(30, 100, 220));
            SetColorButton(backgroundColorButton, Color.White);
        }


        private static void SetColorButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
        }

        private static void PickColor(Button button)
        {
            using var dialog = new ColorDialog
            {
                FullOpen = true,
                Color = button.BackColor
            };

            if (dialog.ShowDialog() == DialogResult.OK)
                SetColorButton(button, dialog.Color);
        }
    }
}