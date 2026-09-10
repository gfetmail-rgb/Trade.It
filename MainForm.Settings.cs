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

            var symbols = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false))
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

            if (!int.TryParse(navigationSpeedTextBox.Text.Trim(), out var seconds) || seconds <= 0)
            {
                MessageBox.Show(this, "سرعت پیمایش را به صورت تعداد ثانیه وارد کنید.", "پیمایش", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                navigationSpeedTextBox.Focus();
                return;
            }

            navigationSymbols = symbols;
            navigationIndex = 0;
            navigationRunning = true;
            navigationTimer.Interval = Math.Clamp(seconds * 1000, 100, 3600000);
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
