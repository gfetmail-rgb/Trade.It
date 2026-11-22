using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private System.Windows.Forms.ToolTip? toolbarToolTip;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                InitializeToolbarToolTips();
            }

            InitializeChartDrawingTools();
            MainForm_HandleCreatedForChartTabs(this, EventArgs.Empty);
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

            SetToolbarToolTip(gridButton, "نمایش یا پنهان‌کردن گرید");
            SetToolbarToolTip(crossButton, "نمایش یا پنهان‌کردن خطوط کراس");
            SetToolbarToolTip(indicatorPanelButton, "نمایش یا پنهان‌کردن پنل حجم");
            SetToolbarToolTip(testModeButton, "حالت تست");
            SetToolbarToolTip(testStepBackButton, "رفتن به کندل قبلی");
            SetToolbarToolTip(testStepForwardButton, "رفتن به کندل بعدی");
            SetToolbarToolTip(zoomInButton, "بزرگ‌نمایی افقی");
            SetToolbarToolTip(zoomOutButton, "کوچک‌نمایی افقی");
            SetToolbarToolTip(resetChartButton, "بازنشانی نمای نمودار");
            SetToolbarToolTip(hideChartButton, "پنهان‌کردن نمودار");
            SetToolbarToolTip(hideToolsButton, "حذف همه ابزارهای رسم");
            SetToolbarToolTip(printChartButton, "چاپ نمودار");
            SetToolbarToolTip(snapshotChartButton, "ذخیره تصویر نمودار");
            SetToolbarToolTip(fullScreenChartButton, "تمام صفحه");
            SetToolbarToolTip(closeAllChartsToolbarButton, "بستن همه چارت‌ها");
            SetToolbarToolTip(saveAnalysisButton, "برای مشاهده زمان آخرین ذخیره مکث کنید.");

            saveAnalysisButton.MouseEnter += SaveAnalysisButton_MouseEnter;
            saveAnalysisButton.MouseHover += SaveAnalysisButton_MouseEnter;
            saveAnalysisButton.MouseLeave += (_, _) => toolbarToolTip.Hide(saveAnalysisButton);
        }

        private void SaveAnalysisButton_MouseEnter(object? sender, EventArgs e)
        {
            string tooltipText;

            try
            {
                var chart = GetActiveChart();
                var document = chart == null || string.IsNullOrWhiteSpace(chart.ChartSymbol)
                    ? null
                    : ChartAnalysisStorage.Load(chart.ChartSymbol);

                tooltipText = document == null || document.SavedAt == default
                    ? "هنوز تحلیلی ذخیره نشده است."
                    : $"آخرین ذخیره: {document.SavedAt.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.GetCultureInfo("fa-IR"))}";
            }
            catch
            {
                tooltipText = "زمان آخرین ذخیره در دسترس نیست.";
            }

            SetToolbarToolTip(saveAnalysisButton, tooltipText);
        }

        private void SetToolbarToolTip(System.Windows.Forms.Button button, string text)
        {
            toolbarToolTip!.SetToolTip(button, text);
        }

        private void ApplyUiFixes()
        {
            // تصاویر آیکون‌ها در Designer و از منابع تصویری فرم بارگذاری می‌شوند.
            hideToolsButton.Visible = true;
        }
    }
}
