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

            ConfigureRow(adxLineCheckBox, adxLineColorButton, "خط ADX", 62);
            ConfigureRow(adxPlusDiCheckBox, adxPlusDiColorButton, "+DI", 102);
            ConfigureRow(adxMinusDiCheckBox, adxMinusDiColorButton, "-DI", 142);
            ConfigureRow(adx25CheckBox, adx25ColorButton, "خط 25", 182);

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

        private static void ConfigureRow(CheckBox checkBox, Button colorButton, string text, int y)
        {
            checkBox.AutoSize = true;
            checkBox.Location = new Point(280, y + 4);
            checkBox.Size = new Size(110, 29);
            checkBox.Text = text;
            colorButton.Location = new Point(120, y);
            colorButton.Size = new Size(125, 34);
            colorButton.Text = "انتخاب رنگ";
            colorButton.UseVisualStyleBackColor = false;
        }
    }
}
