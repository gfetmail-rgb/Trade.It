namespace Trade.It;

partial class MarketStructureForm
{
    private System.ComponentModel.IContainer? components = null;
    private TabControl tabs;
    private TabPage marketPage;
    private TreeView marketTreeView;
    private TextBox marketTitleTextBox;

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
        closeButton = new Button();
        addChildButton = new Button();
        exportMarketStructureButton = new Button();
        moveDownButton = new Button();
        moveUpButton = new Button();
        deleteButton = new Button();
        renameButton = new Button();
        addRootButton = new Button();
        importMarketStructureButton = new Button();
        tabs.SuspendLayout();
        marketPage.SuspendLayout();
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
        tabs.Size = new Size(1500, 823);
        tabs.TabIndex = 0;
        // 
        // marketPage
        // 
        marketPage.Controls.Add(closeButton);
        marketPage.Controls.Add(addChildButton);
        marketPage.Controls.Add(exportMarketStructureButton);
        marketPage.Controls.Add(moveDownButton);
        marketPage.Controls.Add(moveUpButton);
        marketPage.Controls.Add(deleteButton);
        marketPage.Controls.Add(renameButton);
        marketPage.Controls.Add(addRootButton);
        marketPage.Controls.Add(importMarketStructureButton);
        marketPage.Controls.Add(marketTreeView);
        marketPage.Controls.Add(marketTitleTextBox);
        marketPage.Location = new Point(4, 34);
        marketPage.Margin = new Padding(4, 5, 4, 5);
        marketPage.Name = "marketPage";
        marketPage.Padding = new Padding(11, 13, 11, 13);
        marketPage.Size = new Size(1492, 785);
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
        marketTreeView.Size = new Size(1470, 709);
        marketTreeView.TabIndex = 0;
        // 
        // marketTitleTextBox
        // 
        marketTitleTextBox.BackColor = Color.Bisque;
        marketTitleTextBox.Dock = DockStyle.Bottom;
        marketTitleTextBox.Location = new Point(11, 722);
        marketTitleTextBox.Margin = new Padding(4, 5, 4, 5);
        marketTitleTextBox.Multiline = true;
        marketTitleTextBox.Name = "marketTitleTextBox";
        marketTitleTextBox.Size = new Size(1470, 50);
        marketTitleTextBox.TabIndex = 1;
        // 
        // closeButton
        // 
        closeButton.Location = new Point(184, 728);
        closeButton.Margin = new Padding(4, 5, 4, 5);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(102, 38);
        closeButton.TabIndex = 8;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // addChildButton
        // 
        addChildButton.Location = new Point(294, 728);
        addChildButton.Margin = new Padding(4, 5, 4, 5);
        addChildButton.Name = "addChildButton";
        addChildButton.Size = new Size(128, 38);
        addChildButton.TabIndex = 13;
        addChildButton.Text = "ثبت / افزودن زیرمجموعه";
        addChildButton.UseVisualStyleBackColor = true;
        // 
        // exportMarketStructureButton
        // 
        exportMarketStructureButton.Location = new Point(429, 726);
        exportMarketStructureButton.Name = "exportMarketStructureButton";
        exportMarketStructureButton.Size = new Size(125, 40);
        exportMarketStructureButton.TabIndex = 16;
        exportMarketStructureButton.Text = "خروجی اکسل";
        // 
        // moveDownButton
        // 
        moveDownButton.Location = new Point(561, 728);
        moveDownButton.Margin = new Padding(4, 5, 4, 5);
        moveDownButton.Name = "moveDownButton";
        moveDownButton.Size = new Size(130, 38);
        moveDownButton.TabIndex = 9;
        moveDownButton.Text = "انتقال به پایین";
        moveDownButton.UseVisualStyleBackColor = true;
        // 
        // moveUpButton
        // 
        moveUpButton.Location = new Point(699, 728);
        moveUpButton.Margin = new Padding(4, 5, 4, 5);
        moveUpButton.Name = "moveUpButton";
        moveUpButton.Size = new Size(129, 38);
        moveUpButton.TabIndex = 10;
        moveUpButton.Text = "انتقال به بالا";
        moveUpButton.UseVisualStyleBackColor = true;
        // 
        // deleteButton
        // 
        deleteButton.Location = new Point(836, 728);
        deleteButton.Margin = new Padding(4, 5, 4, 5);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(107, 38);
        deleteButton.TabIndex = 11;
        deleteButton.Text = "حذف";
        deleteButton.UseVisualStyleBackColor = true;
        // 
        // renameButton
        // 
        renameButton.Location = new Point(951, 728);
        renameButton.Margin = new Padding(4, 5, 4, 5);
        renameButton.Name = "renameButton";
        renameButton.Size = new Size(107, 38);
        renameButton.TabIndex = 12;
        renameButton.Text = "تغییر نام";
        renameButton.UseVisualStyleBackColor = true;
        // 
        // addRootButton
        // 
        addRootButton.Location = new Point(1065, 726);
        addRootButton.Name = "addRootButton";
        addRootButton.Size = new Size(102, 40);
        addRootButton.TabIndex = 14;
        addRootButton.Text = "گره جدید";
        // 
        // importMarketStructureButton
        // 
        importMarketStructureButton.Location = new Point(1173, 726);
        importMarketStructureButton.Name = "importMarketStructureButton";
        importMarketStructureButton.Size = new Size(118, 40);
        importMarketStructureButton.TabIndex = 15;
        importMarketStructureButton.Text = "ورودی اکسل";
        // 
        // MarketStructureForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1500, 823);
        Controls.Add(tabs);
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
    private Button closeButton;
    private Button addChildButton;
    private Button exportMarketStructureButton;
    private Button moveDownButton;
    private Button moveUpButton;
    private Button deleteButton;
    private Button renameButton;
    private Button addRootButton;
    private Button importMarketStructureButton;
}
