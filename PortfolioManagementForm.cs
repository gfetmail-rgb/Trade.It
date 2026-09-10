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

            portfoliosListBox.SelectedIndexChanged += PortfoliosListBox_SelectedIndexChanged;
            reloadButton.Click += ReloadButton_Click;
            deletePortfoliosButton.Click += DeletePortfoliosButton_Click;
            deleteSymbolsButton.Click += DeleteSymbolsButton_Click;
            closeButton.Click += CloseButton_Click;

            symbolsGrid.MultiSelect = true;
            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
                    : "یک سبد را انتخاب کنید.";
            }
            finally
            {
                internalUpdate = false;
            }
        }

        private void PortfoliosListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (internalUpdate)
                return;

            if (portfoliosListBox.SelectedItem is not string portfolioName ||
                !portfolioFiles.TryGetValue(portfolioName, out var file))
            {
                ClearPortfolioDetails();
                return;
            }

            LoadPortfolio(file);
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

        private void DeletePortfoliosButton_Click(object? sender, EventArgs e)
        {
            if (portfoliosListBox.SelectedItem is not string portfolioName ||
                !portfolioFiles.TryGetValue(portfolioName, out var file))
            {
                MessageBox.Show(this,
                    "ابتدا یک سبد را انتخاب کنید.",
                    "حذف سبد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this,
                $"آیا از حذف سبد «{portfolioName}» مطمئن هستید؟\nاین عملیات قابل بازگشت نیست.",
                "حذف سبد",
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
                        "حذف سبد",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                File.Delete(file);
                LoadPortfolios();

                statusLabel.Text = "سبد با موفقیت حذف شد.";
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
            if (portfoliosListBox.SelectedItem is not string portfolioName ||
                !portfolioFiles.TryGetValue(portfolioName, out var file))
            {
                MessageBox.Show(this,
                    "ابتدا یک سبد را انتخاب کنید.",
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
