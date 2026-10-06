namespace Trade.It
{
    public partial class StochasticRsiSettingsForm : Form
    {
        public int RsiPeriod => (int)rsiPeriodNumeric.Value;
        public int StochasticPeriod => (int)stochasticPeriodNumeric.Value;
        public int KPeriod => (int)kPeriodNumeric.Value;
        public int DPeriod => (int)dPeriodNumeric.Value;
        public bool ShowK => showKCheckBox.Checked; public bool ShowD => showDCheckBox.Checked; public bool Show20 => show20CheckBox.Checked; public bool Show80 => show80CheckBox.Checked;
        public Color KColor => kColorButton.BackColor; public Color DColor => dColorButton.BackColor; public Color C20Color => c20ColorButton.BackColor; public Color C80Color => c80ColorButton.BackColor;

        internal StochasticRsiSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            var max=Math.Max(2,maxPeriod);
            rsiPeriodNumeric.Minimum=2;rsiPeriodNumeric.Maximum=Math.Max(2,max-1);stochasticPeriodNumeric.Minimum=2;stochasticPeriodNumeric.Maximum=max;kPeriodNumeric.Minimum=1;kPeriodNumeric.Maximum=max;dPeriodNumeric.Minimum=1;dPeriodNumeric.Maximum=max;
            rsiPeriodNumeric.Value=Math.Clamp(indicator.StochasticRsiRsiPeriod,2,Math.Max(2,max-1));stochasticPeriodNumeric.Value=Math.Clamp(indicator.StochasticRsiPeriod,2,max);kPeriodNumeric.Value=Math.Clamp(indicator.StochasticRsiKPeriod,1,max);dPeriodNumeric.Value=Math.Clamp(indicator.StochasticRsiDPeriod,1,max);
            showKCheckBox.Checked=indicator.ShowStochasticRsiK;showDCheckBox.Checked=indicator.ShowStochasticRsiD;show20CheckBox.Checked=indicator.ShowStochasticRsi20;show80CheckBox.Checked=indicator.ShowStochasticRsi80;
            SetColor(kColorButton,indicator.StochasticRsiKColor);SetColor(dColorButton,indicator.StochasticRsiDColor);SetColor(c20ColorButton,indicator.StochasticRsi20Color);SetColor(c80ColorButton,indicator.StochasticRsi80Color);
            kColorButton.Click+=(_,_)=>ChooseColor(kColorButton);dColorButton.Click+=(_,_)=>ChooseColor(dColorButton);c20ColorButton.Click+=(_,_)=>ChooseColor(c20ColorButton);c80ColorButton.Click+=(_,_)=>ChooseColor(c80ColorButton);
            defaultButton.Click+=(_,_)=>ApplyDefaults();
        }
        private void ApplyDefaults()
        {
            rsiPeriodNumeric.Value = Math.Min(14, rsiPeriodNumeric.Maximum);
            stochasticPeriodNumeric.Value = Math.Min(14, stochasticPeriodNumeric.Maximum);
            kPeriodNumeric.Value = Math.Min(3, kPeriodNumeric.Maximum);
            dPeriodNumeric.Value = Math.Min(3, dPeriodNumeric.Maximum);
            showKCheckBox.Checked = true;
            showDCheckBox.Checked = true;
            show20CheckBox.Checked = true;
            show80CheckBox.Checked = true;
            SetColor(kColorButton, Color.FromArgb(30, 100, 220));
            SetColor(dColorButton, Color.FromArgb(220, 80, 80));
            SetColor(c20ColorButton, Color.FromArgb(150, 150, 150));
            SetColor(c80ColorButton, Color.FromArgb(150, 150, 150));
        }

        private static void SetColor(Button b,Color c){b.BackColor=c;b.ForeColor=c.GetBrightness()<0.5f?Color.White:Color.Black;}
        private void ChooseColor(Button b){using var d=new ColorDialog{FullOpen=true,Color=b.BackColor};if(d.ShowDialog(this)==DialogResult.OK)SetColor(b,d.Color);}
    }
}