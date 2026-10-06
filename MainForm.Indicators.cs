namespace Trade.It
{
    public partial class MainForm
    {
        private void InitializeIndicatorMenuRuntime()
        {
            indicatorMaMenuItem.Click += (_, _) => AddMovingAverageToActiveChart();
            indicatorEmaMenuItem.Click += (_, _) => AddExponentialMovingAverageToActiveChart();
            indicatorRemoveMaMenuItem.Click += (_, _) => RemoveOneIndicatorFromActiveChart(ChartIndicatorType.MovingAverage);
            indicatorRemoveEmaMenuItem.Click += (_, _) => RemoveOneIndicatorFromActiveChart(ChartIndicatorType.ExponentialMovingAverage);
            indicatorClearMenuItem.Click += (_, _) => ClearIndicatorsFromActiveChart();
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

        private void RemoveOneIndicatorFromActiveChart(ChartIndicatorType type)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            if (!chart.RemoveOneIndicator(type))
            {
                var name = type == ChartIndicatorType.MovingAverage ? "MA" : "EMA";
                MessageBox.Show(this, $"هیچ اندیکاتور {name} فعالی وجود ندارد.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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