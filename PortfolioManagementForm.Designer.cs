namespace Trade.It
{
    partial class PortfolioManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel rootTable;
        private Panel headerPanel;
        private Label titleLabel;
        private Button reloadButton;
        private TableLayoutPanel upperTable;
        private GroupBox parametersGroup;
        private TableLayoutPanel parameterTable;
        private Label portfolioNameLabel;
        private TextBox portfolioNameTextBox;
        private Label sourceTypeLabel;
        private ComboBox sourceTypeComboBox;
        private Label dataPathLabel;
        private TextBox dataPathTextBox;
        private Label dataTypeLabel;
        private ComboBox dataTypeComboBox;
        private Label symbolSourceLabel;
        private ComboBox symbolSourceComboBox;
        private Label separatorLabel;
        private ComboBox separatorComboBox;
        private Label timeFormatLabel;
        private ComboBox timeFormatComboBox;
        private Label dateFormatLabel;
        private ComboBox dateFormatComboBox;
        private Label headerLabel;
        private CheckBox headerCheckBox;
        private Label calendarLabel;
        private ComboBox calendarComboBox;
        private Label symbolCountLabel;
        private TextBox symbolCountTextBox;
        private GroupBox existingGroup;
        private ListBox portfoliosListBox;
        private Button deletePortfoliosButton;
        private GroupBox symbolsGroup;
        private DataGridView symbolsGrid;
        private Button deleteSymbolsButton;
        private Panel footerPanel;
        private Label statusLabel;
        private Button closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootTable = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new Label();
            reloadButton = new Button();
            upperTable = new TableLayoutPanel();
            parametersGroup = new GroupBox();
            parameterTable = new TableLayoutPanel();
            portfolioNameLabel = new Label();
            portfolioNameTextBox = new TextBox();
            sourceTypeLabel = new Label();
            sourceTypeComboBox = new ComboBox();
            dataPathLabel = new Label();
            dataPathTextBox = new TextBox();
            dataTypeLabel = new Label();
            dataTypeComboBox = new ComboBox();
            symbolSourceLabel = new Label();
            symbolSourceComboBox = new ComboBox();
            separatorLabel = new Label();
            separatorComboBox = new ComboBox();
            timeFormatLabel = new Label();
            timeFormatComboBox = new ComboBox();
            dateFormatLabel = new Label();
            dateFormatComboBox = new ComboBox();
            headerLabel = new Label();
            headerCheckBox = new CheckBox();
            calendarLabel = new Label();
            calendarComboBox = new ComboBox();
            symbolCountLabel = new Label();
            symbolCountTextBox = new TextBox();
            existingGroup = new GroupBox();
            portfoliosListBox = new ListBox();
            deletePortfoliosButton = new Button();
            symbolsGroup = new GroupBox();
            symbolsGrid = new DataGridView();
            deleteSymbolsButton = new Button();
            footerPanel = new Panel();
            statusLabel = new Label();
            closeButton = new Button();
            SuspendLayout();

            Text = "مدیریت سبدها";
            StartPosition = FormStartPosition.CenterParent;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            MinimumSize = new Size(1150, 700);
            ClientSize = new Size(1450, 850);
            Font = new Font("Segoe UI", 10F);

            rootTable.Dock = DockStyle.Fill;
            rootTable.Padding = new Padding(12);
            rootTable.ColumnCount = 1;
            rootTable.RowCount = 4;
            rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 290F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

            headerPanel.Dock = DockStyle.Fill;
            headerPanel.BorderStyle = BorderStyle.FixedSingle;
            titleLabel.Text = "مدیریت سبدها";
            titleLabel.Dock = DockStyle.Right;
            titleLabel.Width = 260;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            reloadButton.Text = "بازخوانی";
            reloadButton.Size = new Size(120, 34);
            reloadButton.Location = new Point(14, 12);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(reloadButton);
            rootTable.Controls.Add(headerPanel, 0, 0);

            upperTable.Dock = DockStyle.Fill;
            upperTable.ColumnCount = 2;
            upperTable.RowCount = 1;
            upperTable.RightToLeft = RightToLeft.Yes;
            upperTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            upperTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            rootTable.Controls.Add(upperTable, 0, 1);

            parametersGroup.Text = "پارامترهای سبد";
            parametersGroup.Dock = DockStyle.Fill;
            parametersGroup.Padding = new Padding(10);
            parameterTable.Dock = DockStyle.Fill;
            parameterTable.ColumnCount = 4;
            parameterTable.RowCount = 6;
            parameterTable.RightToLeft = RightToLeft.Yes;
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));

            portfolioNameLabel.Text = "نام سبد:";
            portfolioNameLabel.Dock = DockStyle.Fill;
            portfolioNameLabel.TextAlign = ContentAlignment.MiddleRight;
            portfolioNameTextBox.Dock = DockStyle.Fill;
            portfolioNameTextBox.Margin = new Padding(4);
            sourceTypeLabel.Text = "نوع منبع:";
            sourceTypeLabel.Dock = DockStyle.Fill;
            sourceTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            sourceTypeComboBox.Dock = DockStyle.Fill;
            sourceTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceTypeComboBox.Items.AddRange(new object[] { "فایل", "داخل فایل" });
            sourceTypeComboBox.SelectedIndex = 0;

            dataPathLabel.Text = "مسیر داده:";
            dataPathLabel.Dock = DockStyle.Fill;
            dataPathLabel.TextAlign = ContentAlignment.MiddleRight;
            dataPathTextBox.Dock = DockStyle.Fill;
            dataPathTextBox.Margin = new Padding(4);
            dataTypeLabel.Text = "نوع داده:";
            dataTypeLabel.Dock = DockStyle.Fill;
            dataTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            dataTypeComboBox.Dock = DockStyle.Fill;
            dataTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dataTypeComboBox.Items.AddRange(new object[] { "CSV", "Excel", "متنی" });
            dataTypeComboBox.SelectedIndex = 0;

            symbolSourceLabel.Text = "منبع نام نماد:";
            symbolSourceLabel.Dock = DockStyle.Fill;
            symbolSourceLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolSourceComboBox.Dock = DockStyle.Fill;
            symbolSourceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            symbolSourceComboBox.Items.AddRange(new object[] { "نام فایل", "داخل فایل" });
            symbolSourceComboBox.SelectedIndex = 0;

            separatorLabel.Text = "جداکننده:";
            separatorLabel.Dock = DockStyle.Fill;
            separatorLabel.TextAlign = ContentAlignment.MiddleRight;
            separatorComboBox.Dock = DockStyle.Fill;
            separatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            separatorComboBox.Items.AddRange(new object[] { "Comma (,)", "Semicolon (;)", "Tab", "Pipe (|)" });
            separatorComboBox.SelectedIndex = 0;

            timeFormatLabel.Text = "فرمت زمان:";
            timeFormatLabel.Dock = DockStyle.Fill;
            timeFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            timeFormatComboBox.Dock = DockStyle.Fill;
            timeFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            timeFormatComboBox.Items.AddRange(new object[] { "HHMMSS", "HH:MM:SS", "HHMM" });
            timeFormatComboBox.SelectedIndex = 0;

            dateFormatLabel.Text = "فرمت تاریخ:";
            dateFormatLabel.Dock = DockStyle.Fill;
            dateFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            dateFormatComboBox.Dock = DockStyle.Fill;
            dateFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dateFormatComboBox.Items.AddRange(new object[] { "YYYYMMDD", "YYYY-MM-DD", "YYYY/MM/DD" });
            dateFormatComboBox.SelectedIndex = 0;

            headerLabel.Text = "Header:";
            headerLabel.Dock = DockStyle.Fill;
            headerLabel.TextAlign = ContentAlignment.MiddleRight;
            headerCheckBox.Text = "سطر اول عنوان ستون‌ها";
            headerCheckBox.Dock = DockStyle.Fill;
            calendarLabel.Text = "تقویم:";
            calendarLabel.Dock = DockStyle.Fill;
            calendarLabel.TextAlign = ContentAlignment.MiddleRight;
            calendarComboBox.Dock = DockStyle.Fill;
            calendarComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            calendarComboBox.Items.AddRange(new object[] { "شمسی (Persian)", "لاتین (Gregorian)" });
            calendarComboBox.SelectedIndex = 0;
            symbolCountLabel.Text = "تعداد نماد:";
            symbolCountLabel.Dock = DockStyle.Fill;
            symbolCountLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolCountTextBox.Dock = DockStyle.Fill;
            symbolCountTextBox.ReadOnly = true;

            parameterTable.Controls.Add(portfolioNameLabel, 0, 0);
            parameterTable.Controls.Add(portfolioNameTextBox, 1, 0);
            parameterTable.Controls.Add(sourceTypeLabel, 2, 0);
            parameterTable.Controls.Add(sourceTypeComboBox, 3, 0);
            parameterTable.Controls.Add(dataPathLabel, 0, 1);
            parameterTable.Controls.Add(dataPathTextBox, 1, 1);
            parameterTable.Controls.Add(dataTypeLabel, 2, 1);
            parameterTable.Controls.Add(dataTypeComboBox, 3, 1);
            parameterTable.Controls.Add(symbolSourceLabel, 0, 2);
            parameterTable.Controls.Add(symbolSourceComboBox, 1, 2);
            parameterTable.Controls.Add(separatorLabel, 2, 2);
            parameterTable.Controls.Add(separatorComboBox, 3, 2);
            parameterTable.Controls.Add(timeFormatLabel, 0, 3);
            parameterTable.Controls.Add(timeFormatComboBox, 1, 3);
            parameterTable.Controls.Add(dateFormatLabel, 2, 3);
            parameterTable.Controls.Add(dateFormatComboBox, 3, 3);
            parameterTable.Controls.Add(headerLabel, 0, 4);
            parameterTable.Controls.Add(headerCheckBox, 1, 4);
            parameterTable.Controls.Add(calendarLabel, 2, 4);
            parameterTable.Controls.Add(calendarComboBox, 3, 4);
            parameterTable.Controls.Add(symbolCountLabel, 0, 5);
            parameterTable.Controls.Add(symbolCountTextBox, 1, 5);
            parametersGroup.Controls.Add(parameterTable);
            upperTable.Controls.Add(parametersGroup, 0, 0);

            existingGroup.Text = "سبدهای موجود";
            existingGroup.Dock = DockStyle.Fill;
            existingGroup.Padding = new Padding(10);
            portfoliosListBox.Dock = DockStyle.Fill;
            portfoliosListBox.IntegralHeight = false;
            deletePortfoliosButton.Text = "حذف سبد انتخاب شده";
            deletePortfoliosButton.Dock = DockStyle.Bottom;
            deletePortfoliosButton.Height = 36;
            deletePortfoliosButton.Margin = new Padding(4);
            existingGroup.Controls.Add(portfoliosListBox);
            existingGroup.Controls.Add(deletePortfoliosButton);
            upperTable.Controls.Add(existingGroup, 1, 0);

            symbolsGroup.Text = "نمادهای سبد";
            symbolsGroup.Dock = DockStyle.Fill;
            symbolsGroup.Padding = new Padding(10);
            symbolsGrid.Dock = DockStyle.Fill;
            symbolsGrid.AllowUserToAddRows = false;
            symbolsGrid.AllowUserToDeleteRows = false;
            symbolsGrid.ReadOnly = true;
            symbolsGrid.AutoGenerateColumns = false;
            symbolsGrid.RowHeadersVisible = false;
            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            symbolsGrid.MultiSelect = true;
            symbolsGrid.RightToLeft = RightToLeft.Yes;
            symbolsGrid.BackgroundColor = SystemColors.Window;
            symbolsGrid.BorderStyle = BorderStyle.FixedSingle;
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "rowColumn", HeaderText = "ردیف", Width = 70 });
            symbolsGrid.Columns.Add(new DataGridViewCheckBoxColumn { Name = "selectedColumn", HeaderText = "انتخاب", Width = 80 });
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "symbolColumn", HeaderText = "نماد", Width = 140 });
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "displayNameColumn", HeaderText = "نام نمایشی", Width = 190 });
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "lastTradeColumn", HeaderText = "آخرین معامله", Width = 150 });
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "volumeColumn", HeaderText = "حجم", Width = 130 });
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "closeColumn", HeaderText = "قیمت پایانی", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            deleteSymbolsButton.Text = "حذف نمادهای انتخاب شده";
            deleteSymbolsButton.Dock = DockStyle.Bottom;
            deleteSymbolsButton.Height = 36;
            deleteSymbolsButton.Margin = new Padding(4);
            symbolsGroup.Controls.Add(symbolsGrid);
            symbolsGroup.Controls.Add(deleteSymbolsButton);
            rootTable.Controls.Add(symbolsGroup, 0, 2);

            footerPanel.Dock = DockStyle.Fill;
            footerPanel.BorderStyle = BorderStyle.FixedSingle;
            statusLabel.Text = "هیچ سبدی انتخاب نشده است.";
            statusLabel.Dock = DockStyle.Right;
            statusLabel.Width = 420;
            statusLabel.TextAlign = ContentAlignment.MiddleRight;
            closeButton.Text = "بستن";
            closeButton.Size = new Size(110, 34);
            closeButton.Location = new Point(12, 8);
            footerPanel.Controls.Add(statusLabel);
            footerPanel.Controls.Add(closeButton);
            rootTable.Controls.Add(footerPanel, 0, 3);

            Controls.Add(rootTable);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
