namespace Trade.It
{
    public partial class MainForm
    {
        private void RestoreChartTabsLayout()
        {
            if (IsDisposed || chartPanel == null || chartTabControl == null || chartToolbarPanel == null)
                return;

            chartPanel.SuspendLayout();
            try
            {
                // Dock order is important here: the toolbar consumes the top area,
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
