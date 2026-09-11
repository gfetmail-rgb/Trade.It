namespace Trade.It
{
    public partial class MainForm
    {
        private System.Windows.Forms.ToolTip? toolbarToolTip;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                InitializeToolbarToolTips();

            InitializeChartDrawingTools();
        }

        private void InitializeToolbarToolTips()
        {
            if (toolbarToolTip != null)
                return;

            toolbarToolTip = new System.Windows.Forms.ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 400,
                ReshowDelay = 100,
                ShowAlways = true
            };

            SetToolbarToolTip(gridButton, "گرید");
            SetToolbarToolTip(crossButton, "کراس‌هیر");
            SetToolbarToolTip(zoomInButton, "بزرگ‌نمایی");
            SetToolbarToolTip(zoomOutButton, "کوچک‌نمایی");
            SetToolbarToolTip(resetChartButton, "بازنشانی نمودار");
            SetToolbarToolTip(hideChartButton, "مخفی کردن نمودار");
            SetToolbarToolTip(hideToolsButton, "مخفی کردن ابزارها");
            SetToolbarToolTip(printChartButton, "چاپ نمودار");
            SetToolbarToolTip(snapshotChartButton, "تصویر از نمودار");
            SetToolbarToolTip(fullScreenChartButton, "تمام صفحه");

            SetToolbarToolTip(drawTrendLineButton, "خط روند");
            SetToolbarToolTip(drawTrendChannelButton, "کانال روند");
            SetToolbarToolTip(drawHorizontalDoubleButton, "خط افقی دو سر");
            SetToolbarToolTip(drawVerticalDoubleButton, "خط عمودی دو سر");
            SetToolbarToolTip(drawHorizontalRayButton, "نیم‌خط افقی");
            SetToolbarToolTip(drawTrendLineArrowButton, "خط روند فلش‌دار");
            SetToolbarToolTip(drawRectangleButton, "مستطیل");
            SetToolbarToolTip(drawFibonacciButton, "فیبوناچی");
            SetToolbarToolTip(drawTextButton, "متن");
            SetToolbarToolTip(drawPitchforkButton, "چنگال");
            SetToolbarToolTip(drawFibonacciExtensionButton, "فیبو اکسپنشن");
            SetToolbarToolTip(drawMeasureButton, "خط‌کش");
        }

        private void SetToolbarToolTip(System.Windows.Forms.Button button, string text)
        {
            toolbarToolTip!.SetToolTip(button, text);
        }
    }
}
