using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Trade.It
{
    public partial class MainForm
    {
        private readonly Dictionary<string, TradingChartControl> chartControls = new(StringComparer.OrdinalIgnoreCase);
        private bool chartRuntimeInitialized;
        private string? activeChartSymbol;
        private bool testMode;
        private MultiTimeframeWorkspace? multiTimeframeWorkspace;

        // آخرین تنظیمات عمومی پنل حجم؛ برای چارت‌های جدید استفاده می‌شود.
        private bool lastVolumePanelVisible = true;
        private double lastVolumePanelRatio = 0.22;

        private bool chartDrawingToolsInitialized;
        private readonly System.Windows.Forms.Timer drawingStateTimer = new();
        private readonly System.Windows.Forms.Timer analysisAutoSaveTimer = new();

        private void InitializeChartRuntime()
        {
            if (chartRuntimeInitialized) return;
            chartRuntimeInitialized = true;
            chartTypeComboBox.SelectedIndexChanged += ChartTypeComboBox_SelectedIndexChanged;
            stocksDataGridView.CellClick += StocksDataGridView_CellClickForChart;
            resetChartButton.Click += (_, _) => GetActiveChart()?.ResetView();
            zoomInButton.Click += (_, _) => GetActiveChart()?.ZoomX(0.75);
            zoomOutButton.Click += (_, _) => GetActiveChart()?.ZoomX(1.35);
            gridButton.Click += GridButton_Click;
            crossButton.Click += CrossButton_Click;
            indicatorPanelButton.Click += IndicatorPanelButton_Click;
            testModeButton.Click += TestModeButton_Click;
            testStepBackButton.Click += TestStepBackButton_Click;
            testStepForwardButton.Click += TestStepForwardButton_Click;
            hideChartButton.Click += HideChartButton_Click;
            printChartButton.Click += PrintChartButton_Click;
            snapshotChartButton.Click += SnapshotChartButton_Click;
            saveAnalysisButton.Click += SaveAnalysisButton_Click;

            // باز کردن خودکار همه تایم‌فریم‌های موجود برای نماد فعال.
            var multiTimeframeMenuItem = new ToolStripMenuItem("باز کردن همه تایم‌فریم‌ها");
            multiTimeframeMenuItem.Click += (_, _) =>
            {
                var symbol = activeChartSymbol;
                if (!string.IsNullOrWhiteSpace(symbol))
                    ShowMultiTimeframeCharts(symbol);
                else
                    MessageBox.Show(this, "ابتدا یک نماد را باز کنید.", "تایم‌فریم‌ها",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            mainMenuStrip.Items.Insert(Math.Min(1, mainMenuStrip.Items.Count), multiTimeframeMenuItem);
            analysisAutoSaveTimer.Interval = 1000;
            analysisAutoSaveTimer.Tick += AnalysisAutoSaveTimer_Tick;
            analysisAutoSaveTimer.Start();
            if (chartTypeComboBox.SelectedIndex < 0) chartTypeComboBox.SelectedIndex = 0;
            SetToggleButtonState(gridButton, false);
            SetToggleButtonState(crossButton, true);
            SetIndicatorPanelButtonState(GetActiveChart());
            SetToggleButtonState(hideChartButton, false);
            ApplyChartDisplayMode();
        }

        private void TestModeButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            testMode = !testMode;
            chart.SetTestMode(testMode);
            testModeButton.Text = "تست";
            SetToggleButtonState(testModeButton, testMode);
        }

        private void TestStepBackButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart != null && chart.TestMode)
                chart.StepTest(-1);
        }

        private void TestStepForwardButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart != null && chart.TestMode)
                chart.StepTest(1);
        }

        private void IndicatorPanelButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.ToggleVolumePanel();
            SetIndicatorPanelButtonState(chart);
        }

        private void SetIndicatorPanelButtonState(TradingChartControl? chart)
        {
            if (chart == null)
            {
                indicatorPanelButton.Text = "اندیکاتورها";
                SetToggleButtonState(indicatorPanelButton, false);
                return;
            }

            indicatorPanelButton.Text = "حجم";
            SetToggleButtonState(indicatorPanelButton, chart.VolumePanelVisible);
        }

        private void SaveAnalysisButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (multiTimeframeWorkspace != null || chart == null || string.IsNullOrWhiteSpace(chart.ChartSymbol))
            {
                MessageBox.Show(this, "ابتدا یک چارت فعال انتخاب کنید.", "ذخیره تحلیل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var document = chart.CreateAnalysisDocument();
                ChartAnalysisStorage.Save(document);

                MessageBox.Show(
                    this,
                    $"تحلیل نماد «{document.Symbol}» ذخیره شد.",
                    "ذخیره تحلیل",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"ذخیره تحلیل انجام نشد:\n{ex.Message}",
                    "ذخیره تحلیل",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AnalysisAutoSaveTimer_Tick(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (multiTimeframeWorkspace != null || chart == null || string.IsNullOrWhiteSpace(chart.ChartSymbol))
                return;

            try
            {
                ChartAnalysisStorage.Save(chart.CreateAnalysisDocument());
            }
            catch
            {
                // ذخیره خودکار نباید مزاحم کاربر شود.
            }
        }

        private void ApplyAnalysisDocument(TradingChartControl chart, ChartAnalysisDocument document)
        {
            chart.RestoreAnalysisDocument(document);
            SetToggleButtonState(gridButton, chart.GridVisible);
            SetToggleButtonState(crossButton, chart.CrosshairVisible);

            if (chart.ChartType == TradingChartType.Candlestick)
                chartTypeComboBox.SelectedIndex = 0;
            else if (chart.ChartType == TradingChartType.Line)
                chartTypeComboBox.SelectedIndex = 1;
            else
                chartTypeComboBox.SelectedIndex = 2;

            ResetDrawingToolButtons();
        }

        private void InitializeChartDrawingTools()
        {
            if (chartDrawingToolsInitialized)
                return;

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            chartDrawingToolsInitialized = true;

            drawTrendLineButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLine, drawTrendLineButton);
            drawTrendChannelButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendChannel, drawTrendChannelButton);
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalLine, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalLine, drawVerticalDoubleButton);
            drawHorizontalRayButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalRay, drawHorizontalRayButton);
            drawTrendLineArrowButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLineWithArrow, drawTrendLineArrowButton);
            drawRectangleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.Rectangle, drawRectangleButton);
            drawFibonacciButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Fibonacci, drawFibonacciButton);
            drawTextButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Text, drawTextButton);
            drawPitchforkButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.Pitchfork, drawPitchforkButton);
            drawFibonacciExtensionButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.FibonacciExtension, drawFibonacciExtensionButton);
            drawMeasureButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.Measure, drawMeasureButton);
            hideToolsButton.Click += (_, _) =>
            {
                var result = MessageBox.Show(
                    this,
                    "آیا از حذف همه ابزارهای رسم روی این چارت مطمئن هستید؟",
                    "حذف ابزارها",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                GetActiveChart()?.ClearAllDrawings();
                ResetDrawingToolButtons();
            };

            chartTabControl.SelectedIndexChanged += ChartDrawingTabChanged;
            drawingStateTimer.Interval = 100;
            drawingStateTimer.Tick += DrawingStateTimer_Tick;
            drawingStateTimer.Start();
            ResetDrawingToolButtons();
        }

        private enum AdvancedDrawingSelection
        {
            Fibonacci,
            Text
        }

        private enum ExtraDrawingSelection
        {
            Pitchfork,
            FibonacciExtension,
            Measure
        }

        private void ActivateDrawingTool(ChartDrawingTool tool, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelAdvancedDrawing();
            chart.CancelExtraDrawing();
            chart.EnableExtraDrawingMouseSafety();

            if (chart.ActiveDrawingTool == tool)
            {
                chart.CancelDrawing();
                ResetDrawingToolButtons();
                return;
            }

            chart.CancelDrawing();
            chart.SetDrawingTool(tool);
            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, true);
        }

        private void ActivateAdvancedDrawingTool(AdvancedDrawingSelection selection, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelDrawing();
            chart.CancelExtraDrawing();
            chart.EnableExtraDrawingMouseSafety();

            if (selection == AdvancedDrawingSelection.Fibonacci)
                chart.ActivateFibonacciRetracement();
            else
                chart.ActivateTextLabel();

            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, chart.AdvancedDrawingActive);
        }

        private void ActivateExtraDrawingTool(ExtraDrawingSelection selection, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelDrawing();
            chart.CancelAdvancedDrawing();

            if (selection == ExtraDrawingSelection.Pitchfork)
                chart.ActivatePitchfork();
            else if (selection == ExtraDrawingSelection.FibonacciExtension)
                chart.ActivateFibonacciExtension();
            else
                chart.ActivateMeasureTool();

            chart.EnableExtraDrawingMouseSafety();

            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, chart.ExtraDrawingActive);
        }

        private void DrawingStateTimer_Tick(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null ||
                (chart.ActiveDrawingTool == ChartDrawingTool.None &&
                 !chart.AdvancedDrawingActive &&
                 !chart.ExtraDrawingActive))
                ResetDrawingToolButtons();
        }

        private void ChartDrawingTabChanged(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            chart?.CancelDrawing();
            chart?.CancelAdvancedDrawing();
            chart?.CancelExtraDrawing();
            chart?.EnableExtraDrawingMouseSafety();

            // همگام‌سازی وضعیت دکمه تست با چارت فعال.
            testMode = chart?.TestMode == true;
            testModeButton.Text = "تست";
            SetToggleButtonState(testModeButton, testMode);

            ResetDrawingToolButtons();
        }

        private void ResetDrawingToolButtons()
        {
            SetToggleButtonState(drawTrendLineButton, false);
            SetToggleButtonState(drawTrendChannelButton, false);
            SetToggleButtonState(drawHorizontalDoubleButton, false);
            SetToggleButtonState(drawVerticalDoubleButton, false);
            SetToggleButtonState(drawHorizontalRayButton, false);
            SetToggleButtonState(drawTrendLineArrowButton, false);
            SetToggleButtonState(drawRectangleButton, false);
            SetToggleButtonState(drawFibonacciButton, false);
            SetToggleButtonState(drawTextButton, false);
            SetToggleButtonState(drawPitchforkButton, false);
            SetToggleButtonState(drawFibonacciExtensionButton, false);
            SetToggleButtonState(drawMeasureButton, false);
        }

        private void ApplyChartDisplayMode()
        {
            if (!chartRuntimeInitialized) return;
            if (chartDisplayMode == ChartDisplayMode.SingleTab)
            {
                while (chartTabControl.TabPages.Count > 1) chartTabControl.TabPages.RemoveAt(chartTabControl.TabPages.Count - 1);
                if (chartTabControl.TabPages.Count == 0) chartTabControl.TabPages.Add(chartTabPage);
                chartTabPage.Text = string.IsNullOrWhiteSpace(activeChartSymbol) ? "چارت" : activeChartSymbol;
                chartTabControl.SelectedTab = chartTabPage;
            }
            else if (!string.IsNullOrWhiteSpace(activeChartSymbol) && chartControls.TryGetValue(activeChartSymbol, out var chart) && chart.Parent is null)
            {
                AttachChartToTab(chart, activeChartSymbol);
            }
        }

        private void StocksDataGridView_CellClickForChart(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != symbolColumn.Index) return;
            var symbol = Convert.ToString(stocksDataGridView.Rows[e.RowIndex].Cells[symbolColumn.Index].Value)?.Trim();
            if (!string.IsNullOrWhiteSpace(symbol)) ShowSymbolChart(symbol);
        }

        private void ChartTypeComboBox_SelectedIndexChanged(object? sender, EventArgs e) => GetActiveChart()?.SetChartType(GetSelectedChartType());

        private TradingChartType GetSelectedChartType()
        {
            var text = chartTypeComboBox.SelectedItem?.ToString() ?? string.Empty;
            if (text.Contains("خط", StringComparison.OrdinalIgnoreCase)) return TradingChartType.Line;
            if (text.Contains("میله", StringComparison.OrdinalIgnoreCase) || text.Contains("OHLC", StringComparison.OrdinalIgnoreCase)) return TradingChartType.Bar;
            return TradingChartType.Candlestick;
        }

        private void ShowSymbolChart(string symbol)
        {
            CloseMultiTimeframeWorkspace();
            var currentChart = GetActiveChart();
            if (currentChart != null && !string.Equals(currentChart.ChartSymbol, symbol, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    ChartAnalysisStorage.Save(currentChart.CreateAnalysisDocument());
                }
                catch
                {
                    // ذخیره خودکار نباید مانع باز شدن چارت جدید شود.
                }
            }

            if (string.IsNullOrWhiteSpace(displayedPortfolioName) || !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition)) return;
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
                var isNewChart = !chartControls.ContainsKey(symbol);
                var chart = GetOrCreateChart(symbol);
                chart.SetData(points, symbol);
                chart.SetChartType(GetSelectedChartType());

                if (isNewChart)
                {
                    var savedAnalysis = ChartAnalysisStorage.Load(symbol);
                    if (savedAnalysis != null)
                        ApplyAnalysisDocument(chart, savedAnalysis);
                }

                // حالت تست متعلق به همان چارت فعال است. با تعویض نماد، چارت جدید
                // نباید وضعیت تست چارت قبلی را به دکمه منتقل کند.
                testMode = chart.TestMode;
                testModeButton.Text = "تست";
                SetToggleButtonState(testModeButton, testMode);

                chart.Visible = true;
                SetToggleButtonState(hideChartButton, false);
                SetToggleButtonState(crossButton, chart.CrosshairVisible);
                SetIndicatorPanelButtonState(chart);
                AttachChartToTab(chart, symbol);

                // بعد از نمایش چارت، فوکوس را روی فهرست سهام نگه می‌داریم
                // تا کلیدهای ناوبری مستقیماً روی فهرست عمل کنند.
                var selectedRow = stocksDataGridView.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(row => string.Equals(
                        Convert.ToString(row.Cells[symbolColumn.Index].Value)?.Trim(),
                        symbol,
                        StringComparison.OrdinalIgnoreCase));

                if (selectedRow != null)
                {
                    stocksDataGridView.CurrentCell = selectedRow.Cells[symbolColumn.Index];
                    stocksDataGridView.ClearSelection();
                    selectedRow.Selected = true;
                    stocksDataGridView.Focus();
                }
            }
            catch (Exception ex)
            {
                chartInfoLabel.Text = $"خطا در رسم چارت «{symbol}»: {ex.Message}";
                chartPlaceholderLabel.Visible = true;
            }
        }

        private TradingChartControl GetOrCreateChart(string symbol)
        {
            if (chartControls.TryGetValue(symbol, out var existing)) return existing;
            var chart = new TradingChartControl { Dock = DockStyle.Fill };
            chart.VolumeSettingsChanged += Chart_VolumeSettingsChanged;
            chart.SetVolumePanelVisible(lastVolumePanelVisible);
            chart.VolumePanelRatio = lastVolumePanelRatio;
            chartControls[symbol] = chart;
            return chart;
        }

        private void AttachChartToTab(TradingChartControl chart, string symbol)
        {
            if (chartDisplayMode == ChartDisplayMode.SingleTab)
            {
                chartTabPage.Controls.Clear();
                chartTabPage.Controls.Add(chart);
                chartTabPage.Text = symbol;
                chartTabControl.SelectedTab = chartTabPage;
                return;
            }
            if (chart.Parent is TabPage currentPage) { chartTabControl.SelectedTab = currentPage; return; }
            if (chartTabControl.TabPages.Count == 1 && chartTabPage.Controls.OfType<TradingChartControl>().Count() == 0)
            {
                chartTabPage.Controls.Clear();
                chartTabPage.Controls.Add(chart);
                chartTabPage.Text = symbol;
                chartTabControl.SelectedTab = chartTabPage;
                return;
            }
            var tab = new TabPage(symbol) { RightToLeft = RightToLeft.Yes };
            tab.Controls.Add(chart);
            chartTabControl.TabPages.Add(tab);
            chartTabControl.SelectedTab = tab;
        }

        private TradingChartControl? GetActiveChart()
        {
            if (multiTimeframeWorkspace != null)
                return multiTimeframeWorkspace.ActiveChart;

            if (chartDisplayMode == ChartDisplayMode.SingleTab)
                return chartTabPage.Controls.OfType<TradingChartControl>().FirstOrDefault();

            return chartTabControl.SelectedTab?.Controls.OfType<TradingChartControl>().FirstOrDefault();
        }

        private void ShowMultiTimeframeCharts(string symbol)
        {
            if (string.IsNullOrWhiteSpace(displayedPortfolioName) ||
                !loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
                return;

            try
            {
                var files = GetSymbolTimeframeFiles(definition, symbol);
                if (files.Count == 0)
                {
                    MessageBox.Show(
                        this,
                        $"برای «{symbol}» فایل تایم‌فریم استانداردی پیدا نشد.",
                        "تایم‌فریم‌ها",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                CloseMultiTimeframeWorkspace();

                multiTimeframeWorkspace = new MultiTimeframeWorkspace
                {
                    Dock = DockStyle.Fill,
                    RightToLeft = RightToLeft.No
                };

                foreach (var item in files)
                {
                    var points = LoadChartData(definition, symbol, new[] { item.FilePath });
                    if (points.Count == 0)
                        continue;

                    var chart = GetOrCreateChart(symbol + "|" + item.TimeFrame);
                    chart.SetData(points, symbol);
                    chart.SetChartType(GetSelectedChartType());
                    chart.SetVolumePanelVisible(lastVolumePanelVisible);
                    chart.VolumePanelRatio = lastVolumePanelRatio;

                    // تحلیل‌های ذخیره‌شده فقط برای چارت عادی نماد بازیابی می‌شوند؛
                    // در Workspace چندتایم‌فریمی هر پنجره نمایشی مستقل است.
                    multiTimeframeWorkspace.AddChart(chart, item.TimeFrame);
                }

                if (multiTimeframeWorkspace.ChartCount == 0)
                {
                    multiTimeframeWorkspace.Dispose();
                    multiTimeframeWorkspace = null;
                    MessageBox.Show(this, $"هیچ داده قابل رسم برای «{symbol}» پیدا نشد.",
                        "تایم‌فریم‌ها", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                activeChartSymbol = symbol;
                chartInfoLabel.Text = $"{symbol}   |   {multiTimeframeWorkspace.ChartCount:N0} تایم‌فریم";
                chartPlaceholderLabel.Visible = false;

                chartTabPage.Controls.Clear();
                chartTabPage.Controls.Add(multiTimeframeWorkspace);
                chartTabPage.Text = symbol + " | چندتایم‌فریم";
                chartTabControl.SelectedTab = chartTabPage;
                chartTabControl.Visible = true;

                multiTimeframeWorkspace.ActiveChartChanged += (_, _) => SyncChartToolbarFromActiveChart();
                multiTimeframeWorkspace.SelectFirstChart();

                SetToggleButtonState(hideChartButton, false);
                SetIndicatorPanelButtonState(GetActiveChart());
            }
            catch (Exception ex)
            {
                CloseMultiTimeframeWorkspace();
                MessageBox.Show(this,
                    $"باز کردن تایم‌فریم‌ها انجام نشد:\n{ex.Message}",
                    "تایم‌فریم‌ها", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseMultiTimeframeWorkspace()
        {
            if (multiTimeframeWorkspace == null)
                return;

            var workspace = multiTimeframeWorkspace;
            multiTimeframeWorkspace = null;

            foreach (var chart in workspace.Charts.ToList())
            {
                foreach (var item in chartControls.Where(x => ReferenceEquals(x.Value, chart)).ToList())
                    chartControls.Remove(item.Key);

                chart.Dispose();
            }

            workspace.Dispose();

            chartTabPage.Controls.Clear();
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Text = string.IsNullOrWhiteSpace(activeChartSymbol) ? "چارت" : activeChartSymbol;
        }

        private List<(string FilePath, string TimeFrame)> GetSymbolTimeframeFiles(
      PortfolioDefinition definition,
      string symbol)
        {
            var result = new List<(string FilePath, string TimeFrame)>();

            if (definition == null ||
                string.IsNullOrWhiteSpace(definition.DataPath) ||
                !Directory.Exists(definition.DataPath) ||
                string.IsNullOrWhiteSpace(symbol))
                return result;

            var extension = definition.FileType?.Trim().ToUpperInvariant() switch
            {
                "CSV" => ".csv",
                "PRN" => ".prn",
                _ => ".txt"
            };

            if (definition.SymbolSource != SymbolSource.FileName)
                return result;

            // نماد پایه را از نماد فعال استخراج می‌کنیم.
            // مثال:
            // EURUSD@H1  -> EURUSD
            // EURUSD@M15 -> EURUSD
            var baseSymbol = symbol;
            var atIndex = baseSymbol.IndexOf('@');
            if (atIndex > 0)
                baseSymbol = baseSymbol[..atIndex];

            foreach (var basketSymbol in definition.Symbols ?? new List<string>())
            {
                if (string.IsNullOrWhiteSpace(basketSymbol))
                    continue;

                var memberName = basketSymbol.Trim();

                if (Path.HasExtension(memberName))
                    memberName = Path.GetFileNameWithoutExtension(memberName);

                // فقط اعضای همین سبد که متعلق به نماد پایه فعلی هستند.
                if (!memberName.StartsWith(baseSymbol, StringComparison.OrdinalIgnoreCase))
                    continue;

                var remainder = memberName.Substring(baseSymbol.Length);

                if (string.IsNullOrWhiteSpace(remainder))
                    continue;

                remainder = remainder.TrimStart('@', '_', '-', '.', ' ', '\\');

                var timeframe = remainder.ToUpperInvariant() switch
                {
                    "M1" => "M1",
                    "M5" => "M5",
                    "M15" => "M15",
                    "M30" => "M30",
                    "H1" => "1H",
                    "H4" => "4H",
                    "1H" => "1H",
                    "4H" => "4H",
                    "D" => "D",
                    "M" => "M",
                    "Y" => "Y",
                    _ => null
                };

                if (timeframe == null)
                    continue;

                var filePath = Path.Combine(
                    definition.DataPath,
                    memberName + extension);

                if (!File.Exists(filePath))
                    continue;

                // تشخیص از روی داده داخل فایل نیز انجام می‌شود.
                var detected = DetectTimeframeFromFile(definition, filePath);
                var finalTimeframe = detected ?? timeframe;

                result.Add((filePath, finalTimeframe));
            }

            return result
                .GroupBy(x => x.TimeFrame, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .OrderBy(x => TimeframeOrder(x.TimeFrame))
                .ToList();
        }
        private static string? GetTimeframeSuffix(string fileNameWithoutExtension, string symbol)
        {
            if (!fileNameWithoutExtension.StartsWith(symbol, StringComparison.OrdinalIgnoreCase))
                return null;

            var remainder = fileNameWithoutExtension.Substring(symbol.Length);
            if (remainder.Length == 0)
                return null;

            remainder = remainder.TrimStart('_', '-', '.', ' ', '\\');
            var candidates = new[] { "M15", "M30", "1H", "4H", "M1", "M5", "D", "M", "Y" };

            foreach (var candidate in candidates)
            {
                if (string.Equals(remainder, candidate, StringComparison.OrdinalIgnoreCase))
                    return candidate;
            }

            return null;
        }
        private string? DetectTimeframeFromFile(PortfolioDefinition definition, string filePath)
        {
            if (definition.NoDateTime)
                return null;

            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0) dateColumn = GetMappingColumn(definition, "تاریخ لاتین");
            var timeColumn = GetMappingColumn(definition, "زمان");
            if (timeColumn <= 0) timeColumn = GetMappingColumn(definition, "ساعت لاتین");

            if (dateColumn <= 0)
                return null;

            var dates = new List<DateTime>();
            try
            {
                foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var row = SplitTradingDataLine(line, definition.Separator);
                    if (definition.HasHeader && dates.Count == 0)
                    {
                        // هدر فقط زمانی رد می‌شود که ستون تاریخ در آن قابل تبدیل نباشد.
                        if (dateColumn > row.Length ||
                            !TryParseChartDate(row[dateColumn - 1], definition, out _))
                            continue;
                    }

                    if (dateColumn > row.Length ||
                        !TryParseChartDate(row[dateColumn - 1], definition, out var date))
                        continue;

                    if (timeColumn > 0 && timeColumn <= row.Length &&
                        TryParseChartTime(row[timeColumn - 1], out var time))
                        date = date.Date.Add(time);

                    dates.Add(date);
                    if (dates.Count >= 300)
                        break;
                }
            }
            catch
            {
                return null;
            }

            if (dates.Count < 3)
                return null;

            dates = dates.OrderBy(x => x).Distinct().ToList();
            if (dates.Count < 3)
                return null;

            var minuteSteps = new List<double>();
            for (var i = 1; i < dates.Count; i++)
            {
                var minutes = (dates[i] - dates[i - 1]).TotalMinutes;
                if (minutes > 0 && minutes < 10000000)
                    minuteSteps.Add(minutes);
            }

            if (minuteSteps.Count == 0)
                return null;

            var median = minuteSteps.OrderBy(x => x).ElementAt(minuteSteps.Count / 2);

            if (median <= 1.5) return "M1";
            if (median <= 6) return "M5";
            if (median <= 16) return "M15";
            if (median <= 31) return "M30";
            if (median <= 90) return "1H";
            if (median <= 300) return "4H";

            var first = dates[0];
            var second = dates[1];

            if (first.AddYears(1).Year == second.Year &&
                first.Month == second.Month &&
                first.Day == second.Day)
                return "Y";

            if (first.AddMonths(1).Year == second.Year &&
                first.AddMonths(1).Month == second.Month)
                return "M";

            if (first.Date.AddDays(1) == second.Date)
                return "D";

            // برای فایل‌هایی که به دلیل تعطیلی بازار فاصله‌های چندروزه دارند،
            // وجود یک تغییر روزانه/تقویمی همچنان می‌تواند Daily باشد.
            if (first.TimeOfDay == second.TimeOfDay &&
                second.Date > first.Date &&
                (second.Date - first.Date).TotalDays <= 7)
                return "D";

            return null;
        }

        private static int TimeframeOrder(string value) => value.ToUpperInvariant() switch
        {
            "M1" => 1,
            "M5" => 2,
            "M15" => 3,
            "M30" => 4,
            "1H" => 5,
            "4H" => 6,
            "D" => 7,
            "M" => 8,
            "Y" => 9,
            _ => 99
        };

        private void Chart_VolumeSettingsChanged(object? sender, EventArgs e)
        {
            if (sender is not TradingChartControl chart)
                return;

            lastVolumePanelVisible = chart.VolumePanelVisible;
            lastVolumePanelRatio = chart.VolumePanelRatio;
        }

        private void GridButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null) return;
            chart.ToggleGrid();
            SetToggleButtonState(gridButton, chart.GridVisible);
        }

        private void CrossButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null) return;
            chart.ToggleCrosshair();
            SetToggleButtonState(crossButton, chart.CrosshairVisible);
        }

        private void HideChartButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null) return;
            chart.Visible = !chart.Visible;
            SetToggleButtonState(hideChartButton, !chart.Visible);
        }

        private static void SetToggleButtonState(Button button, bool active)
        {
            if (active)
            {
                // فقط در حالت فعال، دکمه به حالت آبی تغییر می‌کند.
                button.UseVisualStyleBackColor = false;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 1;
                button.BackColor = SystemColors.Highlight;
                button.ForeColor = SystemColors.HighlightText;
                button.FlatAppearance.BorderColor = SystemColors.Highlight;
                button.FlatAppearance.MouseOverBackColor = SystemColors.Highlight;
                button.FlatAppearance.MouseDownBackColor = SystemColors.Highlight;
            }
            else
            {
                // در حالت عادی هیچ رنگی به‌صورت دستی تحمیل نمی‌شود.
                // با فعال بودن VisualStyle، ظاهر دقیقاً از Theme/Designer ویندوز گرفته می‌شود.
                button.UseVisualStyleBackColor = true;
                button.FlatStyle = FlatStyle.Standard;
                button.ForeColor = SystemColors.ControlText;
                button.FlatAppearance.BorderSize = 1;
            }

            button.Invalidate();
        }

        private void PrintChartButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Width <= 0 || chart.Height <= 0) return;
            using var bitmap = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            using var document = new PrintDocument { DocumentName = string.IsNullOrWhiteSpace(activeChartSymbol) ? "Trade.It Chart" : activeChartSymbol };
            document.PrintPage += (_, args) =>
            {
                var bounds = args.MarginBounds;
                var scale = Math.Min(bounds.Width / (float)bitmap.Width, bounds.Height / (float)bitmap.Height);
                var width = (int)(bitmap.Width * scale);
                var height = (int)(bitmap.Height * scale);
                args.Graphics.DrawImage(bitmap, new Rectangle(bounds.Left + (bounds.Width - width) / 2, bounds.Top + (bounds.Height - height) / 2, width, height));
            };
            using var dialog = new PrintDialog { Document = document, UseEXDialog = true };
            if (dialog.ShowDialog(this) == DialogResult.OK) document.Print();
        }

        private void SnapshotChartButton_Click(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null || chart.Width <= 0 || chart.Height <= 0) return;
            using var dialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp",
                DefaultExt = "png",
                AddExtension = true,
                FileName = string.IsNullOrWhiteSpace(activeChartSymbol) ? "chart.png" : activeChartSymbol + ".png"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            using var bitmap = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            var extension = Path.GetExtension(dialog.FileName).ToLowerInvariant();
            var format = extension switch { ".jpg" or ".jpeg" => System.Drawing.Imaging.ImageFormat.Jpeg, ".bmp" => System.Drawing.Imaging.ImageFormat.Bmp, _ => System.Drawing.Imaging.ImageFormat.Png };
            bitmap.Save(dialog.FileName, format);
        }

        private void CloseAllChartTabs()
        {
            CloseMultiTimeframeWorkspace();
            foreach (var chart in chartControls.Values.ToList()) chart.Parent = null;
            chartControls.Clear();
            activeChartSymbol = null;
            chartTabPage.Controls.Clear();
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Text = "چارت";
            chartTabControl.SelectedTab = chartTabPage;
            while (chartTabControl.TabPages.Count > 1) chartTabControl.TabPages.RemoveAt(chartTabControl.TabPages.Count - 1);
            chartInfoLabel.Text = "هنوز سهمی برای نمایش انتخاب نشده است.";
            chartPlaceholderLabel.Visible = true;
            SetToggleButtonState(gridButton, false);
            SetToggleButtonState(crossButton, false);
            SetToggleButtonState(hideChartButton, false);
        }

        private List<TradingChartPoint> LoadChartData(PortfolioDefinition definition, string symbol, IEnumerable<string>? specificFiles = null)
        {
            var result = new List<TradingChartPoint>();

            if (definition == null || string.IsNullOrWhiteSpace(symbol) ||
                string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath))
                return result;

            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0) dateColumn = GetMappingColumn(definition, "تاریخ لاتین");

            var timeColumn = GetMappingColumn(definition, "زمان");
            if (timeColumn <= 0) timeColumn = GetMappingColumn(definition, "ساعت لاتین");

            var openColumn = GetMappingColumn(definition, "باز");
            var highColumn = GetMappingColumn(definition, "بیشترین");
            var lowColumn = GetMappingColumn(definition, "کمترین");
            var closeColumn = GetMappingColumn(definition, "پایانی");
            if (closeColumn <= 0) closeColumn = GetMappingColumn(definition, "قیمت پایانی بورس");
            var volumeColumn = GetMappingColumn(definition, "حجم");
            if (volumeColumn <= 0) volumeColumn = GetMappingColumn(definition, "حجم معاملات");

            if (openColumn <= 0 || highColumn <= 0 || lowColumn <= 0 || closeColumn <= 0)
                return result;

            var symbolColumn = GetMappingColumn(definition, "نماد");
            var syntheticIndex = 0L;

            try
            {
                foreach (var filePath in specificFiles ?? GetSymbolFiles(definition, symbol))
                {
                    var firstLine = true;
                    foreach (var line in File.ReadLines(filePath, DetectTradingDataEncoding(filePath)))
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var row = SplitTradingDataLine(line, definition.Separator);
                        if (firstLine && definition.HasHeader)
                        {
                            firstLine = false;
                            continue;
                        }
                        firstLine = false;

                        if (definition.SymbolSource == SymbolSource.InsideFile &&
                            (symbolColumn <= 0 || symbolColumn > row.Length ||
                             !string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase)))
                            continue;

                        var requiredMaxColumn = Math.Max(Math.Max(openColumn, highColumn), Math.Max(lowColumn, closeColumn));
                        if (requiredMaxColumn > row.Length)
                            continue;

                        DateTime date;
                        if (!definition.NoDateTime)
                        {
                            if (dateColumn <= 0 || dateColumn > row.Length ||
                                !TryParseChartDate(row[dateColumn - 1], definition, out date))
                                continue;

                            if (timeColumn > 0 && timeColumn <= row.Length && TryParseChartTime(row[timeColumn - 1], out var time))
                                date = date.Date.Add(time);
                        }
                        else
                        {
                            date = DateTime.UnixEpoch.AddDays(syntheticIndex++);
                        }

                        if (!TryParseTradingNumber(row[openColumn - 1], out var open) ||
                            !TryParseTradingNumber(row[highColumn - 1], out var high) ||
                            !TryParseTradingNumber(row[lowColumn - 1], out var low) ||
                            !TryParseTradingNumber(row[closeColumn - 1], out var close))
                            continue;

                        var volume = 0d;
                        if (volumeColumn > 0 && volumeColumn <= row.Length)
                            TryParseTradingNumber(row[volumeColumn - 1], out volume);

                        if (double.IsNaN(open) || double.IsInfinity(open) ||
                            double.IsNaN(high) || double.IsInfinity(high) ||
                            double.IsNaN(low) || double.IsInfinity(low) ||
                            double.IsNaN(close) || double.IsInfinity(close))
                            continue;

                        result.Add(new TradingChartPoint
                        {
                            Date = date,
                            Open = open,
                            High = high,
                            Low = low,
                            Close = close,
                            Volume = double.IsNaN(volume) || double.IsInfinity(volume) ? 0d : volume
                        });
                    }
                }
            }
            catch
            {
                return new List<TradingChartPoint>();
            }

            return result
                .OrderBy(x => x.Date)
                .ToList();
        }

        private static bool TryParseChartDate(string value, PortfolioDefinition definition, out DateTime date)
        {
            date = default;
            var normalized = NormalizeTradingDigits(value).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            var parts = normalized.Split(new[] { '/', '-', '.', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int year;
            int month;
            int day;

            if (parts.Length >= 3 &&
                int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) &&
                int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out month) &&
                int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out day))
            {
            }
            else
            {
                var digits = new string(normalized.Where(char.IsDigit).ToArray());
                if (digits.Length < 8)
                    return false;

                if (!int.TryParse(digits[..4], NumberStyles.Integer, CultureInfo.InvariantCulture, out year) ||
                    !int.TryParse(digits.Substring(4, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out month) ||
                    !int.TryParse(digits.Substring(6, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out day))
                    return false;
            }

            try
            {
                date = definition.Calendar == InputCalendar.Gregorian
                    ? new DateTime(year, month, day)
                    : new PersianCalendar().ToDateTime(year, month, day, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryParseChartTime(string value, out TimeSpan time)
        {
            time = TimeSpan.Zero;
            var normalized = NormalizeTradingDigits(value).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            if (TimeSpan.TryParse(normalized, CultureInfo.InvariantCulture, out time))
                return time >= TimeSpan.Zero && time < TimeSpan.FromDays(1);

            var digits = new string(normalized.Where(char.IsDigit).ToArray());
            if (digits.Length < 4)
                return false;

            if (!int.TryParse(digits[..2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var hour) ||
                !int.TryParse(digits.Substring(2, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out var minute))
                return false;

            var second = 0;
            if (digits.Length >= 6 && !int.TryParse(digits.Substring(4, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out second))
                return false;

            if (hour is < 0 or > 23 || minute is < 0 or > 59 || second is < 0 or > 59)
                return false;

            time = new TimeSpan(hour, minute, second);
            return true;
        }
    }
}
