namespace Trade.It
{
    partial class AdvancedTextPromptForm
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.TextBox textBox = null!;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel = null!;
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
            textBox = new System.Windows.Forms.TextBox();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Dock = System.Windows.Forms.DockStyle.Top;
            textBox.Location = new System.Drawing.Point(0, 0);
            textBox.Multiline = false;
            textBox.Name = "textBox";
            textBox.Size = new System.Drawing.Size(360, 23);
            textBox.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            buttonsPanel.Location = new System.Drawing.Point(0, 105);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new System.Windows.Forms.Padding(5);
            buttonsPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            buttonsPanel.Size = new System.Drawing.Size(360, 45);
            buttonsPanel.TabIndex = 1;
            // 
            // okButton
            // 
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(80, 30);
            okButton.TabIndex = 0;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(80, 30);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "لغو";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // AdvancedTextPromptForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(360, 150);
            Controls.Add(textBox);
            Controls.Add(buttonsPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdvancedTextPromptForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "درج متن";
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
