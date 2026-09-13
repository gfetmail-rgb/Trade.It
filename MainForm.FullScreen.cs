namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartFullScreen;
        private FormBorderStyle savedFormBorderStyle;
        private FormWindowState savedWindowState;
        private Rectangle savedBounds;
        private bool savedTopMost;
        private bool savedMenuVisible;
        private Point savedSplitLocation;

        private bool fullScreenHandlerInitialized;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (fullScreenHandlerInitialized)
                return;

            fullScreenHandlerInitialized = true;

            // Keep the fullscreen button in the chart toolbar, regardless of any
            // designer-generated layout changes.
            chartToolbarPanel.Controls.Add(fullScreenChartButton);
            fullScreenChartButton.BringToFront();

            // MainForm already attaches FullScreenChartButton_Click in its constructor.
            // Replace that handler with the real full-window fullscreen behavior.
            fullScreenChartButton.Click -= FullScreenChartButton_Click;
            fullScreenChartButton.Click += FullScreenChartButtonFullWindow_Click;
        }

        private void FullScreenChartButtonFullWindow_Click(object? sender, EventArgs e)
        {
            if (chartFullScreen)
            {
                ExitChartFullScreen();
                return;
            }

            savedFormBorderStyle = FormBorderStyle;
            savedWindowState = WindowState;
            savedBounds = Bounds;
            savedTopMost = TopMost;
            savedMenuVisible = mainMenuStrip.Visible;
            savedSplitLocation = mainSplitContainer.Location;

            chartFullScreen = true;

            mainSplitContainer.Panel1Collapsed = true;
            mainMenuStrip.Visible = false;
            mainSplitContainer.Location = Point.Empty;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            Bounds = Screen.FromControl(this).Bounds;
            TopMost = true;

            fullScreenChartButton.Text = "بازگشت";
            chartPanel.BringToFront();
            chartToolbarPanel.BringToFront();
        }

        private void ExitChartFullScreen()
        {
            chartFullScreen = false;

            TopMost = savedTopMost;
            FormBorderStyle = savedFormBorderStyle;
            WindowState = FormWindowState.Normal;
            Bounds = savedBounds;
            mainSplitContainer.Location = savedSplitLocation;
            mainMenuStrip.Visible = savedMenuVisible;
            mainSplitContainer.Panel1Collapsed = false;

            if (savedWindowState != FormWindowState.Normal)
                WindowState = savedWindowState;

            fullScreenChartButton.Text = "تمام صفحه";
        }
    }
}
