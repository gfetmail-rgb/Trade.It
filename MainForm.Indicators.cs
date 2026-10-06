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
            indicatorStochasticRsiMenuItem.Click += (_, _) => AddStochasticRsiToActiveChart();
            indicatorAtrMenuItem.Click += (_, _) => AddAtrToActiveChart();
            indicatorAdxMenuItem.Click += (_, _) => AddAdxToActiveChart();
            indicatorBollingerMenuItem.Click += (_, _) => AddBollingerToActiveChart();
            indicatorObvMenuItem.Click += (_, _) => AddObvToActiveChart();
            indicatorClearMenuItem.Click += (_, _) => ClearIndicatorsFromActiveChart();

            indicatorContextMenuStrip.Opening += IndicatorContextMenuStrip_Opening;
            indicatorDeleteMenuItem.Click += (_, _) => DeleteContextIndicator();
            indicatorSettingsMenuItem.Click += (_, _) => OpenIndicatorSettings();
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

        private void AddStochasticRsiToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chart.AddStochasticRelativeStrengthIndex();
        }

        private void AddAtrToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddAverageTrueRange(14);
        }

        private void AddBollingerToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chart.AddBollingerBands(20, 2.0);
        }

        private void AddObvToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chart.AddOnBalanceVolume();
        }

        private void AddAdxToActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Points.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال باز کنید.", "اندیکاتورها",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chart.AddAverageDirectionalIndex(14);
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
        }

        private void OpenIndicatorSettings()
        {
            var chart = indicatorContextChart;
            if (chart == null)
                return;

            var index = chart.SelectedIndicatorIndex;
            if (index < 0 || index >= chart.Indicators.Count)
                return;

            if (chart.Indicators[index].Type == ChartIndicatorType.StochasticRelativeStrengthIndex)
            {
                using var dialog = new StochasticRsiSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyStochasticRsiSettings(index, dialog.RsiPeriod, dialog.StochasticPeriod, dialog.KPeriod, dialog.DPeriod, dialog.ShowK, dialog.ShowD, dialog.Show20, dialog.Show80, dialog.KColor, dialog.DColor, dialog.C20Color, dialog.C80Color);
            }
            else if (chart.Indicators[index].Type == ChartIndicatorType.Stochastic)
            {
                using var dialog = new StochasticSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyStochasticSettings(index, dialog.Period, dialog.KPeriod, dialog.DPeriod, dialog.ShowK, dialog.ShowD, dialog.Show20, dialog.Show80, dialog.KColor, dialog.DColor, dialog.C20Color, dialog.C80Color);
            }
            else             if (chart.Indicators[index].Type == ChartIndicatorType.MovingAverageConvergenceDivergence)
            {
                using var dialog = new MacdSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyMacdSettings(
                        index,
                        dialog.FastPeriod,
                        dialog.SlowPeriod,
                        dialog.SignalPeriod,
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
                using var dialog = new IchimokuSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyIchimokuSettings(
                        index,
                        dialog.TenkanPeriod,
                        dialog.KijunPeriod,
                        dialog.SpanBPeriod,
                        dialog.Displacement,
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
            else if (chart.Indicators[index].Type == ChartIndicatorType.BollingerBands)
            {
                using var dialog = new BollingerSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyBollingerSettings(index, dialog.Period, dialog.StandardDeviation, dialog.ShowMiddle, dialog.ShowUpper, dialog.ShowLower, dialog.MiddleColor, dialog.UpperColor, dialog.LowerColor);
            }
            else if (chart.Indicators[index].Type == ChartIndicatorType.OnBalanceVolume)
            {
                using var dialog = new ObvSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    chart.ApplyObvSettings(index, dialog.ShowLine, dialog.ShowZero, dialog.LineColor, dialog.ZeroColor);
            }
            else if (chart.Indicators[index].Type == ChartIndicatorType.AverageDirectionalIndex)
            {
                using var dialog = new AdxSettingsForm(chart.Indicators[index], chart.Points.Count);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    chart.ApplyAdxSettings(
                        index,
                        dialog.Period,
                        dialog.ShowAdxLine,
                        dialog.ShowAdxPlusDi,
                        dialog.ShowAdxMinusDi,
                        dialog.ShowAdx25,
                        dialog.AdxLineColor,
                        dialog.AdxPlusDiColor,
                        dialog.AdxMinusDiColor,
                        dialog.Adx25Color);
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