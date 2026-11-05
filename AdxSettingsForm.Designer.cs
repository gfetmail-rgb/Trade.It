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
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(13, 30);
            periodLabel.Margin = new Padding(4, 0, 4, 0);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(49, 25);
            periodLabel.TabIndex = 0;
            periodLabel.Text = "دوره:";
            // 
            // periodNumeric
            // 
            periodNumeric.Location = new Point(70, 30);
            periodNumeric.Margin = new Padding(4, 5, 4, 5);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(95, 31);
            periodNumeric.TabIndex = 1;
            periodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // adxLineCheckBox
            // 
            adxLineCheckBox.AutoSize = true;
            adxLineCheckBox.Location = new Point(186, 98);
            adxLineCheckBox.Margin = new Padding(4, 5, 4, 5);
            adxLineCheckBox.Name = "adxLineCheckBox";
            adxLineCheckBox.Size = new Size(104, 29);
            adxLineCheckBox.TabIndex = 2;
            adxLineCheckBox.Text = "خط ADX";
            // 
            // adxPlusDiCheckBox
            // 
            adxPlusDiCheckBox.AutoSize = true;
            adxPlusDiCheckBox.Location = new Point(186, 165);
            adxPlusDiCheckBox.Margin = new Padding(4, 5, 4, 5);
            adxPlusDiCheckBox.Name = "adxPlusDiCheckBox";
            adxPlusDiCheckBox.Size = new Size(68, 29);
            adxPlusDiCheckBox.TabIndex = 4;
            adxPlusDiCheckBox.Text = "+DI";
            // 
            // adxMinusDiCheckBox
            // 
            adxMinusDiCheckBox.AutoSize = true;
            adxMinusDiCheckBox.Location = new Point(186, 231);
            adxMinusDiCheckBox.Margin = new Padding(4, 5, 4, 5);
            adxMinusDiCheckBox.Name = "adxMinusDiCheckBox";
            adxMinusDiCheckBox.Size = new Size(63, 29);
            adxMinusDiCheckBox.TabIndex = 6;
            adxMinusDiCheckBox.Text = "-DI";
            // 
            // adx25CheckBox
            // 
            adx25CheckBox.AutoSize = true;
            adx25CheckBox.Location = new Point(186, 298);
            adx25CheckBox.Margin = new Padding(4, 5, 4, 5);
            adx25CheckBox.Name = "adx25CheckBox";
            adx25CheckBox.Size = new Size(87, 29);
            adx25CheckBox.TabIndex = 8;
            adx25CheckBox.Text = "خط 25";
            // 
            // adxLineColorButton
            // 
            adxLineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            adxLineColorButton.ForeColor = Color.White;
            adxLineColorButton.Location = new Point(27, 82);
            adxLineColorButton.Margin = new Padding(4, 5, 4, 5);
            adxLineColorButton.Name = "adxLineColorButton";
            adxLineColorButton.Size = new Size(138, 57);
            adxLineColorButton.TabIndex = 3;
            adxLineColorButton.Text = "انتخاب رنگ";
            adxLineColorButton.UseVisualStyleBackColor = false;
            // 
            // adxPlusDiColorButton
            // 
            adxPlusDiColorButton.BackColor = Color.FromArgb(50, 160, 80);
            adxPlusDiColorButton.ForeColor = Color.White;
            adxPlusDiColorButton.Location = new Point(27, 149);
            adxPlusDiColorButton.Margin = new Padding(4, 5, 4, 5);
            adxPlusDiColorButton.Name = "adxPlusDiColorButton";
            adxPlusDiColorButton.Size = new Size(138, 57);
            adxPlusDiColorButton.TabIndex = 5;
            adxPlusDiColorButton.Text = "انتخاب رنگ";
            adxPlusDiColorButton.UseVisualStyleBackColor = false;
            // 
            // adxMinusDiColorButton
            // 
            adxMinusDiColorButton.BackColor = Color.FromArgb(220, 80, 80);
            adxMinusDiColorButton.ForeColor = Color.White;
            adxMinusDiColorButton.Location = new Point(27, 216);
            adxMinusDiColorButton.Margin = new Padding(4, 5, 4, 5);
            adxMinusDiColorButton.Name = "adxMinusDiColorButton";
            adxMinusDiColorButton.Size = new Size(138, 57);
            adxMinusDiColorButton.TabIndex = 7;
            adxMinusDiColorButton.Text = "انتخاب رنگ";
            adxMinusDiColorButton.UseVisualStyleBackColor = false;
            // 
            // adx25ColorButton
            // 
            adx25ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            adx25ColorButton.ForeColor = Color.Black;
            adx25ColorButton.Location = new Point(27, 282);
            adx25ColorButton.Margin = new Padding(4, 5, 4, 5);
            adx25ColorButton.Name = "adx25ColorButton";
            adx25ColorButton.Size = new Size(138, 57);
            adx25ColorButton.TabIndex = 9;
            adx25ColorButton.Text = "انتخاب رنگ";
            adx25ColorButton.UseVisualStyleBackColor = false;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(13, 392);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(109, 45);
            okButton.TabIndex = 11;
            okButton.Text = "تأیید";
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(130, 392);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(109, 45);
            defaultButton.TabIndex = 10;
            defaultButton.Text = "پیش‌فرض";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(247, 392);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(109, 45);
            cancelButton.TabIndex = 12;
            cancelButton.Text = "انصراف";
            // 
            // AdxSettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(370, 442);
            Controls.Add(periodLabel);
            Controls.Add(periodNumeric);
            Controls.Add(adxLineCheckBox);
            Controls.Add(adxLineColorButton);
            Controls.Add(adxPlusDiCheckBox);
            Controls.Add(adxPlusDiColorButton);
            Controls.Add(adxMinusDiCheckBox);
            Controls.Add(adxMinusDiColorButton);
            Controls.Add(adx25CheckBox);
            Controls.Add(adx25ColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdxSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات ADX";
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

    }
}
