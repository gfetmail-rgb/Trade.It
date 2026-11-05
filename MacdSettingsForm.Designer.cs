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

            fastPeriodLabel.AutoSize = true; fastPeriodLabel.Text = "دوره سریع:"; fastPeriodLabel.Location = new Point(285, 26); fastPeriodLabel.Size = new Size(115, 25);
            fastPeriodNumeric.Location = new Point(140, 22); fastPeriodNumeric.Size = new Size(125, 33); fastPeriodNumeric.TextAlign = HorizontalAlignment.Center; slowPeriodLabel.AutoSize = true; slowPeriodLabel.Text = "دوره کند:"; slowPeriodLabel.Location = new Point(285, 66); slowPeriodLabel.Size = new Size(115, 25);
            slowPeriodNumeric.Location = new Point(140, 62); slowPeriodNumeric.Size = new Size(125, 33); slowPeriodNumeric.TextAlign = HorizontalAlignment.Center; signalPeriodLabel.AutoSize = true; signalPeriodLabel.Text = "دوره Signal:"; signalPeriodLabel.Location = new Point(285, 106); signalPeriodLabel.Size = new Size(115, 25);
            signalPeriodNumeric.Location = new Point(140, 102); signalPeriodNumeric.Size = new Size(125, 33); signalPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            macdLineCheckBox.AutoSize = true; macdLineCheckBox.Text = "خط MACD"; macdLineCheckBox.Location = new Point(285, 152); macdLineCheckBox.Size = new Size(120, 29);
            macdLineColorButton.Text = "انتخاب رنگ"; macdLineColorButton.Location = new Point(115, 148); macdLineColorButton.Size = new Size(125, 34);
            macdLineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            macdLineColorButton.ForeColor = Color.White; macdLineColorButton.UseVisualStyleBackColor = false; macdSignalCheckBox.AutoSize = true; macdSignalCheckBox.Text = "خط Signal"; macdSignalCheckBox.Location = new Point(285, 192); macdSignalCheckBox.Size = new Size(120, 29);
            macdSignalColorButton.Text = "انتخاب رنگ"; macdSignalColorButton.Location = new Point(115, 188); macdSignalColorButton.Size = new Size(125, 34);
            macdSignalColorButton.BackColor = Color.FromArgb(220, 80, 80);
            macdSignalColorButton.ForeColor = Color.White; macdSignalColorButton.UseVisualStyleBackColor = false;
            macdHistogramCheckBox.AutoSize=true; macdHistogramCheckBox.Text="هیستوگرام"; macdHistogramCheckBox.Location=new Point(285,228); macdHistogramCheckBox.Size=new Size(120,29);
            macdBullishHistogramColorButton.Text="رنگ صعودی"; macdBullishHistogramColorButton.Location=new Point(115,224); macdBullishHistogramColorButton.Size=new Size(125,34);
            macdBullishHistogramColorButton.BackColor = Color.FromArgb(80, 170, 100);
            macdBullishHistogramColorButton.ForeColor = Color.White;
            macdBearishHistogramColorButton.Text="رنگ نزولی"; macdBearishHistogramColorButton.Location=new Point(115,264); macdBearishHistogramColorButton.Size=new Size(125,34);
            macdBearishHistogramColorButton.BackColor = Color.FromArgb(210, 100, 100);
            macdBearishHistogramColorButton.ForeColor = Color.White;
            macdZeroCheckBox.AutoSize=true; macdZeroCheckBox.Text="خط صفر"; macdZeroCheckBox.Location=new Point(285,304); macdZeroCheckBox.Size=new Size(120,29);
            macdZeroColorButton.Text="انتخاب رنگ"; macdZeroColorButton.Location=new Point(115,300); macdZeroColorButton.Size=new Size(125,34);
            macdZeroColorButton.BackColor = Color.FromArgb(150, 150, 150);
            macdZeroColorButton.ForeColor = Color.Black; macdZeroColorButton.UseVisualStyleBackColor=false;
            okButton.Text="تأیید"; okButton.DialogResult=DialogResult.OK; okButton.Location=new Point(200,350); okButton.Size=new Size(100,34);
            cancelButton.Text="انصراف"; cancelButton.DialogResult=DialogResult.Cancel; cancelButton.Location=new Point(90,350); cancelButton.Size=new Size(100,34);
            AcceptButton=okButton; CancelButton=cancelButton;
            defaultButton.Name="defaultButton"; defaultButton.Text="پیش‌فرض"; defaultButton.Location=new Point(315,350); defaultButton.Size=new Size(100,34);
            Controls.AddRange(new Control[]{fastPeriodLabel,fastPeriodNumeric,slowPeriodLabel,slowPeriodNumeric,signalPeriodLabel,signalPeriodNumeric,macdLineCheckBox,macdLineColorButton,macdSignalCheckBox,macdSignalColorButton,macdHistogramCheckBox,macdBullishHistogramColorButton,macdBearishHistogramColorButton,macdZeroCheckBox,macdZeroColorButton,defaultButton,okButton,cancelButton});
            AutoScaleDimensions=new SizeF(7F,15F); AutoScaleMode=AutoScaleMode.Font; ClientSize=new Size(420,405);
            FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; MinimizeBox=false; Name="MacdSettingsForm"; StartPosition=FormStartPosition.CenterParent; Text="تنظیمات MACD"; RightToLeft=RightToLeft.Yes; RightToLeftLayout=true; ShowInTaskbar=false;
            ((System.ComponentModel.ISupportInitialize)fastPeriodNumeric).EndInit(); ((System.ComponentModel.ISupportInitialize)slowPeriodNumeric).EndInit(); ((System.ComponentModel.ISupportInitialize)signalPeriodNumeric).EndInit(); ResumeLayout(false); PerformLayout();
        }
    }
}