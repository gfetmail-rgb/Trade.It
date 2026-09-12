using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private List<TradingChartPoint> LoadChartData(PortfolioDefinition definition, string symbol)
        {
            var result = new List<TradingChartPoint>();

            if (definition == null || string.IsNullOrWhiteSpace(symbol) ||
                string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath) ||
                definition.NoDateTime)
                return result;

            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0) dateColumn = GetMappingColumn(definition, "تاریخ لاتین");

            var timeColumn = GetMappingColumn(definition, "زمان");
            if (timeColumn <= 0) timeColumn = GetMappingColumn(definition, "ساعت لاتین");

            var openColumn = GetMappingColumn(definition, "باز");
            var highColumn = GetMappingColumn(definition, "بیشترین");
            var lowColumn = GetMappingColumn(definition, "کمترین");
            var closeColumn = GetMappingColumn(definition, "پایانی");
            if (closeColumn <= 0) closeColumn = GetMappingColumn(definition, "قیمت پایانی بورس");
            var volumeColumn = GetMappingColumn(definition, "حجم");
            if (volumeColumn <= 0) volumeColumn = GetMappingColumn(definition, "حجم معاملات");

            if (dateColumn <= 0 || openColumn <= 0 || highColumn <= 0 || lowColumn <= 0 || closeColumn <= 0)
                return result;

            var symbolColumn = GetMappingColumn(definition, "نماد");

            try
            {
                foreach (var filePath in GetSymbolFiles(definition, symbol))
                {
                    var firstLine = true;
                    foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var row = SplitTradingDataLine(line, definition.Separator);
                        if (firstLine && definition.HasHeader)
                        {
                            firstLine = false;
                            continue;
                        }
                        firstLine = false;

                        if (definition.SymbolSource == SymbolSource.InsideFile &&
                            (symbolColumn <= 0 || symbolColumn > row.Length ||
                             !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase)))
                            continue;

                        var requiredMaxColumn = Math.Max(Math.Max(openColumn, highColumn), Math.Max(lowColumn, closeColumn));
                        if (dateColumn > row.Length || requiredMaxColumn > row.Length)
                            continue;

                        if (!TryParseChartDate(row[dateColumn - 1], definition, out var date))
                            continue;

                        if (timeColumn > 0 && timeColumn <= row.Length && TryParseChartTime(row[timeColumn - 1], out var time))
                            date = date.Date.Add(time);

                        if (!TryParseTradingNumber(row[openColumn - 1], out var open) ||
                            !TryParseTradingNumber(row[highColumn - 1], out var high) ||
                            !TryParseTradingNumber(row[lowColumn - 1], out var low) ||
                            !TryParseTradingNumber(row[closeColumn - 1], out var close))
                            continue;

                        var volume = 0d;
                        if (volumeColumn > 0 && volumeColumn <= row.Length)
                            TryParseTradingNumber(row[volumeColumn - 1], out volume);

                        if (double.IsNaN(open) || double.IsInfinity(open) ||
                            double.IsNaN(high) || double.IsInfinity(high) ||
                            double.IsNaN(low) || double.IsInfinity(low) ||
                            double.IsNaN(close) || double.IsInfinity(close))
                            continue;

                        result.Add(new TradingChartPoint
                        {
                            Date = date,
                            Open = open,
                            High = high,
                            Low = low,
                            Close = close,
                            Volume = double.IsNaN(volume) || double.IsInfinity(volume) ? 0d : volume
                        });
                    }
                }
            }
            catch
            {
                return new List<TradingChartPoint>();
            }

            return result
                .OrderBy(x => x.Date)
                .ToList();
        }

        private static bool TryParseChartDate(string value, PortfolioDefinition definition, out DateTime date)
        {
            date = default;
            var normalized = NormalizeTradingDigits(value).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            var parts = normalized.Split(new[] { '/', '-', '.', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int year;
            int month;
            int day;

            if (parts.Length >= 3 &&
                int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) &&
                int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out month) &&
                int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out day))
            {
            }
            else
            {
                var digits = new string(normalized.Where(char.IsDigit).ToArray());
                if (digits.Length < 8)
                    return false;

                if (!int.TryParse(digits[..4], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) ||
                    !int.TryParse(digits.Substring(4, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out month) ||
                    !int.TryParse(digits.Substring(6, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out day))
                    return false;
            }

            try
            {
                date = definition.Calendar == InputCalendar.Gregorian
                    ? new DateTime(year, month, day)
                    : new PersianCalendar().ToDateTime(year, month, day, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryParseChartTime(string value, out TimeSpan time)
        {
            time = TimeSpan.Zero;
            var normalized = NormalizeTradingDigits(value).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            if (TimeSpan.TryParse(normalized, CultureInfo.InvariantCulture, out time))
                return time >= TimeSpan.Zero && time < TimeSpan.FromDays(1);

            var digits = new string(normalized.Where(char.IsDigit).ToArray());
            if (digits.Length < 4)
                return false;

            if (!int.TryParse(digits[..2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var hour) ||
                !int.TryParse(digits.Substring(2, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out var minute))
                return false;

            var second = 0;
            if (digits.Length >= 6 && !int.TryParse(digits.Substring(4, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out second))
                return false;

            if (hour is < 0 or > 23 || minute is < 0 or > 59 || second is < 0 or > 59)
                return false;

            time = new TimeSpan(hour, minute, second);
            return true;
        }
    }
}
