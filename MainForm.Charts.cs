using System.Drawing.Printing;
using System.Globalization;
using System.Text;

namespace Trade.It
{
    public partial class MainForm
    {
        private readonly Dictionary<string, TradingChartControl> chartControls = new(StringComparer.OrdinalIgnoreCase);
        private bool chartRuntimeInitialized;
        private string? activeChartSymbol;

        private bool chartDrawingToolsInitialized;
        private readonly System.Windows.Forms.Timer drawingStateTimer = new();

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
            hideChartButton.Click += HideChartButton_Click;
            printChartButton.Click += PrintChartButton_Click;
            snapshotChartButton.Click += SnapshotChartButton_Click;
            closeAllChartsMenuItem.Click += (_, _) => CloseAllChartTabs();
            if (chartTypeComboBox.SelectedIndex < 0) chartTypeComboBox.SelectedIndex = 0;
            SetToggleButtonState(gridButton, false);
            SetToggleButtonState(crossButton, true);
            SetToggleButtonState(hideChartButton, false);
            ApplyChartDisplayMode();
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
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalDoubleArrow, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalDoubleArrow, drawVerticalDoubleButton);
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
            closeAllChartsMenuItem.Click += (_, _) => ResetDrawingToolButtons();
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
                var chart = GetOrCreateChart(symbol);
                chart.SetData(points);
                chart.SetChartType(GetSelectedChartType());
                chart.Visible = true;
                SetToggleButtonState(hideChartButton, false);
                SetToggleButtonState(crossButton, chart.CrosshairVisible);
                AttachChartToTab(chart, symbol);
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
            if (chartDisplayMode == ChartDisplayMode.SingleTab) return chartTabPage.Controls.OfType<TradingChartControl>().FirstOrDefault();
            return chartTabControl.SelectedTab?.Controls.OfType<TradingChartControl>().FirstOrDefault();
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
            button.UseVisualStyleBackColor = false;
            button.BackColor = active ? SystemColors.Highlight : SystemColors.Control;
            button.ForeColor = active ? SystemColors.HighlightText : SystemColors.ControlText;
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

        private List<TradingChartPoint> LoadChartData(PortfolioDefinition definition, string symbol)
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

            if (dateColumn <= 0 || highColumn <= 0 || lowColumn <= 0 || closeColumn <= 0)
                return result;

            var files = Directory.GetFiles(definition.DataPath, "*", SearchOption.TopDirectoryOnly)
                .Where(f => Path.GetFileNameWithoutExtension(f).Contains(symbol, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (files.Count == 0)
                return result;

            foreach (var file in files)
            {
                foreach (var row in ReadDataRows(file, definition.Separator))
                {
                    if (row.Length < Math.Max(Math.Max(Math.Max(dateColumn, highColumn), lowColumn), closeColumn))
                        continue;

                    if (!TryParseChartDate(row, dateColumn, timeColumn, out var date))
                        continue;

                    if (!TryParseDouble(row[highColumn - 1], out var high) ||
                        !TryParseDouble(row[lowColumn - 1], out var low) ||
                        !TryParseDouble(row[closeColumn - 1], out var close))
                        continue;

                    var open = close;
                    if (openColumn > 0 && openColumn <= row.Length)
                        TryParseDouble(row[openColumn - 1], out open);

                    var volume = 0d;
                    if (volumeColumn > 0 && volumeColumn <= row.Length)
                        TryParseDouble(row[volumeColumn - 1], out volume);

                    result.Add(new TradingChartPoint(date, open, high, low, close, volume));
                }
            }

            return result.OrderBy(x => x.DateTime).ToList();
        }

        private static IEnumerable<string[]> ReadDataRows(string file, char separator)
        {
            using var reader = new StreamReader(file, Encoding.UTF8, true);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                yield return line.Split(separator);
            }
        }

        private static bool TryParseChartDate(string[] row, int dateColumn, int timeColumn, out DateTime value)
        {
            value = default;
            var dateText = row[dateColumn - 1].Trim();
            var timeText = timeColumn > 0 && timeColumn <= row.Length ? row[timeColumn - 1].Trim() : string.Empty;

            var formats = new[]
            {
                "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss",
                "HH:mm:ss"
            };

            if (DateTime.TryParseExact(dateText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
            {
                if (!string.IsNullOrWhiteSpace(timeText) && TimeSpan.TryParse(timeText, CultureInfo.InvariantCulture, out var time))
                    value = value.Date.Add(time);
                return true;
            }

            return DateTime.TryParse($"{dateText} {timeText}".Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
        }

        private static bool TryParseDouble(string text, out double value)
        {
            text = text.Replace(",", string.Empty).Trim();
            return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value) ||
                   double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
        }

        private static int GetMappingColumn(PortfolioDefinition definition, string field)
        {
            var mapping = definition.Mappings?.FirstOrDefault(x => string.Equals(x.Field?.Trim(), field, StringComparison.OrdinalIgnoreCase));
            return mapping?.Column ?? 0;
        }

        private static bool HasDateColumn(PortfolioDefinition definition) => GetMappingColumn(definition, "تاریخ") > 0 || GetMappingColumn(definition, "تاریخ لاتین") > 0;

        private static string GetLatestTradeDateText(PortfolioDefinition definition, string symbol)
        {
            try
            {
                var points = LoadLatestDatePoints(definition, symbol);
                if (points == null) return string.Empty;
                return points.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static DateTime? LoadLatestDatePoints(PortfolioDefinition definition, string symbol)
        {
            var dateColumn = GetMappingColumn(definition, "تاریخ");
            if (dateColumn <= 0) dateColumn = GetMappingColumn(definition, "تاریخ لاتین");
            if (dateColumn <= 0 || string.IsNullOrWhiteSpace(definition.DataPath) || !Directory.Exists(definition.DataPath))
                return null;

            DateTime? latest = null;
            foreach (var file in Directory.GetFiles(definition.DataPath, "*", SearchOption.TopDirectoryOnly)
                         .Where(f => Path.GetFileNameWithoutExtension(f).Contains(symbol, StringComparison.OrdinalIgnoreCase)))
            {
                foreach (var row in ReadDataRows(file, definition.Separator))
                {
                    if (row.Length < dateColumn)
                        continue;
                    if (DateTime.TryParse(row[dateColumn - 1], CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                        (!latest.HasValue || date > latest.Value))
                        latest = date;
                }
            }

            return latest;
        }
    }
}
