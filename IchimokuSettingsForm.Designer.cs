namespace Trade.It
{
    partial class IchimokuSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private CheckBox tenkanCheckBox;
        private CheckBox kijunCheckBox;
        private CheckBox spanACheckBox;
        private CheckBox spanBCheckBox;
        private CheckBox chikouCheckBox;
        private CheckBox bullishCloudCheckBox;
        private CheckBox bearishCloudCheckBox;
        private Button tenkanColorButton;
        private Button kijunColorButton;
        private Button spanAColorButton;
        private Button spanBColorButton;
        private Button chikouColorButton;
        private Button bullishCloudColorButton;
        private Button bearishCloudColorButton;
        private Button okButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tenkanCheckBox = new CheckBox();
            kijunCheckBox = new CheckBox();
            spanACheckBox = new CheckBox();
            spanBCheckBox = new CheckBox();
            chikouCheckBox = new CheckBox();
            bullishCloudCheckBox = new CheckBox();
            bearishCloudCheckBox = new CheckBox();
            tenkanColorButton = new Button();
            kijunColorButton = new Button();
            spanAColorButton = new Button();
            spanBColorButton = new Button();
            chikouColorButton = new Button();
            bullishCloudColorButton = new Button();
            bearishCloudColorButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();

            ConfigureRow(tenkanCheckBox, tenkanColorButton, "Tenkan-sen");
            ConfigureRow(kijunCheckBox, kijunColorButton, "Kijun-sen");
            ConfigureRow(spanACheckBox, spanAColorButton, "Senkou Span A");
            ConfigureRow(spanBCheckBox, spanBColorButton, "Senkou Span B");
            ConfigureRow(chikouCheckBox, chikouColorButton, "Chikou Span");
            ConfigureRow(bullishCloudCheckBox, bullishCloudColorButton, "ابر صعودی");
            ConfigureRow(bearishCloudCheckBox, bearishCloudColorButton, "ابر نزولی");

            tenkanCheckBox.Location = new Point(205, 20);
            tenkanColorButton.Location = new Point(45, 15);
            kijunCheckBox.Location = new Point(205, 60);
            kijunColorButton.Location = new Point(45, 55);
            spanACheckBox.Location = new Point(205, 100);
            spanAColorButton.Location = new Point(45, 95);
            spanBCheckBox.Location = new Point(205, 140);
            spanBColorButton.Location = new Point(45, 135);
            chikouCheckBox.Location = new Point(205, 180);
            chikouColorButton.Location = new Point(45, 175);
            bullishCloudCheckBox.Location = new Point(205, 220);
            bullishCloudColorButton.Location = new Point(45, 215);
            bearishCloudCheckBox.Location = new Point(205, 260);
            bearishCloudColorButton.Location = new Point(45, 255);

            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(165, 310);
            okButton.Size = new Size(100, 36);
            okButton.Text = "تأیید";

            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(55, 310);
            cancelButton.Size = new Size(100, 36);
            cancelButton.Text = "انصراف";

            Controls.AddRange(new Control[]
            {
                tenkanCheckBox, tenkanColorButton,
                kijunCheckBox, kijunColorButton,
                spanACheckBox, spanAColorButton,
                spanBCheckBox, spanBColorButton,
                chikouCheckBox, chikouColorButton,
                bullishCloudCheckBox, bullishCloudColorButton,
                bearishCloudCheckBox, bearishCloudColorButton,
                okButton, cancelButton
            });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 365);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IchimokuSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات ایچیموکو";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            ResumeLayout(false);
        }

        private static void ConfigureRow(CheckBox checkBox, Button colorButton, string text)
        {
            checkBox.AutoSize = true;
            checkBox.Size = new Size(140, 29);
            checkBox.Text = text;

            colorButton.Size = new Size(130, 32);
            colorButton.Text = "انتخاب رنگ";
            colorButton.UseVisualStyleBackColor = false;
        }
    }
}