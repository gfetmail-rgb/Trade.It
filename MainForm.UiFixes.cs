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

        private void ApplyUiFixes()
        {
            hideToolsButton.Visible = true;
            hideToolsButton.Text = "حذف ابزار";

            gridButton.Image = null;
            crossButton.Image = null;
            zoomInButton.Image = null;
            zoomOutButton.Image = null;
            resetChartButton.Image = null;
            hideChartButton.Image = null;
            hideToolsButton.Image = null;
            printChartButton.Image = null;
            snapshotChartButton.Image = null;
            fullScreenChartButton.Image = null;

            drawTrendLineButton.Image = null;
            drawTrendChannelButton.Image = null;
            drawHorizontalDoubleButton.Image = null;
            drawVerticalDoubleButton.Image = null;
            drawHorizontalRayButton.Image = null;
            drawTrendLineArrowButton.Image = null;
            drawRectangleButton.Image = null;
            drawFibonacciButton.Image = null;
            drawTextButton.Image = null;
            drawPitchforkButton.Image = null;
            drawFibonacciExtensionButton.Image = null;
            drawMeasureButton.Image = null;

            drawTrendLineButton.Text = "خط روند";
            drawTrendChannelButton.Text = "کانال روند";
            drawHorizontalDoubleButton.Text = "خط افقی دو سر";
            drawVerticalDoubleButton.Text = "خط عمودی دو سر";
            drawHorizontalRayButton.Text = "خط افقی نیم‌خط";
            drawTrendLineArrowButton.Text = "خط روند فلش‌دار";
            drawRectangleButton.Text = "مستطیل";
            drawFibonacciButton.Text = "فیبوناچی";
            drawTextButton.Text = "متن";
            drawPitchforkButton.Text = "چنگال";
            drawFibonacciExtensionButton.Text = "فیبو اکسپنشن";
            drawMeasureButton.Text = "خط‌کش";
        }
    }
}
