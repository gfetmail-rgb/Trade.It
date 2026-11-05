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
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(26, 37);
            periodLabel.Margin = new Padding(4, 0, 4, 0);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(113, 25);
            periodLabel.TabIndex = 8;
            periodLabel.Text = "پارامتر / دوره:";
            // 
            // periodNumeric
            // 
            periodNumeric.Location = new Point(147, 35);
            periodNumeric.Margin = new Padding(4, 5, 4, 5);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(138, 31);
            periodNumeric.TabIndex = 7;
            periodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // lineColorLabel
            // 
            lineColorLabel.AutoSize = true;
            lineColorLabel.Location = new Point(26, 103);
            lineColorLabel.Margin = new Padding(4, 0, 4, 0);
            lineColorLabel.Name = "lineColorLabel";
            lineColorLabel.Size = new Size(76, 25);
            lineColorLabel.TabIndex = 6;
            lineColorLabel.Text = "رنگ خط:";
            // 
            // lineColorButton
            // 
            lineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            lineColorButton.ForeColor = Color.White;
            lineColorButton.Location = new Point(147, 85);
            lineColorButton.Margin = new Padding(4, 5, 4, 5);
            lineColorButton.Name = "lineColorButton";
            lineColorButton.Size = new Size(138, 60);
            lineColorButton.TabIndex = 5;
            lineColorButton.Text = "انتخاب رنگ";
            lineColorButton.UseVisualStyleBackColor = false;
            // 
            // backgroundColorLabel
            // 
            backgroundColorLabel.AutoSize = true;
            backgroundColorLabel.Location = new Point(28, 171);
            backgroundColorLabel.Margin = new Padding(4, 0, 4, 0);
            backgroundColorLabel.Name = "backgroundColorLabel";
            backgroundColorLabel.Size = new Size(88, 25);
            backgroundColorLabel.TabIndex = 4;
            backgroundColorLabel.Text = "رنگ زمینه:";
            // 
            // backgroundColorButton
            // 
            backgroundColorButton.BackColor = Color.White;
            backgroundColorButton.ForeColor = Color.Black;
            backgroundColorButton.Location = new Point(154, 155);
            backgroundColorButton.Margin = new Padding(4, 5, 4, 5);
            backgroundColorButton.Name = "backgroundColorButton";
            backgroundColorButton.Size = new Size(131, 52);
            backgroundColorButton.TabIndex = 3;
            backgroundColorButton.Text = "انتخاب رنگ";
            backgroundColorButton.UseVisualStyleBackColor = false;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(142, 256);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(79, 45);
            okButton.TabIndex = 2;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(50, 256);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(79, 45);
            defaultButton.TabIndex = 0;
            defaultButton.Text = "پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(235, 256);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(79, 45);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // IndicatorSettingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 315);
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
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IndicatorSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات اندیکاتور";
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}