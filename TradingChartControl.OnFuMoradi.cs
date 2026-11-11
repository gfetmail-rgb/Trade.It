using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private bool onFuMoradiAutoMode;
        private bool onFuMoradiHasOptimizedParameters;

        private sealed class OnFuMoradiSavedParameters
        {
            public string Symbol { get; set; } = string.Empty;
            public string TimeFrame { get; set; } = string.Empty;
            public double Alpha { get; set; }
            public double Beta { get; set; }
            public double Gamma { get; set; }
            public double Delta { get; set; }
            public double Epsilon { get; set; }
        }

        private static string OnFuMoradiSettingsPath =>
            System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Trade.It",
                "OnFuMoradiSettings.json");

        private string OnFuMoradiSettingsKey =>
            $"{chartSymbol.Trim()}|{(string.IsNullOrWhiteSpace(chartTimeFrame) ? "Normal" : chartTimeFrame.Trim())}";

        private void SyncOnFuMoradiButtons()
        {
            var visible = IsOnFuMoradiActive;
            onFuMoradiManualButton.Visible = visible;
            onFuMoradiAutoButton.Visible = visible;
            onFuMoradiSaveButton.Visible = visible;
            onFuMoradiSaveButton.Enabled = visible && onFuMoradiHasOptimizedParameters;
            if (!visible)
                return;

            var plot = GetPlotRectangle();
            var y = Math.Max(2, plot.Top + 2);
            onFuMoradiManualButton.Location = new Point(plot.Left + 3, y);
            onFuMoradiAutoButton.Location = new Point(plot.Left + 82, y);
            onFuMoradiSaveButton.Location = new Point(plot.Left + 161, y);
            onFuMoradiManualButton.BringToFront();
            onFuMoradiAutoButton.BringToFront();
            onFuMoradiSaveButton.BringToFront();
        }

        private void ApplyOnFuMoradiManualParameters(ChartIndicator indicator)
        {
            indicator.OnFuMoradiAlpha = 0.55;
            indicator.OnFuMoradiBeta = 0.25;
            indicator.OnFuMoradiGamma = 0.20;
            indicator.OnFuMoradiDelta = 0.35;
            indicator.OnFuMoradiEpsilon = 0.15;
            LoadOnFuMoradiManualParameters(indicator);
        }

        private void LoadOnFuMoradiManualParameters(ChartIndicator indicator)
        {
            try
            {
                var path = OnFuMoradiSettingsPath;
                if (!System.IO.File.Exists(path))
                    return;
                var all = System.Text.Json.JsonSerializer.Deserialize<List<OnFuMoradiSavedParameters>>(
                    System.IO.File.ReadAllText(path)) ?? new();
                var saved = all.LastOrDefault(x => string.Equals(
                    $"{x.Symbol}|{x.TimeFrame}", OnFuMoradiSettingsKey, StringComparison.OrdinalIgnoreCase));
                if (saved == null)
                    return;
                indicator.OnFuMoradiAlpha = saved.Alpha;
                indicator.OnFuMoradiBeta = saved.Beta;
                indicator.OnFuMoradiGamma = saved.Gamma;
                indicator.OnFuMoradiDelta = saved.Delta;
                indicator.OnFuMoradiEpsilon = saved.Epsilon;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[OnFuMoradi] Settings load failed: {ex}");
            }
        }

        private void SaveOnFuMoradiManualParameters(ChartIndicator indicator)
        {
            var path = OnFuMoradiSettingsPath;
            var directory = System.IO.Path.GetDirectoryName(path)!;
            System.IO.Directory.CreateDirectory(directory);
            var all = System.IO.File.Exists(path)
                ? System.Text.Json.JsonSerializer.Deserialize<List<OnFuMoradiSavedParameters>>(
                    System.IO.File.ReadAllText(path)) ?? new()
                : new List<OnFuMoradiSavedParameters>();
            all.RemoveAll(x => string.Equals(
                $"{x.Symbol}|{x.TimeFrame}", OnFuMoradiSettingsKey, StringComparison.OrdinalIgnoreCase));
            all.Add(new OnFuMoradiSavedParameters
            {
                Symbol = chartSymbol.Trim(),
                TimeFrame = string.IsNullOrWhiteSpace(chartTimeFrame) ? "Normal" : chartTimeFrame.Trim(),
                Alpha = indicator.OnFuMoradiAlpha,
                Beta = indicator.OnFuMoradiBeta,
                Gamma = indicator.OnFuMoradiGamma,
                Delta = indicator.OnFuMoradiDelta,
                Epsilon = indicator.OnFuMoradiEpsilon
            });
            System.IO.File.WriteAllText(path, System.Text.Json.JsonSerializer.Serialize(
                all, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        private void OnFuMoradiManualButton_Click(object? sender, EventArgs e)
        {
            var indicator = GetOnFuMoradiIndicator();
            if (indicator == null)
                return;
            ApplyOnFuMoradiManualParameters(indicator);
            onFuMoradiAutoMode = false;
            onFuMoradiHasOptimizedParameters = false;
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiForecast.Clear();
            SyncOnFuMoradiButtons();
            Invalidate();
        }

        private void OnFuMoradiAutoButton_Click(object? sender, EventArgs e)
        {
            var indicator = GetOnFuMoradiIndicator();
            var anchor = ResolveOnFuMoradiAnchor();
            if (indicator == null || anchor < 20)
                return;

            var bestScore = double.PositiveInfinity;
            var best = (Alpha: indicator.OnFuMoradiAlpha, Beta: indicator.OnFuMoradiBeta,
                Gamma: indicator.OnFuMoradiGamma, Delta: indicator.OnFuMoradiDelta,
                Epsilon: indicator.OnFuMoradiEpsilon);
            var firstValidationAnchor = Math.Max(19, anchor - 99);
            var validationAnchors = Enumerable.Range(firstValidationAnchor, anchor - firstValidationAnchor)
                .Where(i => i < anchor)
                .ToArray();

            double[] alphas = { 0.30, 0.55, 0.80 };
            double[] betas = { 0.10, 0.25, 0.40 };
            double[] gammas = { 0.10, 0.20, 0.35 };
            double[] deltas = { 0.20, 0.35, 0.50 };
            double[] epsilons = { 0.00, 0.15, 0.30 };

            foreach (var alpha in alphas)
            foreach (var beta in betas)
            foreach (var gamma in gammas)
            foreach (var delta in deltas)
            foreach (var epsilon in epsilons)
            {
                var candidate = new ChartIndicator
                {
                    Type = ChartIndicatorType.OnFuMoradi,
                    Period = 1,
                    OnFuMoradiAlpha = alpha,
                    OnFuMoradiBeta = beta,
                    OnFuMoradiGamma = gamma,
                    OnFuMoradiDelta = delta,
                    OnFuMoradiEpsilon = epsilon
                };
                var error = 0.0;
                var samples = 0;
                foreach (var validationAnchor in validationAnchors)
                {
                    // Forecast input ends at validationAnchor; the next candle is used
                    // only as the out-of-sample target and never enters the calculation.
                    var forecast = CalculateOnFuMoradiForecast(validationAnchor, candidate);
                    if (forecast.Count == 0)
                        continue;
                    var actual = points[validationAnchor + 1].Close;
                    var predicted = forecast[0].Close;
                    if (!double.IsFinite(actual) || actual <= 0 || !double.IsFinite(predicted))
                        continue;
                    error += Math.Abs(Math.Log(predicted / actual));
                    samples++;
                }
                if (samples > 0 && error / samples < bestScore)
                {
                    bestScore = error / samples;
                    best = (alpha, beta, gamma, delta, epsilon);
                }
            }

            if (!double.IsFinite(bestScore))
            {
                MessageBox.Show(FindForm(), "داده کافی برای بهینه‌سازی معتبر وجود ندارد.",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            indicator.OnFuMoradiAlpha = best.Alpha;
            indicator.OnFuMoradiBeta = best.Beta;
            indicator.OnFuMoradiGamma = best.Gamma;
            indicator.OnFuMoradiDelta = best.Delta;
            indicator.OnFuMoradiEpsilon = best.Epsilon;
            onFuMoradiAutoMode = true;
            onFuMoradiHasOptimizedParameters = true;
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiForecast.Clear();
            SyncOnFuMoradiButtons();
            Invalidate();
        }

        private void OnFuMoradiSaveButton_Click(object? sender, EventArgs e)
        {
            var indicator = GetOnFuMoradiIndicator();
            if (indicator == null || !onFuMoradiHasOptimizedParameters)
                return;
            try
            {
                SaveOnFuMoradiManualParameters(indicator);
                onFuMoradiHasOptimizedParameters = false;
                onFuMoradiAutoMode = false;
                SyncOnFuMoradiButtons();
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(FindForm(), $"ذخیره پارامترها انجام نشد: {ex.Message}",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private readonly string onFuMoradiInstanceId = Guid.NewGuid().ToString("N").Substring(0, 8);
        private int onFuMoradiAnchorIndex = -1;
        private int onFuMoradiCachedAnchorIndex = -1;
        private int onFuMoradiCachedIndicatorIndex = -1;
        private List<TradingChartPoint> onFuMoradiForecast = new();
        private bool onFuMoradiClickCandidate;
        private Point onFuMoradiMouseDownPoint;
        private int onFuMoradiLastRenderLogAnchor = int.MinValue;

        private bool IsOnFuMoradiActive =>
            indicators.Any(x => x.Type == ChartIndicatorType.OnFuMoradi);

        public void ToggleOnFuMoradi()
        {
            System.Diagnostics.Debug.WriteLine($"[OnFuMoradiTrace] TOGGLE_ENTER Instance={onFuMoradiInstanceId}, Symbol={chartSymbol}, Thread={Environment.CurrentManagedThreadId}, Tick={Environment.TickCount64}, Indicators={indicators.Count}, Stack={Environment.StackTrace}");
            var existingIndex = indicators.FindIndex(x => x.Type == ChartIndicatorType.OnFuMoradi);
            if (existingIndex >= 0)
            {
                RemoveIndicatorAt(existingIndex);
                onFuMoradiAnchorIndex = -1;
                onFuMoradiForecast.Clear();
                onFuMoradiClickCandidate = false;
                onFuMoradiAutoMode = false;
                onFuMoradiHasOptimizedParameters = false;
                SyncOnFuMoradiButtons();
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

            var newIndicator = new ChartIndicator
            {
                Type = ChartIndicatorType.OnFuMoradi,
                Period = 10,
                OnFuMoradiAlpha = 0.55,
                OnFuMoradiBeta = 0.25,
                OnFuMoradiGamma = 0.20,
                OnFuMoradiDelta = 0.35,
                OnFuMoradiEpsilon = 0.15
            };
            ApplyOnFuMoradiManualParameters(newIndicator);
            indicators.Add(newIndicator);
            onFuMoradiAutoMode = false;
            onFuMoradiHasOptimizedParameters = false;

            onFuMoradiAnchorIndex = testMode
                ? (testStartSelected ? testEndIndex : -1)
                : points.Count - 1;
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiCachedIndicatorIndex = -1;
            onFuMoradiForecast.Clear();
            onFuMoradiLastRenderLogAnchor = int.MinValue;
            System.Diagnostics.Debug.WriteLine($"[OnFuMoradi] Activated: Instance={onFuMoradiInstanceId}, Symbol={chartSymbol}, Points={points.Count}, TestMode={testMode}, TestStartSelected={testStartSelected}, TestEnd={testEndIndex}, Anchor={onFuMoradiAnchorIndex}, Indicators={indicators.Count}, Types={string.Join(",", indicators.Select(x => x.Type.ToString()))}");
            SyncOnFuMoradiButtons();
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
            System.Diagnostics.Debug.WriteLine($"[OnFuMoradi] Forecast calculated: Anchor={anchor}, Period={indicator.Period}, Candles={onFuMoradiForecast.Count}, LastClose={points[anchor].Close}, FirstIndex={firstIndex}, VisibleCount={visibleCount}");
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
            var candleRangeSum = 0.0;
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

                var candleRange = p.High - p.Low;
                if (double.IsFinite(candleRange) && candleRange > 0)
                    candleRangeSum += candleRange;
            }

            var atr = atrSamples > 0 ? atrSum / atrSamples : lastClose * 0.01;
            var volatility = Math.Clamp(atr / lastClose, 0.0001, 0.20);
            var samples = Math.Max(1, anchor - atrStart + 1);
            var averageCandleRange = candleRangeSum > 0
                ? candleRangeSum / samples
                : atr;
            var totalWick = upperWickSum + lowerWickSum;
            var upperWickShare = totalWick > 1e-12 ? upperWickSum / totalWick : 0.5;
            var lowerWickShare = totalWick > 1e-12 ? lowerWickSum / totalWick : 0.5;
            var averageBodyRatio = candleRangeSum > 1e-12
                ? Math.Clamp(bodySum / candleRangeSum, 0.20, 0.65)
                : 0.35;

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
                var rawClose = open * Math.Exp(drift);

                // Forecast body size follows the recent empirical body/range ratio.
                // The sign remains model-driven; only a near-zero move uses the
                // historical trend/momentum direction as a fallback.
                var expectedRange = Math.Max(averageCandleRange, Math.Abs(rawClose - open));
                var direction = Math.Abs(drift) > 1e-10
                    ? Math.Sign(drift)
                    : Math.Abs(trend) > 1e-10
                        ? Math.Sign(trend)
                        : Math.Sign(momentum);
                if (direction == 0)
                    direction = 1;

                var targetBody = Math.Min(
                    expectedRange * averageBodyRatio,
                    expectedRange * 0.80);
                var body = Math.Max(Math.Abs(rawClose - open), targetBody);
                var close = open + direction * body;
                var residualRange = Math.Max(0, expectedRange - body);
                var patternBias = Math.Clamp(epsilon * (upperWickShare - lowerWickShare), -0.5, 0.5);
                var adjustedUpperShare = Math.Clamp(upperWickShare + patternBias * 0.25, 0.05, 0.95);
                var adjustedLowerShare = Math.Clamp(lowerWickShare - patternBias * 0.25, 0.05, 0.95);
                var wickShareTotal = adjustedUpperShare + adjustedLowerShare;
                var upperWick = residualRange * adjustedUpperShare / wickShareTotal;
                var lowerWick = residualRange * adjustedLowerShare / wickShareTotal;
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
            {
                if (onFuMoradiLastRenderLogAnchor != anchor)
                {
                    System.Diagnostics.Debug.WriteLine($"[OnFuMoradi] Render skipped: Anchor={anchor}, LocalAnchor={localAnchor}, DisplayedCount={displayedCount}, FirstIndex={firstIndex}, TestMode={testMode}, TestEnd={testEndIndex}");
                    onFuMoradiLastRenderLogAnchor = anchor;
                }
                return;
            }

            using var bullishFill = new SolidBrush(onFuMoradiAutoMode
                ? Color.FromArgb(105, 125, 125, 125)
                : Color.FromArgb(90, 45, 145, 190));
            using var bearishFill = new SolidBrush(onFuMoradiAutoMode
                ? Color.FromArgb(105, 125, 125, 125)
                : Color.FromArgb(90, 205, 85, 85));
            using var bullishOutline = new Pen(onFuMoradiAutoMode
                ? Color.FromArgb(220, 90, 90, 90)
                : Color.FromArgb(210, 30, 115, 165), 1.2f) { DashStyle = DashStyle.Dash };
            using var bearishOutline = new Pen(onFuMoradiAutoMode
                ? Color.FromArgb(220, 90, 90, 90)
                : Color.FromArgb(210, 180, 55, 55), 1.2f) { DashStyle = DashStyle.Dash };
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
            if (onFuMoradiLastRenderLogAnchor != anchor)
            {
                System.Diagnostics.Debug.WriteLine($"[OnFuMoradi] Render: Anchor={anchor}, LocalAnchor={localAnchor}, Candles={onFuMoradiForecast.Count}, Plot={plot}, Step={step:F2}, InitialOffset={initialOffset:F2}, Pan={horizontalPanOffset:F2}, ForecastStartX={forecastStartX:F2}, ForecastEndX={forecastStartX + forecastStep * (onFuMoradiForecast.Count - 1):F2}, ForecastStep={forecastStep:F2}, Clip={plot}");
                onFuMoradiLastRenderLogAnchor = anchor;
            }

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
            if (GetOnFuMoradiIndicator() is { } activeIndicator)
                ApplyOnFuMoradiManualParameters(activeIndicator);
            onFuMoradiHasOptimizedParameters = false;
            onFuMoradiAutoMode = false;
            SyncOnFuMoradiButtons();
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiLastRenderLogAnchor = int.MinValue;

            if (testMode)
            {
                // In Test Mode the selected candle becomes the visible data boundary.
                // No real candle after it remains on screen while its forecast is shown.
                testEndIndex = selectedIndex;
                testAnchorIndex = selectedIndex;
                testAnchorScreenX = location.X;
                testStartSelected = true;
                KeepTestAnchorFixed();
                TestReferenceTimeChanged?.Invoke(this, points[selectedIndex].Date);
            }

            Invalidate();
        }
    }
}
