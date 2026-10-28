namespace Trade.It;

partial class SymbolDefinitionForm
{
    private System.ComponentModel.IContainer components = null!;
    private TextBox symbolTextBox = null!;
    private TextBox symbolSearchTextBox = null!;
    private TextBox nameTextBox = null!;
    private TreeView marketTreeView = null!;
    private TextBox descriptionTextBox = null!;
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
    private Label label12 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolDefinitionForm));
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        symbolTextBox = new TextBox();
        symbolSearchTextBox = new TextBox();
        nameTextBox = new TextBox();
        marketTreeView = new TreeView();
        descriptionTextBox = new TextBox();
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
        descriptionColumn = new DataGridViewTextBoxColumn();
        label00 = new Label();
        label10 = new Label();
        label30 = new Label();
        label11 = new Label();
        label12 = new Label();
        toolTip1 = new ToolTip(components);
        ((System.ComponentModel.ISupportInitialize)symbolsDataGridView).BeginInit();
        SuspendLayout();
        // 
        // symbolTextBox
        // 
        symbolTextBox.Location = new Point(175, 19);
        symbolTextBox.Name = "symbolTextBox";
        symbolTextBox.Size = new Size(201, 29);
        symbolTextBox.TabIndex = 0;
        // 
        // symbolSearchTextBox
        // 
        symbolSearchTextBox.Location = new Point(10, 176);
        symbolSearchTextBox.Name = "symbolSearchTextBox";
        symbolSearchTextBox.PlaceholderText = "جستجوی نماد";
        symbolSearchTextBox.RightToLeft = RightToLeft.Yes;
        symbolSearchTextBox.Size = new Size(360, 29);
        symbolSearchTextBox.TabIndex = 15;
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(493, 19);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(302, 29);
        nameTextBox.TabIndex = 1;
        // 
        // marketTreeView
        // 
        marketTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        marketTreeView.HideSelection = false;
        marketTreeView.Location = new Point(916, 48);
        marketTreeView.Name = "marketTreeView";
        marketTreeView.RightToLeft = RightToLeft.Yes;
        marketTreeView.RightToLeftLayout = true;
        marketTreeView.Size = new Size(369, 640);
        marketTreeView.TabIndex = 25;
        // 
        // descriptionTextBox
        // 
        descriptionTextBox.Location = new Point(175, 76);
        descriptionTextBox.Name = "descriptionTextBox";
        descriptionTextBox.Size = new Size(620, 30);
        descriptionTextBox.TabIndex = 5;
        // 
        // newButton
        // 
        newButton.AutoSize = true;
        newButton.Location = new Point(99, 134);
        newButton.Name = "newButton";
        newButton.Size = new Size(75, 35);
        newButton.TabIndex = 8;
        newButton.Text = "جدید";
        newButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.AutoSize = true;
        saveButton.Location = new Point(180, 134);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 35);
        saveButton.TabIndex = 9;
        saveButton.Text = "ذخیره";
        saveButton.UseVisualStyleBackColor = true;
        // 
        // deleteButton
        // 
        deleteButton.AutoSize = true;
        deleteButton.Location = new Point(261, 134);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(75, 35);
        deleteButton.TabIndex = 10;
        deleteButton.Text = "حذف";
        deleteButton.UseVisualStyleBackColor = true;
        // 
        // deleteAllButton
        // 
        deleteAllButton.AutoSize = true;
        deleteAllButton.Location = new Point(342, 134);
        deleteAllButton.Name = "deleteAllButton";
        deleteAllButton.Size = new Size(100, 35);
        deleteAllButton.TabIndex = 11;
        deleteAllButton.Text = "حذف همه";
        deleteAllButton.UseVisualStyleBackColor = true;
        // 
        // importButton
        // 
        importButton.AutoSize = true;
        importButton.Location = new Point(448, 134);
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
        exportButton.Location = new Point(574, 134);
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
        closeButton.Location = new Point(725, 134);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(75, 35);
        closeButton.TabIndex = 14;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // countLabel
        // 
        countLabel.AutoSize = true;
        countLabel.Location = new Point(793, 179);
        countLabel.Name = "countLabel";
        countLabel.Size = new Size(68, 22);
        countLabel.TabIndex = 16;
        countLabel.Text = "تعداد: 0";
        // 
        // symbolsDataGridView
        // 
        symbolsDataGridView.AllowUserToAddRows = false;
        symbolsDataGridView.AllowUserToDeleteRows = false;
        symbolsDataGridView.AllowUserToResizeRows = false;
        symbolsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        symbolsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Tahoma", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        symbolsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        symbolsDataGridView.ColumnHeadersHeight = 34;
        symbolsDataGridView.Columns.AddRange(new DataGridViewColumn[] { rowNumberColumn, symbolTitleColumn, nameColumn, descriptionColumn });
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle9.BackColor = SystemColors.Window;
        dataGridViewCellStyle9.Font = new Font("Tahoma", 9F);
        dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
        symbolsDataGridView.DefaultCellStyle = dataGridViewCellStyle9;
        symbolsDataGridView.Location = new Point(10, 212);
        symbolsDataGridView.MultiSelect = false;
        symbolsDataGridView.Name = "symbolsDataGridView";
        symbolsDataGridView.ReadOnly = true;
        symbolsDataGridView.RightToLeft = RightToLeft.Yes;
        symbolsDataGridView.RowHeadersVisible = false;
        symbolsDataGridView.RowHeadersWidth = 62;
        symbolsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        symbolsDataGridView.Size = new Size(900, 473);
        symbolsDataGridView.TabIndex = 0;
        // 
        // rowNumberColumn
        // 
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
        rowNumberColumn.DefaultCellStyle = dataGridViewCellStyle2;
        rowNumberColumn.FillWeight = 45F;
        rowNumberColumn.HeaderText = "ردیف";
        rowNumberColumn.MinimumWidth = 45;
        rowNumberColumn.Name = "rowNumberColumn";
        rowNumberColumn.ReadOnly = true;
        rowNumberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // symbolTitleColumn
        // 
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
        symbolTitleColumn.DefaultCellStyle = dataGridViewCellStyle3;
        symbolTitleColumn.HeaderText = "نماد";
        symbolTitleColumn.MinimumWidth = 8;
        symbolTitleColumn.Name = "symbolTitleColumn";
        symbolTitleColumn.ReadOnly = true;
        symbolTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // nameColumn
        // 
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
        nameColumn.DefaultCellStyle = dataGridViewCellStyle4;
        nameColumn.FillWeight = 150F;
        nameColumn.HeaderText = "نام";
        nameColumn.MinimumWidth = 8;
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // descriptionColumn
        // 
        descriptionColumn.HeaderText = "توضیحات";
        descriptionColumn.MinimumWidth = 100;
        descriptionColumn.Name = "descriptionColumn";
        descriptionColumn.ReadOnly = true;
        descriptionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // label00
        // 
        label00.Location = new Point(112, 18);
        label00.Name = "label00";
        label00.Size = new Size(57, 30);
        label00.TabIndex = 16;
        label00.Text = "نماد";
        label00.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label10
        // 
        label10.Location = new Point(361, 18);
        label10.Name = "label10";
        label10.Size = new Size(90, 30);
        label10.TabIndex = 17;
        label10.Text = "نام";
        label10.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label30
        // 
        label30.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label30.Location = new Point(916, 9);
        label30.Name = "label30";
        label30.Size = new Size(100, 30);
        label30.TabIndex = 19;
        label30.Text = "ساختار بازار";
        label30.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label11
        // 
        label11.Location = new Point(112, 75);
        label11.Name = "label11";
        label11.Size = new Size(57, 30);
        label11.TabIndex = 21;
        label11.Text = "توضیحات";
        label11.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label12
        // 
        label12.Visible = false;
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
        Controls.Add(descriptionTextBox);
        Controls.Add(newButton);
        Controls.Add(saveButton);
        Controls.Add(deleteButton);
        Controls.Add(deleteAllButton);
        Controls.Add(importButton);
        Controls.Add(exportButton);
        Controls.Add(closeButton);
        Controls.Add(countLabel);
        Controls.Add(symbolSearchTextBox);
        Controls.Add(symbolsDataGridView);
        Controls.Add(label00);
        Controls.Add(label10);
        Controls.Add(label30);
        Controls.Add(label11);
        Controls.Add(label12);
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
    private ToolTip toolTip1;
    private Button exportButton;
    private DataGridViewTextBoxColumn rowNumberColumn;
    private DataGridViewTextBoxColumn symbolTitleColumn;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn descriptionColumn;
}
