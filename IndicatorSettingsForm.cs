namespace Trade.It
{
    public partial class IndicatorSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public Color LineColor => lineColorButton.BackColor;
        public Color BackgroundColor => backgroundColorButton.BackColor;

        internal IndicatorSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            Text = indicator.Type == ChartIndicatorType.MovingAverage
                ? "تنظیمات MA"
                : "تنظیمات EMA";

            periodNumeric.Minimum = 2;
            periodNumeric.Maximum = Math.Max(2, maxPeriod);
            periodNumeric.Value = Math.Clamp(indicator.Period, 2, Math.Max(2, maxPeriod));

            SetColorButton(lineColorButton, indicator.LineColor);
            SetColorButton(backgroundColorButton, indicator.BackgroundColor);

            lineColorButton.Click += (_, _) => PickColor(lineColorButton);
            backgroundColorButton.Click += (_, _) => PickColor(backgroundColorButton);
            okButton.Click += (_, _) => DialogResult = DialogResult.OK;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
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