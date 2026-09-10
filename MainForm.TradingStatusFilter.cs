using System.Globalization;
using System.Text;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool tradingStatusFilterInitialized;
        private Label? filterCountLabel;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (tradingStatusFilterInitialized)
                return;

            tradingStatusFilterInitialized = true;
            InitializeFilterStatusDisplay();
            statusAllRadio.CheckedChanged += TradingStatusFilterChanged;
            statusPositiveRadio.CheckedChanged += TradingStatusFilterChanged;
            statusNegativeRadio.CheckedChanged += TradingStatusFilterChanged;
            portfolioComboBox.SelectedIndexChanged += TradingStatusPortfolioChanged;
            refreshButton.Click += TradingStatusRefreshChanged;
        }

        private void InitializeFilterStatusDisplay()
        {
            var parent = clearFiltersButton.Parent;
            if (parent == null)
                return;

            clearFiltersButton.Width = 100;
            clearFiltersButton.Location = new Point(8, clearFiltersButton.Top);

            filterCountLabel = new Label
            {
                AutoSize = false,
                Width = 205,
                Height = 36,
                Text = "کل: ۰    پیدا شده: ۰",
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.None,
                RightToLeft = RightToLeft.Yes,
                Font = new Font("Segoe UI", 9F)
            };

            filterCountLabel.Location = new Point(clearFiltersButton.Right + 4, clearFiltersButton.Top);
            parent.Controls.Add(filterCountLabel);
            filterCountLabel.BringToFront();
            clearFiltersButton.BringToFront();
        }

        private void TradingStatusFilterChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton radio && !radio.Checked)
                return;
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void TradingStatusPortfolioChanged(object? sender, EventArgs e)
        {
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void TradingStatusRefreshChanged(object? sender, EventArgs e)
        {
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void ApplyTradingStatusFilterWithWaitCursor()
        {
            UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                ApplyTradingStatusFilter();
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }
        }

        private void ApplyTradingStatusFilter()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
            {
                UpdateFilterCounts(0, 0);
                return;
            }

            var symbols = (definition.Symbols ?? new List<string>())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            IEnumerable<string> filtered = symbols;

            if (!statusAllRadio.Checked)
            {
                var todaySymbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var symbol in symbols)
                {
                    if (HasTradeOnToday(definition, symbol))
                        todaySymbols.Add(symbol);
                }

                var showTraded = statusPositiveRadio.Checked;
                filtered = symbols.Where(symbol => showTraded == todaySymbols.Contains(symbol));
            }

            var result = filtered.ToList();
            UpdateFilterCounts(result.Count, symbols.Count);

            internalPortfolioUpdate = true;
            try
            {
                stocksDataGridView.Rows.Clear();
                for (var i = 0; i < result.Count; i++)
                    stocksDataGridView.Rows.Add(i + 1, result[i], "", false);
            }
            finally
            {
                internalPortfolioUpdate = false;
            }

            selectAllCheckBox.Checked = false;
            selectNoneCheckBox.Checked = result.Count > 0;
            UpdateSelectionControls();
        }

        private void UpdateFilterCounts(int foundCount, int totalCount)
        {
            if (filterCountLabel != null)
                filterCountLabel.Text = $"کل: {ToPersianDigits(totalCount.ToString())}    پیدا شده: {ToPersianDigits(foundCount.ToString())}";
        }

        private bool HasTradeOnToday(PortfolioDefinition definition, string symbol)
        {
            if (definition.NoDateTime || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath))
                return false;

            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0)
                dateColumn = GetMappingColumn(definition, "تاریخ لاتین");
            if (dateColumn <= 0)
                return false;

            var symbolColumn = GetMappingColumn(definition, "نماد");

            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                {
                    if (FileContainsToday(definition, file, symbol, symbolColumn, dateColumn))
                        return true;
                }
            }
            catch
            {
            }

            return false;
        }

        private IEnumerable<string> GetSymbolFiles(PortfolioDefinition definition, string symbol)
        {
            var extension = definition.FileType?.Trim().ToUpperInvariant() switch
            {
                "CSV" => ".csv",
                "PRN" => ".prn",
                _ => ".txt"
            };

            if (definition.SymbolSource == SymbolSource.FileName)
            {
                var direct = Path.Combine(definition.DataPath, symbol + extension);
                if (File.Exists(direct))
                {
                    yield return direct;
                    yield break;
                }

                foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly))
                {
                    if (string.Equals(Path.GetFileNameWithoutExtension(file), symbol, StringComparison.OrdinalIgnoreCase))
                        yield return file;
                }

                yield break;
            }

            foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly))
                yield return file;
        }

        private bool FileContainsToday(PortfolioDefinition definition, string filePath, string symbol, int symbolColumn, int dateColumn)
        {
            var lines = File.ReadLines(filePath, DetectTradingDataEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line));

            var firstLine = true;
            foreach (var line in lines)
            {
                var row = SplitTradingDataLine(line, definition.Separator);

                if (firstLine && definition.HasHeader)
                {
                    firstLine = false;
                    continue;
                }
                firstLine = false;

                if (definition.SymbolSource == SymbolSource.InsideFile)
                {
                    if (symbolColumn <= 0 || symbolColumn > row.Length ||
                        !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                if (dateColumn <= row.Length && IsSourceDateToday(row[dateColumn - 1], definition))
                    return true;
            }

            return false;
        }

        private static int GetMappingColumn(PortfolioDefinition definition, string field)
        {
            var mapping = definition.Mappings?.FirstOrDefault(m =>
                string.Equals(m.Field?.Trim(), field, StringComparison.OrdinalIgnoreCase));
            return mapping?.Column ?? 0;
        }

        private static bool IsSourceDateToday(string value, PortfolioDefinition definition)
        {
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
                var today = DateTime.Today;
                var persian = new PersianCalendar();

                if (definition.Calendar == InputCalendar.Gregorian)
                {
                    var sourceGregorian = new DateTime(year, month, day);
                    return persian.GetYear(sourceGregorian) == persian.GetYear(today) &&
                           persian.GetMonth(sourceGregorian) == persian.GetMonth(today) &&
                           persian.GetDayOfMonth(sourceGregorian) == persian.GetDayOfMonth(today);
                }

                var sourcePersian = persian.ToDateTime(year, month, day, 0, 0, 0, 0);
                return persian.GetYear(sourcePersian) == persian.GetYear(today) &&
                       persian.GetMonth(sourcePersian) == persian.GetMonth(today) &&
                       persian.GetDayOfMonth(sourcePersian) == persian.GetDayOfMonth(today);
            }
            catch
            {
                return false;
            }
        }

        private static string NormalizeTradingDigits(string value)
        {
            return value
                .Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3').Replace('۴', '4')
                .Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');
        }

        private static string[] SplitTradingDataLine(string line, string? separator)
        {
            return line.Split(new[] { string.IsNullOrEmpty(separator) ? "," : separator }, StringSplitOptions.None);
        }

        private static Encoding DetectTradingDataEncoding(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            if (stream.Length >= 3)
            {
                var bom = new byte[3];
                stream.ReadExactly(bom, 0, 3);
                if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                    return new UTF8Encoding(false);
            }

            return Encoding.UTF8;
        }
    }
}