namespace Trade.It
{
    public partial class MainForm
    {
        private TradingChartControl? indicatorContextChart;

        private void InitializeIndicatorMenuRuntime()
        {
            indicatorMaMenuItem.Click += (_, _) => AddMovingAverageToActiveChart();
            indicatorEmaMenuItem.Click += (_, _) => AddExponentialMovingAverageToActiveChart();
            indicatorClearMenuItem.Click += (_, _) => ClearIndicatorsFromActiveChart();

            indicatorContextMenuStrip.Opening += IndicatorContextMenuStrip_Opening;
            indicatorDeleteMenuItem.Click += (_, _) => DeleteContextIndicator();
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