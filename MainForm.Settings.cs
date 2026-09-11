namespace Trade.It
{
    public partial class MainForm
    {
        private ChartDisplayMode chartDisplayMode = ChartDisplayMode.SeparateTabs;
        private bool settingsMenuInitialized;
        private readonly System.Windows.Forms.Timer navigationTimer = new();
        private bool navigationRunning;
        private List<string> navigationSymbols = new();
        private int navigationIndex = -1;
        private TabPage? navigationTabPage;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (settingsMenuInitialized)
                return;

            settingsMenuInitialized = true;
            settingsMenuItem.Click += SettingsMenuItem_Click;
            navigationButton.Click += NavigationButton_Click;
            navigationTimer.Tick += NavigationTimer_Tick;
            portfolioComboBox.SelectedIndexChanged += NavigationPortfolioChanged;
            InitializeChartRuntime();
        }

        private void SettingsMenuItem_Click(object? sender, EventArgs e)
        {
            using var form = new SettingsForm(chartDisplayMode);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                chartDisplayMode = form.ChartDisplayMode;
                ApplyChartDisplayMode();
            }
        }

        private void NavigationButton_Click(object? sender, EventArgs e)
        {
            if (navigationRunning)
            {
                StopNavigation();
                return;
            }

            var selectedRow = stocksDataGridView.SelectedRows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(row => !row.IsNewRow);
            var selectedSymbol = selectedRow == null
                ? null
                : Convert.ToString(selectedRow.Cells[symbolColumn.Index].Value)?.Trim();

            var symbols = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && row.Cells[selectColumn.Index].Value is true)
                .Select(row => Convert.ToString(row.Cells[symbolColumn.Index].Value)?.Trim())
                .Where(symbol => !string.IsNullOrWhiteSpace(symbol))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (symbols.Count == 0)
            {
                symbols = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                    .Where(row => !row.IsNewRow)
                    .Select(row => Convert.ToString(row.Cells[symbolColumn.Index].Value)?.Trim())
                    .Where(symbol => !string.IsNullOrWhiteSpace(symbol))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }

            if (symbols.Count == 0)
            {
                MessageBox.Show(this, "هیچ سهمی برای پیمایش وجود ندارد.", "پیمایش", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(navigationSpeedTextBox.Text.Trim(), out var milliseconds) || milliseconds <= 0)
            {
                MessageBox.Show(this, "سرعت پیمایش را به صورت تعداد میلی‌ثانیه وارد کنید.", "پیمایش", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                navigationSpeedTextBox.Focus();
                return;
            }

            navigationSymbols = symbols;
            navigationIndex = 0;

            if (!string.IsNullOrWhiteSpace(selectedSymbol))
            {
                var selectedIndex = navigationSymbols.FindIndex(symbol =>
                    string.Equals(symbol, selectedSymbol, StringComparison.OrdinalIgnoreCase));

                if (selectedIndex >= 0)
                    navigationIndex = selectedIndex;
            }

            navigationRunning = true;
            navigationTimer.Interval = Math.Clamp(milliseconds, 100, 3600000);
            navigationButton.UseVisualStyleBackColor = false;
            navigationButton.BackColor = SystemColors.Highlight;
            navigationButton.ForeColor = SystemColors.HighlightText;

            ShowNavigationChart(navigationSymbols[navigationIndex]);
            navigationTimer.Start();
        }

        private void NavigationTimer_Tick(object? sender, EventArgs e)
        {
            if (!navigationRunning || navigationSymbols.Count == 0)
                return;

            navigationIndex++;
            if (navigationIndex >= navigationSymbols.Count)
                navigationIndex = 0;

            ShowNavigationChart(navigationSymbols[navigationIndex]);
        }

        private void NavigationPortfolioChanged(object? sender, EventArgs e)
        {
            StopNavigation();
            CloseAllChartTabs();
        }

        private void ShowNavigationChart(string symbol)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
                return;

            try
            {
                var points = LoadChartData(definition, symbol);
                if (points.Count == 0)
                {
                    chartInfoLabel.Text = $"داده قابل رسم برای «{symbol}» پیدا نشد.";
                    chartPlaceholderLabel.Visible = true;
                    return;
                }

                activeChartSymbol = symbol;
                chartInfoLabel.Text = $"{symbol}   |   {points.Count:N0} رکورد";
                chartPlaceholderLabel.Visible = false;

                var chart = GetOrCreateChart(symbol);
                chart.Parent = null;
                chart.SetData(points);
                chart.SetChartType(GetSelectedChartType());
                chart.Visible = true;

                if (!chart.CrosshairVisible)
                    chart.ToggleCrosshair();

                if (navigationTabPage == null ||
                    navigationTabPage.IsDisposed ||
                    !chartTabControl.TabPages.Contains(navigationTabPage))
                {
                    navigationTabPage = new TabPage("__AUTO_SCROLL__")
                    {
                        RightToLeft = RightToLeft.Yes
                    };
                    chartTabControl.TabPages.Add(navigationTabPage);
                }

                navigationTabPage.Controls.Clear();
                navigationTabPage.Controls.Add(chart);
                navigationTabPage.Text = symbol;
                chartTabControl.SelectedTab = navigationTabPage;

                var row = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => !r.IsNewRow &&
                        string.Equals(
                            Convert.ToString(r.Cells[symbolColumn.Index].Value)?.Trim(),
                            symbol,
                            StringComparison.OrdinalIgnoreCase));

                if (row != null)
                {
                    stocksDataGridView.ClearSelection();
                    row.Selected = true;
                    stocksDataGridView.CurrentCell = row.Cells[symbolColumn.Index];
                    if (row.Index >= 0 && row.Index < stocksDataGridView.Rows.Count)
                    {
                        try
                        {
                            stocksDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                        }
                        catch (InvalidOperationException)
                        {
                            // The row may already be visible or scrolling may be unavailable.
                        }
                    }
                }

                SetToggleButtonState(crossButton, chart.CrosshairVisible);
                SetToggleButtonState(gridButton, chart.GridVisible);
                SetToggleButtonState(hideChartButton, false);
            }
            catch (Exception ex)
            {
                chartInfoLabel.Text = $"خطا در رسم چارت «{symbol}»: {ex.Message}";
                chartPlaceholderLabel.Visible = true;
            }
        }

        private void StopNavigation()
        {
            navigationRunning = false;
            navigationTimer.Stop();
            navigationSymbols.Clear();
            navigationIndex = -1;
            navigationButton.UseVisualStyleBackColor = true;
            navigationButton.BackColor = SystemColors.Control;
            navigationButton.ForeColor = SystemColors.ControlText;
        }
    }
}
