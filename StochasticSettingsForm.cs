namespace Trade.It
{
    public partial class StochasticSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public int KPeriod => (int)kPeriodNumeric.Value;
        public int DPeriod => (int)dPeriodNumeric.Value;
        public bool ShowK => showKCheckBox.Checked; public bool ShowD => showDCheckBox.Checked; public bool Show20 => show20CheckBox.Checked; public bool Show80 => show80CheckBox.Checked;
        public Color KColor => kColorButton.BackColor; public Color DColor => dColorButton.BackColor; public Color C20Color => c20ColorButton.BackColor; public Color C80Color => c80ColorButton.BackColor;

        internal StochasticSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            var max=Math.Max(2,maxPeriod);
            periodNumeric.Minimum=2; periodNumeric.Maximum=max; kPeriodNumeric.Minimum=1; kPeriodNumeric.Maximum=max; dPeriodNumeric.Minimum=1; dPeriodNumeric.Maximum=max;
            periodNumeric.Value=Math.Clamp(indicator.StochasticPeriod,2,max); kPeriodNumeric.Value=Math.Clamp(indicator.StochasticKPeriod,1,max); dPeriodNumeric.Value=Math.Clamp(indicator.StochasticDPeriod,1,max);
            showKCheckBox.Checked=indicator.ShowStochasticK; showDCheckBox.Checked=indicator.ShowStochasticD; show20CheckBox.Checked=indicator.ShowStochastic20; show80CheckBox.Checked=indicator.ShowStochastic80;
            SetColor(kColorButton,indicator.StochasticKColor); SetColor(dColorButton,indicator.StochasticDColor); SetColor(c20ColorButton,indicator.Stochastic20Color); SetColor(c80ColorButton,indicator.Stochastic80Color);
            kColorButton.Click+=(_,_)=>ChooseColor(kColorButton); dColorButton.Click+=(_,_)=>ChooseColor(dColorButton); c20ColorButton.Click+=(_,_)=>ChooseColor(c20ColorButton); c80ColorButton.Click+=(_,_)=>ChooseColor(c80ColorButton);
            defaultButton.Click+=(_,_)=>ApplyDefaults();
        }
        private void ApplyDefaults()
        {
            periodNumeric.Value = Math.Min(14, periodNumeric.Maximum);
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