namespace Trade.It
{
    partial class MacdSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private CheckBox macdLineCheckBox;
        private Button macdLineColorButton;
        private CheckBox macdSignalCheckBox;
        private Button macdSignalColorButton;
        private CheckBox macdHistogramCheckBox;
        private Button macdBullishHistogramColorButton;
        private Button macdBearishHistogramColorButton;
        private CheckBox macdZeroCheckBox;
        private Button macdZeroColorButton;
        private Label bullishLabel;
        private Label bearishLabel;
        private Button okButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            macdLineCheckBox = new CheckBox();
            macdLineColorButton = new Button();
            macdSignalCheckBox = new CheckBox();
            macdSignalColorButton = new Button();
            macdHistogramCheckBox = new CheckBox();
            macdBullishHistogramColorButton = new Button();
            macdBearishHistogramColorButton = new Button();
            macdZeroCheckBox = new CheckBox();
            macdZeroColorButton = new Button();
            bullishLabel = new Label();
            bearishLabel = new Label();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();

            macdLineCheckBox.AutoSize = true;
            macdLineCheckBox.Location = new Point(250, 22);
            macdLineCheckBox.Size = new Size(125, 29);
            macdLineCheckBox.Text = "خط MACD";

            macdLineColorButton.Location = new Point(105, 18);
            macdLineColorButton.Size = new Size(125, 36);
            macdLineColorButton.Text = "انتخاب رنگ";
            macdLineColorButton.UseVisualStyleBackColor = false;

            macdSignalCheckBox.AutoSize = true;
            macdSignalCheckBox.Location = new Point(250, 68);
            macdSignalCheckBox.Size = new Size(125, 29);
            macdSignalCheckBox.Text = "خط Signal";

            macdSignalColorButton.Location = new Point(105, 64);
            macdSignalColorButton.Size = new Size(125, 36);
            macdSignalColorButton.Text = "انتخاب رنگ";
            macdSignalColorButton.UseVisualStyleBackColor = false;

            macdHistogramCheckBox.AutoSize = true;
            macdHistogramCheckBox.Location = new Point(250, 114);
            macdHistogramCheckBox.Size = new Size(125, 29);
            macdHistogramCheckBox.Text = "هیستوگرام";

            bullishLabel.AutoSize = true;
            bullishLabel.Location = new Point(300, 164);
            bullishLabel.Size = new Size(75, 25);
            bullishLabel.Text = "صعودی:";

            macdBullishHistogramColorButton.Location = new Point(105, 158);
            macdBullishHistogramColorButton.Size = new Size(125, 36);
            macdBullishHistogramColorButton.Text = "رنگ صعودی";
            macdBullishHistogramColorButton.UseVisualStyleBackColor = false;

            bearishLabel.AutoSize = true;
            bearishLabel.Location = new Point(300, 210);
            bearishLabel.Size = new Size(75, 25);
            bearishLabel.Text = "نزولی:";

            macdBearishHistogramColorButton.Location = new Point(105, 204);
            macdBearishHistogramColorButton.Size = new Size(125, 36);
            macdBearishHistogramColorButton.Text = "رنگ نزولی";
            macdBearishHistogramColorButton.UseVisualStyleBackColor = false;

            macdZeroCheckBox.AutoSize = true;
            macdZeroCheckBox.Location = new Point(250, 256);
            macdZeroCheckBox.Size = new Size(125, 29);
            macdZeroCheckBox.Text = "خط صفر";

            macdZeroColorButton.Location = new Point(105, 252);
            macdZeroColorButton.Size = new Size(125, 36);
            macdZeroColorButton.Text = "انتخاب رنگ";
            macdZeroColorButton.UseVisualStyleBackColor = false;

            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(190, 310);
            okButton.Size = new Size(100, 36);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(80, 310);
            cancelButton.Size = new Size(100, 36);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 370);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(macdZeroColorButton);
            Controls.Add(macdZeroCheckBox);
            Controls.Add(bearishLabel);
            Controls.Add(macdBearishHistogramColorButton);
            Controls.Add(bullishLabel);
            Controls.Add(macdBullishHistogramColorButton);
            Controls.Add(macdHistogramCheckBox);
            Controls.Add(macdSignalColorButton);
            Controls.Add(macdSignalCheckBox);
            Controls.Add(macdLineColorButton);
            Controls.Add(macdLineCheckBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MacdSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات MACD";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
