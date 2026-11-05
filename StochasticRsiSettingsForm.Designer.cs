namespace Trade.It
{
    partial class StochasticRsiSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label rsiPeriodLabel,stochasticPeriodLabel,kPeriodLabel,dPeriodLabel;
        private NumericUpDown rsiPeriodNumeric,stochasticPeriodNumeric,kPeriodNumeric,dPeriodNumeric;
        private CheckBox showKCheckBox,showDCheckBox,show20CheckBox,show80CheckBox;
        private Button kColorButton,dColorButton,c20ColorButton,c80ColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            rsiPeriodLabel=new Label();stochasticPeriodLabel=new Label();kPeriodLabel=new Label();dPeriodLabel=new Label();rsiPeriodNumeric=new NumericUpDown();stochasticPeriodNumeric=new NumericUpDown();kPeriodNumeric=new NumericUpDown();dPeriodNumeric=new NumericUpDown();
            showKCheckBox=new CheckBox();showDCheckBox=new CheckBox();show20CheckBox=new CheckBox();show80CheckBox=new CheckBox();kColorButton=new Button();dColorButton=new Button();c20ColorButton=new Button();c80ColorButton=new Button();defaultButton=new Button();okButton=new Button();cancelButton=new Button();
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).BeginInit();SuspendLayout();
            rsiPeriodLabel.AutoSize = true; rsiPeriodLabel.Text = "دوره RSI:"; rsiPeriodLabel.Location = new Point(285, 20); rsiPeriodLabel.Size = new Size(115, 25);
            rsiPeriodNumeric.Location = new Point(140, 16); rsiPeriodNumeric.Size = new Size(125, 33); rsiPeriodNumeric.TextAlign = HorizontalAlignment.Center;stochasticPeriodLabel.AutoSize = true; stochasticPeriodLabel.Text = "دوره Stochastic:"; stochasticPeriodLabel.Location = new Point(285, 60); stochasticPeriodLabel.Size = new Size(115, 25);
            stochasticPeriodNumeric.Location = new Point(140, 56); stochasticPeriodNumeric.Size = new Size(125, 33); stochasticPeriodNumeric.TextAlign = HorizontalAlignment.Center;kPeriodLabel.AutoSize = true; kPeriodLabel.Text = "هموارسازی %K:"; kPeriodLabel.Location = new Point(285, 100); kPeriodLabel.Size = new Size(115, 25);
            kPeriodNumeric.Location = new Point(140, 96); kPeriodNumeric.Size = new Size(125, 33); kPeriodNumeric.TextAlign = HorizontalAlignment.Center;dPeriodLabel.AutoSize = true; dPeriodLabel.Text = "دوره %D:"; dPeriodLabel.Location = new Point(285, 140); dPeriodLabel.Size = new Size(115, 25);
            dPeriodNumeric.Location = new Point(140, 136); dPeriodNumeric.Size = new Size(125, 33); dPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            showKCheckBox.AutoSize = true; showKCheckBox.Text = "نمایش %K"; showKCheckBox.Location = new Point(285, 182); showKCheckBox.Size = new Size(120, 29);
            kColorButton.Text = "رنگ %K"; kColorButton.Location = new Point(115, 178); kColorButton.Size = new Size(125, 34);
            kColorButton.BackColor = Color.FromArgb(30, 100, 220);
            kColorButton.ForeColor = Color.White; kColorButton.UseVisualStyleBackColor = false;showDCheckBox.AutoSize = true; showDCheckBox.Text = "نمایش %D"; showDCheckBox.Location = new Point(285, 222); showDCheckBox.Size = new Size(120, 29);
            dColorButton.Text = "رنگ %D"; dColorButton.Location = new Point(115, 218); dColorButton.Size = new Size(125, 34);
            dColorButton.BackColor = Color.FromArgb(220, 80, 80);
            dColorButton.ForeColor = Color.White; dColorButton.UseVisualStyleBackColor = false;show20CheckBox.AutoSize = true; show20CheckBox.Text = "نمایش سطح 20"; show20CheckBox.Location = new Point(285, 262); show20CheckBox.Size = new Size(120, 29);
            c20ColorButton.Text = "رنگ سطح 20"; c20ColorButton.Location = new Point(115, 258); c20ColorButton.Size = new Size(125, 34);
            c20ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c20ColorButton.ForeColor = Color.Black; c20ColorButton.UseVisualStyleBackColor = false;show80CheckBox.AutoSize = true; show80CheckBox.Text = "نمایش سطح 80"; show80CheckBox.Location = new Point(285, 302); show80CheckBox.Size = new Size(120, 29);
            c80ColorButton.Text = "رنگ سطح 80"; c80ColorButton.Location = new Point(115, 298); c80ColorButton.Size = new Size(125, 34);
            c80ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c80ColorButton.ForeColor = Color.Black; c80ColorButton.UseVisualStyleBackColor = false;
            defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(90,348);defaultButton.Size=new Size(100,34);okButton.Text="تأیید";okButton.DialogResult=DialogResult.OK;okButton.Location=new Point(310,348);okButton.Size=new Size(100,34);cancelButton.Text="انصراف";cancelButton.DialogResult=DialogResult.Cancel;cancelButton.Location=new Point(200,348);cancelButton.Size=new Size(100,34);AcceptButton=okButton;CancelButton=cancelButton;
            Controls.AddRange(new Control[]{rsiPeriodLabel,rsiPeriodNumeric,stochasticPeriodLabel,stochasticPeriodNumeric,kPeriodLabel,kPeriodNumeric,dPeriodLabel,dPeriodNumeric,showKCheckBox,kColorButton,showDCheckBox,dColorButton,show20CheckBox,c20ColorButton,show80CheckBox,c80ColorButton,defaultButton,okButton,cancelButton});
            AutoScaleDimensions=new SizeF(7F,15F);AutoScaleMode=AutoScaleMode.Font;ClientSize=new Size(420,400);FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;Name="StochasticRsiSettingsForm";StartPosition=FormStartPosition.CenterParent;Text="تنظیمات Stochastic RSI";RightToLeft=RightToLeft.Yes;RightToLeftLayout=true;ShowInTaskbar=false;
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).EndInit();ResumeLayout(false);PerformLayout();
        }
    }
}