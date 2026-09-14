namespace Trade.It
{
    public partial class MainForm
    {
        private static readonly bool chartTabsLayoutFixInitialized = InitializeChartTabsLayoutFix();
        private static readonly HashSet<MainForm> chartTabsFixAttachedForms = new();

        private static bool InitializeChartTabsLayoutFix()
        {
            Application.Idle += ChartTabsLayoutFix_Idle;
            return true;
        }

        private static void ChartTabsLayoutFix_Idle(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is not MainForm mainForm || mainForm.IsDisposed)
                    continue;

                if (!chartTabsFixAttachedForms.Add(mainForm))
                    continue;

                mainForm.mainSplitContainer.Layout += mainForm.ChartTabsLayoutChanged;
                mainForm.mainSplitContainer.SizeChanged += mainForm.ChartTabsLayoutChanged;
                mainForm.chartPanel.SizeChanged += mainForm.ChartTabsLayoutChanged;
                mainForm.ChartTabsLayoutChanged(null, EventArgs.Empty);
            }
        }

        private void ChartTabsLayoutChanged(object? sender, EventArgs e)
        {
            if (IsDisposed || chartPanel == null || chartTabControl == null || chartToolbarPanel == null)
                return;

            // The chart area has two real regions: the toolbar at the top and
            // the TabControl below it. Keep the layout under normal WinForms
            // docking; do not assign Bounds manually during SplitContainer
            // collapse/restore, because that can leave stale coordinates after
            // the Panel1Collapsed transition.
            chartToolbarPanel.Visible = true;
            chartTabControl.Visible = true;
            chartToolbarPanel.Dock = DockStyle.Top;
            chartTabControl.Dock = DockStyle.Fill;

            chartPanel.PerformLayout();
            chartToolbarPanel.BringToFront();
            chartTabControl.BringToFront();
            chartToolbarPanel.BringToFront();
            chartTabControl.Invalidate(true);
        }
    }
}