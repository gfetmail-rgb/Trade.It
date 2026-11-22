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

            ConfigureToolbarIcons();

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

        private void ConfigureToolbarIcons()
        {
            var tip = toolbarToolTip!;

            ChartToolbarIcons.Apply(saveAnalysisButton, "save", "ذخیره تحلیل", tip);
            ChartToolbarIcons.Apply(gridButton, "grid", "گرید", tip);
            ChartToolbarIcons.Apply(crossButton, "crosshair", "کراس", tip);
            ChartToolbarIcons.Apply(indicatorPanelButton, "volume", "پنل حجم", tip);
            ChartToolbarIcons.Apply(testModeButton, "test", "حالت تست", tip);
            ChartToolbarIcons.Apply(testStepBackButton, "step-back", "کندل قبلی", tip);
            ChartToolbarIcons.Apply(testStepForwardButton, "step-forward", "کندل بعدی", tip);
            ChartToolbarIcons.Apply(zoomInButton, "zoom-in", "بزرگ‌نمایی", tip);
            ChartToolbarIcons.Apply(zoomOutButton, "zoom-out", "کوچک‌نمایی", tip);
            ChartToolbarIcons.Apply(resetChartButton, "reset", "بازنشانی", tip);
            ChartToolbarIcons.Apply(hideChartButton, "hide-chart", "پنهان‌کردن چارت", tip);
            ChartToolbarIcons.Apply(hideToolsButton, "erase", "حذف ابزارها", tip);
            ChartToolbarIcons.Apply(printChartButton, "print", "چاپ", tip);
            ChartToolbarIcons.Apply(snapshotChartButton, "camera", "تصویر چارت", tip);
            ChartToolbarIcons.Apply(fullScreenChartButton, "fullscreen", "تمام صفحه", tip);

            ChartToolbarIcons.Apply(drawTrendLineButton, "trend", "خط روند", tip);
            ChartToolbarIcons.Apply(drawTrendChannelButton, "channel", "کانال روند", tip);
            ChartToolbarIcons.Apply(drawHorizontalDoubleButton, "horizontal", "خط افقی", tip);
            ChartToolbarIcons.Apply(drawVerticalDoubleButton, "vertical", "خط عمودی", tip);
            ChartToolbarIcons.Apply(drawHorizontalRayButton, "ray", "نیم‌خط افقی", tip);
            ChartToolbarIcons.Apply(drawTrendLineArrowButton, "arrow-trend", "خط روند فلش‌دار", tip);
            ChartToolbarIcons.Apply(drawRectangleButton, "rectangle", "مستطیل", tip);
            ChartToolbarIcons.Apply(drawFibonacciButton, "fibonacci", "فیبوناچی اصلاحی", tip);
            ChartToolbarIcons.Apply(drawTextButton, "text", "متن", tip);
            ChartToolbarIcons.Apply(drawPitchforkButton, "pitchfork", "چنگال", tip);
            ChartToolbarIcons.Apply(drawFibonacciExtensionButton, "fib-extension", "فیبوناچی اکسپنشن", tip);
            ChartToolbarIcons.Apply(drawMeasureButton, "ruler", "خط‌کش", tip);
            ChartToolbarIcons.Apply(closeAllChartsToolbarButton, "close-all", "بستن همه چارت‌ها", tip);
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
            // آیکون‌ها در InitializeToolbarToolTips تنظیم می‌شوند؛
            // این متد نباید تصاویر یا متن‌های آن‌ها را بازنویسی کند.
            hideToolsButton.Visible = true;
        }
    }
}
