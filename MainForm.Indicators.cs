namespace Trade.It
{
    public partial class MainForm
    {
        private TradingChartControl? indicatorContextChart;

        private void InitializeIndicatorMenuRuntime()
        {
            indicatorMaMenuItem.Click += (_, _) => AddMovingAverageToActiveChart();
            indicatorEmaMenuItem.Click += (_, _) => AddExponentialMovingAverageToActiveChart();
            indicatorIchimokuMenuItem.Click += (_, _) => AddIchimokuToActiveChart();
            indicatorClearMenuItem.Click += (_, _) => ClearIndicatorsFromActiveChart();

            indicatorContextMenuStrip.Opening += IndicatorContextMenuStrip_Opening;
            indicatorDeleteMenuItem.Click += (_, _) => DeleteContextIndicator();
            indicatorSettingsMenuItem.Click += (_, _) => OpenIndicatorSettings();
            indicatorPeriod5MenuItem.Click += (_, _) => SetContextIndicatorPeriod(5);
            indicatorPeriod10MenuItem.Click += (_, _) => SetContextIndicatorPeriod(10);
            indicatorPeriod20MenuItem.Click += (_, _) => SetContextIndicatorPeriod(20);
            indicatorPeriod50MenuItem.Click += (_, _) => SetContextIndicatorPeriod(50);
            indicatorPeriod100MenuItem.Click += (_, _) => SetContextIndicatorPeriod(100);
            indicatorPeriod200MenuItem.Click += (_, _) => SetContextIndicatorPeriod(200);
        }

        private void AddMovingAverageToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddMovingAverage(20);
        }

        private void AddIchimokuToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddIchimoku();
        }

        private void AddExponentialMovingAverageToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddExponentialMovingAverage(20);
        }

        private void IndicatorContextMenuStrip_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            indicatorContextChart = indicatorContextMenuStrip.SourceControl as TradingChartControl;
            e.Cancel = indicatorContextChart == null ||
                       indicatorContextChart.SelectedIndicatorIndex < 0 ||
                       indicatorContextChart.SelectedIndicatorIndex >= indicatorContextChart.Indicators.Count;

            if (e.Cancel)
                return;

            var period = indicatorContextChart!.Indicators[indicatorContextChart.SelectedIndicatorIndex].Period;
            indicatorPeriod5MenuItem.Checked = period == 5;
            indicatorPeriod10MenuItem.Checked = period == 10;
            indicatorPeriod20MenuItem.Checked = period == 20;
            indicatorPeriod50MenuItem.Checked = period == 50;
            indicatorPeriod100MenuItem.Checked = period == 100;
            indicatorPeriod200MenuItem.Checked = period == 200;
        }

        private void OpenIndicatorSettings()
        {
            var chart = indicatorContextChart;
            if (chart == null)
                return;

            var index = chart.SelectedIndicatorIndex;
            if (index < 0 || index >= chart.Indicators.Count)
                return;

            if (chart.Indicators[index].Type == ChartIndicatorType.Ichimoku)
            {
                using var dialog = new IchimokuSettingsForm(chart.Indicators[index]);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyIchimokuSettings(
                        index,
                        dialog.ShowTenkan,
                        dialog.ShowKijun,
                        dialog.ShowSpanA,
                        dialog.ShowSpanB,
                        dialog.ShowChikou,
                        dialog.ShowBullishCloud,
                        dialog.ShowBearishCloud,
                        dialog.TenkanColor,
                        dialog.KijunColor,
                        dialog.SpanAColor,
                        dialog.SpanBColor,
                        dialog.ChikouColor,
                        dialog.BullishCloudColor,
                        dialog.BearishCloudColor);
                }
            }
            else
            {
                using var dialog = new IndicatorSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyIndicatorSettings(index, dialog.Period, dialog.LineColor, dialog.BackgroundColor);
            }

            indicatorContextChart = null;
        }

        private void DeleteContextIndicator()
        {
            var chart = indicatorContextChart;
            if (chart == null)
                return;

            chart.RemoveIndicatorAt(chart.SelectedIndicatorIndex);
            indicatorContextChart = null;
        }

        private void SetContextIndicatorPeriod(int period)
        {
            var chart = indicatorContextChart;
            if (chart == null)
                return;

            chart.SetIndicatorPeriod(chart.SelectedIndicatorIndex, period);
            indicatorContextChart = null;
        }

        private void ClearIndicatorsFromActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Indicators.Count == 0)
                return;

            var result = MessageBox.Show(
                this,
                "همه اندیکاتورهای چارت فعال حذف شوند؟",
                "حذف همه اندیکاتورها",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
                chart.RemoveAllIndicators();
        }
    }
}