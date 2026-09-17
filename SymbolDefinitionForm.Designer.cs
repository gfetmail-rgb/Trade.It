namespace Trade.It;

partial class SymbolDefinitionForm
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootTableLayoutPanel = null!;
    private TableLayoutPanel fieldsTableLayoutPanel = null!;
    private FlowLayoutPanel buttonsFlowLayoutPanel = null!;
    private TextBox symbolTextBox = null!;
    private TextBox nameTextBox = null!;
    private TextBox exchangeTextBox = null!;
    private ComboBox marketComboBox = null!;
    private ComboBox boardComboBox = null!;
    private ComboBox assetComboBox = null!;
    private TextBox groupTextBox = null!;
    private Button newButton = null!;
    private Button saveButton = null!;
    private Button deleteButton = null!;
    private Button deleteAllButton = null!;
    private Button importButton = null!;
    private Button closeButton = null!;
    private Label countLabel = null!;
    private DataGridView symbolsDataGridView = null!;
    private DataGridViewTextBoxColumn symbolTitleColumn = null!;
    private DataGridViewTextBoxColumn nameColumn = null!;
    private DataGridViewTextBoxColumn exchangeColumn = null!;
    private DataGridViewTextBoxColumn marketColumn = null!;
    private DataGridViewTextBoxColumn boardColumn = null!;
    private DataGridViewTextBoxColumn assetColumn = null!;
    private DataGridViewTextBoxColumn groupColumn = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        rootTableLayoutPanel = new TableLayoutPanel();
        fieldsTableLayoutPanel = new TableLayoutPanel();
        buttonsFlowLayoutPanel = new FlowLayoutPanel();
        symbolTextBox = new TextBox();
        nameTextBox = new TextBox();
        exchangeTextBox = new TextBox();
        marketComboBox = new ComboBox();
        boardComboBox = new ComboBox();
        assetComboBox = new ComboBox();
        groupTextBox = new TextBox();
        newButton = new Button();
        saveButton = new Button();
        deleteButton = new Button();
        deleteAllButton = new Button();
        importButton = new Button();
        closeButton = new Button();
        countLabel = new Label();
        symbolsDataGridView = new DataGridView();
        symbolTitleColumn = new DataGridViewTextBoxColumn();
        nameColumn = new DataGridViewTextBoxColumn();
        exchangeColumn = new DataGridViewTextBoxColumn();
        marketColumn = new DataGridViewTextBoxColumn();
        boardColumn = new DataGridViewTextBoxColumn();
        assetColumn = new DataGridViewTextBoxColumn();
        groupColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).BeginInit();
        SuspendLayout();

        rootTableLayoutPanel.ColumnCount = 1;
        rootTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootTableLayoutPanel.Dock = DockStyle.Fill;
        rootTableLayoutPanel.Padding = new Padding(10);
        rootTableLayoutPanel.RowCount = 3;
        rootTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        rootTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        rootTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        fieldsTableLayoutPanel.ColumnCount = 4;
        for (int i = 0; i < 4; i++) fieldsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        fieldsTableLayoutPanel.RowCount = 2;
        fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        fieldsTableLayoutPanel.Dock = DockStyle.Fill;
        fieldsTableLayoutPanel.Padding = new Padding(4);
        rootTableLayoutPanel.Controls.Add(fieldsTableLayoutPanel, 0, 0);

        AddDesignerField(0, 0, "عنوان نماد", symbolTextBox);
        AddDesignerField(1, 0, "نام نماد", nameTextBox);
        AddDesignerField(2, 0, "عنوان بورس", exchangeTextBox);
        AddDesignerField(3, 0, "نوع بازار", marketComboBox);
        AddDesignerField(0, 1, "نوع تابلو", boardComboBox);
        AddDesignerField(1, 1, "نوع دارایی", assetComboBox);
        AddDesignerField(2, 1, "گروه صنعت / نوع صندوق", groupTextBox);

        marketComboBox.DropDownStyle = ComboBoxStyle.DropDown;
        marketComboBox.Items.AddRange(new object[] { "بورس تهران", "فرابورس ایران", "بورس کالا", "بورس انرژی" });
        boardComboBox.DropDownStyle = ComboBoxStyle.DropDown;
        boardComboBox.Items.AddRange(new object[] { "تابلوی اصلی", "تابلوی فرعی", "بازار اول", "بازار دوم", "پایه زرد", "پایه نارنجی", "پایه قرمز" });
        assetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        assetComboBox.Items.AddRange(new object[] { "سهام", "etf صندوق" });

        buttonsFlowLayoutPanel.Dock = DockStyle.Fill;
        buttonsFlowLayoutPanel.FlowDirection = FlowDirection.RightToLeft;
        buttonsFlowLayoutPanel.WrapContents = false;
        rootTableLayoutPanel.Controls.Add(buttonsFlowLayoutPanel, 0, 1);

        ConfigureDesignerButton(newButton, "جدید");
        ConfigureDesignerButton(saveButton, "ذخیره");
        ConfigureDesignerButton(deleteButton, "حذف");
        ConfigureDesignerButton(deleteAllButton, "حذف همه");
        ConfigureDesignerButton(importButton, "ورود از Excel");
        ConfigureDesignerButton(closeButton, "بستن");
        countLabel.AutoSize = true;
        countLabel.Margin = new Padding(20, 9, 10, 3);
        countLabel.Text = "تعداد: 0";
        buttonsFlowLayoutPanel.Controls.AddRange(new Control[] { newButton, saveButton, deleteButton, deleteAllButton, importButton, closeButton, countLabel });

        symbolsDataGridView.AllowUserToAddRows = false;
        symbolsDataGridView.AllowUserToDeleteRows = false;
        symbolsDataGridView.AllowUserToResizeRows = false;
        symbolsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        symbolsDataGridView.ColumnHeadersHeight = 34;
        symbolsDataGridView.Columns.AddRange(new DataGridViewColumn[] { symbolTitleColumn, nameColumn, exchangeColumn, marketColumn, boardColumn, assetColumn, groupColumn });
        symbolsDataGridView.Dock = DockStyle.Fill;
        symbolsDataGridView.MultiSelect = false;
        symbolsDataGridView.ReadOnly = true;
        symbolsDataGridView.RightToLeft = RightToLeft.Yes;
        symbolsDataGridView.RowHeadersVisible = false;
        symbolsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        rootTableLayoutPanel.Controls.Add(symbolsDataGridView, 0, 2);

        symbolTitleColumn.FillWeight = 100F;
        symbolTitleColumn.HeaderText = "عنوان نماد";
        symbolTitleColumn.Name = "symbolTitleColumn";
        symbolTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        nameColumn.FillWeight = 150F;
        nameColumn.HeaderText = "نام نماد";
        nameColumn.Name = "nameColumn";
        nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        exchangeColumn.FillWeight = 100F;
        exchangeColumn.HeaderText = "عنوان بورس";
        exchangeColumn.Name = "exchangeColumn";
        exchangeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        marketColumn.FillWeight = 100F;
        marketColumn.HeaderText = "نوع بازار";
        marketColumn.Name = "marketColumn";
        marketColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        boardColumn.FillWeight = 100F;
        boardColumn.HeaderText = "نوع تابلو";
        boardColumn.Name = "boardColumn";
        boardColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        assetColumn.FillWeight = 80F;
        assetColumn.HeaderText = "نوع دارایی";
        assetColumn.Name = "assetColumn";
        assetColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        groupColumn.FillWeight = 150F;
        groupColumn.HeaderText = "گروه صنعت / نوع صندوق";
        groupColumn.Name = "groupColumn";
        groupColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

        ClientSize = new Size(1180, 700);
        Controls.Add(rootTableLayoutPanel);
        Font = new Font("Tahoma", 9F);
        MinimumSize = new Size(950, 600);
        Name = "SymbolDefinitionForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "تعریف نمادها";
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).EndInit();
        ResumeLayout(false);
    }

    private void AddDesignerField(int column, int row, string caption, Control control)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
        var label = new Label { Dock = DockStyle.Right, Width = 135, Text = caption, TextAlign = ContentAlignment.MiddleRight };
        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control);
        panel.Controls.Add(label);
        fieldsTableLayoutPanel.Controls.Add(panel, column, row);
    }

    private static void ConfigureDesignerButton(Button button, string text)
    {
        button.AutoSize = true;
        button.Height = 32;
        button.Margin = new Padding(4);
        button.Text = text;
        button.UseVisualStyleBackColor = true;
    }
}
