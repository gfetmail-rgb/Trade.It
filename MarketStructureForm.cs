using ClosedXML.Excel;

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
            categoryTextBox.Text = categoryListBox.SelectedItems.Count == 1
                ? categoryListBox.SelectedItem?.ToString() ?? ""
                : "";

        otherCategoryListBox.SelectedIndexChanged += (_, _) =>
            otherCategoryTextBox.Text = otherCategoryListBox.SelectedItems.Count == 1
                ? otherCategoryListBox.SelectedItem?.ToString() ?? ""
                : "";

        addChildButton.Click += (_, _) => AddChild();
        renameButton.Click += (_, _) => RenameNode();
        deleteButton.Click += (_, _) => DeleteNode();
        moveUpButton.Click += (_, _) => MoveNode(-1);
        moveDownButton.Click += (_, _) => MoveNode(1);

        addCategoryButton.Click += (_, _) => AddCategory();
        renameCategoryButton.Click += (_, _) => RenameCategory();
        deleteCategoryButton.Click += (_, _) => DeleteCategory();
        importCategoryButton.Click += (_, _) => ImportCategoriesFromExcel();
        exportCategoryButton.Click += (_, _) => ExportCategoriesToExcel();
        addOtherCategoryButton.Click += (_, _) => AddOtherCategory();
        renameOtherCategoryButton.Click += (_, _) => RenameOtherCategory();
        deleteOtherCategoryButton.Click += (_, _) => DeleteOtherCategory();
        importOtherCategoryButton.Click += (_, _) => ImportOtherCategoriesFromExcel();
        exportOtherCategoryButton.Click += (_, _) => ExportOtherCategoriesToExcel();
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
        SortCategoryLists();
        RebuildTree();

        categoryListBox.Items.Clear();
        foreach (var category in data.AssetCategories)
            categoryListBox.Items.Add(category);

        categoryTextBox.Clear();
        otherCategoryTextBox.Clear();
        marketTitleTextBox.Clear();

        otherCategoryListBox.Items.Clear();
        foreach (var item in data.OtherCategories)
            otherCategoryListBox.Items.Add(item);

        UpdateMarketButtons();
    }

    private void SortCategoryLists()
    {
        data.AssetCategories = data.AssetCategories
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        data.OtherCategories = data.OtherCategories
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        MarketStructureStore.Save(data);
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
        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();

        categoryListBox.SelectedItem = title;
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

        var normalizedOldValue =
            SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(oldValue);

        var symbolDefinitions = SymbolDefinitionForm.SymbolDefinitionStore.Load();

        foreach (var symbol in symbolDefinitions)
        {
            var category =
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(symbol.AssetCategory);

            var assetType =
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(symbol.AssetType);

            if (string.Equals(category, normalizedOldValue, StringComparison.OrdinalIgnoreCase))
                symbol.AssetCategory = newValue;

            if (string.Equals(assetType, normalizedOldValue, StringComparison.OrdinalIgnoreCase))
                symbol.AssetType = newValue;
        }

        data.AssetCategories.Remove(oldValue);
        data.AssetCategories.Add(newValue);
        SortCategoryLists();
        MarketStructureStore.Save(data);

        SymbolDefinitionForm.SymbolDefinitionStore.Save(symbolDefinitions);

        LoadData();

        categoryListBox.SelectedItem = newValue;
    }

    private void AddOtherCategory()
    {
        var title = otherCategoryTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            MessageBox.Show(this, "مقدار «سایر موارد» را وارد کنید.", "سایر موارد",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (data.OtherCategories.Any(x =>
            string.Equals(x, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این مقدار «سایر موارد» قبلاً ثبت شده است.", "سایر موارد",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        data.OtherCategories.Add(title);
        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();
        otherCategoryListBox.SelectedItem = title;
    }

    private void ImportCategoriesFromExcel()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "ورود انواع دارایی / ابزار از Excel",
            CheckFileExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        var imported = ReadFirstColumnFromExcel(dialog.FileName, "انواع دارایی / ابزار");
        var added = 0;

        foreach (var value in imported)
        {
            if (data.AssetCategories.Any(x => string.Equals(x, value, StringComparison.CurrentCultureIgnoreCase)))
                continue;

            data.AssetCategories.Add(value);
            added++;
        }

        if (added == 0)
        {
            MessageBox.Show(this, "مورد جدیدی برای افزودن پیدا نشد.", "ورود از Excel",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();

        MessageBox.Show(this, $"{added} مورد جدید وارد شد.", "ورود از Excel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ExportCategoriesToExcel()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "خروجی انواع دارایی / ابزار به Excel",
            FileName = "انواع دارایی و ابزار.xlsx",
            AddExtension = true,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        ExportFirstColumnToExcel(dialog.FileName, "انواع دارایی / ابزار", data.AssetCategories);
        MessageBox.Show(this, "خروجی Excel با موفقیت ایجاد شد.", "خروجی به Excel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ImportOtherCategoriesFromExcel()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "ورود سایر موارد از Excel",
            CheckFileExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        var imported = ReadFirstColumnFromExcel(dialog.FileName, "سایر موارد");
        var added = 0;

        foreach (var value in imported)
        {
            if (data.OtherCategories.Any(x => string.Equals(x, value, StringComparison.CurrentCultureIgnoreCase)))
                continue;

            data.OtherCategories.Add(value);
            added++;
        }

        if (added == 0)
        {
            MessageBox.Show(this, "مورد جدیدی برای افزودن پیدا نشد.", "ورود از Excel",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();

        MessageBox.Show(this, $"{added} مورد جدید وارد شد.", "ورود از Excel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ExportOtherCategoriesToExcel()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "خروجی سایر موارد به Excel",
            FileName = "سایر موارد.xlsx",
            AddExtension = true,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        ExportFirstColumnToExcel(dialog.FileName, "سایر موارد", data.OtherCategories);
        MessageBox.Show(this, "خروجی Excel با موفقیت ایجاد شد.", "خروجی به Excel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static List<string> ReadFirstColumnFromExcel(string fileName, string header)
    {
        using var workbook = new XLWorkbook(fileName);
        var worksheet = workbook.Worksheets.FirstOrDefault();
        if (worksheet == null)
            return new List<string>();

        var result = new List<string>();
        var first = true;

        foreach (var row in worksheet.RowsUsed())
        {
            var value = row.Cell(1).GetString().Trim();
            if (string.IsNullOrWhiteSpace(value))
                continue;

            if (first && string.Equals(value, header, StringComparison.CurrentCultureIgnoreCase))
            {
                first = false;
                continue;
            }

            first = false;
            if (!result.Contains(value, StringComparer.CurrentCultureIgnoreCase))
                result.Add(value);
        }

        return result;
    }

    private static void ExportFirstColumnToExcel(string fileName, string header, IEnumerable<string> values)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("لیست");

        worksheet.Cell(1, 1).Value = header;
        var row = 2;
        foreach (var value in values)
        {
            worksheet.Cell(row++, 1).Value = value;
        }

        worksheet.Column(1).AdjustToContents();
        workbook.SaveAs(fileName);
    }

    private void RenameOtherCategory()
    {
        if (otherCategoryListBox.SelectedIndex < 0)
            return;

        var oldValue = otherCategoryListBox.SelectedItem?.ToString() ?? "";
        var newValue = otherCategoryTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(newValue) || newValue == oldValue)
            return;

        if (data.OtherCategories.Any(x =>
            !string.Equals(x, oldValue, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x, newValue, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این مقدار «سایر موارد» قبلاً ثبت شده است.", "سایر موارد",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var normalizedOldValue =
            SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(oldValue);

        var symbolDefinitions = SymbolDefinitionForm.SymbolDefinitionStore.Load();

        foreach (var symbol in symbolDefinitions)
        {
            var otherItem =
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(symbol.OtherItem);

            if (string.Equals(otherItem, normalizedOldValue, StringComparison.OrdinalIgnoreCase))
                symbol.OtherItem = newValue;
        }

        data.OtherCategories.Remove(oldValue);
        data.OtherCategories.Add(newValue);
        SortCategoryLists();
        MarketStructureStore.Save(data);
        SymbolDefinitionForm.SymbolDefinitionStore.Save(symbolDefinitions);

        LoadData();
        otherCategoryListBox.SelectedItem = newValue;
    }

    private void DeleteOtherCategory()
    {
        var selectedValues = otherCategoryListBox.SelectedItems
            .Cast<object>()
            .Select(x => x?.ToString()?.Trim() ?? "")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (selectedValues.Count == 0)
            return;

        var normalizedValues = selectedValues
            .Select(SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var assignedSymbols = SymbolDefinitionForm.SymbolDefinitionStore
            .Load()
            .Where(x => normalizedValues.Contains(
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.OtherItem)))
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
                $"حداقل یکی از موارد انتخاب‌شده به نماد اختصاص داده شده است و حذف هیچ‌یک از موارد انتخاب‌شده انجام نشد.{Environment.NewLine}{Environment.NewLine}" +
                $"نمادها: {preview}",
                "حذف سایر موارد",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        var previewValues = string.Join("، ", selectedValues.Take(10));
        if (selectedValues.Count > 10)
            previewValues += "، ...";

        if (MessageBox.Show(
                this,
                $"{selectedValues.Count} مورد انتخاب شده است و حذف خواهد شد.{Environment.NewLine}{Environment.NewLine}" +
                $"موارد: {previewValues}{Environment.NewLine}{Environment.NewLine}ادامه می‌دهید؟",
                "حذف سایر موارد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        data.OtherCategories.RemoveAll(x =>
            normalizedValues.Contains(
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x)));

        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();
    }

    private void DeleteCategory()
    {
        var selectedValues = categoryListBox.SelectedItems
            .Cast<object>()
            .Select(x => x?.ToString()?.Trim() ?? "")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (selectedValues.Count == 0)
            return;

        var normalizedValues = selectedValues
            .Select(SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var assignedSymbols = SymbolDefinitionForm.SymbolDefinitionStore
            .Load()
            .Where(x =>
                normalizedValues.Contains(
                    SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.AssetCategory)) ||
                normalizedValues.Contains(
                    SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x.AssetType)))
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
                $"حداقل یکی از موارد انتخاب‌شده به نماد اختصاص داده شده است و حذف هیچ‌یک از موارد انتخاب‌شده انجام نشد.{Environment.NewLine}{Environment.NewLine}" +
                $"نمادها: {preview}",
                "حذف انواع دارایی",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        var previewValues = string.Join("، ", selectedValues.Take(10));
        if (selectedValues.Count > 10)
            previewValues += "، ...";

        if (MessageBox.Show(
                this,
                $"{selectedValues.Count} مورد انتخاب شده است و حذف خواهد شد.{Environment.NewLine}{Environment.NewLine}" +
                $"موارد: {previewValues}{Environment.NewLine}{Environment.NewLine}ادامه می‌دهید؟",
                "حذف انواع دارایی",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        data.AssetCategories.RemoveAll(x =>
            normalizedValues.Contains(
                SymbolDefinitionForm.SymbolDefinitionRules.NormalizeText(x)));

        SortCategoryLists();
        MarketStructureStore.Save(data);
        LoadData();
    }
}
