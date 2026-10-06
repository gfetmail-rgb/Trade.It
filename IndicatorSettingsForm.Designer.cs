namespace Trade.It
{
    partial class IndicatorSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label periodLabel;
        private NumericUpDown periodNumeric;
        private Label lineColorLabel;
        private Button lineColorButton;
        private Label backgroundColorLabel;
        private Button backgroundColorButton;
        private Button okButton;
        private Button defaultButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            periodLabel = new Label();
            periodNumeric = new NumericUpDown();
            lineColorLabel = new Label();
            lineColorButton = new Button();
            backgroundColorLabel = new Label();
            backgroundColorButton = new Button();
            okButton = new Button();
            defaultButton = new Button();
            cancelButton = new Button();

            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            SuspendLayout();

            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(255, 28);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(105, 25);
            periodLabel.Text = "پارامتر / دوره:";

            periodNumeric.Location = new Point(105, 23);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(130, 33);
            periodNumeric.TextAlign = HorizontalAlignment.Center;

            lineColorLabel.AutoSize = true;
            lineColorLabel.Location = new Point(255, 78);
            lineColorLabel.Name = "lineColorLabel";
            lineColorLabel.Size = new Size(105, 25);
            lineColorLabel.Text = "رنگ خط:";

            lineColorButton.Location = new Point(105, 72);
            lineColorButton.Name = "lineColorButton";
            lineColorButton.Size = new Size(130, 36);
            lineColorButton.Text = "انتخاب رنگ";
            lineColorButton.UseVisualStyleBackColor = false;

            backgroundColorLabel.AutoSize = true;
            backgroundColorLabel.Location = new Point(255, 128);
            backgroundColorLabel.Name = "backgroundColorLabel";
            backgroundColorLabel.Size = new Size(105, 25);
            backgroundColorLabel.Text = "رنگ زمینه:";

            backgroundColorButton.Location = new Point(105, 122);
            backgroundColorButton.Name = "backgroundColorButton";
            backgroundColorButton.Size = new Size(130, 36);
            backgroundColorButton.Text = "انتخاب رنگ";
            backgroundColorButton.UseVisualStyleBackColor = false;

            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(275, 180);
            okButton.Name = "okButton";
            okButton.Size = new Size(100, 36);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            defaultButton.Location = new Point(55, 180);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(100, 36);
            defaultButton.Text = "پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;

            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(165, 180);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 36);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 235);
            Controls.Add(defaultButton);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(backgroundColorButton);
            Controls.Add(backgroundColorLabel);
            Controls.Add(lineColorButton);
            Controls.Add(lineColorLabel);
            Controls.Add(periodNumeric);
            Controls.Add(periodLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IndicatorSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات اندیکاتور";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;

            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}