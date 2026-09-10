using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Trade.It
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, PortfolioDefinition> loadedPortfolios = new(StringComparer.OrdinalIgnoreCase);
        private bool internalPortfolioUpdate;
        private string? displayedPortfolioName;
        private bool tradingStatusFilterInitialized;
        private Label? filterCountLabel;
        private bool resettingFilters;
        private bool comparisonFiltersInitialized;
        private bool comparisonFilterEventsAttached;
        private bool ohlcChangeFilterEventsAttached;
        private TextBox textBox1;

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
            stocksDataGridView.CellClick += StocksDataGridView_CellClick;
            deleteButton.Click += DeleteButton_Click;
            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChanged;
            selectNoneCheckBox.CheckedChanged += SelectNoneCheckBox_CheckedChanged;
            Load += MainForm_Portfolios_Load;

            AttachOhlcChangeFilterEvents();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (comparisonFiltersInitialized)
                return;

            comparisonFiltersInitialized = true;
            ConfigureComparisonFilterControls();
            BeginInvoke(new Action(AttachComparisonFilterEvents));
        }

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

        private void MainForm_Portfolios_Load(object? sender, EventArgs e)
        {
            RefreshPortfolioListAndClearSelection();
        }

        private void RefreshPortfolioButton_Click(object? sender, EventArgs e)
        {
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
                            catch { return false; }
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
                    catch { }
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

        private void StocksDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= stocksDataGridView.Rows.Count)
                return;

            var sourceRow = stocksDataGridView.Rows[e.RowIndex];
            if (sourceRow.IsNewRow)
                return;

            stockPreviewGrid.Rows.Clear();
            stockPreviewGrid.Rows.Add(
                sourceRow.Cells["rowColumn"].Value,
                sourceRow.Cells["symbolColumn"].Value,
                sourceRow.Cells["lastTradeColumn"].Value,
                sourceRow.Cells["selectColumn"].Value);
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
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var current))
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
                file = Path.Combine(folder, $"{safeName}-{suffix++}.json");

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
                File.WriteAllText(file, json, new UTF8Encoding(false));
                RefreshPortfolioListAndClearSelection();
                var index = portfolioComboBox.Items.IndexOf(newName);
                if (index >= 0)
                    portfolioComboBox.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"ساخت سبد جدید انجام نشد:\n{ex.Message}", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var current))
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
                selectedSymbols.Count == 1 ? $"آیا سهم «{selectedSymbols[0]}» از سبد «{current.Name}» حذف شود؟" : $"آیا {ToPersianDigits(selectedSymbols.Count.ToString())} سهم انتخاب‌شده از سبد «{current.Name}» حذف شوند؟",
                "حذف سهم", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes)
                return;

            try
            {
                var remove = new HashSet<string>(selectedSymbols, StringComparer.OrdinalIgnoreCase);
                current.Symbols = (current.Symbols ?? new List<string>()).Where(symbol => !remove.Contains(symbol)).ToList();

                var file = Path.Combine(Path.Combine(AppContext.BaseDirectory, "Portfolios"), string.Concat(current.Name.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)) + ".json");
                if (!File.Exists(file))
                {
                    var matchingFile = Directory.Exists(Path.Combine(AppContext.BaseDirectory, "Portfolios"))
                        ? Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Portfolios"), "*.json").FirstOrDefault(f =>
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
                File.WriteAllText(file, json, new UTF8Encoding(false));
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

            var selected = stocksDataGridView.Rows.Cast<DataGridViewRow>().Count(row => Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false));
            selectAllCheckBox.Checked = selected == stocksDataGridView.Rows.Count;
            selectNoneCheckBox.Checked = selected == 0;
        }

        private static string ToPersianDigits(string value)
        {
            return value.Replace('0', '۰').Replace('1', '۱').Replace('2', '۲').Replace('3', '۳').Replace('4', '۴')
                .Replace('5', '۵').Replace('6', '۶').Replace('7', '۷').Replace('8', '۸').Replace('9', '۹');
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
            return GetMappingColumn(definition, "تاریخ") > 0 || GetMappingColumn(definition, "تاریخ لاتین") > 0;
        }

        private static bool HasVolumeColumn(PortfolioDefinition? definition)
        {
            if (definition == null)
                return false;
            return GetMappingColumn(definition, "حجم") > 0 || GetMappingColumn(definition, "حجم معاملات") > 0;
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
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
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
            if (mode < 0) mode = 0;
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

        private static string NormalizeSymbolName(string value) => (value ?? string.Empty).Trim().Replace('ي', 'ی').Replace('ى', 'ی').Replace('ك', 'ک');

        private void UpdateFilterCounts(int foundCount, int totalCount)
        {
            if (filterCountLabel != null)
                filterCountLabel.Text = $"کل: {ToPersianDigits(totalCount.ToString())}    پیدا شده: {ToPersianDigits(foundCount.ToString())}";
        }

        private IEnumerable<string> ApplyVolumeRatioFilter(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            if (!HasVolumeColumn(definition)) return symbols;
            var ratioText = NormalizeTradingDigits(volumeRatioTextBox.Text).Trim();
            var nText = NormalizeTradingDigits(textBox1.Text).Trim();
            if (!double.TryParse(ratioText, NumberStyles.Float, CultureInfo.InvariantCulture, out var threshold) ||
                !int.TryParse(nText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0)
                return symbols;
            if (volumeRatioOperatorComboBox.SelectedIndex < 0 || volumeRatioOperatorComboBox.SelectedItem == null) return symbols;
            var op = volumeRatioOperatorComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;
            return symbols.Where(symbol => TryGetLatestVolumeRatio(definition, symbol, n, out var ratio) && CompareNumeric(ratio, threshold, op));
        }

        private bool TryGetLatestVolumeRatio(PortfolioDefinition definition, string symbol, int n, out double ratio)
        {
            ratio = 0;
            var volumeColumn = GetMappingColumn(definition, "حجم");
            if (volumeColumn <= 0) volumeColumn = GetMappingColumn(definition, "حجم معاملات");
            if (volumeColumn <= 0 || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath)) return false;
            var volumes = new List<double>();
            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                    ReadVolumesFromFile(definition, file, symbol, volumeColumn, volumes);
            }
            catch { return false; }
            if (volumes.Count < n + 1) return false;
            var last = volumes[^1];
            var average = volumes.Skip(volumes.Count - n - 1).Take(n).Average();
            if (average <= 0 || double.IsNaN(last) || double.IsInfinity(last)) return false;
            ratio = last / average;
            return !double.IsNaN(ratio) && !double.IsInfinity(ratio);
        }

        private void ReadVolumesFromFile(PortfolioDefinition definition, string filePath, string symbol, int volumeColumn, List<double> volumes)
        {
            var symbolColumn = GetMappingColumn(definition, "نماد");
            var firstLine = true;
            foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var row = SplitTradingDataLine(line, definition.Separator);
                if (firstLine && definition.HasHeader) { firstLine = false; continue; }
                firstLine = false;
                if (definition.SymbolSource == SymbolSource.InsideFile &&
                    (symbolColumn <= 0 || symbolColumn > row.Length || !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))) continue;
                if (volumeColumn > row.Length) continue;
                if (TryParseTradingNumber(row[volumeColumn - 1], out var volume)) volumes.Add(volume);
            }
        }

        private IEnumerable<string> ApplyPastDaysFilter(IEnumerable<string> symbols, PortfolioDefinition definition)
        {
            if (!HasDateColumn(definition)) return symbols;
            var nText = NormalizeTradingDigits(pastDaysTextBox.Text).Trim();
            if (!int.TryParse(nText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n < 0) return symbols;
            if (pastDaysStatusComboBox.SelectedIndex < 0 || pastDaysStatusComboBox.SelectedItem == null) return symbols;
            var targetDate = DateTime.Today.AddDays(-n).Date;
            var op = pastDaysStatusComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;
            var showTraded = !IsNegativePastDaysOption(op);
            return symbols.Where(symbol => showTraded == HasTradeOnDate(definition, symbol, targetDate));
        }

        private static bool IsNegativePastDaysOption(string value)
        {
            var normalized = NormalizeSymbolName(value);
            return normalized.Contains("نداشته", StringComparison.OrdinalIgnoreCase) || normalized.Contains("خیر", StringComparison.OrdinalIgnoreCase) || normalized.Contains("عدم", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasTradeOnDate(PortfolioDefinition definition, string symbol, DateTime targetDate)
        {
            if (!HasDateColumn(definition) || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath)) return false;
            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0) dateColumn = GetMappingColumn(definition, "تاریخ لاتین");
            if (dateColumn <= 0) return false;
            var symbolColumn = GetMappingColumn(definition, "نماد");
            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol))
                    if (FileContainsDate(definition, file, symbol, symbolColumn, dateColumn, targetDate)) return true;
            }
            catch { }
            return false;
        }

        private bool FileContainsDate(PortfolioDefinition definition, string filePath, string symbol, int symbolColumn, int dateColumn, DateTime targetDate)
        {
            var firstLine = true;
            foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var row = SplitTradingDataLine(line, definition.Separator);
                if (firstLine && definition.HasHeader) { firstLine = false; continue; }
                firstLine = false;
                if (definition.SymbolSource == SymbolSource.InsideFile &&
                    (symbolColumn <= 0 || symbolColumn > row.Length || !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))) continue;
                if (dateColumn <= row.Length && TryParseSourceDate(row[dateColumn - 1], definition, out var date) && date.Date == targetDate) return true;
            }
            return false;
        }

        private bool HasTradeOnToday(PortfolioDefinition definition, string symbol) => HasTradeOnDate(definition, symbol, DateTime.Today.Date);

        private static DateTime? ParseTradingDate(string value, PortfolioDefinition definition)
        {
            return TryParseSourceDate(value, definition, out var date) ? date : null;
        }

        private static bool TryParseSourceDate(string value, PortfolioDefinition definition, out DateTime date)
        {
            date = default;
            var normalized = NormalizeTradingDigits(value).Trim();
            if (string.IsNullOrWhiteSpace(normalized)) return false;
            var parts = normalized.Split(new[] { '/', '-', '.', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int year, month, day;
            if (parts.Length >= 3 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) && int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out month) && int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out day))
            {
            }
            else
            {
                var digits = new string(normalized.Where(char.IsDigit).ToArray());
                if (digits.Length < 8) return false;
                if (!int.TryParse(digits[..4], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) || !int.TryParse(digits.Substring(4, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out month) || !int.TryParse(digits.Substring(6, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out day)) return false;
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
            catch { return false; }
        }

        private static bool CompareNumeric(double left, double right, string op)
        {
            var normalized = NormalizeSymbolName(op);
            return normalized switch
            {
                ">" => left > right, ">=" => left >= right, "<" => left < right, "<=" => left <= right,
                "=" => Math.Abs(left - right) < 1e-12, "==" => Math.Abs(left - right) < 1e-12,
                "مساوی" => Math.Abs(left - right) < 1e-12, "بزرگتر از" => left > right, "بزرگتر" => left > right,
                "بزرگتر یا مساوی" => left >= right, "بزرگتر مساوی" => left >= right, "کوچکتر از" => left < right,
                "کوچکتر" => left < right, "کوچکتر یا مساوی" => left <= right, "کوچکتر مساوی" => left <= right,
                _ => false
            };
        }

        private static bool TryParseTradingNumber(string value, out double number)
        {
            var normalized = NormalizeTradingDigits(value).Trim().Replace(",", string.Empty).Replace("٬", string.Empty).Replace(" ", string.Empty);
            return double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out number);
        }

        private IEnumerable<string> GetSymbolFiles(PortfolioDefinition definition, string symbol)
        {
            var extension = definition.FileType?.Trim().ToUpperInvariant() switch { "CSV" => ".csv", "PRN" => ".prn", _ => ".txt" };
            if (definition.SymbolSource == SymbolSource.FileName)
            {
                var direct = Path.Combine(definition.DataPath, symbol + extension);
                if (File.Exists(direct)) { yield return direct; yield break; }
                foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly))
                    if (string.Equals(Path.GetFileNameWithoutExtension(file), symbol, StringComparison.OrdinalIgnoreCase)) yield return file;
                yield break;
            }
            foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly)) yield return file;
        }

        private static int GetMappingColumn(PortfolioDefinition definition, string field)
        {
            var mapping = definition.Mappings?.FirstOrDefault(m => string.Equals(m.Field?.Trim(), field, StringComparison.OrdinalIgnoreCase));
            return mapping?.Column ?? 0;
        }

        private static string NormalizeTradingDigits(string value) => value.Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3').Replace('۴', '4').Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');

        private static string[] SplitTradingDataLine(string line, string? separator) => line.Split(new[] { string.IsNullOrEmpty(separator) ? "," : separator }, StringSplitOptions.None);

        private static Encoding DetectTradingDataEncoding(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            if (stream.Length >= 3)
            {
                var bom = new byte[3];
                stream.ReadExactly(bom, 0, 3);
                if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) return new UTF8Encoding(false);
            }
            return Encoding.UTF8;
        }

        private void ConfigureComparisonFilterControls()
        {
            var filters = new[]
            {
                (comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox),
                (comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6),
                (comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7),
                (comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8)
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
            if (comparisonFilterEventsAttached || IsDisposed) return;
            comparisonFilterEventsAttached = true;
            var filters = new[]
            {
                (comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox),
                (comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6),
                (comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7),
                (comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8)
            };
            foreach (var filter in filters)
            {
                filter.Item1.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item2.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item3.SelectedIndexChanged += ComparisonFilterChanged;
                filter.Item4.TextChanged += ComparisonFilterChanged;
                filter.Item5.TextChanged += ComparisonFilterChanged;
            }
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
                if (string.Equals(comboBox.Items[i]?.ToString()?.Trim(), "FINAL FEE", StringComparison.OrdinalIgnoreCase)) comboBox.Items[i] = "پایانی";
        }

        private void ComparisonFilterChanged(object? sender, EventArgs e) => ApplyComparisonFiltersToGridWithWaitCursor();

        private void ComparisonBaseFilterChanged(object? sender, EventArgs e)
        {
            if (resettingFilters) return;
            ApplyComparisonFiltersToGridWithWaitCursor();
        }

        private void ApplyComparisonFiltersToGridWithWaitCursor()
        {
            if (!HasAnyActiveComparisonFilter()) return;
            UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            try { Application.DoEvents(); ApplyComparisonFiltersToGrid(); }
            finally { UseWaitCursor = false; Cursor.Current = Cursors.Default; Application.DoEvents(); }
        }

        private void ApplyComparisonFiltersToGrid()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition)) return;
            IEnumerable<string> filtered = GetDisplayedGridSymbols();
            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox);
            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6);
            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7);
            filtered = ApplyComparisonFilter(filtered, definition, comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8);
            var result = filtered.ToList();
            var totalCount = (definition.Symbols ?? new List<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            UpdateFilterCounts(result.Count, totalCount);
            internalPortfolioUpdate = true;
            try
            {
                stocksDataGridView.Rows.Clear();
                for (var i = 0; i < result.Count; i++) stocksDataGridView.Rows.Add(i + 1, result[i], "", false);
            }
            finally { internalPortfolioUpdate = false; }
            selectAllCheckBox.Checked = false;
            selectNoneCheckBox.Checked = result.Count > 0;
            UpdateSelectionControls();
        }

        private IEnumerable<string> GetDisplayedGridSymbols()
        {
            foreach (DataGridViewRow row in stocksDataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                var symbol = Convert.ToString(row.Cells.Count > 1 ? row.Cells[1].Value : null)?.Trim();
                if (!string.IsNullOrWhiteSpace(symbol)) yield return symbol;
            }
        }

        private bool HasAnyActiveComparisonFilter() =>
            IsComparisonFilterActive(comparisonFirstComboBox, comparisonOperatorComboBox, comparisonSecondComboBox, comparisonFirstTextBox, comparisonSecondTextBox) ||
            IsComparisonFilterActive(comparisonFirstComboBox6, comparisonOperatorComboBox6, comparisonSecondComboBox6, comparisonFirstTextBox6, comparisonSecondTextBox6) ||
            IsComparisonFilterActive(comparisonFirstComboBox7, comparisonOperatorComboBox7, comparisonSecondComboBox7, comparisonFirstTextBox7, comparisonSecondTextBox7) ||
            IsComparisonFilterActive(comparisonFirstComboBox8, comparisonOperatorComboBox8, comparisonSecondComboBox8, comparisonFirstTextBox8, comparisonSecondTextBox8);

        private static bool IsComparisonFilterActive(ComboBox firstFieldComboBox, ComboBox operatorComboBox, ComboBox secondFieldComboBox, TextBox firstOffsetTextBox, TextBox secondOffsetTextBox) =>
            TryGetComparisonSettings(firstFieldComboBox, operatorComboBox, secondFieldComboBox, firstOffsetTextBox, secondOffsetTextBox, out _, out _, out _, out _, out _);

        private IEnumerable<string> ApplyComparisonFilter(IEnumerable<string> symbols, PortfolioDefinition definition, ComboBox firstFieldComboBox, ComboBox operatorComboBox, ComboBox secondFieldComboBox, TextBox firstOffsetTextBox, TextBox secondOffsetTextBox)
        {
            if (!TryGetComparisonSettings(firstFieldComboBox, operatorComboBox, secondFieldComboBox, firstOffsetTextBox, secondOffsetTextBox, out var firstField, out var op, out var secondField, out var firstOffset, out var relativeOffset)) return symbols;
            int totalOffset;
            try { totalOffset = checked(firstOffset + relativeOffset); } catch (OverflowException) { return Enumerable.Empty<string>(); }
            return symbols.Where(symbol => TryGetComparisonValues(definition, symbol, firstField, secondField, firstOffset, totalOffset, out var left, out var right) && CompareComparisonOperator(left, right, op));
        }

        private static bool TryGetComparisonSettings(ComboBox firstFieldComboBox, ComboBox operatorComboBox, ComboBox secondFieldComboBox, TextBox firstOffsetTextBox, TextBox secondOffsetTextBox, out string firstField, out string op, out string secondField, out int firstOffset, out int relativeOffset)
        {
            firstField = string.Empty; op = string.Empty; secondField = string.Empty; firstOffset = 0; relativeOffset = 0;
            if (firstFieldComboBox.SelectedItem == null || operatorComboBox.SelectedItem == null || secondFieldComboBox.SelectedItem == null) return false;
            if (!int.TryParse(NormalizeTradingDigits(firstOffsetTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out firstOffset) || firstOffset < 0) return false;
            if (!int.TryParse(NormalizeTradingDigits(secondOffsetTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out relativeOffset) || relativeOffset < 0) return false;
            firstField = NormalizeComparisonField(firstFieldComboBox.SelectedItem.ToString());
            secondField = NormalizeComparisonField(secondFieldComboBox.SelectedItem.ToString());
            op = operatorComboBox.SelectedItem.ToString()?.Trim() ?? string.Empty;
            return !string.IsNullOrEmpty(firstField) && !string.IsNullOrEmpty(secondField) && !string.IsNullOrEmpty(op);
        }

        private static string NormalizeComparisonField(string? value) => (value ?? string.Empty).Trim().ToUpperInvariant() switch
        {
            "O" => "باز", "H" => "بیشترین", "L" => "کمترین", "C" => "پایانی", "V" => "حجم", "FINAL FEE" => "پایانی", "پایانی" => "پایانی", _ => string.Empty
        };

        private bool TryGetComparisonValues(PortfolioDefinition definition, string symbol, string firstField, string secondField, int firstOffset, int secondOffset, out double left, out double right)
        {
            left = 0; right = 0;
            var firstColumn = GetMappingColumn(definition, firstField);
            var secondColumn = GetMappingColumn(definition, secondField);
            if (firstColumn <= 0 || secondColumn <= 0 || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath)) return false;
            var rows = new List<(double? First, double? Second)>();
            var symbolColumn = GetMappingColumn(definition, "نماد");
            try
            {
                foreach (var file in GetSymbolFiles(definition, symbol)) ReadComparisonRows(definition, file, symbol, symbolColumn, firstColumn, secondColumn, rows);
            }
            catch { return false; }
            var firstIndex = rows.Count - 1 - firstOffset;
            var secondIndex = rows.Count - 1 - secondOffset;
            if (firstIndex < 0 || secondIndex < 0 || firstIndex >= rows.Count || secondIndex >= rows.Count) return false;
            var first = rows[firstIndex].First; var second = rows[secondIndex].Second;
            if (!first.HasValue || !second.HasValue) return false;
            left = first.Value; right = second.Value;
            return !double.IsNaN(left) && !double.IsInfinity(left) && !double.IsNaN(right) && !double.IsInfinity(right);
        }

        private void ReadComparisonRows(PortfolioDefinition definition, string filePath, string symbol, int symbolColumn, int firstColumn, int secondColumn, List<(double? First, double? Second)> rows)
        {
            var firstLine = true;
            foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var row = SplitTradingDataLine(line, definition.Separator);
                if (firstLine && definition.HasHeader) { firstLine = false; continue; }
                firstLine = false;
                if (definition.SymbolSource == SymbolSource.InsideFile && (symbolColumn <= 0 || symbolColumn > row.Length || !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))) continue;
                if (firstColumn > row.Length || secondColumn > row.Length) { rows.Add((null, null)); continue; }
                double? firstValue = TryParseTradingNumber(row[firstColumn - 1], out var firstParsed) ? firstParsed : null;
                double? secondValue = TryParseTradingNumber(row[secondColumn - 1], out var secondParsed) ? secondParsed : null;
                rows.Add((firstValue, secondValue));
            }
        }

        private static bool CompareComparisonOperator(double left, double right, string op) => op.Trim() switch
        {
            ">" => left > right, "<" => left < right, "=" => Math.Abs(left - right) < 1e-12,
            ">=" => left >= right, "<=" => left <= right, "!=" => Math.Abs(left - right) >= 1e-12, _ => false
        };

        private void AttachOhlcChangeFilterEvents()
        {
            if (ohlcChangeFilterEventsAttached || IsDisposed) return;
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

        private void UpdateOhlcChangeFilterAvailability() => ohlcChangeFilterGroup.Enabled = true;

        private void ApplyOhlcChangeFilterToGridWithWaitCursor()
        {
            if (!IsOhlcChangeFilterActive()) return;
            UseWaitCursor = true; Cursor.Current = Cursors.WaitCursor;
            try { Application.DoEvents(); ApplyOhlcChangeFilterToGrid(); }
            finally { UseWaitCursor = false; Cursor.Current = Cursors.Default; Application.DoEvents(); }
        }

        private bool IsOhlcChangeFilterActive()
        {
            if (ohlcChangeFieldComboBox.SelectedItem == null || ohlcChangeDirectionComboBox.SelectedItem == null) return false;
            if (string.IsNullOrEmpty(NormalizeOhlcChangeField(ohlcChangeFieldComboBox.SelectedItem.ToString()))) return false;
            if (!int.TryParse(NormalizeTradingDigits(ohlcChangeDaysTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0) return false;
            if (!double.TryParse(NormalizeTradingDigits(ohlcChangePercentTextBox.Text).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent) || percent < 0) return false;
            return GetOhlcChangeDirection() >= 0;
        }

        private int GetOhlcChangeDirection()
        {
            var value = ohlcChangeDirectionComboBox.SelectedItem?.ToString()?.Trim() ?? string.Empty;
            return value.Contains("رشد", StringComparison.Ordinal) ? 1 : value.Contains("افت", StringComparison.Ordinal) ? -1 : -1;
        }

        private void ApplyOhlcChangeFilterToGrid()
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition)) return;
            var field = NormalizeOhlcChangeField(ohlcChangeFieldComboBox.SelectedItem?.ToString());
            if (string.IsNullOrEmpty(field)) return;
            if (!int.TryParse(NormalizeTradingDigits(ohlcChangeDaysTextBox.Text).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) || n <= 0) return;
            if (!double.TryParse(NormalizeTradingDigits(ohlcChangePercentTextBox.Text).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent) || percent < 0) return;
            var direction = GetOhlcChangeDirection();
            if (direction < 0) return;
            var symbols = GetDisplayedGridSymbolsForOhlcFilter().ToList();
            var result = symbols.Where(symbol => TryGetOhlcChange(definition, symbol, field, n, out var changePercent) && (direction > 0 ? changePercent >= percent : changePercent <= -percent)).ToList();
            var totalCount = (definition.Symbols ?? new List<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            UpdateFilterCounts(result.Count, totalCount);
            internalPortfolioUpdate = true;
            try
            {
                stocksDataGridView.Rows.Clear();
                for (var i = 0; i < result.Count; i++) stocksDataGridView.Rows.Add(i + 1, result[i], "", false);
            }
            finally { internalPortfolioUpdate = false; }
            selectAllCheckBox.Checked = false; selectNoneCheckBox.Checked = result.Count > 0; UpdateSelectionControls();
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
                foreach (var file in GetSymbolFiles(definition, symbol)) ReadOhlcValues(definition, file, symbol, symbolColumn, column, values);
            }
            catch { return false; }
            if (values.Count <= n) return false;
            var latest = values[^1]; var previous = values[values.Count - 1 - n];
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
                if (definition.SymbolSource == SymbolSource.InsideFile && (symbolColumn <= 0 || symbolColumn > row.Length || !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase))) continue;
                if (valueColumn > row.Length) continue;
                if (TryParseTradingNumber(row[valueColumn - 1], out var value)) values.Add(value);
            }
        }

        private static string NormalizeOhlcChangeField(string? value) => (value ?? string.Empty).Trim().ToUpperInvariant() switch
        {
            "O" => "باز", "H" => "بیشترین", "L" => "کمترین", "C" => "پایانی", "FINAL FEE" => "پایانی", "پایانی" => "پایانی", _ => string.Empty
        };
    }
}
