namespace Trade.It
{
    partial class PortfolioDefinitionForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainTable;
        private TableLayoutPanel nameTable;
        private Label portfolioNameLabel;
        private TextBox portfolioNameTextBox;
        private TableLayoutPanel sourceTable;
        private Label sourceLabel;
        private RadioButton fileNameRadioButton;
        private RadioButton insideFileRadioButton;
        private TableLayoutPanel pathTable;
        private Label dataPathLabel;
        private TextBox dataPathTextBox;
        private Button browseButton;
        private TableLayoutPanel optionsTable;
        private Label separatorLabel;
        private ComboBox separatorComboBox;
        private Label calendarLabel;
        private ComboBox calendarComboBox;
        private Label timeFormatLabel;
        private ComboBox timeFormatComboBox;
        private Label dateFormatLabel;
        private ComboBox dateFormatComboBox;
        private CheckBox headerCheckBox;
        private CheckBox noDateTimeCheckBox;
        private TableLayoutPanel middleTable;
        private TableLayoutPanel mappingTable;
        private Label mappingTitleLabel;
        private DataGridView mappingGrid;
        private TableLayoutPanel symbolSelectionTable;
        private Label symbolSelectionTitleLabel;
        private TableLayoutPanel symbolTable;
        private TextBox symbolSearchTextBox;
        private Button selectAllButton;
        private Button deselectAllButton;
        private Label selectedCountLabel;
        private DataGridView symbolGrid;
        private Label previewTitleLabel;
        private DataGridView previewGrid;
        private TableLayoutPanel footerTable;
        private Button testMappingButton;
        private Button cancelButton;
        private Button saveButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainTable = new TableLayoutPanel();
            nameTable = new TableLayoutPanel();
            portfolioNameLabel = new Label();
            portfolioNameTextBox = new TextBox();
            sourceTable = new TableLayoutPanel();
            sourceLabel = new Label();
            fileNameRadioButton = new RadioButton();
            insideFileRadioButton = new RadioButton();
            pathTable = new TableLayoutPanel();
            dataPathLabel = new Label();
            dataPathTextBox = new TextBox();
            browseButton = new Button();
            optionsTable = new TableLayoutPanel();
            separatorLabel = new Label();
            separatorComboBox = new ComboBox();
            calendarLabel = new Label();
            calendarComboBox = new ComboBox();
            timeFormatLabel = new Label();
            timeFormatComboBox = new ComboBox();
            dateFormatLabel = new Label();
            dateFormatComboBox = new ComboBox();
            headerCheckBox = new CheckBox();
            noDateTimeCheckBox = new CheckBox();
            middleTable = new TableLayoutPanel();
            mappingTable = new TableLayoutPanel();
            mappingTitleLabel = new Label();
            mappingGrid = new DataGridView();
            symbolSelectionTable = new TableLayoutPanel();
            symbolSelectionTitleLabel = new Label();
            symbolTable = new TableLayoutPanel();
            selectAllButton = new Button();
            deselectAllButton = new Button();
            symbolSearchTextBox = new TextBox();
            selectedCountLabel = new Label();
            symbolGrid = new DataGridView();
            previewTitleLabel = new Label();
            previewGrid = new DataGridView();
            footerTable = new TableLayoutPanel();
            testMappingButton = new Button();
            cancelButton = new Button();
            saveButton = new Button();
            dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            mainTable.SuspendLayout();
            nameTable.SuspendLayout();
            sourceTable.SuspendLayout();
            pathTable.SuspendLayout();
            optionsTable.SuspendLayout();
            middleTable.SuspendLayout();
            mappingTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mappingGrid).BeginInit();
            symbolSelectionTable.SuspendLayout();
            symbolTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)symbolGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            footerTable.SuspendLayout();
            SuspendLayout();
            // 
            // mainTable
            // 
            mainTable.ColumnCount = 1;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTable.Controls.Add(nameTable, 0, 0);
            mainTable.Controls.Add(sourceTable, 0, 1);
            mainTable.Controls.Add(pathTable, 0, 2);
            mainTable.Controls.Add(optionsTable, 0, 3);
            mainTable.Controls.Add(middleTable, 0, 4);
            mainTable.Controls.Add(previewTitleLabel, 0, 5);
            mainTable.Controls.Add(previewGrid, 0, 6);
            mainTable.Controls.Add(footerTable, 0, 7);
            mainTable.Dock = DockStyle.Fill;
            mainTable.Location = new Point(0, 0);
            mainTable.Name = "mainTable";
            mainTable.Padding = new Padding(12);
            mainTable.RowCount = 8;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            mainTable.Size = new Size(1500, 900);
            mainTable.TabIndex = 0;
            // 
            // nameTable
            // 
            nameTable.ColumnCount = 2;
            nameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            nameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nameTable.Controls.Add(portfolioNameLabel, 0, 0);
            nameTable.Controls.Add(portfolioNameTextBox, 1, 0);
            nameTable.Dock = DockStyle.Fill;
            nameTable.Location = new Point(15, 15);
            nameTable.Name = "nameTable";
            nameTable.RowCount = 1;
            nameTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            nameTable.Size = new Size(1470, 42);
            nameTable.TabIndex = 0;
            // 
            // portfolioNameLabel
            // 
            portfolioNameLabel.Dock = DockStyle.Fill;
            portfolioNameLabel.Location = new Point(1373, 0);
            portfolioNameLabel.Name = "portfolioNameLabel";
            portfolioNameLabel.Size = new Size(94, 42);
            portfolioNameLabel.TabIndex = 0;
            portfolioNameLabel.Text = "نام سبد:";
            portfolioNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // portfolioNameTextBox
            // 
            portfolioNameTextBox.Dock = DockStyle.Fill;
            portfolioNameTextBox.Location = new Point(4, 4);
            portfolioNameTextBox.Margin = new Padding(4);
            portfolioNameTextBox.Name = "portfolioNameTextBox";
            portfolioNameTextBox.Size = new Size(1362, 34);
            portfolioNameTextBox.TabIndex = 1;
            // 
            // sourceTable
            // 
            sourceTable.ColumnCount = 3;
            sourceTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            sourceTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            sourceTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sourceTable.Controls.Add(sourceLabel, 0, 0);
            sourceTable.Controls.Add(fileNameRadioButton, 1, 0);
            sourceTable.Controls.Add(insideFileRadioButton, 2, 0);
            sourceTable.Dock = DockStyle.Fill;
            sourceTable.Location = new Point(15, 63);
            sourceTable.Name = "sourceTable";
            sourceTable.RowCount = 1;
            sourceTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            sourceTable.Size = new Size(1470, 36);
            sourceTable.TabIndex = 1;
            // 
            // sourceLabel
            // 
            sourceLabel.Dock = DockStyle.Fill;
            sourceLabel.Location = new Point(1353, 0);
            sourceLabel.Name = "sourceLabel";
            sourceLabel.Size = new Size(114, 36);
            sourceLabel.TabIndex = 0;
            sourceLabel.Text = "منبع نام نماد:";
            sourceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fileNameRadioButton
            // 
            fileNameRadioButton.Anchor = AnchorStyles.Left;
            fileNameRadioButton.AutoSize = true;
            fileNameRadioButton.Checked = true;
            fileNameRadioButton.Location = new Point(1246, 3);
            fileNameRadioButton.Name = "fileNameRadioButton";
            fileNameRadioButton.Size = new Size(101, 30);
            fileNameRadioButton.TabIndex = 1;
            fileNameRadioButton.TabStop = true;
            fileNameRadioButton.Text = "نام فایل";
            // 
            // insideFileRadioButton
            // 
            insideFileRadioButton.Anchor = AnchorStyles.Left;
            insideFileRadioButton.AutoSize = true;
            insideFileRadioButton.Location = new Point(1080, 3);
            insideFileRadioButton.Name = "insideFileRadioButton";
            insideFileRadioButton.Size = new Size(117, 30);
            insideFileRadioButton.TabIndex = 2;
            insideFileRadioButton.Text = "داخل فایل";
            // 
            // pathTable
            // 
            pathTable.ColumnCount = 3;
            pathTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pathTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pathTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            pathTable.Controls.Add(dataPathLabel, 0, 0);
            pathTable.Controls.Add(dataPathTextBox, 1, 0);
            pathTable.Controls.Add(browseButton, 2, 0);
            pathTable.Dock = DockStyle.Fill;
            pathTable.Location = new Point(15, 105);
            pathTable.Name = "pathTable";
            pathTable.RowCount = 1;
            pathTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pathTable.Size = new Size(1470, 42);
            pathTable.TabIndex = 2;
            // 
            // dataPathLabel
            // 
            dataPathLabel.Dock = DockStyle.Fill;
            dataPathLabel.Location = new Point(1373, 0);
            dataPathLabel.Name = "dataPathLabel";
            dataPathLabel.Size = new Size(94, 42);
            dataPathLabel.TabIndex = 0;
            dataPathLabel.Text = "مسیر داده:";
            dataPathLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataPathTextBox
            // 
            dataPathTextBox.Dock = DockStyle.Fill;
            dataPathTextBox.Location = new Point(144, 4);
            dataPathTextBox.Margin = new Padding(4);
            dataPathTextBox.Name = "dataPathTextBox";
            dataPathTextBox.Size = new Size(1222, 34);
            dataPathTextBox.TabIndex = 1;
            // 
            // browseButton
            // 
            browseButton.Dock = DockStyle.Fill;
            browseButton.Location = new Point(4, 4);
            browseButton.Margin = new Padding(4);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(132, 34);
            browseButton.TabIndex = 2;
            browseButton.Text = "انتخاب پوشه...";
            // 
            // optionsTable
            // 
            optionsTable.ColumnCount = 10;
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            optionsTable.Controls.Add(separatorLabel, 0, 0);
            optionsTable.Controls.Add(separatorComboBox, 1, 0);
            optionsTable.Controls.Add(calendarLabel, 2, 0);
            optionsTable.Controls.Add(calendarComboBox, 3, 0);
            optionsTable.Controls.Add(timeFormatLabel, 4, 0);
            optionsTable.Controls.Add(timeFormatComboBox, 5, 0);
            optionsTable.Controls.Add(dateFormatLabel, 6, 0);
            optionsTable.Controls.Add(dateFormatComboBox, 7, 0);
            optionsTable.Controls.Add(headerCheckBox, 8, 0);
            optionsTable.Controls.Add(noDateTimeCheckBox, 9, 0);
            optionsTable.Dock = DockStyle.Fill;
            optionsTable.Location = new Point(15, 153);
            optionsTable.Name = "optionsTable";
            optionsTable.RowCount = 1;
            optionsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            optionsTable.Size = new Size(1470, 58);
            optionsTable.TabIndex = 3;
            // 
            // separatorLabel
            // 
            separatorLabel.Dock = DockStyle.Fill;
            separatorLabel.Location = new Point(1403, 0);
            separatorLabel.Name = "separatorLabel";
            separatorLabel.Size = new Size(64, 58);
            separatorLabel.TabIndex = 0;
            separatorLabel.Text = "جداکننده:";
            separatorLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // separatorComboBox
            // 
            separatorComboBox.Dock = DockStyle.Fill;
            separatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            separatorComboBox.Items.AddRange(new object[] { "Comma (,)", "Semicolon (;)", "Tab", "Pipe (|)" });
            separatorComboBox.Location = new Point(1258, 3);
            separatorComboBox.Name = "separatorComboBox";
            separatorComboBox.Size = new Size(139, 36);
            separatorComboBox.TabIndex = 1;
            // 
            // calendarLabel
            // 
            calendarLabel.Dock = DockStyle.Fill;
            calendarLabel.Location = new Point(1203, 0);
            calendarLabel.Name = "calendarLabel";
            calendarLabel.Size = new Size(49, 58);
            calendarLabel.TabIndex = 2;
            calendarLabel.Text = "تقویم:";
            calendarLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // calendarComboBox
            // 
            calendarComboBox.Dock = DockStyle.Fill;
            calendarComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            calendarComboBox.Items.AddRange(new object[] { "شمسی (Persian)", "لاتین (Gregorian)" });
            calendarComboBox.Location = new Point(1058, 3);
            calendarComboBox.Name = "calendarComboBox";
            calendarComboBox.Size = new Size(139, 36);
            calendarComboBox.TabIndex = 3;
            // 
            // timeFormatLabel
            // 
            timeFormatLabel.Dock = DockStyle.Fill;
            timeFormatLabel.Location = new Point(998, 0);
            timeFormatLabel.Name = "timeFormatLabel";
            timeFormatLabel.Size = new Size(54, 58);
            timeFormatLabel.TabIndex = 4;
            timeFormatLabel.Text = "زمان:";
            timeFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // timeFormatComboBox
            // 
            timeFormatComboBox.Dock = DockStyle.Fill;
            timeFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            timeFormatComboBox.Items.AddRange(new object[] { "HHMMSS", "HH:MM:SS", "HHMM" });
            timeFormatComboBox.Location = new Point(863, 3);
            timeFormatComboBox.Name = "timeFormatComboBox";
            timeFormatComboBox.Size = new Size(129, 36);
            timeFormatComboBox.TabIndex = 5;
            // 
            // dateFormatLabel
            // 
            dateFormatLabel.Dock = DockStyle.Fill;
            dateFormatLabel.Location = new Point(803, 0);
            dateFormatLabel.Name = "dateFormatLabel";
            dateFormatLabel.Size = new Size(54, 58);
            dateFormatLabel.TabIndex = 6;
            dateFormatLabel.Text = "تاریخ:";
            dateFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateFormatComboBox
            // 
            dateFormatComboBox.Dock = DockStyle.Fill;
            dateFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dateFormatComboBox.Items.AddRange(new object[] { "YYYYMMDD", "YYYY-MM-DD", "YYYY/MM/DD" });
            dateFormatComboBox.Location = new Point(668, 3);
            dateFormatComboBox.Name = "dateFormatComboBox";
            dateFormatComboBox.Size = new Size(129, 36);
            dateFormatComboBox.TabIndex = 7;
            // 
            // headerCheckBox
            // 
            headerCheckBox.AutoSize = true;
            headerCheckBox.Location = new Point(493, 3);
            headerCheckBox.Name = "headerCheckBox";
            headerCheckBox.Size = new Size(169, 32);
            headerCheckBox.TabIndex = 8;
            headerCheckBox.Text = "سطر اول عنوان ستون‌ها";
            // 
            // noDateTimeCheckBox
            // 
            noDateTimeCheckBox.AutoSize = true;
            noDateTimeCheckBox.Location = new Point(324, 3);
            noDateTimeCheckBox.Name = "noDateTimeCheckBox";
            noDateTimeCheckBox.Size = new Size(163, 32);
            noDateTimeCheckBox.TabIndex = 9;
            noDateTimeCheckBox.Text = "فاقد تاریخ/زمان";
            // 
            // middleTable
            // 
            middleTable.ColumnCount = 2;
            middleTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            middleTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            middleTable.Controls.Add(mappingTable, 0, 0);
            middleTable.Controls.Add(symbolSelectionTable, 1, 0);
            middleTable.Dock = DockStyle.Fill;
            middleTable.Location = new Point(15, 217);
            middleTable.Name = "middleTable";
            middleTable.RowCount = 1;
            middleTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            middleTable.Size = new Size(1470, 435);
            middleTable.TabIndex = 4;
            // 
            // mappingTable
            // 
            mappingTable.ColumnCount = 1;
            mappingTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mappingTable.Controls.Add(mappingTitleLabel, 0, 0);
            mappingTable.Controls.Add(mappingGrid, 0, 1);
            mappingTable.Dock = DockStyle.Fill;
            mappingTable.Location = new Point(856, 3);
            mappingTable.Name = "mappingTable";
            mappingTable.RowCount = 2;
            mappingTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            mappingTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mappingTable.Size = new Size(611, 429);
            mappingTable.TabIndex = 0;
            // 
            // mappingTitleLabel
            // 
            mappingTitleLabel.Dock = DockStyle.Fill;
            mappingTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            mappingTitleLabel.Location = new Point(3, 0);
            mappingTitleLabel.Name = "mappingTitleLabel";
            mappingTitleLabel.Size = new Size(605, 34);
            mappingTitleLabel.TabIndex = 0;
            mappingTitleLabel.Text = "Mapping ستون‌های فایل";
            mappingTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // mappingGrid
            // 
            mappingGrid.AllowUserToAddRows = false;
            mappingGrid.AllowUserToDeleteRows = false;
            mappingGrid.AllowUserToResizeRows = false;
            mappingGrid.BackgroundColor = SystemColors.Window;
            mappingGrid.ColumnHeadersHeight = 32;
            mappingGrid.Dock = DockStyle.Fill;
            mappingGrid.Location = new Point(3, 37);
            mappingGrid.Name = "mappingGrid";
            mappingGrid.RightToLeft = RightToLeft.Yes;
            mappingGrid.RowHeadersVisible = false;
            mappingGrid.RowHeadersWidth = 62;
            mappingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mappingGrid.Size = new Size(605, 389);
            mappingGrid.TabIndex = 1;
            // 
            // symbolSelectionTable
            // 
            symbolSelectionTable.ColumnCount = 1;
            symbolSelectionTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            symbolSelectionTable.Controls.Add(symbolSelectionTitleLabel, 0, 0);
            symbolSelectionTable.Controls.Add(symbolTable, 0, 1);
            symbolSelectionTable.Dock = DockStyle.Fill;
            symbolSelectionTable.Location = new Point(3, 3);
            symbolSelectionTable.Name = "symbolSelectionTable";
            symbolSelectionTable.RowCount = 2;
            symbolSelectionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            symbolSelectionTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            symbolSelectionTable.Size = new Size(847, 429);
            symbolSelectionTable.TabIndex = 1;
            // 
            // symbolSelectionTitleLabel
            // 
            symbolSelectionTitleLabel.Dock = DockStyle.Fill;
            symbolSelectionTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            symbolSelectionTitleLabel.Location = new Point(3, 0);
            symbolSelectionTitleLabel.Name = "symbolSelectionTitleLabel";
            symbolSelectionTitleLabel.Size = new Size(841, 34);
            symbolSelectionTitleLabel.TabIndex = 0;
            symbolSelectionTitleLabel.Text = "انتخاب سهام";
            symbolSelectionTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // symbolTable
            // 
            symbolTable.ColumnCount = 4;
            symbolTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            symbolTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            symbolTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            symbolTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            symbolTable.Controls.Add(selectAllButton, 0, 0);
            symbolTable.Controls.Add(deselectAllButton, 1, 0);
            symbolTable.Controls.Add(symbolSearchTextBox, 2, 0);
            symbolTable.Controls.Add(selectedCountLabel, 3, 0);
            symbolTable.Controls.Add(symbolGrid, 0, 1);
            symbolTable.Dock = DockStyle.Fill;
            symbolTable.Location = new Point(3, 37);
            symbolTable.Name = "symbolTable";
            symbolTable.RowCount = 2;
            symbolTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            symbolTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            symbolTable.Size = new Size(841, 389);
            symbolTable.TabIndex = 1;
            // 
            // selectAllButton
            // 
            selectAllButton.Dock = DockStyle.Fill;
            selectAllButton.Location = new Point(739, 3);
            selectAllButton.Name = "selectAllButton";
            selectAllButton.Size = new Size(99, 32);
            selectAllButton.TabIndex = 0;
            selectAllButton.Text = "انتخاب همه";
            // 
            // deselectAllButton
            // 
            deselectAllButton.Dock = DockStyle.Fill;
            deselectAllButton.Location = new Point(614, 3);
            deselectAllButton.Name = "deselectAllButton";
            deselectAllButton.Size = new Size(119, 32);
            deselectAllButton.TabIndex = 1;
            deselectAllButton.Text = "عدم انتخاب همه";
            // 
            // symbolSearchTextBox
            // 
            symbolSearchTextBox.Dock = DockStyle.Fill;
            symbolSearchTextBox.Location = new Point(173, 3);
            symbolSearchTextBox.Name = "symbolSearchTextBox";
            symbolSearchTextBox.PlaceholderText = "جستجوی نماد...";
            symbolSearchTextBox.Size = new Size(435, 34);
            symbolSearchTextBox.TabIndex = 2;
            // 
            // selectedCountLabel
            // 
            selectedCountLabel.Dock = DockStyle.Fill;
            selectedCountLabel.Location = new Point(3, 0);
            selectedCountLabel.Name = "selectedCountLabel";
            selectedCountLabel.Size = new Size(164, 38);
            selectedCountLabel.TabIndex = 3;
            selectedCountLabel.Text = "انتخاب شده: ۰ از ۰";
            selectedCountLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // symbolGrid
            // 
            symbolGrid.AllowUserToAddRows = false;
            symbolGrid.AllowUserToDeleteRows = false;
            symbolGrid.AllowUserToResizeRows = false;
            symbolGrid.BackgroundColor = SystemColors.Window;
            symbolGrid.ColumnHeadersHeight = 32;
            symbolGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewCheckBoxColumn1, dataGridViewTextBoxColumn1 });
            symbolTable.SetColumnSpan(symbolGrid, 4);
            symbolGrid.Dock = DockStyle.Fill;
            symbolGrid.Location = new Point(3, 41);
            symbolGrid.Name = "symbolGrid";
            symbolGrid.RightToLeft = RightToLeft.Yes;
            symbolGrid.RowHeadersVisible = false;
            symbolGrid.RowHeadersWidth = 62;
            symbolGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            symbolGrid.Size = new Size(835, 345);
            symbolGrid.TabIndex = 4;
            // 
            // previewTitleLabel
            // 
            previewTitleLabel.Dock = DockStyle.Fill;
            previewTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            previewTitleLabel.Location = new Point(15, 655);
            previewTitleLabel.Name = "previewTitleLabel";
            previewTitleLabel.Size = new Size(1470, 34);
            previewTitleLabel.TabIndex = 5;
            previewTitleLabel.Text = "پیش‌نمایش داده";
            previewTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.BackgroundColor = SystemColors.Window;
            previewGrid.ColumnHeadersHeight = 32;
            previewGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            previewGrid.Dock = DockStyle.Fill;
            previewGrid.Location = new Point(15, 692);
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RightToLeft = RightToLeft.Yes;
            previewGrid.RowHeadersVisible = false;
            previewGrid.RowHeadersWidth = 62;
            previewGrid.Size = new Size(1470, 139);
            previewGrid.TabIndex = 6;
            // 
            // footerTable
            // 
            footerTable.ColumnCount = 3;
            footerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            footerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            footerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            footerTable.Controls.Add(testMappingButton, 0, 0);
            footerTable.Controls.Add(cancelButton, 1, 0);
            footerTable.Controls.Add(saveButton, 2, 0);
            footerTable.Dock = DockStyle.Fill;
            footerTable.Location = new Point(15, 837);
            footerTable.Name = "footerTable";
            footerTable.RowCount = 1;
            footerTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            footerTable.Size = new Size(1470, 48);
            footerTable.TabIndex = 7;
            // 
            // testMappingButton
            // 
            testMappingButton.Dock = DockStyle.Fill;
            testMappingButton.Location = new Point(1338, 3);
            testMappingButton.Name = "testMappingButton";
            testMappingButton.Size = new Size(129, 42);
            testMappingButton.TabIndex = 0;
            testMappingButton.Text = "تست Mapping";
            // 
            // cancelButton
            // 
            cancelButton.Dock = DockStyle.Fill;
            cancelButton.Location = new Point(1248, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(84, 42);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "لغو";
            // 
            // saveButton
            // 
            saveButton.Dock = DockStyle.Fill;
            saveButton.Location = new Point(3, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(1239, 42);
            saveButton.TabIndex = 2;
            saveButton.Text = "ذخیره سبد";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.MinimumWidth = 8;
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            dataGridViewCheckBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 150;
            // 
            // PortfolioDefinitionForm
            // 
            ClientSize = new Size(1500, 900);
            Controls.Add(mainTable);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1200, 760);
            Name = "PortfolioDefinitionForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تعریف سبد";
            mainTable.ResumeLayout(false);
            nameTable.ResumeLayout(false);
            nameTable.PerformLayout();
            sourceTable.ResumeLayout(false);
            sourceTable.PerformLayout();
            pathTable.ResumeLayout(false);
            pathTable.PerformLayout();
            optionsTable.ResumeLayout(false);
            optionsTable.PerformLayout();
            middleTable.ResumeLayout(false);
            mappingTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mappingGrid).EndInit();
            symbolSelectionTable.ResumeLayout(false);
            symbolTable.ResumeLayout(false);
            symbolTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)symbolGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            footerTable.ResumeLayout(false);
            ResumeLayout(false);
        }
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
