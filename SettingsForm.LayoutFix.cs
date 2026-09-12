namespace Trade.It
{
    public partial class SettingsForm
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // فرم تنظیمات باید فضای کافی برای همه کنترل‌ها، به‌خصوص تنظیمات ضخامت و استایل ابزارهای رسم، داشته باشد.
            ClientSize = new Size(800, 920);
            MinimumSize = new Size(800, 920);
            StartPosition = FormStartPosition.CenterParent;

            chartColorsGroupBox.Location = new Point(16, 226);
            chartColorsGroupBox.Size = new Size(760, 145);

            drawingColorsGroupBox.Location = new Point(16, 381);
            drawingColorsGroupBox.Size = new Size(760, 250);
            drawingColorsPanel.Location = new Point(15, 28);
            drawingColorsPanel.Size = new Size(650, 165);
            drawingColorsPanel.AutoScroll = true;

            if (Controls["crosshairGridGroupBox"] is GroupBox crosshairGroup)
            {
                crosshairGroup.Location = new Point(16, 641);
                crosshairGroup.Size = new Size(760, 160);
            }

            okButton.Location = new Point(610, 850);
            cancelButton.Location = new Point(700, 850);
        }
    }
}
