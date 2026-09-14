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
        private DockStyle savedChartPanelDock;
        private DockStyle savedChartTabDock;
        private bool savedChartTabVisible;
        private bool savedChartToolbarVisible;

        private bool fullScreenHandlerInitialized;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            InitializeSettingsRuntime();

            if (fullScreenHandlerInitialized)
                return;

            fullScreenHandlerInitialized = true;

            chartToolbarPanel.Controls.Add(fullScreenChartButton);
            fullScreenChartButton.BringToFront();

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
            savedChartPanelDock = chartPanel.Dock;
            savedChartTabDock = chartTabControl.Dock;
            savedChartTabVisible = chartTabControl.Visible;
            savedChartToolbarVisible = chartToolbarPanel.Visible;

            chartFullScreen = true;

            mainSplitContainer.Panel1Collapsed = true;
            mainMenuStrip.Visible = false;
            mainSplitContainer.Location = Point.Empty;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            Bounds = Screen.FromControl(this).Bounds;
            TopMost = true;

            chartPanel.Dock = DockStyle.Fill;
            chartPanel.Visible = true;
            chartToolbarPanel.Visible = true;
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Visible = true;

            fullScreenChartButton.Text = "بازگشت";

            // Keep the designer z-order. chartToolbarPanel is Dock.Top and
            // chartTabControl is Dock.Fill; changing sibling z-order here causes
            // the tab header to disappear after returning from fullscreen.
            chartPanel.PerformLayout();
            chartTabControl.PerformLayout();
            chartTabControl.Invalidate();
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

            chartPanel.Dock = savedChartPanelDock;
            chartTabControl.Dock = savedChartTabDock;
            chartToolbarPanel.Visible = savedChartToolbarVisible;
            chartPanel.Visible = true;
            chartTabControl.Visible = savedChartTabVisible;

            if (savedWindowState != FormWindowState.Normal)
                WindowState = savedWindowState;

            fullScreenChartButton.Text = "تمام صفحه";

            chartPanel.PerformLayout();
            chartTabControl.PerformLayout();
            chartTabControl.BringToFront();
            chartTabControl.Invalidate();
        }
    }
}
