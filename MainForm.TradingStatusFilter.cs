using System.Globalization;
using System.Text;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool tradingStatusFilterInitialized;
        private Label? filterCountLabel;
        private bool resettingFilters;

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
            nameComboBox.SelectedIndexChanged += NameFilterChanged;
            nameTextBox.TextChanged += NameFilterChanged;
            volumeRatioTextBox.TextChanged += AdditionalFilterChanged;
            volumeRatioOperatorComboBox.SelectedIndexChanged += AdditionalFilterChanged;
            textBox1.TextChanged += AdditionalFilterChanged;
            pastDaysTextBox.TextChanged += AdditionalFilterChanged;
            pastDaysStatusComboBox.SelectedIndexChanged += AdditionalFilterChanged;
            clearFiltersButton.Click += ClearFiltersButton_Click;
            controlTabControl.SelectedIndexChanged += FilterTabSelected;
            UpdateFilterControlAvailability();
        }

        private void InitializeFilterStatusDisplay()
        {
            var parent = clearFiltersButton.Parent;
            if (parent == null)
                return;

            ConfigureFilterComboBoxes();

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

        private void ConfigureFilterComboBoxes()
        {
            nameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comparisonFirstComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comparisonFirstComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

            comparisonFirstComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;

            comparisonFirstComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FilterTabSelected(object? sender, EventArgs e)
        {
            if (controlTabControl.SelectedTab == tabPage2)
            {
                nameComboBox.SelectedIndex = -1;
                UpdateFilterControlAvailability();
            }
        }

        private void ClearFiltersButton_Click(object? sender, EventArgs e)
        {
            resettingFilters = true;
            try
            {
                statusAllRadio.Checked = true;
                statusPositiveRadio.Checked = false;
                statusNegativeRadio.Checked = false;

                ResetFilterControls(tabPage2);

                statusAllRadio.Checked = true;
                nameComboBox.SelectedIndex = -1;
            }
            finally
            {
                resettingFilters = false;
            }

            UpdateFilterControlAvailability();
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private static void ResetFilterControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
                else if (control is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;
                else if (control is GroupBox || control is Panel)
                    ResetFilterControls(control);
            }
        }

        private void TradingStatusFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;

            if (sender is RadioButton radio && !radio.Checked)
                return;
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void TradingStatusPortfolioChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;
            UpdateFilterControlAvailability();
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void TradingStatusRefreshChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;
            UpdateFilterControlAvailability();
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void NameFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void AdditionalFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;
            ApplyTradingStatusFilterWithWaitCursor();
        }

        private void UpdateFilterControlAvailability()
        {
            PortfolioDefinition? definition = null;
            if (!string.IsNullOrWhiteSpace(displayedPortfolioName))
                loadedPortfolios.TryGetValue(displayedPortfolioName, out definition);

            var hasDate = HasDateColumn(definition);
            var hasVolume = HasVolumeColumn(definition);

            tradingStatusGroup.Enabled = hasDate;
            pastDaysGroup.Enabled = hasDate;
            volumeRatioGroup.Enabled = hasVolume;

            if (!hasDate)
            {
                statusAllRadio.Checked = true;
                pastDaysStatusComboBox.SelectedIndex = -1;
            }
        }

        private static bool HasDateColumn(PortfolioDefinition? definition)
        {
            if (definition == null || definition.NoDateTime)
                return false;

            return GetMappingColumn(definition, "تاریخ") > 0 ||
                   GetMappingColumn(definition, "تاریخ لاتین") > 0;
        }

        private static bool HasVolumeColumn(PortfolioDefinition? definition)
        {
            if (definition == null)
                return false;

            return GetMappingColumn(definition, "حجم") > 0 ||
                   GetMappingColumn(definition, "حجم معاملات") > 0;
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

            UpdateFilterControlAvailability();

            var symbols = (definition.Symbols ?? new List<string>())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            IEnumerable<string> filtered = symbols;

            if (HasDateColumn(definition) && !statusAllRadio.Checked)
            {
                var todaySymbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var symbol in symbols)
                {
                    if (HasTradeOnToday(definition, symbol))
                        todaySymbols.Add(symbol);
                }

                var showTraded = statusPositiveRadio.Checked;
                filtered = filtered.Where(symbol => showTraded == todaySymbols.Contains(symbol));
            }

            filtered = ApplyNameFilter(filtered);
            filtered = ApplyVolumeRatioFilter(filtered, definition);
            filtered = ApplyPastDaysFilter(filtered, definition);

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

        private IEnumerable<string> ApplyNameFilter(IEnumerable<string> symbols)
        {
            var phrase = NormalizeSymbolName(nameTextBox.Text);
            if (string.IsNullOrWhiteSpace(phrase))
                return symbols;

            var mode = nameComboBox.SelectedIndex;
            if (mode < 0)
                mode = 0;

            return symbols.Where(symbol => MatchesNameFilter(symbol, phrase, mode));
        }

        private static bool MatchesNameFilter(string symbol, string phrase, int mode)
        {
            var name = NormalizeSymbolName(symbol);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phrase))
                return true;

            var index = name.IndexOf(phrase, StringComparison.OrdinalIgnoreCase);
            var found = index >= 0;

            return mode switch
            {
                0 => found,
                1 => found && index == 0,
                2 => found && index + phrase.Length == name.Length,
                3 => found && index > 0 && index + phrase.Length < name.Length,
                4 => !found,
                _ => found
            };
        }

        private static string NormalizeSymbolName(string value)
        {
            return (value ?? string.Empty)
                .Trim()
                .Replace('ي', 'ی')
                .Replace('ى', 'ی')
                .Replace('ك', 'ک');
        }

        private void UpdateFilterCounts(int foundCount, int totalCount)
        {
            if (filterCountLabel != null)
                filterCountLabel.Text = $"کل: {ToPersianDigits(totalCount.ToString())}    پیدا شده: {ToPersianDigits(foundCount.ToString())}";
        }

        private IEnumerable<string> ApplyVolumeRatioFilter(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            if (!HasVolumeColumn(definition))
                return symbols;

            var ratioText = NormalizeTradingDigits(volumeRatioTextBox.Text).Trim();
            var nText = NormalizeTradingDigits(textBox1.Text).Trim();
            if (!double.TryParse(ratioText, NumberStyles.Float, CultureInfo.InvariantCulture, out var threshold) ||
                !int.TryParse(nText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0)
                return symbols;

            if (volumeRatioOperatorComboBox.SelectedIndex < 0 || volumeRatioOperatorComboBox.SelectedItem == null)
                return symbols;

            var op = volumeRatioOperatorComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;
            return symbols.Where(symbol =>
            {
                if (!TryGetLatestVolumeRatio(definition, symbol, n, out var ratio))
                    return false;
                return CompareNumeric(ratio, threshold, op);
            });
        }

        private bool TryGetLatestVolumeRatio(PortfolioDefinition definition, string symbol, int n, out double ratio)
        {
            ratio = 0;
            var volumeColumn = GetMappingColumn(definition, "حجم");
            if (volumeColumn <= 0)
                volumeColumn = GetMappingColumn(definition, "حجم معاملات");
            if (volumeColumn <= 0 || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath))
                return false;

            var volumes = new List<double>();
            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                    ReadVolumesFromFile(definition, file, symbol, volumeColumn, volumes);
            }
            catch
            {
                return false;
            }

            if (volumes.Count < n + 1)
                return false;

            var last = volumes[^1];
            var average = volumes.Skip(volumes.Count - n - 1).Take(n).Average();
            if (average <= 0 || double.IsNaN(last) || double.IsInfinity(last))
                return false;

            ratio = last / average;
            return !double.IsNaN(ratio) && !double.IsInfinity(ratio);
        }

        private void ReadVolumesFromFile(PortfolioDefinition definition, string filePath, string symbol, int volumeColumn, List<double> volumes)
        {
            var symbolColumn = GetMappingColumn(definition, "نماد");
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

                if (definition.SymbolSource == SymbolSource.InsideFile)
                {
                    if (symbolColumn <= 0 || symbolColumn > row.Length ||
                        !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                if (volumeColumn > row.Length)
                    continue;

                if (TryParseTradingNumber(row[volumeColumn - 1], out var volume))
                    volumes.Add(volume);
            }
        }

        private IEnumerable<string> ApplyPastDaysFilter(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            if (!HasDateColumn(definition))
                return symbols;

            var nText = NormalizeTradingDigits(pastDaysTextBox.Text).Trim();
            if (!int.TryParse(nText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n < 0)
                return symbols;

            if (pastDaysStatusComboBox.SelectedIndex < 0 || pastDaysStatusComboBox.SelectedItem == null)
                return symbols;

            var targetDate = DateTime.Today.AddDays(-n).Date;
            var op = pastDaysStatusComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;
            var showTraded = !IsNegativePastDaysOption(op);

            return symbols.Where(symbol =>
            {
                var traded = HasTradeOnDate(definition, symbol, targetDate);
                return showTraded == traded;
            });
        }

        private static bool IsNegativePastDaysOption(string value)
        {
            var normalized = NormalizeSymbolName(value);
            return normalized.Contains("نداشته", StringComparison.OrdinalIgnoreCase) ||
                   normalized.Contains("خیر", StringComparison.OrdinalIgnoreCase) ||
                   normalized.Contains("عدم", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasTradeOnDate(PortfolioDefinition definition, string symbol, DateTime targetDate)
        {
            if (!HasDateColumn(definition) || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath))
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
                    if (FileContainsDate(definition, file, symbol, symbolColumn, dateColumn, targetDate))
                        return true;
                }
            }
            catch
            {
            }

            return false;
        }

        private bool FileContainsDate(PortfolioDefinition definition, string filePath, string symbol, int symbolColumn, int dateColumn, DateTime targetDate)
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

                if (definition.SymbolSource == SymbolSource.InsideFile)
                {
                    if (symbolColumn <= 0 || symbolColumn > row.Length ||
                        !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                if (dateColumn <= row.Length && TryParseSourceDate(row[dateColumn - 1], definition, out var date) && date.Date == targetDate)
                    return true;
            }

            return false;
        }

        private bool HasTradeOnToday(PortfolioDefinition definition, string symbol)
        {
            return HasTradeOnDate(definition, symbol, DateTime.Today.Date);
        }

        private static bool TryParseSourceDate(string value, PortfolioDefinition definition, out DateTime date)
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
                if (definition.Calendar == InputCalendar.Gregorian)
                {
                    date = new DateTime(year, month, day);
                    return true;
                }

                date = new PersianCalendar().ToDateTime(year, month, day, 0, 0, 0, 0).Date;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool CompareNumeric(double left, double right, string op)
        {
            var normalized = NormalizeSymbolName(op);
            return normalized switch
            {
                ">" => left > right,
                ">=" => left >= right,
                "<" => left < right,
                "<=" => left <= right,
                "=" => Math.Abs(left - right) < 1e-12,
                "==" => Math.Abs(left - right) < 1e-12,
                "مساوی" => Math.Abs(left - right) < 1e-12,
                "بزرگتر از" => left > right,
                "بزرگتر" => left > right,
                "بزرگتر یا مساوی" => left >= right,
                "بزرگتر مساوی" => left >= right,
                "کوچکتر از" => left < right,
                "کوچکتر" => left < right,
                "کوچکتر یا مساوی" => left <= right,
                "کوچکتر مساوی" => left <= right,
                _ => false
            };
        }

        private static bool TryParseTradingNumber(string value, out double number)
        {
            var normalized = NormalizeTradingDigits(value)
                .Trim()
                .Replace(",", string.Empty)
                .Replace("٬", string.Empty)
                .Replace(" ", string.Empty);

            return double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out number);
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

        private static int GetMappingColumn(PortfolioDefinition definition, string field)
        {
            var mapping = definition.Mappings?.FirstOrDefault(m =>
                string.Equals(m.Field?.Trim(), field, StringComparison.OrdinalIgnoreCase));
            return mapping?.Column ?? 0;
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