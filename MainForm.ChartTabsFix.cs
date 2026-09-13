namespace Trade.It
{
    public partial class MainForm
    {
        private static readonly bool chartTabsLayoutFixInitialized = InitializeChartTabsLayoutFix();

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

                AttachFullscreenChartTabsFix(mainForm);
            }
        }

        private static readonly HashSet<MainForm> chartTabsFixAttachedForms = new();

        private static void AttachFullscreenChartTabsFix(MainForm mainForm)
        {
            if (!chartTabsFixAttachedForms.Add(mainForm))
                return;

            mainForm.fullScreenChartButton.Click += (_, _) =>
            {
                if (mainForm.IsDisposed)
                    return;

                mainForm.BeginInvoke(new Action(() => RestoreChartTabsAfterFullscreen(mainForm)));
            };
        }

        private static void RestoreChartTabsAfterFullscreen(MainForm mainForm)
        {
            if (mainForm.IsDisposed || mainForm.chartPanel == null || mainForm.chartTabControl == null)
                return;

            mainForm.chartPanel.SuspendLayout();
            try
            {
                mainForm.chartTabControl.Dock = DockStyle.Fill;
                mainForm.chartTabControl.Visible = true;
                mainForm.chartTabControl.BringToFront();
                mainForm.chartTabControl.PerformLayout();
                mainForm.chartPanel.PerformLayout();
            }
            finally
            {
                mainForm.chartPanel.ResumeLayout(true);
            }

            mainForm.chartTabControl.Invalidate(true);
            mainForm.chartTabControl.Update();
        }
    }
}
