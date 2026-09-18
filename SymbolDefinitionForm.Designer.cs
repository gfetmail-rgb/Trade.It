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
    private ComboBox groupComboBox = null!;
    private ComboBox industryGroupComboBox = null!;
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
    private Label label20 = null!;
    private Label label30 = null!;
    private Label label01 = null!;
    private Label label11 = null!;
    private Label label21 = null!;
    private Label label22 = null!;

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
        exchangeComboBox = new ComboBox();
        marketComboBox = new ComboBox();
        boardComboBox = new ComboBox();
        assetComboBox = new ComboBox();
        groupComboBox = new ComboBox();
        industryGroupComboBox = new ComboBox();
        newButton = new Button();
        saveButton = new Button();
        deleteButton = new Button();
        deleteAllButton = new Button();
        importButton = new Button();
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
        fundTypeColumn = new DataGridViewTextBoxColumn();
        industryGroupColumn = new DataGridViewTextBoxColumn();
        label00 = new Label();
        label10 = new Label();
        label20 = new Label();
        label30 = new Label();
        label01 = new Label();
        label11 = new Label();
        label21 = new Label();
        label22 = new Label();
        label1 = new Label();
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
        // exchangeComboBox
        // 
        exchangeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        exchangeComboBox.Items.AddRange(new object[] { "-", "بورس", "فرابورس", "بورس کالا", "بورس انرژی" });
        exchangeComboBox.Location = new Point(814, 20);
        exchangeComboBox.Name = "exchangeComboBox";
        exchangeComboBox.Size = new Size(180, 30);
        exchangeComboBox.TabIndex = 2;
        // 
        // marketComboBox
        // 
        marketComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        marketComboBox.Items.AddRange(new object[] { "-", "اول", "دوم", "پایه", "SME", "نوآفرین" });
        marketComboBox.Location = new Point(1075, 20);
        marketComboBox.Name = "marketComboBox";
        marketComboBox.Size = new Size(186, 30);
        marketComboBox.TabIndex = 3;
        // 
        // boardComboBox
        // 
        boardComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        boardComboBox.Items.AddRange(new object[] { "-", "اصلی", "فرعی", "رشد", "دانش بنیان", "زرد", "نارنجی", "قرمز" });
        boardComboBox.Location = new Point(75, 76);
        boardComboBox.Name = "boardComboBox";
        boardComboBox.Size = new Size(176, 30);
        boardComboBox.TabIndex = 4;
        // 
        // assetComboBox
        // 
        assetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        assetComboBox.Items.AddRange(new object[] { "-", "سهام", "صندوق ETF سهامی", "صندوق ETF کالایی" });
        assetComboBox.Location = new Point(359, 76);
        assetComboBox.Name = "assetComboBox";
        assetComboBox.Size = new Size(175, 30);
        assetComboBox.TabIndex = 5;
        // 
        // groupComboBox
        // 
        groupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        groupComboBox.Items.AddRange(new object[] { "-", "سهامی", "اهرمی", "شاخصی", "بخشی", "مختلط", "در صندوق", "تضمین اصل سرمایه", "رمین و ساختمان", "پروژه", "جسورانه", "خصوصی", "درآمد ثابت", "مبتنی بر سپرده کالایی - طلا", "مبتنی بر سپرده کالایی - نقره", "مبتنی بر سپرده کالایی - انرژی", "مبتنی بر سپرده کالایی - کشاورزی" });
        groupComboBox.Location = new Point(653, 75);
        groupComboBox.Name = "groupComboBox";
        groupComboBox.Size = new Size(247, 30);
        groupComboBox.TabIndex = 6;
        // 
        // industryGroupComboBox
        // 
        industryGroupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        industryGroupComboBox.Items.AddRange(new object[] { "-", "ابزار پزشکی، اپتیکی و اندازه‌گیری", "استخراج زغال سنگ", "استخراج سایر معادن", "استخراج کانه های فلزی", "استخراج نفت گاز و خدمات جنبی جز اکتشاف", "اطلاعات و ارتباطات", "انبوه سازی، املاک و مستغلات", "انتشار، چاپ و تکثیر", "بانكها و موسسات اعتباری", "بیمه و صندوق بازنشستگی به جز تامین اجتماعی", "پیمانكاری صنعتی", "تجارت عمده فروشی به جز وسایل نقلیه موتور", "تولید محصولات كامپیوتری الكترونیكی ونوری", "حمل و نقل آبی", "حمل ونقل، انبارداری و ارتباطات", "خدمات فنی و مهندسی", "خرده فروشی،باستثنای وسایل نقلیه موتوری", "خودرو و ساخت قطعات", "دباغی، پرداخت چرم و ساخت انواع پاپوش", "رایانه و فعالیت‌های وابسته به آن", "زراعت و خدمات وابسته", "ساخت دستگاه‌ها و وسایل ارتباطی", "ساخت محصولات فلزی", "سایر محصولات كانی غیرفلزی", "سایر واسطه گری های مالی", "سرمایه گذاری ها", "سلامت انسان و مددكاری اجتماعی", "سیمان، آهك و گچ", "شرکتهای چند رشته ای صنعتی", "عرضه برق، گاز، بخار و آب گرم", "فراورده های نفتی، كك و سوخت هسته ای", "فعالیت مهندسی، تجزیه، تحلیل و آزمایش فنی", "فعالیت های هنری، سرگرمی و خلاقانه", "فعالیتهای فرهنگی و ورزشی", "فعالیتهای كمكی به نهادهای مالی واسط", "فلزات اساسی", "قند و شكر", "کاشی و سرامیک", "لاستیك و پلاستیك", "ماشین آلات و تجهیزات", "ماشین آلات و دستگاه‌های برقی", "محصولات چوبی", "محصولات شیمیایی", "محصولات غذایی و آشامیدنی به جز قند و شكر", "محصولات كاغذی", "مخابرات", "منسوجات", "مواد و محصولات دارویی", "واسطه‌گری های مالی و پولی", "هتل و رستوران" });
        industryGroupComboBox.Location = new Point(1008, 75);
        industryGroupComboBox.Name = "industryGroupComboBox";
        industryGroupComboBox.Size = new Size(253, 30);
        industryGroupComboBox.TabIndex = 7;
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
        // closeButton
        // 
        closeButton.AutoSize = true;
        closeButton.Location = new Point(1186, 118);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(75, 35);
        closeButton.TabIndex = 13;
        closeButton.Text = "بستن";
        closeButton.UseVisualStyleBackColor = true;
        // 
        // countLabel
        // 
        countLabel.AutoSize = true;
        countLabel.Location = new Point(22, 131);
        countLabel.Name = "countLabel";
        countLabel.Size = new Size(68, 22);
        countLabel.TabIndex = 14;
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
        rowNumberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // symbolTitleColumn
        // 
        symbolTitleColumn.HeaderText = "نماد";
        symbolTitleColumn.MinimumWidth = 8;
        symbolTitleColumn.Name = "symbolTitleColumn";
        symbolTitleColumn.ReadOnly = true;
        symbolTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // nameColumn
        // 
        nameColumn.FillWeight = 150F;
        nameColumn.HeaderText = "نام";
        nameColumn.MinimumWidth = 8;
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // exchangeColumn
        // 
        exchangeColumn.HeaderText = "بورس";
        exchangeColumn.MinimumWidth = 8;
        exchangeColumn.Name = "exchangeColumn";
        exchangeColumn.ReadOnly = true;
        exchangeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // marketColumn
        // 
        marketColumn.HeaderText = "بازار";
        marketColumn.MinimumWidth = 8;
        marketColumn.Name = "marketColumn";
        marketColumn.ReadOnly = true;
        marketColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // boardColumn
        // 
        boardColumn.HeaderText = "تابلو";
        boardColumn.MinimumWidth = 8;
        boardColumn.Name = "boardColumn";
        boardColumn.ReadOnly = true;
        boardColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // assetColumn
        // 
        assetColumn.FillWeight = 80F;
        assetColumn.HeaderText = "دارایی";
        assetColumn.MinimumWidth = 8;
        assetColumn.Name = "assetColumn";
        assetColumn.ReadOnly = true;
        assetColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // fundTypeColumn
        // 
        fundTypeColumn.FillWeight = 125F;
        fundTypeColumn.HeaderText = "نوع صندوق";
        fundTypeColumn.MinimumWidth = 8;
        fundTypeColumn.Name = "fundTypeColumn";
        fundTypeColumn.ReadOnly = true;
        fundTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // industryGroupColumn
        // 
        industryGroupColumn.FillWeight = 125F;
        industryGroupColumn.HeaderText = "گروه صنعت";
        industryGroupColumn.MinimumWidth = 8;
        industryGroupColumn.Name = "industryGroupColumn";
        industryGroupColumn.ReadOnly = true;
        industryGroupColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // label00
        // 
        label00.Location = new Point(12, 18);
        label00.Name = "label00";
        label00.Size = new Size(57, 30);
        label00.TabIndex = 15;
        label00.Text = "نماد";
        label00.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label10
        // 
        label10.Location = new Point(229, 19);
        label10.Name = "label10";
        label10.Size = new Size(90, 30);
        label10.TabIndex = 16;
        label10.Text = "نام";
        label10.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label20
        // 
        label20.Location = new Point(701, 19);
        label20.Name = "label20";
        label20.Size = new Size(113, 30);
        label20.TabIndex = 17;
        label20.Text = "بورس";
        label20.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label30
        // 
        label30.Location = new Point(997, 20);
        label30.Name = "label30";
        label30.Size = new Size(76, 30);
        label30.TabIndex = 18;
        label30.Text = "بازار";
        label30.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label01
        // 
        label01.Location = new Point(22, 75);
        label01.Name = "label01";
        label01.Size = new Size(47, 30);
        label01.TabIndex = 19;
        label01.Text = "تابلو";
        label01.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label11
        // 
        label11.Location = new Point(262, 76);
        label11.Name = "label11";
        label11.Size = new Size(90, 30);
        label11.TabIndex = 20;
        label11.Text = "دارایی";
        label11.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label21
        // 
        label21.Location = new Point(536, 75);
        label21.Name = "label21";
        label21.Size = new Size(108, 30);
        label21.TabIndex = 21;
        label21.Text = "نوع صندوق";
        label21.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label22
        // 
        label22.Location = new Point(560, 118);
        label22.Name = "label22";
        label22.Size = new Size(130, 30);
        label22.TabIndex = 22;
        label22.Text = "گروه صنعت";
        label22.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label1
        // 
        label1.Location = new Point(901, 76);
        label1.Name = "label1";
        label1.Size = new Size(101, 30);
        label1.TabIndex = 23;
        label1.Text = "گروه صنعت";
        label1.TextAlign = ContentAlignment.MiddleRight;
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
        Controls.Add(label1);
        Controls.Add(symbolTextBox);
        Controls.Add(nameTextBox);
        Controls.Add(exchangeComboBox);
        Controls.Add(marketComboBox);
        Controls.Add(boardComboBox);
        Controls.Add(assetComboBox);
        Controls.Add(groupComboBox);
        Controls.Add(industryGroupComboBox);
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
        Controls.Add(label22);
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
    private DataGridViewTextBoxColumn rowNumberColumn;
    private DataGridViewTextBoxColumn symbolTitleColumn;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn exchangeColumn;
    private DataGridViewTextBoxColumn marketColumn;
    private DataGridViewTextBoxColumn boardColumn;
    private DataGridViewTextBoxColumn assetColumn;
    private DataGridViewTextBoxColumn fundTypeColumn;
    private DataGridViewTextBoxColumn industryGroupColumn;
}
