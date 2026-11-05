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
            fastPeriodLabel = new Label();
            slowPeriodLabel = new Label();
            signalPeriodLabel = new Label();
            fastPeriodNumeric = new NumericUpDown();
            slowPeriodNumeric = new NumericUpDown();
            signalPeriodNumeric = new NumericUpDown();
            macdLineCheckBox = new CheckBox();
            macdSignalCheckBox = new CheckBox();
            macdHistogramCheckBox = new CheckBox();
            macdZeroCheckBox = new CheckBox();
            macdLineColorButton = new Button();
            macdSignalColorButton = new Button();
            macdBullishHistogramColorButton = new Button();
            macdBearishHistogramColorButton = new Button();
            macdZeroColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)fastPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)slowPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)signalPeriodNumeric).BeginInit();
            SuspendLayout();
            // 
            // fastPeriodLabel
            // 
            fastPeriodLabel.AutoSize = true;
            fastPeriodLabel.Location = new Point(13, 22);
            fastPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            fastPeriodLabel.Name = "fastPeriodLabel";
            fastPeriodLabel.Size = new Size(95, 25);
            fastPeriodLabel.TabIndex = 0;
            fastPeriodLabel.Text = "دوره سریع:";
            // 
            // slowPeriodLabel
            // 
            slowPeriodLabel.AutoSize = true;
            slowPeriodLabel.Location = new Point(13, 89);
            slowPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            slowPeriodLabel.Name = "slowPeriodLabel";
            slowPeriodLabel.Size = new Size(78, 25);
            slowPeriodLabel.TabIndex = 2;
            slowPeriodLabel.Text = "دوره کند:";
            // 
            // signalPeriodLabel
            // 
            signalPeriodLabel.AutoSize = true;
            signalPeriodLabel.Location = new Point(13, 156);
            signalPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            signalPeriodLabel.Name = "signalPeriodLabel";
            signalPeriodLabel.Size = new Size(102, 25);
            signalPeriodLabel.TabIndex = 4;
            signalPeriodLabel.Text = "دوره Signal:";
            // 
            // fastPeriodNumeric
            // 
            fastPeriodNumeric.Location = new Point(123, 21);
            fastPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            fastPeriodNumeric.Name = "fastPeriodNumeric";
            fastPeriodNumeric.Size = new Size(100, 31);
            fastPeriodNumeric.TabIndex = 1;
            fastPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // slowPeriodNumeric
            // 
            slowPeriodNumeric.Location = new Point(123, 87);
            slowPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            slowPeriodNumeric.Name = "slowPeriodNumeric";
            slowPeriodNumeric.Size = new Size(100, 31);
            slowPeriodNumeric.TabIndex = 3;
            slowPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // signalPeriodNumeric
            // 
            signalPeriodNumeric.Location = new Point(123, 154);
            signalPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            signalPeriodNumeric.Name = "signalPeriodNumeric";
            signalPeriodNumeric.Size = new Size(100, 31);
            signalPeriodNumeric.TabIndex = 5;
            signalPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // macdLineCheckBox
            // 
            macdLineCheckBox.AutoSize = true;
            macdLineCheckBox.Location = new Point(172, 227);
            macdLineCheckBox.Margin = new Padding(4, 5, 4, 5);
            macdLineCheckBox.Name = "macdLineCheckBox";
            macdLineCheckBox.Size = new Size(121, 29);
            macdLineCheckBox.TabIndex = 6;
            macdLineCheckBox.Text = "خط MACD";
            // 
            // macdSignalCheckBox
            // 
            macdSignalCheckBox.AutoSize = true;
            macdSignalCheckBox.Location = new Point(172, 294);
            macdSignalCheckBox.Margin = new Padding(4, 5, 4, 5);
            macdSignalCheckBox.Name = "macdSignalCheckBox";
            macdSignalCheckBox.Size = new Size(117, 29);
            macdSignalCheckBox.TabIndex = 8;
            macdSignalCheckBox.Text = "خط Signal";
            // 
            // macdHistogramCheckBox
            // 
            macdHistogramCheckBox.AutoSize = true;
            macdHistogramCheckBox.Location = new Point(172, 392);
            macdHistogramCheckBox.Margin = new Padding(4, 5, 4, 5);
            macdHistogramCheckBox.Name = "macdHistogramCheckBox";
            macdHistogramCheckBox.Size = new Size(117, 29);
            macdHistogramCheckBox.TabIndex = 10;
            macdHistogramCheckBox.Text = "هیستوگرام";
            // 
            // macdZeroCheckBox
            // 
            macdZeroCheckBox.AutoSize = true;
            macdZeroCheckBox.Location = new Point(172, 481);
            macdZeroCheckBox.Margin = new Padding(4, 5, 4, 5);
            macdZeroCheckBox.Name = "macdZeroCheckBox";
            macdZeroCheckBox.Size = new Size(104, 29);
            macdZeroCheckBox.TabIndex = 13;
            macdZeroCheckBox.Text = "خط صفر";
            // 
            // macdLineColorButton
            // 
            macdLineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            macdLineColorButton.ForeColor = Color.White;
            macdLineColorButton.Location = new Point(13, 213);
            macdLineColorButton.Margin = new Padding(4, 5, 4, 5);
            macdLineColorButton.Name = "macdLineColorButton";
            macdLineColorButton.Size = new Size(136, 57);
            macdLineColorButton.TabIndex = 7;
            macdLineColorButton.Text = "انتخاب رنگ";
            macdLineColorButton.UseVisualStyleBackColor = false;
            // 
            // macdSignalColorButton
            // 
            macdSignalColorButton.BackColor = Color.FromArgb(220, 80, 80);
            macdSignalColorButton.ForeColor = Color.White;
            macdSignalColorButton.Location = new Point(13, 279);
            macdSignalColorButton.Margin = new Padding(4, 5, 4, 5);
            macdSignalColorButton.Name = "macdSignalColorButton";
            macdSignalColorButton.Size = new Size(136, 57);
            macdSignalColorButton.TabIndex = 9;
            macdSignalColorButton.Text = "انتخاب رنگ";
            macdSignalColorButton.UseVisualStyleBackColor = false;
            // 
            // macdBullishHistogramColorButton
            // 
            macdBullishHistogramColorButton.BackColor = Color.FromArgb(80, 170, 100);
            macdBullishHistogramColorButton.ForeColor = Color.White;
            macdBullishHistogramColorButton.Location = new Point(13, 339);
            macdBullishHistogramColorButton.Margin = new Padding(4, 5, 4, 5);
            macdBullishHistogramColorButton.Name = "macdBullishHistogramColorButton";
            macdBullishHistogramColorButton.Size = new Size(136, 57);
            macdBullishHistogramColorButton.TabIndex = 11;
            macdBullishHistogramColorButton.Text = "رنگ صعودی";
            macdBullishHistogramColorButton.UseVisualStyleBackColor = false;
            // 
            // macdBearishHistogramColorButton
            // 
            macdBearishHistogramColorButton.BackColor = Color.FromArgb(210, 100, 100);
            macdBearishHistogramColorButton.ForeColor = Color.White;
            macdBearishHistogramColorButton.Location = new Point(13, 406);
            macdBearishHistogramColorButton.Margin = new Padding(4, 5, 4, 5);
            macdBearishHistogramColorButton.Name = "macdBearishHistogramColorButton";
            macdBearishHistogramColorButton.Size = new Size(136, 57);
            macdBearishHistogramColorButton.TabIndex = 12;
            macdBearishHistogramColorButton.Text = "رنگ نزولی";
            macdBearishHistogramColorButton.UseVisualStyleBackColor = false;
            // 
            // macdZeroColorButton
            // 
            macdZeroColorButton.BackColor = Color.FromArgb(150, 150, 150);
            macdZeroColorButton.ForeColor = Color.Black;
            macdZeroColorButton.Location = new Point(13, 466);
            macdZeroColorButton.Margin = new Padding(4, 5, 4, 5);
            macdZeroColorButton.Name = "macdZeroColorButton";
            macdZeroColorButton.Size = new Size(136, 57);
            macdZeroColorButton.TabIndex = 14;
            macdZeroColorButton.Text = "انتخاب رنگ";
            macdZeroColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(17, 583);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(98, 43);
            defaultButton.TabIndex = 15;
            defaultButton.Text = "پیش‌فرض";
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(123, 583);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(98, 43);
            okButton.TabIndex = 16;
            okButton.Text = "تأیید";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(224, 583);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(98, 43);
            cancelButton.TabIndex = 17;
            cancelButton.Text = "انصراف";
            // 
            // MacdSettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(339, 635);
            Controls.Add(fastPeriodLabel);
            Controls.Add(fastPeriodNumeric);
            Controls.Add(slowPeriodLabel);
            Controls.Add(slowPeriodNumeric);
            Controls.Add(signalPeriodLabel);
            Controls.Add(signalPeriodNumeric);
            Controls.Add(macdLineCheckBox);
            Controls.Add(macdLineColorButton);
            Controls.Add(macdSignalCheckBox);
            Controls.Add(macdSignalColorButton);
            Controls.Add(macdHistogramCheckBox);
            Controls.Add(macdBullishHistogramColorButton);
            Controls.Add(macdBearishHistogramColorButton);
            Controls.Add(macdZeroCheckBox);
            Controls.Add(macdZeroColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MacdSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات MACD";
            ((System.ComponentModel.ISupportInitialize)fastPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)slowPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)signalPeriodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}