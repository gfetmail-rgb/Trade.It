namespace Trade.It
{
    public partial class AdxSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public bool ShowAdxLine => adxLineCheckBox.Checked;
        public bool ShowAdxPlusDi => adxPlusDiCheckBox.Checked;
        public bool ShowAdxMinusDi => adxMinusDiCheckBox.Checked;
        public bool ShowAdx25 => adx25CheckBox.Checked;
        public Color AdxLineColor => adxLineColorButton.BackColor;
        public Color AdxPlusDiColor => adxPlusDiColorButton.BackColor;
        public Color AdxMinusDiColor => adxMinusDiColorButton.BackColor;
        public Color Adx25Color => adx25ColorButton.BackColor;

        internal AdxSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            periodNumeric.Minimum = 2;
            periodNumeric.Maximum = Math.Max(2, maxPeriod - 1);
            periodNumeric.Value = Math.Clamp(indicator.Period, 2, Math.Max(2, maxPeriod - 1));

            adxLineCheckBox.Checked = indicator.ShowAdxLine;
            adxPlusDiCheckBox.Checked = indicator.ShowAdxPlusDi;
            adxMinusDiCheckBox.Checked = indicator.ShowAdxMinusDi;
            adx25CheckBox.Checked = indicator.ShowAdx25;

            SetColor(adxLineColorButton, indicator.AdxLineColor);
            SetColor(adxPlusDiColorButton, indicator.AdxPlusDiColor);
            SetColor(adxMinusDiColorButton, indicator.AdxMinusDiColor);
            SetColor(adx25ColorButton, indicator.Adx25Color);

            adxLineColorButton.Click += (_, _) => PickColor(adxLineColorButton);
            adxPlusDiColorButton.Click += (_, _) => PickColor(adxPlusDiColorButton);
            adxMinusDiColorButton.Click += (_, _) => PickColor(adxMinusDiColorButton);
            adx25ColorButton.Click += (_, _) => PickColor(adx25ColorButton);
            defaultButton.Click += (_, _) => ApplyDefaults();
        }

        private void ApplyDefaults()
        {
            periodNumeric.Value = Math.Min(14, periodNumeric.Maximum);
            adxLineCheckBox.Checked = true;
            adxPlusDiCheckBox.Checked = true;
            adxMinusDiCheckBox.Checked = true;
            adx25CheckBox.Checked = true;
            SetColor(adxLineColorButton, Color.FromArgb(30, 100, 220));
            SetColor(adxPlusDiColorButton, Color.FromArgb(50, 160, 80));
            SetColor(adxMinusDiColorButton, Color.FromArgb(220, 80, 80));
            SetColor(adx25ColorButton, Color.FromArgb(150, 150, 150));
        }

        private static void SetColor(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
        }

        private static void PickColor(Button button)
        {
            using var dialog = new ColorDialog { FullOpen = true, Color = button.BackColor };
            if (dialog.ShowDialog() == DialogResult.OK)
                SetColor(button, dialog.Color);
        }
    }
}
