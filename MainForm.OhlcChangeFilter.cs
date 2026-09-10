using System.Globalization;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool ohlcChangeFilterEventsAttached;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            AttachOhlcChangeFilterEvents();
        }

        private void AttachOhlcChangeFilterEvents()
        {
            if (ohlcChangeFilterEventsAttached || IsDisposed)
                return;

            ohlcChangeFilterEventsAttached = true;
            ohlcChangeFieldComboBox.SelectedIndexChanged += OhlcChangeFilterChanged;
            ohlcChangeDaysTextBox.TextChanged += OhlcChangeFilterChanged;
            ohlcChangePercentTextBox.TextChanged += OhlcChangeFilterChanged;
            ohlcChangeDirectionComboBox.SelectedIndexChanged += OhlcChangeFilterChanged;

            nameComboBox.SelectedIndexChanged += OhlcBaseFilterChanged;
            nameTextBox.TextChanged += OhlcBaseFilterChanged;
            volumeRatioTextBox.TextChanged += OhlcBaseFilterChanged;
            volumeRatioOperatorComboBox.SelectedIndexChanged += OhlcBaseFilterChanged;
            textBox1.TextChanged += OhlcBaseFilterChanged;
            pastDaysTextBox.TextChanged += OhlcBaseFilterChanged;
            pastDaysStatusComboBox.SelectedIndexChanged += OhlcBaseFilterChanged;
            statusAllRadio.CheckedChanged += OhlcBaseFilterChanged;
            statusPositiveRadio.CheckedChanged += OhlcBaseFilterChanged;
            statusNegativeRadio.CheckedChanged += OhlcBaseFilterChanged;
            portfolioComboBox.SelectedIndexChanged += OhlcBaseFilterChanged;
            refreshButton.Click += OhlcBaseFilterChanged;
            clearFiltersButton.Click += OhlcBaseFilterChanged;

            UpdateOhlcChangeFilterAvailability();
        }

        private void OhlcChangeFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters) return;
            UpdateOhlcChangeFilterAvailability();
            ApplyOhlcChangeFilterToGridWithWaitCursor();
        }

        private void OhlcBaseFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters) return;
            UpdateOhlcChangeFilterAvailability();
            ApplyOhlcChangeFilterToGridWithWaitCursor();
        }

        private void UpdateOhlcChangeFilterAvailability()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
            {
                ohlcChangeFilterGroup.Enabled = false;
                return;
            }

            var field = NormalizeOhlcChangeField(ohlcChangeFieldComboBox.SelectedItem?.ToString());
            ohlcChangeFilterGroup.Enabled = !string.IsNullOrEmpty(field) && GetMappingColumn(definition, field) > 0;
        }

        private void ApplyOhlcChangeFilterToGridWithWaitCursor()
        {
            if (!IsOhlcChangeFilterActive()) return;
            UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                ApplyOhlcChangeFilterToGrid();
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }
        }

        private bool IsOhlcChangeFilterActive()
        {
            if (ohlcChangeFieldComboBox.SelectedItem == null || ohlcChangeDirectionComboBox.SelectedItem == null)
                return false;
            if (string.IsNullOrEmpty(NormalizeOhlcChangeField(ohlcChangeFieldComboBox.SelectedItem.ToString())))
                return false;
            if (!int.TryParse(NormalizeTradingDigits(ohlcChangeDaysTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0)
                return false;
            if (!double.TryParse(NormalizeTradingDigits(ohlcChangePercentTextBox.Text).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent) || percent < 0)
                return false;
            return GetOhlcChangeDirection() >= 0;
        }

        private int GetOhlcChangeDirection()
        {
            var value = ohlcChangeDirectionComboBox.SelectedItem?.ToString()?.Trim() ?? string.Empty;
            return value.Contains("رشد", StringComparison.Ordinal) ? 1 : value.Contains("افت", StringComparison.Ordinal) ? -1 : -1;
        }

        private void ApplyOhlcChangeFilterToGrid()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition)) return;

            var field = NormalizeOhlcChangeField(ohlcChangeFieldComboBox.SelectedItem?.ToString());
            if (string.IsNullOrEmpty(field)) return;
            if (!int.TryParse(NormalizeTradingDigits(ohlcChangeDaysTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0) return;
            if (!double.TryParse(NormalizeTradingDigits(ohlcChangePercentTextBox.Text).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent) || percent < 0) return;

            var direction = GetOhlcChangeDirection();
            if (direction < 0) return;

            var symbols = GetDisplayedGridSymbolsForOhlcFilter().ToList();
            var result = symbols.Where(symbol =>
                TryGetOhlcChange(definition, symbol, field, n, out var changePercent) &&
                (direction > 0 ? changePercent >= percent : changePercent <= -percent)).ToList();

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

        private IEnumerable<string> GetDisplayedGridSymbolsForOhlcFilter()
        {
            foreach (DataGridViewRow row in stocksDataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                var symbol = Convert.ToString(row.Cells.Count > 1 ? row.Cells[1].Value : null)?.Trim();
                if (!string.IsNullOrWhiteSpace(symbol)) yield return symbol;
            }
        }

        private bool TryGetOhlcChange(PortfolioDefinition definition, string symbol, string field, int n, out double changePercent)
        {
            changePercent = 0;
            var column = GetMappingColumn(definition, field);
            if (column <= 0 || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath)) return false;

            var values = new List<double>();
            var symbolColumn = GetMappingColumn(definition, "نماد");
            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                    ReadOhlcValues(definition, file, symbol, symbolColumn, column, values);
            }
            catch { return false; }

            if (values.Count <= n) return false;
            var latest = values[^1];
            var previous = values[values.Count - 1 - n];
            if (previous == 0 || double.IsNaN(latest) || double.IsInfinity(latest) || double.IsNaN(previous) || double.IsInfinity(previous)) return false;

            changePercent = (latest - previous) / Math.Abs(previous) * 100.0;
            return !double.IsNaN(changePercent) && !double.IsInfinity(changePercent);
        }

        private void ReadOhlcValues(PortfolioDefinition definition, string filePath, string symbol, int symbolColumn, int valueColumn, List<double> values)
        {
            var firstLine = true;
            foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var row = SplitTradingDataLine(line, definition.Separator);
                if (firstLine && definition.HasHeader) { firstLine = false; continue; }
                firstLine = false;

                if (definition.SymbolSource == SymbolSource.InsideFile &&
                    (symbolColumn <= 0 || symbolColumn > row.Length ||
                     !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase)))
                    continue;

                if (valueColumn > row.Length) continue;
                if (TryParseTradingNumber(row[valueColumn - 1], out var value))
                    values.Add(value);
            }
        }

        private static string NormalizeOhlcChangeField(string? value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant() switch
            {
                "O" => "باز",
                "H" => "بیشترین",
                "L" => "کمترین",
                "C" => "پایانی",
                "FINAL FEE" => "پایانی",
                "پایانی" => "پایانی",
                _ => string.Empty
            };
        }
    }
}