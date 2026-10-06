namespace Trade.It
{
    partial class MacdSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label fastPeriodLabel, slowPeriodLabel, signalPeriodLabel;
        private NumericUpDown fastPeriodNumeric, slowPeriodNumeric, signalPeriodNumeric;
        private CheckBox macdLineCheckBox, macdSignalCheckBox, macdHistogramCheckBox, macdZeroCheckBox;
        private Button macdLineColorButton, macdSignalColorButton, macdBullishHistogramColorButton, macdBearishHistogramColorButton, macdZeroColorButton, defaultButton, okButton, cancelButton;

        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            fastPeriodLabel=new Label(); slowPeriodLabel=new Label(); signalPeriodLabel=new Label();
            fastPeriodNumeric=new NumericUpDown(); slowPeriodNumeric=new NumericUpDown(); signalPeriodNumeric=new NumericUpDown();
            macdLineCheckBox=new CheckBox(); macdSignalCheckBox=new CheckBox(); macdHistogramCheckBox=new CheckBox(); macdZeroCheckBox=new CheckBox();
            macdLineColorButton=new Button(); macdSignalColorButton=new Button(); macdBullishHistogramColorButton=new Button(); macdBearishHistogramColorButton=new Button(); macdZeroColorButton=new Button();
            defaultButton=new Button(); okButton=new Button(); cancelButton=new Button();
            ((System.ComponentModel.ISupportInitialize)fastPeriodNumeric).BeginInit(); ((System.ComponentModel.ISupportInitialize)slowPeriodNumeric).BeginInit(); ((System.ComponentModel.ISupportInitialize)signalPeriodNumeric).BeginInit(); SuspendLayout();

            ConfigureNumber(fastPeriodLabel,fastPeriodNumeric,"دوره سریع:",22); ConfigureNumber(slowPeriodLabel,slowPeriodNumeric,"دوره کند:",62); ConfigureNumber(signalPeriodLabel,signalPeriodNumeric,"دوره Signal:",102);
            ConfigureRow(macdLineCheckBox,macdLineColorButton,"خط MACD",148); ConfigureRow(macdSignalCheckBox,macdSignalColorButton,"خط Signal",188);
            macdHistogramCheckBox.AutoSize=true; macdHistogramCheckBox.Text="هیستوگرام"; macdHistogramCheckBox.Location=new Point(285,228); macdHistogramCheckBox.Size=new Size(120,29);
            macdBullishHistogramColorButton.Text="رنگ صعودی"; macdBullishHistogramColorButton.Location=new Point(115,224); macdBullishHistogramColorButton.Size=new Size(125,34);
            macdBearishHistogramColorButton.Text="رنگ نزولی"; macdBearishHistogramColorButton.Location=new Point(115,264); macdBearishHistogramColorButton.Size=new Size(125,34);
            macdZeroCheckBox.AutoSize=true; macdZeroCheckBox.Text="خط صفر"; macdZeroCheckBox.Location=new Point(285,304); macdZeroCheckBox.Size=new Size(120,29);
            macdZeroColorButton.Text="انتخاب رنگ"; macdZeroColorButton.Location=new Point(115,300); macdZeroColorButton.Size=new Size(125,34); macdZeroColorButton.UseVisualStyleBackColor=false;
            okButton.Text="تأیید"; okButton.DialogResult=DialogResult.OK; okButton.Location=new Point(200,350); okButton.Size=new Size(100,34);
            cancelButton.Text="انصراف"; cancelButton.DialogResult=DialogResult.Cancel; cancelButton.Location=new Point(90,350); cancelButton.Size=new Size(100,34);
            AcceptButton=okButton; CancelButton=cancelButton;
            defaultButton.Name="defaultButton"; defaultButton.Text="پیش‌فرض"; defaultButton.Location=new Point(70,235); defaultButton.Size=new Size(100,36);
            Controls.AddRange(new Control[]{fastPeriodLabel,fastPeriodNumeric,slowPeriodLabel,slowPeriodNumeric,signalPeriodLabel,signalPeriodNumeric,macdLineCheckBox,macdLineColorButton,macdSignalCheckBox,macdSignalColorButton,macdHistogramCheckBox,macdBullishHistogramColorButton,macdBearishHistogramColorButton,macdZeroCheckBox,macdZeroColorButton,defaultButton,okButton,cancelButton});
            AutoScaleDimensions=new SizeF(7F,15F); AutoScaleMode=AutoScaleMode.Font; ClientSize=new Size(420,405);
            FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; MinimizeBox=false; Name="MacdSettingsForm"; StartPosition=FormStartPosition.CenterParent; Text="تنظیمات MACD"; RightToLeft=RightToLeft.Yes; RightToLeftLayout=true; ShowInTaskbar=false;
            ((System.ComponentModel.ISupportInitialize)fastPeriodNumeric).EndInit(); ((System.ComponentModel.ISupportInitialize)slowPeriodNumeric).EndInit(); ((System.ComponentModel.ISupportInitialize)signalPeriodNumeric).EndInit(); ResumeLayout(false); PerformLayout();
        }
        private static void ConfigureNumber(Label label, NumericUpDown numeric,string text,int y){label.AutoSize=true;label.Text=text;label.Location=new Point(285,y+4);label.Size=new Size(115,25);numeric.Location=new Point(140,y);numeric.Size=new Size(125,33);numeric.TextAlign=HorizontalAlignment.Center;}
        private static void ConfigureRow(CheckBox check,Button color,string text,int y){check.AutoSize=true;check.Text=text;check.Location=new Point(285,y+4);check.Size=new Size(120,29);color.Text="انتخاب رنگ";color.Location=new Point(115,y);color.Size=new Size(125,34);color.UseVisualStyleBackColor=false;}
    }
}