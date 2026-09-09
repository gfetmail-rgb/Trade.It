using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Trade.It
{
    public partial class PortfolioDefinitionForm : Form
    {
        private const int MaxColumns = 18;
        private List<string[]> previewRows = new();
        private string[]? previewHeader;
        private bool internalUpdate;

        private static readonly string[] MappingFields =
        {
            "نماد", "تاریخ", "زمان", "باز", "بیشترین", "کمترین", "پایانی",
            "حجم", "قیمت پایانی بورس", "قیمت قبلی", "تعداد معاملات", "ارزش معاملات",
            "تعداد سهام", "ارزش بازار", "نماد لاتین", "دوره", "تاریخ لاتین", "ساعت لاتین"
        };

        public PortfolioDefinitionForm()
        {
            InitializeComponent();
            InitializeFormLogic();
        }

        private void InitializeFormLogic()
        {
            fileTypeComboBox.SelectedIndex = 0;
            separatorComboBox.SelectedIndex = 0;
            calendarComboBox.SelectedIndex = 0;
            dateFormatComboBox.SelectedIndex = 0;
            timeFormatComboBox.SelectedIndex = 0;
            headerCheckBox.Checked = true;

            mappingGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            mappingGrid.Columns["mappingFieldColumn"].ReadOnly = true;

            previewGrid.AutoGenerateColumns = false;
            previewGrid.ReadOnly = true;

            browseButton.Click += BrowseButton_Click;
            dataPathTextBox.TextChanged += DataSettingsChanged;
            fileTypeComboBox.SelectedIndexChanged += DataSettingsChanged;
            separatorComboBox.SelectedIndexChanged += DataSettingsChanged;
            calendarComboBox.SelectedIndexChanged += DataSettingsChanged;
            dateFormatComboBox.SelectedIndexChanged += DataSettingsChanged;
            timeFormatComboBox.SelectedIndexChanged += DataSettingsChanged;
            headerCheckBox.CheckedChanged += DataSettingsChanged;
            noDateTimeCheckBox.CheckedChanged += DataSettingsChanged;
            fileNameRadioButton.CheckedChanged += SymbolSourceChanged;
            insideFileRadioButton.CheckedChanged += SymbolSourceChanged;
            symbolSearchTextBox.TextChanged += SymbolSearchTextBox_TextChanged;
            selectAllButton.Click += (_, _) => SetAllSymbols(true);
            deselectAllButton.Click += (_, _) => SetAllSymbols(false);
            symbolGrid.CurrentCellDirtyStateChanged += SymbolGrid_CurrentCellDirtyStateChanged;
            symbolGrid.CellValueChanged += SymbolGrid_CellValueChanged;
            symbolGrid.CellDoubleClick += SymbolGrid_CellDoubleClick;
            mappingGrid.CellValidating += MappingGrid_CellValidating;
            testMappingButton.Click += TestMappingButton_Click;
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (_, _) => Close();
            resetButton.Click += ResetButton_Click;

            InitializeMappingRows();
            ClearPreviewAndMapping();
            UpdateControlState();
        }

        private void MappingGrid_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != mappingGrid.Columns["mappingNumberColumn"].Index)
                return;

            var text = Convert.ToString(e.FormattedValue)?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(text))
                return;

            if (!text.All(char.IsDigit))
            {
                e.Cancel = true;
                MessageBox.Show(this,
                    "شماره ستون فقط باید عدد صحیح باشد.",
                    "Mapping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out var value) ||
                value < 1 || value > MaxColumns)
            {
                e.Cancel = true;
                MessageBox.Show(this,
                    $"شماره ستون باید بین ۱ تا {MaxColumns} باشد.",
                    "Mapping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void InitializeMappingRows()
        {
            mappingGrid.Rows.Clear();
            foreach (var field in MappingFields)
                mappingGrid.Rows.Add(field, string.Empty);
        }

        private void BrowseButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "پوشه داده‌های سبد را انتخاب کنید",
                ShowNewFolderButton = false
            };

            if (Directory.Exists(dataPathTextBox.Text))
                dialog.SelectedPath = dataPathTextBox.Text;

            if (dialog.ShowDialog(this) == DialogResult.OK)
                dataPathTextBox.Text = dialog.SelectedPath;
        }

        private void DataSettingsChanged(object? sender, EventArgs e)
        {
            if (internalUpdate)
                return;

            UpdateControlState();
            if (Directory.Exists(dataPathTextBox.Text))
                LoadSymbolsAndPreview();
        }

        private void SymbolSourceChanged(object? sender, EventArgs e)
        {
            if (internalUpdate)
                return;

            if (Directory.Exists(dataPathTextBox.Text))
                LoadSymbolsAndPreview();
        }

        private IEnumerable<string> GetDataFiles()
        {
            if (!Directory.Exists(dataPathTextBox.Text))
                yield break;

            var extension = fileTypeComboBox.SelectedItem?.ToString()?.ToUpperInvariant() switch
            {
                "CSV" => ".csv",
                "PRN" => ".prn",
                _ => ".txt"
            };

            foreach (var file in Directory.EnumerateFiles(dataPathTextBox.Text, "*" + extension, SearchOption.TopDirectoryOnly)
                         .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
                yield return file;
        }

        private void LoadSymbolsAndPreview()
        {
            try
            {
                var files = GetDataFiles().ToList();
                PopulateSymbolGrid(files);

                var firstFile = files.FirstOrDefault();
                if (firstFile == null)
                    ClearPreviewAndMapping();
                else
                    LoadPreview(firstFile);
            }
            catch (Exception ex)
            {
                ClearPreviewAndMapping();
                MessageBox.Show(this, $"خواندن پوشه داده‌ها انجام نشد:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateSymbolGrid(IReadOnlyList<string> files)
        {
            internalUpdate = true;
            try
            {
                symbolGrid.Rows.Clear();

                if (fileNameRadioButton.Checked)
                {
                    foreach (var file in files)
                    {
                        var rowIndex = symbolGrid.Rows.Add(false, Path.GetFileNameWithoutExtension(file));
                        symbolGrid.Rows[rowIndex].Tag = file;
                    }
                }
                else
                {
                    var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var file in files)
                    {
                        foreach (var symbol in ExtractSymbolsFromFile(file))
                        {
                            if (!seen.Add(symbol))
                                continue;

                            var rowIndex = symbolGrid.Rows.Add(false, symbol);
                            symbolGrid.Rows[rowIndex].Tag = file;
                        }
                    }
                }

                UpdateSelectedCount();
            }
            finally
            {
                internalUpdate = false;
            }

            ApplySymbolFilter();
        }

        private IEnumerable<string> ExtractSymbolsFromFile(string filePath)
        {
            var lines = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Take(101)
                .ToList();

            if (lines.Count == 0)
                yield break;

            var rows = lines.Select(line => SplitLine(line, GetSeparator())).ToList();
            var header = headerCheckBox.Checked ? rows[0] : null;
            var dataRows = rows.Skip(headerCheckBox.Checked ? 1 : 0).ToList();
            var symbolColumn = FindMappingColumn("نماد", header, dataRows);

            if (symbolColumn <= 0)
                yield break;

            foreach (var row in dataRows)
            {
                if (symbolColumn > row.Length)
                    continue;

                var value = row[symbolColumn - 1].Trim();
                if (!string.IsNullOrWhiteSpace(value))
                    yield return value;
            }
        }

        private void SymbolGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= symbolGrid.Rows.Count)
                return;

            var file = symbolGrid.Rows[e.RowIndex].Tag as string;
            if (!string.IsNullOrWhiteSpace(file) && File.Exists(file))
                LoadPreview(file);
        }

        private void SymbolSearchTextBox_TextChanged(object? sender, EventArgs e) => ApplySymbolFilter();

        private void ApplySymbolFilter()
        {
            var query = symbolSearchTextBox.Text.Trim();
            foreach (DataGridViewRow row in symbolGrid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                var value = Convert.ToString(row.Cells["symbolColumn"].Value) ?? string.Empty;
                row.Visible = query.Length == 0 || value.Contains(query, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void SetAllSymbols(bool selected)
        {
            internalUpdate = true;
            try
            {
                foreach (DataGridViewRow row in symbolGrid.Rows)
                {
                    if (!row.IsNewRow && row.Visible)
                        row.Cells["selectedColumn"].Value = selected;
                }
            }
            finally
            {
                internalUpdate = false;
            }

            UpdateSelectedCount();
        }

        private void SymbolGrid_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (symbolGrid.IsCurrentCellDirty)
                symbolGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void SymbolGrid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (!internalUpdate && e.RowIndex >= 0)
                UpdateSelectedCount();
        }

        private void UpdateSelectedCount()
        {
            var total = symbolGrid.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
            var selected = symbolGrid.Rows.Cast<DataGridViewRow>()
                .Count(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["selectedColumn"].Value ?? false));
            selectedCountLabel.Text = $"انتخاب شده: {selected:N0} از {total:N0}";
        }

        private void LoadPreview(string filePath)
        {
            var lines = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Take(101)
                .ToList();

            if (lines.Count == 0)
            {
                ClearPreviewAndMapping();
                return;
            }

            var rows = lines.Select(line => SplitLine(line, GetSeparator())).ToList();
            var width = Math.Min(MaxColumns, rows.Max(r => r.Length));
            if (width <= 0)
            {
                ClearPreviewAndMapping();
                return;
            }

            previewHeader = headerCheckBox.Checked ? NormalizeWidth(rows[0], width) : null;
            var dataStart = headerCheckBox.Checked ? 1 : 0;
            previewRows = rows.Skip(dataStart).Take(100)
                .Select(r => NormalizeWidth(r, width))
                .ToList();

            BuildPreviewGrid(width);
            AutoMap(width);
        }

        private void BuildPreviewGrid(int width)
        {
            previewGrid.Rows.Clear();

            foreach (var sourceRow in previewRows)
            {
                var values = new object[MaxColumns];
                for (var column = 0; column < MaxColumns; column++)
                    values[column] = column < width ? sourceRow[column] : string.Empty;
                previewGrid.Rows.Add(values);
            }

            for (var column = 0; column < MaxColumns; column++)
            {
                var title = previewHeader != null && column < previewHeader.Length
                    ? previewHeader[column]
                    : string.Empty;
                previewGrid.Columns[column].HeaderText = string.IsNullOrWhiteSpace(title)
                    ? $"ستون {column + 1}"
                    : $"{column + 1}: {title}";
                previewGrid.Columns[column].Visible = column < width;
            }
        }

        private void AutoMap(int width)
        {
            var suggestions = new Dictionary<string, int>(StringComparer.Ordinal);

            for (var column = 0; column < width; column++)
            {
                var header = previewHeader != null && column < previewHeader.Length
                    ? previewHeader[column]
                    : string.Empty;
                var values = previewRows.Select(r => r[column])
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Take(50)
                    .ToList();

                var field = DetectField(header, values);
                if (field != null && !suggestions.ContainsKey(field))
                    suggestions[field] = column + 1;
            }

            for (var rowIndex = 0; rowIndex < MappingFields.Length; rowIndex++)
            {
                var field = MappingFields[rowIndex];
                mappingGrid.Rows[rowIndex].Cells["mappingNumberColumn"].Value =
                    suggestions.TryGetValue(field, out var column)
                        ? column.ToString(CultureInfo.InvariantCulture)
                        : string.Empty;
            }
        }

        private static string? DetectField(string header, IReadOnlyList<string> values)
        {
            var normalized = NormalizeHeader(header);

            if (ContainsAny(normalized, "tickerfa", "symbol", "ticker", "نماد")) return "نماد";
            if (ContainsAny(normalized, "ticker en", "ticker_en", "symbolen")) return "نماد لاتین";
            if (ContainsAny(normalized, "date-en", "dateen")) return "تاریخ لاتین";
            if (ContainsAny(normalized, "date-fa", "date", "تاریخ")) return "تاریخ";
            if (ContainsAny(normalized, "time", "زمان")) return "زمان";
            if (ContainsAny(normalized, "open", "باز")) return "باز";
            if (ContainsAny(normalized, "high", "بیشترین")) return "بیشترین";
            if (ContainsAny(normalized, "low", "کمترین")) return "کمترین";
            if (ContainsAny(normalized, "close", "پایانی")) return "پایانی";
            if (ContainsAny(normalized, "vol", "volume", "حجم")) return "حجم";
            if (ContainsAny(normalized, "tseclose")) return "قیمت پایانی بورس";
            if (ContainsAny(normalized, "previous")) return "قیمت قبلی";
            if (ContainsAny(normalized, "count")) return "تعداد معاملات";
            if (ContainsAny(normalized, "val")) return "ارزش معاملات";
            if (ContainsAny(normalized, "sharecount")) return "تعداد سهام";
            if (ContainsAny(normalized, "marketvalue")) return "ارزش بازار";
            if (ContainsAny(normalized, "per", "period", "دوره")) return "دوره";

            if (values.Count == 0)
                return null;

            var dateLike = values.Count(LooksLikeDate);
            if (dateLike >= 2 && dateLike * 2 >= values.Count)
                return "تاریخ";

            var timeLike = values.Count(LooksLikeTime);
            if (timeLike >= 2 && timeLike * 2 >= values.Count)
                return "زمان";

            return null;
        }

        private static bool ContainsAny(string value, params string[] tokens) =>
            tokens.Any(token => value.Contains(NormalizeHeader(token), StringComparison.OrdinalIgnoreCase));

        private static string NormalizeHeader(string value) => value.Trim().Trim('<', '>')
            .Replace("_", string.Empty)
            .Replace("-", string.Empty)
            .Replace(" ", string.Empty)
            .ToLowerInvariant();

        private static bool LooksLikeDate(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            return digits.Length == 8 && int.TryParse(digits[..4], out var year) && year is >= 1200 and <= 2500;
        }

        private static bool LooksLikeTime(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            if (digits.Length != 6)
                return false;

            return int.TryParse(digits[..2], out var h) &&
                   int.TryParse(digits.Substring(2, 2), out var m) &&
                   int.TryParse(digits.Substring(4, 2), out var s) &&
                   h is >= 0 and <= 23 && m is >= 0 and <= 59 && s is >= 0 and <= 59;
        }

        private int FindMappingColumn(string field, string[]? header, IReadOnlyList<string[]> dataRows)
        {
            var width = Math.Min(MaxColumns, Math.Max(header?.Length ?? 0,
                dataRows.Count == 0 ? 0 : dataRows.Max(r => r.Length)));

            for (var column = 0; column < width; column++)
            {
                var headerValue = header != null && column < header.Length ? header[column] : string.Empty;
                if (string.Equals(DetectField(headerValue, Array.Empty<string>()), field, StringComparison.Ordinal))
                    return column + 1;
            }

            return 0;
        }

        private static Encoding DetectEncoding(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            Span<byte> bom = stackalloc byte[4];
            var read = stream.Read(bom);
            if (read >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                return new UTF8Encoding(true);
            if (read >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
                return Encoding.Unicode;
            if (read >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
                return Encoding.BigEndianUnicode;
            return new UTF8Encoding(false);
        }

        private string GetSeparator() => separatorComboBox.SelectedIndex switch
        {
            1 => ";",
            2 => "\t",
            3 => "|",
            _ => ","
        };

        private static string[] SplitLine(string line, string separator)
        {
            if (separator != ",")
                return line.Split(new[] { separator }, StringSplitOptions.None);

            var result = new List<string>();
            var current = new StringBuilder();
            var quoted = false;

            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                        quoted = !quoted;
                }
                else if (c == ',' && !quoted)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                    current.Append(c);
            }

            result.Add(current.ToString());
            return result.ToArray();
        }

        private static string[] NormalizeWidth(string[] row, int width)
        {
            var result = new string[width];
            Array.Copy(row, result, Math.Min(row.Length, width));
            return result;
        }

        private void ClearPreviewAndMapping()
        {
            previewRows.Clear();
            previewHeader = null;
            previewGrid.Rows.Clear();

            for (var column = 0; column < MaxColumns; column++)
            {
                previewGrid.Columns[column].HeaderText = $"ستون {column + 1}";
                previewGrid.Columns[column].Visible = false;
            }

            InitializeMappingRows();
        }

        private void UpdateControlState()
        {
            var noDateTime = noDateTimeCheckBox.Checked;
            calendarComboBox.Enabled = !noDateTime;
            dateFormatComboBox.Enabled = !noDateTime;
            timeFormatComboBox.Enabled = !noDateTime;
        }

        private void TestMappingButton_Click(object? sender, EventArgs e)
        {
            if (!ValidateDefinition(out var error))
            {
                MessageBox.Show(this, error, "تست Mapping", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mappings = ReadMappings();
            var invalid = mappings.FirstOrDefault(x => x.Column < 0 || x.Column > MaxColumns);
            if (invalid != null)
            {
                MessageBox.Show(this,
                    $"شماره ستون برای «{invalid.Field}» معتبر نیست. مقدار را خالی یا عددی بین ۱ تا {MaxColumns} وارد کنید.",
                    "خطا در Mapping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var duplicate = mappings.Where(x => x.Column > 0)
                .GroupBy(x => x.Column)
                .FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
            {
                MessageBox.Show(this,
                    $"ستون {duplicate.Key} برای چند داده انتخاب شده است.",
                    "هشدار Mapping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(this, "تنظیمات و Mapping با موفقیت بررسی شد.", "تست Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateDefinition(out string error)
        {
            if (string.IsNullOrWhiteSpace(portfolioNameTextBox.Text))
            {
                error = "نام سبد را وارد کنید.";
                return false;
            }

            if (!Directory.Exists(dataPathTextBox.Text))
            {
                error = "مسیر پوشه داده معتبر نیست.";
                return false;
            }

            if (!symbolGrid.Rows.Cast<DataGridViewRow>().Any(r =>
                    !r.IsNewRow && Convert.ToBoolean(r.Cells["selectedColumn"].Value ?? false)))
            {
                error = "حداقل یک نماد را انتخاب کنید.";
                return false;
            }

            var mappings = ReadMappings();
            if (mappings.Any(x => x.Column < 0 || x.Column > MaxColumns))
            {
                error = $"شماره ستون Mapping باید خالی یا بین ۱ تا {MaxColumns} باشد.";
                return false;
            }

            error = string.Empty;
            return true;
        }

        private List<PortfolioMapping> ReadMappings()
        {
            var result = new List<PortfolioMapping>();
            foreach (DataGridViewRow row in mappingGrid.Rows)
            {
                var field = Convert.ToString(row.Cells["mappingFieldColumn"].Value) ?? string.Empty;
                var text = Convert.ToString(row.Cells["mappingNumberColumn"].Value)?.Trim() ?? string.Empty;
                var column = string.IsNullOrWhiteSpace(text)
                    ? 0
                    : int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out var value) ? value : -1;
                result.Add(new PortfolioMapping(field, column));
            }
            return result;
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            if (!ValidateDefinition(out var error))
            {
                MessageBox.Show(this, error, "ذخیره سبد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var definition = new PortfolioDefinition
            {
                Name = portfolioNameTextBox.Text.Trim(),
                SymbolSource = fileNameRadioButton.Checked ? SymbolSource.FileName : SymbolSource.InsideFile,
                DataPath = dataPathTextBox.Text.Trim(),
                FileType = fileTypeComboBox.Text,
                Separator = GetSeparator(),
                HasHeader = headerCheckBox.Checked,
                NoDateTime = noDateTimeCheckBox.Checked,
                Calendar = calendarComboBox.SelectedIndex == 0 ? InputCalendar.Persian : InputCalendar.Gregorian,
                DateFormat = dateFormatComboBox.Text,
                TimeFormat = timeFormatComboBox.Text,
                Mappings = ReadMappings(),
                Symbols = symbolGrid.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["selectedColumn"].Value ?? false))
                    .Select(r => Convert.ToString(r.Cells["symbolColumn"].Value) ?? string.Empty)
                    .Where(s => s.Length > 0)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };

            try
            {
                var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
                Directory.CreateDirectory(folder);

                var safeName = string.Concat(definition.Name.Select(c =>
                    Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
                var file = Path.Combine(folder, safeName + ".json");
                var json = JsonSerializer.Serialize(definition, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file, json, new UTF8Encoding(false));

                MessageBox.Show(this, "سبد با موفقیت ذخیره شد.", "ذخیره سبد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"ذخیره سبد انجام نشد:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetButton_Click(object? sender, EventArgs e)
        {
            internalUpdate = true;
            try
            {
                portfolioNameTextBox.Clear();
                fileNameRadioButton.Checked = true;
                insideFileRadioButton.Checked = false;
                dataPathTextBox.Clear();
                fileTypeComboBox.SelectedIndex = 0;
                separatorComboBox.SelectedIndex = 0;
                calendarComboBox.SelectedIndex = 0;
                dateFormatComboBox.SelectedIndex = 0;
                timeFormatComboBox.SelectedIndex = 0;
                headerCheckBox.Checked = true;
                noDateTimeCheckBox.Checked = false;
                symbolSearchTextBox.Clear();
                symbolGrid.Rows.Clear();
                ClearPreviewAndMapping();
            }
            finally
            {
                internalUpdate = false;
            }

            UpdateSelectedCount();
            UpdateControlState();
        }
    }
}