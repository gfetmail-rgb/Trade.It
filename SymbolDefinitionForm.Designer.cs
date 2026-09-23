namespace Trade.It;

partial class SymbolDefinitionForm
{
    private System.ComponentModel.IContainer components = null!;
    private TextBox symbolTextBox = null!;
    private TextBox nameTextBox = null!;
    private TreeView marketTreeView = null!;
    private ComboBox assetComboBox = null!;
    private Button newButton = null!;
    private Button saveButton = null!;
    private Button deleteButton = null!;
    private Button deleteAllButton = null!;
    private Button importButton = null!;
    private Button closeButton = null!;
    private Label countLabel = null!;
    private DataGridView symbolsDataGridView = null!;
    private Label label00 = null!;
    private Label label10 = null!;
    private Label label30 = null!;
    private Label label11 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolDefinitionForm));
        symbolTextBox = new TextBox();
        nameTextBox = new TextBox();
        marketTreeView = new TreeView();
        assetComboBox = new ComboBox();
        newButton = new Button();
        saveButton = new Button();
        deleteButton = new Button();
        deleteAllButton = new Button();
        importButton = new Button();
        exportButton = new Button();
        closeButton = new Button();
        countLabel = new Label();
        symbolsDataGridView = new DataGridView();
        rowNumberColumn = new DataGridViewTextBoxColumn();
        symbolTitleColumn = new DataGridViewTextBoxColumn();
        nameColumn = new DataGridViewTextBoxColumn();
        exchangeColumn = new DataGridViewTextBoxColumn();
        marketColumn = new DataGridViewTextBoxColumn();
        boardColumn = new DataGridViewTextBoxColumn();
        assetColumn = new DataGridViewTextBoxColumn();
        label00 = new Label();
        label10 = new Label();
        label30 = new Label();
        label11 = new Label();
        toolTip1 = new ToolTip(components);
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).BeginInit();
        SuspendLayout();
        // 
        // symbolTextBox
        // 
        symbolTextBox.Location = new Point(75, 19);
        symbolTextBox.Name = "symbolTextBox";
        symbolTextBox.Size = new Size(201, 29);
        symbolTextBox.TabIndex = 0;
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(325, 19);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(370, 29);
        nameTextBox.TabIndex = 1;
        // 
        // marketTreeView
        // 
        marketTreeView.Location = new Point(860, 55);
        marketTreeView.Name = "marketTreeView";
        marketTreeView.Size = new Size(401, 360);
        marketTreeView.TabIndex = 25;
        marketTreeView.HideSelection = false;
        marketTreeView.RightToLeft = RightToLeft.Yes;
        marketTreeView.RightToLeftLayout = true;
        // 
        // 
        // assetComboBox
        // 
        assetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        assetComboBox.Items.AddRange(new object[] { "سهام نیست", "سهام", "صندوق ETF سهامی", "صندوق ETF کالایی" });
        assetComboBox.Location = new Point(359, 76);
        assetComboBox.Name = "assetComboBox";
        assetComboBox.Size = new Size(175, 30);
        assetComboBox.TabIndex = 5;
        // 
        // newButton
        // 
        newButton.AutoSize = true;
        newButton.Location = new Point(701, 118);
        newButton.Name = "newButton";
        newButton.Size = new Size(75, 35);
        newButton.TabIndex = 8;
        newButton.Text = "جدید";
        newButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.AutoSize = true;
        saveButton.Location = new Point(787, 118);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 35);
        saveButton.TabIndex = 9;
        saveButton.Text = "ذخیره";
        saveButton.UseVisualStyleBackColor = true;
        // 
        // deleteButton
        // 
        deleteButton.AutoSize = true;
        deleteButton.Location = new Point(994, 118);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(75, 35);
        deleteButton.TabIndex = 10;
        deleteButton.Text = "حذف";
        deleteButton.UseVisualStyleBackColor = true;
        // 
        // deleteAllButton
        // 
        deleteAllButton.AutoSize = true;
        deleteAllButton.Location = new Point(1075, 118);
        deleteAllButton.Name = "deleteAllButton";
        deleteAllButton.Size = new Size(100, 35);
        deleteAllButton.TabIndex = 11;
        deleteAllButton.Text = "حذف همه";
        deleteAllButton.UseVisualStyleBackColor = true;
        // 
        // importButton
        // 
        importButton.AutoSize = true;
        importButton.Location = new Point(868, 118);
        importButton.Name = "importButton";
        importButton.Size = new Size(120, 35);
        importButton.TabIndex = 12;
        importButton.Text = "ورود از Excel";
        toolTip1.SetToolTip(importButton, resources.GetString("importButton.ToolTip"));
        importButton.UseVisualStyleBackColor = true;
        // 
        // exportButton
        // 
        exportButton.AutoSize = true;
        exportButton.Location = new Point(280, 118);
        exportButton.Name = "exportButton";
        exportButton.Size = new Size(145, 35);
        exportButton.TabIndex = 13;
        exportButton.Text = "خروجی به Excel";
        toolTip1.SetToolTip(exportButton, "تمام نمادهای ثبت‌شده را در یک فایل Excel جدید ذخیره می‌کند.");
        exportButton.UseVisualStyleBackColor = true;
        // 
        // closeButton
        // 
        closeButton.AutoSize = true;
        closeButton.Location = new Point(1186, 118);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(75, 35);
        closeButton.TabIndex = 14;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // countLabel
        // 
        countLabel.AutoSize = true;
        countLabel.Location = new Point(22, 131);
        countLabel.Name = "countLabel";
        countLabel.Size = new Size(68, 22);
        countLabel.TabIndex = 15;
        countLabel.Text = "تعداد: 0";
        // 
        // symbolsDataGridView
        // 
        symbolsDataGridView.AllowUserToAddRows = false;
        symbolsDataGridView.AllowUserToDeleteRows = false;
        symbolsDataGridView.AllowUserToResizeRows = false;
        symbolsDataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        symbolsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        symbolsDataGridView.ColumnHeadersHeight = 34;
        symbolsDataGridView.Columns.AddRange(new DataGridViewColumn[] { rowNumberColumn, symbolTitleColumn, nameColumn, exchangeColumn, marketColumn, boardColumn, assetColumn, fundTypeColumn, industryGroupColumn });
        symbolsDataGridView.Location = new Point(10, 165);
        symbolsDataGridView.MultiSelect = false;
        symbolsDataGridView.Name = "symbolsDataGridView";
        symbolsDataGridView.ReadOnly = true;
        symbolsDataGridView.RightToLeft = RightToLeft.Yes;
        symbolsDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        symbolsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        symbolsDataGridView.RowHeadersVisible = false;
        symbolsDataGridView.RowHeadersWidth = 62;
        symbolsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        symbolsDataGridView.Size = new Size(1279, 520);
        symbolsDataGridView.TabIndex = 0;
        // 
        // rowNumberColumn
        // 
        rowNumberColumn.FillWeight = 45F;
        rowNumberColumn.HeaderText = "ردیف";
        rowNumberColumn.MinimumWidth = 45;
        rowNumberColumn.Name = "rowNumberColumn";
        rowNumberColumn.ReadOnly = true;
        rowNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        rowNumberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // symbolTitleColumn
        // 
        symbolTitleColumn.HeaderText = "نماد";
        symbolTitleColumn.MinimumWidth = 8;
        symbolTitleColumn.Name = "symbolTitleColumn";
        symbolTitleColumn.ReadOnly = true;
        symbolTitleColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        symbolTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // nameColumn
        // 
        nameColumn.FillWeight = 150F;
        nameColumn.HeaderText = "نام";
        nameColumn.MinimumWidth = 8;
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        nameColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // exchangeColumn
        // 
        exchangeColumn.HeaderText = "بورس";
        exchangeColumn.MinimumWidth = 8;
        exchangeColumn.Name = "exchangeColumn";
        exchangeColumn.ReadOnly = true;
        exchangeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        exchangeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // marketColumn
        // 
        marketColumn.HeaderText = "بازار";
        marketColumn.MinimumWidth = 8;
        marketColumn.Name = "marketColumn";
        marketColumn.ReadOnly = true;
        marketColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        marketColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // boardColumn
        // 
        boardColumn.HeaderText = "تابلو";
        boardColumn.MinimumWidth = 8;
        boardColumn.Name = "boardColumn";
        boardColumn.ReadOnly = true;
        boardColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        boardColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // assetColumn
        // 
        assetColumn.FillWeight = 80F;
        assetColumn.HeaderText = "دارایی";
        assetColumn.MinimumWidth = 8;
        assetColumn.Name = "assetColumn";
        assetColumn.ReadOnly = true;
        assetColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        assetColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // label00
        // 
        label00.Location = new Point(12, 18);
        label00.Name = "label00";
        label00.Size = new Size(57, 30);
        label00.TabIndex = 16;
        label00.Text = "نماد";
        label00.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label10
        // 
        label10.Location = new Point(229, 19);
        label10.Name = "label10";
        label10.Size = new Size(90, 30);
        label10.TabIndex = 17;
        label10.Text = "نام";
        label10.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label30
        // 
        label30.Location = new Point(860, 20);
        label30.Name = "label30";
        label30.Size = new Size(100, 30);
        label30.TabIndex = 19;
        label30.Text = "ساختار بازار";
        label30.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label11
        // 
        label11.Location = new Point(262, 76);
        label11.Name = "label11";
        label11.Size = new Size(90, 30);
        label11.TabIndex = 21;
        label11.Text = "دارایی";
        label11.TextAlign = ContentAlignment.MiddleRight;
        // 
        // toolTip1
        // 
        toolTip1.AutoPopDelay = 5000;
        toolTip1.InitialDelay = 800;
        toolTip1.IsBalloon = true;
        toolTip1.ReshowDelay = 100;
        toolTip1.ToolTipIcon = ToolTipIcon.Info;
        toolTip1.ToolTipTitle = "هشدار";
        // 
        // SymbolDefinitionForm
        // 
        ClientSize = new Size(1293, 700);
        Controls.Add(symbolTextBox);
        Controls.Add(nameTextBox);
        Controls.Add(assetComboBox);
        Controls.Add(newButton);
        Controls.Add(saveButton);
        Controls.Add(deleteButton);
        Controls.Add(deleteAllButton);
        Controls.Add(importButton);
        Controls.Add(exportButton);
        Controls.Add(closeButton);
        Controls.Add(countLabel);
        Controls.Add(symbolsDataGridView);
        Controls.Add(label00);
        Controls.Add(label10);
        Controls.Add(label30);
        Controls.Add(label11);
        Controls.Add(marketTreeView);
        Font = new Font("Tahoma", 9F);
        MinimumSize = new Size(950, 600);
        Name = "SymbolDefinitionForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "تعریف نمادها";
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
    private Label label1;
    private ToolTip toolTip1;
    private Button exportButton;
    private DataGridViewTextBoxColumn rowNumberColumn;
    private DataGridViewTextBoxColumn symbolTitleColumn;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn exchangeColumn;
    private DataGridViewTextBoxColumn marketColumn;
    private DataGridViewTextBoxColumn boardColumn;
    private DataGridViewTextBoxColumn assetColumn;
}
