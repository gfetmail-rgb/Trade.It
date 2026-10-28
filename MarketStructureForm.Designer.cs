namespace Trade.It;

partial class MarketStructureForm
{
    private System.ComponentModel.IContainer? components = null;
    private TabControl tabs;
    private TabPage marketPage;
    private TreeView marketTreeView;
    private FlowLayoutPanel marketButtons;
    private TextBox marketTitleTextBox;
    private Button addRootButton;
    private Button importMarketStructureButton;
    private Button exportMarketStructureButton;
    private Button addChildButton;
    private Button renameButton;
    private Button deleteButton;
    private Button moveUpButton;
    private Button moveDownButton;
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
        addRootButton = new Button();
        importMarketStructureButton = new Button();
        exportMarketStructureButton = new Button();
        addChildButton = new Button();
        bottomPanel = new Panel();
        closeButton = new Button();
        label2 = new Label();
        label1 = new Label();
        tabs.SuspendLayout();
        marketPage.SuspendLayout();
        marketButtons.SuspendLayout();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        // 
        // tabs
        // 
        tabs.Controls.Add(marketPage);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 0);
        tabs.Margin = new Padding(4, 5, 4, 5);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.Size = new Size(1500, 1123);
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
        marketPage.Size = new Size(1492, 1085);
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
        marketTreeView.Size = new Size(1470, 941);
        marketTreeView.TabIndex = 0;
        // 
        // marketTitleTextBox
        // 
        marketTitleTextBox.BackColor = Color.Bisque;
        marketTitleTextBox.Dock = DockStyle.Bottom;
        marketTitleTextBox.Location = new Point(11, 930);
        marketTitleTextBox.Margin = new Padding(4, 5, 4, 5);
        marketTitleTextBox.Name = "marketTitleTextBox";
        marketTitleTextBox.PlaceholderText = "نام‌ها را از Excel در خط‌های جداگانه بچسبانید؛ بازار سطح اول را انتخاب کنید و «ثبت / افزودن زیرمجموعه» را بزنید.";
        marketTitleTextBox.Multiline = true;
        marketTitleTextBox.ScrollBars = ScrollBars.Vertical;
        marketTitleTextBox.Size = new Size(1470, 55);
        marketTitleTextBox.TabIndex = 1;
        // 
        // marketButtons
        // 
        marketButtons.Controls.Add(moveDownButton);
        marketButtons.Controls.Add(moveUpButton);
        marketButtons.Controls.Add(deleteButton);
        marketButtons.Controls.Add(renameButton);
        marketButtons.Controls.Add(addChildButton);
        marketButtons.Controls.Add(addRootButton);
        marketButtons.Controls.Add(importMarketStructureButton);
        marketButtons.Controls.Add(exportMarketStructureButton);
        marketButtons.Dock = DockStyle.Bottom;
        marketButtons.FlowDirection = FlowDirection.RightToLeft;
        marketButtons.Location = new Point(11, 985);
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
        moveDownButton.Size = new Size(130, 38);
        moveDownButton.TabIndex = 0;
        moveDownButton.Text = "انتقال به پایین";
        moveDownButton.UseVisualStyleBackColor = true;
        // 
        // moveUpButton
        // 
        moveUpButton.Location = new Point(142, 17);
        moveUpButton.Margin = new Padding(4, 5, 4, 5);
        moveUpButton.Name = "moveUpButton";
        moveUpButton.Size = new Size(129, 38);
        moveUpButton.TabIndex = 1;
        moveUpButton.Text = "انتقال به بالا";
        moveUpButton.UseVisualStyleBackColor = true;
        // 
        // deleteButton
        // 
        deleteButton.Location = new Point(279, 17);
        deleteButton.Margin = new Padding(4, 5, 4, 5);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(107, 38);
        deleteButton.TabIndex = 2;
        deleteButton.Text = "حذف";
        deleteButton.UseVisualStyleBackColor = true;
        // 
        // renameButton
        // 
        renameButton.Location = new Point(394, 17);
        renameButton.Margin = new Padding(4, 5, 4, 5);
        renameButton.Name = "renameButton";
        renameButton.Size = new Size(107, 38);
        renameButton.TabIndex = 3;
        renameButton.Text = "تغییر نام";
        renameButton.UseVisualStyleBackColor = true;
        // 
        // addChildButton
        // 
        addChildButton.Location = new Point(509, 17);
        addChildButton.Margin = new Padding(4, 5, 4, 5);
        addChildButton.Name = "addChildButton";
        addChildButton.Size = new Size(128, 38);
        addChildButton.TabIndex = 4;
        addChildButton.Text = "ثبت / افزودن زیرمجموعه";
        addChildButton.UseVisualStyleBackColor = true;
        // 
        // bottomPanel
        // 
        bottomPanel.Controls.Add(closeButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new Point(0, 1123);
        bottomPanel.Margin = new Padding(4, 5, 4, 5);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(1500, 44);
        bottomPanel.TabIndex = 1;
        // 
        // closeButton
        // 
        closeButton.Dock = DockStyle.Right;
        closeButton.Location = new Point(1420, 0);
        closeButton.Margin = new Padding(4, 5, 4, 5);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(80, 44);
        closeButton.TabIndex = 0;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label2.ForeColor = Color.Brown;
        label2.Location = new Point(609, 12);
        label2.Name = "label2";
        label2.Size = new Size(823, 34);
        label2.TabIndex = 7;
        label2.Text = "اکسل باید فاقد هدر باشد. غیر تکراری ها (نسبت به مقادیر موجود) وارد خواهند شد و تکراری ها وارد نمی شوند";
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label1.ForeColor = Color.Brown;
        label1.Location = new Point(609, 12);
        label1.Name = "label1";
        label1.Size = new Size(823, 34);
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
