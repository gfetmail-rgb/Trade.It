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

                if (mainForm.chartPanel == null || mainForm.chartTabControl == null || mainForm.chartToolbarPanel == null)
                    continue;

                mainForm.chartTabControl.Visible = true;
                mainForm.chartTabControl.Top = mainForm.chartToolbarPanel.Bottom;
                mainForm.chartTabControl.Left = 0;
                mainForm.chartTabControl.Width = mainForm.chartPanel.ClientSize.Width;
                mainForm.chartTabControl.Height = Math.Max(0, mainForm.chartPanel.ClientSize.Height - mainForm.chartToolbarPanel.Height);
                mainForm.chartTabControl.BringToFront();
                mainForm.chartToolbarPanel.BringToFront();
            }
        }
    }
}
