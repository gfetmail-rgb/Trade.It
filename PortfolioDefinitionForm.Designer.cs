namespace Trade.It
{
    partial class PortfolioDefinitionForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainTable;
        private TextBox portfolioNameTextBox;
        private GroupBox sourceGroup;
        private RadioButton fileNameRadioButton;
        private RadioButton insideFileRadioButton;
        private Label symbolSourceLabel;
        private TextBox dataPathTextBox;
        private Button browseButton;
        private ComboBox separatorComboBox;
        private ComboBox calendarComboBox;
        private CheckBox headerCheckBox;
        private CheckBox noDateTimeCheckBox;
        private CheckBox timeFormatCheckBox;
        private ComboBox timeFormatComboBox;
        private CheckBox dateFormatCheckBox;
        private ComboBox dateFormatComboBox;
        private GroupBox symbolSelectionGroup;
        private Button deselectAllButton;
        private Button selectAllButton;
        private TextBox symbolSearchTextBox;
        private Label selectedCountLabel;
        private DataGridView symbolGrid;
        private GroupBox mappingGroup;
        private DataGridView mappingGrid;
        private GroupBox previewGroup;
        private DataGridView previewGrid;
        private Button saveButton;
        private Button cancelButton;
        private Button testMappingButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainTable = new TableLayoutPanel();
            portfolioNameTextBox = new TextBox();
            sourceGroup = new GroupBox();
            fileNameRadioButton = new RadioButton();
            insideFileRadioButton = new RadioButton();
            symbolSourceLabel = new Label();
            dataPathTextBox = new TextBox();
            browseButton = new Button();
            separatorComboBox = new ComboBox();
            calendarComboBox = new ComboBox();
            headerCheckBox = new CheckBox();
            noDateTimeCheckBox = new CheckBox();
            timeFormatCheckBox = new CheckBox();
            timeFormatComboBox = new ComboBox();
            dateFormatCheckBox = new CheckBox();
            dateFormatComboBox = new ComboBox();
            symbolSelectionGroup = new GroupBox();
            deselectAllButton = new Button();
            selectAllButton = new Button();
            symbolSearchTextBox = new TextBox();
            selectedCountLabel = new Label();
            symbolGrid = new DataGridView();
            mappingGroup = new GroupBox();
            mappingGrid = new DataGridView();
            previewGroup = new GroupBox();
            previewGrid = new DataGridView();
            saveButton = new Button();
            cancelButton = new Button();
            testMappingButton = new Button();
            SuspendLayout();

            Text = "تعریف سبد";
            StartPosition = FormStartPosition.CenterParent;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            MinimumSize = new Size(1150, 760);
            ClientSize = new Size(1500, 920);
            Font = new Font("Segoe UI", 10F);

            mainTable.Dock = DockStyle.Fill;
            mainTable.Padding = new Padding(14, 10, 14, 70);
            mainTable.ColumnCount = 1;
            mainTable.RowCount = 7;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 165F));

            var namePanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes };
            namePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            namePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            namePanel.Controls.Add(new Label { Text = "نام سبد:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4) }, 0, 0);
            namePanel.Controls.Add(portfolioNameTextBox, 1, 0);
            portfolioNameTextBox.Dock = DockStyle.Fill;
            portfolioNameTextBox.Margin = new Padding(4);
            mainTable.Controls.Add(namePanel, 0, 0);

            sourceGroup.Text = "منبع اطلاعات 1";
            sourceGroup.Dock = DockStyle.Fill;
            sourceGroup.Padding = new Padding(10);
            fileNameRadioButton.Text = "نام فایل";
            fileNameRadioButton.Checked = true;
            fileNameRadioButton.AutoSize = true;
            fileNameRadioButton.Location = new Point(30, 30);
            insideFileRadioButton.Text = "داخل فایل";
            insideFileRadioButton.AutoSize = true;
            insideFileRadioButton.Location = new Point(145, 30);
            sourceGroup.Controls.Add(fileNameRadioButton);
            sourceGroup.Controls.Add(insideFileRadioButton);
            mainTable.Controls.Add(sourceGroup, 0, 1);

            symbolSourceLabel.Text = "منبع نام نماد: فایل‌های موجود در مسیر انتخاب شده";
            symbolSourceLabel.Dock = DockStyle.Fill;
            symbolSourceLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainTable.Controls.Add(symbolSourceLabel, 0, 2);

            var pathPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, RightToLeft = RightToLeft.Yes };
            pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pathPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            pathPanel.Controls.Add(new Label { Text = "مسیر داده:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4) }, 0, 0);
            pathPanel.Controls.Add(dataPathTextBox, 1, 0);
            pathPanel.Controls.Add(browseButton, 2, 0);
            dataPathTextBox.Dock = DockStyle.Fill; dataPathTextBox.Margin = new Padding(4);
            browseButton.Text = "انتخاب پوشه..."; browseButton.Dock = DockStyle.Fill; browseButton.Margin = new Padding(4);
            mainTable.Controls.Add(pathPanel, 0, 3);

            var generalPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 1, RightToLeft = RightToLeft.Yes };
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 185F));
            generalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            generalPanel.Controls.Add(new Label { Text = "جداکننده:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            separatorComboBox.Items.AddRange(new object[] { "Comma (,)", "Semicolon (;)" , "Tab", "Pipe (|)" }); separatorComboBox.SelectedIndex = 0; separatorComboBox.Dock = DockStyle.Fill; separatorComboBox.Margin = new Padding(4); generalPanel.Controls.Add(separatorComboBox, 1, 0);
            generalPanel.Controls.Add(new Label { Text = "تقویم:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 0);
            calendarComboBox.Items.AddRange(new object[] { "شمسی (Persian)", "لاتین (Gregorian)" }); calendarComboBox.SelectedIndex = 0; calendarComboBox.Dock = DockStyle.Fill; calendarComboBox.Margin = new Padding(4); generalPanel.Controls.Add(calendarComboBox, 3, 0);
            headerCheckBox.Text = "سطر اول عنوان ستون‌ها"; headerCheckBox.AutoSize = true; generalPanel.Controls.Add(headerCheckBox, 4, 0);
            noDateTimeCheckBox.Text = "داده فاقد تاریخ/زمان است"; noDateTimeCheckBox.AutoSize = true; generalPanel.Controls.Add(noDateTimeCheckBox, 5, 0);
            mainTable.Controls.Add(generalPanel, 0, 4);

            var formatPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, RightToLeft = RightToLeft.Yes };
            formatPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); formatPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F)); formatPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); formatPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
            timeFormatCheckBox.Text = "فرمت زمان:"; timeFormatCheckBox.AutoSize = true; formatPanel.Controls.Add(timeFormatCheckBox, 0, 0);
            timeFormatComboBox.Items.AddRange(new object[] { "HHMMSS", "HH:MM:SS", "HHMM" }); timeFormatComboBox.SelectedIndex = 0; timeFormatComboBox.Dock = DockStyle.Fill; timeFormatComboBox.Margin = new Padding(4); formatPanel.Controls.Add(timeFormatComboBox, 1, 0);
            dateFormatCheckBox.Text = "فرمت تاریخ:"; dateFormatCheckBox.AutoSize = true; formatPanel.Controls.Add(dateFormatCheckBox, 2, 0);
            dateFormatComboBox.Items.AddRange(new object[] { "YYYYMMDD", "YYYY-MM-DD", "YYYY/MM/DD" }); dateFormatComboBox.SelectedIndex = 0; dateFormatComboBox.Dock = DockStyle.Fill; dateFormatComboBox.Margin = new Padding(4); formatPanel.Controls.Add(dateFormatComboBox, 3, 0);
            mainTable.Controls.Add(formatPanel, 0, 5);

            var middle = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes };
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            middle.Controls.Add(mappingGroup, 0, 0);
            middle.Controls.Add(symbolSelectionGroup, 1, 0);
            mappingGroup.Text = "Mapping ستون‌ها"; mappingGroup.Dock = DockStyle.Fill; mappingGroup.Padding = new Padding(8);
            symbolSelectionGroup.Text = "انتخاب سهام"; symbolSelectionGroup.Dock = DockStyle.Fill; symbolSelectionGroup.Padding = new Padding(8);

            var symbolLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes };
            symbolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F)); symbolLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            var symbolTop = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RightToLeft = RightToLeft.Yes };
            symbolTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F)); symbolTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F)); symbolTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); symbolTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            deselectAllButton.Text = "عدم انتخاب همه"; deselectAllButton.Dock = DockStyle.Fill; deselectAllButton.Margin = new Padding(4); symbolTop.Controls.Add(deselectAllButton, 0, 0);
            selectAllButton.Text = "انتخاب همه"; selectAllButton.Dock = DockStyle.Fill; selectAllButton.Margin = new Padding(4); symbolTop.Controls.Add(selectAllButton, 1, 0);
            symbolSearchTextBox.PlaceholderText = "جستجوی نماد..."; symbolSearchTextBox.Dock = DockStyle.Fill; symbolSearchTextBox.Margin = new Padding(4); symbolTop.Controls.Add(symbolSearchTextBox, 2, 0);
            selectedCountLabel.Text = "انتخاب شده: ۰ از ۰"; selectedCountLabel.Dock = DockStyle.Fill; selectedCountLabel.TextAlign = ContentAlignment.MiddleRight; selectedCountLabel.Margin = new Padding(4); symbolTop.Controls.Add(selectedCountLabel, 3, 0);
            symbolLayout.Controls.Add(symbolTop, 0, 0);
            symbolGrid.Dock = DockStyle.Fill; symbolGrid.AllowUserToAddRows = false; symbolGrid.AllowUserToDeleteRows = false; symbolGrid.AutoGenerateColumns = false; symbolGrid.RowHeadersVisible = false; symbolGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; symbolGrid.RightToLeft = RightToLeft.Yes; symbolGrid.BackgroundColor = SystemColors.Window; symbolGrid.BorderStyle = BorderStyle.FixedSingle;
            symbolGrid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "انتخاب", Width = 80, Name = "selectedColumn" });
            symbolGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Name = "symbolColumn", ReadOnly = true });
            symbolLayout.Controls.Add(symbolGrid, 0, 1); symbolSelectionGroup.Controls.Add(symbolLayout);

            mappingGrid.Dock = DockStyle.Fill; mappingGrid.AllowUserToAddRows = false; mappingGrid.AllowUserToDeleteRows = false; mappingGrid.AutoGenerateColumns = false; mappingGrid.RowHeadersVisible = false; mappingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; mappingGrid.RightToLeft = RightToLeft.Yes; mappingGrid.BackgroundColor = SystemColors.Window; mappingGrid.BorderStyle = BorderStyle.FixedSingle; mappingGrid.ScrollBars = ScrollBars.Vertical;
            mappingGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "فیلد استاندارد", Width = 155, Name = "standardField", ReadOnly = true });
            mappingGrid.Columns.Add(new DataGridViewComboBoxColumn { HeaderText = "ستون فایل", Width = 155, Name = "fileColumn" });
            mappingGroup.Controls.Add(mappingGrid);
            mainTable.Controls.Add(middle, 0, 6);

            previewGroup.Text = "پیش‌نمایش داده"; previewGroup.Dock = DockStyle.Fill; previewGroup.Padding = new Padding(8);
            previewGrid.Dock = DockStyle.Fill; previewGrid.AllowUserToAddRows = false; previewGrid.AllowUserToDeleteRows = false; previewGrid.ReadOnly = true; previewGrid.AutoGenerateColumns = false; previewGrid.RowHeadersVisible = false; previewGrid.RightToLeft = RightToLeft.Yes; previewGrid.BackgroundColor = SystemColors.Window; previewGrid.BorderStyle = BorderStyle.FixedSingle;
            previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", Width = 120 });
            previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام فایل", Width = 220 });
            previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ستون‌های موجود در فایل", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            previewGroup.Controls.Add(previewGrid); mainTable.Controls.Add(previewGroup, 0, 7);

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 62, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, RightToLeft = RightToLeft.Yes, Padding = new Padding(0, 6, 0, 0) };
            saveButton.Text = "ذخیره سبد"; saveButton.Width = 125; saveButton.Height = 36; saveButton.Margin = new Padding(4);
            cancelButton.Text = "لغو"; cancelButton.Width = 90; cancelButton.Height = 36; cancelButton.Margin = new Padding(4);
            testMappingButton.Text = "تست Mapping"; testMappingButton.Width = 135; testMappingButton.Height = 36; testMappingButton.Margin = new Padding(4);
            bottom.Controls.Add(saveButton); bottom.Controls.Add(cancelButton); bottom.Controls.Add(testMappingButton);
            Controls.Add(mainTable); Controls.Add(bottom);
            bottom.BringToFront();
            ResumeLayout(false);
        }
    }
}
