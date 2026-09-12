namespace Trade.It
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox chartDisplayGroupBox;
        private System.Windows.Forms.RadioButton separateTabsRadioButton;
        private System.Windows.Forms.RadioButton singleTabRadioButton;
        private System.Windows.Forms.GroupBox chartMarginGroupBox;
        private System.Windows.Forms.Label chartRightEmptyPercentLabel;
        private System.Windows.Forms.TextBox chartRightEmptyPercentTextBox;
        private System.Windows.Forms.Label chartRightEmptyPercentHintLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            chartDisplayGroupBox = new GroupBox();
            separateTabsRadioButton = new RadioButton();
            singleTabRadioButton = new RadioButton();
            chartMarginGroupBox = new GroupBox();
            chartRightEmptyPercentLabel = new Label();
            chartRightEmptyPercentTextBox = new TextBox();
            chartRightEmptyPercentHintLabel = new Label();
            okButton = new Button();
            cancelButton = new Button();
            chartDisplayGroupBox.SuspendLayout();
            chartMarginGroupBox.SuspendLayout();
            SuspendLayout();
            //
            // chartDisplayGroupBox
            //
            chartDisplayGroupBox.Controls.Add(singleTabRadioButton);
            chartDisplayGroupBox.Controls.Add(separateTabsRadioButton);
            chartDisplayGroupBox.Location = new Point(16, 16);
            chartDisplayGroupBox.Name = "chartDisplayGroupBox";
            chartDisplayGroupBox.RightToLeft = RightToLeft.Yes;
            chartDisplayGroupBox.Size = new Size(388, 112);
            chartDisplayGroupBox.TabIndex = 0;
            chartDisplayGroupBox.TabStop = false;
            chartDisplayGroupBox.Text = "نحوه نمایش چارت‌ها";
            //
            // separateTabsRadioButton
            //
            separateTabsRadioButton.AutoSize = true;
            separateTabsRadioButton.Location = new Point(20, 34);
            separateTabsRadioButton.Name = "separateTabsRadioButton";
            separateTabsRadioButton.RightToLeft = RightToLeft.Yes;
            separateTabsRadioButton.Size = new Size(344, 24);
            separateTabsRadioButton.TabIndex = 0;
            separateTabsRadioButton.TabStop = true;
            separateTabsRadioButton.Text = "هر چارت در یک تب جداگانه";
            separateTabsRadioButton.UseVisualStyleBackColor = true;
            //
            // singleTabRadioButton
            //
            singleTabRadioButton.AutoSize = true;
            singleTabRadioButton.Location = new Point(20, 72);
            singleTabRadioButton.Name = "singleTabRadioButton";
            singleTabRadioButton.RightToLeft = RightToLeft.Yes;
            singleTabRadioButton.Size = new Size(344, 24);
            singleTabRadioButton.TabIndex = 1;
            singleTabRadioButton.Text = "همه چارت‌ها در یک تب واحد";
            singleTabRadioButton.UseVisualStyleBackColor = true;
            //
            // chartMarginGroupBox
            //
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentHintLabel);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentTextBox);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentLabel);
            chartMarginGroupBox.Location = new Point(16, 138);
            chartMarginGroupBox.Name = "chartMarginGroupBox";
            chartMarginGroupBox.RightToLeft = RightToLeft.Yes;
            chartMarginGroupBox.Size = new Size(388, 118);
            chartMarginGroupBox.TabIndex = 1;
            chartMarginGroupBox.TabStop = false;
            chartMarginGroupBox.Text = "حاشیه خالی سمت راست چارت";
            //
            // chartRightEmptyPercentLabel
            //
            chartRightEmptyPercentLabel.AutoSize = true;
            chartRightEmptyPercentLabel.Location = new Point(214, 35);
            chartRightEmptyPercentLabel.Name = "chartRightEmptyPercentLabel";
            chartRightEmptyPercentLabel.Size = new Size(150, 20);
            chartRightEmptyPercentLabel.TabIndex = 0;
            chartRightEmptyPercentLabel.Text = "درصد فضای خالی سمت راست:";
            //
            // chartRightEmptyPercentTextBox
            //
            chartRightEmptyPercentTextBox.Location = new Point(104, 32);
            chartRightEmptyPercentTextBox.Name = "chartRightEmptyPercentTextBox";
            chartRightEmptyPercentTextBox.Size = new Size(82, 27);
            chartRightEmptyPercentTextBox.TabIndex = 1;
            chartRightEmptyPercentTextBox.TextAlign = HorizontalAlignment.Center;
            //
            // chartRightEmptyPercentHintLabel
            //
            chartRightEmptyPercentHintLabel.AutoSize = true;
            chartRightEmptyPercentHintLabel.Location = new Point(38, 72);
            chartRightEmptyPercentHintLabel.Name = "chartRightEmptyPercentHintLabel";
            chartRightEmptyPercentHintLabel.Size = new Size(326, 20);
            chartRightEmptyPercentHintLabel.TabIndex = 2;
            chartRightEmptyPercentHintLabel.Text = "مثلاً 25 یعنی یک‌چهارم عرض چارت خالی بماند (۰ تا ۹۰).";
            //
            // okButton
            //
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(238, 272);
            okButton.Name = "okButton";
            okButton.Size = new Size(80, 30);
            okButton.TabIndex = 2;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            //
            // cancelButton
            //
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(324, 272);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 30);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            //
            // SettingsForm
            //
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(420, 320);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(chartMarginGroupBox);
            Controls.Add(chartDisplayGroupBox);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات";
            chartDisplayGroupBox.ResumeLayout(false);
            chartDisplayGroupBox.PerformLayout();
            chartMarginGroupBox.ResumeLayout(false);
            chartMarginGroupBox.PerformLayout();
            ResumeLayout(false);
        }
    }
}
