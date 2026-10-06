namespace Trade.It
{
    public partial class IchimokuSettingsForm : Form
    {
        public int TenkanPeriod => (int)tenkanPeriodNumeric.Value;
        public int KijunPeriod => (int)kijunPeriodNumeric.Value;
        public int SpanBPeriod => (int)spanBPeriodNumeric.Value;
        public bool ShowTenkan => tenkanCheckBox.Checked;
        public bool ShowKijun => kijunCheckBox.Checked;
        public bool ShowSpanA => spanACheckBox.Checked;
        public bool ShowSpanB => spanBCheckBox.Checked;
        public bool ShowChikou => chikouCheckBox.Checked;
        public bool ShowBullishCloud => bullishCloudCheckBox.Checked;
        public bool ShowBearishCloud => bearishCloudCheckBox.Checked;

        public Color TenkanColor => tenkanColorButton.BackColor;
        public Color KijunColor => kijunColorButton.BackColor;
        public Color SpanAColor => spanAColorButton.BackColor;
        public Color SpanBColor => spanBColorButton.BackColor;
        public Color ChikouColor => chikouColorButton.BackColor;
        public Color BullishCloudColor => bullishCloudColorButton.BackColor;
        public Color BearishCloudColor => bearishCloudColorButton.BackColor;

        internal IchimokuSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "تنظیمات ایچیموکو";

            var max = Math.Max(2, maxPeriod);
            tenkanPeriodNumeric.Minimum = 2;
            tenkanPeriodNumeric.Maximum = max;
            kijunPeriodNumeric.Minimum = 2;
            kijunPeriodNumeric.Maximum = max;
            spanBPeriodNumeric.Minimum = 2;
            spanBPeriodNumeric.Maximum = max;

            tenkanPeriodNumeric.Value = Math.Clamp(indicator.IchimokuTenkanPeriod, 2, max);
            kijunPeriodNumeric.Value = Math.Clamp(indicator.IchimokuKijunPeriod, 2, max);
            spanBPeriodNumeric.Value = Math.Clamp(indicator.IchimokuSpanBPeriod, 2, max);

            tenkanCheckBox.Checked = indicator.ShowTenkan;
            kijunCheckBox.Checked = indicator.ShowKijun;
            spanACheckBox.Checked = indicator.ShowSpanA;
            spanBCheckBox.Checked = indicator.ShowSpanB;
            chikouCheckBox.Checked = indicator.ShowChikou;
            bullishCloudCheckBox.Checked = indicator.ShowBullishCloud;
            bearishCloudCheckBox.Checked = indicator.ShowBearishCloud;

            SetColorButton(tenkanColorButton, indicator.TenkanColor);
            SetColorButton(kijunColorButton, indicator.KijunColor);
            SetColorButton(spanAColorButton, indicator.SpanAColor);
            SetColorButton(spanBColorButton, indicator.SpanBColor);
            SetColorButton(chikouColorButton, indicator.ChikouColor);
            SetColorButton(bullishCloudColorButton, indicator.BullishCloudColor);
            SetColorButton(bearishCloudColorButton, indicator.BearishCloudColor);

            tenkanColorButton.Click += (_, _) => PickColor(tenkanColorButton);
            kijunColorButton.Click += (_, _) => PickColor(kijunColorButton);
            spanAColorButton.Click += (_, _) => PickColor(spanAColorButton);
            spanBColorButton.Click += (_, _) => PickColor(spanBColorButton);
            chikouColorButton.Click += (_, _) => PickColor(chikouColorButton);
            bullishCloudColorButton.Click += (_, _) => PickColor(bullishCloudColorButton);
            bearishCloudColorButton.Click += (_, _) => PickColor(bearishCloudColorButton);
        }

        private static void SetColorButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
        }

        private static void PickColor(Button button)
        {
            using var dialog = new ColorDialog { FullOpen = true, Color = button.BackColor };
            if (dialog.ShowDialog() == DialogResult.OK)
                SetColorButton(button, dialog.Color);
        }
    }
}