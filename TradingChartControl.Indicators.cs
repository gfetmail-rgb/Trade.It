using System.Globalization;
namespace Trade.It
{
    internal enum ChartIndicatorType
    {
        MovingAverage,
        ExponentialMovingAverage,
        Ichimoku,
        RelativeStrengthIndex,
        MovingAverageConvergenceDivergence,
        Stochastic,
        StochasticRelativeStrengthIndex,
        AverageTrueRange,
        AverageDirectionalIndex,
        BollingerBands,
        OnBalanceVolume,
        VolumeWeightedAveragePrice,
        DonchianChannel,
        AwesomeOscillator

    internal sealed class ChartIndicator
    {
        public ChartIndicatorType Type { get; init; }
        public int Period { get; set; }
        public bool IsVisible { get; set; } = true;
        public Color LineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color BackgroundColor { get; set; } = Color.White;
        public bool ShowTenkan { get; set; } = false;
        public bool ShowKijun { get; set; } = true;
        public bool ShowSpanA { get; set; } = false;
        public bool ShowSpanB { get; set; } = false;
        public bool ShowChikou { get; set; } = false;
        public bool ShowBullishCloud { get; set; } = true;
        public bool ShowBearishCloud { get; set; } = true;
        public Color TenkanColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color KijunColor { get; set; } = Color.FromArgb(80, 100, 220);
        public Color SpanAColor { get; set; } = Color.FromArgb(50, 150, 80);
        public Color SpanBColor { get; set; } = Color.FromArgb(180, 100, 60);
        public Color ChikouColor { get; set; } = Color.FromArgb(150, 80, 180);
        public Color BullishCloudColor { get; set; } = Color.FromArgb(130, 200, 130);
        public Color BearishCloudColor { get; set; } = Color.FromArgb(230, 150, 150);
        public int IchimokuTenkanPeriod { get; set; } = 9;
        public int IchimokuKijunPeriod { get; set; } = 26;
        public int IchimokuSpanBPeriod { get; set; } = 52;
        public int IchimokuDisplacement { get; set; } = 26;
        public bool ShowRsiLine { get; set; } = true;
        public bool ShowRsi30 { get; set; } = true;
        public bool ShowRsi70 { get; set; } = true;
        public Color RsiLineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color Rsi30Color { get; set; } = Color.FromArgb(150, 150, 150);
        public Color Rsi70Color { get; set; } = Color.FromArgb(150, 150, 150);
        public bool ShowMacdLine { get; set; } = true;
        public bool ShowMacdSignal { get; set; } = true;
        public bool ShowMacdHistogram { get; set; } = true;
        public bool ShowMacdZero { get; set; } = true;
        public Color MacdLineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color MacdSignalColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color MacdBullishHistogramColor { get; set; } = Color.FromArgb(80, 170, 100);
        public Color MacdBearishHistogramColor { get; set; } = Color.FromArgb(210, 100, 100);
        public Color MacdZeroColor { get; set; } = Color.FromArgb(150, 150, 150);
        public int MacdFastPeriod { get; set; } = 12;
        public int MacdSlowPeriod { get; set; } = 26;
        public int MacdSignalPeriod { get; set; } = 9;
        public int AwesomeOscillatorFastPeriod { get; set; } = 5;
        public int AwesomeOscillatorSlowPeriod { get; set; } = 34;
        public Color AwesomeOscillatorRisingColor { get; set; } = Color.FromArgb(80, 170, 100);
        public Color AwesomeOscillatorFallingColor { get; set; } = Color.FromArgb(210, 100, 100);
        public bool ShowStochasticK { get; set; } = true;
        public bool ShowStochasticD { get; set; } = true;
        public bool ShowStochastic20 { get; set; } = true;
        public bool ShowStochastic80 { get; set; } = true;
        public Color StochasticKColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color StochasticDColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color Stochastic20Color { get; set; } = Color.FromArgb(150, 150, 150);
        public Color Stochastic80Color { get; set; } = Color.FromArgb(150, 150, 150);
        public int StochasticPeriod { get; set; } = 13;
        public int StochasticKPeriod { get; set; } = 8;
        public int StochasticDPeriod { get; set; } = 5;
        public bool ShowStochasticRsiK { get; set; } = true;
        public bool ShowStochasticRsiD { get; set; } = true;
        public bool ShowStochasticRsi20 { get; set; } = true;
        public bool ShowStochasticRsi80 { get; set; } = true;
        public Color StochasticRsiKColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color StochasticRsiDColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color StochasticRsi20Color { get; set; } = Color.FromArgb(150, 150, 150);
        public Color StochasticRsi80Color { get; set; } = Color.FromArgb(150, 150, 150);
        public bool ShowBollingerMiddle { get; set; } = true;
        public bool ShowBollingerUpper { get; set; } = true;
        public bool ShowBollingerLower { get; set; } = true;
        public double BollingerStdDev { get; set; } = 2.0;
        public Color BollingerMiddleColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color BollingerUpperColor { get; set; } = Color.FromArgb(50, 160, 80);
        public Color BollingerLowerColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color BollingerBandColor { get; set; } = Color.FromArgb(150, 150, 150);
        public Color ObvLineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public bool ShowObvLine { get; set; } = true;
        public bool ShowObvZero { get; set; } = true;
        public Color ObvZeroColor { get; set; } = Color.FromArgb(150, 150, 150);
        public Color VwapLineColor { get; set; } = Color.FromArgb(180, 90, 30);
        public bool ShowAdxLine { get; set; } = true;
        public bool ShowAdxPlusDi { get; set; } = true;
        public bool ShowAdxMinusDi { get; set; } = true;
        public bool ShowAdx25 { get; set; } = true;
        public Color AdxLineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color AdxPlusDiColor { get; set; } = Color.FromArgb(50, 160, 80);
        public Color AdxMinusDiColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color Adx25Color { get; set; } = Color.FromArgb(150, 150, 150);
        public int StochasticRsiRsiPeriod { get; set; } = 21;
        public int StochasticRsiPeriod { get; set; } = 13;
        public int StochasticRsiKPeriod { get; set; } = 8;
        public int StochasticRsiDPeriod { get; set; } = 5;
    }

    internal sealed partial class TradingChartControl
    {
        private readonly List<ChartIndicator> indicators = new();
        private int selectedIndicatorIndex = -1;

        // Ichimoku values are independent of the current viewport. Cache them so
        // panning/zooming only redraws the existing values instead of recalculating
        // all 9/26/52-period windows on every Paint.
        private double[]? ichimokuTenkanCache;
        private double[]? ichimokuKijunCache;
        private double[]? ichimokuSpanACache;
        private double[]? ichimokuSpanBCache;
        private double[]? ichimokuChikouCache;
        private double[]? macdLineCache;
        private double[]? macdSignalCache;
        private double[]? macdHistogramCache;
        private double[]? awesomeOscillatorCache;
        private double[]? stochasticKCache;
        private double[]? stochasticDCache;
        private double[]? stochasticRsiKCache;
        private double[]? stochasticRsiDCache;
        private double[]? atrCache;
        private double[]? adxCache;
        private double[]? adxPlusDiCache;
        private double[]? adxMinusDiCache;
        private double[]? bollingerMiddleCache;
        private double[]? bollingerUpperCache;
        private double[]? bollingerLowerCache;
        private double[]? obvCache;
        private double[]? vwapCache;

        public IReadOnlyList<ChartIndicator> Indicators => indicators;

        private bool IsPriceOverlayIndicator(ChartIndicator indicator) =>
            indicator.Type == ChartIndicatorType.MovingAverage ||
            indicator.Type == ChartIndicatorType.ExponentialMovingAverage ||
            indicator.Type == ChartIndicatorType.DonchianChannel ||
            indicator.Type == ChartIndicatorType.Ichimoku ||
            indicator.Type == ChartIndicatorType.BollingerBands ||
            indicator.Type == ChartIndicatorType.VolumeWeightedAveragePrice;

        private string GetPriceOverlayIndicatorTitle(ChartIndicator indicator) =>
            indicator.Type switch
            {
                ChartIndicatorType.MovingAverage => $"MA({indicator.Period})",
                ChartIndicatorType.ExponentialMovingAverage => $"EMA({indicator.Period})",
                ChartIndicatorType.DonchianChannel => $"Donchian({indicator.Period})",
                ChartIndicatorType.Ichimoku => $"Ichimoku({indicator.IchimokuTenkanPeriod},{indicator.IchimokuKijunPeriod},{indicator.IchimokuSpanBPeriod},{indicator.IchimokuDisplacement})",
                ChartIndicatorType.BollingerBands => $"BB({indicator.Period},{indicator.BollingerStdDev:0.##})",
                ChartIndicatorType.VolumeWeightedAveragePrice => "VWAP",
                _ => string.Empty
            };

        private Color GetPriceOverlayIndicatorTitleColor(ChartIndicator indicator) =>
            indicator.Type switch
            {
                ChartIndicatorType.BollingerBands => indicator.BollingerMiddleColor,
                ChartIndicatorType.VolumeWeightedAveragePrice => indicator.VwapLineColor,
                _ => indicator.LineColor
            };

        private void SyncPriceIndicatorButtons()
        {
            var plot = GetPlotRectangle();
            var font = new Font(Font.FontFamily, Math.Max(9f, Font.Size), FontStyle.Bold);
            var buttonHeight = Math.Max(34, font.Height + 12);
            try
            {
                var items = indicators
                    .Where(IsPriceOverlayIndicator)
                    .Select(indicator => new
                    {
                        Indicator = indicator,
                        Title = GetPriceOverlayIndicatorTitle(indicator)
                    })
                    .OrderBy(item => item.Title, StringComparer.OrdinalIgnoreCase)
                    .Take(priceIndicatorButtons.Length)
                    .ToList();

                var y = plot.Top + 2;
                for (var i = 0; i < priceIndicatorButtons.Length; i++)
                {
                    var button = priceIndicatorButtons[i];
                    if (i >= items.Count)
                    {
                        button.Tag = null;
                        button.Text = string.Empty;
                        button.Visible = false;
                        continue;
                    }

                    var item = items[i];
                    var indicator = item.Indicator;
                    button.Tag = indicator;
                    button.Text = item.Title;
                    if (!button.Font.Equals(font))
                        button.Font = (Font)font.Clone();

                    var textWidth = TextRenderer.MeasureText(button.Text, button.Font).Width;
                    button.Width = Math.Clamp(textWidth + 20, 90, 320);
                    button.Height = buttonHeight;
                    button.Location = new Point(Math.Max(plot.Left + 2, plot.Right - button.Width - 3), y);
                    button.ContextMenuStrip = ContextMenuStrip;
                    button.Visible = true;
                    button.BringToFront();
                    UpdatePriceIndicatorButtonStyle(button, indicator);
                    y += button.Height + 3;
                }
            }
            finally
            {
                font.Dispose();
            }
        }

        private void PriceIndicatorButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button || button.Tag is not ChartIndicator indicator ||
                !indicators.Contains(indicator))
                return;

            indicator.IsVisible = !indicator.IsVisible;
            UpdatePriceIndicatorButtonStyle(button, indicator);
            Invalidate();
        }

        private void UpdatePriceIndicatorButtonStyle(Button button, ChartIndicator indicator)
        {
            var titleColor = GetPriceOverlayIndicatorTitleColor(indicator);
            button.ForeColor = titleColor;
            button.BackColor = indicator.IsVisible
                ? (BackColor == Color.Transparent ? Color.White : BackColor)
                : Color.FromArgb(232, 232, 232);
            button.FlatAppearance.BorderColor = indicator.IsVisible
                ? Color.FromArgb(160, titleColor)
                : Color.DarkGray;
        }

        internal bool HasRsiIndicator => indicators.Any(x => x.Type == ChartIndicatorType.RelativeStrengthIndex);
        internal bool HasMacdIndicator => indicators.Any(x => x.Type == ChartIndicatorType.MovingAverageConvergenceDivergence);
        internal bool HasAwesomeOscillator => indicators.Any(x => x.Type == ChartIndicatorType.AwesomeOscillator);
        internal bool HasStochasticIndicator => indicators.Any(x => x.Type == ChartIndicatorType.Stochastic);
        internal bool HasStochasticRsiIndicator => indicators.Any(x => x.Type == ChartIndicatorType.StochasticRelativeStrengthIndex);
        internal bool HasAtrIndicator => indicators.Any(x => x.Type == ChartIndicatorType.AverageTrueRange);
        internal bool HasAdxIndicator => indicators.Any(x => x.Type == ChartIndicatorType.AverageDirectionalIndex);
        internal bool HasObvIndicator => indicators.Any(x => x.Type == ChartIndicatorType.OnBalanceVolume);
        internal bool HasBollingerIndicator => indicators.Any(x => x.Type == ChartIndicatorType.BollingerBands);
        internal bool HasVwapIndicator => indicators.Any(x => x.Type == ChartIndicatorType.VolumeWeightedAveragePrice);
        public int SelectedIndicatorIndex => selectedIndicatorIndex;

        internal bool SelectIndicator(ChartIndicator indicator)
        {
            var index = indicators.IndexOf(indicator);
            if (index < 0)
                return false;

            selectedIndicatorIndex = index;
            return true;
        }

        public void AddMovingAverage(int period = 21)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.MovingAverage,
                Period = period,
                BackgroundColor = BackColor
            });
            Invalidate();
        }

        public void AddExponentialMovingAverage(int period = 21)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.ExponentialMovingAverage,
                Period = period,
                BackgroundColor = BackColor
            });
            Invalidate();
        }

        public void AddRelativeStrengthIndex(int period = 13)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count - 1));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.RelativeStrengthIndex,
                Period = period,
                RsiLineColor = Color.FromArgb(30, 100, 220),
                Rsi30Color = Color.FromArgb(150, 150, 150),
                Rsi70Color = Color.FromArgb(150, 150, 150)
            });
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyRsiSettings(
            int index,
            int period,
            bool showRsiLine,
            bool showRsi30,
            bool showRsi70,
            Color rsiLineColor,
            Color rsi30Color,
            Color rsi70Color)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.RelativeStrengthIndex ||
                points.Count < 2)
                return false;

            var indicator = indicators[index];
            indicator.Period = Math.Clamp(period, 2, Math.Max(2, points.Count - 1));
            indicator.ShowRsiLine = showRsiLine;
            indicator.ShowRsi30 = showRsi30;
            indicator.ShowRsi70 = showRsi70;
            indicator.RsiLineColor = rsiLineColor;
            indicator.Rsi30Color = rsi30Color;
            indicator.Rsi70Color = rsi70Color;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddDonchianChannel(int period = 21)
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.DonchianChannel,
                Period = Math.Clamp(period, 2, Math.Max(2, points.Count)),
                LineColor = Color.FromArgb(50, 160, 80)
            });
            selectedIndicatorIndex = indicators.Count - 1;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddBollingerBands(int period = 21, double standardDeviation = 2.0)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.BollingerBands,
                Period = period,
                BollingerStdDev = Math.Clamp(standardDeviation, 0.1, 10.0)
            });
            InvalidateBollingerCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyBollingerSettings(int index, int period, double standardDeviation, bool showMiddle, bool showUpper, bool showLower, Color middleColor, Color upperColor, Color lowerColor)
        {
            if (index < 0 || index >= indicators.Count || indicators[index].Type != ChartIndicatorType.BollingerBands || points.Count < 2)
                return false;
            var indicator = indicators[index];
            indicator.Period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicator.BollingerStdDev = Math.Clamp(standardDeviation, 0.1, 10.0);
            indicator.ShowBollingerMiddle = showMiddle;
            indicator.ShowBollingerUpper = showUpper;
            indicator.ShowBollingerLower = showLower;
            indicator.BollingerMiddleColor = middleColor;
            indicator.BollingerUpperColor = upperColor;
            indicator.BollingerLowerColor = lowerColor;
            selectedIndicatorIndex = index;
            InvalidateBollingerCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddOnBalanceVolume()
        {
            indicators.Add(new ChartIndicator { Type = ChartIndicatorType.OnBalanceVolume });
            InvalidateObvCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddVolumeWeightedAveragePrice()
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.VolumeWeightedAveragePrice,
                VwapLineColor = Color.FromArgb(180, 90, 30)
            });
            InvalidateVwapCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyObvSettings(int index, bool showLine, bool showZero, Color lineColor, Color zeroColor)
        {
            if (index < 0 || index >= indicators.Count || indicators[index].Type != ChartIndicatorType.OnBalanceVolume || points.Count < 2)
                return false;
            var indicator = indicators[index];
            indicator.ShowObvLine = showLine;
            indicator.ShowObvZero = showZero;
            indicator.ObvLineColor = lineColor;
            indicator.ObvZeroColor = zeroColor;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddAverageTrueRange(int period = 13)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.AverageTrueRange,
                Period = period,
                LineColor = Color.FromArgb(30, 100, 220),
                BackgroundColor = BackColor
            });
            atrCache = null;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddAverageDirectionalIndex(int period = 13)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count - 1));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.AverageDirectionalIndex,
                Period = period,
                AdxLineColor = Color.FromArgb(30, 100, 220),
                AdxPlusDiColor = Color.FromArgb(50, 160, 80),
                AdxMinusDiColor = Color.FromArgb(220, 80, 80),
                Adx25Color = Color.FromArgb(150, 150, 150)
            });
            InvalidateAdxCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyAdxSettings(
            int index,
            int period,
            bool showAdxLine,
            bool showAdxPlusDi,
            bool showAdxMinusDi,
            bool showAdx25,
            Color adxLineColor,
            Color adxPlusDiColor,
            Color adxMinusDiColor,
            Color adx25Color)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.AverageDirectionalIndex ||
                points.Count < 2)
                return false;

            var indicator = indicators[index];
            indicator.Period = Math.Clamp(period, 2, Math.Max(2, points.Count - 1));
            indicator.ShowAdxLine = showAdxLine;
            indicator.ShowAdxPlusDi = showAdxPlusDi;
            indicator.ShowAdxMinusDi = showAdxMinusDi;
            indicator.ShowAdx25 = showAdx25;
            indicator.AdxLineColor = adxLineColor;
            indicator.AdxPlusDiColor = adxPlusDiColor;
            indicator.AdxMinusDiColor = adxMinusDiColor;
            indicator.Adx25Color = adx25Color;
            selectedIndicatorIndex = index;
            InvalidateAdxCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddStochastic()
        {
            indicators.Add(new ChartIndicator { Type = ChartIndicatorType.Stochastic, Period = 13 });
            InvalidateStochasticCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyStochasticSettings(int index, int period, int kPeriod, int dPeriod, bool showK, bool showD, bool show20, bool show80, Color kColor, Color dColor, Color c20, Color c80)
        {
            if (index < 0 || index >= indicators.Count || indicators[index].Type != ChartIndicatorType.Stochastic || points.Count < 2) return false;
            var indicator = indicators[index];
            indicator.StochasticPeriod = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicator.StochasticKPeriod = Math.Clamp(kPeriod, 1, Math.Max(1, points.Count));
            indicator.StochasticDPeriod = Math.Clamp(dPeriod, 1, Math.Max(1, points.Count));
            indicator.ShowStochasticK = showK; indicator.ShowStochasticD = showD; indicator.ShowStochastic20 = show20; indicator.ShowStochastic80 = show80;
            indicator.StochasticKColor = kColor; indicator.StochasticDColor = dColor; indicator.Stochastic20Color = c20; indicator.Stochastic80Color = c80;
            selectedIndicatorIndex = index; InvalidateStochasticCache(); Invalidate(); AnalysisChanged?.Invoke(this, EventArgs.Empty); return true;
        }

        public void AddStochasticRelativeStrengthIndex()
        {
            indicators.Add(new ChartIndicator { Type = ChartIndicatorType.StochasticRelativeStrengthIndex, Period = 21 });
            InvalidateStochasticRsiCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyStochasticRsiSettings(int index, int rsiPeriod, int stochasticPeriod, int kPeriod, int dPeriod, bool showK, bool showD, bool show20, bool show80, Color kColor, Color dColor, Color c20, Color c80)
        {
            if (index < 0 || index >= indicators.Count || indicators[index].Type != ChartIndicatorType.StochasticRelativeStrengthIndex || points.Count < 2)
                return false;
            var indicator = indicators[index];
            indicator.StochasticRsiRsiPeriod = Math.Clamp(rsiPeriod, 2, Math.Max(2, points.Count - 1));
            indicator.StochasticRsiPeriod = Math.Clamp(stochasticPeriod, 2, Math.Max(2, points.Count));
            indicator.StochasticRsiKPeriod = Math.Clamp(kPeriod, 1, Math.Max(1, points.Count));
            indicator.StochasticRsiDPeriod = Math.Clamp(dPeriod, 1, Math.Max(1, points.Count));
            indicator.ShowStochasticRsiK = showK;
            indicator.ShowStochasticRsiD = showD;
            indicator.ShowStochasticRsi20 = show20;
            indicator.ShowStochasticRsi80 = show80;
            indicator.StochasticRsiKColor = kColor;
            indicator.StochasticRsiDColor = dColor;
            indicator.StochasticRsi20Color = c20;
            indicator.StochasticRsi80Color = c80;
            selectedIndicatorIndex = index;
            InvalidateStochasticRsiCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddMovingAverageConvergenceDivergence()
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.MovingAverageConvergenceDivergence,
                Period = 9
            });
            InvalidateMacdCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyMacdSettings(
            int index,
            int fastPeriod,
            int slowPeriod,
            int signalPeriod,
            bool showMacdLine,
            bool showMacdSignal,
            bool showMacdHistogram,
            bool showMacdZero,
            Color macdLineColor,
            Color macdSignalColor,
            Color macdBullishHistogramColor,
            Color macdBearishHistogramColor,
            Color macdZeroColor)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.MovingAverageConvergenceDivergence)
                return false;

            var indicator = indicators[index];
            indicator.MacdFastPeriod = Math.Clamp(fastPeriod, 2, Math.Max(2, points.Count));
            indicator.MacdSlowPeriod = Math.Clamp(slowPeriod, 2, Math.Max(2, points.Count));
            indicator.MacdSignalPeriod = Math.Clamp(signalPeriod, 2, Math.Max(2, points.Count));
            if (indicator.MacdFastPeriod >= indicator.MacdSlowPeriod)
                indicator.MacdFastPeriod = Math.Max(2, indicator.MacdSlowPeriod - 1);
            indicator.ShowMacdLine = showMacdLine;
            indicator.ShowMacdSignal = showMacdSignal;
            indicator.ShowMacdHistogram = showMacdHistogram;
            indicator.ShowMacdZero = showMacdZero;
            indicator.MacdLineColor = macdLineColor;
            indicator.MacdSignalColor = macdSignalColor;
            indicator.MacdBullishHistogramColor = macdBullishHistogramColor;
            indicator.MacdBearishHistogramColor = macdBearishHistogramColor;
            indicator.MacdZeroColor = macdZeroColor;
            selectedIndicatorIndex = index;
            InvalidateMacdCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void AddAwesomeOscillator()
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.AwesomeOscillator,
                AwesomeOscillatorFastPeriod = 5,
                AwesomeOscillatorSlowPeriod = 34
            });
            InvalidateAwesomeOscillatorCache();
            selectedIndicatorIndex = indicators.Count - 1;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddIchimoku()
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.Ichimoku,
                Period = 9,
                BackgroundColor = BackColor
            });
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool RemoveIndicatorAt(int index)
        {
            if (index < 0 || index >= indicators.Count)
                return false;

            indicators.RemoveAt(index);
            selectedIndicatorIndex = -1;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool SetIndicatorPeriod(int index, int period)
        {
            if (index < 0 || index >= indicators.Count || points.Count < 2)
                return false;

            period = Math.Clamp(period, 2, points.Count);
            indicators[index].Period = period;
            if (indicators[index].Type == ChartIndicatorType.AverageTrueRange)
                InvalidateAtrCache();
            if (indicators[index].Type == ChartIndicatorType.AverageDirectionalIndex)
                InvalidateAdxCache();
            if (indicators[index].Type == ChartIndicatorType.BollingerBands)
                InvalidateBollingerCache();
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool ApplyIndicatorSettings(int index, int period, Color lineColor, Color backgroundColor)
        {
            if (index < 0 || index >= indicators.Count || points.Count < 2)
                return false;

            period = Math.Clamp(period, 2, points.Count);

            // Transparent is not useful as a channel fill color.
            // If an older indicator instance contains Transparent, use the current chart background as fallback.
            if (backgroundColor == Color.Transparent)
                backgroundColor = BackColor == Color.Transparent
                    ? Color.White
                    : BackColor;

            var indicator = indicators[index];
            indicator.Period = period;
            indicator.LineColor = lineColor;
            indicator.BackgroundColor = backgroundColor;
            if (indicator.Type == ChartIndicatorType.AverageTrueRange)
                InvalidateAtrCache();
            if (indicator.Type == ChartIndicatorType.AverageDirectionalIndex)
                InvalidateAdxCache();
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool ApplyIchimokuSettings(
            int index,
            int tenkanPeriod,
            int kijunPeriod,
            int spanBPeriod,
            int displacement,
            bool showTenkan,
            bool showKijun,
            bool showSpanA,
            bool showSpanB,
            bool showChikou,
            bool showBullishCloud,
            bool showBearishCloud,
            Color tenkanColor,
            Color kijunColor,
            Color spanAColor,
            Color spanBColor,
            Color chikouColor,
            Color bullishCloudColor,
            Color bearishCloudColor)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.Ichimoku)
                return false;

            var indicator = indicators[index];
            indicator.IchimokuTenkanPeriod = Math.Clamp(tenkanPeriod, 2, Math.Max(2, points.Count));
            indicator.IchimokuKijunPeriod = Math.Clamp(kijunPeriod, 2, Math.Max(2, points.Count));
            indicator.IchimokuSpanBPeriod = Math.Clamp(spanBPeriod, 2, Math.Max(2, points.Count));
            indicator.IchimokuDisplacement = Math.Clamp(displacement, 1, Math.Max(1, points.Count));
            indicator.ShowTenkan = showTenkan;
            indicator.ShowKijun = showKijun;
            indicator.ShowSpanA = showSpanA;
            indicator.ShowSpanB = showSpanB;
            indicator.ShowChikou = showChikou;
            indicator.ShowBullishCloud = showBullishCloud;
            indicator.ShowBearishCloud = showBearishCloud;
            indicator.TenkanColor = tenkanColor;
            indicator.KijunColor = kijunColor;
            indicator.SpanAColor = spanAColor;
            indicator.SpanBColor = spanBColor;
            indicator.ChikouColor = chikouColor;
            indicator.BullishCloudColor = bullishCloudColor;
            indicator.BearishCloudColor = bearishCloudColor;
            selectedIndicatorIndex = index;
            InvalidateIchimokuCache();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void RemoveAllIndicators()
        {
            if (indicators.Count == 0)
                return;

            indicators.Clear();
            selectedIndicatorIndex = -1;

            // پاک‌سازی Cacheهای محاسباتی اندیکاتورهای حذف‌شده.
            InvalidateIchimokuCache();
            InvalidateMacdCache();
            InvalidateStochasticCache();
            InvalidateStochasticRsiCache();
            InvalidateAtrCache();
            InvalidateAdxCache();
            InvalidateBollingerCache();
            InvalidateObvCache();
            InvalidateVwapCache();

            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        internal int HitTestIndicator(Point location)
        {
            if (indicators.Count == 0 || points.Count == 0)
                return -1;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var displayedCount = endIndex - firstIndex;
            if (displayedCount <= 0)
                return -1;

            var visible = points.Skip(firstIndex).Take(displayedCount).ToList();
            if (visible.Count == 0)
                return -1;

            var adxPlot = GetAdxPlotRectangle();
            if (adxPlot != Rectangle.Empty && adxPlot.Contains(location))
            {
                selectedIndicatorIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.AverageDirectionalIndex);
                return selectedIndicatorIndex;
            }

            var atrPlot = GetAtrPlotRectangle();
            if (atrPlot != Rectangle.Empty && atrPlot.Contains(location))
            {
                selectedIndicatorIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.AverageTrueRange);
                return selectedIndicatorIndex;
            }

            var obvPlot = GetObvPlotRectangle();
            if (obvPlot != Rectangle.Empty && obvPlot.Contains(location))
            {
                selectedIndicatorIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.OnBalanceVolume);
                return selectedIndicatorIndex;
            }

            var stochasticRsiPlot = GetStochasticRsiPlotRectangle();
            if (stochasticRsiPlot != Rectangle.Empty && stochasticRsiPlot.Contains(location))
            {
                selectedIndicatorIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.StochasticRelativeStrengthIndex);
                return selectedIndicatorIndex;
            }

            var stochasticPlot = GetStochasticPlotRectangle();
            if (stochasticPlot != Rectangle.Empty && stochasticPlot.Contains(location))
            {
                selectedIndicatorIndex = HitTestStochastic(location, stochasticPlot, displayedCount, stochasticPlot.Width / (double)Math.Max(1, visibleCount), -stochasticPlot.Width * 0.25);
                return selectedIndicatorIndex;
            }

            var macdPlot = GetMacdPlotRectangle();
            if (macdPlot != Rectangle.Empty && macdPlot.Contains(location))
            {
                var macdIndex = HitTestMacd(location, macdPlot, displayedCount, macdPlot.Width / (double)Math.Max(1, visibleCount), -macdPlot.Width * 0.25);
                selectedIndicatorIndex = macdIndex;
                return selectedIndicatorIndex;
            }

            var awesomePlot = GetAwesomeOscillatorPlotRectangle();
            if (awesomePlot != Rectangle.Empty && awesomePlot.Contains(location))
            {
                selectedIndicatorIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.AwesomeOscillator);
                return selectedIndicatorIndex;
            }

            var rsiPlot = GetRsiPlotRectangle();
            if (rsiPlot != Rectangle.Empty && rsiPlot.Contains(location))
            {
                var rsiIndex = HitTestRsi(location, rsiPlot, displayedCount, step: rsiPlot.Width / (double)Math.Max(1, visibleCount), initialOffset: -rsiPlot.Width * 0.25);
                selectedIndicatorIndex = rsiIndex;
                return selectedIndicatorIndex;
            }

            if (!GetPlotRectangle().Contains(location))
            {
                selectedIndicatorIndex = -1;
                return -1;
            }

            GetVerticalRange(visible, out var min, out var max);
            var step = plot.Width / (double)Math.Max(1, visibleCount);
            var initialOffset = -plot.Width * 0.25;
            const double tolerance = 8.0;

            var bestIndex = -1;
            var bestDistance = double.MaxValue;

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type == ChartIndicatorType.RelativeStrengthIndex ||
                    indicator.Type == ChartIndicatorType.MovingAverageConvergenceDivergence ||
                    indicator.Type == ChartIndicatorType.Stochastic ||
                    indicator.Type == ChartIndicatorType.StochasticRelativeStrengthIndex ||
                    indicator.Type == ChartIndicatorType.AverageTrueRange ||
                    indicator.Type == ChartIndicatorType.AverageDirectionalIndex ||
                    indicator.Type == ChartIndicatorType.OnBalanceVolume ||
                    indicator.Type == ChartIndicatorType.AwesomeOscillator)
                    continue;

                if (indicator.Type == ChartIndicatorType.MovingAverageConvergenceDivergence || indicator.Type == ChartIndicatorType.Stochastic)
                    continue;

                if (indicator.Type == ChartIndicatorType.Ichimoku)
                {
                    if (HitTestIchimoku(location, indicator, plot, min, max, displayedCount, step, initialOffset, tolerance, out var ichimokuDistance))
                    {
                        if (ichimokuDistance < bestDistance)
                        {
                            bestDistance = ichimokuDistance;
                            bestIndex = indicatorIndex;
                        }
                    }
                    continue;
                }

                if (indicator.Type == ChartIndicatorType.DonchianChannel)
                {
                    CalculateDonchianChannel(indicator.Period, out var upper, out var lower, out var middle);
                    foreach (var seriesValues in new[] { upper, lower, middle })
                    {
                        PointF? previousPoint = null;
                        for (var i = 0; i < displayedCount; i++)
                        {
                            var absoluteIndex = firstIndex + i;
                            if (absoluteIndex < 0 || absoluteIndex >= seriesValues.Length || !double.IsFinite(seriesValues[absoluteIndex]))
                            {
                                previousPoint = null;
                                continue;
                            }

                            var current = new PointF(
                                (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                                (float)PriceToScreen(seriesValues[absoluteIndex], plot, min, max));
                            if (previousPoint.HasValue)
                            {
                                var distance = DistanceToIndicatorSegment(location, previousPoint.Value, current);
                                if (distance < bestDistance)
                                {
                                    bestDistance = distance;
                                    bestIndex = indicatorIndex;
                                }
                            }
                            previousPoint = current;
                        }
                    }
                    continue;
                }

                if (indicator.Type == ChartIndicatorType.BollingerBands)
                {
                    EnsureBollingerCache(indicator);
                    var series = new[] { bollingerMiddleCache!, bollingerUpperCache!, bollingerLowerCache! };
                    foreach (var seriesValues in series)
                    {
                        PointF? previousPoint = null;
                        for (var i = 0; i < displayedCount; i++)
                        {
                            var absoluteIndex = firstIndex + i;
                            if (absoluteIndex < 0 || absoluteIndex >= seriesValues.Length || double.IsNaN(seriesValues[absoluteIndex])) { previousPoint = null; continue; }
                            var current = new PointF(
                                (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                                (float)PriceToScreen(seriesValues[absoluteIndex], plot, min, max));
                            if (previousPoint.HasValue)
                            {
                                var distance = DistanceToIndicatorSegment(location, previousPoint.Value, current);
                                if (distance < bestDistance) { bestDistance = distance; bestIndex = indicatorIndex; }
                            }
                            previousPoint = current;
                        }
                    }
                    continue;
                }

                var values = indicator.Type == ChartIndicatorType.MovingAverage
                    ? CalculateMovingAverage(indicator.Period)
                    : CalculateExponentialMovingAverage(indicator.Period);

                PointF? previous = null;
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = new PointF(
                        (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                        (float)PriceToScreen(values[absoluteIndex], plot, min, max));

                    if (previous.HasValue)
                    {
                        var distance = DistanceToIndicatorSegment(location, previous.Value, current);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = indicatorIndex;
                        }
                    }

                    previous = current;
                }
            }

            selectedIndicatorIndex = bestDistance <= tolerance ? bestIndex : -1;
            return selectedIndicatorIndex;
        }

        private int HitTestStochastic(Point location, Rectangle plot, int displayedCount, double step, double initialOffset)
        {
            var index = indicators.FindIndex(x => x.Type == ChartIndicatorType.Stochastic);
            if (index < 0) return -1;
            return index;
        }

        private int HitTestMacd(Point location, Rectangle macdPlot, int displayedCount, double step, double initialOffset)
        {
            var bestIndex = -1;
            var bestDistance = 8.0;
            var macdIndicator = indicators.FirstOrDefault(i => i.Type == ChartIndicatorType.MovingAverageConvergenceDivergence);
            if (macdIndicator == null)
                return -1;
            EnsureMacdCache(macdIndicator);
            var maxAbs = GetMacdScaleMax(macdLineCache!, macdSignalCache!, macdHistogramCache!, firstIndex, displayedCount);

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type != ChartIndicatorType.MovingAverageConvergenceDivergence)
                    continue;

                if (indicator.ShowMacdLine)
                    CheckLine(macdLineCache!, indicatorIndex);
                if (indicator.ShowMacdSignal)
                    CheckLine(macdSignalCache!, indicatorIndex);

                if (indicator.ShowMacdHistogram)
                {
                    var zeroY = MacdValueToScreen(0, macdPlot, maxAbs);
                    for (var i = 0; i < displayedCount; i++)
                    {
                        var absoluteIndex = firstIndex + i;
                        if (absoluteIndex < 0 || absoluteIndex >= macdHistogramCache!.Length ||
                            double.IsNaN(macdHistogramCache[absoluteIndex]))
                            continue;

                        var x = (float)(macdPlot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                        var y = MacdValueToScreen(macdHistogramCache[absoluteIndex], macdPlot, maxAbs);
                        var distance = Math.Abs(location.X - x);
                        if (location.Y >= Math.Min(y, zeroY) &&
                            location.Y <= Math.Max(y, zeroY) &&
                            distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = indicatorIndex;
                        }
                    }
                }

                if (indicator.ShowMacdZero)
                {
                    var zeroY = MacdValueToScreen(0, macdPlot, maxAbs);
                    var distance = Math.Abs(location.Y - zeroY);
                    if (distance <= bestDistance)
                    {
                        bestDistance = distance;
                        bestIndex = indicatorIndex;
                    }
                }
            }

            return bestIndex >= 0
                ? bestIndex
                : indicators.FindIndex(x => x.Type == ChartIndicatorType.MovingAverageConvergenceDivergence);

            void CheckLine(double[] values, int index)
            {
                PointF? previous = null;
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = new PointF(
                        (float)(macdPlot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                        (float)MacdValueToScreen(values[absoluteIndex], macdPlot, maxAbs));

                    if (previous.HasValue)
                    {
                        var distance = DistanceToIndicatorSegment(location, previous.Value, current);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = index;
                        }
                    }

                    previous = current;
                }
            }
        }

        private int HitTestRsi(Point location, Rectangle rsiPlot, int displayedCount, double step, double initialOffset)
        {
            var bestIndex = -1;
            var bestDistance = 8.0;

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type != ChartIndicatorType.RelativeStrengthIndex)
                    continue;

                var values = CalculateRsi(indicator.Period);
                PointF? previous = null;
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = RsiValueToScreen(values[absoluteIndex], rsiPlot, step, i, initialOffset);
                    if (previous.HasValue)
                    {
                        var distance = DistanceToIndicatorSegment(location, previous.Value, current);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = indicatorIndex;
                        }
                    }

                    if (indicator.ShowRsi30)
                    {
                        var y30 = RsiValueToScreen(30, rsiPlot, step, i, initialOffset).Y;
                        if (Math.Abs(location.Y - y30) <= bestDistance)
                        {
                            bestDistance = Math.Abs(location.Y - y30);
                            bestIndex = indicatorIndex;
                        }
                    }

                    if (indicator.ShowRsi70)
                    {
                        var y70 = RsiValueToScreen(70, rsiPlot, step, i, initialOffset).Y;
                        if (Math.Abs(location.Y - y70) <= bestDistance)
                        {
                            bestDistance = Math.Abs(location.Y - y70);
                            bestIndex = indicatorIndex;
                        }
                    }

                    previous = current;
                }
            }

            if (bestIndex >= 0)
                return bestIndex;

            return indicators.FindIndex(x => x.Type == ChartIndicatorType.RelativeStrengthIndex);
        }

        private bool HitTestIchimoku(

            Point location,
            ChartIndicator indicator,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double tolerance,
            out double bestDistance)
        {
            var localBestDistance = double.MaxValue;
            EnsureIchimokuCache(indicator);
            var tenkan = ichimokuTenkanCache!;
            var kijun = ichimokuKijunCache!;
            var spanA = ichimokuSpanACache!;
            var spanB = ichimokuSpanBCache!;
            var chikou = ichimokuChikouCache!;

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= points.Count) continue;

                Check(tenkan, absoluteIndex, 0, indicator.ShowTenkan, indicator.TenkanColor);
                Check(kijun, absoluteIndex, 0, indicator.ShowKijun, indicator.KijunColor);
                Check(spanA, absoluteIndex, 26, indicator.ShowSpanA, indicator.SpanAColor);
                Check(spanB, absoluteIndex, 26, indicator.ShowSpanB, indicator.SpanBColor);
                Check(chikou, absoluteIndex, -26, indicator.ShowChikou, indicator.ChikouColor);
                CheckCloud(absoluteIndex, spanA, spanB, indicator);
            }

            bestDistance = localBestDistance;
            return localBestDistance < double.MaxValue;

            void CheckCloud(int sourceIndex, double[] valuesA, double[] valuesB, ChartIndicator currentIndicator)
            {
                if (sourceIndex < 0 || sourceIndex + 1 >= valuesA.Length ||
                    double.IsNaN(valuesA[sourceIndex]) || double.IsNaN(valuesB[sourceIndex]) ||
                    double.IsNaN(valuesA[sourceIndex + 1]) || double.IsNaN(valuesB[sourceIndex + 1]))
                    return;

                var bullish = valuesA[sourceIndex] >= valuesB[sourceIndex];
                if ((bullish && !currentIndicator.ShowBullishCloud) ||
                    (!bullish && !currentIndicator.ShowBearishCloud))
                    return;

                var screenIndex = sourceIndex - firstIndex + 26;
                var nextScreenIndex = screenIndex + 1;
                if (nextScreenIndex < 0 || screenIndex > displayedCount)
                    return;

                var polygon = new[]
                {
                    new PointF(
                        (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesA[sourceIndex], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesA[sourceIndex + 1], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesB[sourceIndex + 1], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesB[sourceIndex], plot, min, max))
                };

                if (PointInPolygon(location, polygon))
                    localBestDistance = 0;
            }

            void Check(double[] values, int sourceIndex, int shift, bool show, Color color)
            {
                if (!show || double.IsNaN(values[sourceIndex])) return;
                var screenIndex = sourceIndex - firstIndex + shift;
                var current = new PointF(
                    (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                    PriceToScreen(values[sourceIndex], plot, min, max));

                if (screenIndex < 0 || screenIndex > displayedCount) return;

                var distance = DistanceToIndicatorSegment(location, current, current);
                if (distance < localBestDistance) localBestDistance = distance;
            }
        }

        private static bool PointInPolygon(Point point, IReadOnlyList<PointF> polygon)
        {
            var inside = false;
            for (var i = 0; i < polygon.Count; i++)
            {
                var j = i == 0 ? polygon.Count - 1 : i - 1;
                var pi = polygon[i];
                var pj = polygon[j];

                if ((pi.Y > point.Y) != (pj.Y > point.Y) &&
                    point.X < (pj.X - pi.X) * (point.Y - pi.Y) / (pj.Y - pi.Y) + pi.X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        private static double DistanceToIndicatorSegment(Point p, PointF a, PointF b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            if (dx == 0 && dy == 0)
                return Math.Sqrt(Math.Pow(p.X - a.X, 2) + Math.Pow(p.Y - a.Y, 2));

            var t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0.0f, 1.0f);

            var x = a.X + t * dx;
            var y = a.Y + t * dy;
            return Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2));
        }

        private void DrawDonchianChannel(Graphics g, Rectangle plot, double min, double max, int displayedCount, double step, double initialOffset, ChartIndicator indicator)
        {
            CalculateDonchianChannel(indicator.Period, out var upper, out var lower, out var middle);

            // رنگ زمینهٔ دونچیان فقط داخل محدودهٔ کانال رسم می‌شود و به زمینهٔ چارت دست نمی‌زند.
            var upperPoints = new List<PointF>();
            var lowerPoints = new List<PointF>();
            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= upper.Length ||
                    double.IsNaN(upper[absoluteIndex]) || double.IsInfinity(upper[absoluteIndex]) ||
                    double.IsNaN(lower[absoluteIndex]) || double.IsInfinity(lower[absoluteIndex]))
                    continue;

                var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                upperPoints.Add(new PointF(x, (float)PriceToScreen(upper[absoluteIndex], plot, min, max)));
                lowerPoints.Add(new PointF(x, (float)PriceToScreen(lower[absoluteIndex], plot, min, max)));
            }

            if (upperPoints.Count > 1 && lowerPoints.Count == upperPoints.Count)
            {
                var polygon = upperPoints.Concat(lowerPoints.AsEnumerable().Reverse()).ToArray();
                using var fill = new SolidBrush(Color.FromArgb(45, indicator.BackgroundColor.R,
                    indicator.BackgroundColor.G, indicator.BackgroundColor.B));
                g.FillPolygon(fill, polygon);
            }

            using var pen = new Pen(indicator.LineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, upper, pen, true);
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, lower, pen, true);
            using var middlePen = new Pen(Color.FromArgb(150, 150, 150), Math.Max(1f, LineAppearanceSettings.ChartLineWidth));
            middlePen.DashPattern = new[] { 4f, 3f };
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, middle, middlePen, true);
        }

        private void DrawBollinger(Graphics g, Rectangle plot, double min, double max, int displayedCount, double step, double initialOffset, ChartIndicator indicator)
        {
            var middle = bollingerMiddleCache!;
            var upper = bollingerUpperCache!;
            var lower = bollingerLowerCache!;

            using var middlePen = new Pen(indicator.BollingerMiddleColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            using var upperPen = new Pen(Color.FromArgb(50, 160, 80), Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            using var lowerPen = new Pen(Color.FromArgb(220, 80, 80), Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, middle, middlePen, indicator.ShowBollingerMiddle);
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, upper, upperPen, indicator.ShowBollingerUpper);
            DrawSeries(g, plot, min, max, displayedCount, step, initialOffset, lower, lowerPen, indicator.ShowBollingerLower);
        }

        private void DrawBollingerFill(Graphics g, Rectangle plot, double min, double max, int displayedCount,
            double step, double initialOffset, double[] firstSeries, double[] secondSeries, Color color, bool show)
        {
            if (!show)
                return;

            var firstSegment = new List<PointF>();
            var secondSegment = new List<PointF>();

            void FillSegment()
            {
                if (firstSegment.Count >= 2 && firstSegment.Count == secondSegment.Count)
                {
                    var polygon = firstSegment.Concat(secondSegment.AsEnumerable().Reverse()).ToArray();
                    using var brush = new SolidBrush(Color.FromArgb(45, color.R, color.G, color.B));
                    g.FillPolygon(brush, polygon);
                }

                firstSegment.Clear();
                secondSegment.Clear();
            }

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= firstSeries.Length ||
                    absoluteIndex >= secondSeries.Length ||
                    !double.IsFinite(firstSeries[absoluteIndex]) ||
                    !double.IsFinite(secondSeries[absoluteIndex]))
                {
                    FillSegment();
                    continue;
                }

                var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                firstSegment.Add(new PointF(x, (float)PriceToScreen(firstSeries[absoluteIndex], plot, min, max)));
                secondSegment.Add(new PointF(x, (float)PriceToScreen(secondSeries[absoluteIndex], plot, min, max)));
            }

            FillSegment();
        }

        private void DrawSeries(Graphics g, Rectangle plot, double min, double max, int displayedCount, double step, double initialOffset, double[] values, Pen pen, bool show)
        {
            if (!show) return;
            PointF? prev=null;
            for(var i=0;i<displayedCount;i++)
            {
                var ai=firstIndex+i;
                if(ai<0||ai>=values.Length||double.IsNaN(values[ai])||double.IsInfinity(values[ai])){prev=null;continue;}
                var pt=new PointF((float)(plot.Left+step*(i+.5)+initialOffset+horizontalPanOffset),(float)PriceToScreen(values[ai],plot,min,max));
                if(prev.HasValue)g.DrawLine(pen,prev.Value,pt); prev=pt;
            }
        }

        private void DrawIndicators(
            Graphics g,
            Rectangle plot,
            int layoutCount,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset)
        {
            if (indicators.Count == 0 || displayedCount <= 0 || points.Count == 0)
                return;

            foreach (var indicator in indicators)
            {
                if (!indicator.IsVisible)
                    continue;

                if (indicator.Type == ChartIndicatorType.RelativeStrengthIndex || indicator.Type == ChartIndicatorType.MovingAverageConvergenceDivergence || indicator.Type == ChartIndicatorType.Stochastic || indicator.Type == ChartIndicatorType.StochasticRelativeStrengthIndex || indicator.Type == ChartIndicatorType.AverageTrueRange || indicator.Type == ChartIndicatorType.OnBalanceVolume || indicator.Type == ChartIndicatorType.AwesomeOscillator
                    continue;

                if (indicator.Type == ChartIndicatorType.DonchianChannel)
                {
                    DrawDonchianChannel(g, plot, min, max, displayedCount, step, initialOffset, indicator);
                    continue;
                }

                if (indicator.Type == ChartIndicatorType.BollingerBands)
                {
                    EnsureBollingerCache(indicator);
                    DrawBollinger(g, plot, min, max, displayedCount, step, initialOffset, indicator);
                    continue;
                }

                if (indicator.Type == ChartIndicatorType.VolumeWeightedAveragePrice)
                {
                    if (!points.Any(x => double.IsFinite(x.Volume) && x.Volume > 0))
                        continue;

                    EnsureVwapCache();
                    DrawVwap(g, plot, min, max, displayedCount, step, initialOffset, indicator);
                    continue;
                }

                if (indicator.Type == ChartIndicatorType.Ichimoku)
                {
                    DrawIchimoku(g, plot, min, max, displayedCount, step, initialOffset, indicator);
                    continue;
                }

                var values = indicator.Type == ChartIndicatorType.MovingAverage
                    ? CalculateMovingAverage(indicator.Period)
                    : CalculateExponentialMovingAverage(indicator.Period);

                using var pen = new Pen(indicator.LineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
                var linePoints = new List<PointF>();
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                        continue;
                    var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                    linePoints.Add(new PointF(x, PriceToScreen(values[absoluteIndex], plot, min, max)));
                }

                if (linePoints.Count > 1)
                    g.DrawLines(pen, linePoints.ToArray());


            }
        }

        private void DrawVwap(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            ChartIndicator indicator)
        {
            if (vwapCache == null || displayedCount <= 0)
                return;

            using var pen = new Pen(indicator.VwapLineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= vwapCache.Length ||
                    double.IsNaN(vwapCache[absoluteIndex]) || double.IsInfinity(vwapCache[absoluteIndex]))
                {
                    previous = null;
                    continue;
                }

                var current = new PointF(
                    (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                    (float)PriceToScreen(vwapCache[absoluteIndex], plot, min, max));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, current);

                previous = current;
            }

        }

        private void DrawIchimoku(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            ChartIndicator indicator)
        {
            EnsureIchimokuCache(indicator);
            var tenkan = ichimokuTenkanCache!;
            var kijun = ichimokuKijunCache!;
            var spanA = ichimokuSpanACache!;
            var spanB = ichimokuSpanBCache!;
            var chikou = ichimokuChikouCache!;

            var displacement = Math.Max(0, indicator.IchimokuDisplacement);

            DrawIchimokuCloud(g, plot, min, max, displayedCount, step, initialOffset,
                spanA, spanB, displacement, indicator.ShowBullishCloud, indicator.ShowBearishCloud,
                indicator.BullishCloudColor, indicator.BearishCloudColor);

            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, tenkan, 0, indicator.ShowTenkan, indicator.TenkanColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, kijun, 0, indicator.ShowKijun, indicator.KijunColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, spanA, displacement, indicator.ShowSpanA, indicator.SpanAColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, spanB, displacement, indicator.ShowSpanB, indicator.SpanBColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, chikou, -displacement, indicator.ShowChikou, indicator.ChikouColor);

        }

        private void DrawIchimokuLine(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double[] values,
            int shift,
            bool show,
            Color color)
        {
            if (!show) return;

            using var pen = new Pen(color, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;
            var lastSourceIndexExclusive = Math.Min(values.Length, firstIndex + displayedCount);

            // در Test Mode فقط داده‌های تا کندل مرجع مجازند؛
            // اما Spanها پس از انتقال زمانی می‌توانند در فضای خالی سمت راست دیده شوند.
            for (var sourceIndex = 0; sourceIndex < lastSourceIndexExclusive; sourceIndex++)
            {
                if (double.IsNaN(values[sourceIndex]))
                {
                    previous = null;
                    continue;
                }

                var screenIndex = sourceIndex - firstIndex + shift;
                if (screenIndex < -1)
                {
                    previous = null;
                    continue;
                }

                var point = new PointF(
                    (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                    PriceToScreen(values[sourceIndex], plot, min, max));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, point);

                previous = point;
            }
        }

        private void DrawIchimokuCloud(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double[] spanA,
            double[] spanB,
            int displacement,
            bool showBullish,
            bool showBearish,
            Color bullishColor,
            Color bearishColor)
        {
            var lastSourceIndexExclusive = Math.Min(points.Count, firstIndex + displayedCount);

            // در حالت عادی تا آخرین کندل داده و در Test Mode فقط تا کندل مرجع رسم می‌کنیم.
            // محدودیت انتهای محدوده نمایش کندل‌ها نباید ابر منتقل‌شده به آینده را حذف کند؛
            // Clip خود Plot بخش خارج از فضای رسم را کنترل می‌کند.
            for (var sourceIndex = 0; sourceIndex < lastSourceIndexExclusive - 1; sourceIndex++)
            {
                if (double.IsNaN(spanA[sourceIndex]) || double.IsNaN(spanB[sourceIndex]) ||
                    double.IsNaN(spanA[sourceIndex + 1]) || double.IsNaN(spanB[sourceIndex + 1]))
                    continue;

                var screenIndex = sourceIndex - firstIndex + displacement;
                var nextScreenIndex = screenIndex + 1;
                if (nextScreenIndex < 0)
                    continue;

                var bullish = spanA[sourceIndex] >= spanB[sourceIndex];
                if ((bullish && !showBullish) || (!bullish && !showBearish))
                    continue;

                var x1 = (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset);
                var x2 = (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset);
                var yA1 = PriceToScreen(spanA[sourceIndex], plot, min, max);
                var yB1 = PriceToScreen(spanB[sourceIndex], plot, min, max);
                var yA2 = PriceToScreen(spanA[sourceIndex + 1], plot, min, max);
                var yB2 = PriceToScreen(spanB[sourceIndex + 1], plot, min, max);

                using var brush = new SolidBrush(Color.FromArgb(45, bullish ? bullishColor : bearishColor));
                g.FillPolygon(brush, new[]
                {
                    new PointF(x1, yA1), new PointF(x2, yA2),
                    new PointF(x2, yB2), new PointF(x1, yB1)
                });
            }
        }

        private void EnsureIchimokuCache(ChartIndicator indicator)
        {
            if (ichimokuTenkanCache != null && ichimokuKijunCache != null &&
                ichimokuSpanACache != null && ichimokuSpanBCache != null &&
                ichimokuChikouCache != null && ichimokuTenkanCache.Length == points.Count &&
                ichimokuKijunCache.Length == points.Count && ichimokuSpanACache.Length == points.Count &&
                ichimokuSpanBCache.Length == points.Count && ichimokuChikouCache.Length == points.Count)
                return;

            ichimokuTenkanCache = CalculateIchimokuMidpoint(indicator.IchimokuTenkanPeriod);
            ichimokuKijunCache = CalculateIchimokuMidpoint(indicator.IchimokuKijunPeriod);
            ichimokuSpanACache = CalculateIchimokuSpanA(ichimokuTenkanCache, ichimokuKijunCache);
            ichimokuSpanBCache = CalculateIchimokuMidpoint(indicator.IchimokuSpanBPeriod);
            ichimokuChikouCache = points.Select(p => p.Close).ToArray();
        }

        internal void InvalidateIchimokuCache()
        {
            ichimokuTenkanCache = null;
            ichimokuKijunCache = null;
            ichimokuSpanACache = null;
            ichimokuSpanBCache = null;
            ichimokuChikouCache = null;
            InvalidateMacdCache();
        }

        private double[] CalculateIchimokuTenkan()
        {
            return CalculateIchimokuMidpoint(9);
        }

        private double[] CalculateIchimokuKijun()
        {
            return CalculateIchimokuMidpoint(26);
        }

        private double[] CalculateIchimokuSpanB()
        {
            return CalculateIchimokuMidpoint(52);
        }

        private double[] CalculateIchimokuMidpoint(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = period - 1; i < points.Count; i++)
            {
                var highest = double.MinValue;
                var lowest = double.MaxValue;
                for (var j = i - period + 1; j <= i; j++)
                {
                    highest = Math.Max(highest, points[j].High);
                    lowest = Math.Min(lowest, points[j].Low);
                }
                result[i] = (highest + lowest) / 2.0;
            }
            return result;
        }

        private double[] CalculateRsi(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count <= period)
                return result;

            double gainSum = 0;
            double lossSum = 0;
            for (var i = 1; i <= period; i++)
            {
                var change = points[i].Close - points[i - 1].Close;
                if (change >= 0)
                    gainSum += change;
                else
                    lossSum -= change;
            }

            var averageGain = gainSum / period;
            var averageLoss = lossSum / period;
            result[period] = RsiFromAverages(averageGain, averageLoss);

            for (var i = period + 1; i < points.Count; i++)
            {
                var change = points[i].Close - points[i - 1].Close;
                var gain = Math.Max(0, change);
                var loss = Math.Max(0, -change);
                averageGain = ((averageGain * (period - 1)) + gain) / period;
                averageLoss = ((averageLoss * (period - 1)) + loss) / period;
                result[i] = RsiFromAverages(averageGain, averageLoss);
            }

            return result;
        }

        private static double RsiFromAverages(double averageGain, double averageLoss)
        {
            if (averageLoss <= 1e-12)
                return averageGain <= 1e-12 ? 50.0 : 100.0;
            var relativeStrength = averageGain / averageLoss;
            return 100.0 - (100.0 / (1.0 + relativeStrength));
        }

        private static double RsiValueToScreenY(double value, Rectangle plot)
        {
            return plot.Bottom - (value / 100.0 * plot.Height);
        }

        private PointF RsiValueToScreen(double value, Rectangle plot, double step, int relativeIndex, double initialOffset)
        {
            var x = (float)(plot.Left + step * (relativeIndex + 0.5) + initialOffset + horizontalPanOffset);
            var y = (float)RsiValueToScreenY(value, plot);
            return new PointF(x, y);
        }

        private static double[] CalculateIchimokuSpanA(double[] tenkan, double[] kijun)
        {
            var result = Enumerable.Repeat(double.NaN, tenkan.Length).ToArray();
            for (var i = 0; i < result.Length; i++)
            {
                if (!double.IsNaN(tenkan[i]) && !double.IsNaN(kijun[i]))
                    result[i] = (tenkan[i] + kijun[i]) / 2.0;
            }
            return result;
        }

        private void EnsureStochasticRsiCache(ChartIndicator indicator)
        {
            if (stochasticRsiKCache != null && stochasticRsiDCache != null &&
                stochasticRsiKCache.Length == points.Count && stochasticRsiDCache.Length == points.Count)
                return;

            var rsiPeriod = indicator.StochasticRsiRsiPeriod;
            var stochasticPeriod = indicator.StochasticRsiPeriod;
            var smoothK = indicator.StochasticRsiKPeriod;
            var smoothD = indicator.StochasticRsiDPeriod;
            var rsi = CalculateRsi(rsiPeriod);
            var rawK = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            stochasticRsiKCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            stochasticRsiDCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();

            for (var i = stochasticPeriod - 1; i < points.Count; i++)
            {
                if (double.IsNaN(rsi[i])) continue;
                var highest = double.MinValue; var lowest = double.MaxValue; var valid = true;
                for (var j = i - stochasticPeriod + 1; j <= i; j++)
                {
                    if (double.IsNaN(rsi[j])) { valid = false; break; }
                    highest = Math.Max(highest, rsi[j]); lowest = Math.Min(lowest, rsi[j]);
                }
                if (valid) rawK[i] = highest - lowest <= 1e-12 ? 50.0 : 100.0 * (rsi[i] - lowest) / (highest - lowest);
            }
            for (var i = stochasticPeriod - 1 + smoothK - 1; i < points.Count; i++)
            {
                var sum = 0.0; var valid = true;
                for (var j = i - smoothK + 1; j <= i; j++) { if (double.IsNaN(rawK[j])) { valid = false; break; } sum += rawK[j]; }
                if (valid) stochasticRsiKCache[i] = sum / smoothK;
            }
            for (var i = stochasticPeriod - 1 + smoothK - 1 + smoothD - 1; i < points.Count; i++)
            {
                var sum = 0.0; var valid = true;
                for (var j = i - smoothD + 1; j <= i; j++) { if (double.IsNaN(stochasticRsiKCache[j])) { valid = false; break; } sum += stochasticRsiKCache[j]; }
                if (valid) stochasticRsiDCache[i] = sum / smoothD;
            }
        }

        private void EnsureStochasticCache(ChartIndicator indicator)
        {
            if (stochasticKCache != null && stochasticDCache != null &&
                stochasticKCache.Length == points.Count && stochasticDCache.Length == points.Count)
                return;

            var period = indicator.StochasticPeriod;
            var smoothK = indicator.StochasticKPeriod;
            var smoothD = indicator.StochasticDPeriod;

            var rawK = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            stochasticKCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            stochasticDCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();

            for (var i = period - 1; i < points.Count; i++)
            {
                var high = double.MinValue;
                var low = double.MaxValue;
                for (var j = i - period + 1; j <= i; j++)
                {
                    high = Math.Max(high, points[j].High);
                    low = Math.Min(low, points[j].Low);
                }

                rawK[i] = high - low <= 1e-12
                    ? 50.0
                    : 100.0 * (points[i].Close - low) / (high - low);
            }

            for (var i = period - 1 + smoothK - 1; i < points.Count; i++)
            {
                var sum = 0.0;
                for (var j = i - smoothK + 1; j <= i; j++)
                    sum += rawK[j];

                stochasticKCache[i] = sum / smoothK;
            }

            for (var i = period - 1 + smoothK - 1 + smoothD - 1; i < points.Count; i++)
            {
                var sum = 0.0;
                for (var j = i - smoothD + 1; j <= i; j++)
                    sum += stochasticKCache[j];

                stochasticDCache[i] = sum / smoothD;
            }
        }
        internal void InvalidateStochasticCache() { stochasticKCache = null; stochasticDCache = null; }

        internal void InvalidateStochasticRsiCache()
        {
            stochasticRsiKCache = null;
            stochasticRsiDCache = null;
        }

        private void EnsureAtrCache(ChartIndicator indicator)
        {
            if (atrCache != null && atrCache.Length == points.Count)
                return;
            atrCache = CalculateAverageTrueRange(indicator.Period);
        }

        internal void InvalidateAtrCache() => atrCache = null;

        private void EnsureAdxCache(ChartIndicator indicator)
        {
            if (adxCache != null && adxPlusDiCache != null && adxMinusDiCache != null &&
                adxCache.Length == points.Count && adxPlusDiCache.Length == points.Count &&
                adxMinusDiCache.Length == points.Count)
                return;

            CalculateAverageDirectionalIndex(
                indicator.Period,
                out adxCache,
                out adxPlusDiCache,
                out adxMinusDiCache);
        }

        internal void InvalidateAdxCache()
        {
            adxCache = null;
            adxPlusDiCache = null;
            adxMinusDiCache = null;
        }

        private void EnsureMacdCache(ChartIndicator indicator)
        {
            if (macdLineCache != null && macdSignalCache != null && macdHistogramCache != null &&
                macdLineCache.Length == points.Count && macdSignalCache.Length == points.Count &&
                macdHistogramCache.Length == points.Count)
                return;

            var fast = CalculateExponentialMovingAverage(indicator.MacdFastPeriod);
            var slow = CalculateExponentialMovingAverage(indicator.MacdSlowPeriod);
            macdLineCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = 0; i < points.Count; i++)
                if (!double.IsNaN(fast[i]) && !double.IsNaN(slow[i])) macdLineCache[i] = fast[i] - slow[i];

            macdSignalCache = CalculateExponentialMovingAverageSeries(macdLineCache, indicator.MacdSignalPeriod);
            macdHistogramCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = 0; i < points.Count; i++)
                if (!double.IsNaN(macdLineCache[i]) && !double.IsNaN(macdSignalCache[i])) macdHistogramCache[i] = macdLineCache[i] - macdSignalCache[i];
        }

        internal void InvalidateMacdCache()
        {
            InvalidateAwesomeOscillatorCache();
            InvalidateStochasticCache();
            InvalidateStochasticRsiCache();
            macdLineCache = null;
            macdSignalCache = null;
            macdHistogramCache = null;
        }

        private void EnsureAwesomeOscillatorCache(ChartIndicator indicator)
        {
            if (awesomeOscillatorCache != null && awesomeOscillatorCache.Length == points.Count)
                return;

            var fastPeriod = Math.Max(2, indicator.AwesomeOscillatorFastPeriod);
            var slowPeriod = Math.Max(fastPeriod + 1, indicator.AwesomeOscillatorSlowPeriod);
            var medianPrices = points.Select(point => (point.High + point.Low) / 2.0).ToArray();
            var fast = CalculateSimpleMovingAverageSeries(medianPrices, fastPeriod);
            var slow = CalculateSimpleMovingAverageSeries(medianPrices, slowPeriod);
            awesomeOscillatorCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = 0; i < points.Count; i++)
                if (double.IsFinite(fast[i]) && double.IsFinite(slow[i]))
                    awesomeOscillatorCache[i] = fast[i] - slow[i];
        }

        private static double[] CalculateSimpleMovingAverageSeries(double[] source, int period)
        {
            var result = Enumerable.Repeat(double.NaN, source.Length).ToArray();
            if (period <= 0 || source.Length < period)
                return result;

            var sum = 0.0;
            for (var i = 0; i < source.Length; i++)
            {
                sum += source[i];
                if (i >= period)
                    sum -= source[i - period];
                if (i >= period - 1)
                    result[i] = sum / period;
            }
            return result;
        }

        internal void InvalidateAwesomeOscillatorCache() => awesomeOscillatorCache = null;

        private static double[] CalculateExponentialMovingAverageSeries(double[] source, int period)
        {
            var result = Enumerable.Repeat(double.NaN, source.Length).ToArray();
            if (period <= 0 || source.Length < period)
                return result;

            var firstValid = Array.FindIndex(source, x => !double.IsNaN(x));
            if (firstValid < 0 || firstValid + period - 1 >= source.Length)
                return result;

            var sum = 0.0;
            for (var i = firstValid; i < firstValid + period; i++)
            {
                if (double.IsNaN(source[i]))
                    return result;
                sum += source[i];
            }

            var ema = sum / period;
            result[firstValid + period - 1] = ema;
            var multiplier = 2.0 / (period + 1.0);

            for (var i = firstValid + period; i < source.Length; i++)
            {
                if (double.IsNaN(source[i]))
                    continue;
                ema = ((source[i] - ema) * multiplier) + ema;
                result[i] = ema;
            }

            return result;
        }

        private static double GetMacdScaleMax(double[] line, double[] signal, double[] histogram, int firstIndex, int displayedCount)
        {
            var max = 0.0;
            var end = Math.Min(line.Length, firstIndex + displayedCount);
            for (var i = Math.Max(0, firstIndex); i < end; i++)
            {
                if (!double.IsNaN(line[i])) max = Math.Max(max, Math.Abs(line[i]));
                if (!double.IsNaN(signal[i])) max = Math.Max(max, Math.Abs(signal[i]));
                if (!double.IsNaN(histogram[i])) max = Math.Max(max, Math.Abs(histogram[i]));
            }

            return Math.Max(max * 1.15, 1e-9);
        }

        private static double MacdValueToScreen(double value, Rectangle plot, double maxAbs)
        {
            if (maxAbs <= 1e-12)
                return plot.Top + plot.Height / 2.0;

            return plot.Bottom - ((value + maxAbs) / (2.0 * maxAbs)) * plot.Height;
        }

        private double[] CalculateAverageTrueRange(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count < period + 1)
                return result;

            var trueRanges = new double[points.Count];
            trueRanges[0] = points[0].High - points[0].Low;
            for (var i = 1; i < points.Count; i++)
            {
                var high = points[i].High;
                var low = points[i].Low;
                var previousClose = points[i - 1].Close;
                trueRanges[i] = Math.Max(high - low,
                    Math.Max(Math.Abs(high - previousClose), Math.Abs(low - previousClose)));
            }

            var sum = 0.0;
            for (var i = 1; i <= period; i++)
                sum += trueRanges[i];

            result[period] = sum / period;

            for (var i = period + 1; i < points.Count; i++)
                result[i] = ((result[i - 1] * (period - 1)) + trueRanges[i]) / period;

            return result;
        }

        private void CalculateAverageDirectionalIndex(
            int period,
            out double[] adx,
            out double[] plusDi,
            out double[] minusDi)
        {
            adx = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            plusDi = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            minusDi = Enumerable.Repeat(double.NaN, points.Count).ToArray();

            if (period <= 0 || points.Count < (period * 2))
                return;

            var tr = new double[points.Count];
            var plusDm = new double[points.Count];
            var minusDm = new double[points.Count];

            for (var i = 1; i < points.Count; i++)
            {
                var high = points[i].High;
                var low = points[i].Low;
                var previousHigh = points[i - 1].High;
                var previousLow = points[i - 1].Low;
                var previousClose = points[i - 1].Close;

                tr[i] = Math.Max(
                    high - low,
                    Math.Max(Math.Abs(high - previousClose), Math.Abs(low - previousClose)));

                var upMove = high - previousHigh;
                var downMove = previousLow - low;
                plusDm[i] = upMove > downMove && upMove > 0 ? upMove : 0;
                minusDm[i] = downMove > upMove && downMove > 0 ? downMove : 0;
            }

            var smoothedTr = 0.0;
            var smoothedPlusDm = 0.0;
            var smoothedMinusDm = 0.0;

            for (var i = 1; i <= period; i++)
            {
                smoothedTr += tr[i];
                smoothedPlusDm += plusDm[i];
                smoothedMinusDm += minusDm[i];
            }

            var dx = Enumerable.Repeat(double.NaN, points.Count).ToArray();

            for (var i = period; i < points.Count; i++)
            {
                if (i > period)
                {
                    smoothedTr = smoothedTr - (smoothedTr / period) + tr[i];
                    smoothedPlusDm = smoothedPlusDm - (smoothedPlusDm / period) + plusDm[i];
                    smoothedMinusDm = smoothedMinusDm - (smoothedMinusDm / period) + minusDm[i];
                }

                if (smoothedTr <= 1e-12)
                {
                    plusDi[i] = 0;
                    minusDi[i] = 0;
                    dx[i] = 0;
                    continue;
                }

                plusDi[i] = 100.0 * smoothedPlusDm / smoothedTr;
                minusDi[i] = 100.0 * smoothedMinusDm / smoothedTr;

                var denominator = plusDi[i] + minusDi[i];
                dx[i] = denominator <= 1e-12
                    ? 0
                    : 100.0 * Math.Abs(plusDi[i] - minusDi[i]) / denominator;
            }

            var firstAdx = (period * 2) - 1;
            if (firstAdx >= points.Count)
                return;

            var dxSum = 0.0;
            for (var i = period; i < firstAdx + 1; i++)
                dxSum += double.IsNaN(dx[i]) ? 0 : dx[i];

            adx[firstAdx] = dxSum / period;

            for (var i = firstAdx + 1; i < points.Count; i++)
                adx[i] = ((adx[i - 1] * (period - 1)) + dx[i]) / period;
        }

        private void CalculateDonchianChannel(int period, out double[] upper, out double[] lower, out double[] middle)
        {
            upper = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            lower = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            middle = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));

            // The channel at candle i is calculated from the preceding N candles,
            // excluding candle i itself. This makes breakouts comparable to the
            // prior range and avoids including the breakout candle in its own channel.
            for (var i = period; i < points.Count; i++)
            {
                var highest = double.NegativeInfinity;
                var lowest = double.PositiveInfinity;
                for (var j = i - period; j < i; j++)
                {
                    if (double.IsFinite(points[j].High))
                        highest = Math.Max(highest, points[j].High);
                    if (double.IsFinite(points[j].Low))
                        lowest = Math.Min(lowest, points[j].Low);
                }

                if (double.IsFinite(highest) && double.IsFinite(lowest))
                {
                    upper[i] = highest;
                    lower[i] = lowest;
                    middle[i] = (highest + lowest) / 2.0;
                }
            }
        }

        private void EnsureBollingerCache(ChartIndicator indicator)
        {
            if (bollingerMiddleCache != null && bollingerUpperCache != null && bollingerLowerCache != null &&
                bollingerMiddleCache.Length == points.Count && bollingerUpperCache.Length == points.Count && bollingerLowerCache.Length == points.Count)
                return;
            bollingerMiddleCache = CalculateMovingAverage(indicator.Period);
            bollingerUpperCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            bollingerLowerCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = indicator.Period - 1; i < points.Count; i++)
            {
                var mean = bollingerMiddleCache[i];
                if (double.IsNaN(mean)) continue;
                var sum = 0.0;
                for (var j = i - indicator.Period + 1; j <= i; j++)
                {
                    var d = points[j].Close - mean;
                    sum += d * d;
                }
                var sd = Math.Sqrt(sum / indicator.Period);
                bollingerUpperCache[i] = mean + indicator.BollingerStdDev * sd;
                bollingerLowerCache[i] = mean - indicator.BollingerStdDev * sd;
            }
        }
        internal void InvalidateBollingerCache()
        {
            bollingerMiddleCache = null; bollingerUpperCache = null; bollingerLowerCache = null;
        }

        private void EnsureObvCache()
        {
            if (obvCache != null && obvCache.Length == points.Count) return;
            obvCache = new double[points.Count];
            if (points.Count == 0) return;
            obvCache[0] = 0;
            for (var i = 1; i < points.Count; i++)
            {
                obvCache[i] = obvCache[i - 1];
                if (points[i].Close > points[i - 1].Close) obvCache[i] += points[i].Volume;
                else if (points[i].Close < points[i - 1].Close) obvCache[i] -= points[i].Volume;
            }
        }
        internal void InvalidateObvCache() => obvCache = null;
        private void EnsureVwapCache()
        {
            if (vwapCache != null && vwapCache.Length == points.Count)
                return;

            vwapCache = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (points.Count == 0)
                return;

            var cumulativePriceVolume = 0.0;
            var cumulativeVolume = 0.0;
            DateTime currentSessionDate = points[0].Date.Date;

            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                if (point.Date.Date != currentSessionDate)
                {
                    cumulativePriceVolume = 0.0;
                    cumulativeVolume = 0.0;
                    currentSessionDate = point.Date.Date;
                }

                if (!double.IsFinite(point.Volume) || point.Volume <= 0)
                    continue;

                var typicalPrice = (point.High + point.Low + point.Close) / 3.0;
                if (!double.IsFinite(typicalPrice))
                    continue;

                cumulativePriceVolume += typicalPrice * point.Volume;
                cumulativeVolume += point.Volume;
                if (cumulativeVolume > 0)
                    vwapCache[i] = cumulativePriceVolume / cumulativeVolume;
            }
        }

        internal void InvalidateVwapCache() => vwapCache = null;

        private double[] CalculateMovingAverage(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count < period)
                return result;

            double sum = 0;
            for (var i = 0; i < points.Count; i++)
            {
                sum += points[i].Close;

                if (i >= period)
                    sum -= points[i - period].Close;

                if (i >= period - 1)
                    result[i] = sum / period;
            }

            return result;
        }

        private double[] CalculateExponentialMovingAverage(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count < period)
                return result;

            double sum = 0;
            for (var i = 0; i < period; i++)
                sum += points[i].Close;

            var ema = sum / period;
            result[period - 1] = ema;

            var multiplier = 2.0 / (period + 1.0);
            for (var i = period; i < points.Count; i++)
            {
                ema = ((points[i].Close - ema) * multiplier) + ema;
                result[i] = ema;
            }

            return result;
        }
    }
}