namespace Trade.It;

partial class MarketStructureForm
{
    private System.ComponentModel.IContainer? components = null;
    private TabControl tabs;
    private TabPage marketPage;
    private TabPage categoryPage;
    private TabPage otherPage;
    private TreeView marketTreeView;
    private FlowLayoutPanel marketButtons;
    private TextBox marketTitleTextBox;
    private Button addChildButton;
    private Button renameButton;
    private Button deleteButton;
    private Button moveUpButton;
    private Button moveDownButton;
    private ListBox categoryListBox;
    private TextBox categoryTextBox;
    private FlowLayoutPanel categoryButtons;
    private Button addCategoryButton;
    private Button renameCategoryButton;
    private Button deleteCategoryButton;
    private Button importCategoryButton;
    private Button exportCategoryButton;
    private ListBox otherCategoryListBox;
    private TextBox otherCategoryTextBox;
    private FlowLayoutPanel otherCategoryButtons;
    private Button addOtherCategoryButton;
    private Button renameOtherCategoryButton;
    private Button deleteOtherCategoryButton;
    private Button importOtherCategoryButton;
    private Button exportOtherCategoryButton;
    private Panel bottomPanel;
    private Button closeButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabs = new TabControl();
        marketPage = new TabPage();
        marketTreeView = new TreeView();
        marketTitleTextBox = new TextBox();
        marketButtons = new FlowLayoutPanel();
        moveDownButton = new Button();
        moveUpButton = new Button();
        deleteButton = new Button();
        renameButton = new Button();
        addChildButton = new Button();
        categoryPage = new TabPage();
        categoryListBox = new ListBox();
        categoryTextBox = new TextBox();
        categoryButtons = new FlowLayoutPanel();
        exportCategoryButton = new Button();
        importCategoryButton = new Button();
        deleteCategoryButton = new Button();
        renameCategoryButton = new Button();
        addCategoryButton = new Button();
        otherPage = new TabPage();
        otherCategoryListBox = new ListBox();
        otherCategoryTextBox = new TextBox();
        otherCategoryButtons = new FlowLayoutPanel();
        exportOtherCategoryButton = new Button();
        importOtherCategoryButton = new Button();
        deleteOtherCategoryButton = new Button();
        renameOtherCategoryButton = new Button();
        addOtherCategoryButton = new Button();
        bottomPanel = new Panel();
        closeButton = new Button();
        label2 = new Label();
        label1 = new Label();
        tabs.SuspendLayout();
        marketPage.SuspendLayout();
        marketButtons.SuspendLayout();
        categoryPage.SuspendLayout();
        categoryButtons.SuspendLayout();
        otherPage.SuspendLayout();
        otherCategoryButtons.SuspendLayout();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        // 
        // tabs
        // 
        tabs.Controls.Add(marketPage);
        tabs.Controls.Add(categoryPage);
        tabs.Controls.Add(otherPage);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 0);
        tabs.Margin = new Padding(4, 5, 4, 5);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.Size = new Size(1500, 1075);
        tabs.TabIndex = 0;
        // 
        // marketPage
        // 
        marketPage.Controls.Add(marketTreeView);
        marketPage.Controls.Add(marketTitleTextBox);
        marketPage.Controls.Add(marketButtons);
        marketPage.Location = new Point(4, 34);
        marketPage.Margin = new Padding(4, 5, 4, 5);
        marketPage.Name = "marketPage";
        marketPage.Padding = new Padding(11, 13, 11, 13);
        marketPage.Size = new Size(1492, 1037);
        marketPage.TabIndex = 0;
        marketPage.Text = "ساختار بازارها";
        marketPage.UseVisualStyleBackColor = true;
        // 
        // marketTreeView
        // 
        marketTreeView.Dock = DockStyle.Fill;
        marketTreeView.HideSelection = false;
        marketTreeView.Location = new Point(11, 13);
        marketTreeView.Margin = new Padding(4, 5, 4, 5);
        marketTreeView.Name = "marketTreeView";
        marketTreeView.RightToLeft = RightToLeft.Yes;
        marketTreeView.Size = new Size(1470, 893);
        marketTreeView.TabIndex = 0;
        // 
        // marketTitleTextBox
        // 
        marketTitleTextBox.Dock = DockStyle.Bottom;
        marketTitleTextBox.Location = new Point(11, 906);
        marketTitleTextBox.Margin = new Padding(4, 5, 4, 5);
        marketTitleTextBox.Name = "marketTitleTextBox";
        marketTitleTextBox.PlaceholderText = "نام بازار / زیرمجموعه";
        marketTitleTextBox.Size = new Size(1470, 31);
        marketTitleTextBox.TabIndex = 1;
        // 
        // marketButtons
        // 
        marketButtons.Controls.Add(moveDownButton);
        marketButtons.Controls.Add(moveUpButton);
        marketButtons.Controls.Add(deleteButton);
        marketButtons.Controls.Add(renameButton);
        marketButtons.Controls.Add(addChildButton);
        marketButtons.Dock = DockStyle.Bottom;
        marketButtons.FlowDirection = FlowDirection.RightToLeft;
        marketButtons.Location = new Point(11, 937);
        marketButtons.Margin = new Padding(4, 5, 4, 5);
        marketButtons.Name = "marketButtons";
        marketButtons.Padding = new Padding(0, 12, 0, 0);
        marketButtons.Size = new Size(1470, 87);
        marketButtons.TabIndex = 2;
        marketButtons.WrapContents = false;
        // 
        // moveDownButton
        // 
        moveDownButton.Location = new Point(4, 17);
        moveDownButton.Margin = new Padding(4, 5, 4, 5);
        moveDownButton.Name = "moveDownButton";
        moveDownButton.Size = new Size(107, 38);
        moveDownButton.TabIndex = 0;
        // 
        // moveUpButton
        // 
        moveUpButton.Location = new Point(119, 17);
        moveUpButton.Margin = new Padding(4, 5, 4, 5);
        moveUpButton.Name = "moveUpButton";
        moveUpButton.Size = new Size(107, 38);
        moveUpButton.TabIndex = 1;
        // 
        // deleteButton
        // 
        deleteButton.Location = new Point(234, 17);
        deleteButton.Margin = new Padding(4, 5, 4, 5);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(107, 38);
        deleteButton.TabIndex = 2;
        // 
        // renameButton
        // 
        renameButton.Location = new Point(349, 17);
        renameButton.Margin = new Padding(4, 5, 4, 5);
        renameButton.Name = "renameButton";
        renameButton.Size = new Size(107, 38);
        renameButton.TabIndex = 3;
        // 
        // addChildButton
        // 
        addChildButton.Location = new Point(464, 17);
        addChildButton.Margin = new Padding(4, 5, 4, 5);
        addChildButton.Name = "addChildButton";
        addChildButton.Size = new Size(107, 38);
        addChildButton.TabIndex = 4;
        // 
        // categoryPage
        // 
        categoryPage.Controls.Add(categoryListBox);
        categoryPage.Controls.Add(categoryTextBox);
        categoryPage.Controls.Add(categoryButtons);
        categoryPage.Location = new Point(4, 34);
        categoryPage.Margin = new Padding(4, 5, 4, 5);
        categoryPage.Name = "categoryPage";
        categoryPage.Padding = new Padding(17, 20, 17, 20);
        categoryPage.Size = new Size(1492, 1037);
        categoryPage.TabIndex = 1;
        categoryPage.Text = "انواع دارایی / ابزار";
        categoryPage.UseVisualStyleBackColor = true;
        // 
        // categoryListBox
        // 
        categoryListBox.Dock = DockStyle.Fill;
        categoryListBox.ItemHeight = 25;
        categoryListBox.Location = new Point(17, 20);
        categoryListBox.Margin = new Padding(4, 5, 4, 5);
        categoryListBox.Name = "categoryListBox";
        categoryListBox.RightToLeft = RightToLeft.Yes;
        categoryListBox.SelectionMode = SelectionMode.MultiExtended;
        categoryListBox.Size = new Size(1458, 879);
        categoryListBox.Sorted = true;
        categoryListBox.TabIndex = 0;
        // 
        // categoryTextBox
        // 
        categoryTextBox.Dock = DockStyle.Bottom;
        categoryTextBox.Location = new Point(17, 899);
        categoryTextBox.Margin = new Padding(4, 5, 4, 5);
        categoryTextBox.Name = "categoryTextBox";
        categoryTextBox.PlaceholderText = "نام نوع دارایی / ابزار";
        categoryTextBox.Size = new Size(1458, 31);
        categoryTextBox.TabIndex = 1;
        // 
        // categoryButtons
        // 
        categoryButtons.Controls.Add(exportCategoryButton);
        categoryButtons.Controls.Add(importCategoryButton);
        categoryButtons.Controls.Add(deleteCategoryButton);
        categoryButtons.Controls.Add(renameCategoryButton);
        categoryButtons.Controls.Add(addCategoryButton);
        categoryButtons.Controls.Add(label2);
        categoryButtons.Dock = DockStyle.Bottom;
        categoryButtons.FlowDirection = FlowDirection.RightToLeft;
        categoryButtons.Location = new Point(17, 930);
        categoryButtons.Margin = new Padding(4, 5, 4, 5);
        categoryButtons.Name = "categoryButtons";
        categoryButtons.Padding = new Padding(0, 12, 0, 0);
        categoryButtons.Size = new Size(1458, 87);
        categoryButtons.TabIndex = 2;
        categoryButtons.WrapContents = false;
        // 
        // exportCategoryButton
        // 
        exportCategoryButton.Location = new Point(4, 17);
        exportCategoryButton.Margin = new Padding(4, 5, 4, 5);
        exportCategoryButton.Name = "exportCategoryButton";
        exportCategoryButton.Size = new Size(107, 38);
        exportCategoryButton.TabIndex = 0;
        // 
        // importCategoryButton
        // 
        importCategoryButton.Location = new Point(119, 17);
        importCategoryButton.Margin = new Padding(4, 5, 4, 5);
        importCategoryButton.Name = "importCategoryButton";
        importCategoryButton.Size = new Size(107, 38);
        importCategoryButton.TabIndex = 1;
        // 
        // deleteCategoryButton
        // 
        deleteCategoryButton.Location = new Point(234, 17);
        deleteCategoryButton.Margin = new Padding(4, 5, 4, 5);
        deleteCategoryButton.Name = "deleteCategoryButton";
        deleteCategoryButton.Size = new Size(107, 38);
        deleteCategoryButton.TabIndex = 2;
        // 
        // renameCategoryButton
        // 
        renameCategoryButton.Location = new Point(349, 17);
        renameCategoryButton.Margin = new Padding(4, 5, 4, 5);
        renameCategoryButton.Name = "renameCategoryButton";
        renameCategoryButton.Size = new Size(107, 38);
        renameCategoryButton.TabIndex = 3;
        // 
        // addCategoryButton
        // 
        addCategoryButton.Location = new Point(464, 17);
        addCategoryButton.Margin = new Padding(4, 5, 4, 5);
        addCategoryButton.Name = "addCategoryButton";
        addCategoryButton.Size = new Size(107, 38);
        addCategoryButton.TabIndex = 4;
        // 
        // otherPage
        // 
        otherPage.Controls.Add(otherCategoryListBox);
        otherPage.Controls.Add(otherCategoryTextBox);
        otherPage.Controls.Add(otherCategoryButtons);
        otherPage.Location = new Point(4, 34);
        otherPage.Margin = new Padding(4, 5, 4, 5);
        otherPage.Name = "otherPage";
        otherPage.Padding = new Padding(17, 20, 17, 20);
        otherPage.Size = new Size(1492, 1037);
        otherPage.TabIndex = 2;
        otherPage.Text = "سایر موارد";
        otherPage.UseVisualStyleBackColor = true;
        // 
        // otherCategoryListBox
        // 
        otherCategoryListBox.Dock = DockStyle.Fill;
        otherCategoryListBox.ItemHeight = 25;
        otherCategoryListBox.Location = new Point(17, 20);
        otherCategoryListBox.Margin = new Padding(4, 5, 4, 5);
        otherCategoryListBox.Name = "otherCategoryListBox";
        otherCategoryListBox.RightToLeft = RightToLeft.Yes;
        otherCategoryListBox.SelectionMode = SelectionMode.MultiExtended;
        otherCategoryListBox.Size = new Size(1458, 879);
        otherCategoryListBox.Sorted = true;
        otherCategoryListBox.TabIndex = 0;
        // 
        // otherCategoryTextBox
        // 
        otherCategoryTextBox.Dock = DockStyle.Bottom;
        otherCategoryTextBox.Location = new Point(17, 899);
        otherCategoryTextBox.Margin = new Padding(4, 5, 4, 5);
        otherCategoryTextBox.Name = "otherCategoryTextBox";
        otherCategoryTextBox.PlaceholderText = "مقدار سایر موارد";
        otherCategoryTextBox.Size = new Size(1458, 31);
        otherCategoryTextBox.TabIndex = 1;
        // 
        // otherCategoryButtons
        // 
        otherCategoryButtons.Controls.Add(exportOtherCategoryButton);
        otherCategoryButtons.Controls.Add(importOtherCategoryButton);
        otherCategoryButtons.Controls.Add(deleteOtherCategoryButton);
        otherCategoryButtons.Controls.Add(renameOtherCategoryButton);
        otherCategoryButtons.Controls.Add(addOtherCategoryButton);
        otherCategoryButtons.Controls.Add(label1);
        otherCategoryButtons.Dock = DockStyle.Bottom;
        otherCategoryButtons.FlowDirection = FlowDirection.RightToLeft;
        otherCategoryButtons.Location = new Point(17, 930);
        otherCategoryButtons.Margin = new Padding(4, 5, 4, 5);
        otherCategoryButtons.Name = "otherCategoryButtons";
        otherCategoryButtons.Padding = new Padding(0, 12, 0, 0);
        otherCategoryButtons.Size = new Size(1458, 87);
        otherCategoryButtons.TabIndex = 2;
        otherCategoryButtons.WrapContents = false;
        // 
        // exportOtherCategoryButton
        // 
        exportOtherCategoryButton.Location = new Point(4, 17);
        exportOtherCategoryButton.Margin = new Padding(4, 5, 4, 5);
        exportOtherCategoryButton.Name = "exportOtherCategoryButton";
        exportOtherCategoryButton.Size = new Size(107, 38);
        exportOtherCategoryButton.TabIndex = 0;
        // 
        // importOtherCategoryButton
        // 
        importOtherCategoryButton.Location = new Point(119, 17);
        importOtherCategoryButton.Margin = new Padding(4, 5, 4, 5);
        importOtherCategoryButton.Name = "importOtherCategoryButton";
        importOtherCategoryButton.Size = new Size(107, 38);
        importOtherCategoryButton.TabIndex = 1;
        // 
        // deleteOtherCategoryButton
        // 
        deleteOtherCategoryButton.Location = new Point(234, 17);
        deleteOtherCategoryButton.Margin = new Padding(4, 5, 4, 5);
        deleteOtherCategoryButton.Name = "deleteOtherCategoryButton";
        deleteOtherCategoryButton.Size = new Size(107, 38);
        deleteOtherCategoryButton.TabIndex = 2;
        // 
        // renameOtherCategoryButton
        // 
        renameOtherCategoryButton.Location = new Point(349, 17);
        renameOtherCategoryButton.Margin = new Padding(4, 5, 4, 5);
        renameOtherCategoryButton.Name = "renameOtherCategoryButton";
        renameOtherCategoryButton.Size = new Size(107, 38);
        renameOtherCategoryButton.TabIndex = 3;
        // 
        // addOtherCategoryButton
        // 
        addOtherCategoryButton.Location = new Point(464, 17);
        addOtherCategoryButton.Margin = new Padding(4, 5, 4, 5);
        addOtherCategoryButton.Name = "addOtherCategoryButton";
        addOtherCategoryButton.Size = new Size(107, 38);
        addOtherCategoryButton.TabIndex = 4;
        // 
        // bottomPanel
        // 
        bottomPanel.Controls.Add(closeButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new Point(0, 1075);
        bottomPanel.Margin = new Padding(4, 5, 4, 5);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(1500, 92);
        bottomPanel.TabIndex = 1;
        // 
        // closeButton
        // 
        closeButton.Dock = DockStyle.Right;
        closeButton.Location = new Point(1343, 0);
        closeButton.Margin = new Padding(4, 5, 4, 5);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(157, 92);
        closeButton.TabIndex = 0;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label2.Location = new Point(578, 12);
        label2.Name = "label2";
        label2.Size = new Size(877, 58);
        label2.TabIndex = 6;
        label2.Text = "اکسل باید فاقد هدر باشد. غیر تکراری ها (نسبت به مقادیر موجود) وارد خواهند شد و تکراری ها وارد نمی شوند";
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label1.Location = new Point(578, 12);
        label1.Name = "label1";
        label1.Size = new Size(877, 58);
        label1.TabIndex = 7;
        label1.Text = "اکسل باید فاقد هدر باشد. غیر تکراری ها (نسبت به مقادیر موجود) وارد خواهند شد و تکراری ها وارد نمی شوند";
        // 
        // MarketStructureForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1500, 1167);
        Controls.Add(tabs);
        Controls.Add(bottomPanel);
        Margin = new Padding(4, 5, 4, 5);
        MinimumSize = new Size(1205, 879);
        Name = "MarketStructureForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "تعریف ساختار بازارها";
        tabs.ResumeLayout(false);
        marketPage.ResumeLayout(false);
        marketPage.PerformLayout();
        marketButtons.ResumeLayout(false);
        categoryPage.ResumeLayout(false);
        categoryPage.PerformLayout();
        categoryButtons.ResumeLayout(false);
        otherPage.ResumeLayout(false);
        otherPage.PerformLayout();
        otherCategoryButtons.ResumeLayout(false);
        bottomPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureDesignerButton(Button button, string text)
    {
        button.AutoSize = true;
        button.Height = 38;
        button.Padding = new Padding(10, 0, 10, 0);
        button.Text = text;
        button.UseVisualStyleBackColor = true;
    }
    private Label label2;
    private Label label1;
}
