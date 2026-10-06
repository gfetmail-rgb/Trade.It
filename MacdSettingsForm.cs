namespace Trade.It
{
    public partial class MacdSettingsForm : Form
    {
        public bool ShowMacdLine => macdLineCheckBox.Checked;
        public bool ShowMacdSignal => macdSignalCheckBox.Checked;
        public bool ShowMacdHistogram => macdHistogramCheckBox.Checked;
        public bool ShowMacdZero => macdZeroCheckBox.Checked;
        public Color MacdLineColor => macdLineColorButton.BackColor;
        public Color MacdSignalColor => macdSignalColorButton.BackColor;
        public Color MacdBullishHistogramColor => macdBullishHistogramColorButton.BackColor;
        public Color MacdBearishHistogramColor => macdBearishHistogramColorButton.BackColor;
        public Color MacdZeroColor => macdZeroColorButton.BackColor;

        internal MacdSettingsForm(ChartIndicator indicator)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            macdLineCheckBox.Checked = indicator.ShowMacdLine;
            macdSignalCheckBox.Checked = indicator.ShowMacdSignal;
            macdHistogramCheckBox.Checked = indicator.ShowMacdHistogram;
            macdZeroCheckBox.Checked = indicator.ShowMacdZero;

            SetColorButton(macdLineColorButton, indicator.MacdLineColor);
            SetColorButton(macdSignalColorButton, indicator.MacdSignalColor);
            SetColorButton(macdBullishHistogramColorButton, indicator.MacdBullishHistogramColor);
            SetColorButton(macdBearishHistogramColorButton, indicator.MacdBearishHistogramColor);
            SetColorButton(macdZeroColorButton, indicator.MacdZeroColor);

            macdLineColorButton.Click += (_, _) => PickColor(macdLineColorButton);
            macdSignalColorButton.Click += (_, _) => PickColor(macdSignalColorButton);
            macdBullishHistogramColorButton.Click += (_, _) => PickColor(macdBullishHistogramColorButton);
            macdBearishHistogramColorButton.Click += (_, _) => PickColor(macdBearishHistogramColorButton);
            macdZeroColorButton.Click += (_, _) => PickColor(macdZeroColorButton);
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
