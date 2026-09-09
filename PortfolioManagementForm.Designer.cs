namespace Trade.It
{
    partial class PortfolioManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel rootTable;
        private Label titleLabel;
        private Button reloadButton;
        private TableLayoutPanel parameterTable;
        private Label parametersTitleLabel;
        private Label portfolioNameCaption;
        private Label portfolioNameLabel;
        private Label sourceTypeCaption;
        private Label sourceTypeLabel;
        private Label dataPathCaption;
        private Label dataPathLabel;
        private Label dataTypeCaption;
        private Label dataTypeLabel;
        private Label symbolSourceCaption;
        private Label symbolSourceLabel;
        private Label separatorCaption;
        private Label separatorLabel;
        private Label timeFormatCaption;
        private Label timeFormatLabel;
        private Label dateFormatCaption;
        private Label dateFormatLabel;
        private Label headerCaption;
        private Label headerLabel;
        private Label calendarCaption;
        private Label calendarLabel;
        private Label symbolCountCaption;
        private Label symbolCountLabel;
        private TableLayoutPanel lowerTable;
        private TableLayoutPanel portfoliosTable;
        private TableLayoutPanel symbolsTable;
        private Label existingTitleLabel;
        private ListBox portfoliosListBox;
        private Button deletePortfoliosButton;
        private Label symbolsTitleLabel;
        private DataGridView symbolsGrid;
        private Button deleteSymbolsButton;
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
            titleLabel = new Label();
            reloadButton = new Button();
            parameterTable = new TableLayoutPanel();
            parametersTitleLabel = new Label();
            portfolioNameCaption = new Label();
            portfolioNameLabel = new Label();
            sourceTypeCaption = new Label();
            sourceTypeLabel = new Label();
            dataPathCaption = new Label();
            dataPathLabel = new Label();
            dataTypeCaption = new Label();
            dataTypeLabel = new Label();
            symbolSourceCaption = new Label();
            symbolSourceLabel = new Label();
            separatorCaption = new Label();
            separatorLabel = new Label();
            timeFormatCaption = new Label();
            timeFormatLabel = new Label();
            dateFormatCaption = new Label();
            dateFormatLabel = new Label();
            headerCaption = new Label();
            headerLabel = new Label();
            calendarCaption = new Label();
            calendarLabel = new Label();
            symbolCountCaption = new Label();
            symbolCountLabel = new Label();
            lowerTable = new TableLayoutPanel();
            portfoliosTable = new TableLayoutPanel();
            symbolsTable = new TableLayoutPanel();
            existingTitleLabel = new Label();
            portfoliosListBox = new ListBox();
            deletePortfoliosButton = new Button();
            symbolsTitleLabel = new Label();
            symbolsGrid = new DataGridView();
            deleteSymbolsButton = new Button();
            statusLabel = new Label();
            closeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)(symbolsGrid)).BeginInit();
            SuspendLayout();

            Text = "مدیریت سبدها";
            StartPosition = FormStartPosition.CenterParent;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            MinimumSize = new Size(1100, 700);
            ClientSize = new Size(1400, 820);
            Font = new Font("Segoe UI", 10F);

            rootTable.Dock = DockStyle.Fill;
            rootTable.Padding = new Padding(14);
            rootTable.ColumnCount = 2;
            rootTable.RowCount = 5;
            rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));

            titleLabel.Text = "مدیریت سبدها";
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleRight;
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            rootTable.Controls.Add(titleLabel, 0, 0);

            reloadButton.Text = "بازخوانی";
            reloadButton.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            reloadButton.Size = new Size(110, 34);
            reloadButton.Margin = new Padding(4, 8, 4, 4);
            rootTable.Controls.Add(reloadButton, 1, 0);

            parameterTable.Dock = DockStyle.Fill;
            parameterTable.ColumnCount = 4;
            parameterTable.RowCount = 7;
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667F));
            parametersTitleLabel.Text = "پارامترهای سبد";
            parametersTitleLabel.Dock = DockStyle.Fill;
            parametersTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            parametersTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            parameterTable.Controls.Add(parametersTitleLabel, 0, 0);
            parameterTable.SetColumnSpan(parametersTitleLabel, 4);

            portfolioNameCaption.Text = "نام سبد:";
            portfolioNameCaption.Dock = DockStyle.Fill;
            portfolioNameCaption.TextAlign = ContentAlignment.MiddleRight;
            portfolioNameLabel.Text = "—";
            portfolioNameLabel.Dock = DockStyle.Fill;
            portfolioNameLabel.TextAlign = ContentAlignment.MiddleRight;
            portfolioNameLabel.BorderStyle = BorderStyle.FixedSingle;

            sourceTypeCaption.Text = "نوع منبع:";
            sourceTypeCaption.Dock = DockStyle.Fill;
            sourceTypeCaption.TextAlign = ContentAlignment.MiddleRight;
            sourceTypeLabel.Text = "—";
            sourceTypeLabel.Dock = DockStyle.Fill;
            sourceTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            sourceTypeLabel.BorderStyle = BorderStyle.FixedSingle;

            dataPathCaption.Text = "مسیر داده:";
            dataPathCaption.Dock = DockStyle.Fill;
            dataPathCaption.TextAlign = ContentAlignment.MiddleRight;
            dataPathLabel.Text = "—";
            dataPathLabel.Dock = DockStyle.Fill;
            dataPathLabel.TextAlign = ContentAlignment.MiddleRight;
            dataPathLabel.BorderStyle = BorderStyle.FixedSingle;
            dataPathLabel.AutoEllipsis = true;

            dataTypeCaption.Text = "نوع داده:";
            dataTypeCaption.Dock = DockStyle.Fill;
            dataTypeCaption.TextAlign = ContentAlignment.MiddleRight;
            dataTypeLabel.Text = "—";
            dataTypeLabel.Dock = DockStyle.Fill;
            dataTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            dataTypeLabel.BorderStyle = BorderStyle.FixedSingle;

            symbolSourceCaption.Text = "منبع نام نماد:";
            symbolSourceCaption.Dock = DockStyle.Fill;
            symbolSourceCaption.TextAlign = ContentAlignment.MiddleRight;
            symbolSourceLabel.Text = "—";
            symbolSourceLabel.Dock = DockStyle.Fill;
            symbolSourceLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolSourceLabel.BorderStyle = BorderStyle.FixedSingle;

            separatorCaption.Text = "جداکننده:";
            separatorCaption.Dock = DockStyle.Fill;
            separatorCaption.TextAlign = ContentAlignment.MiddleRight;
            separatorLabel.Text = "—";
            separatorLabel.Dock = DockStyle.Fill;
            separatorLabel.TextAlign = ContentAlignment.MiddleRight;
            separatorLabel.BorderStyle = BorderStyle.FixedSingle;

            timeFormatCaption.Text = "فرمت زمان:";
            timeFormatCaption.Dock = DockStyle.Fill;
            timeFormatCaption.TextAlign = ContentAlignment.MiddleRight;
            timeFormatLabel.Text = "—";
            timeFormatLabel.Dock = DockStyle.Fill;
            timeFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            timeFormatLabel.BorderStyle = BorderStyle.FixedSingle;

            dateFormatCaption.Text = "فرمت تاریخ:";
            dateFormatCaption.Dock = DockStyle.Fill;
            dateFormatCaption.TextAlign = ContentAlignment.MiddleRight;
            dateFormatLabel.Text = "—";
            dateFormatLabel.Dock = DockStyle.Fill;
            dateFormatLabel.TextAlign = ContentAlignment.MiddleRight;
            dateFormatLabel.BorderStyle = BorderStyle.FixedSingle;

            headerCaption.Text = "Header:";
            headerCaption.Dock = DockStyle.Fill;
            headerCaption.TextAlign = ContentAlignment.MiddleRight;
            headerLabel.Text = "—";
            headerLabel.Dock = DockStyle.Fill;
            headerLabel.TextAlign = ContentAlignment.MiddleRight;
            headerLabel.BorderStyle = BorderStyle.FixedSingle;

            calendarCaption.Text = "تقویم:";
            calendarCaption.Dock = DockStyle.Fill;
            calendarCaption.TextAlign = ContentAlignment.MiddleRight;
            calendarLabel.Text = "—";
            calendarLabel.Dock = DockStyle.Fill;
            calendarLabel.TextAlign = ContentAlignment.MiddleRight;
            calendarLabel.BorderStyle = BorderStyle.FixedSingle;

            symbolCountCaption.Text = "تعداد نماد:";
            symbolCountCaption.Dock = DockStyle.Fill;
            symbolCountCaption.TextAlign = ContentAlignment.MiddleRight;
            symbolCountLabel.Text = "۰";
            symbolCountLabel.Dock = DockStyle.Fill;
            symbolCountLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolCountLabel.BorderStyle = BorderStyle.FixedSingle;

            parameterTable.Controls.Add(portfolioNameCaption, 0, 1);
            parameterTable.Controls.Add(portfolioNameLabel, 1, 1);
            parameterTable.Controls.Add(sourceTypeCaption, 2, 1);
            parameterTable.Controls.Add(sourceTypeLabel, 3, 1);
            parameterTable.Controls.Add(dataPathCaption, 0, 2);
            parameterTable.Controls.Add(dataPathLabel, 1, 2);
            parameterTable.Controls.Add(dataTypeCaption, 2, 2);
            parameterTable.Controls.Add(dataTypeLabel, 3, 2);
            parameterTable.Controls.Add(symbolSourceCaption, 0, 3);
            parameterTable.Controls.Add(symbolSourceLabel, 1, 3);
            parameterTable.Controls.Add(separatorCaption, 2, 3);
            parameterTable.Controls.Add(separatorLabel, 3, 3);
            parameterTable.Controls.Add(timeFormatCaption, 0, 4);
            parameterTable.Controls.Add(timeFormatLabel, 1, 4);
            parameterTable.Controls.Add(dateFormatCaption, 2, 4);
            parameterTable.Controls.Add(dateFormatLabel, 3, 4);
            parameterTable.Controls.Add(headerCaption, 0, 5);
            parameterTable.Controls.Add(headerLabel, 1, 5);
            parameterTable.Controls.Add(calendarCaption, 2, 5);
            parameterTable.Controls.Add(calendarLabel, 3, 5);
            parameterTable.Controls.Add(symbolCountCaption, 0, 6);
            parameterTable.Controls.Add(symbolCountLabel, 1, 6);
            rootTable.Controls.Add(parameterTable, 0, 1);
            rootTable.SetColumnSpan(parameterTable, 2);

            lowerTable.Dock = DockStyle.Fill;
            lowerTable.ColumnCount = 2;
            lowerTable.RowCount = 1;
            lowerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            lowerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            lowerTable.Padding = new Padding(0, 6, 0, 6);
            rootTable.Controls.Add(lowerTable, 0, 2);
            rootTable.SetColumnSpan(lowerTable, 2);

            portfoliosTable.Dock = DockStyle.Fill;
            portfoliosTable.ColumnCount = 1;
            portfoliosTable.RowCount = 3;
            portfoliosTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            portfoliosTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            portfoliosTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            portfoliosTable.Margin = new Padding(0, 0, 8, 0);
            existingTitleLabel.Text = "سبدهای موجود";
            existingTitleLabel.Dock = DockStyle.Fill;
            existingTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            existingTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            portfoliosTable.Controls.Add(existingTitleLabel, 0, 0);
            portfoliosListBox.Dock = DockStyle.Fill;
            portfoliosListBox.IntegralHeight = false;
            portfoliosListBox.Margin = new Padding(0);
            portfoliosTable.Controls.Add(portfoliosListBox, 0, 1);
            deletePortfoliosButton.Text = "حذف سبد انتخاب شده";
            deletePortfoliosButton.Dock = DockStyle.Fill;
            deletePortfoliosButton.Margin = new Padding(0, 4, 0, 0);
            portfoliosTable.Controls.Add(deletePortfoliosButton, 0, 2);
            lowerTable.Controls.Add(portfoliosTable, 0, 0);

            symbolsTable.Dock = DockStyle.Fill;
            symbolsTable.ColumnCount = 1;
            symbolsTable.RowCount = 3;
            symbolsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            symbolsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            symbolsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            symbolsTable.Margin = new Padding(8, 0, 0, 0);
            symbolsTitleLabel.Text = "نمادهای سبد";
            symbolsTitleLabel.Dock = DockStyle.Fill;
            symbolsTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolsTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            symbolsTable.Controls.Add(symbolsTitleLabel, 0, 0);
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
            symbolsTable.Controls.Add(symbolsGrid, 0, 1);
            deleteSymbolsButton.Text = "حذف نمادهای انتخاب شده";
            deleteSymbolsButton.Dock = DockStyle.Fill;
            deleteSymbolsButton.Margin = new Padding(0, 4, 0, 0);
            symbolsTable.Controls.Add(deleteSymbolsButton, 0, 2);
            lowerTable.Controls.Add(symbolsTable, 1, 0);

            statusLabel.Text = "هیچ سبدی انتخاب نشده است.";
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.TextAlign = ContentAlignment.MiddleRight;
            rootTable.Controls.Add(statusLabel, 0, 3);
            closeButton.Text = "بستن";
            closeButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            closeButton.Size = new Size(100, 34);
            closeButton.Margin = new Padding(4, 4, 4, 4);
            rootTable.Controls.Add(closeButton, 1, 3);

            Controls.Add(rootTable);
            ((System.ComponentModel.ISupportInitialize)(symbolsGrid)).EndInit();
            rootTable.ResumeLayout(false);
            parameterTable.ResumeLayout(false);
            lowerTable.ResumeLayout(false);
            portfoliosTable.ResumeLayout(false);
            symbolsTable.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
