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
            SetToggleButtonState(crossButton, false);
            SetToggleButtonState(hideChartButton, false);
            ApplyChartDisplayMode();
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
            var file = FindSymbolFile(definition, symbol);
            if (file == null) return new List<TradingChartPoint>();
            var separator = string.IsNullOrEmpty(definition.Separator) ? ',' : definition.Separator[0];
            var lines = File.ReadLines(file).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (lines.Count == 0) return new List<TradingChartPoint>();
            var first = SplitLine(lines[0], separator);
            var hasHeader = definition.HasHeader;
            var header = hasHeader ? first : Array.Empty<string>();
            var start = hasHeader ? 1 : 0;
            var dateColumn = FindMappedColumn(definition, header, "date", "تاریخ", "روز");
            var timeColumn = FindMappedColumn(definition, header, "time", "زمان", "ساعت");
            var openColumn = FindMappedColumn(definition, header, "open", "باز", "اولین", "اول");
            var highColumn = FindMappedColumn(definition, header, "high", "بیشترین", "بیشترين", "بیشینه");
            var lowColumn = FindMappedColumn(definition, header, "low", "کمترین", "کمترين", "کمینه");
            var closeColumn = FindMappedColumn(definition, header, "close", "پایانی", "پاياني", "بسته", "closeprice");
            if (openColumn < 0 || highColumn < 0 || lowColumn < 0 || closeColumn < 0) return new List<TradingChartPoint>();
            var result = new List<TradingChartPoint>();
            for (var i = start; i < lines.Count; i++)
            {
                var fields = SplitLine(lines[i], separator);
                if (!TryGetDouble(fields, openColumn, out var open) || !TryGetDouble(fields, highColumn, out var high) || !TryGetDouble(fields, lowColumn, out var low) || !TryGetDouble(fields, closeColumn, out var close)) continue;
                if (!TryGetDate(fields, dateColumn, timeColumn, definition.Calendar, out var date)) continue;
                result.Add(new TradingChartPoint { Date = date, Open = open, High = high, Low = low, Close = close });
            }
            return result.OrderBy(x => x.Date).ToList();
        }

        private static string? FindSymbolFile(PortfolioDefinition definition, string symbol)
        {
            var path = definition.DataPath?.Trim();
            if (string.IsNullOrWhiteSpace(path)) return null;
            if (File.Exists(path)) return path;
            if (!Directory.Exists(path)) return null;
            var extensions = definition.FileType.Equals("CSV", StringComparison.OrdinalIgnoreCase) ? new[] { ".csv", ".txt" } : new[] { ".txt", ".csv" };
            foreach (var extension in extensions)
            {
                var exact = Path.Combine(path, symbol + extension);
                if (File.Exists(exact)) return exact;
            }
            return Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly).FirstOrDefault(file => string.Equals(Path.GetFileNameWithoutExtension(file), symbol, StringComparison.OrdinalIgnoreCase));
        }

        private static int FindMappedColumn(PortfolioDefinition definition, string[] header, params string[] aliases)
        {
            var mapping = definition.Mappings?.FirstOrDefault(m => aliases.Any(a => Normalize(m.Field).Contains(Normalize(a), StringComparison.OrdinalIgnoreCase)));
            if (mapping != null) return Math.Max(0, mapping.Column - 1);
            if (header.Length > 0)
                for (var i = 0; i < header.Length; i++) if (aliases.Any(a => Normalize(header[i]).Contains(Normalize(a), StringComparison.OrdinalIgnoreCase))) return i;
            return -1;
        }

        private static string Normalize(string value) => value.Replace("ی", "ي", StringComparison.Ordinal).Replace("ک", "ك", StringComparison.Ordinal).Replace(" ", string.Empty, StringComparison.Ordinal).Replace("_", string.Empty, StringComparison.Ordinal).Trim();

        private static string[] SplitLine(string line, char separator)
        {
            var result = new List<string>(); var current = new StringBuilder(); var quoted = false;
            foreach (var ch in line)
            {
                if (ch == '"') { quoted = !quoted; continue; }
                if (ch == separator && !quoted) { result.Add(current.ToString().Trim()); current.Clear(); } else current.Append(ch);
            }
            result.Add(current.ToString().Trim()); return result.ToArray();
        }

        private static bool TryGetDouble(string[] fields, int index, out double value)
        {
            value = 0; if (index < 0 || index >= fields.Length) return false;
            var text = fields[index].Trim().Replace(",", string.Empty, StringComparison.Ordinal);
            return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value) || double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
        }

        private static bool TryGetDate(string[] fields, int dateColumn, int timeColumn, InputCalendar calendar, out DateTime date)
        {
            date = default; if (dateColumn < 0 || dateColumn >= fields.Length) return false;
            var dateText = new string(fields[dateColumn].Where(char.IsDigit).ToArray());
            var timeText = timeColumn >= 0 && timeColumn < fields.Length ? new string(fields[timeColumn].Where(char.IsDigit).ToArray()) : string.Empty;
            if (calendar == InputCalendar.Persian && dateText.Length >= 8 && int.TryParse(dateText[..4], out var py) && int.TryParse(dateText.Substring(4, 2), out var pm) && int.TryParse(dateText.Substring(6, 2), out var pd))
            {
                try
                {
                    var pc = new PersianCalendar(); var hour = timeText.Length >= 2 ? int.Parse(timeText[..2]) : 0; var minute = timeText.Length >= 4 ? int.Parse(timeText.Substring(2, 2)) : 0; var second = timeText.Length >= 6 ? int.Parse(timeText.Substring(4, 2)) : 0;
                    date = pc.ToDateTime(py, pm, pd, hour, minute, second, 0); return true;
                }
                catch { return false; }
            }
            if (DateTime.TryParse(fields[dateColumn], CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date))
            {
                if (timeText.Length >= 2 && int.TryParse(timeText[..2], out var hour))
                {
                    var minute = timeText.Length >= 4 && int.TryParse(timeText.Substring(2, 2), out var m) ? m : 0;
                    var second = timeText.Length >= 6 && int.TryParse(timeText.Substring(4, 2), out var s) ? s : 0;
                    date = date.Date.Add(new TimeSpan(hour, minute, second));
                }
                return true;
            }
            return false;
        }
    }
}
