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
            indicatorRsiMenuItem.Click += (_, _) => AddRsiToActiveChart();
            indicatorMacdMenuItem.Click += (_, _) => AddMacdToActiveChart();
            indicatorStochasticMenuItem.Click += (_, _) => AddStochasticToActiveChart();
            indicatorClearMenuItem.Click += (_, _) => ClearIndicatorsFromActiveChart();

            indicatorContextMenuStrip.Opening += IndicatorContextMenuStrip_Opening;
            indicatorDeleteMenuItem.Click += (_, _) => DeleteContextIndicator();
            indicatorSettingsMenuItem.Click += (_, _) => OpenIndicatorSettings();
            indicatorPeriod9MenuItem.Click += (_, _) => SetContextIndicatorPeriod(9);
            indicatorPeriod13MenuItem.Click += (_, _) => SetContextIndicatorPeriod(13);
            indicatorPeriod21MenuItem.Click += (_, _) => SetContextIndicatorPeriod(21);
            indicatorPeriod34MenuItem.Click += (_, _) => SetContextIndicatorPeriod(34);
            indicatorPeriod55MenuItem.Click += (_, _) => SetContextIndicatorPeriod(55);
            indicatorPeriod89MenuItem.Click += (_, _) => SetContextIndicatorPeriod(89);
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

        private void AddRsiToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddRelativeStrengthIndex(14);
        }

        private void AddStochasticToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chart.AddStochastic();
        }

        private void AddMacdToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddMovingAverageConvergenceDivergence();
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

            var indicator = indicatorContextChart!.Indicators[indicatorContextChart.SelectedIndicatorIndex];
            var isIchimoku = indicator.Type == ChartIndicatorType.Ichimoku;
            var isRsi = indicator.Type == ChartIndicatorType.RelativeStrengthIndex;
            var isMacd = indicator.Type == ChartIndicatorType.MovingAverageConvergenceDivergence;
            var isStochastic = indicator.Type == ChartIndicatorType.Stochastic;

            indicatorPeriod9MenuItem.Visible = !isIchimoku && !isRsi && !isMacd && !isStochastic;
            indicatorPeriod13MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;
            indicatorPeriod21MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;
            indicatorPeriod34MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;
            indicatorPeriod55MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;
            indicatorPeriod89MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;
            indicatorPeriod200MenuItem.Visible = !isIchimoku && !isRsi && !isMacd;

            if (!isIchimoku)
            {
                indicatorPeriod9MenuItem.Checked = indicator.Period == 9;
                indicatorPeriod13MenuItem.Checked = indicator.Period == 13;
                indicatorPeriod21MenuItem.Checked = indicator.Period == 21;
                indicatorPeriod34MenuItem.Checked = indicator.Period == 34;
                indicatorPeriod55MenuItem.Checked = indicator.Period == 55;
                indicatorPeriod89MenuItem.Checked = indicator.Period == 89;
                indicatorPeriod200MenuItem.Checked = indicator.Period == 200;
            }
        }

        private void OpenIndicatorSettings()
        {
            var chart = indicatorContextChart;
            if (chart == null)
                return;

            var index = chart.SelectedIndicatorIndex;
            if (index < 0 || index >= chart.Indicators.Count)
                return;

            if (chart.Indicators[index].Type == ChartIndicatorType.Stochastic)
            {
                using var dialog = new StochasticSettingsForm(chart.Indicators[index]);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyStochasticSettings(index, dialog.ShowK, dialog.ShowD, dialog.Show20, dialog.Show80, dialog.KColor, dialog.DColor, dialog.C20Color, dialog.C80Color);
            }
            else             if (chart.Indicators[index].Type == ChartIndicatorType.MovingAverageConvergenceDivergence)
            {
                using var dialog = new MacdSettingsForm(chart.Indicators[index]);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyMacdSettings(
                        index,
                        dialog.ShowMacdLine,
                        dialog.ShowMacdSignal,
                        dialog.ShowMacdHistogram,
                        dialog.ShowMacdZero,
                        dialog.MacdLineColor,
                        dialog.MacdSignalColor,
                        dialog.MacdBullishHistogramColor,
                        dialog.MacdBearishHistogramColor,
                        dialog.MacdZeroColor);
                }
            }
            else if (chart.Indicators[index].Type == ChartIndicatorType.Ichimoku)
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
            else if (chart.Indicators[index].Type == ChartIndicatorType.RelativeStrengthIndex)
            {
                using var dialog = new RsiSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyRsiSettings(
                        index,
                        dialog.Period,
                        dialog.ShowRsiLine,
                        dialog.ShowRsi30,
                        dialog.ShowRsi70,
                        dialog.RsiLineColor,
                        dialog.Rsi30Color,
                        dialog.Rsi70Color);
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