namespace Trade.It
{
    partial class IchimokuSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label tenkanPeriodLabel, kijunPeriodLabel, spanBPeriodLabel, displacementLabel;
        private NumericUpDown tenkanPeriodNumeric, kijunPeriodNumeric, spanBPeriodNumeric, displacementNumeric;
        private CheckBox tenkanCheckBox, kijunCheckBox, spanACheckBox, spanBCheckBox, chikouCheckBox, bullishCloudCheckBox, bearishCloudCheckBox;
        private Button tenkanColorButton, kijunColorButton, spanAColorButton, spanBColorButton, chikouColorButton, bullishCloudColorButton, bearishCloudColorButton;
        private Button defaultButton, okButton, cancelButton;

        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            tenkanPeriodLabel = new Label(); kijunPeriodLabel = new Label(); spanBPeriodLabel = new Label(); displacementLabel = new Label();
            tenkanPeriodNumeric = new NumericUpDown(); kijunPeriodNumeric = new NumericUpDown(); spanBPeriodNumeric = new NumericUpDown(); displacementNumeric = new NumericUpDown();
            tenkanCheckBox = new CheckBox(); kijunCheckBox = new CheckBox(); spanACheckBox = new CheckBox(); spanBCheckBox = new CheckBox(); chikouCheckBox = new CheckBox(); bullishCloudCheckBox = new CheckBox(); bearishCloudCheckBox = new CheckBox();
            tenkanColorButton = new Button(); kijunColorButton = new Button(); spanAColorButton = new Button(); spanBColorButton = new Button(); chikouColorButton = new Button(); bullishCloudColorButton = new Button(); bearishCloudColorButton = new Button();
            defaultButton = new Button(); okButton = new Button(); cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)tenkanPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kijunPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spanBPeriodNumeric).BeginInit(); ((System.ComponentModel.ISupportInitialize)displacementNumeric).BeginInit();
            SuspendLayout();

            ConfigureNumber(tenkanPeriodLabel, tenkanPeriodNumeric, "دوره Tenkan:", 24);
            ConfigureNumber(kijunPeriodLabel, kijunPeriodNumeric, "دوره Kijun:", 64);
            ConfigureNumber(spanBPeriodLabel, spanBPeriodNumeric, "دوره Span B:", 104);
            ConfigureNumber(displacementLabel, displacementNumeric, "جابجایی:", 144);

            ConfigureRow(tenkanCheckBox, tenkanColorButton, "Tenkan-sen", 190);
            ConfigureRow(kijunCheckBox, kijunColorButton, "Kijun-sen", 230);
            ConfigureRow(spanACheckBox, spanAColorButton, "Senkou Span A", 270);
            ConfigureRow(spanBCheckBox, spanBColorButton, "Senkou Span B", 310);
            ConfigureRow(chikouCheckBox, chikouColorButton, "Chikou Span", 350);
            ConfigureRow(bullishCloudCheckBox, bullishCloudColorButton, "ابر صعودی", 390);
            ConfigureRow(bearishCloudCheckBox, bearishCloudColorButton, "ابر نزولی", 430);

            defaultButton.Text = "پیش‌فرض"; defaultButton.Location = new Point(80, 480); defaultButton.Size = new Size(100, 34);
            okButton.Text = "تأیید"; okButton.DialogResult = DialogResult.OK; okButton.Location = new Point(300, 480); okButton.Size = new Size(100, 34);
            cancelButton.Text = "انصراف"; cancelButton.DialogResult = DialogResult.Cancel; cancelButton.Location = new Point(190, 480); cancelButton.Size = new Size(100, 34);
            AcceptButton = okButton; CancelButton = cancelButton;

            Controls.AddRange(new Control[] {
                tenkanPeriodLabel, tenkanPeriodNumeric, kijunPeriodLabel, kijunPeriodNumeric, spanBPeriodLabel, spanBPeriodNumeric, displacementLabel, displacementNumeric,
                tenkanCheckBox, tenkanColorButton, kijunCheckBox, kijunColorButton, spanACheckBox, spanAColorButton,
                spanBCheckBox, spanBColorButton, chikouCheckBox, chikouColorButton, bullishCloudCheckBox, bullishCloudColorButton,
                bearishCloudCheckBox, bearishCloudColorButton, defaultButton, okButton, cancelButton
            });

            AutoScaleDimensions = new SizeF(7F,15F); AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 535); FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
            Name = "IchimokuSettingsForm"; StartPosition = FormStartPosition.CenterParent; Text = "تنظیمات ایچیموکو";
            RightToLeft = RightToLeft.Yes; RightToLeftLayout = true; ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)tenkanPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)kijunPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)spanBPeriodNumeric).EndInit(); ((System.ComponentModel.ISupportInitialize)displacementNumeric).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        private static void ConfigureNumber(Label label, NumericUpDown numeric, string text, int y)
        {
            label.AutoSize = true; label.Text = text; label.Location = new Point(285, y + 4); label.Size = new Size(115, 25);
            numeric.Location = new Point(140, y); numeric.Size = new Size(125, 33); numeric.TextAlign = HorizontalAlignment.Center;
        }

        private static void ConfigureRow(CheckBox check, Button color, string text, int y)
        {
            check.AutoSize = true; check.Text = text; check.Location = new Point(265, y + 4); check.Size = new Size(130, 29);
            color.Text = "انتخاب رنگ"; color.Location = new Point(115, y); color.Size = new Size(125, 34); color.UseVisualStyleBackColor = false;
        }
    }
}