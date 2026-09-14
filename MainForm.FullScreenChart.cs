namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartFullScreen;
        private Control? chartFullScreenOriginalParent;
        private int chartFullScreenOriginalIndex;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // MainForm.cs wires the original click handler in the constructor.
            // Replace it after the form is fully created so fullscreen owns the
            // chart host itself instead of fighting SplitContainer's docking.
            fullScreenChartButton.Click -= FullScreenChartButton_Click;
            fullScreenChartButton.Click += FullScreenChartButton_ReparentedClick;
        }

        private void FullScreenChartButton_ReparentedClick(object? sender, EventArgs e)
        {
            if (!chartFullScreen)
                EnterChartFullScreen();
            else
                ExitChartFullScreen();
        }

        private void EnterChartFullScreen()
        {
            if (chartFullScreen || chartPanel.IsDisposed)
                return;

            chartFullScreenOriginalParent = chartPanel.Parent;
            chartFullScreenOriginalIndex = chartFullScreenOriginalParent?.Controls.GetChildIndex(chartPanel) ?? -1;

            chartFullScreen = true;
            fullScreenChartButton.Text = "بازگشت";

            // Remove the chart host from SplitContainer.Panel2 before collapsing
            // Panel1. The chart host then becomes a direct child of MainForm and
            // fills the entire client area, including its TabControl header.
            chartPanel.Parent = this;
            chartPanel.Dock = DockStyle.Fill;
            chartPanel.BringToFront();

            mainSplitContainer.Panel1Collapsed = true;
            chartPanel.BringToFront();
            chartPanel.PerformLayout();
            chartTabControl.PerformLayout();
            chartTabControl.Invalidate(true);
        }

        private void ExitChartFullScreen()
        {
            if (!chartFullScreen)
                return;

            // Restore the splitter first, then put the chart host back into its
            // original panel. This avoids leaving the chart controls detached.
            mainSplitContainer.Panel1Collapsed = false;

            var originalParent = chartFullScreenOriginalParent;
            if (originalParent != null && !originalParent.IsDisposed)
            {
                chartPanel.Parent = originalParent;
                chartPanel.Dock = DockStyle.Fill;

                if (chartFullScreenOriginalIndex >= 0 && chartFullScreenOriginalIndex < originalParent.Controls.Count)
                    originalParent.Controls.SetChildIndex(chartPanel, chartFullScreenOriginalIndex);
            }

            chartFullScreenOriginalParent = null;
            chartFullScreenOriginalIndex = -1;
            chartFullScreen = false;
            fullScreenChartButton.Text = "تمام صفحه";

            mainSplitContainer.PerformLayout();
            mainSplitContainer.Panel2.PerformLayout();
            chartPanel.PerformLayout();
            chartTabControl.PerformLayout();
            chartTabControl.Invalidate(true);
        }
    }
}
