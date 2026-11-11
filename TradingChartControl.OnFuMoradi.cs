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
            public int ForecastCandleCount { get; set; } = 13;
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

            // Highlight the selected parameter mode and the available Save action.
            // Inactive buttons retain the standard gray WinForms background.
            var manualModeActive = !onFuMoradiAutoMode;
            onFuMoradiManualButton.BackColor = manualModeActive
                ? Color.DodgerBlue
                : SystemColors.Control;
            onFuMoradiManualButton.ForeColor = manualModeActive
                ? Color.White
                : SystemColors.ControlText;

            onFuMoradiAutoButton.BackColor = onFuMoradiAutoMode
                ? Color.DodgerBlue
                : SystemColors.Control;
            onFuMoradiAutoButton.ForeColor = onFuMoradiAutoMode
                ? Color.White
                : SystemColors.ControlText;

            onFuMoradiSaveButton.BackColor = onFuMoradiSaveButton.Enabled
                ? Color.DodgerBlue
                : SystemColors.Control;
            onFuMoradiSaveButton.ForeColor = onFuMoradiSaveButton.Enabled
                ? Color.White
                : SystemColors.ControlText;

            if (!visible)
                return;

            var plot = GetPlotRectangle();
            var y = Math.Max(2, plot.Top + 2);
            const int buttonWidth = 76;
            const int buttonGap = 3;
            var totalButtonsWidth = buttonWidth * 3 + buttonGap * 2;
            var buttonsLeft = plot.Left + Math.Max(0, (plot.Width - totalButtonsWidth) / 2);
            onFuMoradiManualButton.Location = new Point(buttonsLeft, y);
            onFuMoradiAutoButton.Location = new Point(buttonsLeft + buttonWidth + buttonGap, y);
            onFuMoradiSaveButton.Location = new Point(buttonsLeft + (buttonWidth + buttonGap) * 2, y);
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
                indicator.Period = Math.Clamp(saved.ForecastCandleCount <= 0
                    ? 13
                    : saved.ForecastCandleCount, 1, 100);
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
                Epsilon = indicator.OnFuMoradiEpsilon,
                ForecastCandleCount = Math.Clamp(indicator.Period, 1, 100)
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

        private sealed class OnFuMoradiEvaluationMetrics
        {
            public int Samples { get; set; }
            public double AbsoluteOhlcErrorSum { get; set; }
            public double SquaredOhlcErrorSum { get; set; }
            public int OhlcValueCount { get; set; }
            public double RangeErrorSum { get; set; }
            public int DirectionCorrect { get; set; }
            public int DirectionSamples { get; set; }
            public double[] ComponentAbsoluteErrorSums { get; } = new double[4];
            public int[] ComponentValueCounts { get; } = new int[4];
            public double[] HorizonAbsoluteErrorSums { get; } = new double[5];
            public int[] HorizonValueCounts { get; } = new int[5];

            public double OhlcMaePercent =>
                OhlcValueCount > 0 ? AbsoluteOhlcErrorSum / OhlcValueCount : double.NaN;

            public double OhlcRmsePercent =>
                OhlcValueCount > 0 ? Math.Sqrt(SquaredOhlcErrorSum / OhlcValueCount) : double.NaN;

            public double RangeMaePercent =>
                Samples > 0 ? RangeErrorSum / Samples : double.NaN;

            public double DirectionalAccuracyPercent =>
                DirectionSamples > 0 ? 100.0 * DirectionCorrect / DirectionSamples : double.NaN;

            public double CompositeScore =>
                0.50 * OhlcMaePercent +
                0.20 * OhlcRmsePercent +
                0.20 * RangeMaePercent +
                0.10 * (100.0 - DirectionalAccuracyPercent);

            public double ComponentMae(int index) =>
                ComponentValueCounts[index] > 0
                    ? ComponentAbsoluteErrorSums[index] / ComponentValueCounts[index]
                    : double.NaN;
        }

        private OnFuMoradiEvaluationMetrics EvaluateOnFuMoradiCandidate(
            ChartIndicator candidate,
            int[] validationAnchors)
        {
            const int horizon = 5;
            candidate.Period = horizon;
            return EvaluateOnFuMoradiForecastFactory(
                validationAnchors,
                horizon,
                (anchor, steps) => CalculateOnFuMoradiForecast(anchor, candidate));
        }

        private OnFuMoradiEvaluationMetrics EvaluateOnFuMoradiForecastFactory(
            int[] validationAnchors,
            int horizon,
            Func<int, int, List<TradingChartPoint>> forecastFactory)
        {
            var metrics = new OnFuMoradiEvaluationMetrics();
            foreach (var validationAnchor in validationAnchors)
            {
                var forecast = forecastFactory(validationAnchor, horizon);
                if (forecast.Count < horizon)
                    continue;

                for (var step = 1; step <= horizon; step++)
                {
                    var actual = points[validationAnchor + step];
                    var predicted = forecast[step - 1];
                    if (!double.IsFinite(actual.Close) || actual.Close <= 0 ||
                        !double.IsFinite(actual.Open) || !double.IsFinite(actual.High) ||
                        !double.IsFinite(actual.Low) || actual.High < actual.Low ||
                        !double.IsFinite(predicted.Open) || !double.IsFinite(predicted.High) ||
                        !double.IsFinite(predicted.Low) || !double.IsFinite(predicted.Close))
                        continue;

                    var scale = actual.Close;
                    var errors = new[]
                    {
                        (predicted.Open - actual.Open) / scale * 100.0,
                        (predicted.High - actual.High) / scale * 100.0,
                        (predicted.Low - actual.Low) / scale * 100.0,
                        (predicted.Close - actual.Close) / scale * 100.0
                    };

                    for (var component = 0; component < errors.Length; component++)
                    {
                        var absoluteError = Math.Abs(errors[component]);
                        metrics.AbsoluteOhlcErrorSum += absoluteError;
                        metrics.SquaredOhlcErrorSum += errors[component] * errors[component];
                        metrics.OhlcValueCount++;
                        metrics.ComponentAbsoluteErrorSums[component] += absoluteError;
                        metrics.ComponentValueCounts[component]++;
                        metrics.HorizonAbsoluteErrorSums[step - 1] += absoluteError;
                        metrics.HorizonValueCounts[step - 1]++;
                    }

                    var predictedRange = Math.Max(0, predicted.High - predicted.Low);
                    var actualRange = actual.High - actual.Low;
                    metrics.RangeErrorSum += Math.Abs(predictedRange - actualRange) / scale * 100.0;

                    var predictedDirection = Math.Sign(predicted.Close - predicted.Open);
                    var actualDirection = Math.Sign(actual.Close - actual.Open);
                    if (predictedDirection == actualDirection)
                        metrics.DirectionCorrect++;
                    metrics.DirectionSamples++;
                    metrics.Samples++;
                }
            }

            return metrics;
        }

        private List<TradingChartPoint> CalculateOnFuMoradiBaselineForecast(
            int anchor,
            int horizon,
            bool statisticalContinuation)
        {
            var result = new List<TradingChartPoint>();
            if (anchor < 19 || anchor >= points.Count || horizon <= 0)
                return result;

            var last = points[anchor];
            if (!double.IsFinite(last.Close) || last.Close <= 0)
                return result;

            var dateStep = anchor > 0
                ? points[anchor].Date - points[anchor - 1].Date
                : TimeSpan.FromDays(1);
            if (dateStep <= TimeSpan.Zero || dateStep > TimeSpan.FromDays(366))
                dateStep = TimeSpan.FromDays(1);

            if (!statisticalContinuation)
            {
                // Persistence baseline: repeat the latest observed OHLC candle unchanged.
                for (var step = 1; step <= horizon; step++)
                {
                    result.Add(new TradingChartPoint
                    {
                        Date = last.Date.AddTicks(dateStep.Ticks * step),
                        HasRealDate = last.HasRealDate,
                        Open = last.Open,
                        High = last.High,
                        Low = last.Low,
                        Close = last.Close,
                        Volume = 0
                    });
                }
                return result;
            }

            // Statistical continuation baseline: extrapolate the mean log return
            // and mean normalized range/wick proportions of the latest five candles.
            var first = Math.Max(1, anchor - 4);
            var returnSum = 0.0;
            var rangeRatioSum = 0.0;
            var upperWickSum = 0.0;
            var lowerWickSum = 0.0;
            var sampleCount = 0;

            for (var index = first; index <= anchor; index++)
            {
                var previousClose = points[index - 1].Close;
                var candle = points[index];
                if (!double.IsFinite(previousClose) || previousClose <= 0 ||
                    !double.IsFinite(candle.Close) || candle.Close <= 0 ||
                    !double.IsFinite(candle.Open) || !double.IsFinite(candle.High) ||
                    !double.IsFinite(candle.Low) || candle.High < candle.Low)
                    continue;

                returnSum += Math.Log(candle.Close / previousClose);
                rangeRatioSum += Math.Max(0, candle.High - candle.Low) / candle.Close;
                upperWickSum += Math.Max(0, candle.High - Math.Max(candle.Open, candle.Close));
                lowerWickSum += Math.Max(0, Math.Min(candle.Open, candle.Close) - candle.Low);
                sampleCount++;
            }

            if (sampleCount == 0)
                return result;

            var averageLogReturn = returnSum / sampleCount;
            var averageRangeRatio = rangeRatioSum / sampleCount;
            var totalWick = upperWickSum + lowerWickSum;
            var upperShare = totalWick > 1e-12 ? upperWickSum / totalWick : 0.5;
            var lowerShare = totalWick > 1e-12 ? lowerWickSum / totalWick : 0.5;
            var previousCloseForecast = last.Close;

            for (var step = 1; step <= horizon; step++)
            {
                var open = previousCloseForecast;
                var close = open * Math.Exp(averageLogReturn);
                var body = Math.Abs(close - open);
                var expectedRange = Math.Max(open * averageRangeRatio, body);
                var residualRange = Math.Max(0, expectedRange - body);
                var wickTotal = upperShare + lowerShare;
                var upperWick = wickTotal > 0 ? residualRange * upperShare / wickTotal : residualRange / 2.0;
                var lowerWick = residualRange - upperWick;

                result.Add(new TradingChartPoint
                {
                    Date = last.Date.AddTicks(dateStep.Ticks * step),
                    HasRealDate = last.HasRealDate,
                    Open = open,
                    High = Math.Max(open, close) + upperWick,
                    Low = Math.Max(1e-12, Math.Min(open, close) - lowerWick),
                    Close = close,
                    Volume = 0
                });

                previousCloseForecast = close;
            }

            return result;
        }

        private void OnFuMoradiAutoButton_Click(object? sender, EventArgs e)
        {
            var indicator = GetOnFuMoradiIndicator();
            var anchor = ResolveOnFuMoradiAnchor();
            const int evaluationHorizon = 5;
            if (indicator == null || anchor < 523)
            {
                MessageBox.Show(FindForm(),
                    "برای مقایسه معتبر با حداقل ۲۰ پنجره آزمون مستقل پنج‌گامی، حداقل ۵۲۴ کندل معتبر لازم است.",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Keep 500 chronological validation origins: 400 for model selection
            // (with a five-origin embargo) and 100 for the untouched holdout.
            var firstValidationAnchor = Math.Max(19, anchor - 504);
            var lastValidationAnchor = anchor - evaluationHorizon;
            var validationAnchors = Enumerable.Range(
                firstValidationAnchor,
                lastValidationAnchor - firstValidationAnchor + 1).ToArray();

            // Keep the newest chronological portion completely out of parameter
            // selection. A five-anchor embargo prevents training targets from
            // overlapping the beginning of the final holdout period.
            if (validationAnchors.Length < 500)
            {
                MessageBox.Show(FindForm(),
                    "برای این گزارش به ۵۰۰ مبدأ تاریخی نیاز است تا حداقل ۲۰ پنجره آزمون مستقل پنج‌گامی تشکیل شود.",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var splitIndex = (int)Math.Floor(validationAnchors.Length * 0.80);
            var trainingAnchors = validationAnchors.Take(splitIndex - evaluationHorizon).ToArray();

            // Use non-overlapping five-candle windows for the final holdout so
            // each real target candle is counted once in the independent report.
            var holdoutAnchors = validationAnchors
                .Skip(splitIndex)
                .Where((_, index) => index % evaluationHorizon == 0)
                .ToArray();

            double[] alphas = { 0.30, 0.55, 0.80 };
            double[] betas = { 0.10, 0.25, 0.40 };
            double[] gammas = { 0.10, 0.20, 0.35 };
            double[] deltas = { 0.20, 0.35, 0.50 };
            double[] epsilons = { 0.00, 0.15, 0.30 };

            var bestScore = double.PositiveInfinity;
            var best = (Alpha: indicator.OnFuMoradiAlpha, Beta: indicator.OnFuMoradiBeta,
                Gamma: indicator.OnFuMoradiGamma, Delta: indicator.OnFuMoradiDelta,
                Epsilon: indicator.OnFuMoradiEpsilon);
            OnFuMoradiEvaluationMetrics? bestMetrics = null;

            foreach (var alpha in alphas)
            foreach (var beta in betas)
            foreach (var gamma in gammas)
            foreach (var delta in deltas)
            foreach (var epsilon in epsilons)
            {
                var candidate = new ChartIndicator
                {
                    Type = ChartIndicatorType.OnFuMoradi,
                    Period = evaluationHorizon,
                    OnFuMoradiAlpha = alpha,
                    OnFuMoradiBeta = beta,
                    OnFuMoradiGamma = gamma,
                    OnFuMoradiDelta = delta,
                    OnFuMoradiEpsilon = epsilon
                };

                var trainingMetrics = EvaluateOnFuMoradiCandidate(candidate, trainingAnchors);
                var score = trainingMetrics.CompositeScore;
                if (trainingMetrics.Samples > 0 && double.IsFinite(score) && score < bestScore)
                {
                    bestScore = score;
                    best = (alpha, beta, gamma, delta, epsilon);
                }
            }

            if (!double.IsFinite(bestScore))
            {
                MessageBox.Show(FindForm(), "داده معتبر کافی برای بهینه‌سازی وجود ندارد.",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var holdoutCandidate = new ChartIndicator
            {
                Type = ChartIndicatorType.OnFuMoradi,
                Period = evaluationHorizon,
                OnFuMoradiAlpha = best.Alpha,
                OnFuMoradiBeta = best.Beta,
                OnFuMoradiGamma = best.Gamma,
                OnFuMoradiDelta = best.Delta,
                OnFuMoradiEpsilon = best.Epsilon
            };
            bestMetrics = EvaluateOnFuMoradiCandidate(holdoutCandidate, holdoutAnchors);
            if (bestMetrics.Samples == 0 || !double.IsFinite(bestMetrics.CompositeScore))
            {
                MessageBox.Show(FindForm(), "ارزیابی مستقل روی داده‌های کنارگذاشته‌شده نتیجه معتبر نداد.",
                    "OnFuMoradi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var persistenceMetrics = EvaluateOnFuMoradiForecastFactory(
                holdoutAnchors, evaluationHorizon,
                (testAnchor, steps) => CalculateOnFuMoradiBaselineForecast(testAnchor, steps, false));
            var statisticalMetrics = EvaluateOnFuMoradiForecastFactory(
                holdoutAnchors, evaluationHorizon,
                (testAnchor, steps) => CalculateOnFuMoradiBaselineForecast(testAnchor, steps, true));

            if (persistenceMetrics.Samples == 0 || statisticalMetrics.Samples == 0)
            {
                MessageBox.Show(FindForm(),
                    "محاسبه مدل‌های پایه روی همان آزمون مستقل کامل نشد؛ گزارش مقایسه معتبر نیست.",
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

            var horizonReport = string.Join(Environment.NewLine,
                Enumerable.Range(0, evaluationHorizon).Select(i =>
                    $"گام {i + 1}: MAE={(
                        bestMetrics.HorizonValueCounts[i] > 0
                            ? bestMetrics.HorizonAbsoluteErrorSums[i] / bestMetrics.HorizonValueCounts[i]
                            : double.NaN):F3}%"));

            var sensitivityReport = BuildOnFuMoradiSensitivityReport(
                best, trainingAnchors, evaluationHorizon);

            var report =
                $"ارزیابی Walk-Forward با افق {evaluationHorizon} کندل" + Environment.NewLine +
                $"تعداد پنجره‌های آزمون مستقل: {holdoutAnchors.Length:N0}" + Environment.NewLine +
                $"تعداد کندل‌های هدف معتبر و غیرتکراری: {bestMetrics.Samples:N0}" + Environment.NewLine +
                $"MAE کل OHLC: {bestMetrics.OhlcMaePercent:F3}%" + Environment.NewLine +
                $"RMSE کل OHLC: {bestMetrics.OhlcRmsePercent:F3}%" + Environment.NewLine +
                $"MAE بازشدن (Open): {bestMetrics.ComponentMae(0):F3}%" + Environment.NewLine +
                $"MAE بیشترین (High): {bestMetrics.ComponentMae(1):F3}%" + Environment.NewLine +
                $"MAE کمترین (Low): {bestMetrics.ComponentMae(2):F3}%" + Environment.NewLine +
                $"MAE پایانی (Close): {bestMetrics.ComponentMae(3):F3}%" + Environment.NewLine +
                $"خطای MAE دامنه High-Low: {bestMetrics.RangeMaePercent:F3}%" + Environment.NewLine +
                $"درستی جهت کندل: {bestMetrics.DirectionalAccuracyPercent:F1}%" + Environment.NewLine +
                "مقایسه با مدل‌های پایه (همان آزمون مستقل):" + Environment.NewLine +
                $"تکرار آخرین کندل: MAE={persistenceMetrics.OhlcMaePercent:F3}%، RMSE={persistenceMetrics.OhlcRmsePercent:F3}%، جهت={persistenceMetrics.DirectionalAccuracyPercent:F1}%" + Environment.NewLine +
                $"تداوم آماری پنج کندل اخیر: MAE={statisticalMetrics.OhlcMaePercent:F3}%، RMSE={statisticalMetrics.OhlcRmsePercent:F3}%، جهت={statisticalMetrics.DirectionalAccuracyPercent:F1}%" + Environment.NewLine +
                $"امتیاز ترکیبی کمتر بهتر است | OnFuMoradi={bestMetrics.CompositeScore:F3}، تکرار آخرین کندل={persistenceMetrics.CompositeScore:F3}، تداوم آماری={statisticalMetrics.CompositeScore:F3}" + Environment.NewLine +
                "خطای MAE به تفکیک افق:" + Environment.NewLine + horizonReport +
                Environment.NewLine + Environment.NewLine +
                $"پارامترهای منتخب: روند={best.Alpha:F2}، شتاب={best.Beta:F2}، " +
                $"بازگشت={best.Gamma:F2}، محدودیت نوسان={best.Delta:F2}، سایه={best.Epsilon:F2}" +
                Environment.NewLine + Environment.NewLine +
                "بررسی حساسیت (روی داده بهینه‌سازی، نه آزمون مستقل):" + Environment.NewLine +
                sensitivityReport;

            MessageBox.Show(FindForm(), report, "گزارش ارزیابی OnFuMoradi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string BuildOnFuMoradiSensitivityReport(
            (double Alpha, double Beta, double Gamma, double Delta, double Epsilon) best,
            int[] trainingAnchors,
            int evaluationHorizon)
        {
            var baseline = new ChartIndicator
            {
                Type = ChartIndicatorType.OnFuMoradi,
                Period = evaluationHorizon,
                OnFuMoradiAlpha = best.Alpha,
                OnFuMoradiBeta = best.Beta,
                OnFuMoradiGamma = best.Gamma,
                OnFuMoradiDelta = best.Delta,
                OnFuMoradiEpsilon = best.Epsilon
            };
            var baselineScore = EvaluateOnFuMoradiCandidate(baseline, trainingAnchors).CompositeScore;
            if (!double.IsFinite(baselineScore))
                return "امکان محاسبه امتیاز مبنا وجود نداشت.";

            var rows = new[]
            {
                ("وزن روند", 0, best.Alpha, 0.25, 0.0, 2.0),
                ("وزن شتاب", 1, best.Beta, 0.15, 0.0, 2.0),
                ("بازگشت به میانگین", 2, best.Gamma, 0.15, 0.0, 2.0),
                ("محدودیت نوسان", 3, best.Delta, 0.15, 0.05, 2.0),
                ("الگوی سایه", 4, best.Epsilon, 0.15, 0.0, 2.0)
            };

            var lines = new List<string>();
            foreach (var row in rows)
            {
                var lowValue = Math.Max(row.Item5, row.Item3 - row.Item4);
                var highValue = Math.Min(row.Item6, row.Item3 + row.Item4);
                var lowCandidate = new ChartIndicator
                {
                    Type = ChartIndicatorType.OnFuMoradi,
                    Period = evaluationHorizon,
                    OnFuMoradiAlpha = best.Alpha,
                    OnFuMoradiBeta = best.Beta,
                    OnFuMoradiGamma = best.Gamma,
                    OnFuMoradiDelta = best.Delta,
                    OnFuMoradiEpsilon = best.Epsilon
                };
                SetOnFuMoradiSensitivityParameter(lowCandidate, row.Item2, lowValue);
                var highCandidate = new ChartIndicator
                {
                    Type = ChartIndicatorType.OnFuMoradi,
                    Period = evaluationHorizon,
                    OnFuMoradiAlpha = best.Alpha,
                    OnFuMoradiBeta = best.Beta,
                    OnFuMoradiGamma = best.Gamma,
                    OnFuMoradiDelta = best.Delta,
                    OnFuMoradiEpsilon = best.Epsilon
                };
                SetOnFuMoradiSensitivityParameter(highCandidate, row.Item2, highValue);

                var lowScore = EvaluateOnFuMoradiCandidate(lowCandidate, trainingAnchors).CompositeScore;
                var highScore = EvaluateOnFuMoradiCandidate(highCandidate, trainingAnchors).CompositeScore;
                lines.Add($"{row.Item1}: مقدار کمتر {lowValue:F2} → {lowScore:F3}، مقدار بیشتر {highValue:F2} → {highScore:F3}");
            }

            lines.Add($"امتیاز مبنا: {baselineScore:F3} (کمتر بهتر است)");
            lines.Add("تغییر هر بار فقط در یک پارامتر انجام شده؛ این آزمون اثر مستقل محلی را نشان می‌دهد، نه تعامل پارامترها.");
            return string.Join(Environment.NewLine, lines);
        }

        private static void SetOnFuMoradiSensitivityParameter(
            ChartIndicator indicator, int parameterIndex, double value)
        {
            switch (parameterIndex)
            {
                case 0: indicator.OnFuMoradiAlpha = value; break;
                case 1: indicator.OnFuMoradiBeta = value; break;
                case 2: indicator.OnFuMoradiGamma = value; break;
                case 3: indicator.OnFuMoradiDelta = value; break;
                case 4: indicator.OnFuMoradiEpsilon = value; break;
            }
        }

        internal bool TryApplyOnFuMoradiManualSettings(
            int index,
            double trendWeight,
            double momentumWeight,
            double meanReversionWeight,
            double volatilityLimit,
            double wickPatternWeight,
            int forecastCandleCount,
            out string error)
        {
            error = string.Empty;
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.OnFuMoradi)
            {
                error = "اندیکاتور OnFuMoradi دیگر فعال نیست.";
                return false;
            }

            var indicator = indicators[index];
            var previous = (
                Alpha: indicator.OnFuMoradiAlpha,
                Beta: indicator.OnFuMoradiBeta,
                Gamma: indicator.OnFuMoradiGamma,
                Delta: indicator.OnFuMoradiDelta,
                Epsilon: indicator.OnFuMoradiEpsilon,
                Period: indicator.Period);

            indicator.OnFuMoradiAlpha = Math.Clamp(trendWeight, 0, 2);
            indicator.OnFuMoradiBeta = Math.Clamp(momentumWeight, 0, 2);
            indicator.OnFuMoradiGamma = Math.Clamp(meanReversionWeight, 0, 2);
            indicator.OnFuMoradiDelta = Math.Clamp(volatilityLimit, 0.05, 2);
            indicator.OnFuMoradiEpsilon = Math.Clamp(wickPatternWeight, 0, 2);
            indicator.Period = Math.Clamp(forecastCandleCount, 1, 100);

            try
            {
                SaveOnFuMoradiManualParameters(indicator);
            }
            catch (Exception ex)
            {
                indicator.OnFuMoradiAlpha = previous.Alpha;
                indicator.OnFuMoradiBeta = previous.Beta;
                indicator.OnFuMoradiGamma = previous.Gamma;
                indicator.OnFuMoradiDelta = previous.Delta;
                indicator.OnFuMoradiEpsilon = previous.Epsilon;
                indicator.Period = previous.Period;
                error = ex.Message;
                return false;
            }

            // Applying parameter values must not change the mode selected by the user.
            // If Auto is active, it remains active; the saved values are loaded when
            // the user explicitly switches to Manual.
            onFuMoradiCachedAnchorIndex = -1;
            onFuMoradiCachedIndicatorIndex = -1;
            onFuMoradiForecast.Clear();
            SyncOnFuMoradiButtons();
            Invalidate();
            return true;
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
                Period = 13,
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

            // Candle outlines and wicks use the same strong directional colors
            // in both modes. Auto mode adds a muted, fully opaque solid body fill.
            using var bullishFill = new SolidBrush(Color.FromArgb(150, 205, 232, 205));
            using var bearishFill = new SolidBrush(Color.FromArgb(150, 245, 211, 180));
            using var bullishOutline = new Pen(Color.DarkGreen, 1.2f)
                { DashStyle = DashStyle.Solid };
            using var bearishOutline = new Pen(Color.DarkRed, 1.2f)
                { DashStyle = DashStyle.Solid };
            using var bullishWickPen = new Pen(Color.DarkGreen, 1.1f)
                { DashStyle = DashStyle.Solid };
            using var bearishWickPen = new Pen(Color.DarkRed, 1.1f)
                { DashStyle = DashStyle.Solid };

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

                var wickPen = rising ? bullishWickPen : bearishWickPen;
                graphics.DrawLine(wickPen, x, high, x, low);
                var top = Math.Min(open, close);
                var bottom = Math.Max(open, close);
                var rect = RectangleF.FromLTRB(
                    x - candleWidth / 2f,
                    top,
                    x + candleWidth / 2f,
                    Math.Max(top + 1f, bottom));
                // Manual bodies remain completely hollow. Auto bodies receive
                // a uniform solid muted fill without hatch or transparency pattern.
                if (onFuMoradiAutoMode)
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

            // Selecting a candle changes only the forecast anchor.
            // The user explicitly controls Manual/Auto mode through the mode buttons.
            onFuMoradiAnchorIndex = selectedIndex;
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
