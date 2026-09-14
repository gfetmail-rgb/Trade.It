namespace Trade.It
{
    public partial class MainForm
    {
        private static readonly HashSet<MainForm> fullScreenFixAttachedForms = new();
        private static readonly bool fullScreenFixInitialized = InitializeFullScreenFix();

        private static bool InitializeFullScreenFix()
        {
            Application.Idle += FullScreenFix_Idle;
            return true;
        }

        private static void FullScreenFix_Idle(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is not MainForm mainForm || mainForm.IsDisposed)
                    continue;

                if (!fullScreenFixAttachedForms.Add(mainForm))
                    continue;

                // MainForm already attaches FullScreenChartButton_Click in its
                // constructor. Replace that handler with the layout-safe version.
                mainForm.fullScreenChartButton.Click -= mainForm.FullScreenChartButton_Click;
                mainForm.fullScreenChartButton.Click += mainForm.FullScreenChartButton_FixedClick;
                mainForm.NormalizeChartTabsLayout();
            }
        }

        private void FullScreenChartButton_FixedClick(object? sender, EventArgs e)
        {
            mainSplitContainer.Panel1Collapsed = !mainSplitContainer.Panel1Collapsed;
            fullScreenChartButton.Text = mainSplitContainer.Panel1Collapsed ? "بازگشت" : "تمام صفحه";

            // Let SplitContainer finish its own collapse/restore first. Then
            // restore the known-good parent layout once, without subscribing to
            // Layout/SizeChanged events that can fight the WinForms layout engine.
            if (IsDisposed || !IsHandleCreated)
                return;

            BeginInvoke(new Action(NormalizeChartTabsLayout));
        }

        private void NormalizeChartTabsLayout()
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            mainSplitContainer.PerformLayout();
            mainSplitContainer.Panel2.PerformLayout();

            chartPanel.Dock = DockStyle.Fill;
            chartToolbarPanel.Dock = DockStyle.Top;
            chartTabControl.Dock = DockStyle.Fill;
            chartToolbarPanel.Visible = true;
            chartTabControl.Visible = true;

            // Deterministic z-order: toolbar is the top docked region and the
            // TabControl occupies the remaining region below it.
            chartPanel.Controls.SetChildIndex(chartToolbarPanel, 0);
            chartPanel.Controls.SetChildIndex(chartTabControl, 1);

            chartPanel.PerformLayout();
            chartTabControl.PerformLayout();
            chartTabControl.Invalidate(true);
        }
    }
}
