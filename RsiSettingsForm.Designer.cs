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
            cancelButton = new Button();

            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            SuspendLayout();

            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(310, 24);
            periodLabel.Size = new Size(65, 25);
            periodLabel.Text = "دوره:";

            periodNumeric.Location = new Point(165, 19);
            periodNumeric.Size = new Size(130, 33);
            periodNumeric.TextAlign = HorizontalAlignment.Center;

            rsiLineCheckBox.AutoSize = true;
            rsiLineCheckBox.Location = new Point(250, 70);
            rsiLineCheckBox.Size = new Size(125, 29);
            rsiLineCheckBox.Text = "خط RSI";

            rsiLineColorButton.Location = new Point(105, 66);
            rsiLineColorButton.Size = new Size(125, 36);
            rsiLineColorButton.Text = "انتخاب رنگ";
            rsiLineColorButton.UseVisualStyleBackColor = false;

            rsi30CheckBox.AutoSize = true;
            rsi30CheckBox.Location = new Point(250, 120);
            rsi30CheckBox.Size = new Size(125, 29);
            rsi30CheckBox.Text = "خط 30";

            rsi30ColorButton.Location = new Point(105, 116);
            rsi30ColorButton.Size = new Size(125, 36);
            rsi30ColorButton.Text = "انتخاب رنگ";
            rsi30ColorButton.UseVisualStyleBackColor = false;

            rsi70CheckBox.AutoSize = true;
            rsi70CheckBox.Location = new Point(250, 170);
            rsi70CheckBox.Size = new Size(125, 29);
            rsi70CheckBox.Text = "خط 70";

            rsi70ColorButton.Location = new Point(105, 166);
            rsi70ColorButton.Size = new Size(125, 36);
            rsi70ColorButton.Text = "انتخاب رنگ";
            rsi70ColorButton.UseVisualStyleBackColor = false;

            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(190, 225);
            okButton.Size = new Size(100, 36);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(80, 225);
            cancelButton.Size = new Size(100, 36);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 285);
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
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RsiSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات RSI";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;

            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
