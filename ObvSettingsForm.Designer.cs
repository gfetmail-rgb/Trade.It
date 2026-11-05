namespace Trade.It
{
    partial class ObvSettingsForm
    {
        private System.ComponentModel.IContainer components=null; private CheckBox showLineCheckBox,showZeroCheckBox; private Button lineColorButton,zeroColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            showLineCheckBox = new CheckBox();
            showZeroCheckBox = new CheckBox();
            lineColorButton = new Button();
            zeroColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // showLineCheckBox
            // 
            showLineCheckBox.AutoSize = true;
            showLineCheckBox.Location = new Point(250, 25);
            showLineCheckBox.Name = "showLineCheckBox";
            showLineCheckBox.Size = new Size(158, 29);
            showLineCheckBox.TabIndex = 0;
            showLineCheckBox.Text = "نمایش خط OBV";
            // 
            // showZeroCheckBox
            // 
            showZeroCheckBox.AutoSize = true;
            showZeroCheckBox.Location = new Point(250, 70);
            showZeroCheckBox.Name = "showZeroCheckBox";
            showZeroCheckBox.Size = new Size(158, 29);
            showZeroCheckBox.TabIndex = 2;
            showZeroCheckBox.Text = "نمایش خط صفر";
            // 
            // lineColorButton
            // 
            lineColorButton.BackColor = Color.FromArgb(30, 100, 220);
            lineColorButton.ForeColor = Color.White;
            lineColorButton.Location = new Point(50, 20);
            lineColorButton.Name = "lineColorButton";
            lineColorButton.Size = new Size(150, 39);
            lineColorButton.TabIndex = 1;
            lineColorButton.Text = "رنگ خط";
            lineColorButton.UseVisualStyleBackColor = false;
            // 
            // zeroColorButton
            // 
            zeroColorButton.BackColor = Color.FromArgb(150, 150, 150);
            zeroColorButton.ForeColor = Color.Black;
            zeroColorButton.Location = new Point(50, 65);
            zeroColorButton.Name = "zeroColorButton";
            zeroColorButton.Size = new Size(150, 34);
            zeroColorButton.TabIndex = 3;
            zeroColorButton.Text = "رنگ خط صفر";
            zeroColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(274, 126);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(100, 36);
            defaultButton.TabIndex = 4;
            defaultButton.Text = "پیش‌فرض";
            // 
            // okButton
            // 
            okButton.Location = new Point(169, 126);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 36);
            okButton.TabIndex = 5;
            okButton.Text = "تأیید";
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(69, 126);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 36);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "انصراف";
            // 
            // ObvSettingsForm
            // 
            ClientSize = new Size(440, 185);
            Controls.Add(showLineCheckBox);
            Controls.Add(lineColorButton);
            Controls.Add(showZeroCheckBox);
            Controls.Add(zeroColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ObvSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات OBV";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}