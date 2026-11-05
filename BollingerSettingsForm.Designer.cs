namespace Trade.It
{
    partial class BollingerSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label periodLabel,stdLabel;
        private NumericUpDown periodNumeric,stdNumeric;
        private CheckBox middleCheckBox,upperCheckBox,lowerCheckBox;
        private Button middleColorButton,upperColorButton,lowerColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            periodLabel = new Label();
            stdLabel = new Label();
            periodNumeric = new NumericUpDown();
            stdNumeric = new NumericUpDown();
            middleCheckBox = new CheckBox();
            upperCheckBox = new CheckBox();
            lowerCheckBox = new CheckBox();
            middleColorButton = new Button();
            upperColorButton = new Button();
            lowerColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stdNumeric).BeginInit();
            SuspendLayout();
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(304, 24);
            periodLabel.Name = "periodLabel";
            periodLabel.RightToLeft = RightToLeft.Yes;
            periodLabel.Size = new Size(49, 25);
            periodLabel.TabIndex = 0;
            periodLabel.Text = "دوره:";
            // 
            // stdLabel
            // 
            stdLabel.AutoSize = true;
            stdLabel.Location = new Point(240, 60);
            stdLabel.Name = "stdLabel";
            stdLabel.RightToLeft = RightToLeft.Yes;
            stdLabel.Size = new Size(113, 25);
            stdLabel.TabIndex = 2;
            stdLabel.Text = "انحراف معیار:";
            // 
            // periodNumeric
            // 
            periodNumeric.Location = new Point(97, 18);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(120, 31);
            periodNumeric.TabIndex = 1;
            // 
            // stdNumeric
            // 
            stdNumeric.Location = new Point(97, 58);
            stdNumeric.Name = "stdNumeric";
            stdNumeric.Size = new Size(120, 31);
            stdNumeric.TabIndex = 3;
            // 
            // middleCheckBox
            // 
            middleCheckBox.AutoSize = true;
            middleCheckBox.Location = new Point(187, 111);
            middleCheckBox.Name = "middleCheckBox";
            middleCheckBox.Size = new Size(166, 29);
            middleCheckBox.TabIndex = 4;
            middleCheckBox.Text = "نمایش خط میانی";
            // 
            // upperCheckBox
            // 
            upperCheckBox.AutoSize = true;
            upperCheckBox.Location = new Point(187, 174);
            upperCheckBox.Name = "upperCheckBox";
            upperCheckBox.Size = new Size(145, 29);
            upperCheckBox.TabIndex = 6;
            upperCheckBox.Text = "نمایش باند بالا";
            // 
            // lowerCheckBox
            // 
            lowerCheckBox.AutoSize = true;
            lowerCheckBox.Location = new Point(187, 220);
            lowerCheckBox.Name = "lowerCheckBox";
            lowerCheckBox.Size = new Size(158, 29);
            lowerCheckBox.TabIndex = 8;
            lowerCheckBox.Text = "نمایش باند پایین";
            // 
            // middleColorButton
            // 
            middleColorButton.BackColor = Color.FromArgb(30, 100, 220);
            middleColorButton.ForeColor = Color.White;
            middleColorButton.Location = new Point(12, 100);
            middleColorButton.Name = "middleColorButton";
            middleColorButton.Size = new Size(160, 48);
            middleColorButton.TabIndex = 5;
            middleColorButton.Text = "رنگ خط میانی";
            middleColorButton.UseVisualStyleBackColor = false;
            // 
            // upperColorButton
            // 
            upperColorButton.BackColor = Color.FromArgb(50, 160, 80);
            upperColorButton.ForeColor = Color.White;
            upperColorButton.Location = new Point(12, 154);
            upperColorButton.Name = "upperColorButton";
            upperColorButton.Size = new Size(160, 49);
            upperColorButton.TabIndex = 7;
            upperColorButton.Text = "رنگ باند بالا";
            upperColorButton.UseVisualStyleBackColor = false;
            // 
            // lowerColorButton
            // 
            lowerColorButton.BackColor = Color.FromArgb(220, 80, 80);
            lowerColorButton.ForeColor = Color.White;
            lowerColorButton.Location = new Point(12, 209);
            lowerColorButton.Name = "lowerColorButton";
            lowerColorButton.Size = new Size(160, 48);
            lowerColorButton.TabIndex = 9;
            lowerColorButton.Text = "رنگ باند پایین";
            lowerColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(207, 276);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(100, 36);
            defaultButton.TabIndex = 10;
            defaultButton.Text = "پیش‌فرض";
            // 
            // okButton
            // 
            okButton.Location = new Point(111, 276);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 36);
            okButton.TabIndex = 11;
            okButton.Text = "تأیید";
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(15, 276);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 36);
            cancelButton.TabIndex = 12;
            cancelButton.Text = "انصراف";
            // 
            // BollingerSettingsForm
            // 
            ClientSize = new Size(359, 322);
            Controls.Add(periodLabel);
            Controls.Add(periodNumeric);
            Controls.Add(stdLabel);
            Controls.Add(stdNumeric);
            Controls.Add(middleCheckBox);
            Controls.Add(middleColorButton);
            Controls.Add(upperCheckBox);
            Controls.Add(upperColorButton);
            Controls.Add(lowerCheckBox);
            Controls.Add(lowerColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BollingerSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات بولینگر";
            Load += BollingerSettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)stdNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}