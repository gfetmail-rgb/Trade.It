using System.Text.Json;

namespace Trade.It;

public sealed partial class MarketStructureForm : Form
{
    private MarketStructureData data = null!;

    public MarketStructureForm()
    {
        InitializeComponent();

        marketTreeView.AfterSelect += (_, _) => UpdateMarketButtons();
        marketTitleTextBox.KeyDown += MarketTitleTextBox_KeyDown;

        addRootButton.Click += (_, _) => AddRootNode();
        importMarketStructureButton.Click += (_, _) => ImportMarketStructure();
        exportMarketStructureButton.Click += (_, _) => ExportMarketStructure();
        addChildButton.Click += (_, _) => AddChild();
        renameButton.Click += (_, _) => RenameNode();
        deleteButton.Click += (_, _) => DeleteNode();
        moveUpButton.Click += (_, _) => MoveNode(-1);
        moveDownButton.Click += (_, _) => MoveNode(1);

        closeButton.Click += (_, _) => Close();

        LoadData();
    }

    private void MarketTitleTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        // Enter adds another line; Ctrl+Enter submits a batch of children.
        if (e.KeyCode != Keys.Enter || !e.Control)
            return;

        AddChild();
        e.SuppressKeyPress = true;
        e.Handled = true;
    }

    private void LoadData()
    {
        data = MarketStructureStore.Load();
        RebuildTree();
        marketTitleTextBox.Clear();
        UpdateMarketButtons();
    }

    private void RebuildTree(string? selectId = null)
    {
        marketTreeView.BeginUpdate();
        try
        {
            marketTreeView.Nodes.Clear();

            foreach (var node in data.Nodes
                         .Where(x => string.IsNullOrEmpty(x.ParentId))
                         .OrderBy(x => x.SortOrder))
            {
                marketTreeView.Nodes.Add(BuildTreeNode(node));
            }

            if (!string.IsNullOrWhiteSpace(selectId))
            {
                var selected = FindTreeNode(marketTreeView.Nodes, selectId);
                if (selected != null)
                    marketTreeView.SelectedNode = selected;
            }

            marketTreeView.ExpandAll();
        }
        finally
        {
            marketTreeView.EndUpdate();
        }
    }

    private TreeNode BuildTreeNode(MarketStructureNode node)
    {
        var treeNode = new TreeNode(node.Title) { Tag = node };

        foreach (var child in data.Nodes
                     .Where(x => x.ParentId == node.Id)
                     .OrderBy(x => x.SortOrder))
        {
            treeNode.Nodes.Add(BuildTreeNode(child));
        }

        return treeNode;
    }

    private static TreeNode? FindTreeNode(TreeNodeCollection nodes, string id)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag is MarketStructureNode item && item.Id == id)
                return node;

            var found = FindTreeNode(node.Nodes, id);
            if (found != null)
                return found;
        }

        return null;
    }

    private MarketStructureNode? SelectedNode =>
        marketTreeView.SelectedNode?.Tag as MarketStructureNode;

    private void UpdateMarketButtons()
    {
        var selected = SelectedNode;

        renameButton.Enabled = selected != null;
        deleteButton.Enabled = selected != null;
        moveUpButton.Enabled = selected != null;
        moveDownButton.Enabled = selected != null;
        addChildButton.Enabled = true;

        if (selected != null)
            marketTitleTextBox.Text = selected.Title;
        else
            marketTitleTextBox.Clear();
    }

    private void AddRootNode()
    {
        var title = marketTitleTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(title))
            return;

        if (data.Nodes.Any(x =>
            string.IsNullOrEmpty(x.ParentId) &&
            string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این بازار قبلاً وجود دارد.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var rootCount = data.Nodes.Count(x => string.IsNullOrEmpty(x.ParentId));

        var node = new MarketStructureNode
        {
            ParentId = "",
            Title = title,
            SortOrder = rootCount
        };

        data.Nodes.Add(node);
        MarketStructureStore.Save(data);
        RebuildTree(node.Id);
        marketTitleTextBox.Clear();
    }

    private void AddChild()
    {
        var parent = SelectedNode;
        if (parent == null)
        {
            MessageBox.Show(this, "ابتدا یک بازار سطح اول را انتخاب کنید.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!string.IsNullOrWhiteSpace(parent.ParentId))
        {
            MessageBox.Show(this, "زیرمجموعه فقط برای گره‌های سطح اول قابل ثبت است.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var titles = marketTitleTextBox.Text
            .Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (titles.Count == 0)
            return;

        var existingTitles = data.Nodes
            .Where(x => x.ParentId == parent.Id)
            .Select(x => x.Title)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var added = new List<MarketStructureNode>();
        foreach (var title in titles)
        {
            if (!existingTitles.Add(title))
                continue;

            var node = new MarketStructureNode
            {
                ParentId = parent.Id,
                Title = title,
                SortOrder = data.Nodes.Count(x => x.ParentId == parent.Id)
            };
            data.Nodes.Add(node);
            added.Add(node);
        }

        if (added.Count == 0)
        {
            MessageBox.Show(this, "همه موارد واردشده قبلاً زیر این بازار ثبت شده‌اند.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        MarketStructureStore.Save(data);
        RebuildTree(added[0].Id);
        marketTitleTextBox.Clear();
        MessageBox.Show(this, $"تعداد {added.Count} زیرمجموعه ثبت شد.", "تعریف بازارها",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RenameNode()
    {
        var node = SelectedNode;
        var title = marketTitleTextBox.Text.Trim();

        if (node == null || string.IsNullOrWhiteSpace(title) || title == node.Title)
            return;

        if (data.Nodes.Any(x =>
            x.Id != node.Id &&
            x.ParentId == node.ParentId &&
            string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "در این سطح نام تکراری مجاز نیست.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        node.Title = title;
        MarketStructureStore.Save(data);
        RebuildTree(node.Id);
    }

    private void DeleteNode()
    {
        var node = SelectedNode;
        if (node == null)
            return;

        var descendants = GetDescendants(node.Id);
        var ids = descendants.Select(x => x.Id).Append(node.Id).ToHashSet();

        var assignedSymbols = SymbolDefinitionForm.SymbolDefinitionStore
            .Load()
            .Where(x => ids.Contains(
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.MarketNodeId)))
            .Select(x => SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.SymbolTitle))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (assignedSymbols.Count > 0)
        {
            var preview = string.Join("، ", assignedSymbols.Take(10));
            if (assignedSymbols.Count > 10)
                preview += "، ...";

            MessageBox.Show(
                this,
                $"این گره یا یکی از زیرمجموعه‌های آن به {assignedSymbols.Count} نماد اختصاص داده شده است و حذف آن مجاز نیست.{Environment.NewLine}{Environment.NewLine}" +
                $"نمادها: {preview}",
                "حذف ساختار بازار",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        if (MessageBox.Show(
                this,
                $"این گره و {descendants.Count} زیرمجموعه آن حذف می‌شود. ادامه می‌دهید؟",
                "حذف ساختار بازار",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        data.Nodes.RemoveAll(x => ids.Contains(x.Id));

        RecalculateSortOrders();
        MarketStructureStore.Save(data);
        RebuildTree();
    }

    private List<MarketStructureNode> GetDescendants(string parentId)
    {
        var result = new List<MarketStructureNode>();

        foreach (var child in data.Nodes.Where(x => x.ParentId == parentId).ToList())
        {
            result.Add(child);
            result.AddRange(GetDescendants(child.Id));
        }

        return result;
    }

    private void MoveNode(int direction)
    {
        var node = SelectedNode;
        if (node == null)
            return;

        var siblings = data.Nodes
            .Where(x => x.ParentId == node.ParentId)
            .OrderBy(x => x.SortOrder)
            .ToList();

        var index = siblings.FindIndex(x => x.Id == node.Id);
        var target = index + direction;

        if (index < 0 || target < 0 || target >= siblings.Count)
            return;

        (siblings[index].SortOrder, siblings[target].SortOrder) =
            (siblings[target].SortOrder, siblings[index].SortOrder);

        MarketStructureStore.Save(data);
        RebuildTree(node.Id);
    }

    private void RecalculateSortOrders()
    {
        foreach (var parentGroup in data.Nodes.GroupBy(x => x.ParentId))
        {
            var order = 0;
            foreach (var node in parentGroup.OrderBy(x => x.SortOrder))
                node.SortOrder = order++;
        }
    }

    private void ExportMarketStructure()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "JSON files (*.json)|*.json",
            Title = "خروجی ساختار بازار به JSON",
            FileName = "MarketStructure.json",
            AddExtension = true,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(
            dialog.FileName,
            JsonSerializer.Serialize(data.Nodes, options),
            new System.Text.UTF8Encoding(false));

        MessageBox.Show(this, "ساختار بازار با موفقیت خروجی گرفته شد.", "خروجی ساختار بازار",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ImportMarketStructure()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json",
            Title = "ورود ساختار بازار از JSON",
            CheckFileExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var importedNodes = JsonSerializer.Deserialize<List<MarketStructureNode>>(
                File.ReadAllText(dialog.FileName));

            if (importedNodes == null)
                throw new InvalidDataException("فایل JSON معتبر نیست.");

            data.Nodes = importedNodes;
            MarketStructureStore.Save(data);
            LoadData();

            MessageBox.Show(this, "ساختار بازار با موفقیت وارد شد.", "ورود ساختار بازار",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"ورود ساختار بازار انجام نشد.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                "خطا در ورود ساختار بازار", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
