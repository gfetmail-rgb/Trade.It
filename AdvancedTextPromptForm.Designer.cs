namespace Trade.It
{
    partial class AdvancedTextPromptForm
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.TextBox textBox = null!;
        private System.Windows.Forms.Button okButton = null!;
        private System.Windows.Forms.Button cancelButton = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Dock = DockStyle.Top;
            textBox.Location = new Point(0, 0);
            textBox.Margin = new Padding(4, 5, 4, 5);
            textBox.Name = "textBox";
            textBox.Size = new Size(483, 31);
            textBox.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(139, 56);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 36);
            okButton.TabIndex = 0;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(237, 56);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 36);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "لغو";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // AdvancedTextPromptForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(483, 103);
            Controls.Add(okButton);
            Controls.Add(textBox);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdvancedTextPromptForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "درج متن";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
