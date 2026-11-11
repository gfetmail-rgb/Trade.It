using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private int onFuMoradiAnchorIndex = -1;
        private int onFuMoradiCachedAnchorIndex = -1;
        private int onFuMoradiCachedIndicatorIndex = -1;
        private List<TradingChartPoint> onFuMoradiForecast = new();
        private bool onFuMoradiClickCandidate;
        private Point onFuMoradiMouseDownPoint;

        private bool IsOnFuMoradiActive =>
            indicators.Any(x => x.Type == ChartIndicatorType.OnFuMoradi);

        public void ToggleOnFuMoradi()
        {
            var existingIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.OnFuMoradi);
            if (existingIndex >= 0)
            {
                RemoveIndicatorAt(existingIndex);
                onFuMoradiAnchorIndex = -1;
                onFuMoradiForecast.Clear();
                onFuMoradiClickCandidate = false;
                Invalidate();
                return;
            }

            if (points.Count < 20)
            {
                MessageBox.Show(
                    FindForm(),
                    "برای اجرای OnFuMoradi حداقل ۲۰ کندل معتبر لازم است.",
                    "OnFuMoradi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.OnFuMoradi,
                Period = 10,
                OnFuMoradiAlpha = 0.55,
                OnFuMoradiBeta = 0.25,
                OnFuMoradiGamma = 0.20,
                OnFuMoradiDelta = 0.35,
                OnFuMoradiEpsilon = 0.15
            });

            onFuMoradiAnchorIndex = testMode
                ? (testStartSelected ? testEndIndex : -1)
                : points.Count - 1;
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiCachedIndicatorIndex = -1;
            onFuMoradiForecast.Clear();
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        private ChartIndicator? GetOnFuMoradiIndicator()
        {
            return indicators.FirstOrDefault(x => x.Type == ChartIndicatorType.OnFuMoradi);
        }

        private int ResolveOnFuMoradiAnchor()
        {
            if (!IsOnFuMoradiActive || points.Count < 20)
                return -1;

            if (testMode)
                return testStartSelected
                    ? Math.Clamp(onFuMoradiAnchorIndex >= 0 ? onFuMoradiAnchorIndex : testEndIndex, 0, Math.Max(0, testEndIndex))
                    : -1;

            return Math.Clamp(
                onFuMoradiAnchorIndex >= 0 ? onFuMoradiAnchorIndex : points.Count - 1,
                0,
                points.Count - 1);
        }

        private void EnsureOnFuMoradiForecast()
        {
            var indicator = GetOnFuMoradiIndicator();
            var anchor = ResolveOnFuMoradiAnchor();
            if (indicator == null || anchor < 19)
            {
                onFuMoradiForecast.Clear();
                onFuMoradiCachedAnchorIndex = anchor;
                onFuMoradiCachedIndicatorIndex = -1;
                return;
            }

            var indicatorIndex = indicators.IndexOf(indicator);
            if (onFuMoradiCachedAnchorIndex == anchor &&
                onFuMoradiCachedIndicatorIndex == indicatorIndex &&
                onFuMoradiForecast.Count == Math.Clamp(indicator.Period, 1, 100))
                return;

            onFuMoradiForecast = CalculateOnFuMoradiForecast(anchor, indicator);
            onFuMoradiCachedAnchorIndex = anchor;
            onFuMoradiCachedIndicatorIndex = indicatorIndex;
        }

        private List<TradingChartPoint> CalculateOnFuMoradiForecast(int anchor, ChartIndicator indicator)
        {
            var result = new List<TradingChartPoint>();
            var count = Math.Clamp(indicator.Period, 1, 100);
            if (anchor < 19 || anchor >= points.Count)
                return result;

            // Only data at or before the selected candle is read. This is essential
            // for Test Mode and also prevents look-ahead when a historical candle is clicked.
            var lookback = Math.Min(30, anchor + 1);
            var first = anchor - lookback + 1;
            var lastClose = points[anchor].Close;
            if (!double.IsFinite(lastClose) || lastClose <= 0)
                return result;

            var logValues = new double[lookback];
            for (var i = 0; i < lookback; i++)
            {
                var close = points[first + i].Close;
                if (!double.IsFinite(close) || close <= 0)
                    return result;
                logValues[i] = Math.Log(close);
            }

            var meanX = (lookback - 1) / 2.0;
            var meanY = logValues.Average();
            var numerator = 0.0;
            var denominator = 0.0;
            for (var i = 0; i < lookback; i++)
            {
                var dx = i - meanX;
                numerator += dx * (logValues[i] - meanY);
                denominator += dx * dx;
            }
            var trend = denominator > 0 ? numerator / denominator : 0.0;

            var momentumCount = Math.Min(5, anchor);
            var momentum = 0.0;
            for (var i = anchor - momentumCount + 1; i <= anchor; i++)
            {
                var previous = points[i - 1].Close;
                var current = points[i].Close;
                if (previous > 0 && current > 0)
                    momentum += Math.Log(current / previous);
            }
            momentum = momentumCount > 0 ? momentum / momentumCount : 0.0;

            var emaPeriod = Math.Min(20, anchor + 1);
            var ema = points[anchor - emaPeriod + 1].Close;
            var emaMultiplier = 2.0 / (emaPeriod + 1.0);
            for (var i = anchor - emaPeriod + 2; i <= anchor; i++)
                ema += (points[i].Close - ema) * emaMultiplier;
            var meanReversion = Math.Log(Math.Max(1e-12, ema) / lastClose) / Math.Max(emaPeriod, 1);

            var atrStart = Math.Max(1, anchor - 13);
            var atrSum = 0.0;
            var atrSamples = 0;
            var upperWickSum = 0.0;
            var lowerWickSum = 0.0;
            var bodySum = 0.0;
            for (var i = atrStart; i <= anchor; i++)
            {
                var p = points[i];
                var previousClose = points[i - 1].Close;
                var trueRange = Math.Max(
                    p.High - p.Low,
                    Math.Max(Math.Abs(p.High - previousClose), Math.Abs(p.Low - previousClose)));
                if (double.IsFinite(trueRange) && trueRange >= 0)
                {
                    atrSum += trueRange;
                    atrSamples++;
                }

                var bodyTop = Math.Max(p.Open, p.Close);
                var bodyBottom = Math.Min(p.Open, p.Close);
                upperWickSum += Math.Max(0, p.High - bodyTop);
                lowerWickSum += Math.Max(0, bodyBottom - p.Low);
                bodySum += Math.Max(0, bodyTop - bodyBottom);
            }

            var atr = atrSamples > 0 ? atrSum / atrSamples : lastClose * 0.01;
            var volatility = Math.Clamp(atr / lastClose, 0.0001, 0.20);
            var samples = Math.Max(1, anchor - atrStart + 1);
            var upperWickRatio = Math.Clamp(upperWickSum / samples / Math.Max(atr, 1e-12), 0.05, 0.75);
            var lowerWickRatio = Math.Clamp(lowerWickSum / samples / Math.Max(atr, 1e-12), 0.05, 0.75);
            var bodyRatio = Math.Clamp(bodySum / samples / Math.Max(atr, 1e-12), 0.10, 0.90);

            var alpha = Math.Clamp(indicator.OnFuMoradiAlpha, 0, 2);
            var beta = Math.Clamp(indicator.OnFuMoradiBeta, 0, 2);
            var gamma = Math.Clamp(indicator.OnFuMoradiGamma, 0, 2);
            var delta = Math.Clamp(indicator.OnFuMoradiDelta, 0.05, 2);
            var epsilon = Math.Clamp(indicator.OnFuMoradiEpsilon, 0, 2);

            var previousCloseForecast = lastClose;
            var dateStep = anchor > 0
                ? points[anchor].Date - points[anchor - 1].Date
                : TimeSpan.FromDays(1);
            if (dateStep <= TimeSpan.Zero || dateStep > TimeSpan.FromDays(366))
                dateStep = TimeSpan.FromDays(1);

            for (var step = 1; step <= count; step++)
            {
                var decay = 1.0 / Math.Sqrt(step);
                var drift = (alpha * trend + beta * momentum + gamma * meanReversion) * decay;

                // Limit each projected return to a volatility-scaled range.
                drift = Math.Clamp(drift, -volatility * delta, volatility * delta);
                var open = previousCloseForecast;
                var close = open * Math.Exp(drift);

                // A small candle-shape component carries recent average body/wick proportions forward.
                var expectedRange = Math.Max(atr * delta, open * volatility * 0.35);
                var body = Math.Abs(close - open);
                var residualRange = Math.Max(0, expectedRange - body);
                var patternBias = Math.Clamp(epsilon * (upperWickRatio - lowerWickRatio), -0.5, 0.5);
                var upperWick = residualRange * Math.Clamp(upperWickRatio + patternBias * 0.25, 0.05, 0.80);
                var lowerWick = residualRange * Math.Clamp(lowerWickRatio - patternBias * 0.25, 0.05, 0.80);
                var high = Math.Max(open, close) + upperWick;
                var low = Math.Max(1e-12, Math.Min(open, close) - lowerWick);

                result.Add(new TradingChartPoint
                {
                    Date = points[anchor].Date.AddTicks(dateStep.Ticks * step),
                    HasRealDate = points[anchor].HasRealDate,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = 0
                });

                previousCloseForecast = close;
                // Reduce trend persistence as the horizon grows; no future observations are used.
                trend *= 0.94;
                momentum *= 0.82;
                meanReversion *= 0.90;
            }

            return result;
        }

        private void ExpandPriceRangeForOnFuMoradi(ref double min, ref double max)
        {
            EnsureOnFuMoradiForecast();
            var anchor = ResolveOnFuMoradiAnchor();
            if (onFuMoradiForecast.Count == 0 ||
                anchor < firstIndex ||
                anchor >= firstIndex + Math.Max(1, visibleCount))
                return;

            foreach (var candle in onFuMoradiForecast)
            {
                min = Math.Min(min, candle.Low);
                max = Math.Max(max, candle.High);
            }

            var padding = Math.Max((max - min) * 0.04, Math.Abs(max) * 0.0001);
            min -= padding;
            max += padding;
        }

        private void RenderOnFuMoradiForecast(
            Graphics graphics,
            Rectangle plot,
            int layoutCount,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset)
        {
            EnsureOnFuMoradiForecast();
            if (onFuMoradiForecast.Count == 0 || plot.Width <= 0 || max <= min)
                return;

            var anchor = ResolveOnFuMoradiAnchor();
            var localAnchor = anchor - firstIndex;
            if (localAnchor < 0 || localAnchor >= displayedCount)
                return;

            using var bullishFill = new SolidBrush(Color.FromArgb(90, 45, 145, 190));
            using var bearishFill = new SolidBrush(Color.FromArgb(90, 205, 85, 85));
            using var bullishOutline = new Pen(Color.FromArgb(210, 30, 115, 165), 1.2f) { DashStyle = DashStyle.Dash };
            using var bearishOutline = new Pen(Color.FromArgb(210, 180, 55, 55), 1.2f) { DashStyle = DashStyle.Dash };
            using var wickPen = new Pen(Color.FromArgb(190, 90, 90, 90), 1.1f) { DashStyle = DashStyle.Dash };

            // The right side of the chart has a reserved 25% margin. If the user zooms
            // very close, compress only projected spacing enough to keep all forecast
            // candles inside that margin; actual candle spacing is never changed.
            var normalFutureWidth = step * onFuMoradiForecast.Count;
            var availableFutureWidth = plot.Width * 0.235;
            var forecastStep = normalFutureWidth > availableFutureWidth && normalFutureWidth > 0
                ? step * availableFutureWidth / normalFutureWidth
                : step;
            var candleWidth = Math.Max(1.5f, (float)(Math.Min(step, forecastStep) * 0.65));
            var forecastStartX = plot.Left + step * (localAnchor + 1.5) + initialOffset + horizontalPanOffset;

            for (var i = 0; i < onFuMoradiForecast.Count; i++)
            {
                var item = onFuMoradiForecast[i];
                var x = (float)(forecastStartX + forecastStep * i);
                var high = PriceToScreen(item.High, plot, min, max);
                var low = PriceToScreen(item.Low, plot, min, max);
                var open = PriceToScreen(item.Open, plot, min, max);
                var close = PriceToScreen(item.Close, plot, min, max);
                var rising = item.Close >= item.Open;
                var fill = rising ? bullishFill : bearishFill;
                var outline = rising ? bullishOutline : bearishOutline;

                graphics.DrawLine(wickPen, x, high, x, low);
                var top = Math.Min(open, close);
                var bottom = Math.Max(open, close);
                var rect = RectangleF.FromLTRB(
                    x - candleWidth / 2f,
                    top,
                    x + candleWidth / 2f,
                    Math.Max(top + 1f, bottom));
                graphics.FillRectangle(fill, rect);
                graphics.DrawRectangle(outline, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        private void SelectOnFuMoradiAnchor(Point location)
        {
            if (!IsOnFuMoradiActive || !IsInsidePlot(location) || points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visibleCountNow = Math.Max(1, endIndex - firstIndex);
            var step = plot.Width / (double)visibleCountNow;
            var initialOffset = -plot.Width * 0.25;
            var relativeX = location.X - plot.Left - initialOffset - horizontalPanOffset;
            var localIndex = (int)Math.Round(relativeX / step - 0.5);
            var displayedCount = testMode && testEndIndex >= firstIndex
                ? Math.Clamp(testEndIndex - firstIndex + 1, 0, visibleCountNow)
                : visibleCountNow;

            if (localIndex < 0 || localIndex >= displayedCount)
                return;

            var selectedIndex = firstIndex + localIndex;
            if (testMode && selectedIndex > testEndIndex)
                return;

            onFuMoradiAnchorIndex = selectedIndex;
            onFuMoradiCachedAnchorIndex = -1;
            Invalidate();
        }
    }
}
