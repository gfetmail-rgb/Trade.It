namespace Trade.It;

public sealed partial class MarketStructureForm : Form
{
    private MarketStructureData data = null!;

    public MarketStructureForm()
    {
        InitializeComponent();

        marketTreeView.AfterSelect += (_, _) => UpdateMarketButtons();
        marketTitleTextBox.KeyDown += MarketTitleTextBox_KeyDown;

        categoryListBox.SelectedIndexChanged += (_, _) =>
            categoryTextBox.Text = categoryListBox.SelectedItem?.ToString() ?? "";

        addChildButton.Click += (_, _) => AddChild();
        renameButton.Click += (_, _) => RenameNode();
        deleteButton.Click += (_, _) => DeleteNode();
        moveUpButton.Click += (_, _) => MoveNode(-1);
        moveDownButton.Click += (_, _) => MoveNode(1);

        addCategoryButton.Click += (_, _) => AddCategory();
        renameCategoryButton.Click += (_, _) => RenameCategory();
        deleteCategoryButton.Click += (_, _) => DeleteCategory();
        closeButton.Click += (_, _) => Close();

        LoadData();
    }

    private void MarketTitleTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
            return;

        if (SelectedNode == null)
            AddRootNode();
        else
            AddChild();

        e.SuppressKeyPress = true;
        e.Handled = true;
    }

    private void LoadData()
    {
        data = MarketStructureStore.Load();
        RebuildTree();

        categoryListBox.Items.Clear();
        foreach (var category in data.AssetCategories)
            categoryListBox.Items.Add(category);

        categoryTextBox.Clear();
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
        addChildButton.Enabled = selected != null;

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
            ParentId = null,
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
        var title = marketTitleTextBox.Text.Trim();

        if (parent == null || string.IsNullOrWhiteSpace(title))
            return;

        if (data.Nodes.Any(x =>
            x.ParentId == parent.Id &&
            string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این زیرمجموعه قبلاً وجود دارد.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var node = new MarketStructureNode
        {
            ParentId = parent.Id,
            Title = title,
            SortOrder = data.Nodes.Count(x => x.ParentId == parent.Id)
        };

        data.Nodes.Add(node);
        MarketStructureStore.Save(data);
        RebuildTree(node.Id);
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

    private void AddCategory()
    {
        var title = categoryTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            MessageBox.Show(this, "نام نوع دارایی/ابزار را وارد کنید.", "انواع دارایی",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (data.AssetCategories.Any(x =>
            string.Equals(x, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این نوع دارایی قبلاً ثبت شده است.", "انواع دارایی",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        data.AssetCategories.Add(title);
        MarketStructureStore.Save(data);
        LoadData();

        categoryListBox.SelectedIndex = categoryListBox.Items.Count - 1;
    }

    private void RenameCategory()
    {
        if (categoryListBox.SelectedIndex < 0)
            return;

        var oldValue = categoryListBox.SelectedItem?.ToString() ?? "";
        var newValue = categoryTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(newValue) || newValue == oldValue)
            return;

        if (data.AssetCategories.Any(x =>
            !string.Equals(x, oldValue, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x, newValue, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این نوع دارایی قبلاً ثبت شده است.", "انواع دارایی",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var index = categoryListBox.SelectedIndex;
        var normalizedOldValue =
            SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(oldValue);
        var normalizedNewValue =
            SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(newValue);

        var symbolDefinitions = SymbolDefinitionForm.SymbolDefinitionStore.Load();
        var changedSymbols = 0;

        foreach (var symbol in symbolDefinitions)
        {
            var category =
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(symbol.AssetCategory);

            var assetType =
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(symbol.AssetType);

            var changed = false;

            if (string.Equals(category, normalizedOldValue, StringComparison.OrdinalIgnoreCase))
            {
                symbol.AssetCategory = newValue;
                changed = true;
            }

            if (string.Equals(assetType, normalizedOldValue, StringComparison.OrdinalIgnoreCase))
            {
                symbol.AssetType = newValue;
                changed = true;
            }

            if (changed)
                changedSymbols++;
        }

        data.AssetCategories[index] = newValue;

        MarketStructureStore.Save(data);

        SymbolDefinitionForm.SymbolDefinitionStore.Save(symbolDefinitions);

        LoadData();

        categoryListBox.SelectedIndex = index;
    }

    private void DeleteCategory()
    {
        if (categoryListBox.SelectedIndex < 0)
            return;

        var value = categoryListBox.SelectedItem?.ToString() ?? "";

        var normalizedValue = SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(value);

        var assignedSymbols = SymbolDefinitionForm.SymbolDefinitionStore
            .Load()
            .Where(x =>
                string.Equals(
                    SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.AssetCategory),
                    normalizedValue,
                    StringComparison.OrdinalIgnoreCase) ||
                string.Equals(
                    SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.AssetType),
                    normalizedValue,
                    StringComparison.OrdinalIgnoreCase))
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
                $"نوع دارایی «{value}» به {assignedSymbols.Count} نماد اختصاص داده شده است و حذف آن مجاز نیست.{Environment.NewLine}{Environment.NewLine}" +
                $"نمادها: {preview}",
                "حذف نوع دارایی",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        if (MessageBox.Show(
                this,
                $"نوع «{value}» حذف شود؟",
                "حذف نوع دارایی",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        data.AssetCategories.RemoveAt(categoryListBox.SelectedIndex);
        MarketStructureStore.Save(data);
        LoadData();
    }
}
