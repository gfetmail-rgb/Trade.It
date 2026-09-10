using System.Text.Json;

namespace Trade.It
{
    public partial class MainForm
    {
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
                File.WriteAllText(file, json, new System.Text.UTF8Encoding(false));
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
                File.WriteAllText(file, json, new System.Text.UTF8Encoding(false));
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
    }
}