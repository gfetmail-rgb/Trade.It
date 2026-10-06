namespace Trade.It
{
    partial class StochasticRsiSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private CheckBox showKCheckBox, showDCheckBox, show20CheckBox, show80CheckBox;
        private Button kColorButton, dColorButton, c20ColorButton, c80ColorButton, okButton, cancelButton;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            showKCheckBox = new CheckBox(); showDCheckBox = new CheckBox(); show20CheckBox = new CheckBox(); show80CheckBox = new CheckBox();
            kColorButton = new Button(); dColorButton = new Button(); c20ColorButton = new Button(); c80ColorButton = new Button();
            okButton = new Button(); cancelButton = new Button(); SuspendLayout();
            Text = "تنظیمات Stochastic RSI"; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            ClientSize = new Size(360, 270); FormBorderStyle = FormBorderStyle.FixedDialog; StartPosition = FormStartPosition.CenterParent; MaximizeBox = false; MinimizeBox = false;
            showKCheckBox.Text = "نمایش %K"; showKCheckBox.Location = new Point(210,25); showKCheckBox.Size = new Size(120,25);
            kColorButton.Text = "رنگ %K"; kColorButton.Location = new Point(40,22); kColorButton.Size = new Size(120,30); kColorButton.Click += kColorButton_Click;
            showDCheckBox.Text = "نمایش %D"; showDCheckBox.Location = new Point(210,70); showDCheckBox.Size = new Size(120,25);
            dColorButton.Text = "رنگ %D"; dColorButton.Location = new Point(40,67); dColorButton.Size = new Size(120,30); dColorButton.Click += dColorButton_Click;
            show20CheckBox.Text = "نمایش سطح 20"; show20CheckBox.Location = new Point(190,115); show20CheckBox.Size = new Size(140,25);
            c20ColorButton.Text = "رنگ سطح 20"; c20ColorButton.Location = new Point(40,112); c20ColorButton.Size = new Size(120,30); c20ColorButton.Click += c20ColorButton_Click;
            show80CheckBox.Text = "نمایش سطح 80"; show80CheckBox.Location = new Point(190,160); show80CheckBox.Size = new Size(140,25);
            c80ColorButton.Text = "رنگ سطح 80"; c80ColorButton.Location = new Point(40,157); c80ColorButton.Size = new Size(120,30); c80ColorButton.Click += c80ColorButton_Click;
            okButton.Text = "تأیید"; okButton.DialogResult = DialogResult.OK; okButton.Location = new Point(190,210); okButton.Size = new Size(100,32);
            cancelButton.Text = "انصراف"; cancelButton.DialogResult = DialogResult.Cancel; cancelButton.Location = new Point(70,210); cancelButton.Size = new Size(100,32);
            AcceptButton = okButton; CancelButton = cancelButton;
            Controls.AddRange(new Control[] { showKCheckBox,kColorButton,showDCheckBox,dColorButton,show20CheckBox,c20ColorButton,show80CheckBox,c80ColorButton,okButton,cancelButton }); ResumeLayout(false);
        }
    }
}