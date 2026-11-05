namespace Trade.It
{
    partial class AdxSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label periodLabel;
        private NumericUpDown periodNumeric;
        private CheckBox adxLineCheckBox, adxPlusDiCheckBox, adxMinusDiCheckBox, adx25CheckBox;
        private Button adxLineColorButton, adxPlusDiColorButton, adxMinusDiColorButton, adx25ColorButton;
        private Button okButton, defaultButton, cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            periodLabel = new Label();
            periodNumeric = new NumericUpDown();
            adxLineCheckBox = new CheckBox();
            adxPlusDiCheckBox = new CheckBox();
            adxMinusDiCheckBox = new CheckBox();
            adx25CheckBox = new CheckBox();
            adxLineColorButton = new Button();
            adxPlusDiColorButton = new Button();
            adxMinusDiColorButton = new Button();
            adx25ColorButton = new Button();
            okButton = new Button();
            defaultButton = new Button();
            cancelButton = new Button();

            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            SuspendLayout();

            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(300, 22);
            periodLabel.Size = new Size(70, 25);
            periodLabel.Text = "دوره:";

            periodNumeric.Location = new Point(150, 17);
            periodNumeric.Size = new Size(125, 33);
            periodNumeric.TextAlign = HorizontalAlignment.Center;

            adxLineCheckBox.AutoSize = true;
            adxLineCheckBox.Location = new Point(280, 66);
            adxLineCheckBox.Size = new Size(110, 29);
            adxLineCheckBox.Text = "خط ADX";
            adxLineColorButton.Location = new Point(120, 62);
            adxLineColorButton.Size = new Size(125, 34);
            adxLineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            adxLineColorButton.ForeColor = Color.White;
            adxLineColorButton.Text = "انتخاب رنگ";
            adxLineColorButton.UseVisualStyleBackColor = false;
            adxPlusDiCheckBox.AutoSize = true;
            adxPlusDiCheckBox.Location = new Point(280, 106);
            adxPlusDiCheckBox.Size = new Size(110, 29);
            adxPlusDiCheckBox.Text = "+DI";
            adxPlusDiColorButton.Location = new Point(120, 102);
            adxPlusDiColorButton.Size = new Size(125, 34);
            adxPlusDiColorButton.BackColor = Color.FromArgb(50, 160, 80);
            adxPlusDiColorButton.ForeColor = Color.White;
            adxPlusDiColorButton.Text = "انتخاب رنگ";
            adxPlusDiColorButton.UseVisualStyleBackColor = false;
            adxMinusDiCheckBox.AutoSize = true;
            adxMinusDiCheckBox.Location = new Point(280, 146);
            adxMinusDiCheckBox.Size = new Size(110, 29);
            adxMinusDiCheckBox.Text = "-DI";
            adxMinusDiColorButton.Location = new Point(120, 142);
            adxMinusDiColorButton.Size = new Size(125, 34);
            adxMinusDiColorButton.BackColor = Color.FromArgb(220, 80, 80);
            adxMinusDiColorButton.ForeColor = Color.White;
            adxMinusDiColorButton.Text = "انتخاب رنگ";
            adxMinusDiColorButton.UseVisualStyleBackColor = false;
            adx25CheckBox.AutoSize = true;
            adx25CheckBox.Location = new Point(280, 186);
            adx25CheckBox.Size = new Size(110, 29);
            adx25CheckBox.Text = "خط 25";
            adx25ColorButton.Location = new Point(120, 182);
            adx25ColorButton.Size = new Size(125, 34);
            adx25ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            adx25ColorButton.ForeColor = Color.Black;
            adx25ColorButton.Text = "انتخاب رنگ";
            adx25ColorButton.UseVisualStyleBackColor = false;

            okButton.Text = "تأیید";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(300, 235);
            okButton.Size = new Size(100, 36);

            cancelButton.Text = "انصراف";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(190, 235);
            cancelButton.Size = new Size(100, 36);

            defaultButton.Name = "defaultButton";
            defaultButton.Text = "پیش‌فرض";
            defaultButton.Location = new Point(80, 235);
            defaultButton.Size = new Size(100, 36);

            AcceptButton = okButton;
            CancelButton = cancelButton;
            Controls.AddRange(new Control[]
            {
                periodLabel, periodNumeric,
                adxLineCheckBox, adxLineColorButton,
                adxPlusDiCheckBox, adxPlusDiColorButton,
                adxMinusDiCheckBox, adxMinusDiColorButton,
                adx25CheckBox, adx25ColorButton,
                defaultButton, okButton, cancelButton
            });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 290);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdxSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات ADX";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;

            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

    }
}
