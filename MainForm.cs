using System.Text.Json;

namespace Trade.It
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, PortfolioDefinition> loadedPortfolios = new(StringComparer.OrdinalIgnoreCase);
        private bool internalPortfolioUpdate;
        private string? displayedPortfolioName;

        public MainForm()
        {
            InitializeComponent();

            // The WinForms designer creates MainForm inside the design-tools process.
            // Runtime-only initialization must not execute while the designer is loading.
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            mainMenuStrip.RightToLeft = RightToLeft.No;

            portfolioDefinitionMenuItem.Click += (_, _) =>
            {
                using var form = new PortfolioDefinitionForm();
                form.ShowDialog(this);
                RefreshPortfolioListAndClearSelection();
            };

            portfolioManagementMenuItem.Click += (_, _) =>
            {
                using var form = new PortfolioManagementForm();
                form.ShowDialog(this);
                RefreshPortfolioListAndClearSelection();
            };

            tabPage3.Controls.Clear();
            tabPage3.AutoScroll = true;
            var identifierButtonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 52, Padding = new Padding(4), FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes };
            foreach (var buttonInfo in new[] { (Text: "جدید", Width: 62), (Text: "ذخیره", Width: 70), (Text: "حذف", Width: 62), (Text: "حذف همه", Width: 80), (Text: "ورود از اکسل", Width: 105) })
                identifierButtonsPanel.Controls.Add(new Button { Text = buttonInfo.Text, Width = buttonInfo.Width, Height = 36, Margin = new Padding(3) });
            var identifierGroup = new GroupBox { Dock = DockStyle.Top, AutoSize = true, Text = "اطلاعات شناسه", Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
            var identifierFields = new (string Label, bool Combo)[] { ("کد ۱۲ رقمی نماد", false), ("کد ۵ رقمی نماد", false), ("نام لاتین شرکت", false), ("کد ۴ رقمی شرکت", false), ("نام شرکت", false), ("نماد فارسی", false), ("نماد ۳۰ رقمی فارسی", false), ("کد ۱۲ رقمی شرکت", false), ("بازار", true), ("کد تابلو", false), ("کد گروه صنعت", false), ("گروه صنعت", false), ("کد زیر گروه صنعت", false), ("زیر گروه صنعت", false) };
            var identifierTable = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, RowCount = identifierFields.Length, Padding = new Padding(6), RightToLeft = RightToLeft.Yes };
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            foreach (var field in identifierFields)
            {
                identifierTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                identifierTable.Controls.Add(new Label { Text = field.Label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4), Font = new Font("Segoe UI", 10F) });
                Control input = field.Combo ? new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(4) } : new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) };
                identifierTable.Controls.Add(input);
            }
            identifierGroup.Controls.Add(identifierTable);
            tabPage3.Controls.Add(identifierGroup);
            tabPage3.Controls.Add(identifierButtonsPanel);
            identifierButtonsPanel.BringToFront();

            portfolioComboBox.SelectedIndexChanged += PortfolioComboBox_SelectedIndexChanged;
            newPortfolioButton.Click += NewPortfolioButton_Click;
            refreshButton.Click += RefreshStocksButton_Click;
            refreshButtonPortfolio.Click += RefreshPortfolioButton_Click;
            deleteButton.Click += DeleteButton_Click;
            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChanged;
            selectNoneCheckBox.CheckedChanged += SelectNoneCheckBox_CheckedChanged;
            Load += MainForm_Portfolios_Load;
        }

        private void MainForm_Portfolios_Load(object? sender, EventArgs e)
        {
            RefreshPortfolioListAndClearSelection();
        }

        private void RefreshPortfolioButton_Click(object? sender, EventArgs e)
        {
            // The upper refresh button refreshes only the portfolio list in the ComboBox.
            RefreshPortfolioListAndClearSelection();
        }

        private void RefreshStocksButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName))
            {
                stocksDataGridView.Rows.Clear();
                UpdateSelectionControls();
                return;
            }

            // The lower refresh button refreshes only the symbols of the currently
            // displayed portfolio; the portfolio ComboBox remains unchanged.
            var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
            if (!Directory.Exists(folder))
                return;

            try
            {
                var currentName = displayedPortfolioName.Trim();
                var safeName = string.Concat(currentName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
                var file = Path.Combine(folder, safeName + ".json");

                if (!File.Exists(file))
                {
                    file = Directory.GetFiles(folder, "*.json")
                        .FirstOrDefault(f =>
                        {
                            try
                            {
                                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(f));
                                return definition != null && string.Equals(definition.Name?.Trim(), currentName, StringComparison.OrdinalIgnoreCase);
                            }
                            catch
                            {
                                return false;
                            }
                        }) ?? string.Empty;
                }

                if (string.IsNullOrEmpty(file))
                    return;

                var refreshed = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(file));
                if (refreshed == null || string.IsNullOrWhiteSpace(refreshed.Name))
                    return;

                loadedPortfolios[currentName] = refreshed;
                PopulateStocksGrid(refreshed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"تازه‌سازی فهرست سهام انجام نشد:\n{ex.Message}", "تازه‌سازی", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshPortfolioListAndClearSelection()
        {
            LoadPortfoliosIntoGrid();
            portfolioComboBox.SelectedIndex = -1;
            stocksDataGridView.Rows.Clear();
            displayedPortfolioName = null;
            UpdateSelectionControls();
        }

        private void LoadPortfoliosIntoGrid()
        {
            internalPortfolioUpdate = true;
            try
            {
                loadedPortfolios.Clear();
                portfolioComboBox.Items.Clear();
                stocksDataGridView.Rows.Clear();
                displayedPortfolioName = null;

                var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
                if (!Directory.Exists(folder))
                    return;

                foreach (var file in Directory.GetFiles(folder, "*.json")
                             .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
                {
                    try
                    {
                        var json = File.ReadAllText(file);
                        var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                        if (definition == null || string.IsNullOrWhiteSpace(definition.Name))
                            continue;

                        var name = definition.Name.Trim();
                        if (loadedPortfolios.ContainsKey(name))
                            continue;

                        loadedPortfolios[name] = definition;
                        portfolioComboBox.Items.Add(name);
                    }
                    catch
                    {
                    }
                }
            }
            finally
            {
                internalPortfolioUpdate = false;
            }
        }

        private void PortfolioComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (internalPortfolioUpdate)
                return;

            if (portfolioComboBox.SelectedItem is not string name || !loadedPortfolios.TryGetValue(name, out var definition))
            {
                stocksDataGridView.Rows.Clear();
                displayedPortfolioName = null;
                UpdateSelectionControls();
                return;
            }

            displayedPortfolioName = name;
            PopulateStocksGrid(definition);
        }

        private void PopulateStocksGrid(PortfolioDefinition definition)
        {
            internalPortfolioUpdate = true;
            try
            {
                stocksDataGridView.Rows.Clear();
                var symbols = definition.Symbols ?? new List<string>();
                for (var i = 0; i < symbols.Count; i++)
                    stocksDataGridView.Rows.Add(i + 1, symbols[i], "", false);
            }
            finally
            {
                internalPortfolioUpdate = false;
            }

            selectAllCheckBox.Checked = false;
            selectNoneCheckBox.Checked = true;
            UpdateSelectionControls();
        }

        private void SelectAllCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (internalPortfolioUpdate || !selectAllCheckBox.Checked)
                return;

            internalPortfolioUpdate = true;
            try
            {
                foreach (DataGridViewRow row in stocksDataGridView.Rows)
                    row.Cells[selectColumn.Index].Value = true;
                selectNoneCheckBox.Checked = false;
            }
            finally
            {
                internalPortfolioUpdate = false;
            }
        }

        private void SelectNoneCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (internalPortfolioUpdate || !selectNoneCheckBox.Checked)
                return;

            internalPortfolioUpdate = true;
            try
            {
                foreach (DataGridViewRow row in stocksDataGridView.Rows)
                    row.Cells[selectColumn.Index].Value = false;
                selectAllCheckBox.Checked = false;
            }
            finally
            {
                internalPortfolioUpdate = false;
            }
        }

        private void NewPortfolioButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var current))
            {
                MessageBox.Show(this, "ابتدا یک سبد جاری انتخاب کنید.", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedSymbols = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false))
                .Select(row => Convert.ToString(row.Cells[symbolColumn.Index].Value) ?? string.Empty)
                .Where(symbol => !string.IsNullOrWhiteSpace(symbol))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (selectedSymbols.Count == 0)
            {
                MessageBox.Show(this, "حداقل یک سهم را انتخاب کنید.", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var newName = $"{current.Name.Trim()} {DateTime.Now:yyyyMMdd-HHmmss}";
            var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
            Directory.CreateDirectory(folder);
            var safeName = string.Concat(newName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
            var file = Path.Combine(folder, safeName + ".json");
            var suffix = 1;
            while (File.Exists(file))
            {
                file = Path.Combine(folder, $"{safeName}-{suffix++}.json");
            }

            var newDefinition = new PortfolioDefinition
            {
                Name = newName,
                SymbolSource = current.SymbolSource,
                DataPath = current.DataPath,
                FileType = current.FileType,
                Separator = current.Separator,
                HasHeader = current.HasHeader,
                NoDateTime = current.NoDateTime,
                Calendar = current.Calendar,
                DateFormat = current.DateFormat,
                TimeFormat = current.TimeFormat,
                Mappings = current.Mappings?.Select(m => new PortfolioMapping(m.Field, m.Column)).ToList() ?? new List<PortfolioMapping>(),
                Symbols = selectedSymbols
            };

            try
            {
                var json = JsonSerializer.Serialize(newDefinition, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file, json, new System.Text.UTF8Encoding(false));
                RefreshPortfolioListAndClearSelection();
                var index = portfolioComboBox.Items.IndexOf(newName);
                if (index >= 0)
                {
                    portfolioComboBox.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"ساخت سبد جدید انجام نشد:\n{ex.Message}", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var current))
            {
                MessageBox.Show(this, "ابتدا یک سبد جاری انتخاب کنید.", "حذف سهم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedSymbols = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false))
                .Select(row => Convert.ToString(row.Cells[symbolColumn.Index].Value) ?? string.Empty)
                .Where(symbol => !string.IsNullOrWhiteSpace(symbol))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (selectedSymbols.Count == 0)
            {
                MessageBox.Show(this, "ابتدا یک یا چند سهم را انتخاب کنید.", "حذف سهم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this,
                selectedSymbols.Count == 1
                    ? $"آیا سهم «{selectedSymbols[0]}» از سبد «{current.Name}» حذف شود؟"
                    : $"آیا {ToPersianDigits(selectedSymbols.Count.ToString())} سهم انتخاب‌شده از سبد «{current.Name}» حذف شوند؟",
                "حذف سهم",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var remove = new HashSet<string>(selectedSymbols, StringComparer.OrdinalIgnoreCase);
                current.Symbols = (current.Symbols ?? new List<string>())
                    .Where(symbol => !remove.Contains(symbol))
                    .ToList();

                var file = Path.Combine(Path.Combine(AppContext.BaseDirectory, "Portfolios"),
                    string.Concat(current.Name.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)) + ".json");

                if (!File.Exists(file))
                {
                    var matchingFile = Directory.Exists(Path.Combine(AppContext.BaseDirectory, "Portfolios"))
                        ? Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Portfolios"), "*.json")
                            .FirstOrDefault(f =>
                            {
                                try
                                {
                                    var d = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(f));
                                    return d != null && string.Equals(d.Name?.Trim(), current.Name.Trim(), StringComparison.OrdinalIgnoreCase);
                                }
                                catch { return false; }
                            })
                        : null;
                    file = matchingFile ?? file;
                }

                var json = JsonSerializer.Serialize(current, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file, json, new System.Text.UTF8Encoding(false));

                // After deletion the grid is cleared, the portfolio list is rebuilt,
                // and the current portfolio is selected again so its remaining symbols are reloaded.
                RefreshPortfolioListAndClearSelection();
                var index = portfolioComboBox.Items.IndexOf(current.Name);
                if (index >= 0)
                    portfolioComboBox.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"حذف سهم انجام نشد:\n{ex.Message}", "حذف سهم", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSelectionControls()
        {
            if (stocksDataGridView.Rows.Count == 0)
            {
                selectAllCheckBox.Checked = false;
                selectNoneCheckBox.Checked = false;
                return;
            }

            var selected = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Count(row => Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false));
            selectAllCheckBox.Checked = selected == stocksDataGridView.Rows.Count;
            selectNoneCheckBox.Checked = selected == 0;
        }

        private static string ToPersianDigits(string value)
        {
            return value
                .Replace('0', '۰').Replace('1', '۱').Replace('2', '۲').Replace('3', '۳').Replace('4', '۴')
                .Replace('5', '۵').Replace('6', '۶').Replace('7', '۷').Replace('8', '۸').Replace('9', '۹');
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e) { }
        private void pastDaysStatusComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void identifierMainGroup_Enter(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}