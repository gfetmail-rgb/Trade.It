namespace Trade.It
{
    partial class RsiSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label periodLabel;
        private NumericUpDown periodNumeric;
        private CheckBox rsiLineCheckBox;
        private Button rsiLineColorButton;
        private CheckBox rsi30CheckBox;
        private Button rsi30ColorButton;
        private CheckBox rsi70CheckBox;
        private Button rsi70ColorButton;
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
            rsiLineCheckBox = new CheckBox();
            rsiLineColorButton = new Button();
            rsi30CheckBox = new CheckBox();
            rsi30ColorButton = new Button();
            rsi70CheckBox = new CheckBox();
            rsi70ColorButton = new Button();
            okButton = new Button();
            defaultButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            SuspendLayout();
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(18, 19);
            periodLabel.Margin = new Padding(4, 0, 4, 0);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(49, 25);
            periodLabel.TabIndex = 10;
            periodLabel.Text = "دوره:";
            // 
            // periodNumeric
            // 
            periodNumeric.Location = new Point(71, 17);
            periodNumeric.Margin = new Padding(4, 5, 4, 5);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(108, 31);
            periodNumeric.TabIndex = 9;
            periodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // rsiLineCheckBox
            // 
            rsiLineCheckBox.AutoSize = true;
            rsiLineCheckBox.Location = new Point(187, 93);
            rsiLineCheckBox.Margin = new Padding(4, 5, 4, 5);
            rsiLineCheckBox.Name = "rsiLineCheckBox";
            rsiLineCheckBox.Size = new Size(95, 29);
            rsiLineCheckBox.TabIndex = 8;
            rsiLineCheckBox.Text = "خط RSI";
            // 
            // rsiLineColorButton
            // 
            rsiLineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            rsiLineColorButton.ForeColor = Color.White;
            rsiLineColorButton.Location = new Point(29, 76);
            rsiLineColorButton.Margin = new Padding(4, 5, 4, 5);
            rsiLineColorButton.Name = "rsiLineColorButton";
            rsiLineColorButton.Size = new Size(150, 60);
            rsiLineColorButton.TabIndex = 7;
            rsiLineColorButton.Text = "انتخاب رنگ";
            rsiLineColorButton.UseVisualStyleBackColor = false;
            // 
            // rsi30CheckBox
            // 
            rsi30CheckBox.AutoSize = true;
            rsi30CheckBox.Location = new Point(187, 176);
            rsi30CheckBox.Margin = new Padding(4, 5, 4, 5);
            rsi30CheckBox.Name = "rsi30CheckBox";
            rsi30CheckBox.Size = new Size(86, 29);
            rsi30CheckBox.TabIndex = 6;
            rsi30CheckBox.Text = "خط 30";
            // 
            // rsi30ColorButton
            // 
            rsi30ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            rsi30ColorButton.ForeColor = Color.Black;
            rsi30ColorButton.Location = new Point(29, 159);
            rsi30ColorButton.Margin = new Padding(4, 5, 4, 5);
            rsi30ColorButton.Name = "rsi30ColorButton";
            rsi30ColorButton.Size = new Size(150, 60);
            rsi30ColorButton.TabIndex = 5;
            rsi30ColorButton.Text = "انتخاب رنگ";
            rsi30ColorButton.UseVisualStyleBackColor = false;
            // 
            // rsi70CheckBox
            // 
            rsi70CheckBox.AutoSize = true;
            rsi70CheckBox.Location = new Point(187, 260);
            rsi70CheckBox.Margin = new Padding(4, 5, 4, 5);
            rsi70CheckBox.Name = "rsi70CheckBox";
            rsi70CheckBox.Size = new Size(86, 29);
            rsi70CheckBox.TabIndex = 4;
            rsi70CheckBox.Text = "خط 70";
            // 
            // rsi70ColorButton
            // 
            rsi70ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            rsi70ColorButton.ForeColor = Color.Black;
            rsi70ColorButton.Location = new Point(29, 243);
            rsi70ColorButton.Margin = new Padding(4, 5, 4, 5);
            rsi70ColorButton.Name = "rsi70ColorButton";
            rsi70ColorButton.Size = new Size(150, 60);
            rsi70ColorButton.TabIndex = 3;
            rsi70ColorButton.Text = "انتخاب رنگ";
            rsi70ColorButton.UseVisualStyleBackColor = false;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(13, 326);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(92, 44);
            okButton.TabIndex = 2;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(115, 326);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(92, 44);
            defaultButton.TabIndex = 0;
            defaultButton.Text = "پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(215, 326);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(92, 44);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // RsiSettingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 384);
            Controls.Add(defaultButton);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(rsi70ColorButton);
            Controls.Add(rsi70CheckBox);
            Controls.Add(rsi30ColorButton);
            Controls.Add(rsi30CheckBox);
            Controls.Add(rsiLineColorButton);
            Controls.Add(rsiLineCheckBox);
            Controls.Add(periodNumeric);
            Controls.Add(periodLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RsiSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات RSI";
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
