using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool filters3Applied;

        private bool ValidateFilters3Inputs()
        {
            var errors = new List<string>();

            bool ReadPositiveInt(TextBox box, string title, out int value)
            {
                var text = NormalizeTradingDigits(box.Text).Trim();
                if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) &&
                    value > 0 && value <= 1000)
                    return true;
                errors.Add($"{title}: عدد صحیح بین ۱ تا ۱۰۰۰ وارد کنید.");
                return false;
            }

            bool ReadNonNegative(TextBox box, string title, out double value)
            {
                var text = NormalizeTradingDigits(box.Text).Trim();
                if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value) &&
                    double.IsFinite(value) && value >= 0)
                    return true;
                errors.Add($"{title}: عدد معتبر صفر یا بزرگ‌تر وارد کنید.");
                return false;
            }

            if (maFilterEnabled3.Checked)
            {
                ReadPositiveInt(maFilterPeriod1_3, "میانگین متحرک، دوره اول", out var first);
                if (maFilterMode3.SelectedIndex == 1)
                {
                    if (ReadPositiveInt(maFilterPeriod2_3, "میانگین متحرک، دوره دوم", out var second) &&
                        first > 0 && first >= second)
                        errors.Add("برای مقایسه دو میانگین، دوره اول باید از دوره دوم کوتاه‌تر باشد.");
                }
            }

            if (rsiFilterEnabled3.Checked)
            {
                ReadPositiveInt(rsiPeriod3, "RSI، دوره", out _);
                var validMin = ReadNonNegative(rsiMinimum3, "RSI، حد پایین", out var min);
                var validMax = ReadNonNegative(rsiMaximum3, "RSI، حد بالا", out var max);
                if (validMin && validMax && (min > 100 || max > 100 || min > max))
                    errors.Add("مقادیر RSI باید بین صفر تا ۱۰۰ باشند و حد پایین از حد بالا بیشتر نباشد.");
            }

            if (distanceFilterEnabled3.Checked)
            {
                ReadPositiveInt(distanceHighPeriod3, "فاصله از سقف، دوره", out _);
                ReadNonNegative(distanceHighMax3, "حداکثر فاصله از سقف", out _);
                ReadPositiveInt(distanceLowPeriod3, "فاصله از کف، دوره", out _);
                ReadNonNegative(distanceLowMin3, "حداقل فاصله از کف", out _);
            }

            if (volumePriceFilterEnabled3.Checked)
            {
                ReadPositiveInt(volumePricePeriod3, "افزایش حجم، دوره میانگین", out _);
                var validRatio = ReadNonNegative(volumePriceRatio3, "افزایش حجم، نسبت حداقل", out var ratio);
                if (validRatio && ratio <= 0)
                    errors.Add("نسبت حجم باید بزرگ‌تر از صفر باشد.");
            }

            if (trendStrengthFilterEnabled3.Checked)
            {
                ReadPositiveInt(adxPeriod3, "ADX، دوره", out _);
                if (!ReadNonNegative(adxMinimum3, "ADX، حداقل قدرت روند", out var adx) || adx > 100)
                    errors.Add("حداقل ADX باید بین صفر تا ۱۰۰ باشد.");
            }

            if (errors.Count == 0)
                return true;

            MessageBox.Show(this,
                "فیلترهای تب سوم اعمال نشدند. موارد زیر را اصلاح کنید:\n\n" +
                string.Join(Environment.NewLine, errors.Distinct()),
                "اعتبارسنجی فیلترها",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        private IEnumerable<string> ApplyFilters3(
            IEnumerable<string> symbols,
            PortfolioDefinition definition,
            FilterRunContext context)
        {
            if (!maFilterEnabled3.Checked && !rsiFilterEnabled3.Checked &&
                !distanceFilterEnabled3.Checked && !volumePriceFilterEnabled3.Checked &&
                !trendStrengthFilterEnabled3.Checked)
                return symbols;

            var closeColumn = GetMappingColumn(definition, "آخرین");
            var highColumn = GetMappingColumn(definition, "بیشترین");
            var lowColumn = GetMappingColumn(definition, "کمترین");
            var volumeColumn = GetMappingColumn(definition, "حجم");

            return symbols.Where(symbol =>
            {
                List<string[]> rows;
                try { rows = context.GetRows(symbol); }
                catch { return false; }

                if (rows == null || rows.Count == 0)
                    return false;

                if (maFilterEnabled3.Checked)
                {
                    var maLookback = Math.Max(ReadInt(maFilterPeriod1_3),
                        maFilterMode3.SelectedIndex == 1 ? ReadInt(maFilterPeriod2_3) : 0);
                    if (closeColumn <= 0 ||
                        !TryReadSeries(rows, closeColumn, maLookback, out var closes) ||
                        !PassMovingAverageFilter(closes))
                        return false;
                }

                if (rsiFilterEnabled3.Checked)
                {
                    var rsiPeriod = ReadInt(rsiPeriod3);
                    if (closeColumn <= 0 ||
                        !TryReadSeries(rows, closeColumn, rsiPeriod * 5 + 1, out var rsiCloses) ||
                        !TryCalculateRsi(rsiCloses, rsiPeriod, out var rsi) ||
                        !PassRsiFilter(rsi))
                        return false;
                }

                if (distanceFilterEnabled3.Checked)
                {
                    if (closeColumn <= 0 || highColumn <= 0 || lowColumn <= 0 ||
                        !TryReadLatest(rows, closeColumn, out var close) || close <= 0 ||
                        !TryGetPreviousExtreme(rows, highColumn, ReadInt(distanceHighPeriod3), true, out var recentHigh) ||
                        !TryGetPreviousExtreme(rows, lowColumn, ReadInt(distanceLowPeriod3), false, out var recentLow))
                        return false;

                    var distanceFromHigh = Math.Max(0, (recentHigh - close) / close * 100.0);
                    var distanceFromLow = Math.Max(0, (close - recentLow) / close * 100.0);
                    if (distanceFromHigh > ReadDouble(distanceHighMax3) ||
                        distanceFromLow < ReadDouble(distanceLowMin3))
                        return false;
                }

                if (volumePriceFilterEnabled3.Checked)
                {
                    var period = ReadInt(volumePricePeriod3);
                    if (volumeColumn <= 0 || closeColumn <= 0 ||
                        !TryGetVolumeRatio(rows, volumeColumn, period, out var volumeRatio) ||
                        volumeRatio < ReadDouble(volumePriceRatio3) ||
                        !TryReadLatest(rows, closeColumn, out var latestClose) ||
                        !TryReadAtOffset(rows, closeColumn, 1, out var previousClose))
                        return false;

                    var direction = volumePriceDirection3.SelectedItem?.ToString();
                    if (direction == "مثبت" && latestClose <= previousClose)
                        return false;
                    if (direction == "منفی" && latestClose >= previousClose)
                        return false;
                }

                if (trendStrengthFilterEnabled3.Checked)
                {
                    if (highColumn <= 0 || lowColumn <= 0 || closeColumn <= 0 ||
                        !TryCalculateAdx(rows, highColumn, lowColumn, closeColumn, ReadInt(adxPeriod3),
                            out var adx, out var positiveDi, out var negativeDi) ||
                        adx < ReadDouble(adxMinimum3) || positiveDi <= negativeDi)
                        return false;
                }

                return true;
            });
        }

        private bool PassMovingAverageFilter(IReadOnlyList<double> closes)
        {
            var firstPeriod = ReadInt(maFilterPeriod1_3);
            if (maFilterMode3.SelectedIndex == 0)
            {
                if (closes.Count < firstPeriod ||
                    !TryAverage(closes, firstPeriod, out var average))
                    return false;
                return closes[^1] > average;
            }

            var secondPeriod = ReadInt(maFilterPeriod2_3);
            return TryAverage(closes, firstPeriod, out var shortAverage) &&
                   TryAverage(closes, secondPeriod, out var longAverage) &&
                   shortAverage > longAverage && closes[^1] > longAverage;
        }

        private bool PassRsiFilter(double rsi)
        {
            var min = ReadDouble(rsiMinimum3);
            var max = ReadDouble(rsiMaximum3);
            return rsiOperator3.SelectedIndex switch
            {
                0 => rsi >= min && rsi <= max,
                1 => rsi < min,
                2 => rsi > max,
                _ => false
            };
        }

        private static bool TryReadSeries(List<string[]> rows, int column, int requiredCount, out List<double> values)
        {
            values = new List<double>(requiredCount);
            if (requiredCount <= 0 || rows.Count < requiredCount)
                return false;

            var firstIndex = rows.Count - requiredCount;
            for (var rowIndex = firstIndex; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                if (column > row.Length || !TryParseTradingNumber(row[column - 1], out var value) ||
                    !double.IsFinite(value) || value <= 0)
                    return false;
                values.Add(value);
            }
            return values.Count == requiredCount;
        }

        private static bool TryReadLatest(List<string[]> rows, int column, out double value) =>
            TryReadAtOffset(rows, column, 0, out value);

        private static bool TryReadAtOffset(List<string[]> rows, int column, int offset, out double value)
        {
            value = 0;
            var index = rows.Count - 1 - offset;
            if (column <= 0 || index < 0 || index >= rows.Count ||
                column > rows[index].Length ||
                !TryParseTradingNumber(rows[index][column - 1], out value) ||
                !double.IsFinite(value))
                return false;
            return true;
        }

        private static bool TryAverage(IReadOnlyList<double> values, int period, out double average)
        {
            average = 0;
            if (period <= 0 || values.Count < period)
                return false;
            var sum = 0.0;
            for (var i = values.Count - period; i < values.Count; i++)
                sum += values[i];
            average = sum / period;
            return double.IsFinite(average);
        }

        private static bool TryCalculateRsi(IReadOnlyList<double> closes, int period, out double rsi)
        {
            rsi = 0;
            if (period <= 0 || closes.Count <= period)
                return false;

            var gain = 0.0;
            var loss = 0.0;
            for (var i = 1; i <= period; i++)
            {
                var change = closes[i] - closes[i - 1];
                if (change > 0) gain += change;
                else loss -= change;
            }

            var averageGain = gain / period;
            var averageLoss = loss / period;
            for (var i = period + 1; i < closes.Count; i++)
            {
                var change = closes[i] - closes[i - 1];
                var currentGain = Math.Max(0, change);
                var currentLoss = Math.Max(0, -change);
                averageGain = (averageGain * (period - 1) + currentGain) / period;
                averageLoss = (averageLoss * (period - 1) + currentLoss) / period;
            }

            if (averageLoss <= 1e-12)
            {
                rsi = averageGain <= 1e-12 ? 50 : 100;
                return true;
            }

            var relativeStrength = averageGain / averageLoss;
            rsi = 100.0 - 100.0 / (1.0 + relativeStrength);
            return double.IsFinite(rsi);
        }

        private static bool TryGetPreviousExtreme(
            List<string[]> rows, int column, int period, bool highest,
            out double extreme)
        {
            extreme = highest ? double.NegativeInfinity : double.PositiveInfinity;
            if (period <= 0 || rows.Count <= period || column <= 0)
                return false;

            // Exclude the current candle: the reference level must be formed by prior candles.
            for (var offset = 1; offset <= period; offset++)
            {
                var row = rows[rows.Count - 1 - offset];
                if (column > row.Length ||
                    !TryParseTradingNumber(row[column - 1], out var value) ||
                    !double.IsFinite(value))
                    return false;
                extreme = highest ? Math.Max(extreme, value) : Math.Min(extreme, value);
            }

            return double.IsFinite(extreme);
        }

        private static bool TryGetVolumeRatio(
            List<string[]> rows, int volumeColumn, int period, out double ratio)
        {
            ratio = 0;
            if (period <= 0 || rows.Count <= period || volumeColumn <= 0)
                return false;

            if (!TryReadAtOffset(rows, volumeColumn, 0, out var latestVolume) || latestVolume < 0)
                return false;

            var sum = 0.0;
            for (var offset = 1; offset <= period; offset++)
            {
                if (!TryReadAtOffset(rows, volumeColumn, offset, out var volume) || volume < 0)
                    return false;
                sum += volume;
            }

            var average = sum / period;
            if (average <= 0)
                return false;

            ratio = latestVolume / average;
            return double.IsFinite(ratio);
        }

        private static bool TryCalculateAdx(
            List<string[]> rows, int highColumn, int lowColumn, int closeColumn,
            int period, out double adx, out double positiveDi, out double negativeDi)
        {
            adx = 0;
            positiveDi = 0;
            negativeDi = 0;
            if (period <= 0 || rows.Count < period * 5 + 1)
                return false;

            var startIndex = rows.Count - (period * 5 + 1);
            var sampleCount = rows.Count - startIndex;
            var highs = new double[sampleCount];
            var lows = new double[sampleCount];
            var closes = new double[sampleCount];
            for (var i = 0; i < sampleCount; i++)
            {
                var row = rows[startIndex + i];
                if (highColumn > row.Length || lowColumn > row.Length ||
                    closeColumn > row.Length ||
                    !TryParseTradingNumber(row[highColumn - 1], out highs[i]) ||
                    !TryParseTradingNumber(row[lowColumn - 1], out lows[i]) ||
                    !TryParseTradingNumber(row[closeColumn - 1], out closes[i]) ||
                    !double.IsFinite(highs[i]) || !double.IsFinite(lows[i]) ||
                    !double.IsFinite(closes[i]) || highs[i] < lows[i])
                    return false;
            }

            var tr = new double[sampleCount];
            var plusDm = new double[sampleCount];
            var minusDm = new double[sampleCount];
            for (var i = 1; i < sampleCount; i++)
            {
                var upMove = highs[i] - highs[i - 1];
                var downMove = lows[i - 1] - lows[i];
                plusDm[i] = upMove > downMove && upMove > 0 ? upMove : 0;
                minusDm[i] = downMove > upMove && downMove > 0 ? downMove : 0;
                tr[i] = Math.Max(highs[i] - lows[i],
                    Math.Max(Math.Abs(highs[i] - closes[i - 1]),
                             Math.Abs(lows[i] - closes[i - 1])));
            }

            var smoothTr = 0.0;
            var smoothPlus = 0.0;
            var smoothMinus = 0.0;
            for (var i = 1; i <= period; i++)
            {
                smoothTr += tr[i];
                smoothPlus += plusDm[i];
                smoothMinus += minusDm[i];
            }

            var dxValues = new List<double>();
            AddDx();
            for (var i = period + 1; i < sampleCount; i++)
            {
                smoothTr = smoothTr - smoothTr / period + tr[i];
                smoothPlus = smoothPlus - smoothPlus / period + plusDm[i];
                smoothMinus = smoothMinus - smoothMinus / period + minusDm[i];
                AddDx();
            }

            if (dxValues.Count < period)
                return false;

            var currentAdx = dxValues.Take(period).Average();
            for (var i = period; i < dxValues.Count; i++)
                currentAdx = (currentAdx * (period - 1) + dxValues[i]) / period;

            adx = currentAdx;
            if (smoothTr > 1e-12)
            {
                positiveDi = 100.0 * smoothPlus / smoothTr;
                negativeDi = 100.0 * smoothMinus / smoothTr;
            }
            return double.IsFinite(adx) && double.IsFinite(positiveDi) && double.IsFinite(negativeDi);

            void AddDx()
            {
                if (smoothTr <= 1e-12)
                {
                    dxValues.Add(0);
                    return;
                }

                var plusDi = 100.0 * smoothPlus / smoothTr;
                var minusDi = 100.0 * smoothMinus / smoothTr;
                var denominator = plusDi + minusDi;
                dxValues.Add(denominator <= 1e-12
                    ? 0
                    : 100.0 * Math.Abs(plusDi - minusDi) / denominator);
            }
        }

        private static int ReadInt(TextBox box) =>
            int.TryParse(NormalizeTradingDigits(box.Text).Trim(), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var value) ? value : 0;

        private static double ReadDouble(TextBox box) =>
            double.TryParse(NormalizeTradingDigits(box.Text).Trim(), NumberStyles.Float,
                CultureInfo.InvariantCulture, out var value) && double.IsFinite(value) ? value : double.NaN;

        private void ResetFilters3()
        {
            maFilterEnabled3.Checked = false;
            rsiFilterEnabled3.Checked = false;
            distanceFilterEnabled3.Checked = false;
            volumePriceFilterEnabled3.Checked = false;
            trendStrengthFilterEnabled3.Checked = false;

            maFilterMode3.SelectedIndex = 0;
            maFilterPeriod1_3.Text = "20";
            maFilterPeriod2_3.Text = "50";
            rsiPeriod3.Text = "14";
            rsiOperator3.SelectedIndex = 0;
            rsiMinimum3.Text = "40";
            rsiMaximum3.Text = "70";
            distanceHighPeriod3.Text = "20";
            distanceHighMax3.Text = "8";
            distanceLowPeriod3.Text = "20";
            distanceLowMin3.Text = "2";
            volumePricePeriod3.Text = "20";
            volumePriceRatio3.Text = "1.3";
            volumePriceDirection3.SelectedIndex = 0;
            adxPeriod3.Text = "14";
            adxMinimum3.Text = "20";
        }

        private void UpdateFilters3Count(string text)
        {
            if (filterCountLabel3 != null)
                filterCountLabel3.Text = text;
        }
    }
}
