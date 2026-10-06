namespace Trade.It
{
    public partial class ObvSettingsForm : Form
    {
        public bool ShowLine=>showLineCheckBox.Checked; public bool ShowZero=>showZeroCheckBox.Checked;
        public Color LineColor=>lineColorButton.BackColor; public Color ZeroColor=>zeroColorButton.BackColor;
        internal ObvSettingsForm(ChartIndicator indicator,int maxPeriod)
        {
            InitializeComponent();RightToLeft=RightToLeft.Yes;RightToLeftLayout=true;
            showLineCheckBox.Checked=indicator.ShowObvLine;showZeroCheckBox.Checked=indicator.ShowObvZero;SetColor(lineColorButton,indicator.ObvLineColor);SetColor(zeroColorButton,indicator.ObvZeroColor);
            lineColorButton.Click+=(_,_)=>Pick(lineColorButton);zeroColorButton.Click+=(_,_)=>Pick(zeroColorButton);defaultButton.Click+=(_,_)=>ApplyDefaults();okButton.Click+=(_,_)=>DialogResult=DialogResult.OK;cancelButton.Click+=(_,_)=>DialogResult=DialogResult.Cancel;
        }
        private void ApplyDefaults(){showLineCheckBox.Checked=true;showZeroCheckBox.Checked=true;SetColor(lineColorButton,Color.FromArgb(30,100,220));SetColor(zeroColorButton,Color.FromArgb(150,150,150));}
        private static void SetColor(Button b,Color c){b.BackColor=c;b.ForeColor=Color.White;} private static void Pick(Button b){using var d=new ColorDialog{Color=b.BackColor};if(d.ShowDialog()==DialogResult.OK)SetColor(b,d.Color);}
    }
}