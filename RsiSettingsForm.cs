namespace Trade.It
{
    public partial class RsiSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public bool ShowRsiLine => rsiLineCheckBox.Checked;
        public bool ShowRsi30 => rsi30CheckBox.Checked;
        public bool ShowRsi70 => rsi70CheckBox.Checked;
        public Color RsiLineColor => rsiLineColorButton.BackColor;
        public Color Rsi30Color => rsi30ColorButton.BackColor;
        public Color Rsi70Color => rsi70ColorButton.BackColor;

        internal RsiSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            periodNumeric.Minimum = 2;
            periodNumeric.Maximum = Math.Max(2, maxPeriod);
            periodNumeric.Value = Math.Clamp(indicator.Period, 2, Math.Max(2, maxPeriod));

            rsiLineCheckBox.Checked = indicator.ShowRsiLine;
            rsi30CheckBox.Checked = indicator.ShowRsi30;
            rsi70CheckBox.Checked = indicator.ShowRsi70;

            SetColorButton(rsiLineColorButton, indicator.RsiLineColor);
            SetColorButton(rsi30ColorButton, indicator.Rsi30Color);
            SetColorButton(rsi70ColorButton, indicator.Rsi70Color);

            rsiLineColorButton.Click += (_, _) => PickColor(rsiLineColorButton);
            rsi30ColorButton.Click += (_, _) => PickColor(rsi30ColorButton);
            rsi70ColorButton.Click += (_, _) => PickColor(rsi70ColorButton);
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
