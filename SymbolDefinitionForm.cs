using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Trade.It;

public sealed partial class SymbolDefinitionForm : Form
{
    private bool loading;
    private string selectedMarketNodeId = "";
    private int sortColumnIndex = -1;
    private SortOrder sortOrder = SortOrder.None;
    private bool invalidMarketReferenceWarningShown;

    public SymbolDefinitionForm()
    {
        InitializeComponent();
        foreach (DataGridViewColumn column in symbolsDataGridView.Columns)
            column.SortMode = column == rowNumberColumn ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Programmatic;
        SetComboDefaults();
        LoadMarketTree();
        LoadGrid();
        ShowInvalidMarketReferenceWarning();
        newButton.Click += (_, _) => ClearEditor();
        saveButton.Click += (_, _) => SaveCurrent();
        deleteButton.Click += (_, _) => DeleteCurrent();
        deleteAllButton.Click += (_, _) => DeleteAll();
        importButton.Click += (_, _) => ImportExcel();
        exportButton.Click += (_, _) => ExportExcel();
        closeButton.Click += (_, _) => Close();
        symbolsDataGridView.SelectionChanged += (_, _) => LoadSelected();
        marketTreeView.AfterSelect += (_, e) =>
        {
            selectedMarketNodeId = e.Node?.Tag as string ?? e.Node?.Name ?? "";
        };
        symbolsDataGridView.ColumnHeaderMouseClick += SymbolsDataGridView_ColumnHeaderMouseClick;
    }

    private void SetComboDefaults()
    {
        var marketData = MarketStructureStore.Load();

        assetComboBox.Items.Clear();
        foreach (var category in marketData.AssetCategories)
            assetComboBox.Items.Add(category);
        assetComboBox.SelectedIndex = -1;

        otherItemComboBox.Items.Clear();
        foreach (var item in marketData.OtherCategories)
            otherItemComboBox.Items.Add(item);
        otherItemComboBox.SelectedIndex = -1;
    }

    private void LoadMarketTree(string? selectedNodeId = null)
    {
        marketTreeView.BeginUpdate();
        try
        {
            marketTreeView.Nodes.Clear();

            var data = MarketStructureStore.Load();
            var roots = data.Nodes
                .Where(x => string.IsNullOrWhiteSpace(x.ParentId))
                .OrderBy(x => x.SortOrder)
                .ToList();

            foreach (var root in roots)
                marketTreeView.Nodes.Add(CreateMarketTreeNode(root, data.Nodes, selectedNodeId));

            if (string.IsNullOrWhiteSpace(selectedNodeId))
                marketTreeView.SelectedNode = null;
        }
        finally
        {
            marketTreeView.EndUpdate();
        }
    }

    private static TreeNode CreateMarketTreeNode(
        MarketStructureNode item,
        IReadOnlyList<MarketStructureNode> allNodes,
        string? selectedNodeId)
    {
        var node = new TreeNode(item.Title)
        {
            Name = item.Id,
            Tag = item.Id
        };


        foreach (var child in allNodes
            .Where(x => string.Equals(x.ParentId, item.Id, StringComparison.Ordinal))
            .OrderBy(x => x.SortOrder))
        {
            node.Nodes.Add(CreateMarketTreeNode(child, allNodes, selectedNodeId));
        }

        return node;
    }

    private string GetSelectedMarketNodeId()
    {
        if (!string.IsNullOrWhiteSpace(selectedMarketNodeId))
            return selectedMarketNodeId;

        var selectedNode = marketTreeView.SelectedNode;
        if (selectedNode == null)
            return "";

        return selectedNode.Tag as string ?? selectedNode.Name ?? "";
    }

    private void SelectMarketTreeNode(string nodeId)
    {
        marketTreeView.SelectedNode = null;
        selectedMarketNodeId = "";

        nodeId = SymbolDefinitionRules.NormalizeText(nodeId);
        if (string.IsNullOrWhiteSpace(nodeId))
            return;

        foreach (TreeNode root in marketTreeView.Nodes)
        {
            var found = FindMarketTreeNode(root, nodeId);
            if (found != null)
            {
                marketTreeView.SelectedNode = found;
                selectedMarketNodeId = found.Tag as string ?? found.Name ?? "";
                found.EnsureVisible();
                return;
            }
        }
    }

    private static TreeNode? FindMarketTreeNode(TreeNode node, string nodeId)
    {
        var tagId = node.Tag as string;
        if (string.Equals(tagId, nodeId, StringComparison.Ordinal) ||
            string.Equals(node.Name, nodeId, StringComparison.Ordinal))
            return node;

        foreach (TreeNode child in node.Nodes)
        {
            var found = FindMarketTreeNode(child, nodeId);
            if (found != null)
                return found;
        }

        return null;
    }

    private static bool HasValidMarketPath(
        IReadOnlyList<MarketStructureNode> nodes,
        MarketStructureNode start)
    {
        var byId = nodes.ToDictionary(x => x.Id, StringComparer.Ordinal);
        var visited = new HashSet<string>(StringComparer.Ordinal);
        var current = start;

        while (visited.Add(current.Id))
        {
            if (string.IsNullOrWhiteSpace(current.ParentId))
                return true;

            if (!byId.TryGetValue(current.ParentId, out current!))
                return false;
        }

        return false;
    }

    private static (string Exchange, string Market, string Board) GetMarketPath(SymbolDefinition item)
    {
        var nodeId = SymbolDefinitionRules.NormalizeText(item.MarketNodeId);
        if (string.IsNullOrWhiteSpace(nodeId))
            return ("", "", "");

        var data = MarketStructureStore.Load();
        var byId = data.Nodes.ToDictionary(x => x.Id, StringComparer.Ordinal);

        if (!byId.TryGetValue(nodeId, out var node))
            return ("", "", "");

        var path = new List<string>();
        var visited = new HashSet<string>(StringComparer.Ordinal);
        var reachedRoot = false;

        while (node != null && visited.Add(node.Id))
        {
            path.Add(node.Title);

            if (string.IsNullOrWhiteSpace(node.ParentId))
            {
                reachedRoot = true;
                break;
            }

            if (!byId.TryGetValue(node.ParentId, out node!))
                break;
        }

        if (!reachedRoot)
            return ("", "", "");

        path.Reverse();

        return (
            path.Count > 0 ? path[0] : "",
            path.Count > 1 ? path[1] : "",
            path.Count > 2 ? path[2] : "");
    }

    private void ShowInvalidMarketReferenceWarning()
    {
        if (invalidMarketReferenceWarningShown)
            return;

        var invalidSymbols = SymbolDefinitionStore.FindInvalidMarketReferences();
        var duplicateSymbols = SymbolDefinitionStore.FindDuplicateSymbols();

        if (invalidSymbols.Count == 0 && duplicateSymbols.Count == 0)
            return;

        invalidMarketReferenceWarningShown = true;

        var message = new StringBuilder();

        if (invalidSymbols.Count > 0)
        {
            var preview = string.Join("، ", invalidSymbols.Take(10));
            if (invalidSymbols.Count > 10)
                preview += "، ...";

            message.AppendLine(
                $"برای {invalidSymbols.Count} نماد، ساختار بازار ثبت‌شده در فایل نمادها معتبر نیست یا دیگر در ساختار بازار وجود ندارد.");
            message.AppendLine();
            message.AppendLine($"نمادها: {preview}");
            message.AppendLine();
            message.AppendLine(
                "این نمادها حذف یا اصلاح نشده‌اند. برای جلوگیری از انتساب اشتباه، ابتدا ساختار بازار مربوط به آنها را بررسی کنید.");
        }

        if (duplicateSymbols.Count > 0)
        {
            if (message.Length > 0)
                message.AppendLine().AppendLine();

            var preview = string.Join("، ", duplicateSymbols.Take(10));
            if (duplicateSymbols.Count > 10)
                preview += "، ...";

            message.AppendLine(
                $"تعداد {duplicateSymbols.Count} نماد در فایل نمادها بیش از یک بار ثبت شده است.");
            message.AppendLine();
            message.AppendLine($"نمادهای تکراری: {preview}");
            message.AppendLine();
            message.AppendLine(
                "نمادهای تکراری خودکار حذف یا ادغام نشده‌اند.");
        }

        MessageBox.Show(
            this,
            message.ToString().TrimEnd(),
            "اعتبارسنجی نمادها",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    private void LoadGrid(string? selectSymbol = null)
    {
        loading = true;
        try
        {
            symbolsDataGridView.Rows.Clear();
            int rowNumber = 1;
            foreach (var item in SymbolDefinitionStore.Load().OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase))
            {
                var marketPath = GetMarketPath(item);

                int r = symbolsDataGridView.Rows.Add(
                    rowNumber++,
                    item.SymbolTitle,
                    item.Name,
                    marketPath.Exchange,
                    marketPath.Market,
                    marketPath.Board,
                    item.AssetCategory);

                symbolsDataGridView.Rows[r].Tag = item;
            }

            countLabel.Text = $"تعداد: {symbolsDataGridView.Rows.Count}";
            if (!string.IsNullOrWhiteSpace(selectSymbol))
                foreach (DataGridViewRow r in symbolsDataGridView.Rows)
                    if (string.Equals(Convert.ToString(r.Cells[1].Value), selectSymbol, StringComparison.OrdinalIgnoreCase))
                    {
                        r.Selected = true;
                        break;
                    }
        }
        finally { loading = false; }
    }

    private void SymbolsDataGridView_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex == rowNumberColumn.Index) return;

        var nextOrder = sortColumnIndex == e.ColumnIndex && sortOrder == SortOrder.Ascending
            ? SortOrder.Descending
            : SortOrder.Ascending;

        SortSymbolsGrid(e.ColumnIndex, nextOrder);
    }

    private void SortSymbolsGrid(int columnIndex, SortOrder order)
    {
        var selectedSymbol = symbolsDataGridView.SelectedRows.Count > 0
            ? symbolsDataGridView.SelectedRows[0].Tag as SymbolDefinition
            : null;

        var items = symbolsDataGridView.Rows.Cast<DataGridViewRow>()
            .Where(r => r.Tag is SymbolDefinition)
            .Select(r => (SymbolDefinition)r.Tag!)
            .ToList();

        items.Sort((a, b) =>
        {
            var aKey = GetSortValue(a, columnIndex);
            var bKey = GetSortValue(b, columnIndex);
            var result = StringComparer.OrdinalIgnoreCase.Compare(aKey, bKey);
            return order == SortOrder.Descending ? -result : result;
        });

        loading = true;
        try
        {
            symbolsDataGridView.Rows.Clear();
            int rowNumber = 1;
            foreach (var item in items)
            {
                var marketPath = GetMarketPath(item);

                int rowIndex = symbolsDataGridView.Rows.Add(
                    rowNumber++,
                    item.SymbolTitle,
                    item.Name,
                    marketPath.Exchange,
                    marketPath.Market,
                    marketPath.Board,
                    item.AssetCategory);

                symbolsDataGridView.Rows[rowIndex].Tag = item;
                if (selectedSymbol != null && ReferenceEquals(item, selectedSymbol))
                    symbolsDataGridView.Rows[rowIndex].Selected = true;
            }

            sortColumnIndex = columnIndex;
            sortOrder = order;
            foreach (DataGridViewColumn column in symbolsDataGridView.Columns)
                column.HeaderCell.SortGlyphDirection = column.Index == columnIndex ? order : SortOrder.None;
        }
        finally
        {
            loading = false;
        }

        if (selectedSymbol != null && symbolsDataGridView.SelectedRows.Count > 0)
            LoadSelected();
    }

    private static string GetSortValue(SymbolDefinition item, int columnIndex) => columnIndex switch
    {
        1 => item.SymbolTitle ?? string.Empty,
        2 => item.Name ?? string.Empty,
        3 => GetMarketPath(item).Exchange,
        4 => GetMarketPath(item).Market,
        5 => GetMarketPath(item).Board,
        6 => item.AssetCategory ?? item.AssetType ?? string.Empty,
        _ => string.Empty
    };

    private void LoadSelected()
    {
        if (loading || symbolsDataGridView.SelectedRows.Count == 0 || symbolsDataGridView.SelectedRows[0].Tag is not SymbolDefinition x) return;
        symbolTextBox.Text = SymbolDefinitionRules.NormalizeText(x.SymbolTitle);
        nameTextBox.Text = SymbolDefinitionRules.NormalizeText(x.Name);
        SelectComboValue(assetComboBox, string.IsNullOrWhiteSpace(x.AssetCategory) ? x.AssetType : x.AssetCategory);
        SelectComboValue(otherItemComboBox, x.OtherItem);
        SelectMarketTreeNode(x.MarketNodeId);

    }

    private static void SelectComboValue(ComboBox comboBox, string value)
    {
        var normalized = SymbolDefinitionRules.NormalizeText(value);
        if (normalized == SymbolDefinitionRules.EmptyOption) normalized = "";
        if (string.IsNullOrWhiteSpace(normalized))
        {
            comboBox.SelectedIndex = -1;
            return;
        }
        for (int i = 0; i < comboBox.Items.Count; i++)
        {
            if (string.Equals(SymbolDefinitionRules.NormalizeText(Convert.ToString(comboBox.Items[i]) ?? ""), normalized, StringComparison.Ordinal))
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
        comboBox.SelectedIndex = -1;
    }

    private static string[] ComboValues(ComboBox comboBox) =>
        comboBox.Items.Cast<object>().Select(x => SymbolDefinitionRules.NormalizeText(Convert.ToString(x) ?? "")).ToArray();

    private void ClearEditor()
    {
        symbolsDataGridView.ClearSelection();
        symbolTextBox.Clear();
        nameTextBox.Clear();
        SetComboDefaults();
        LoadGrid();
        marketTreeView.SelectedNode = null;
        symbolTextBox.Focus();
    }

    private SymbolDefinition? ReadEditor()
    {
        var symbol = SymbolDefinitionRules.NormalizeText(symbolTextBox.Text);
        if (string.IsNullOrWhiteSpace(symbol))
        {
            MessageBox.Show(this, "عنوان نماد را وارد کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            symbolTextBox.Focus();
            return null;
        }
        var marketNodeId = SymbolDefinitionRules.NormalizeText(GetSelectedMarketNodeId());
        if (string.IsNullOrWhiteSpace(marketNodeId))
        {
            MessageBox.Show(this, "ساختار بازار را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            marketTreeView.Focus();
            return null;
        }

        var marketData = MarketStructureStore.Load();
        var marketNode = marketData.Nodes.FirstOrDefault(x =>
            string.Equals(x.Id, marketNodeId, StringComparison.Ordinal));

        if (marketNode == null || !HasValidMarketPath(marketData.Nodes, marketNode))
        {
            MessageBox.Show(this, "ساختار بازار انتخاب‌شده معتبر نیست. دوباره یک ردیف را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            marketTreeView.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(assetComboBox.Text, assetComboBox.Items, out var asset))
        {
            MessageBox.Show(this, "نوع دارایی را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            assetComboBox.Focus();
            return null;
        }
        return new SymbolDefinition
        {
            SymbolTitle = symbol,
            Name = SymbolDefinitionRules.NormalizeText(nameTextBox.Text),
            MarketNodeId = marketNodeId,
            ExchangeTitle = "",
            MarketType = "",
            BoardType = "",
            AssetType = asset,
            AssetCategory = asset,
            OtherItem = SymbolDefinitionRules.NormalizeText(otherItemComboBox.Text),
            FundType = "",
            IndustryGroup = "",
            IndustryGroupOrFundType = ""
        };
    }

    private void SaveCurrent()
    {
        var item = ReadEditor();
        if (item == null) return;
        var all = SymbolDefinitionStore.Load();
        var selectedSymbol = symbolsDataGridView.SelectedRows.Count > 0
            ? (symbolsDataGridView.SelectedRows[0].Tag as SymbolDefinition)?.SymbolTitle
            : null;

        var existing = all.FirstOrDefault(x =>
            string.Equals(x.SymbolTitle.Trim(), item.SymbolTitle, StringComparison.OrdinalIgnoreCase));

        if (existing != null &&
            (string.IsNullOrWhiteSpace(selectedSymbol) ||
             !string.Equals(existing.SymbolTitle, selectedSymbol, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این نماد قبلاً ثبت شده است.", "تعریف نمادها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (existing == null)
        {
            all.Add(item);
        }
        else
        {
            existing.SymbolTitle = item.SymbolTitle;
            existing.Name = item.Name;
            existing.MarketNodeId = item.MarketNodeId;
            existing.ExchangeTitle = item.ExchangeTitle;
            existing.MarketType = item.MarketType;
            existing.BoardType = item.BoardType;
            existing.AssetType = item.AssetType;
            existing.AssetCategory = item.AssetCategory;
            existing.OtherItem = item.OtherItem;
            existing.FundType = item.FundType;
            existing.IndustryGroup = item.IndustryGroup;
            existing.IndustryGroupOrFundType = "";
        }
        SymbolDefinitionStore.Save(all);
        LoadGrid(item.SymbolTitle);
    }

    private void DeleteCurrent()
    {
        var s = SymbolDefinitionRules.NormalizeText(symbolTextBox.Text);
        if (string.IsNullOrWhiteSpace(s)) return;
        if (MessageBox.Show(this, $"آیا نماد «{s}» حذف شود؟", "حذف نماد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        var all = SymbolDefinitionStore.Load();
        all.RemoveAll(x => string.Equals(x.SymbolTitle.Trim(), s, StringComparison.OrdinalIgnoreCase));
        SymbolDefinitionStore.Save(all);
        LoadGrid();
        ClearEditor();
    }

    private void DeleteAll()
    {
        var all = SymbolDefinitionStore.Load();
        if (all.Count == 0) return;
        if (MessageBox.Show(this, $"هر {all.Count} نماد ثبت‌شده حذف می‌شود. آیا مطمئن هستید؟", "حذف همه نمادها", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        SymbolDefinitionStore.Save(Array.Empty<SymbolDefinition>());
        LoadGrid();
        ClearEditor();
    }

    private void ExportExcel()
    {
        using var d = new SaveFileDialog
        {
            Title = "ذخیره نمادها در فایل Excel",
            Filter = "Excel (*.xlsx)|*.xlsx",
            DefaultExt = "xlsx",
            AddExtension = true,
            FileName = $"Symbols_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
            OverwritePrompt = true
        };

        if (d.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var items = SymbolDefinitionStore.Load();
            ExcelSymbolWriter.Write(d.FileName, items);

            MessageBox.Show(
                this,
                $"خروجی Excel با موفقیت ایجاد شد.{Environment.NewLine}" +
                $"تعداد نمادها: {items.Count}",
                "خروجی به Excel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"ایجاد فایل Excel انجام نشد:{Environment.NewLine}{ex.Message}",
                "خروجی به Excel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void ImportExcel()
    {
        using var d = new OpenFileDialog
        {
            Title = "انتخاب فایل Excel نمادها",
            Filter = "Excel (*.xlsx)|*.xlsx",
            CheckFileExists = true
        };

        if (d.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var imported = ExcelSymbolReader.Read(
                d.FileName,
                ComboValues(assetComboBox),
                out var invalidRows,
                out var invalidDetails);

            if (imported.Count == 0)
            {
                var message = invalidRows > 0
                    ? $"هیچ ردیف معتبری پیدا نشد.{Environment.NewLine}" +
                      $"ردیف‌های نامعتبر: {invalidRows}{Environment.NewLine}{Environment.NewLine}" +
                      invalidDetails
                    : "هیچ ردیف قابل استفاده‌ای پیدا نشد.";

                MessageBox.Show(
                    this,
                    message,
                    "ورود از Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var existingDuplicates = SymbolDefinitionStore.FindDuplicateSymbols();
            if (existingDuplicates.Count > 0)
            {
                var preview = string.Join("، ", existingDuplicates.Take(10));
                if (existingDuplicates.Count > 10)
                    preview += "، ...";

                MessageBox.Show(
                    this,
                    $"فایل فعلی نمادها شامل {existingDuplicates.Count} نماد تکراری است و قبل از ورود Excel باید اصلاح شود.{Environment.NewLine}{Environment.NewLine}" +
                    $"نمادهای تکراری: {preview}",
                    "ورود از Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var importedDuplicates = imported
                .GroupBy(
                    x => SymbolDefinitionRules.NormalizeText(x.SymbolTitle),
                    StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (importedDuplicates.Count > 0)
            {
                var preview = string.Join("، ", importedDuplicates.Take(10));
                if (importedDuplicates.Count > 10)
                    preview += "، ...";

                MessageBox.Show(
                    this,
                    $"در فایل Excel، {importedDuplicates.Count} نماد بیش از یک بار آمده است.{Environment.NewLine}{Environment.NewLine}" +
                    $"نمادهای تکراری: {preview}{Environment.NewLine}{Environment.NewLine}" +
                    "برای جلوگیری از ثبت یا به‌روزرسانی مبهم، ورود انجام نشد.",
                    "ورود از Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var map = SymbolDefinitionStore
                .Load()
                .ToDictionary(
                    x => SymbolDefinitionRules.NormalizeText(x.SymbolTitle),
                    StringComparer.OrdinalIgnoreCase);

            int added = 0;
            int updated = 0;

            foreach (var x in imported)
            {
                var key = SymbolDefinitionRules.NormalizeText(x.SymbolTitle);

                if (map.ContainsKey(key))
                    updated++;
                else
                    added++;

                map[key] = x;
            }

            SymbolDefinitionStore.Save(map.Values);
            LoadGrid();

            var resultMessage =
                $"ورود انجام شد.{Environment.NewLine}" +
                $"جدید: {added}{Environment.NewLine}" +
                $"به‌روزشده: {updated}";

            if (invalidRows > 0)
            {
                resultMessage +=
                    $"{Environment.NewLine}نامعتبر: {invalidRows}" +
                    $"{Environment.NewLine}{Environment.NewLine}" +
                    invalidDetails;
            }

            MessageBox.Show(
                this,
                resultMessage,
                "ورود از Excel",
                MessageBoxButtons.OK,
                invalidRows > 0
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"خواندن فایل Excel انجام نشد:{Environment.NewLine}{ex.Message}",
                "ورود از Excel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    public sealed class SymbolDefinition
    {
        public string SymbolTitle { get; set; } = "";
        public string Name { get; set; } = "";
        public string MarketNodeId { get; set; } = "";
        public string AssetCategory { get; set; } = "";
        public string ExchangeTitle { get; set; } = "";
        public string MarketType { get; set; } = "";
        public string BoardType { get; set; } = "";
        public string AssetType { get; set; } = "";
        public string OtherItem { get; set; } = "";
        public string FundType { get; set; } = "";
        public string IndustryGroup { get; set; } = "";
        public string IndustryGroupOrFundType { get; set; } = "";
    }

    public static class SymbolDefinitionStore
    {
        private static string FilePath => Path.Combine(AppContext.BaseDirectory, "Data", "Symbols.json");
        private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

        public static List<SymbolDefinition> Load()
        {
            try
            {
                if (!File.Exists(FilePath)) return new();
                var items = JsonSerializer.Deserialize<List<SymbolDefinition>>(File.ReadAllText(FilePath, Encoding.UTF8), Options) ?? new();
                foreach (var x in items) SymbolDefinitionRules.Normalize(x);
                return items;
            }
            catch { return new(); }
        }

        public static List<string> FindInvalidMarketReferences()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return new();

                var items = JsonSerializer.Deserialize<List<SymbolDefinition>>(
                    File.ReadAllText(FilePath, Encoding.UTF8), Options) ?? new();

                var marketData = MarketStructureStore.Load();
                var byId = marketData.Nodes.ToDictionary(x => x.Id, StringComparer.Ordinal);
                var invalid = new List<string>();

                foreach (var item in items)
                {
                    var nodeId = SymbolDefinitionRules.NormalizeText(item.MarketNodeId);

                    if (string.IsNullOrWhiteSpace(nodeId) ||
                        !byId.TryGetValue(nodeId, out var node) ||
                        !HasValidMarketPath(byId, node))
                    {
                        var symbol = SymbolDefinitionRules.NormalizeText(item.SymbolTitle);

                        if (!string.IsNullOrWhiteSpace(symbol))
                            invalid.Add(symbol);
                    }
                }

                return invalid
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch
            {
                return new();
            }
        }

        public static List<string> FindDuplicateSymbols()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return new();

                var items = JsonSerializer.Deserialize<List<SymbolDefinition>>(
                    File.ReadAllText(FilePath, Encoding.UTF8), Options) ?? new();

                return items
                    .Select(x => SymbolDefinitionRules.NormalizeText(x.SymbolTitle))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch
            {
                return new();
            }
        }

        private static bool HasValidMarketPath(
            IReadOnlyDictionary<string, MarketStructureNode> byId,
            MarketStructureNode start)
        {
            var visited = new HashSet<string>(StringComparer.Ordinal);
            var current = start;

            while (visited.Add(current.Id))
            {
                if (string.IsNullOrWhiteSpace(current.ParentId))
                    return true;

                if (!byId.TryGetValue(current.ParentId, out current!))
                    return false;
            }

            return false;
        }

        public static void Save(IEnumerable<SymbolDefinition> items)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var normalized = items.ToList();
            foreach (var x in normalized) SymbolDefinitionRules.Normalize(x);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(normalized.OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase).ToList(), Options), new UTF8Encoding(false));
        }
    }

    internal static class SymbolDefinitionRules
    {
        public const string EmptyOption = "-";

        public static string NormalizeText(string value)
        {
            return value
                .Replace('ي', 'ی')
                .Replace('ى', 'ی')
                .Replace('ك', 'ک')
                .Replace('‌', ' ')
                .Replace('‍', ' ')
                .Replace('﻿', ' ')
                .Trim();
        }

        public static void Normalize(SymbolDefinition x)
        {
            x.SymbolTitle = NormalizeText(x.SymbolTitle);
            x.Name = NormalizeText(x.Name);
            x.MarketNodeId = NormalizeText(x.MarketNodeId);
            x.AssetCategory = NormalizeOption(x.AssetCategory);
            x.ExchangeTitle = NormalizeOption(x.ExchangeTitle);
            x.MarketType = NormalizeOption(x.MarketType);
            x.BoardType = NormalizeOption(x.BoardType);
            x.AssetType = NormalizeOption(x.AssetType);
            x.OtherItem = NormalizeOption(x.OtherItem);
            x.FundType = NormalizeOption(x.FundType);
            x.IndustryGroup = NormalizeOption(x.IndustryGroup);
            x.IndustryGroupOrFundType = NormalizeOption(x.IndustryGroupOrFundType);
        }

        private static string NormalizeOption(string value)
        {
            var normalized = NormalizeText(value);
            return normalized == EmptyOption ? "" : normalized;
        }

        public static bool IsAllowed(string value, ComboBox.ObjectCollection items, out string standardValue) =>
            IsAllowed(value, items.Cast<object>(), out standardValue);

        public static bool IsAllowed(string value, IEnumerable<object> items, out string standardValue)
        {
            var normalized = NormalizeText(value);
            if (normalized == EmptyOption) normalized = "";
            if (string.IsNullOrWhiteSpace(normalized))
            {
                standardValue = "";
                return true;
            }
            var match = items
                .Select(x => NormalizeText(Convert.ToString(x) ?? ""))
                .FirstOrDefault(x => string.Equals(x, normalized, StringComparison.Ordinal));
            standardValue = match ?? "";
            return match != null;
        }

        public static bool IsAllowed(string value, IReadOnlyCollection<string> allowed, out string standardValue)
        {
            var normalized = NormalizeText(value);
            if (normalized == EmptyOption) normalized = "";
            if (string.IsNullOrWhiteSpace(normalized))
            {
                standardValue = "";
                return true;
            }
            var match = allowed.FirstOrDefault(x => string.Equals(NormalizeText(x), normalized, StringComparison.Ordinal));
            standardValue = match ?? "";
            return match != null;
        }
    }

    internal static class ExcelSymbolReader
    {
        private static readonly string[] Headers = { "نماد", "نام", "بورس", "بازار", "تابلو", "دارایی" };

        public static List<SymbolDefinition> Read(
            string path,
            IReadOnlyCollection<string> assets,
            out int invalidRows,
            out string invalidDetails)
        {
            invalidRows = 0;
            invalidDetails = "";
            var details = new List<string>();
            using var zip = ZipFile.OpenRead(path);
            XNamespace s = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            XNamespace r = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
            XNamespace pr = "http://schemas.openxmlformats.org/package/2006/relationships";
            var wb = XDocument.Load(Entry(zip, "xl/workbook.xml"));
            var rels = XDocument.Load(Entry(zip, "xl/_rels/workbook.xml.rels"));
            var sheet = wb.Root?.Element(s + "sheets")?.Element(s + "sheet") ?? throw new InvalidDataException("Sheet پیدا نشد.");
            var rid = (string?)sheet.Attribute(r + "id") ?? throw new InvalidDataException("رابطه Sheet پیدا نشد.");
            var target = (string?)rels.Root?.Elements(pr + "Relationship").FirstOrDefault(x => (string?)x.Attribute("Id") == rid)?.Attribute("Target") ?? throw new InvalidDataException("مسیر Sheet پیدا نشد.");
            var sheetPath = target.StartsWith("/") ? target.TrimStart('/') : "xl/" + target.TrimStart('/');
            sheetPath = sheetPath.Replace("xl/xl/", "xl/");
            var shared = SharedStrings(zip, s);
            var doc = XDocument.Load(Entry(zip, sheetPath));
            var rows = doc.Root?.Element(s + "sheetData")?.Elements(s + "row").ToList() ?? new();
            if (rows.Count < 2) return new();
            var headerRow = Row(rows[0], s, shared);
            var indexByHeader = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var pair in headerRow)
            {
                var normalized = SymbolDefinitionRules.NormalizeText(pair.Value);
                foreach (var expected in Headers)
                    if (HeaderMatches(normalized, expected)) indexByHeader[expected] = pair.Key;
            }
            foreach (var h in Headers) if (!indexByHeader.ContainsKey(h)) throw new InvalidDataException($"ستون «{h}» در فایل پیدا نشد.");
            var result = new List<SymbolDefinition>();
            int excelRow = 1;
            foreach (var xmlRow in rows.Skip(1))
            {
                excelRow++;
                var cells = Row(xmlRow, s, shared);
                string V(string h) => cells.TryGetValue(indexByHeader[h], out var value) ? SymbolDefinitionRules.NormalizeText(value) : "";
                var symbol = V(Headers[0]);
                if (string.IsNullOrWhiteSpace(symbol))
                {
                    invalidRows++;
                    if (details.Count < 20) details.Add($"ردیف {excelRow}: عنوان نماد خالی است.");
                    continue;
                }

                var exchange = V(Headers[2]);
                var market = V(Headers[3]);
                var board = V(Headers[4]);
                var asset = V(Headers[5]);

                var errors = new List<string>();

                if (!SymbolDefinitionRules.IsAllowed(asset, assets, out var standardAsset))
                    errors.Add($"نوع دارایی «{asset}»");

                var marketNodeId = ResolveMarketNodeId(exchange, market, board, out var marketError);
                if (marketNodeId == "")
                    errors.Add(marketError);

                if (errors.Count > 0)
                {
                    invalidRows++;
                    if (details.Count < 20) details.Add($"ردیف {excelRow}: {string.Join("، ", errors)}");
                    continue;
                }

                result.Add(new SymbolDefinition
                {
                    SymbolTitle = symbol,
                    Name = V(Headers[1]),
                    MarketNodeId = marketNodeId,
                    ExchangeTitle = exchange,
                    MarketType = market,
                    BoardType = board,
                    AssetType = standardAsset,
                    AssetCategory = standardAsset,
                    FundType = "",
                    IndustryGroup = "",
                    IndustryGroupOrFundType = ""
                });
            }
            invalidDetails = details.Count == 0 ? "" : string.Join(Environment.NewLine, details) + (invalidRows > details.Count ? Environment.NewLine + "..." : "");
            return result;
        }

        private static bool HeaderMatches(string actual, string expected) => actual == SymbolDefinitionRules.NormalizeText(expected) || actual.Replace(" ", "") == SymbolDefinitionRules.NormalizeText(expected).Replace(" ", "");
        private static string ResolveMarketNodeId(
            string exchange,
            string market,
            string board,
            out string error)
        {
            error = "";
            exchange = SymbolDefinitionRules.NormalizeText(exchange);
            market = SymbolDefinitionRules.NormalizeText(market);
            board = SymbolDefinitionRules.NormalizeText(board);

            if (string.IsNullOrWhiteSpace(exchange))
            {
                error = "عنوان بورس خالی است.";
                return "";
            }

            if (string.IsNullOrWhiteSpace(market) && string.IsNullOrWhiteSpace(board))
            {
                error = "ساختار بازار خالی است.";
                return "";
            }

            var data = MarketStructureStore.Load();
            var roots = data.Nodes
                .Where(x => string.IsNullOrWhiteSpace(x.ParentId))
                .ToList();

            var exchangeNode = roots.FirstOrDefault(x =>
                string.Equals(x.Title, exchange, StringComparison.OrdinalIgnoreCase));

            if (exchangeNode == null)
            {
                error = $"بورس «{exchange}» در ساختار بازار پیدا نشد.";
                return "";
            }

            var currentParentId = exchangeNode.Id;

            if (!string.IsNullOrWhiteSpace(market))
            {
                var marketNode = data.Nodes.FirstOrDefault(x =>
                    string.Equals(x.ParentId, currentParentId, StringComparison.Ordinal) &&
                    string.Equals(x.Title, market, StringComparison.OrdinalIgnoreCase));

                if (marketNode == null)
                {
                    error = $"بازار «{market}» زیر «{exchange}» پیدا نشد.";
                    return "";
                }

                currentParentId = marketNode.Id;
            }

            if (!string.IsNullOrWhiteSpace(board))
            {
                var boardNode = data.Nodes.FirstOrDefault(x =>
                    string.Equals(x.ParentId, currentParentId, StringComparison.Ordinal) &&
                    string.Equals(x.Title, board, StringComparison.OrdinalIgnoreCase));

                if (boardNode == null)
                {
                    error = $"تابلو/زیرشاخه «{board}» زیر «{market}» پیدا نشد.";
                    return "";
                }

                currentParentId = boardNode.Id;
            }

            return currentParentId;
        }


        private static Dictionary<int, string> Row(XElement row, XNamespace s, IReadOnlyList<string> shared)
        {
            var result = new Dictionary<int, string>();
            foreach (var c in row.Elements(s + "c"))
            {
                var reference = (string?)c.Attribute("r") ?? "";
                int col = ColumnIndex(reference);
                var value = c.Element(s + "v")?.Value ?? "";
                var type = (string?)c.Attribute("t");
                if (type == "s" && int.TryParse(value, out var n) && n >= 0 && n < shared.Count) value = shared[n];
                else if (type == "inlineStr") value = c.Element(s + "is")?.Value ?? "";
                result[col] = value;
            }
            return result;
        }

        private static int ColumnIndex(string reference)
        {
            int n = 0;
            foreach (char c in reference)
            {
                if (!char.IsLetter(c)) break;
                n = n * 26 + char.ToUpperInvariant(c) - 'A' + 1;
            }
            return n - 1;
        }

        private static List<string> SharedStrings(ZipArchive zip, XNamespace s)
        {
            var e = zip.GetEntry("xl/sharedStrings.xml");
            if (e == null) return new();
            using var stream = e.Open();
            var doc = XDocument.Load(stream);
            return doc.Root?.Elements(s + "si").Select(x => string.Concat(x.Descendants(s + "t").Select(t => t.Value))).ToList() ?? new();
        }

        private static Stream Entry(ZipArchive zip, string path) => (zip.GetEntry(path) ?? throw new InvalidDataException($"فایل داخلی Excel پیدا نشد: {path}")).Open();
    }

    internal static class ExcelSymbolWriter
    {
        private static readonly string[] Headers =
            { "نماد", "نام", "بورس", "بازار", "تابلو", "دارایی" };

        public static void Write(string path, IEnumerable<SymbolDefinition> items)
        {
            var list = items
                .OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase)
                .ToList();

            using var file = File.Create(path);
            using var zip = new ZipArchive(file, ZipArchiveMode.Create);

            WriteEntry(zip, "[Content_Types].xml",
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>
  <Override PartName=""/xl/worksheets/sheet1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>
</Types>");

            WriteEntry(zip, "_rels/.rels",
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>
</Relationships>");

            WriteEntry(zip, "xl/workbook.xml",
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
  <sheets>
    <sheet name=""نمادها"" sheetId=""1"" r:id=""rId1""/>
  </sheets>
</workbook>");

            WriteEntry(zip, "xl/_rels/workbook.xml.rels",
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>
</Relationships>");

            WriteEntry(zip, "xl/worksheets/sheet1.xml", BuildWorksheet(list));
        }

        private static string BuildWorksheet(IReadOnlyList<SymbolDefinition> items)
        {
            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            var sheetData = new XElement(ns + "sheetData");

            sheetData.Add(new XElement(ns + "row",
                new XAttribute("r", "1"),
                Headers.Select((x, i) => Cell(ColumnName(i + 1) + "1", x))));

            for (int i = 0; i < items.Count; i++)
            {
                var x = items[i];
                var marketPath = GetMarketPath(x);

                var values = new[]
                {
                    x.SymbolTitle,
                    x.Name,
                    marketPath.Exchange,
                    marketPath.Market,
                    marketPath.Board,
                    x.AssetCategory ?? x.AssetType
                };

                sheetData.Add(new XElement(ns + "row",
                    new XAttribute("r", (i + 2).ToString()),
                    values.Select((v, c) => Cell(ColumnName(c + 1) + (i + 2), v))));
            }

            var cols = new XElement(ns + "cols",
                Enumerable.Range(1, Headers.Length).Select(i =>
                    new XElement(ns + "col",
                        new XAttribute("min", i),
                        new XAttribute("max", i),
                        new XAttribute("width", i == 2 ? 35 : 20),
                        new XAttribute("customWidth", "1"))));

            var worksheet = new XElement(ns + "worksheet",
                cols,
                sheetData);

            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), worksheet)
                .ToString(SaveOptions.DisableFormatting);
        }

        private static XElement Cell(string reference, string? value)
        {
            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            return new XElement(ns + "c",
                new XAttribute("r", reference),
                new XAttribute("t", "inlineStr"),
                new XElement(ns + "is",
                    new XElement(ns + "t",
                        new XAttribute(XNamespace.Xml + "space", "preserve"),
                        value ?? string.Empty)));
        }

        private static string ColumnName(int number)
        {
            var result = string.Empty;
            while (number > 0)
            {
                number--;
                result = (char)('A' + number % 26) + result;
                number /= 26;
            }
            return result;
        }

        private static void WriteEntry(ZipArchive zip, string name, string content)
        {
            var entry = zip.CreateEntry(name, CompressionLevel.Optimal);
            using var stream = entry.Open();
            using var writer = new StreamWriter(stream, new UTF8Encoding(false));
            writer.Write(content);
        }
    }
}

