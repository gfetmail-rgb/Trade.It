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
        private bool resettingFilters;
        private bool comparisonFiltersInitialized;
        private bool comparisonFilterEventsAttached;
        private bool ohlcChangeFilterEventsAttached;
        private int latestTradeDateLoadVersion;
        private TextBox textBox1;

        public MainForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            mainMenuStrip.RightToLeft = RightToLeft.Yes;
            portfolioDefinitionMenuItem.Click += (_, _) => { using var form = new PortfolioDefinitionForm(); form.ShowDialog(this); RefreshPortfolioListAndClearSelection(); };
            portfolioManagementMenuItem.Click += (_, _) => { using var form = new PortfolioManagementForm(); form.ShowDialog(this); RefreshPortfolioListAndClearSelection(); };
            tabPage3.Controls.Clear();
            tabPage3.AutoScroll = true;
            var identifierButtonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 52, Padding = new Padding(4), FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes };
            foreach (var buttonInfo in new[] { (Text: "جدید", Width: 62), (Text: "ذخیره", Width: 70), (Text: "حذف", Width: 62), (Text: "حذف همه", Width: 80), (Text: "ورود از اکسل", Width: 105) }) identifierButtonsPanel.Controls.Add(new Button { Text = buttonInfo.Text, Width = buttonInfo.Width, Height = 36, Margin = new Padding(3) });
            var identifierGroup = new GroupBox { Dock = DockStyle.Top, AutoSize = true, Text = "اطلاعات شناسه", Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
            var identifierFields = new (string Label, bool Combo)[] { ("کد ۱۲ رقمی نماد", false), ("کد ۵ رقمی نماد", false), ("نام لاتین شرکت", false), ("کد ۴ رقمی شرکت", false), ("نام شرکت", false), ("نماد فارسی", false), ("نماد ۳۰ رقمی فارسی", false), ("کد ۱۲ رقمی شرکت", false), ("بازار", true), ("کد تابلو", false), ("کد گروه صنعت", false), ("گروه صنعت", false), ("کد زیر گروه صنعت", false), ("زیر گروه صنعت", false) };
            var identifierTable = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, RowCount = identifierFields.Length, Padding = new Padding(6), RightToLeft = RightToLeft.Yes };
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F)); identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            foreach (var field in identifierFields) { identifierTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F)); identifierTable.Controls.Add(new Label { Text = field.Label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4), Font = new Font("Segoe UI", 10F) }); Control input = field.Combo ? new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(4) } : new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }; identifierTable.Controls.Add(input); }
            identifierGroup.Controls.Add(identifierTable); tabPage3.Controls.Add(identifierGroup); tabPage3.Controls.Add(identifierButtonsPanel); identifierButtonsPanel.BringToFront();
            portfolioComboBox.SelectedIndexChanged += PortfolioComboBox_SelectedIndexChanged; newPortfolioButton.Click += NewPortfolioButton_Click; refreshButton.Click += RefreshStocksButton_Click; refreshButtonPortfolio.Click += RefreshPortfolioButton_Click; deleteButton.Click += DeleteButton_Click; selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChanged; selectNoneCheckBox.CheckedChanged += SelectNoneCheckBox_CheckedChanged; stocksDataGridView.CurrentCellDirtyStateChanged += StocksDataGridView_CurrentCellDirtyStateChanged; stocksDataGridView.CellValueChanged += StocksDataGridView_CellValueChanged; Load += MainForm_Portfolios_Load; fullScreenChartButton.Click += FullScreenChartButton_Click; AttachOhlcChangeFilterEvents();
        }

        private void FullScreenChartButton_Click(object? sender, EventArgs e) { mainSplitContainer.Panel1Collapsed = !mainSplitContainer.Panel1Collapsed; fullScreenChartButton.Text = mainSplitContainer.Panel1Collapsed ? "بازگشت" : "تمام صفحه"; }

        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); if (comparisonFiltersInitialized) return; comparisonFiltersInitialized = true; ConfigureComparisonFilterControls(); BeginInvoke(new Action(AttachComparisonFilterEvents)); }
        protected override void OnLoad(EventArgs e) { base.OnLoad(e); if (tradingStatusFilterInitialized) return; tradingStatusFilterInitialized = true; InitializeFilterStatusDisplay(); statusAllRadio.CheckedChanged += TradingStatusFilterChanged; statusPositiveRadio.CheckedChanged += TradingStatusFilterChanged; statusNegativeRadio.CheckedChanged += TradingStatusFilterChanged; portfolioComboBox.SelectedIndexChanged += TradingStatusPortfolioChanged; refreshButton.Click += TradingStatusRefreshChanged; nameComboBox.SelectedIndexChanged += NameFilterChanged; nameTextBox.TextChanged += NameFilterChanged; volumeRatioTextBox.TextChanged += AdditionalFilterChanged; volumeRatioOperatorComboBox.SelectedIndexChanged += AdditionalFilterChanged; textBox1.TextChanged += AdditionalFilterChanged; pastDaysTextBox.TextChanged += AdditionalFilterChanged; pastDaysStatusComboBox.SelectedIndexChanged += AdditionalFilterChanged; clearFiltersButton.Click += ClearFiltersButton_Click; controlTabControl.SelectedIndexChanged += FilterTabSelected; UpdateFilterControlAvailability(); }

        private void MainForm_Portfolios_Load(object? sender, EventArgs e) { RefreshPortfolioListAndClearSelection(); }
        private void RefreshPortfolioButton_Click(object? sender, EventArgs e) { RefreshPortfolioListAndClearSelection(); }
        private void RefreshStocksButton_Click(object? sender, EventArgs e) { if (string.IsNullOrWhiteSpace(displayedPortfolioName)) { stocksDataGridView.Rows.Clear(); UpdateSelectionControls(); return; } var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios"); if (!Directory.Exists(folder)) return; try { var currentName = displayedPortfolioName.Trim(); var safeName = string.Concat(currentName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)); var file = Path.Combine(folder, safeName + ".json"); if (!File.Exists(file)) file = Directory.GetFiles(folder, "*.json").FirstOrDefault(f => { try { var definition = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(f)); return definition != null && string.Equals(definition.Name?.Trim(), currentName, StringComparison.OrdinalIgnoreCase); } catch { return false; } }) ?? string.Empty; if (string.IsNullOrEmpty(file)) return; var refreshed = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(file)); if (refreshed == null || string.IsNullOrWhiteSpace(refreshed.Name)) return; loadedPortfolios[currentName] = refreshed; PopulateStocksGrid(refreshed); } catch (Exception ex) { MessageBox.Show(this, $"تازه‌سازی فهرست سهام انجام نشد:\n{ex.Message}", "تازه‌سازی", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
        private void RefreshPortfolioListAndClearSelection() { LoadPortfoliosIntoGrid(); portfolioComboBox.SelectedIndex = -1; stocksDataGridView.Rows.Clear(); displayedPortfolioName = null; UpdateSelectionControls(); }
        private void LoadPortfoliosIntoGrid() { internalPortfolioUpdate = true; try { loadedPortfolios.Clear(); portfolioComboBox.Items.Clear(); stocksDataGridView.Rows.Clear(); displayedPortfolioName = null; var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios"); if (!Directory.Exists(folder)) return; foreach (var file in Directory.GetFiles(folder, "*.json").OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)) { try { var json = File.ReadAllText(file); var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json); if (definition == null || string.IsNullOrWhiteSpace(definition.Name)) continue; var name = definition.Name.Trim(); if (loadedPortfolios.ContainsKey(name)) continue; loadedPortfolios[name] = definition; portfolioComboBox.Items.Add(name); } catch { } } } finally { internalPortfolioUpdate = false; } }
        private void PortfolioComboBox_SelectedIndexChanged(object? sender, EventArgs e) { if (internalPortfolioUpdate) return; if (portfolioComboBox.SelectedItem is not string name || !loadedPortfolios.TryGetValue(name, out var definition)) { stocksDataGridView.Rows.Clear(); displayedPortfolioName = null; UpdateSelectionControls(); return; } displayedPortfolioName = name; UseWaitCursor = true; Cursor.Current = Cursors.WaitCursor; Application.DoEvents(); try { PopulateStocksGrid(definition); } finally { UseWaitCursor = false; Cursor.Current = Cursors.Default; } }
        private void PopulateStocksGrid(PortfolioDefinition definition) { var symbols = (definition.Symbols ?? new List<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList(); var loadVersion = ++latestTradeDateLoadVersion; internalPortfolioUpdate = true; try { stocksDataGridView.Rows.Clear(); for (var i = 0; i < symbols.Count; i++) stocksDataGridView.Rows.Add(i + 1, symbols[i], string.Empty, false); } finally { internalPortfolioUpdate = false; } selectAllCheckBox.Checked = false; selectNoneCheckBox.Checked = true; UpdateSelectionControls(); _ = LoadLatestTradeDatesAsync(definition, symbols, loadVersion); }
        private async Task LoadLatestTradeDatesAsync(PortfolioDefinition definition, List<string> symbols, int loadVersion) { if (!HasDateColumn(definition) || symbols.Count == 0) return; const int batchSize = 25; for (var start = 0; start < symbols.Count; start += batchSize) { if (loadVersion != latestTradeDateLoadVersion || IsDisposed) return; var batchStart = start; var batch = await Task.Run(() => { var values = new string[Math.Min(batchSize, symbols.Count - batchStart)]; for (var i = 0; i < values.Length; i++) values[i] = GetLatestTradeDateText(definition, symbols[batchStart + i]); return values; }); if (loadVersion != latestTradeDateLoadVersion || IsDisposed) return; for (var i = 0; i < batch.Length && batchStart + i < stocksDataGridView.Rows.Count; i++) stocksDataGridView.Rows[batchStart + i].Cells[lastTradeColumn.Index].Value = batch[i]; await Task.Yield(); } }
        private void StocksDataGridView_CurrentCellDirtyStateChanged(object? sender, EventArgs e) { if (stocksDataGridView.IsCurrentCellDirty && stocksDataGridView.CurrentCell is DataGridViewCheckBoxCell) stocksDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit); }
        private void StocksDataGridView_CellValueChanged(object? sender, DataGridViewCellEventArgs e) { if (internalPortfolioUpdate || e.RowIndex < 0 || e.ColumnIndex != selectColumn.Index) return; UpdateSelectionControls(); }
        private void SelectAllCheckBox_CheckedChanged(object? sender, EventArgs e) { if (internalPortfolioUpdate || !selectAllCheckBox.Checked) return; internalPortfolioUpdate = true; try { foreach (DataGridViewRow row in stocksDataGridView.Rows) row.Cells[selectColumn.Index].Value = true; selectNoneCheckBox.Checked = false; } finally { internalPortfolioUpdate = false; } }
        private void SelectNoneCheckBox_CheckedChanged(object? sender, EventArgs e) { if (internalPortfolioUpdate || !selectNoneCheckBox.Checked) return; internalPortfolioUpdate = true; try { foreach (DataGridViewRow row in stocksDataGridView.Rows) row.Cells[selectColumn.Index].Value = false; selectAllCheckBox.Checked = false; } finally { internalPortfolioUpdate = false; } }

        private void NewPortfolioButton_Click(object? sender, EventArgs e) { if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var current)) { MessageBox.Show(this, "ابتدا یک سبد جاری انتخاب کنید.", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } var selectedSymbols = stocksDataGridView.Rows.Cast<DataGridViewRow>().Where(row => !row.IsNewRow && Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false)).Select(row => Convert.ToString(row.Cells[symbolColumn.Index].Value) ?? string.Empty).Where(symbol => !string.IsNullOrWhiteSpace(symbol)).Distinct(StringComparer.OrdinalIgnoreCase).ToList(); if (selectedSymbols.Count == 0) { MessageBox.Show(this, "حداقل یک سهم را انتخاب کنید.", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } var newName = $"{current.Name.Trim()} {DateTime.Now:yyyyMMdd-HHmmss}"; var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios"); Directory.CreateDirectory(folder); var safeName = string.Concat(newName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)); var file = Path.Combine(folder, safeName + ".json"); var suffix = 1; while (File.Exists(file)) file = Path.Combine(folder, $"{safeName}-{suffix++}.json"); var newDefinition = new PortfolioDefinition { Name = newName, SymbolSource = current.SymbolSource, DataPath = current.DataPath, FileType = current.FileType, Separator = current.Separator, HasHeader = current.HasHeader, NoDateTime = current.NoDateTime, Calendar = current.Calendar, DateFormat = current.DateFormat, TimeFormat = current.TimeFormat, Mappings = current.Mappings?.Select(m => new PortfolioMapping(m.Field, m.Column)).ToList() ?? new List<PortfolioMapping>(), Symbols = selectedSymbols }; try { var json = JsonSerializer.Serialize(newDefinition, new JsonSerializerOptions { WriteIndented = true }); File.WriteAllText(file, json, new UTF8Encoding(false)); RefreshPortfolioListAndClearSelection(); var index = portfolioComboBox.Items.IndexOf(newName); if (index >= 0) portfolioComboBox.SelectedIndex = index; } catch (Exception ex) { MessageBox.Show(this, $"ساخت سبد جدید انجام نشد:\n{ex.Message}", "سبد جدید", MessageBoxButtons.OK, MessageBoxIcon.Error); } }

        private void UpdateNameFilterOptions()
        {
            nameComboBox.Items.Clear();
            nameComboBox.Items.AddRange(new object[] { "در ابتدا", "در انتها", "در میانه", "در هرجا", "در هیچ جا" });
            nameComboBox.SelectedIndex = 0;
        }

        private bool MatchesNameFilter(string symbol, string text, string mode)
        {
            if (string.IsNullOrEmpty(text)) return true;
            return mode switch
            {
                "در ابتدا" => symbol.StartsWith(text, StringComparison.OrdinalIgnoreCase),
                "در انتها" => symbol.EndsWith(text, StringComparison.OrdinalIgnoreCase),
                "در میانه" => symbol.Length > text.Length && symbol.IndexOf(text, StringComparison.OrdinalIgnoreCase) > 0 && symbol.IndexOf(text, StringComparison.OrdinalIgnoreCase) + text.Length < symbol.Length,
                "در هرجا" => symbol.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0,
                "در هیچ جا" => symbol.IndexOf(text, StringComparison.OrdinalIgnoreCase) < 0,
                _ => true
            };
        }

        private static bool CompareFilterValue(double value, double target, string op) => op switch
        {
            ">" => value > target,
            "<" => value < target,
            "=" => Math.Abs(value - target) < 0.0000001,
            ">=" => value >= target,
            "<=" => value <= target,
            "<>" => Math.Abs(value - target) >= 0.0000001,
            _ => true
        };
    }
}
