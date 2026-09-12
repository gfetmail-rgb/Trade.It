namespace Trade.It
{
    public partial class SettingsForm
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // فرم تنظیمات باید روی نمایشگرهای با ارتفاع معمولی هم کامل دیده شود.
            // چیدمان را فشرده می‌کنیم؛ پنل رنگ ابزارهای رسم خودش اسکرول دارد.
            ClientSize = new Size(700, 750);
            MinimumSize = new Size(700, 750);
            StartPosition = FormStartPosition.CenterParent;

            chartColorsGroupBox.Location = new Point(16, 226);
            chartColorsGroupBox.Size = new Size(660, 105);

            drawingColorsGroupBox.Location = new Point(16, 341);
            drawingColorsGroupBox.Size = new Size(660, 195);
            drawingColorsPanel.Location = new Point(15, 28);
            drawingColorsPanel.Size = new Size(555, 150);
            drawingColorsPanel.AutoScroll = true;

            if (Controls["crosshairGridGroupBox"] is GroupBox crosshairGroup)
            {
                crosshairGroup.Location = new Point(16, 545);
                crosshairGroup.Size = new Size(660, 140);
            }

            okButton.Location = new Point(500, 700);
            cancelButton.Location = new Point(590, 700);
        }
    }
}
