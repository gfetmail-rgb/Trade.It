namespace Trade.It
{
    public partial class SettingsForm
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            ApplyDesignerLayout();
        }

        private void ApplyDesignerLayout()
        {
            // Keep the two dense settings groups LTR so WinForms Designer uses the
            // same explicit coordinates that are used at runtime. The controls
            // themselves remain RTL for Persian text.
            drawingColorsGroupBox.RightToLeft = RightToLeft.No;
            crosshairGridGroupBox.RightToLeft = RightToLeft.No;

            SetDesignerButton(trendLineColorButton, "خط روند", 565, 30, Color.FromArgb(30, 90, 160));
            SetDesignerButton(trendChannelColorButton, "کانال روند", 375, 30, Color.FromArgb(30, 90, 160));
            SetDesignerButton(horizontalDoubleArrowColorButton, "خط افقی دو سر", 185, 30, Color.FromArgb(30, 90, 160));
            SetDesignerButton(verticalDoubleArrowColorButton, "خط عمودی دو سر", 0, 30, Color.FromArgb(30, 90, 160));
            SetDesignerButton(horizontalRayColorButton, "نیم‌خط افقی", 565, 76, Color.FromArgb(30, 90, 160));
            SetDesignerButton(trendLineWithArrowColorButton, "خط روند با فلش", 375, 76, Color.FromArgb(30, 90, 160));
            SetDesignerButton(rectangleColorButton, "مستطیل", 185, 76, Color.FromArgb(30, 90, 160));
            SetDesignerButton(fibonacciRetracementColorButton, "فیبوناچی اصلاحی", 0, 76, Color.FromArgb(45, 105, 170));
            SetDesignerButton(textLabelColorButton, "برچسب متن", 565, 122, Color.FromArgb(45, 105, 170));
            SetDesignerButton(pitchforkColorButton, "چنگال", 375, 122, Color.FromArgb(155, 80, 45));
            SetDesignerButton(fibonacciExtensionColorButton, "فیبوناچی اکستنشن", 185, 122, Color.FromArgb(155, 80, 45));
            SetDesignerButton(measureColorButton, "اندازه‌گیری", 0, 122, Color.FromArgb(155, 80, 45));

            crosshairColorButton.Location = new Point(570, 28);
            crosshairColorButton.BackColor = Color.FromArgb(120, 120, 120);
            crosshairColorButton.ForeColor = Color.White;
            crosshairColorButton.UseVisualStyleBackColor = false;

            gridColorButton.Location = new Point(570, 112);
            gridColorButton.BackColor = Color.FromArgb(225, 225, 225);
            gridColorButton.ForeColor = Color.Black;
            gridColorButton.UseVisualStyleBackColor = false;
        }

        private static void SetDesignerButton(Button button, string text, int x, int y, Color color)
        {
            button.Location = new Point(x, y);
            button.Size = new Size(175, 36);
            button.Text = text;
            button.BackColor = color;
            button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
            button.RightToLeft = RightToLeft.Yes;
            button.UseVisualStyleBackColor = false;
        }
    }
}
