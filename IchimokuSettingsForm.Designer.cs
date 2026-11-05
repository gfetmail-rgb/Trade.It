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

            // Tenkan period
            tenkanPeriodLabel.AutoSize = true;
            tenkanPeriodLabel.Location = new Point(285, 28);
            tenkanPeriodLabel.Name = "tenkanPeriodLabel";
            tenkanPeriodLabel.Size = new Size(115, 25);
            tenkanPeriodLabel.Text = "دوره Tenkan:";

            tenkanPeriodNumeric.Location = new Point(140, 24);
            tenkanPeriodNumeric.Name = "tenkanPeriodNumeric";
            tenkanPeriodNumeric.Size = new Size(125, 23);
            tenkanPeriodNumeric.TextAlign = HorizontalAlignment.Center;

            // Kijun period
            kijunPeriodLabel.AutoSize = true;
            kijunPeriodLabel.Location = new Point(285, 68);
            kijunPeriodLabel.Name = "kijunPeriodLabel";
            kijunPeriodLabel.Size = new Size(115, 25);
            kijunPeriodLabel.Text = "دوره Kijun:";

            kijunPeriodNumeric.Location = new Point(140, 64);
            kijunPeriodNumeric.Name = "kijunPeriodNumeric";
            kijunPeriodNumeric.Size = new Size(125, 23);
            kijunPeriodNumeric.TextAlign = HorizontalAlignment.Center;

            // Senkou Span B period
            spanBPeriodLabel.AutoSize = true;
            spanBPeriodLabel.Location = new Point(285, 108);
            spanBPeriodLabel.Name = "spanBPeriodLabel";
            spanBPeriodLabel.Size = new Size(115, 25);
            spanBPeriodLabel.Text = "دوره Span B:";

            spanBPeriodNumeric.Location = new Point(140, 104);
            spanBPeriodNumeric.Name = "spanBPeriodNumeric";
            spanBPeriodNumeric.Size = new Size(125, 23);
            spanBPeriodNumeric.TextAlign = HorizontalAlignment.Center;

            // Displacement
            displacementLabel.AutoSize = true;
            displacementLabel.Location = new Point(285, 148);
            displacementLabel.Name = "displacementLabel";
            displacementLabel.Size = new Size(115, 25);
            displacementLabel.Text = "جابجایی:";

            displacementNumeric.Location = new Point(140, 144);
            displacementNumeric.Name = "displacementNumeric";
            displacementNumeric.Size = new Size(125, 23);
            displacementNumeric.TextAlign = HorizontalAlignment.Center;

            // Tenkan-sen
            tenkanCheckBox.AutoSize = true;
            tenkanCheckBox.Location = new Point(265, 194);
            tenkanCheckBox.Name = "tenkanCheckBox";
            tenkanCheckBox.Size = new Size(130, 19);
            tenkanCheckBox.Text = "Tenkan-sen";
            tenkanCheckBox.UseVisualStyleBackColor = true;

            tenkanColorButton.Location = new Point(115, 190);
            tenkanColorButton.Name = "tenkanColorButton";
            tenkanColorButton.Size = new Size(125, 34);
            tenkanColorButton.Text = "انتخاب رنگ";
            tenkanColorButton.UseVisualStyleBackColor = false;

            // Kijun-sen
            kijunCheckBox.AutoSize = true;
            kijunCheckBox.Location = new Point(265, 234);
            kijunCheckBox.Name = "kijunCheckBox";
            kijunCheckBox.Size = new Size(130, 19);
            kijunCheckBox.Text = "Kijun-sen";
            kijunCheckBox.UseVisualStyleBackColor = true;

            kijunColorButton.Location = new Point(115, 230);
            kijunColorButton.Name = "kijunColorButton";
            kijunColorButton.Size = new Size(125, 34);
            kijunColorButton.Text = "انتخاب رنگ";
            kijunColorButton.UseVisualStyleBackColor = false;

            // Senkou Span A
            spanACheckBox.AutoSize = true;
            spanACheckBox.Location = new Point(265, 274);
            spanACheckBox.Name = "spanACheckBox";
            spanACheckBox.Size = new Size(130, 19);
            spanACheckBox.Text = "Senkou Span A";
            spanACheckBox.UseVisualStyleBackColor = true;

            spanAColorButton.Location = new Point(115, 270);
            spanAColorButton.Name = "spanAColorButton";
            spanAColorButton.Size = new Size(125, 34);
            spanAColorButton.Text = "انتخاب رنگ";
            spanAColorButton.UseVisualStyleBackColor = false;

            // Senkou Span B
            spanBCheckBox.AutoSize = true;
            spanBCheckBox.Location = new Point(265, 314);
            spanBCheckBox.Name = "spanBCheckBox";
            spanBCheckBox.Size = new Size(130, 19);
            spanBCheckBox.Text = "Senkou Span B";
            spanBCheckBox.UseVisualStyleBackColor = true;

            spanBColorButton.Location = new Point(115, 310);
            spanBColorButton.Name = "spanBColorButton";
            spanBColorButton.Size = new Size(125, 34);
            spanBColorButton.Text = "انتخاب رنگ";
            spanBColorButton.UseVisualStyleBackColor = false;

            // Chikou Span
            chikouCheckBox.AutoSize = true;
            chikouCheckBox.Location = new Point(265, 354);
            chikouCheckBox.Name = "chikouCheckBox";
            chikouCheckBox.Size = new Size(130, 19);
            chikouCheckBox.Text = "Chikou Span";
            chikouCheckBox.UseVisualStyleBackColor = true;

            chikouColorButton.Location = new Point(115, 350);
            chikouColorButton.Name = "chikouColorButton";
            chikouColorButton.Size = new Size(125, 34);
            chikouColorButton.Text = "انتخاب رنگ";
            chikouColorButton.UseVisualStyleBackColor = false;

            // Bullish cloud
            bullishCloudCheckBox.AutoSize = true;
            bullishCloudCheckBox.Location = new Point(265, 394);
            bullishCloudCheckBox.Name = "bullishCloudCheckBox";
            bullishCloudCheckBox.Size = new Size(130, 19);
            bullishCloudCheckBox.Text = "ابر صعودی";
            bullishCloudCheckBox.UseVisualStyleBackColor = true;

            bullishCloudColorButton.Location = new Point(115, 390);
            bullishCloudColorButton.Name = "bullishCloudColorButton";
            bullishCloudColorButton.Size = new Size(125, 34);
            bullishCloudColorButton.Text = "انتخاب رنگ";
            bullishCloudColorButton.UseVisualStyleBackColor = false;

            // Bearish cloud
            bearishCloudCheckBox.AutoSize = true;
            bearishCloudCheckBox.Location = new Point(265, 434);
            bearishCloudCheckBox.Name = "bearishCloudCheckBox";
            bearishCloudCheckBox.Size = new Size(130, 19);
            bearishCloudCheckBox.Text = "ابر نزولی";
            bearishCloudCheckBox.UseVisualStyleBackColor = true;

            bearishCloudColorButton.Location = new Point(115, 430);
            bearishCloudColorButton.Name = "bearishCloudColorButton";
            bearishCloudColorButton.Size = new Size(125, 34);
            bearishCloudColorButton.Text = "انتخاب رنگ";
            bearishCloudColorButton.UseVisualStyleBackColor = false;

            // Default button
            defaultButton.Location = new Point(80, 480);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(100, 34);
            defaultButton.Text = "پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;

            // OK button
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(300, 480);
            okButton.Name = "okButton";
            okButton.Size = new Size(100, 34);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            // Cancel button
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(190, 480);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 34);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            AcceptButton = okButton;
            CancelButton = cancelButton;

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

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 535);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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