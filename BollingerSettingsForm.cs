namespace Trade.It
{
    public partial class BollingerSettingsForm : Form
    {
        public int Period => (int)periodNumeric.Value;
        public double StandardDeviation => (double)stdNumeric.Value;
        public bool ShowMiddle => middleCheckBox.Checked;
        public bool ShowUpper => upperCheckBox.Checked;
        public bool ShowLower => lowerCheckBox.Checked;
        public Color MiddleColor => middleColorButton.BackColor;
        public Color UpperColor => upperColorButton.BackColor;
        public Color LowerColor => lowerColorButton.BackColor;

        internal BollingerSettingsForm(ChartIndicator indicator, int maxPeriod)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            var max=Math.Max(2,maxPeriod);
            periodNumeric.Minimum=2; periodNumeric.Maximum=max; periodNumeric.Value=Math.Clamp(indicator.Period,2,max);
            stdNumeric.Minimum=0.1m; stdNumeric.Maximum=10m; stdNumeric.DecimalPlaces=1; stdNumeric.Increment=0.1m;
            stdNumeric.Value=(decimal)Math.Clamp(indicator.BollingerStdDev,0.1,10);
            middleCheckBox.Checked=indicator.ShowBollingerMiddle; upperCheckBox.Checked=indicator.ShowBollingerUpper; lowerCheckBox.Checked=indicator.ShowBollingerLower;
            SetColor(middleColorButton,indicator.BollingerMiddleColor); SetColor(upperColorButton,indicator.BollingerUpperColor); SetColor(lowerColorButton,indicator.BollingerLowerColor);
            middleColorButton.Click+=(_,_)=>Pick(middleColorButton); upperColorButton.Click+=(_,_)=>Pick(upperColorButton); lowerColorButton.Click+=(_,_)=>Pick(lowerColorButton);
            defaultButton.Click+=(_,_)=>ApplyDefaults(); okButton.Click+=(_,_)=>DialogResult=DialogResult.OK; cancelButton.Click+=(_,_)=>DialogResult=DialogResult.Cancel;
        }
        private void ApplyDefaults(){periodNumeric.Value=Math.Min(20m,periodNumeric.Maximum);stdNumeric.Value=2;middleCheckBox.Checked=upperCheckBox.Checked=lowerCheckBox.Checked=true;SetColor(middleColorButton,Color.FromArgb(30,100,220));SetColor(upperColorButton,Color.FromArgb(50,160,80));SetColor(lowerColorButton,Color.FromArgb(220,80,80));}
        private static void SetColor(Button b,Color c){b.BackColor=c;b.ForeColor=Color.White;}
        private static void Pick(Button b){using var d=new ColorDialog{Color=b.BackColor};if(d.ShowDialog()==DialogResult.OK)SetColor(b,d.Color);}
    }
}