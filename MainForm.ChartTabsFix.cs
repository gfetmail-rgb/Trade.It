namespace Trade.It
{
    public partial class MainForm
    {
        private static readonly bool chartTabsLayoutFixInitialized = InitializeChartTabsLayoutFix();
        private static readonly HashSet<MainForm> chartTabsFixAttachedForms = new();
        private bool chartTabsLayoutRestorePending;

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
                mainForm.fullScreenChartButton.Click += mainForm.ChartTabsLayoutChanged;
                mainForm.ScheduleChartTabsLayoutRestore();
            }
        }

        private void ChartTabsLayoutChanged(object? sender, EventArgs e)
        {
            ScheduleChartTabsLayoutRestore();
        }

        private void ScheduleChartTabsLayoutRestore()
        {
            if (IsDisposed || !IsHandleCreated || chartTabsLayoutRestorePending)
                return;

            chartTabsLayoutRestorePending = true;
            BeginInvoke(new Action(() =>
            {
                chartTabsLayoutRestorePending = false;
                if (IsDisposed || !IsHandleCreated)
                    return;

                RestoreChartTabsLayout();
            }));
        }

        private void RestoreChartTabsLayout()
        {
            if (IsDisposed || chartPanel == null || chartTabControl == null || chartToolbarPanel == null)
                return;

            chartPanel.SuspendLayout();
            try
            {
                chartToolbarPanel.Visible = true;
                chartTabControl.Visible = true;

                // Do not let the Dock layout engine decide the TabControl's final
                // position after the splitter is collapsed/restored. Set the two
                // regions explicitly so the tab header area can never be covered.
                chartToolbarPanel.Dock = DockStyle.None;
                chartTabControl.Dock = DockStyle.None;

                var width = Math.Max(0, chartPanel.ClientSize.Width);
                var height = Math.Max(0, chartPanel.ClientSize.Height);
                var toolbarHeight = Math.Max(0, chartToolbarPanel.Height);
                toolbarHeight = Math.Min(toolbarHeight, height);

                chartToolbarPanel.Bounds = new Rectangle(0, 0, width, toolbarHeight);
                chartTabControl.Bounds = new Rectangle(
                    0,
                    toolbarHeight,
                    width,
                    Math.Max(0, height - toolbarHeight));

                chartToolbarPanel.BringToFront();
                chartTabControl.BringToFront();
                chartToolbarPanel.BringToFront();

                chartPanel.PerformLayout();
                chartTabControl.PerformLayout();
            }
            finally
            {
                chartPanel.ResumeLayout(true);
            }

            chartTabControl.Invalidate(true);
            chartTabControl.Update();
        }
    }
}
