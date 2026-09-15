using System.Drawing;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartTabCloseHandlerInitialized;

        private void MainForm_HandleCreatedForChartTabs(object? sender, EventArgs e)
        {
            if (chartTabCloseHandlerInitialized ||
                System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            chartTabCloseHandlerInitialized = true;
            chartTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            chartTabControl.DrawItem += ChartTabControl_DrawItem;
            chartTabControl.MouseDown += ChartTabControl_MouseDownForClose;
            chartTabControl.Invalidate();

            BeginInvoke(new Action(InitializeChartToolbarStateFixes));
        }

        private void ChartTabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= chartTabControl.TabPages.Count)
                return;

            var page = chartTabControl.TabPages[e.Index];
            var bounds = chartTabControl.GetTabRect(e.Index);
            var selected = e.Index == chartTabControl.SelectedIndex;

            using var backgroundBrush = new SolidBrush(selected ? SystemColors.Window : SystemColors.Control);
            using var textBrush = new SolidBrush(SystemColors.ControlText);
            using var closePen = new Pen(SystemColors.ControlText, 1.5f);

            e.Graphics.FillRectangle(backgroundBrush, bounds);

            const int closeSize = 8;
            const int closeMargin = 6;
            var closeRect = new Rectangle(
                bounds.Right - closeMargin - closeSize,
                bounds.Top + (bounds.Height - closeSize) / 2,
                closeSize,
                closeSize);

            e.Graphics.DrawString(
                page.Text,
                e.Font,
                textBrush,
                new Rectangle(bounds.Left + 6, bounds.Top, Math.Max(1, bounds.Width - 24), bounds.Height),
                new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                });

            e.Graphics.DrawLine(closePen, closeRect.Left, closeRect.Top, closeRect.Right, closeRect.Bottom);
            e.Graphics.DrawLine(closePen, closeRect.Right, closeRect.Top, closeRect.Left, closeRect.Bottom);
        }

        private void ChartTabControl_MouseDownForClose(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            for (var i = 0; i < chartTabControl.TabPages.Count; i++)
            {
                var bounds = chartTabControl.GetTabRect(i);
                const int closeSize = 8;
                const int closeMargin = 6;
                var closeRect = new Rectangle(
                    bounds.Right - closeMargin - closeSize,
                    bounds.Top + (bounds.Height - closeSize) / 2,
                    closeSize + 4,
                    closeSize + 4);

                if (closeRect.Contains(e.Location))
                {
                    CloseSingleChartTab(i);
                    return;
                }
            }
        }

        private void CloseSingleChartTab(int index)
        {
            if (index < 0 || index >= chartTabControl.TabPages.Count)
                return;

            var page = chartTabControl.TabPages[index];
            var chart = page.Controls.OfType<TradingChartControl>().FirstOrDefault();
            var wasSelected = ReferenceEquals(page, chartTabControl.SelectedTab);

            if (chart != null)
            {
                foreach (var item in chartControls.Where(x => ReferenceEquals(x.Value, chart)).ToList())
                    chartControls.Remove(item.Key);

                page.Controls.Remove(chart);
                chart.Dispose();
            }

            if (ReferenceEquals(page, chartTabPage))
            {
                chartTabPage.Controls.Clear();
                chartTabPage.Controls.Add(chartInfoPanel);
                chartTabPage.Controls.Add(chartPlaceholderLabel);
                chartTabPage.Text = "چارت";
            }
            else
            {
                chartTabControl.TabPages.Remove(page);
                page.Dispose();
            }

            if (wasSelected)
            {
                var active = GetActiveChart();
                if (active != null)
                {
                    activeChartSymbol = chartTabControl.SelectedTab?.Text?.Trim();
                    chartInfoLabel.Text = $"{activeChartSymbol}   |   چارت باز است";
                    chartPlaceholderLabel.Visible = false;
                }
                else
                {
                    activeChartSymbol = null;
                    chartInfoLabel.Text = "هنوز سهمی برای نمایش انتخاب نشده است.";
                    chartPlaceholderLabel.Visible = true;
                }
            }

            chartTabControl.Invalidate();
            SyncChartToolbarFromActiveChart();
        }
    }
}
