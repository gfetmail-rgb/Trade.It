namespace Trade.It;

public sealed class MarketStructureForm : Form
{
    private readonly TreeView marketTreeView = new();
    private readonly ListBox categoryListBox = new();
    private readonly Button addChildButton = new();
    private readonly Button renameButton = new();
    private readonly Button deleteButton = new();
    private readonly Button moveUpButton = new();
    private readonly Button moveDownButton = new();
    private readonly TextBox categoryTextBox = new();
    private readonly Button addCategoryButton = new();
    private readonly Button renameCategoryButton = new();
    private readonly Button deleteCategoryButton = new();
    private readonly Button closeButton = new();

    private MarketStructureData data = null!;

    public MarketStructureForm()
    {
        InitializeUi();
        LoadData();
    }

    private void InitializeUi()
    {
        Text = "تعریف ساختار بازارها";
        StartPosition = FormStartPosition.CenterParent;
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        Width = 1050;
        Height = 700;
        MinimumSize = new Size(850, 550);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        var marketPage = new TabPage("ساختار بازارها");
        var categoryPage = new TabPage("انواع دارایی / ابزار");

        marketTreeView.Dock = DockStyle.Fill;
        marketTreeView.HideSelection = false;
        marketTreeView.AfterSelect += (_, _) => UpdateMarketButtons();

        var marketButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 58,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(8)
        };

        ConfigureButton(addChildButton, "افزودن زیرمجموعه");
        ConfigureButton(renameButton, "تغییر نام");
        ConfigureButton(deleteButton, "حذف");
        ConfigureButton(moveUpButton, "انتقال به بالا");
        ConfigureButton(moveDownButton, "انتقال به پایین");

        addChildButton.Click += (_, _) => AddChild();
        renameButton.Click += (_, _) => RenameNode();
        deleteButton.Click += (_, _) => DeleteNode();
        moveUpButton.Click += (_, _) => MoveNode(-1);
        moveDownButton.Click += (_, _) => MoveNode(1);

        marketButtons.Controls.AddRange(new Control[]
        {
            addChildButton, renameButton, deleteButton, moveUpButton, moveDownButton
        });

        marketPage.Controls.Add(marketTreeView);
        marketPage.Controls.Add(marketButtons);

        var categoryLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(12)
        };
        categoryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
        categoryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));

        categoryListBox.Dock = DockStyle.Fill;
        categoryListBox.SelectedIndexChanged += (_, _) => categoryTextBox.Text =
            categoryListBox.SelectedItem?.ToString() ?? "";

        categoryTextBox.Dock = DockStyle.Fill;

        var categoryButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(0, 5, 0, 0)
        };

        ConfigureButton(addCategoryButton, "افزودن");
        ConfigureButton(renameCategoryButton, "تغییر نام");
        ConfigureButton(deleteCategoryButton, "حذف");

        addCategoryButton.Click += (_, _) => AddCategory();
        renameCategoryButton.Click += (_, _) => RenameCategory();
        deleteCategoryButton.Click += (_, _) => DeleteCategory();

        categoryButtons.Controls.AddRange(new Control[]
        {
            addCategoryButton, renameCategoryButton, deleteCategoryButton
        });

        categoryLayout.Controls.Add(categoryListBox, 0, 0);
        categoryLayout.SetColumnSpan(categoryListBox, 2);
        categoryLayout.Controls.Add(categoryTextBox, 0, 1);
        categoryLayout.SetColumnSpan(categoryTextBox, 2);
        categoryLayout.Controls.Add(categoryButtons, 0, 2);
        categoryLayout.SetColumnSpan(categoryButtons, 2);

        categoryPage.Controls.Add(categoryLayout);

        tabs.TabPages.Add(marketPage);
        tabs.TabPages.Add(categoryPage);

        var bottom = new Panel { Dock = DockStyle.Bottom, Height = 55 };
        ConfigureButton(closeButton, "بستن");
        closeButton.Dock = DockStyle.Right;
        closeButton.Width = 110;
        closeButton.Click += (_, _) => Close();
        bottom.Controls.Add(closeButton);

        Controls.Add(tabs);
        Controls.Add(bottom);
    }

    private static void ConfigureButton(Button button, string text)
    {
        button.Text = text;
        button.AutoSize = true;
        button.Height = 38;
        button.Padding = new Padding(10, 0, 10, 0);
    }

    private void LoadData()
    {
        data = MarketStructureStore.Load();
        RebuildTree();
        categoryListBox.Items.Clear();
        foreach (var category in data.AssetCategories)
            categoryListBox.Items.Add(category);
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
                var treeNode = BuildTreeNode(node);
                marketTreeView.Nodes.Add(treeNode);
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
    }

    private void AddChild()
    {
        var parent = SelectedNode;
        if (parent == null) return;

        var title = Prompt("عنوان زیرمجموعه جدید را وارد کنید:", "افزودن زیرمجموعه");
        if (string.IsNullOrWhiteSpace(title)) return;

        title = title.Trim();

        if (data.Nodes.Any(x =>
            x.ParentId == parent.Id &&
            string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "این زیرمجموعه قبلاً وجود دارد.", "تعریف بازارها",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        data.Nodes.Add(new MarketStructureNode
        {
            ParentId = parent.Id,
            Title = title,
            SortOrder = data.Nodes.Count(x => x.ParentId == parent.Id)
        });

        MarketStructureStore.Save(data);
        RebuildTree(parent.Id);
    }

    private void RenameNode()
    {
        var node = SelectedNode;
        if (node == null) return;

        var title = Prompt("نام جدید را وارد کنید:", "تغییر نام", node.Title);
        if (string.IsNullOrWhiteSpace(title) || title.Trim() == node.Title)
            return;

        title = title.Trim();

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
        if (node == null) return;

        var descendants = GetDescendants(node.Id);
        var count = descendants.Count + 1;

        if (MessageBox.Show(
                this,
                $"این گره و {descendants.Count} زیرمجموعه آن حذف می‌شود. ادامه می‌دهید؟",
                "حذف ساختار بازار",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        var ids = descendants.Select(x => x.Id).Append(node.Id).ToHashSet();
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
        if (node == null) return;

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

        if (data.AssetCategories.Any(x => string.Equals(x, title, StringComparison.OrdinalIgnoreCase)))
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
        if (categoryListBox.SelectedIndex < 0) return;

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
        data.AssetCategories[index] = newValue;
        MarketStructureStore.Save(data);
        LoadData();
        categoryListBox.SelectedIndex = index;
    }

    private void DeleteCategory()
    {
        if (categoryListBox.SelectedIndex < 0) return;

        var value = categoryListBox.SelectedItem?.ToString() ?? "";

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

    private static string? Prompt(string message, string title, string defaultValue = "")
    {
        using var form = new Form
        {
            Width = 480,
            Height = 170,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            RightToLeft = RightToLeft.Yes,
            RightToLeftLayout = true,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var label = new Label
        {
            Text = message,
            Dock = DockStyle.Top,
            Height = 45,
            Padding = new Padding(10)
        };

        var textBox = new TextBox
        {
            Text = defaultValue,
            Dock = DockStyle.Top,
            Height = 32,
            Margin = new Padding(10)
        };

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 48,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(8)
        };

        var ok = new Button { Text = "تأیید", DialogResult = DialogResult.OK, Width = 90 };
        var cancel = new Button { Text = "انصراف", DialogResult = DialogResult.Cancel, Width = 90 };

        panel.Controls.Add(ok);
        panel.Controls.Add(cancel);
        form.Controls.Add(textBox);
        form.Controls.Add(label);
        form.Controls.Add(panel);
        form.AcceptButton = ok;
        form.CancelButton = cancel;

        return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;
    }
}
