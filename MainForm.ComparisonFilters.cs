using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool comparisonFiltersInitialized;
        private bool comparisonFilterEventsAttached;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (comparisonFiltersInitialized)
                return;

            comparisonFiltersInitialized = true;
            ConfigureComparisonFilterControls();

            // Attach after MainForm.OnLoad has installed filters 1-4 so that
            // comparison filters are always applied after the existing pipeline.
            BeginInvoke(new Action(AttachComparisonFilterEvents));
        }

        private void ConfigureComparisonFilterControls()
        {
            var filters = new[]
            {
                (comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox,
                    comparisonFirstTextBox, comparisonSecondTextBox),
                (comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6,
                    comparisonFirstTextBox6, comparisonSecondTextBox6),
                (comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7,
                    comparisonFirstTextBox7, comparisonSecondTextBox7),
                (comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8,
                    comparisonFirstTextBox8, comparisonSecondTextBox8)
            };

            foreach (var filter in filters)
            {
                ReplaceFinalFeeWithFinal(filter.Item1);
                ReplaceFinalFeeWithFinal(filter.Item3);

                filter.Item1.DropDownStyle = ComboBoxStyle.DropDownList;
                filter.Item2.DropDownStyle = ComboBoxStyle.DropDownList;
                filter.Item3.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void AttachComparisonFilterEvents()
        {
            if (comparisonFilterEventsAttached || IsDisposed)
                return;

            comparisonFilterEventsAttached = true;

            var filters = new[]
            {
                (comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox,
                    comparisonFirstTextBox, comparisonSecondTextBox),
                (comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6,
                    comparisonFirstTextBox6, comparisonSecondTextBox6),
                (comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7,
                    comparisonFirstTextBox7, comparisonSecondTextBox7),
                (comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8,
                    comparisonFirstTextBox8, comparisonSecondTextBox8)
            };

            foreach (var filter in filters)
            {
                filter.Item1.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item2.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item3.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item4.TextChanged += ComparisonFilterChanged;
                filter.Item5.TextChanged += ComparisonFilterChanged;
            }

            // Re-apply filters 5-8 after filters 1-4 whenever their input changes.
            nameComboBox.SelectedIndexChanged += ComparisonBaseFilterChanged;
            nameTextBox.TextChanged += ComparisonBaseFilterChanged;
            volumeRatioTextBox.TextChanged += ComparisonBaseFilterChanged;
            volumeRatioOperatorComboBox.SelectedIndexChanged += ComparisonBaseFilterChanged;
            textBox1.TextChanged += ComparisonBaseFilterChanged;
            pastDaysTextBox.TextChanged += ComparisonBaseFilterChanged;
            pastDaysStatusComboBox.SelectedIndexChanged += ComparisonBaseFilterChanged;
            statusAllRadio.CheckedChanged += ComparisonBaseFilterChanged;
            statusPositiveRadio.CheckedChanged += ComparisonBaseFilterChanged;
            statusNegativeRadio.CheckedChanged += ComparisonBaseFilterChanged;
            portfolioComboBox.SelectedIndexChanged += ComparisonBaseFilterChanged;
            refreshButton.Click += ComparisonBaseFilterChanged;
            clearFiltersButton.Click += ComparisonBaseFilterChanged;

            ApplyComparisonFiltersToGridWithWaitCursor();
        }

        private static void ReplaceFinalFeeWithFinal(ComboBox comboBox)
        {
            for (var i = 0; i < comboBox.Items.Count; i++)
            {
                if (string.Equals(comboBox.Items[i]?.ToString()?.Trim(), "FINAL FEE", StringComparison.OrdinalIgnoreCase))
                    comboBox.Items[i] = "پایانی";
            }
        }

        private void ComparisonFilterChanged(object? sender, EventArgs e)
        {
            ApplyComparisonFiltersToGridWithWaitCursor();
        }

        private void ComparisonBaseFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters)
                return;

            ApplyComparisonFiltersToGridWithWaitCursor();
        }

        private void ApplyComparisonFiltersToGridWithWaitCursor()
        {
            if (!HasAnyActiveComparisonFilter())
                return;

            UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                ApplyComparisonFiltersToGrid();
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }
        }

        private void ApplyComparisonFiltersToGrid()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
                return;

            var symbols = GetDisplayedGridSymbols();
            IEnumerable<string> filtered = symbols;

            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox,
                comparisonOperatorComboBox, comparisonSecondComboBox,
                comparisonFirstTextBox, comparisonSecondTextBox);

            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox6,
                comparisonOperatorComboBox6, comparisonSecondComboBox6,
                comparisonFirstTextBox6, comparisonSecondTextBox6);

            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox7,
                comparisonOperatorComboBox7, comparisonSecondComboBox7,
                comparisonFirstTextBox7, comparisonSecondTextBox7);

            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox8,
                comparisonOperatorComboBox8, comparisonSecondComboBox8,
                comparisonFirstTextBox8, comparisonSecondTextBox8);

            var result = filtered.ToList();
            var totalCount = (definition.Symbols ?? new List<string>())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count();

            UpdateFilterCounts(result.Count, totalCount);

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

        private IEnumerable<string> GetDisplayedGridSymbols()
        {
            foreach (DataGridViewRow row in stocksDataGridView.Rows)
            {
                if (row.IsNewRow)
                    continue;

                var symbol = Convert.ToString(row.Cells.Count > 1 ? row.Cells[1].Value : null)?.Trim();
                if (!string.IsNullOrWhiteSpace(symbol))
                    yield return symbol;
            }
        }

        private bool HasAnyActiveComparisonFilter()
        {
            return IsComparisonFilterActive(comparisonFirstComboBox, comparisonOperatorComboBox,
                       comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox) ||
                   IsComparisonFilterActive(comparisonFirstComboBox6, comparisonOperatorComboBox6,
                       comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6) ||
                   IsComparisonFilterActive(comparisonFirstComboBox7, comparisonOperatorComboBox7,
                       comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7) ||
                   IsComparisonFilterActive(comparisonFirstComboBox8, comparisonOperatorComboBox8,
                       comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8);
        }

        private static bool IsComparisonFilterActive(
            ComboBox firstFieldComboBox,
            ComboBox operatorComboBox,
            ComboBox secondFieldComboBox,
            TextBox firstOffsetTextBox,
            TextBox secondOffsetTextBox)
        {
            return TryGetComparisonSettings(firstFieldComboBox, operatorComboBox, secondFieldComboBox,
                firstOffsetTextBox, secondOffsetTextBox,
                out _, out _, out _, out _, out _);
        }

        private IEnumerable<string> ApplyComparisonFilter(
            IEnumerable<string> symbols,
            PortfolioDefinition definition,
            ComboBox firstFieldComboBox,
            ComboBox operatorComboBox,
            ComboBox secondFieldComboBox,
            TextBox firstOffsetTextBox,
            TextBox secondOffsetTextBox)
        {
            if (!TryGetComparisonSettings(
                    firstFieldComboBox,
                    operatorComboBox,
                    secondFieldComboBox,
                    firstOffsetTextBox,
                    secondOffsetTextBox,
                    out var firstField,
                    out var op,
                    out var secondField,
                    out var firstOffset,
                    out var relativeOffset))
                return symbols;

            int totalOffset;
            try
            {
                totalOffset = checked(firstOffset + relativeOffset);
            }
            catch (OverflowException)
            {
                return Enumerable.Empty<string>();
            }

            return symbols.Where(symbol =>
                TryGetComparisonValues(definition, symbol, firstField, secondField, firstOffset, totalOffset,
                    out var left, out var right) &&
                CompareComparisonOperator(left, right, op));
        }

        private static bool TryGetComparisonSettings(
            ComboBox firstFieldComboBox,
            ComboBox operatorComboBox,
            ComboBox secondFieldComboBox,
            TextBox firstOffsetTextBox,
            TextBox secondOffsetTextBox,
            out string firstField,
            out string op,
            out string secondField,
            out int firstOffset,
            out int relativeOffset)
        {
            firstField = string.Empty;
            op = string.Empty;
            secondField = string.Empty;
            firstOffset = 0;
            relativeOffset = 0;

            if (firstFieldComboBox.SelectedItem == null ||
                operatorComboBox.SelectedItem == null ||
                secondFieldComboBox.SelectedItem == null)
                return false;

            if (!int.TryParse(
                    NormalizeTradingDigits(firstOffsetTextBox.Text).Trim(),
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out firstOffset) || firstOffset < 0)
                return false;

            if (!int.TryParse(
                    NormalizeTradingDigits(secondOffsetTextBox.Text).Trim(),
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out relativeOffset) || relativeOffset < 0)
                return false;

            firstField = NormalizeComparisonField(firstFieldComboBox.SelectedItem.ToString());
            secondField = NormalizeComparisonField(secondFieldComboBox.SelectedItem.ToString());
            op = operatorComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;

            return !string.IsNullOrEmpty(firstField) &&
                   !string.IsNullOrEmpty(secondField) &&
                   !string.IsNullOrEmpty(op);
        }

        private static string NormalizeComparisonField(string? value)
        {
            var normalized = (value ?? string.Empty).Trim();
            return normalized.ToUpperInvariant() switch
            {
                "O" => "باز",
                "H" => "بیشترین",
                "L" => "کمترین",
                "C" => "پایانی",
                "V" => "حجم",
                "FINAL FEE" => "پایانی",
                "پایانی" => "پایانی",
                _ => string.Empty
            };
        }

        private bool TryGetComparisonValues(
            PortfolioDefinition definition,
            string symbol,
            string firstField,
            string secondField,
            int firstOffset,
            int secondOffset,
            out double left,
            out double right)
        {
            left = 0;
            right = 0;

            var firstColumn = GetMappingColumn(definition, firstField);
            var secondColumn = GetMappingColumn(definition, secondField);
            if (firstColumn <= 0 || secondColumn <= 0 ||
                string.IsNullOrWhiteSpace(definition.DataPath) ||
                !Directory.Exists(definition.DataPath))
                return false;

            var rows = new List<(double? First, double? Second)>();
            var symbolColumn = GetMappingColumn(definition, "نماد");

            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                    ReadComparisonRows(definition, file, symbol, symbolColumn, firstColumn, secondColumn, rows);
            }
            catch
            {
                return false;
            }

            var firstIndex = rows.Count - 1 - firstOffset;
            var secondIndex = rows.Count - 1 - secondOffset;
            if (firstIndex < 0 || secondIndex < 0 ||
                firstIndex >= rows.Count || secondIndex >= rows.Count)
                return false;

            var first = rows[firstIndex].First;
            var second = rows[secondIndex].Second;
            if (!first.HasValue || !second.HasValue)
                return false;

            left = first.Value;
            right = second.Value;
            return !double.IsNaN(left) && !double.IsInfinity(left) &&
                   !double.IsNaN(right) && !double.IsInfinity(right);
        }

        private void ReadComparisonRows(
            PortfolioDefinition definition,
            string filePath,
            string symbol,
            int symbolColumn,
            int firstColumn,
            int secondColumn,
            List<(double? First, double? Second)> rows)
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

                if (firstColumn > row.Length || secondColumn > row.Length)
                {
                    rows.Add((null, null));
                    continue;
                }

                double? firstValue = TryParseTradingNumber(row[firstColumn - 1], out var firstParsed)
                    ? firstParsed
                    : null;
                double? secondValue = TryParseTradingNumber(row[secondColumn - 1], out var secondParsed)
                    ? secondParsed
                    : null;

                rows.Add((firstValue, secondValue));
            }
        }

        private static bool CompareComparisonOperator(double left, double right, string op)
        {
            return op.Trim() switch
            {
                ">" => left > right,
                "<" => left < right,
                "=" => Math.Abs(left - right) < 1e-12,
                ">=" => left >= right,
                "<=" => left <= right,
                "!=" => Math.Abs(left - right) >= 1e-12,
                _ => false
            };
        }
    }
}
