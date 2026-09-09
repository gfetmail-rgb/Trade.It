using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Trade.It
{
    public partial class PortfolioDefinitionForm : Form
    {
        private const int MaxColumns = 18;
        private readonly List<DetectedColumn> detectedColumns = new();
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
            fileTypeComboBox.Items.Clear();
            fileTypeComboBox.Items.AddRange(new object[] { "TXT", "CSV", "PRN" });
            fileTypeComboBox.SelectedIndex = 0;

            separatorComboBox.SelectedIndex = 0;
            calendarComboBox.SelectedIndex = 0;
            dateFormatComboBox.SelectedIndex = 0;
            timeFormatComboBox.SelectedIndex = 0;

            mappingGrid.Columns.Clear();
            mappingGrid.Columns.Add("fieldColumn", "داده");
            mappingGrid.Columns.Add("columnNumberColumn", "شماره ستون");
            mappingGrid.Columns.Add("confidenceColumn", "تشخیص خودکار");
            mappingGrid.Columns[0].Width = 220;
            mappingGrid.Columns[1].Width = 130;
            mappingGrid.Columns[2].Width = 260;
            mappingGrid.Columns[0].ReadOnly = true;
            mappingGrid.Columns[2].ReadOnly = true;
            mappingGrid.EditMode = DataGridViewEditMode.EditOnEnter;

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
            testMappingButton.Click += TestMappingButton_Click;
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (_, _) => Close();
            resetButton.Click += ResetButton_Click;

            ConfigureSymbolGrid();
            ConfigurePreviewGrid();
            UpdateMappingGrid(Array.Empty<string>());
            UpdateControlState();
        }

        private void ConfigureSymbolGrid()
        {
            symbolGrid.Columns.Clear();
            var selected = new DataGridViewCheckBoxColumn
            {
                Name = "selectedColumn",
                HeaderText = "انتخاب",
                Width = 110,
                FalseValue = false,
                TrueValue = true
            };
            var symbol = new DataGridViewTextBoxColumn
            {
                Name = "symbolColumn",
                HeaderText = "نماد / فایل",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            symbolGrid.Columns.Add(selected);
            symbolGrid.Columns.Add(symbol);
        }

        private void ConfigurePreviewGrid()
        {
            previewGrid.Columns.Clear();
            previewGrid.AllowUserToResizeColumns = true;
            previewGrid.AutoGenerateColumns = false;
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
            {
                dataPathTextBox.Text = dialog.SelectedPath;
                LoadSymbolsAndPreview();
            }
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

        private void LoadSymbolsAndPreview()
        {
            try
            {
                var files = GetDataFiles().ToList();
                PopulateSymbolGrid(files);

                var firstSelectedOrFirst = files.FirstOrDefault();
                if (firstSelectedOrFirst != null)
                    LoadPreview(firstSelectedOrFirst);
                else
                    ClearPreviewAndMapping();
            }
            catch (Exception ex)
            {
                ClearPreviewAndMapping();
                MessageBox.Show(this, $"خواندن پوشه داده‌ها انجام نشد:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void PopulateSymbolGrid(IReadOnlyList<string> files)
        {
            internalUpdate = true;
            try
            {
                symbolGrid.Rows.Clear();
                foreach (var file in files)
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    symbolGrid.Rows.Add(false, name);
                }
                UpdateSelectedCount();
            }
            finally
            {
                internalUpdate = false;
            }

            ApplySymbolFilter();
        }

        private void SymbolSearchTextBox_TextChanged(object? sender, EventArgs e) => ApplySymbolFilter();

        private void ApplySymbolFilter()
        {
            var query = symbolSearchTextBox.Text.Trim();
            foreach (DataGridViewRow row in symbolGrid.Rows)
            {
                if (row.IsNewRow) continue;
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
            var selected = symbolGrid.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["selectedColumn"].Value ?? false));
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

            var separator = GetSeparator();
            var rows = lines.Select(line => SplitLine(line, separator)).ToList();
            var width = Math.Min(MaxColumns, rows.Max(r => r.Length));
            if (width == 0)
            {
                ClearPreviewAndMapping();
                return;
            }

            previewHeader = headerCheckBox.Checked ? NormalizeWidth(rows[0], width) : null;
            var dataStart = headerCheckBox.Checked ? 1 : 0;
            previewRows = rows.Skip(dataStart).Take(100).Select(r => NormalizeWidth(r, width)).ToList();
            if (previewRows.Count == 0)
                previewRows.Add(new string[width]);

            BuildPreviewGrid(width);
            detectedColumns.Clear();
            detectedColumns.AddRange(DetectColumns(width));
            UpdateMappingGrid(previewHeader ?? Array.Empty<string>());
        }

        private Encoding DetectEncoding(string filePath)
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
            // The supplied formats are delimited text. Quoted fields are handled for CSV-style files.
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

        private void BuildPreviewGrid(int width)
        {
            previewGrid.Columns.Clear();
            for (var i = 0; i < width; i++)
            {
                var title = previewHeader != null && i < previewHeader.Length && !string.IsNullOrWhiteSpace(previewHeader[i])
                    ? previewHeader[i]
                    : $"ستون {i + 1}";
                var column = new DataGridViewTextBoxColumn
                {
                    Name = $"preview_{i + 1}",
                    HeaderText = $"{i + 1}: {title}",
                    ReadOnly = true,
                    Width = 150,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                previewGrid.Columns.Add(column);
            }

            previewGrid.Rows.Clear();
            foreach (var row in previewRows)
                previewGrid.Rows.Add(row.Cast<object>().ToArray());
        }

        private List<DetectedColumn> DetectColumns(int width)
        {
            var result = new List<DetectedColumn>();
            for (var i = 0; i < width; i++)
            {
                var header = previewHeader != null && i < previewHeader.Length ? previewHeader[i] : string.Empty;
                var values = previewRows.Select(r => r[i]).Where(v => !string.IsNullOrWhiteSpace(v)).Take(50).ToList();
                result.Add(DetectColumn(i + 1, header, values));
            }
            return result;
        }

        private DetectedColumn DetectColumn(int number, string header, IReadOnlyList<string> values)
        {
            var normalized = NormalizeHeader(header);
            var matches = new List<(string field, int score)>();
            void Add(string field, params string[] tokens)
            {
                var score = tokens.Any(t => normalized.Contains(NormalizeHeader(t), StringComparison.OrdinalIgnoreCase)) ? 100 : 0;
                if (score > 0) matches.Add((field, score));
            }

            Add("نماد", "TickerFa", "نماد", "Symbol", "Ticker");
            Add("نماد لاتین", "TickerEn", "SymbolEn");
            Add("تاریخ", "DATE-Fa", "Date", "DATE");
            Add("تاریخ لاتین", "Date-En", "DateEn");
            Add("زمان", "TIME", "Time");
            Add("باز", "OPEN", "Open");
            Add("بیشترین", "HIGH", "High");
            Add("کمترین", "LOW", "Low");
            Add("پایانی", "CLOSE", "Close");
            Add("حجم", "VOL", "Volume");
            Add("قیمت پایانی بورس", "TSEClose");
            Add("قیمت قبلی", "PREVIOUS", "Previous");
            Add("تعداد معاملات", "COUNT", "Count");
            Add("ارزش معاملات", "VAL", "Value");
            Add("تعداد سهام", "ShareCount");
            Add("ارزش بازار", "MarketValue");
            Add("دوره", "Per", "Period");

            if (matches.Count > 0)
                return new DetectedColumn(number, matches[0].field, 100, header);

            if (values.Count > 0)
            {
                var numeric = values.Count(IsNumeric);
                var dateLike = values.Count(v => LooksLikeDate(v));
                var timeLike = values.Count(LooksLikeTime);
                var textLike = values.Count(v => !IsNumeric(v));
                if (dateLike * 2 >= values.Count && dateLike >= 2)
                    return new DetectedColumn(number, "تاریخ", 75, header);
                if (timeLike * 2 >= values.Count && timeLike >= 2)
                    return new DetectedColumn(number, "زمان", 75, header);
                if (textLike * 2 >= values.Count)
                    return new DetectedColumn(number, "نماد", 55, header);
                if (numeric == values.Count)
                    return new DetectedColumn(number, "عددی", 35, header);
            }

            return new DetectedColumn(number, "نامشخص", 0, header);
        }

        private static string NormalizeHeader(string value) => value.Trim().Trim('<', '>').Replace("_", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();

        private static bool IsNumeric(string value) => double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out _)
            || double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out _);

        private static bool LooksLikeDate(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            return digits.Length == 8 && int.TryParse(digits[..4], out var year) && year is >= 1200 and <= 2500;
        }

        private static bool LooksLikeTime(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            if (digits.Length != 6) return false;
            return int.TryParse(digits[..2], out var h) && int.TryParse(digits.Substring(2, 2), out var m) && int.TryParse(digits.Substring(4, 2), out var s)
                   && h is >= 0 and <= 23 && m is >= 0 and <= 59 && s is >= 0 and <= 59;
        }

        private void UpdateMappingGrid(string[] header)
        {
            mappingGrid.Rows.Clear();
            foreach (var field in MappingFields)
            {
                var detected = detectedColumns.FirstOrDefault(d => string.Equals(d.Field, field, StringComparison.Ordinal));
                var confidence = detected == null ? "" : $"{detected.Confidence}%";
                mappingGrid.Rows.Add(field, detected?.ColumnNumber.ToString(CultureInfo.InvariantCulture) ?? "", confidence);
            }
        }

        private void ClearPreviewAndMapping()
        {
            previewRows.Clear();
            previewHeader = null;
            detectedColumns.Clear();
            previewGrid.Columns.Clear();
            previewGrid.Rows.Clear();
            UpdateMappingGrid(Array.Empty<string>());
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
            var duplicate = mappings.Where(x => x.Column > 0).GroupBy(x => x.Column).FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
            {
                MessageBox.Show(this, $"ستون {duplicate.Key} برای چند داده انتخاب شده است. اگر این کار عمدی نیست، Mapping را اصلاح کنید.", "هشدار Mapping", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (symbolGrid.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow || !Convert.ToBoolean(r.Cells["selectedColumn"].Value ?? false)))
            {
                error = "حداقل یک نماد را انتخاب کنید.";
                return false;
            }
            if (!noDateTimeCheckBox.Checked && string.IsNullOrWhiteSpace(dateFormatComboBox.Text))
            {
                error = "فرمت تاریخ را مشخص کنید.";
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
                var field = Convert.ToString(row.Cells["fieldColumn"].Value) ?? string.Empty;
                var text = Convert.ToString(row.Cells["columnNumberColumn"].Value) ?? string.Empty;
                var column = int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) ? value : 0;
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
                    .ToList()
            };

            try
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Trade.It", "Portfolios");
                Directory.CreateDirectory(folder);
                var file = Path.Combine(folder, definition.Name + ".json");
                var json = JsonSerializer.Serialize(definition, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file, json, new UTF8Encoding(false));
                DialogResult = DialogResult.OK;
                Close();
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
                headerCheckBox.Checked = false;
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

    internal sealed record DetectedColumn(int ColumnNumber, string Field, int Confidence, string Header);
}
