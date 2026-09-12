
namespace Trade.It
{
    public partial class MainForm
    {
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

