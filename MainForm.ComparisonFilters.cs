using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool comparisonFiltersInitialized;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (comparisonFiltersInitialized)
                return;

            comparisonFiltersInitialized = true;
            ConfigureComparisonFilterControls();
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

                filter.Item1.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item2.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item3.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item4.TextChanged += ComparisonFilterChanged;
                filter.Item5.TextChanged += ComparisonFilterChanged;
            }
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
            ApplyTradingStatusFilterWithWaitCursor();
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

            var totalOffset = checked(firstOffset + relativeOffset);
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

        private IEnumerable<string> ApplyComparisonFilter5(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            return ApplyComparisonFilter(symbols, definition, comparisonFirstComboBox, comparisonOperatorComboBox,
                comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox);
        }

        private IEnumerable<string> ApplyComparisonFilter6(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            return ApplyComparisonFilter(symbols, definition, comparisonFirstComboBox6, comparisonOperatorComboBox6,
                comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6);
        }

        private IEnumerable<string> ApplyComparisonFilter7(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            return ApplyComparisonFilter(symbols, definition, comparisonFirstComboBox7, comparisonOperatorComboBox7,
                comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7);
        }

        private IEnumerable<string> ApplyComparisonFilter8(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            return ApplyComparisonFilter(symbols, definition, comparisonFirstComboBox8, comparisonOperatorComboBox8,
                comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8);
        }
    }
}
