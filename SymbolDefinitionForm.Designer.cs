namespace Trade.It;

partial class SymbolDefinitionForm
{
    private System.ComponentModel.IContainer components = null!;
    private TextBox symbolTextBox = null!;
    private TextBox nameTextBox = null!;
    private ComboBox exchangeComboBox = null!;
    private ComboBox marketComboBox = null!;
    private ComboBox boardComboBox = null!;
    private ComboBox assetComboBox = null!;
    private ComboBox groupTextBox = null!;
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
    private Label label00 = null!;
    private Label label10 = null!;
    private Label label20 = null!;
    private Label label30 = null!;
    private Label label01 = null!;
    private Label label11 = null!;
    private Label label21 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        symbolTextBox = new TextBox();
        nameTextBox = new TextBox();
        exchangeComboBox = new ComboBox();
        marketComboBox = new ComboBox();
        boardComboBox = new ComboBox();
        assetComboBox = new ComboBox();
        groupTextBox = new ComboBox();
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
        label00 = new Label();
        label10 = new Label();
        label20 = new Label();
        label30 = new Label();
        label01 = new Label();
        label11 = new Label();
        label21 = new Label();
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).BeginInit();
        SuspendLayout();

        // Row 1
        label00.AutoSize = false;
        label00.Location = new Point(1050, 18);
        label00.Size = new Size(110, 30);
        label00.Text = "نماد";
        label00.TextAlign = ContentAlignment.MiddleRight;
        symbolTextBox.Location = new Point(860, 18);
        symbolTextBox.Name = "symbolTextBox";
        symbolTextBox.Size = new Size(180, 29);

        label10.AutoSize = false;
        label10.Location = new Point(760, 18);
        label10.Size = new Size(90, 30);
        label10.Text = "نام";
        label10.TextAlign = ContentAlignment.MiddleRight;
        nameTextBox.Location = new Point(570, 18);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(180, 29);

        label20.AutoSize = false;
        label20.Location = new Point(470, 18);
        label20.Size = new Size(90, 30);
        label20.Text = "عنوان بورس";
        label20.TextAlign = ContentAlignment.MiddleRight;
        exchangeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        exchangeComboBox.Items.AddRange(new object[] { "-", "بورس", "فرابورس", "بورس کالا", "بورس انرژی" });
        exchangeComboBox.Location = new Point(280, 18);
        exchangeComboBox.Name = "exchangeComboBox";
        exchangeComboBox.Size = new Size(180, 30);

        label30.AutoSize = false;
        label30.Location = new Point(175, 18);
        label30.Size = new Size(90, 30);
        label30.Text = "نوع بازار";
        label30.TextAlign = ContentAlignment.MiddleRight;
        marketComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        marketComboBox.Items.AddRange(new object[] { "-", "اول", "دوم", "پایه", "SMD", "نوآفرین" });
        marketComboBox.Location = new Point(5, 18);
        marketComboBox.Name = "marketComboBox";
        marketComboBox.Size = new Size(165, 30);

        // Row 2
        label01.AutoSize = false;
        label01.Location = new Point(1050, 68);
        label01.Size = new Size(110, 30);
        label01.Text = "تابلو";
        label01.TextAlign = ContentAlignment.MiddleRight;
        boardComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        boardComboBox.Items.AddRange(new object[] { "-", "اصلی", "فرعی", "رشد", "دانش بنیان", "زرد", "نارنجی", "قرمز" });
        boardComboBox.Location = new Point(860, 68);
        boardComboBox.Name = "boardComboBox";
        boardComboBox.Size = new Size(180, 30);

        label11.AutoSize = false;
        label11.Location = new Point(760, 68);
        label11.Size = new Size(90, 30);
        label11.Text = "نوع دارایی";
        label11.TextAlign = ContentAlignment.MiddleRight;
        assetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        assetComboBox.Items.AddRange(new object[] { "-", "سهام", "صندوق ETF" });
        assetComboBox.Location = new Point(570, 68);
        assetComboBox.Name = "assetComboBox";
        assetComboBox.Size = new Size(180, 30);

        label21.AutoSize = false;
        label21.Location = new Point(470, 68);
        label21.Size = new Size(90, 30);
        label21.Text = "گروه صنعت/نوع صندوق";
        label21.TextAlign = ContentAlignment.MiddleRight;
        groupTextBox.DropDownStyle = ComboBoxStyle.DropDownList;
        groupTextBox.Items.AddRange(new object[] { "-" });
        groupTextBox.Location = new Point(280, 68);
        groupTextBox.Name = "groupTextBox";
        groupTextBox.Size = new Size(180, 30);

        // Buttons
        newButton.AutoSize = true;
        newButton.Location = new Point(1040, 118);
        newButton.Name = "newButton";
        newButton.Size = new Size(75, 35);
        newButton.Text = "جدید";
        newButton.UseVisualStyleBackColor = true;

        saveButton.AutoSize = true;
        saveButton.Location = new Point(955, 118);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 35);
        saveButton.Text = "ذخیره";
        saveButton.UseVisualStyleBackColor = true;

        deleteButton.AutoSize = true;
        deleteButton.Location = new Point(870, 118);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(75, 35);
        deleteButton.Text = "حذف";
        deleteButton.UseVisualStyleBackColor = true;

        deleteAllButton.AutoSize = true;
        deleteAllButton.Location = new Point(760, 118);
        deleteAllButton.Name = "deleteAllButton";
        deleteAllButton.Size = new Size(100, 35);
        deleteAllButton.Text = "حذف همه";
        deleteAllButton.UseVisualStyleBackColor = true;

        importButton.AutoSize = true;
        importButton.Location = new Point(630, 118);
        importButton.Name = "importButton";
        importButton.Size = new Size(120, 35);
        importButton.Text = "ورود از Excel";
        importButton.UseVisualStyleBackColor = true;

        closeButton.AutoSize = true;
        closeButton.Location = new Point(545, 118);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(75, 35);
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;

        countLabel.AutoSize = true;
        countLabel.Location = new Point(420, 125);
        countLabel.Name = "countLabel";
        countLabel.Size = new Size(90, 22);
        countLabel.Text = "تعداد: 0";

        // Grid
        symbolsDataGridView.AllowUserToAddRows = false;
        symbolsDataGridView.AllowUserToDeleteRows = false;
        symbolsDataGridView.AllowUserToResizeRows = false;
        symbolsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        symbolsDataGridView.ColumnHeadersHeight = 34;
        symbolsDataGridView.Columns.AddRange(new DataGridViewColumn[] { symbolTitleColumn, nameColumn, exchangeColumn, marketColumn, boardColumn, assetColumn, groupColumn });
        symbolsDataGridView.Location = new Point(10, 165);
        symbolsDataGridView.MultiSelect = false;
        symbolsDataGridView.Name = "symbolsDataGridView";
        symbolsDataGridView.ReadOnly = true;
        symbolsDataGridView.RightToLeft = RightToLeft.Yes;
        symbolsDataGridView.RowHeadersVisible = false;
        symbolsDataGridView.RowHeadersWidth = 62;
        symbolsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        symbolsDataGridView.Size = new Size(1160, 520);
        symbolsDataGridView.TabIndex = 0;

        symbolTitleColumn.HeaderText = "عنوان نماد";
        symbolTitleColumn.MinimumWidth = 8;
        symbolTitleColumn.Name = "symbolTitleColumn";
        symbolTitleColumn.ReadOnly = true;
        symbolTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        nameColumn.FillWeight = 150F;
        nameColumn.HeaderText = "نام نماد";
        nameColumn.MinimumWidth = 8;
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        exchangeColumn.HeaderText = "عنوان بورس";
        exchangeColumn.MinimumWidth = 8;
        exchangeColumn.Name = "exchangeColumn";
        exchangeColumn.ReadOnly = true;
        exchangeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        marketColumn.HeaderText = "نوع بازار";
        marketColumn.MinimumWidth = 8;
        marketColumn.Name = "marketColumn";
        marketColumn.ReadOnly = true;
        marketColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        boardColumn.HeaderText = "نوع تابلو";
        boardColumn.MinimumWidth = 8;
        boardColumn.Name = "boardColumn";
        boardColumn.ReadOnly = true;
        boardColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        assetColumn.FillWeight = 80F;
        assetColumn.HeaderText = "نوع دارایی";
        assetColumn.MinimumWidth = 8;
        assetColumn.Name = "assetColumn";
        assetColumn.ReadOnly = true;
        assetColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        groupColumn.FillWeight = 150F;
        groupColumn.HeaderText = "گروه صنعت / نوع صندوق";
        groupColumn.MinimumWidth = 8;
        groupColumn.Name = "groupColumn";
        groupColumn.ReadOnly = true;
        groupColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

        ClientSize = new Size(1180, 700);
        Controls.Add(symbolTextBox);
        Controls.Add(nameTextBox);
        Controls.Add(exchangeComboBox);
        Controls.Add(marketComboBox);
        Controls.Add(boardComboBox);
        Controls.Add(assetComboBox);
        Controls.Add(groupTextBox);
        Controls.Add(newButton);
        Controls.Add(saveButton);
        Controls.Add(deleteButton);
        Controls.Add(deleteAllButton);
        Controls.Add(importButton);
        Controls.Add(closeButton);
        Controls.Add(countLabel);
        Controls.Add(symbolsDataGridView);
        Controls.Add(label00);
        Controls.Add(label10);
        Controls.Add(label20);
        Controls.Add(label30);
        Controls.Add(label01);
        Controls.Add(label11);
        Controls.Add(label21);
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
}