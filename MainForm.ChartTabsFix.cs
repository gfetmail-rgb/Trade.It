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
            if (IsDisposed || chartTabsLayoutRestorePending)
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
                chartToolbarPanel.Dock = DockStyle.Top;
                chartTabControl.Dock = DockStyle.Fill;
                chartToolbarPanel.Visible = true;
                chartTabControl.Visible = true;

                // Explicit child order: toolbar occupies the top, TabControl fills
                // the remaining area and its tab headers stay visible.
                chartPanel.Controls.SetChildIndex(chartToolbarPanel, 0);
                chartPanel.Controls.SetChildIndex(chartTabControl, 1);

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
