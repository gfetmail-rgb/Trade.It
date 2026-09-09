namespace Trade.It
{
    partial class PortfolioDefinitionForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label portfolioNameLabel;
        private TextBox portfolioNameTextBox;
        private RadioButton fileNameRadioButton;
        private RadioButton insideFileRadioButton;
        private Label dataPathLabel;
        private TextBox dataPathTextBox;
        private Button browseButton;
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
        private Label mappingTitleLabel;
        private DataGridView mappingGrid;
        private DataGridViewTextBoxColumn mappingFieldColumn;
        private DataGridViewTextBoxColumn mappingNumberColumn;
        private Label symbolSelectionTitleLabel;
        private TextBox symbolSearchTextBox;
        private Button selectAllButton;
        private Button deselectAllButton;
        private Label selectedCountLabel;
        private DataGridView symbolGrid;
        private DataGridViewCheckBoxColumn selectedColumn;
        private DataGridViewTextBoxColumn symbolColumn;
        private Label previewTitleLabel;
        private DataGridView previewGrid;
        private DataGridViewTextBoxColumn previewColumn1;
        private DataGridViewTextBoxColumn previewColumn2;
        private DataGridViewTextBoxColumn previewColumn3;
        private DataGridViewTextBoxColumn previewColumn4;
        private DataGridViewTextBoxColumn previewColumn5;
        private DataGridViewTextBoxColumn previewColumn6;
        private DataGridViewTextBoxColumn previewColumn7;
        private DataGridViewTextBoxColumn previewColumn8;
        private DataGridViewTextBoxColumn previewColumn9;
        private DataGridViewTextBoxColumn previewColumn10;
        private DataGridViewTextBoxColumn previewColumn11;
        private DataGridViewTextBoxColumn previewColumn12;
        private DataGridViewTextBoxColumn previewColumn13;
        private DataGridViewTextBoxColumn previewColumn14;
        private DataGridViewTextBoxColumn previewColumn15;
        private DataGridViewTextBoxColumn previewColumn16;
        private DataGridViewTextBoxColumn previewColumn17;
        private DataGridViewTextBoxColumn previewColumn18;
        private Button testMappingButton;
        private Button cancelButton;
        private Button saveButton;
        private GroupBox groupBox1;
        private Label sourceLabel;
        private Button resetButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox fileTypeComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            portfolioNameLabel = new Label();
            portfolioNameTextBox = new TextBox();
            fileNameRadioButton = new RadioButton();
            insideFileRadioButton = new RadioButton();
            dataPathLabel = new Label();
            dataPathTextBox = new TextBox();
            browseButton = new Button();
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
            mappingTitleLabel = new Label();
            mappingGrid = new DataGridView();
            mappingFieldColumn = new DataGridViewTextBoxColumn();
            mappingNumberColumn = new DataGridViewTextBoxColumn();
            symbolSelectionTitleLabel = new Label();
            symbolSearchTextBox = new TextBox();
            selectAllButton = new Button();
            deselectAllButton = new Button();
            selectedCountLabel = new Label();
            symbolGrid = new DataGridView();
            selectedColumn = new DataGridViewCheckBoxColumn();
            symbolColumn = new DataGridViewTextBoxColumn();
            previewTitleLabel = new Label();
            previewGrid = new DataGridView();
            previewColumn1 = new DataGridViewTextBoxColumn();
            previewColumn2 = new DataGridViewTextBoxColumn();
            previewColumn3 = new DataGridViewTextBoxColumn();
            previewColumn4 = new DataGridViewTextBoxColumn();
            previewColumn5 = new DataGridViewTextBoxColumn();
            previewColumn6 = new DataGridViewTextBoxColumn();
            previewColumn7 = new DataGridViewTextBoxColumn();
            previewColumn8 = new DataGridViewTextBoxColumn();
            previewColumn9 = new DataGridViewTextBoxColumn();
            previewColumn10 = new DataGridViewTextBoxColumn();
            previewColumn11 = new DataGridViewTextBoxColumn();
            previewColumn12 = new DataGridViewTextBoxColumn();
            previewColumn13 = new DataGridViewTextBoxColumn();
            previewColumn14 = new DataGridViewTextBoxColumn();
            previewColumn15 = new DataGridViewTextBoxColumn();
            previewColumn16 = new DataGridViewTextBoxColumn();
            previewColumn17 = new DataGridViewTextBoxColumn();
            previewColumn18 = new DataGridViewTextBoxColumn();
            testMappingButton = new Button();
            cancelButton = new Button();
            saveButton = new Button();
            groupBox1 = new GroupBox();
            sourceLabel = new Label();
            resetButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            fileTypeComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)mappingGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)symbolGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();

            portfolioNameLabel.Location = new Point(24, 27);
            portfolioNameLabel.Name = "portfolioNameLabel";
            portfolioNameLabel.Size = new Size(100, 32);
            portfolioNameLabel.TabIndex = 0;
            portfolioNameLabel.Text = "نام سبد:";
            portfolioNameLabel.TextAlign = ContentAlignment.MiddleCenter;

            portfolioNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioNameTextBox.Location = new Point(131, 25);
            portfolioNameTextBox.Name = "portfolioNameTextBox";
            portfolioNameTextBox.Size = new Size(605, 34);
            portfolioNameTextBox.TabIndex = 1;

            fileNameRadioButton.AutoSize = true;
            fileNameRadioButton.Checked = true;
            fileNameRadioButton.Location = new Point(168, 22);
            fileNameRadioButton.Name = "fileNameRadioButton";
            fileNameRadioButton.Size = new Size(101, 32);
            fileNameRadioButton.TabIndex = 3;
            fileNameRadioButton.TabStop = true;
            fileNameRadioButton.Text = "نام فایل";

            insideFileRadioButton.AutoSize = true;
            insideFileRadioButton.Location = new Point(16, 22);
            insideFileRadioButton.Name = "insideFileRadioButton";
            insideFileRadioButton.Size = new Size(117, 32);
            insideFileRadioButton.TabIndex = 4;
            insideFileRadioButton.Text = "داخل فایل";

            dataPathLabel.Location = new Point(24, 85);
            dataPathLabel.Name = "dataPathLabel";
            dataPathLabel.Size = new Size(100, 32);
            dataPathLabel.TabIndex = 5;
            dataPathLabel.Text = "مسیر داده:";
            dataPathLabel.TextAlign = ContentAlignment.MiddleCenter;

            dataPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataPathTextBox.Location = new Point(131, 88);
            dataPathTextBox.Name = "dataPathTextBox";
            dataPathTextBox.Size = new Size(1210, 34);
            dataPathTextBox.TabIndex = 6;

            browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseButton.Location = new Point(1347, 82);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(139, 47);
            browseButton.TabIndex = 7;
            browseButton.Text = "انتخاب پوشه...";

            separatorLabel.Location = new Point(301, 158);
            separatorLabel.Name = "separatorLabel";
            separatorLabel.Size = new Size(105, 34);
            separatorLabel.TabIndex = 8;
            separatorLabel.Text = "جداکننده:";
            separatorLabel.TextAlign = ContentAlignment.MiddleRight;

            separatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            separatorComboBox.Items.AddRange(new object[] { "Comma (,)", "Semicolon (;)", "Tab", "Pipe (|)" });
            separatorComboBox.Location = new Point(412, 158);
            separatorComboBox.Name = "separatorComboBox";
            separatorComboBox.Size = new Size(140, 36);
            separatorComboBox.TabIndex = 9;

            calendarLabel.Location = new Point(811, 156);
            calendarLabel.Name = "calendarLabel";
            calendarLabel.Size = new Size(70, 34);
            calendarLabel.TabIndex = 10;
            calendarLabel.Text = "تقویم:";
            calendarLabel.TextAlign = ContentAlignment.MiddleRight;

            calendarComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            calendarComboBox.Items.AddRange(new object[] { "شمسی (Persian)", "لاتین (Gregorian)" });
            calendarComboBox.Location = new Point(887, 156);
            calendarComboBox.Name = "calendarComboBox";
            calendarComboBox.Size = new Size(140, 36);
            calendarComboBox.TabIndex = 11;

            timeFormatLabel.Location = new Point(1271, 154);
            timeFormatLabel.Name = "timeFormatLabel";
            timeFormatLabel.Size = new Size(70, 34);
            timeFormatLabel.TabIndex = 12;
            timeFormatLabel.Text = "زمان:";
            timeFormatLabel.TextAlign = ContentAlignment.MiddleRight;

            timeFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            timeFormatComboBox.Items.AddRange(new object[] { "HHMMSS", "HH:MM:SS", "HHMM" });
            timeFormatComboBox.Location = new Point(1347, 154);
            timeFormatComboBox.Name = "timeFormatComboBox";
            timeFormatComboBox.Size = new Size(140, 36);
            timeFormatComboBox.TabIndex = 13;

            dateFormatLabel.Location = new Point(1049, 158);
            dateFormatLabel.Name = "dateFormatLabel";
            dateFormatLabel.Size = new Size(70, 34);
            dateFormatLabel.TabIndex = 14;
            dateFormatLabel.Text = "تاریخ:";
            dateFormatLabel.TextAlign = ContentAlignment.MiddleRight;

            dateFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dateFormatComboBox.Items.AddRange(new object[] { "YYYYMMDD", "YYYY-MM-DD", "YYYY/MM/DD" });
            dateFormatComboBox.Location = new Point(1125, 156);
            dateFormatComboBox.Name = "dateFormatComboBox";
            dateFormatComboBox.Size = new Size(140, 36);
            dateFormatComboBox.TabIndex = 15;

            headerCheckBox.AutoSize = true;
            headerCheckBox.Location = new Point(26, 160);
            headerCheckBox.Name = "headerCheckBox";
            headerCheckBox.Size = new Size(235, 32);
            headerCheckBox.TabIndex = 16;
            headerCheckBox.Text = "سطر اول عنوان ستون‌ها";

            noDateTimeCheckBox.AutoSize = true;
            noDateTimeCheckBox.Location = new Point(616, 158);
            noDateTimeCheckBox.Name = "noDateTimeCheckBox";
            noDateTimeCheckBox.Size = new Size(163, 32);
            noDateTimeCheckBox.TabIndex = 17;
            noDateTimeCheckBox.Text = "فاقد تاریخ/زمان";

            mappingTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            mappingTitleLabel.Location = new Point(769, 217);
            mappingTitleLabel.Name = "mappingTitleLabel";
            mappingTitleLabel.Size = new Size(251, 34);
            mappingTitleLabel.TabIndex = 18;
            mappingTitleLabel.Text = "Mapping ستون‌های فایل";
            mappingTitleLabel.TextAlign = ContentAlignment.MiddleCenter;

            mappingGrid.AllowUserToAddRows = false;
            mappingGrid.AllowUserToDeleteRows = false;
            mappingGrid.AllowUserToResizeRows = false;
            mappingGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            mappingGrid.BackgroundColor = SystemColors.Window;
            mappingGrid.ColumnHeadersHeight = 32;
            mappingGrid.Columns.AddRange(new DataGridViewColumn[] { mappingFieldColumn, mappingNumberColumn });
            mappingGrid.Location = new Point(789, 254);
            mappingGrid.Name = "mappingGrid";
            mappingGrid.RightToLeft = RightToLeft.Yes;
            mappingGrid.RowHeadersVisible = false;
            mappingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mappingGrid.Size = new Size(697, 357);
            mappingGrid.TabIndex = 19;

            mappingFieldColumn.HeaderText = "داده";
            mappingFieldColumn.Name = "mappingFieldColumn";
            mappingFieldColumn.ReadOnly = true;
            mappingFieldColumn.Width = 390;

            mappingNumberColumn.HeaderText = "شماره ستون";
            mappingNumberColumn.Name = "mappingNumberColumn";
            mappingNumberColumn.Width = 220;

            symbolSelectionTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            symbolSelectionTitleLabel.Location = new Point(8, 217);
            symbolSelectionTitleLabel.Name = "symbolSelectionTitleLabel";
            symbolSelectionTitleLabel.Size = new Size(146, 34);
            symbolSelectionTitleLabel.TabIndex = 20;
            symbolSelectionTitleLabel.Text = "انتخاب سهام";
            symbolSelectionTitleLabel.TextAlign = ContentAlignment.MiddleCenter;

            symbolSearchTextBox.Location = new Point(26, 577);
            symbolSearchTextBox.Name = "symbolSearchTextBox";
            symbolSearchTextBox.PlaceholderText = "جستجوی نماد...";
            symbolSearchTextBox.Size = new Size(256, 34);
            symbolSearchTextBox.TabIndex = 23;

            selectAllButton.Location = new Point(464, 577);
            selectAllButton.Name = "selectAllButton";
            selectAllButton.Size = new Size(154, 34);
            selectAllButton.TabIndex = 21;
            selectAllButton.Text = "انتخاب همه";

            deselectAllButton.Location = new Point(620, 577);
            deselectAllButton.Name = "deselectAllButton";
            deselectAllButton.Size = new Size(154, 34);
            deselectAllButton.TabIndex = 22;
            deselectAllButton.Text = "عدم انتخاب همه";

            selectedCountLabel.Font = new Font("Segoe UI", 9F);
            selectedCountLabel.Location = new Point(278, 577);
            selectedCountLabel.Name = "selectedCountLabel";
            selectedCountLabel.Size = new Size(182, 34);
            selectedCountLabel.TabIndex = 24;
            selectedCountLabel.Text = "انتخاب شده: ۰ از ۰";
            selectedCountLabel.TextAlign = ContentAlignment.MiddleCenter;

            symbolGrid.AllowUserToAddRows = false;
            symbolGrid.AllowUserToDeleteRows = false;
            symbolGrid.AllowUserToResizeRows = false;
            symbolGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            symbolGrid.BackgroundColor = SystemColors.Window;
            symbolGrid.ColumnHeadersHeight = 32;
            symbolGrid.Columns.AddRange(new DataGridViewColumn[] { selectedColumn, symbolColumn });
            symbolGrid.Location = new Point(26, 254);
            symbolGrid.Name = "symbolGrid";
            symbolGrid.RightToLeft = RightToLeft.Yes;
            symbolGrid.RowHeadersVisible = false;
            symbolGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            symbolGrid.Size = new Size(750, 317);
            symbolGrid.TabIndex = 25;

            selectedColumn.HeaderText = "انتخاب";
            selectedColumn.Name = "selectedColumn";
            selectedColumn.Width = 110;

            symbolColumn.HeaderText = "نماد / فایل";
            symbolColumn.Name = "symbolColumn";
            symbolColumn.ReadOnly = true;
            symbolColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            previewTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            previewTitleLabel.Location = new Point(8, 613);
            previewTitleLabel.Name = "previewTitleLabel";
            previewTitleLabel.Size = new Size(171, 34);
            previewTitleLabel.TabIndex = 26;
            previewTitleLabel.Text = "پیش‌نمایش داده";
            previewTitleLabel.TextAlign = ContentAlignment.MiddleCenter;

            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            previewGrid.BackgroundColor = SystemColors.Window;
            previewGrid.ColumnHeadersHeight = 32;
            previewGrid.Columns.AddRange(new DataGridViewColumn[] {
                previewColumn1, previewColumn2, previewColumn3, previewColumn4, previewColumn5, previewColumn6,
                previewColumn7, previewColumn8, previewColumn9, previewColumn10, previewColumn11, previewColumn12,
                previewColumn13, previewColumn14, previewColumn15, previewColumn16, previewColumn17, previewColumn18 });
            previewGrid.Location = new Point(26, 648);
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RightToLeft = RightToLeft.Yes;
            previewGrid.RowHeadersVisible = false;
            previewGrid.Size = new Size(1461, 190);
            previewGrid.TabIndex = 27;

            previewColumn1.Name = "previewColumn1";
            previewColumn2.Name = "previewColumn2";
            previewColumn3.Name = "previewColumn3";
            previewColumn4.Name = "previewColumn4";
            previewColumn5.Name = "previewColumn5";
            previewColumn6.Name = "previewColumn6";
            previewColumn7.Name = "previewColumn7";
            previewColumn8.Name = "previewColumn8";
            previewColumn9.Name = "previewColumn9";
            previewColumn10.Name = "previewColumn10";
            previewColumn11.Name = "previewColumn11";
            previewColumn12.Name = "previewColumn12";
            previewColumn13.Name = "previewColumn13";
            previewColumn14.Name = "previewColumn14";
            previewColumn15.Name = "previewColumn15";
            previewColumn16.Name = "previewColumn16";
            previewColumn17.Name = "previewColumn17";
            previewColumn18.Name = "previewColumn18";
            previewColumn1.Width = previewColumn2.Width = previewColumn3.Width = previewColumn4.Width = 150;
            previewColumn5.Width = previewColumn6.Width = previewColumn7.Width = previewColumn8.Width = 150;
            previewColumn9.Width = previewColumn10.Width = previewColumn11.Width = previewColumn12.Width = 150;
            previewColumn13.Width = previewColumn14.Width = previewColumn15.Width = previewColumn16.Width = 150;
            previewColumn17.Width = previewColumn18.Width = 150;
            previewColumn1.ReadOnly = previewColumn2.ReadOnly = previewColumn3.ReadOnly = previewColumn4.ReadOnly = true;
            previewColumn5.ReadOnly = previewColumn6.ReadOnly = previewColumn7.ReadOnly = previewColumn8.ReadOnly = true;
            previewColumn9.ReadOnly = previewColumn10.ReadOnly = previewColumn11.ReadOnly = previewColumn12.ReadOnly = true;
            previewColumn13.ReadOnly = previewColumn14.ReadOnly = previewColumn15.ReadOnly = previewColumn16.ReadOnly = true;
            previewColumn17.ReadOnly = previewColumn18.ReadOnly = true;

            testMappingButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            testMappingButton.Location = new Point(483, 846);
            testMappingButton.Name = "testMappingButton";
            testMappingButton.Size = new Size(135, 42);
            testMappingButton.TabIndex = 28;
            testMappingButton.Text = "تست Mapping";

            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(908, 846);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(132, 42);
            cancelButton.TabIndex = 29;
            cancelButton.Text = "انصراف";

            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(627, 846);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(135, 42);
            saveButton.TabIndex = 30;
            saveButton.Text = "ذخیره سبد";

            groupBox1.Controls.Add(insideFileRadioButton);
            groupBox1.Controls.Add(fileNameRadioButton);
            groupBox1.Location = new Point(906, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.Yes;
            groupBox1.Size = new Size(288, 71);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;

            sourceLabel.Location = new Point(745, 25);
            sourceLabel.Name = "sourceLabel";
            sourceLabel.Size = new Size(159, 32);
            sourceLabel.TabIndex = 2;
            sourceLabel.Text = "منبع نام نمادها:";
            sourceLabel.TextAlign = ContentAlignment.MiddleCenter;

            resetButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            resetButton.Location = new Point(768, 846);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(132, 42);
            resetButton.TabIndex = 32;
            resetButton.Text = "بازنشانی";

            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(26, 139);
            label1.Name = "label1";
            label1.Size = new Size(1460, 1);
            label1.TabIndex = 33;

            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(26, 75);
            label2.Name = "label2";
            label2.Size = new Size(1460, 1);
            label2.TabIndex = 34;

            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(24, 208);
            label3.Name = "label3";
            label3.Size = new Size(1460, 1);
            label3.TabIndex = 35;

            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(27, 619);
            label4.Name = "label4";
            label4.Size = new Size(1458, 1);
            label4.TabIndex = 36;

            label5.Location = new Point(1208, 20);
            label5.Name = "label5";
            label5.Size = new Size(137, 34);
            label5.TabIndex = 37;
            label5.Text = "نوع فایل داده:";
            label5.TextAlign = ContentAlignment.MiddleRight;

            fileTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            fileTypeComboBox.Items.AddRange(new object[] { "TXT", "CSV", "PRN" });
            fileTypeComboBox.Location = new Point(1347, 19);
            fileTypeComboBox.Name = "fileTypeComboBox";
            fileTypeComboBox.Size = new Size(136, 36);
            fileTypeComboBox.TabIndex = 38;

            ClientSize = new Size(1500, 900);
            Controls.Add(label5);
            Controls.Add(fileTypeComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(resetButton);
            Controls.Add(groupBox1);
            Controls.Add(portfolioNameLabel);
            Controls.Add(portfolioNameTextBox);
            Controls.Add(sourceLabel);
            Controls.Add(dataPathLabel);
            Controls.Add(dataPathTextBox);
            Controls.Add(browseButton);
            Controls.Add(separatorLabel);
            Controls.Add(separatorComboBox);
            Controls.Add(calendarLabel);
            Controls.Add(calendarComboBox);
            Controls.Add(timeFormatLabel);
            Controls.Add(timeFormatComboBox);
            Controls.Add(dateFormatLabel);
            Controls.Add(dateFormatComboBox);
            Controls.Add(headerCheckBox);
            Controls.Add(noDateTimeCheckBox);
            Controls.Add(mappingTitleLabel);
            Controls.Add(mappingGrid);
            Controls.Add(symbolSelectionTitleLabel);
            Controls.Add(selectAllButton);
            Controls.Add(deselectAllButton);
            Controls.Add(symbolSearchTextBox);
            Controls.Add(selectedCountLabel);
            Controls.Add(symbolGrid);
            Controls.Add(previewTitleLabel);
            Controls.Add(previewGrid);
            Controls.Add(testMappingButton);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1200, 760);
            Name = "PortfolioDefinitionForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تعریف سبد";
            ((System.ComponentModel.ISupportInitialize)mappingGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)symbolGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
