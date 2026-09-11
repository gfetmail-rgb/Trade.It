namespace Trade.It
{
    public partial class MainForm
    {
        private System.Windows.Forms.ToolTip? toolbarToolTip;
        private bool toolbarAppearanceInitialized;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ApplyToolbarAppearance();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                InitializeToolbarToolTips();

            InitializeChartDrawingTools();
        }

        private void ApplyToolbarAppearance()
        {
            if (toolbarAppearanceInitialized)
                return;

            toolbarAppearanceInitialized = true;

            // These are the actual Button.Text glyphs, so they are visible in the WinForms Designer.
            SetToolbarGlyph(gridButton, "▦");
            SetToolbarGlyph(crossButton, "⌖");
            SetToolbarGlyph(zoomInButton, "⊕");
            SetToolbarGlyph(zoomOutButton, "⊖");
            SetToolbarGlyph(resetChartButton, "↻");
            SetToolbarGlyph(hideChartButton, "◉");
            SetToolbarGlyph(hideToolsButton, "⚒");
            SetToolbarGlyph(printChartButton, "▣");
            SetToolbarGlyph(snapshotChartButton, "▣");
            SetToolbarGlyph(fullScreenChartButton, "⛶");

            SetToolbarGlyph(drawTrendLineButton, "╱");
            SetToolbarGlyph(drawTrendChannelButton, "╱╱");
            SetToolbarGlyph(drawHorizontalDoubleButton, "↔");
            SetToolbarGlyph(drawVerticalDoubleButton, "↕");
            SetToolbarGlyph(drawHorizontalRayButton, "⟶");
            SetToolbarGlyph(drawTrendLineArrowButton, "↗");
            SetToolbarGlyph(drawRectangleButton, "▭");
            SetToolbarGlyph(drawFibonacciButton, "F");
            SetToolbarGlyph(drawTextButton, "T");
            SetToolbarGlyph(drawPitchforkButton, "Ψ");
            SetToolbarGlyph(drawFibonacciExtensionButton, "F+");
            SetToolbarGlyph(drawMeasureButton, "↔%");

            foreach (var button in GetToolbarButtons())
            {
                button.Font = new System.Drawing.Font("Segoe UI Symbol", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                button.UseCompatibleTextRendering = true;
            }
        }

        private static void SetToolbarGlyph(System.Windows.Forms.Button button, string glyph)
        {
            button.Text = glyph;
        }

        private System.Windows.Forms.Button[] GetToolbarButtons()
        {
            return new[]
            {
                gridButton, crossButton, zoomInButton, zoomOutButton,
                resetChartButton, hideChartButton, hideToolsButton, printChartButton,
                snapshotChartButton, fullScreenChartButton,
                drawTrendLineButton, drawTrendChannelButton, drawHorizontalDoubleButton,
                drawVerticalDoubleButton, drawHorizontalRayButton, drawTrendLineArrowButton,
                drawRectangleButton, drawFibonacciButton, drawTextButton, drawPitchforkButton,
                drawFibonacciExtensionButton, drawMeasureButton
            };
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
