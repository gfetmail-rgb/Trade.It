namespace Trade.It
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox chartDisplayGroupBox;
        private System.Windows.Forms.RadioButton separateTabsRadioButton;
        private System.Windows.Forms.RadioButton singleTabRadioButton;
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
            okButton = new Button();
            cancelButton = new Button();
            chartDisplayGroupBox.SuspendLayout();
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
            // okButton
            //
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(238, 148);
            okButton.Name = "okButton";
            okButton.Size = new Size(80, 30);
            okButton.TabIndex = 1;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            //
            // cancelButton
            //
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(324, 148);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 30);
            cancelButton.TabIndex = 2;
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
            ClientSize = new Size(420, 194);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
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
            ResumeLayout(false);
        }
    }
}
