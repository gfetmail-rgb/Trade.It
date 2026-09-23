namespace Trade.It;

partial class MarketStructureForm
{
    private System.ComponentModel.IContainer? components = null;
    private TabControl tabs;
    private TabPage marketPage;
    private TabPage categoryPage;
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
        components = new System.ComponentModel.Container();

        tabs = new TabControl();
        marketPage = new TabPage();
        categoryPage = new TabPage();

        marketTreeView = new TreeView();
        marketButtons = new FlowLayoutPanel();
        marketTitleTextBox = new TextBox();
        addChildButton = new Button();
        renameButton = new Button();
        deleteButton = new Button();
        moveUpButton = new Button();
        moveDownButton = new Button();

        categoryListBox = new ListBox();
        categoryTextBox = new TextBox();
        categoryButtons = new FlowLayoutPanel();
        addCategoryButton = new Button();
        renameCategoryButton = new Button();
        deleteCategoryButton = new Button();

        bottomPanel = new Panel();
        closeButton = new Button();

        SuspendLayout();
        tabs.SuspendLayout();
        marketPage.SuspendLayout();
        categoryPage.SuspendLayout();
        marketButtons.SuspendLayout();
        categoryButtons.SuspendLayout();
        bottomPanel.SuspendLayout();

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1050, 700);
        MinimumSize = new Size(850, 550);
        Name = "MarketStructureForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "تعریف ساختار بازارها";

        // tabs
        tabs.Dock = DockStyle.Fill;
        tabs.Name = "tabs";
        tabs.TabPages.AddRange(new TabPage[] { marketPage, categoryPage });

        // marketPage
        marketPage.Text = "ساختار بازارها";
        marketPage.Padding = new Padding(8);
        marketPage.UseVisualStyleBackColor = true;

        // marketTreeView
        marketTreeView.Dock = DockStyle.Fill;
        marketTreeView.HideSelection = false;
        marketTreeView.Name = "marketTreeView";

        // marketTitleTextBox
        marketTitleTextBox.Dock = DockStyle.Bottom;
        marketTitleTextBox.Height = 32;
        marketTitleTextBox.Name = "marketTitleTextBox";
        marketTitleTextBox.PlaceholderText = "نام بازار / زیرمجموعه";

        // marketButtons
        marketButtons.Dock = DockStyle.Bottom;
        marketButtons.FlowDirection = FlowDirection.RightToLeft;
        marketButtons.Height = 52;
        marketButtons.Name = "marketButtons";
        marketButtons.Padding = new Padding(0, 7, 0, 0);
        marketButtons.WrapContents = false;

        ConfigureDesignerButton(addChildButton, "افزودن زیرمجموعه");
        ConfigureDesignerButton(renameButton, "تغییر نام");
        ConfigureDesignerButton(deleteButton, "حذف");
        ConfigureDesignerButton(moveUpButton, "انتقال به بالا");
        ConfigureDesignerButton(moveDownButton, "انتقال به پایین");

        marketButtons.Controls.Add(moveDownButton);
        marketButtons.Controls.Add(moveUpButton);
        marketButtons.Controls.Add(deleteButton);
        marketButtons.Controls.Add(renameButton);
        marketButtons.Controls.Add(addChildButton);

        marketPage.Controls.Add(marketTreeView);
        marketPage.Controls.Add(marketTitleTextBox);
        marketPage.Controls.Add(marketButtons);

        // categoryPage
        categoryPage.Text = "انواع دارایی / ابزار";
        categoryPage.Padding = new Padding(12);
        categoryPage.UseVisualStyleBackColor = true;

        categoryListBox.Dock = DockStyle.Fill;
        categoryListBox.Name = "categoryListBox";

        categoryTextBox.Dock = DockStyle.Bottom;
        categoryTextBox.Height = 32;
        categoryTextBox.Name = "categoryTextBox";
        categoryTextBox.PlaceholderText = "نام نوع دارایی / ابزار";

        categoryButtons.Dock = DockStyle.Bottom;
        categoryButtons.FlowDirection = FlowDirection.RightToLeft;
        categoryButtons.Height = 52;
        categoryButtons.Name = "categoryButtons";
        categoryButtons.Padding = new Padding(0, 7, 0, 0);
        categoryButtons.WrapContents = false;

        ConfigureDesignerButton(addCategoryButton, "افزودن");
        ConfigureDesignerButton(renameCategoryButton, "تغییر نام");
        ConfigureDesignerButton(deleteCategoryButton, "حذف");

        categoryButtons.Controls.Add(deleteCategoryButton);
        categoryButtons.Controls.Add(renameCategoryButton);
        categoryButtons.Controls.Add(addCategoryButton);

        categoryPage.Controls.Add(categoryListBox);
        categoryPage.Controls.Add(categoryTextBox);
        categoryPage.Controls.Add(categoryButtons);

        // bottomPanel
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Height = 55;
        bottomPanel.Name = "bottomPanel";

        closeButton.Dock = DockStyle.Right;
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(110, 38);
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;

        bottomPanel.Controls.Add(closeButton);

        Controls.Add(tabs);
        Controls.Add(bottomPanel);

        bottomPanel.ResumeLayout(false);
        categoryButtons.ResumeLayout(false);
        categoryPage.ResumeLayout(false);
        categoryPage.PerformLayout();
        marketButtons.ResumeLayout(false);
        marketPage.ResumeLayout(false);
        marketPage.PerformLayout();
        tabs.ResumeLayout(false);
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
}
