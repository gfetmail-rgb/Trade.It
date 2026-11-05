namespace Trade.It
{
    partial class IchimokuSettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label tenkanPeriodLabel;
        private Label kijunPeriodLabel;
        private Label spanBPeriodLabel;
        private Label displacementLabel;

        private NumericUpDown tenkanPeriodNumeric;
        private NumericUpDown kijunPeriodNumeric;
        private NumericUpDown spanBPeriodNumeric;
        private NumericUpDown displacementNumeric;

        private CheckBox tenkanCheckBox;
        private CheckBox kijunCheckBox;
        private CheckBox spanACheckBox;
        private CheckBox spanBCheckBox;
        private CheckBox chikouCheckBox;
        private CheckBox bullishCloudCheckBox;
        private CheckBox bearishCloudCheckBox;

        private Button tenkanColorButton;
        private Button kijunColorButton;
        private Button spanAColorButton;
        private Button spanBColorButton;
        private Button chikouColorButton;
        private Button bullishCloudColorButton;
        private Button bearishCloudColorButton;
        private Button defaultButton;
        private Button okButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tenkanPeriodLabel = new Label();
            kijunPeriodLabel = new Label();
            spanBPeriodLabel = new Label();
            displacementLabel = new Label();
            tenkanPeriodNumeric = new NumericUpDown();
            kijunPeriodNumeric = new NumericUpDown();
            spanBPeriodNumeric = new NumericUpDown();
            displacementNumeric = new NumericUpDown();
            tenkanCheckBox = new CheckBox();
            kijunCheckBox = new CheckBox();
            spanACheckBox = new CheckBox();
            spanBCheckBox = new CheckBox();
            chikouCheckBox = new CheckBox();
            bullishCloudCheckBox = new CheckBox();
            bearishCloudCheckBox = new CheckBox();
            tenkanColorButton = new Button();
            kijunColorButton = new Button();
            spanAColorButton = new Button();
            spanBColorButton = new Button();
            chikouColorButton = new Button();
            bullishCloudColorButton = new Button();
            bearishCloudColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)tenkanPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kijunPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spanBPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)displacementNumeric).BeginInit();
            SuspendLayout();
            // 
            // tenkanPeriodLabel
            // 
            tenkanPeriodLabel.AutoSize = true;
            tenkanPeriodLabel.Location = new Point(39, 20);
            tenkanPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            tenkanPeriodLabel.Name = "tenkanPeriodLabel";
            tenkanPeriodLabel.Size = new Size(108, 25);
            tenkanPeriodLabel.TabIndex = 0;
            tenkanPeriodLabel.Text = "دوره Tenkan:";
            // 
            // kijunPeriodLabel
            // 
            kijunPeriodLabel.AutoSize = true;
            kijunPeriodLabel.Location = new Point(39, 86);
            kijunPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            kijunPeriodLabel.Name = "kijunPeriodLabel";
            kijunPeriodLabel.Size = new Size(92, 25);
            kijunPeriodLabel.TabIndex = 2;
            kijunPeriodLabel.Text = "دوره Kijun:";
            // 
            // spanBPeriodLabel
            // 
            spanBPeriodLabel.AutoSize = true;
            spanBPeriodLabel.Location = new Point(39, 153);
            spanBPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            spanBPeriodLabel.Name = "spanBPeriodLabel";
            spanBPeriodLabel.Size = new Size(109, 25);
            spanBPeriodLabel.TabIndex = 4;
            spanBPeriodLabel.Text = "دوره Span B:";
            // 
            // displacementLabel
            // 
            displacementLabel.AutoSize = true;
            displacementLabel.Location = new Point(39, 220);
            displacementLabel.Margin = new Padding(4, 0, 4, 0);
            displacementLabel.Name = "displacementLabel";
            displacementLabel.Size = new Size(75, 25);
            displacementLabel.TabIndex = 6;
            displacementLabel.Text = "جابجایی:";
            // 
            // tenkanPeriodNumeric
            // 
            tenkanPeriodNumeric.Location = new Point(164, 20);
            tenkanPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            tenkanPeriodNumeric.Name = "tenkanPeriodNumeric";
            tenkanPeriodNumeric.Size = new Size(179, 31);
            tenkanPeriodNumeric.TabIndex = 1;
            tenkanPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // kijunPeriodNumeric
            // 
            kijunPeriodNumeric.Location = new Point(164, 87);
            kijunPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            kijunPeriodNumeric.Name = "kijunPeriodNumeric";
            kijunPeriodNumeric.Size = new Size(179, 31);
            kijunPeriodNumeric.TabIndex = 3;
            kijunPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // spanBPeriodNumeric
            // 
            spanBPeriodNumeric.Location = new Point(164, 153);
            spanBPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            spanBPeriodNumeric.Name = "spanBPeriodNumeric";
            spanBPeriodNumeric.Size = new Size(179, 31);
            spanBPeriodNumeric.TabIndex = 5;
            spanBPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // displacementNumeric
            // 
            displacementNumeric.Location = new Point(164, 220);
            displacementNumeric.Margin = new Padding(4, 5, 4, 5);
            displacementNumeric.Name = "displacementNumeric";
            displacementNumeric.Size = new Size(179, 31);
            displacementNumeric.TabIndex = 7;
            displacementNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // tenkanCheckBox
            // 
            tenkanCheckBox.AutoSize = true;
            tenkanCheckBox.Location = new Point(257, 287);
            tenkanCheckBox.Margin = new Padding(4, 5, 4, 5);
            tenkanCheckBox.Name = "tenkanCheckBox";
            tenkanCheckBox.Size = new Size(126, 29);
            tenkanCheckBox.TabIndex = 8;
            tenkanCheckBox.Text = "Tenkan-sen";
            tenkanCheckBox.UseVisualStyleBackColor = true;
            // 
            // kijunCheckBox
            // 
            kijunCheckBox.AutoSize = true;
            kijunCheckBox.Location = new Point(257, 347);
            kijunCheckBox.Margin = new Padding(4, 5, 4, 5);
            kijunCheckBox.Name = "kijunCheckBox";
            kijunCheckBox.Size = new Size(110, 29);
            kijunCheckBox.TabIndex = 10;
            kijunCheckBox.Text = "Kijun-sen";
            kijunCheckBox.UseVisualStyleBackColor = true;
            // 
            // spanACheckBox
            // 
            spanACheckBox.AutoSize = true;
            spanACheckBox.Location = new Point(257, 421);
            spanACheckBox.Margin = new Padding(4, 5, 4, 5);
            spanACheckBox.Name = "spanACheckBox";
            spanACheckBox.Size = new Size(159, 29);
            spanACheckBox.TabIndex = 12;
            spanACheckBox.Text = "Senkou Span A";
            spanACheckBox.UseVisualStyleBackColor = true;
            // 
            // spanBCheckBox
            // 
            spanBCheckBox.AutoSize = true;
            spanBCheckBox.Location = new Point(257, 487);
            spanBCheckBox.Margin = new Padding(4, 5, 4, 5);
            spanBCheckBox.Name = "spanBCheckBox";
            spanBCheckBox.Size = new Size(157, 29);
            spanBCheckBox.TabIndex = 14;
            spanBCheckBox.Text = "Senkou Span B";
            spanBCheckBox.UseVisualStyleBackColor = true;
            // 
            // chikouCheckBox
            // 
            chikouCheckBox.AutoSize = true;
            chikouCheckBox.Location = new Point(257, 554);
            chikouCheckBox.Margin = new Padding(4, 5, 4, 5);
            chikouCheckBox.Name = "chikouCheckBox";
            chikouCheckBox.Size = new Size(138, 29);
            chikouCheckBox.TabIndex = 16;
            chikouCheckBox.Text = "Chikou Span";
            chikouCheckBox.UseVisualStyleBackColor = true;
            // 
            // bullishCloudCheckBox
            // 
            bullishCloudCheckBox.AutoSize = true;
            bullishCloudCheckBox.Location = new Point(257, 621);
            bullishCloudCheckBox.Margin = new Padding(4, 5, 4, 5);
            bullishCloudCheckBox.Name = "bullishCloudCheckBox";
            bullishCloudCheckBox.Size = new Size(123, 29);
            bullishCloudCheckBox.TabIndex = 18;
            bullishCloudCheckBox.Text = "ابر صعودی";
            bullishCloudCheckBox.UseVisualStyleBackColor = true;
            // 
            // bearishCloudCheckBox
            // 
            bearishCloudCheckBox.AutoSize = true;
            bearishCloudCheckBox.Location = new Point(257, 687);
            bearishCloudCheckBox.Margin = new Padding(4, 5, 4, 5);
            bearishCloudCheckBox.Name = "bearishCloudCheckBox";
            bearishCloudCheckBox.Size = new Size(104, 29);
            bearishCloudCheckBox.TabIndex = 20;
            bearishCloudCheckBox.Text = "ابر نزولی";
            bearishCloudCheckBox.UseVisualStyleBackColor = true;
            // 
            // tenkanColorButton
            // 
            tenkanColorButton.BackColor = Color.FromArgb(220, 80, 80);
            tenkanColorButton.ForeColor = Color.White;
            tenkanColorButton.Location = new Point(42, 281);
            tenkanColorButton.Margin = new Padding(4, 5, 4, 5);
            tenkanColorButton.Name = "tenkanColorButton";
            tenkanColorButton.Size = new Size(179, 57);
            tenkanColorButton.TabIndex = 9;
            tenkanColorButton.Text = "انتخاب رنگ";
            tenkanColorButton.UseVisualStyleBackColor = false;
            // 
            // kijunColorButton
            // 
            kijunColorButton.BackColor = Color.FromArgb(80, 100, 220);
            kijunColorButton.ForeColor = Color.White;
            kijunColorButton.Location = new Point(42, 347);
            kijunColorButton.Margin = new Padding(4, 5, 4, 5);
            kijunColorButton.Name = "kijunColorButton";
            kijunColorButton.Size = new Size(179, 57);
            kijunColorButton.TabIndex = 11;
            kijunColorButton.Text = "انتخاب رنگ";
            kijunColorButton.UseVisualStyleBackColor = false;
            // 
            // spanAColorButton
            // 
            spanAColorButton.BackColor = Color.FromArgb(50, 150, 80);
            spanAColorButton.ForeColor = Color.White;
            spanAColorButton.Location = new Point(42, 414);
            spanAColorButton.Margin = new Padding(4, 5, 4, 5);
            spanAColorButton.Name = "spanAColorButton";
            spanAColorButton.Size = new Size(179, 57);
            spanAColorButton.TabIndex = 13;
            spanAColorButton.Text = "انتخاب رنگ";
            spanAColorButton.UseVisualStyleBackColor = false;
            // 
            // spanBColorButton
            // 
            spanBColorButton.BackColor = Color.FromArgb(180, 100, 60);
            spanBColorButton.ForeColor = Color.White;
            spanBColorButton.Location = new Point(42, 481);
            spanBColorButton.Margin = new Padding(4, 5, 4, 5);
            spanBColorButton.Name = "spanBColorButton";
            spanBColorButton.Size = new Size(179, 57);
            spanBColorButton.TabIndex = 15;
            spanBColorButton.Text = "انتخاب رنگ";
            spanBColorButton.UseVisualStyleBackColor = false;
            // 
            // chikouColorButton
            // 
            chikouColorButton.BackColor = Color.FromArgb(150, 80, 180);
            chikouColorButton.ForeColor = Color.White;
            chikouColorButton.Location = new Point(42, 547);
            chikouColorButton.Margin = new Padding(4, 5, 4, 5);
            chikouColorButton.Name = "chikouColorButton";
            chikouColorButton.Size = new Size(179, 57);
            chikouColorButton.TabIndex = 17;
            chikouColorButton.Text = "انتخاب رنگ";
            chikouColorButton.UseVisualStyleBackColor = false;
            // 
            // bullishCloudColorButton
            // 
            bullishCloudColorButton.BackColor = Color.FromArgb(130, 200, 130);
            bullishCloudColorButton.ForeColor = Color.Black;
            bullishCloudColorButton.Location = new Point(42, 614);
            bullishCloudColorButton.Margin = new Padding(4, 5, 4, 5);
            bullishCloudColorButton.Name = "bullishCloudColorButton";
            bullishCloudColorButton.Size = new Size(179, 57);
            bullishCloudColorButton.TabIndex = 19;
            bullishCloudColorButton.Text = "انتخاب رنگ";
            bullishCloudColorButton.UseVisualStyleBackColor = false;
            // 
            // bearishCloudColorButton
            // 
            bearishCloudColorButton.BackColor = Color.FromArgb(230, 150, 150);
            bearishCloudColorButton.ForeColor = Color.Black;
            bearishCloudColorButton.Location = new Point(42, 681);
            bearishCloudColorButton.Margin = new Padding(4, 5, 4, 5);
            bearishCloudColorButton.Name = "bearishCloudColorButton";
            bearishCloudColorButton.Size = new Size(179, 57);
            bearishCloudColorButton.TabIndex = 21;
            bearishCloudColorButton.Text = "انتخاب رنگ";
            bearishCloudColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(280, 759);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(108, 46);
            defaultButton.TabIndex = 22;
            defaultButton.Text = "پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(48, 759);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(108, 46);
            okButton.TabIndex = 23;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(164, 759);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(108, 46);
            cancelButton.TabIndex = 24;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // IchimokuSettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(426, 816);
            Controls.Add(tenkanPeriodLabel);
            Controls.Add(tenkanPeriodNumeric);
            Controls.Add(kijunPeriodLabel);
            Controls.Add(kijunPeriodNumeric);
            Controls.Add(spanBPeriodLabel);
            Controls.Add(spanBPeriodNumeric);
            Controls.Add(displacementLabel);
            Controls.Add(displacementNumeric);
            Controls.Add(tenkanCheckBox);
            Controls.Add(tenkanColorButton);
            Controls.Add(kijunCheckBox);
            Controls.Add(kijunColorButton);
            Controls.Add(spanACheckBox);
            Controls.Add(spanAColorButton);
            Controls.Add(spanBCheckBox);
            Controls.Add(spanBColorButton);
            Controls.Add(chikouCheckBox);
            Controls.Add(chikouColorButton);
            Controls.Add(bullishCloudCheckBox);
            Controls.Add(bullishCloudColorButton);
            Controls.Add(bearishCloudCheckBox);
            Controls.Add(bearishCloudColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IchimokuSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات ایچیموکو";
            ((System.ComponentModel.ISupportInitialize)tenkanPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)kijunPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)spanBPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)displacementNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}