using System.Text;
using System.Text.Json;

namespace Trade.It
{
    public partial class PortfolioManagementForm : Form
    {
        private readonly Dictionary<string, string> portfolioFiles = new(StringComparer.OrdinalIgnoreCase);
        private bool internalUpdate;

        public PortfolioManagementForm()
        {
            InitializeComponent();

            portfoliosListBox.SelectionMode = SelectionMode.MultiExtended;
            portfoliosListBox.MouseClick += PortfoliosListBox_MouseClick;
            reloadButton.Click += ReloadButton_Click;
            deletePortfoliosButton.Click += DeletePortfoliosButton_Click;
            deleteSymbolsButton.Click += DeleteSymbolsButton_Click;
            closeButton.Click += CloseButton_Click;

            symbolsGrid.MultiSelect = true;
            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            symbolsGrid.CellClick += SymbolsGrid_CellClick;

            Load += PortfolioManagementForm_Load;
        }

        private void PortfolioManagementForm_Load(object? sender, EventArgs e)
        {
            LoadPortfolios();
        }

        private void ReloadButton_Click(object? sender, EventArgs e)
        {
            LoadPortfolios();
        }

        private void LoadPortfolios()
        {
            internalUpdate = true;
            try
            {
                portfolioFiles.Clear();
                portfoliosListBox.Items.Clear();
                ClearPortfolioDetails();

                var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
                if (!Directory.Exists(folder))
                {
                    statusLabel.Text = "هیچ سبدی وجود ندارد.";
                    return;
                }

                foreach (var file in Directory.GetFiles(folder, "*.json")
                             .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
                {
                    try
                    {
                        var json = File.ReadAllText(file);
                        var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                        if (definition == null || string.IsNullOrWhiteSpace(definition.Name))
                            continue;

                        var displayName = definition.Name.Trim();
                        if (portfolioFiles.ContainsKey(displayName))
                            continue;

                        portfolioFiles[displayName] = file;
                        portfoliosListBox.Items.Add(displayName);
                    }
                    catch
                    {
                        // Invalid portfolio files are ignored; other portfolios remain available.
                    }
                }

                statusLabel.Text = portfoliosListBox.Items.Count == 0
                    ? "هیچ سبدی وجود ندارد."
                    : "یک یا چند سبد را انتخاب کنید.";
            }
            finally
            {
                internalUpdate = false;
            }
        }

        private void PortfoliosListBox_MouseClick(object? sender, MouseEventArgs e)
        {
            if (internalUpdate || e.Button != MouseButtons.Left)
                return;

            var index = portfoliosListBox.IndexFromPoint(e.Location);
            if (index < 0 || index >= portfoliosListBox.Items.Count)
                return;

            // The clicked portfolio is always the source of the details panel,
            // while the ListBox keeps all current selections for multi-delete.
            var portfolioName = Convert.ToString(portfoliosListBox.Items[index]);
            if (!string.IsNullOrWhiteSpace(portfolioName) &&
                portfolioFiles.TryGetValue(portfolioName, out var file))
            {
                LoadPortfolio(file);
            }
        }

        private void LoadPortfolio(string file)
        {
            try
            {
                var json = File.ReadAllText(file);
                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                if (definition == null)
                {
                    ClearPortfolioDetails();
                    statusLabel.Text = "اطلاعات سبد معتبر نیست.";
                    return;
                }

                portfolioNameLabel.Text = definition.Name;
                sourceTypeLabel.Text = definition.FileType;
                dataPathLabel.Text = definition.DataPath;
                symbolSourceLabel.Text = definition.SymbolSource == SymbolSource.FileName
                    ? "نام فایل"
                    : "داخل فایل";
                separatorLabel.Text = GetSeparatorText(definition.Separator);
                timeFormatLabel.Text = definition.NoDateTime ? "—" : definition.TimeFormat;
                dateFormatLabel.Text = definition.NoDateTime ? "—" : definition.DateFormat;
                calendarLabel.Text = definition.NoDateTime
                    ? "—"
                    : definition.Calendar == InputCalendar.Persian ? "شمسی / فارسی" : "میلادی / لاتین";
                headerLabel.Text = definition.HasHeader ? "بله" : "خیر";
                NoDateTimeLabel.Text = definition.NoDateTime ? "بله" : "خیر";

                var symbols = definition.Symbols ?? new List<string>();
                symbolCountLabel.Text = ToPersianDigits(symbols.Count.ToString());

                symbolsGrid.Rows.Clear();
                for (var index = 0; index < symbols.Count; index++)
                    symbolsGrid.Rows.Add(ToPersianDigits((index + 1).ToString()), symbols[index]);

                statusLabel.Text = $"سبد «{definition.Name}» انتخاب شده است.";
            }
            catch (Exception ex)
            {
                ClearPortfolioDetails();
                statusLabel.Text = "خواندن اطلاعات سبد با خطا مواجه شد.";
                MessageBox.Show(this,
                    $"اطلاعات سبد خوانده نشد:\n{ex.Message}",
                    "مدیریت سبد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void SymbolsGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= symbolsGrid.Rows.Count)
                return;

            var symbol = Convert.ToString(symbolsGrid.Rows[e.RowIndex].Cells["symbolNameColumn"].Value)?.Trim();
            if (string.IsNullOrWhiteSpace(symbol))
                return;

            var portfolioName = portfolioNameLabel.Text.Trim();
            if (string.IsNullOrWhiteSpace(portfolioName) || portfolioName == "—" ||
                !portfolioFiles.TryGetValue(portfolioName, out var portfolioFile) || !File.Exists(portfolioFile))
            {
                ClearPreview();
                return;
            }

            try
            {
                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(portfolioFile));
                if (definition == null)
                {
                    ClearPreview();
                    return;
                }

                var dataFiles = GetPortfolioDataFiles(definition).ToList();
                string? matchingFile;
                if (definition.SymbolSource == SymbolSource.FileName)
                    matchingFile = dataFiles.FirstOrDefault(f => string.Equals(Path.GetFileNameWithoutExtension(f), symbol, StringComparison.OrdinalIgnoreCase));
                else
                    matchingFile = dataFiles.FirstOrDefault(f => FileContainsSymbol(f, definition, symbol));

                if (matchingFile == null)
                {
                    ClearPreview();
                    return;
                }

                LoadSymbolPreview(matchingFile, definition, symbol);
            }
            catch (Exception ex)
            {
                ClearPreview();
                MessageBox.Show(this, $"نمایش اطلاعات نماد انجام نشد:\n{ex.Message}", "پیش نمایش", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<string> GetPortfolioDataFiles(PortfolioDefinition definition)
        {
            if (!Directory.Exists(definition.DataPath))
                yield break;

            var extension = definition.FileType?.Trim().ToUpperInvariant() switch
            {
                "CSV" => ".csv",
                "PRN" => ".prn",
                _ => ".txt"
            };

            foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly)
                         .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
                yield return file;
        }

        private bool FileContainsSymbol(string filePath, PortfolioDefinition definition, string symbol)
        {
            var rows = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => SplitLine(line, definition.Separator))
                .Take(101)
                .ToList();
            if (rows.Count == 0)
                return false;

            var header = definition.HasHeader ? rows[0] : null;
            var dataRows = rows.Skip(definition.HasHeader ? 1 : 0).ToList();
            var symbolColumn = GetSymbolColumn(definition, header);
            return symbolColumn > 0 && dataRows.Any(row => symbolColumn <= row.Length &&
                string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase));
        }

        private void LoadSymbolPreview(string filePath, PortfolioDefinition definition, string symbol)
        {
            var rows = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => SplitLine(line, definition.Separator))
                .ToList();
            if (rows.Count == 0)
            {
                ClearPreview();
                return;
            }

            var header = definition.HasHeader ? rows[0] : null;
            var dataRows = rows.Skip(definition.HasHeader ? 1 : 0).ToList();
            var width = Math.Min(18, Math.Max(header?.Length ?? 0, dataRows.Count == 0 ? 0 : dataRows.Max(r => r.Length)));
            if (width <= 0)
            {
                ClearPreview();
                return;
            }

            var symbolColumn = GetSymbolColumn(definition, header);
            var selectedRows = definition.SymbolSource == SymbolSource.FileName || symbolColumn <= 0
                ? dataRows
                : dataRows.Where(row => symbolColumn <= row.Length &&
                    string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase)).ToList();

            previewGrid.Rows.Clear();
            for (var i = 0; i < 18; i++)
            {
                var title = header != null && i < header.Length && !string.IsNullOrWhiteSpace(header[i])
                    ? $"{i + 1}: {header[i].Trim()}"
                    : $"ستون {i + 1}";
                previewGrid.Columns[i].HeaderText = title;
                previewGrid.Columns[i].Visible = i < width;
            }

            foreach (var row in selectedRows)
            {
                var values = new object[18];
                for (var i = 0; i < 18; i++)
                    values[i] = i < row.Length ? row[i].Trim() : string.Empty;
                previewGrid.Rows.Add(values);
            }

            previewGroup.Text = selectedRows.Count == 0
                ? $"پیش نمایش — {symbol} (داده‌ای یافت نشد)"
                : $"پیش نمایش — {symbol}";
        }

        private int GetSymbolColumn(PortfolioDefinition definition, string[]? header)
        {
            var mapped = definition.Mappings?.FirstOrDefault(m => string.Equals(m.Field, "نماد", StringComparison.Ordinal));
            if (mapped != null && mapped.Column > 0)
                return mapped.Column;

            if (header != null)
            {
                for (var i = 0; i < header.Length; i++)
                {
                    var h = NormalizeHeader(header[i]);
                    if (h.Contains("tickerfa") || h.Contains("symbol") || h.Contains("ticker") || h.Contains("نماد"))
                        return i + 1;
                }
            }
            return 0;
        }

        private static Encoding DetectEncoding(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            Span<byte> bom = stackalloc byte[4];
            var read = stream.Read(bom);
            if (read >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) return new UTF8Encoding(true);
            if (read >= 2 && bom[0] == 0xFF && bom[1] == 0xFE) return Encoding.Unicode;
            if (read >= 2 && bom[0] == 0xFE && bom[1] == 0xFF) return Encoding.BigEndianUnicode;
            return new UTF8Encoding(false);
        }

        private static string[] SplitLine(string line, string separator)
        {
            if (separator != ",")
                return line.Split(new[] { separator }, StringSplitOptions.None);

            var result = new List<string>();
            var current = new System.Text.StringBuilder();
            var quoted = false;
            for (var i = 0; i < line.Length; i++)
            {
                var ch = line[i];
                if (ch == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"') { current.Append('"'); i++; }
                    else quoted = !quoted;
                }
                else if (ch == ',' && !quoted) { result.Add(current.ToString()); current.Clear(); }
                else current.Append(ch);
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        private static string NormalizeHeader(string value) =>
            value.Trim().Trim('<', '>').Replace("_", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();

        private void ClearPreview()
        {
            previewGrid.Rows.Clear();
            for (var i = 0; i < previewGrid.Columns.Count; i++)
            {
                previewGrid.Columns[i].Visible = false;
                previewGrid.Columns[i].HeaderText = $"ستون {i + 1}";
            }
            previewGroup.Text = "پیش نمایش";
        }

        private void DeletePortfoliosButton_Click(object? sender, EventArgs e)
        {
            var selectedPortfolios = portfoliosListBox.SelectedItems
                .Cast<object>()
                .Select(item => Convert.ToString(item) ?? string.Empty)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(name => portfolioFiles.ContainsKey(name))
                .ToList();

            if (selectedPortfolios.Count == 0)
            {
                MessageBox.Show(this,
                    "ابتدا یک یا چند سبد را انتخاب کنید.",
                    "حذف سبد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var portfolioText = selectedPortfolios.Count == 1
                ? $"سبد «{selectedPortfolios[0]}»"
                : $"{ToPersianDigits(selectedPortfolios.Count.ToString())} سبد انتخاب‌شده";

            var result = MessageBox.Show(this,
                $"آیا از حذف {portfolioText} مطمئن هستید؟\nاین عملیات قابل بازگشت نیست.",
                "حذف سبد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var deletedCount = 0;
                var missingCount = 0;

                foreach (var portfolioName in selectedPortfolios)
                {
                    var file = portfolioFiles[portfolioName];
                    if (!File.Exists(file))
                    {
                        missingCount++;
                        continue;
                    }

                    File.Delete(file);
                    deletedCount++;
                }

                LoadPortfolios();

                if (deletedCount > 0 && missingCount == 0)
                    statusLabel.Text = selectedPortfolios.Count == 1
                        ? "سبد با موفقیت حذف شد."
                        : "سبدهای انتخاب‌شده با موفقیت حذف شدند.";
                else if (deletedCount > 0)
                    statusLabel.Text = "سبدهای موجود حذف شدند و فهرست به‌روزرسانی شد.";
                else
                    statusLabel.Text = "هیچ‌یک از فایل‌های سبدهای انتخاب‌شده پیدا نشد.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"حذف سبد انجام نشد:\n{ex.Message}",
                    "حذف سبد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeleteSymbolsButton_Click(object? sender, EventArgs e)
        {
            // Symbol deletion always applies to the portfolio whose details are currently displayed.
            var portfolioName = portfolioNameLabel.Text.Trim();
            if (string.IsNullOrWhiteSpace(portfolioName) ||
                portfolioName == "—" ||
                !portfolioFiles.TryGetValue(portfolioName, out var file))
            {
                MessageBox.Show(this,
                    "ابتدا روی سبد موردنظر کلیک کنید.",
                    "حذف نماد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedRows = symbolsGrid.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow)
                .OrderBy(row => row.Index)
                .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show(this,
                    "ابتدا یک یا چند نماد را انتخاب کنید.",
                    "حذف نماد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedSymbols = selectedRows
                .Select(row => Convert.ToString(row.Cells["symbolNameColumn"].Value) ?? string.Empty)
                .Where(symbol => !string.IsNullOrWhiteSpace(symbol))
                .ToList();

            var symbolText = selectedSymbols.Count == 1
                ? $"نماد «{selectedSymbols[0]}»"
                : $"{ToPersianDigits(selectedSymbols.Count.ToString())} نماد انتخاب‌شده";

            var result = MessageBox.Show(this,
                $"آیا از حذف {symbolText} از سبد «{portfolioName}» مطمئن هستید؟",
                "حذف نماد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                if (!File.Exists(file))
                {
                    LoadPortfolios();
                    MessageBox.Show(this,
                        "فایل سبد پیدا نشد و فهرست سبدها به‌روزرسانی شد.",
                        "حذف نماد",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var json = File.ReadAllText(file);
                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                if (definition == null)
                {
                    MessageBox.Show(this,
                        "اطلاعات سبد معتبر نیست.",
                        "حذف نماد",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var symbols = definition.Symbols ?? new List<string>();
                var symbolsToRemove = new HashSet<string>(selectedSymbols, StringComparer.OrdinalIgnoreCase);
                definition.Symbols = symbols
                    .Where(symbol => !symbolsToRemove.Contains(symbol))
                    .ToList();

                var updatedJson = JsonSerializer.Serialize(definition, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(file, updatedJson, new System.Text.UTF8Encoding(false));

                LoadPortfolio(file);
                statusLabel.Text = selectedSymbols.Count == 1
                    ? "نماد با موفقیت حذف شد."
                    : "نمادهای انتخاب‌شده با موفقیت حذف شدند.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"حذف نماد انجام نشد:\n{ex.Message}",
                    "حذف نماد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearPortfolioDetails()
        {
            portfolioNameLabel.Text = "—";
            sourceTypeLabel.Text = "—";
            dataPathLabel.Text = "—";
            symbolSourceLabel.Text = "—";
            separatorLabel.Text = "—";
            timeFormatLabel.Text = "—";
            dateFormatLabel.Text = "—";
            calendarLabel.Text = "—";
            headerLabel.Text = "—";
            NoDateTimeLabel.Text = "—";
            symbolCountLabel.Text = "۰";
            symbolsGrid.Rows.Clear();
            ClearPreview();
        }

        private static string GetSeparatorText(string separator)
        {
            return separator switch
            {
                "," => "کاما (,) ",
                ";" => "سمی‌کالن (;) ",
                "\t" => "Tab",
                "|" => "Pipe (|)",
                _ => separator
            };
        }

        private static string ToPersianDigits(string value)
        {
            return value
                .Replace('0', '۰')
                .Replace('1', '۱')
                .Replace('2', '۲')
                .Replace('3', '۳')
                .Replace('4', '۴')
                .Replace('5', '۵')
                .Replace('6', '۶')
                .Replace('7', '۷')
                .Replace('8', '۸')
                .Replace('9', '۹');
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
