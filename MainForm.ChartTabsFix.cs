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

                mainForm.fullScreenChartButton.Click += (_, _) =>
                {
                    if (mainForm.IsDisposed)
                        return;

                    mainForm.BeginInvoke(new Action(() => mainForm.RestoreChartTabsLayout()));
                };
            }
        }

        private void RestoreChartTabsLayout()
        {
            if (IsDisposed || chartPanel == null || chartTabControl == null || chartToolbarPanel == null)
                return;

            chartPanel.SuspendLayout();
            try
            {
                // Dock order is important: the toolbar consumes the top area,
                // and the TabControl fills the remaining area including its tab headers.
                chartPanel.Controls.SetChildIndex(chartToolbarPanel, 0);
                chartPanel.Controls.SetChildIndex(chartTabControl, 1);

                chartToolbarPanel.Dock = DockStyle.Top;
                chartTabControl.Dock = DockStyle.Fill;
                chartToolbarPanel.Visible = true;
                chartTabControl.Visible = true;

                chartPanel.PerformLayout();
                chartTabControl.PerformLayout();
                chartTabControl.BringToFront();
                chartToolbarPanel.BringToFront();
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
