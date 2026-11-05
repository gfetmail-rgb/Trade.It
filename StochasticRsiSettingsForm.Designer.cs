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
            rsiPeriodLabel = new Label();
            stochasticPeriodLabel = new Label();
            kPeriodLabel = new Label();
            dPeriodLabel = new Label();
            rsiPeriodNumeric = new NumericUpDown();
            stochasticPeriodNumeric = new NumericUpDown();
            kPeriodNumeric = new NumericUpDown();
            dPeriodNumeric = new NumericUpDown();
            showKCheckBox = new CheckBox();
            showDCheckBox = new CheckBox();
            show20CheckBox = new CheckBox();
            show80CheckBox = new CheckBox();
            kColorButton = new Button();
            dColorButton = new Button();
            c20ColorButton = new Button();
            c80ColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dPeriodNumeric).BeginInit();
            SuspendLayout();
            // 
            // rsiPeriodLabel
            // 
            rsiPeriodLabel.AutoSize = true;
            rsiPeriodLabel.Location = new Point(64, 20);
            rsiPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            rsiPeriodLabel.Name = "rsiPeriodLabel";
            rsiPeriodLabel.Size = new Size(80, 25);
            rsiPeriodLabel.TabIndex = 0;
            rsiPeriodLabel.Text = "دوره RSI:";
            // 
            // stochasticPeriodLabel
            // 
            stochasticPeriodLabel.AutoSize = true;
            stochasticPeriodLabel.Location = new Point(13, 61);
            stochasticPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            stochasticPeriodLabel.Name = "stochasticPeriodLabel";
            stochasticPeriodLabel.Size = new Size(133, 25);
            stochasticPeriodLabel.TabIndex = 2;
            stochasticPeriodLabel.Text = "دوره Stochastic:";
            // 
            // kPeriodLabel
            // 
            kPeriodLabel.AutoSize = true;
            kPeriodLabel.Location = new Point(17, 100);
            kPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            kPeriodLabel.Name = "kPeriodLabel";
            kPeriodLabel.Size = new Size(129, 25);
            kPeriodLabel.TabIndex = 4;
            kPeriodLabel.Text = "هموارسازی %K:";
            // 
            // dPeriodLabel
            // 
            dPeriodLabel.AutoSize = true;
            dPeriodLabel.Location = new Point(64, 147);
            dPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            dPeriodLabel.Name = "dPeriodLabel";
            dPeriodLabel.Size = new Size(82, 25);
            dPeriodLabel.TabIndex = 6;
            dPeriodLabel.Text = "دوره %D:";
            // 
            // rsiPeriodNumeric
            // 
            rsiPeriodNumeric.Location = new Point(154, 17);
            rsiPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            rsiPeriodNumeric.Name = "rsiPeriodNumeric";
            rsiPeriodNumeric.Size = new Size(114, 31);
            rsiPeriodNumeric.TabIndex = 1;
            rsiPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // stochasticPeriodNumeric
            // 
            stochasticPeriodNumeric.Location = new Point(154, 59);
            stochasticPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            stochasticPeriodNumeric.Name = "stochasticPeriodNumeric";
            stochasticPeriodNumeric.Size = new Size(114, 31);
            stochasticPeriodNumeric.TabIndex = 3;
            stochasticPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // kPeriodNumeric
            // 
            kPeriodNumeric.Location = new Point(154, 100);
            kPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            kPeriodNumeric.Name = "kPeriodNumeric";
            kPeriodNumeric.Size = new Size(114, 31);
            kPeriodNumeric.TabIndex = 5;
            kPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // dPeriodNumeric
            // 
            dPeriodNumeric.Location = new Point(154, 141);
            dPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            dPeriodNumeric.Name = "dPeriodNumeric";
            dPeriodNumeric.Size = new Size(114, 31);
            dPeriodNumeric.TabIndex = 7;
            dPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // showKCheckBox
            // 
            showKCheckBox.AutoSize = true;
            showKCheckBox.Location = new Point(193, 216);
            showKCheckBox.Margin = new Padding(4, 5, 4, 5);
            showKCheckBox.Name = "showKCheckBox";
            showKCheckBox.Size = new Size(117, 29);
            showKCheckBox.TabIndex = 8;
            showKCheckBox.Text = "نمایش %K";
            // 
            // showDCheckBox
            // 
            showDCheckBox.AutoSize = true;
            showDCheckBox.Location = new Point(193, 283);
            showDCheckBox.Margin = new Padding(4, 5, 4, 5);
            showDCheckBox.Name = "showDCheckBox";
            showDCheckBox.Size = new Size(120, 29);
            showDCheckBox.TabIndex = 10;
            showDCheckBox.Text = "نمایش %D";
            // 
            // show20CheckBox
            // 
            show20CheckBox.AutoSize = true;
            show20CheckBox.Location = new Point(193, 350);
            show20CheckBox.Margin = new Padding(4, 5, 4, 5);
            show20CheckBox.Name = "show20CheckBox";
            show20CheckBox.Size = new Size(153, 29);
            show20CheckBox.TabIndex = 12;
            show20CheckBox.Text = "نمایش سطح 20";
            // 
            // show80CheckBox
            // 
            show80CheckBox.AutoSize = true;
            show80CheckBox.Location = new Point(193, 416);
            show80CheckBox.Margin = new Padding(4, 5, 4, 5);
            show80CheckBox.Name = "show80CheckBox";
            show80CheckBox.Size = new Size(157, 29);
            show80CheckBox.TabIndex = 14;
            show80CheckBox.Text = "نمایش سطح 80";
            // 
            // kColorButton
            // 
            kColorButton.BackColor = Color.FromArgb(30, 100, 220);
            kColorButton.ForeColor = Color.White;
            kColorButton.Location = new Point(36, 202);
            kColorButton.Margin = new Padding(4, 5, 4, 5);
            kColorButton.Name = "kColorButton";
            kColorButton.Size = new Size(149, 57);
            kColorButton.TabIndex = 9;
            kColorButton.Text = "رنگ %K";
            kColorButton.UseVisualStyleBackColor = false;
            // 
            // dColorButton
            // 
            dColorButton.BackColor = Color.FromArgb(220, 80, 80);
            dColorButton.ForeColor = Color.White;
            dColorButton.Location = new Point(36, 268);
            dColorButton.Margin = new Padding(4, 5, 4, 5);
            dColorButton.Name = "dColorButton";
            dColorButton.Size = new Size(149, 57);
            dColorButton.TabIndex = 11;
            dColorButton.Text = "رنگ %D";
            dColorButton.UseVisualStyleBackColor = false;
            // 
            // c20ColorButton
            // 
            c20ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c20ColorButton.ForeColor = Color.Black;
            c20ColorButton.Location = new Point(36, 335);
            c20ColorButton.Margin = new Padding(4, 5, 4, 5);
            c20ColorButton.Name = "c20ColorButton";
            c20ColorButton.Size = new Size(149, 57);
            c20ColorButton.TabIndex = 13;
            c20ColorButton.Text = "رنگ سطح 20";
            c20ColorButton.UseVisualStyleBackColor = false;
            // 
            // c80ColorButton
            // 
            c80ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c80ColorButton.ForeColor = Color.Black;
            c80ColorButton.Location = new Point(36, 402);
            c80ColorButton.Margin = new Padding(4, 5, 4, 5);
            c80ColorButton.Name = "c80ColorButton";
            c80ColorButton.Size = new Size(149, 57);
            c80ColorButton.TabIndex = 15;
            c80ColorButton.Text = "رنگ سطح 80";
            c80ColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(23, 509);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(100, 45);
            defaultButton.TabIndex = 16;
            defaultButton.Text = "پیش‌فرض";
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(131, 509);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(100, 45);
            okButton.TabIndex = 17;
            okButton.Text = "تأیید";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(239, 509);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 45);
            cancelButton.TabIndex = 18;
            cancelButton.Text = "انصراف";
            // 
            // StochasticRsiSettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(368, 564);
            Controls.Add(rsiPeriodLabel);
            Controls.Add(rsiPeriodNumeric);
            Controls.Add(stochasticPeriodLabel);
            Controls.Add(stochasticPeriodNumeric);
            Controls.Add(kPeriodLabel);
            Controls.Add(kPeriodNumeric);
            Controls.Add(dPeriodLabel);
            Controls.Add(dPeriodNumeric);
            Controls.Add(showKCheckBox);
            Controls.Add(kColorButton);
            Controls.Add(showDCheckBox);
            Controls.Add(dColorButton);
            Controls.Add(show20CheckBox);
            Controls.Add(c20ColorButton);
            Controls.Add(show80CheckBox);
            Controls.Add(c80ColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StochasticRsiSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات Stochastic RSI";
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)kPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)dPeriodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}