namespace Trade.It
{
    partial class StochasticSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label periodLabel,kPeriodLabel,dPeriodLabel;
        private NumericUpDown periodNumeric,kPeriodNumeric,dPeriodNumeric;
        private CheckBox showKCheckBox,showDCheckBox,show20CheckBox,show80CheckBox;
        private Button kColorButton,dColorButton,c20ColorButton,c80ColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            periodLabel=new Label();kPeriodLabel=new Label();dPeriodLabel=new Label();periodNumeric=new NumericUpDown();kPeriodNumeric=new NumericUpDown();dPeriodNumeric=new NumericUpDown();
            showKCheckBox=new CheckBox();showDCheckBox=new CheckBox();show20CheckBox=new CheckBox();show80CheckBox=new CheckBox();kColorButton=new Button();dColorButton=new Button();c20ColorButton=new Button();c80ColorButton=new Button();defaultButton=new Button();okButton=new Button();cancelButton=new Button();
            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).BeginInit();SuspendLayout();
            periodLabel.AutoSize = true; periodLabel.Text = "دوره %K:"; periodLabel.Location = new Point(285, 22); periodLabel.Size = new Size(115, 25);
            periodNumeric.Location = new Point(140, 18); periodNumeric.Size = new Size(125, 33); periodNumeric.TextAlign = HorizontalAlignment.Center;kPeriodLabel.AutoSize = true; kPeriodLabel.Text = "هموارسازی %K:"; kPeriodLabel.Location = new Point(285, 62); kPeriodLabel.Size = new Size(115, 25);
            kPeriodNumeric.Location = new Point(140, 58); kPeriodNumeric.Size = new Size(125, 33); kPeriodNumeric.TextAlign = HorizontalAlignment.Center;dPeriodLabel.AutoSize = true; dPeriodLabel.Text = "دوره %D:"; dPeriodLabel.Location = new Point(285, 102); dPeriodLabel.Size = new Size(115, 25);
            dPeriodNumeric.Location = new Point(140, 98); dPeriodNumeric.Size = new Size(125, 33); dPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            showKCheckBox.AutoSize = true; showKCheckBox.Text = "نمایش %K"; showKCheckBox.Location = new Point(285, 144); showKCheckBox.Size = new Size(120, 29);
            kColorButton.Text = "رنگ %K"; kColorButton.Location = new Point(115, 140); kColorButton.Size = new Size(125, 34);
            kColorButton.BackColor = Color.FromArgb(30, 100, 220);
            kColorButton.ForeColor = Color.White; kColorButton.UseVisualStyleBackColor = false;showDCheckBox.AutoSize = true; showDCheckBox.Text = "نمایش %D"; showDCheckBox.Location = new Point(285, 184); showDCheckBox.Size = new Size(120, 29);
            dColorButton.Text = "رنگ %D"; dColorButton.Location = new Point(115, 180); dColorButton.Size = new Size(125, 34);
            dColorButton.BackColor = Color.FromArgb(220, 80, 80);
            dColorButton.ForeColor = Color.White; dColorButton.UseVisualStyleBackColor = false;show20CheckBox.AutoSize = true; show20CheckBox.Text = "نمایش سطح 20"; show20CheckBox.Location = new Point(285, 224); show20CheckBox.Size = new Size(120, 29);
            c20ColorButton.Text = "رنگ سطح 20"; c20ColorButton.Location = new Point(115, 220); c20ColorButton.Size = new Size(125, 34);
            c20ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c20ColorButton.ForeColor = Color.Black; c20ColorButton.UseVisualStyleBackColor = false;show80CheckBox.AutoSize = true; show80CheckBox.Text = "نمایش سطح 80"; show80CheckBox.Location = new Point(285, 264); show80CheckBox.Size = new Size(120, 29);
            c80ColorButton.Text = "رنگ سطح 80"; c80ColorButton.Location = new Point(115, 260); c80ColorButton.Size = new Size(125, 34);
            c80ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c80ColorButton.ForeColor = Color.Black; c80ColorButton.UseVisualStyleBackColor = false;
            defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(90,310);defaultButton.Size=new Size(100,34);okButton.Text="تأیید";okButton.DialogResult=DialogResult.OK;okButton.Location=new Point(310,310);okButton.Size=new Size(100,34);cancelButton.Text="انصراف";cancelButton.DialogResult=DialogResult.Cancel;cancelButton.Location=new Point(200,310);cancelButton.Size=new Size(100,34);AcceptButton=okButton;CancelButton=cancelButton;
            defaultButton.Name="defaultButton";defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(70,300);defaultButton.Size=new Size(100,36);
            Controls.AddRange(new Control[]{periodLabel,periodNumeric,kPeriodLabel,kPeriodNumeric,dPeriodLabel,dPeriodNumeric,showKCheckBox,kColorButton,showDCheckBox,dColorButton,show20CheckBox,c20ColorButton,show80CheckBox,c80ColorButton,defaultButton,okButton,cancelButton});
            AutoScaleDimensions=new SizeF(7F,15F);AutoScaleMode=AutoScaleMode.Font;ClientSize=new Size(420,365);FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;Name="StochasticSettingsForm";StartPosition=FormStartPosition.CenterParent;Text="تنظیمات Stochastic";RightToLeft=RightToLeft.Yes;RightToLeftLayout=true;ShowInTaskbar=false;
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).EndInit();ResumeLayout(false);PerformLayout();
        }
    }
}