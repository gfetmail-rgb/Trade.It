namespace Trade.It
{
    partial class PortfolioManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootTable;
        private Panel headerPanel;
        private Label titleLabel;
        private Button reloadButton;
        private GroupBox parametersGroup;
        private TableLayoutPanel parameterTable;
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
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootTable = new TableLayoutPanel();
            headerPanel = new Panel(); titleLabel = new Label(); reloadButton = new Button();
            parametersGroup = new GroupBox(); parameterTable = new TableLayoutPanel(); existingGroup = new GroupBox(); portfoliosListBox = new ListBox(); deletePortfoliosButton = new Button();
            symbolsGroup = new GroupBox(); symbolsGrid = new DataGridView(); deleteSymbolsButton = new Button();
            footerPanel = new Panel(); statusLabel = new Label(); closeButton = new Button();
            SuspendLayout();

            Text = "مدیریت سبدها";
            StartPosition = FormStartPosition.CenterParent;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            MinimumSize = new Size(1100, 700);
            ClientSize = new Size(1500, 920);
            Font = new Font("Segoe UI", 10F);

            rootTable.Dock = DockStyle.Fill;
            rootTable.Padding = new Padding(6);
            rootTable.ColumnCount = 1;
            rootTable.RowCount = 4;
            rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));

            headerPanel.Dock = DockStyle.Fill; headerPanel.BorderStyle = BorderStyle.FixedSingle;
            titleLabel.Text = "مدیریت سبدها"; titleLabel.Dock = DockStyle.Right; titleLabel.Width = 260; titleLabel.TextAlign = ContentAlignment.MiddleCenter; titleLabel.Font = new Font("Segoe UI", 13F);
            reloadButton.Text = "بازخوانی"; reloadButton.Width = 145; reloadButton.Height = 36; reloadButton.Location = new Point(14, 12);
            headerPanel.Controls.Add(titleLabel); headerPanel.Controls.Add(reloadButton); rootTable.Controls.Add(headerPanel, 0, 0);

            var upper = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes };
            upper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F)); upper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            upper.Controls.Add(parametersGroup, 0, 0); upper.Controls.Add(existingGroup, 1, 0);

            parametersGroup.Text = "پارامترهای سبد"; parametersGroup.Dock = DockStyle.Fill; parametersGroup.Padding = new Padding(10);
            parameterTable.Dock = DockStyle.Fill; parameterTable.ColumnCount = 4; parameterTable.RowCount = 6; parameterTable.RightToLeft = RightToLeft.Yes;
            parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); parameterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            AddField(parameterTable, "نام سبد:", 0, 0); AddField(parameterTable, "نوع منبع:", 2, 0); AddField(parameterTable, "مسیر داده:", 0, 1); AddField(parameterTable, "نوع داده:", 0, 2); AddField(parameterTable, "منبع نام نماد:", 2, 2); AddField(parameterTable, "جداکننده:", 0, 3); AddField(parameterTable, "فرمت زمان:", 2, 3); AddField(parameterTable, "فرمت تاریخ:", 0, 4); AddField(parameterTable, "Header:", 2, 4); AddField(parameterTable, "تقویم:", 0, 5); AddField(parameterTable, "تعداد نماد:", 2, 5);
            parametersGroup.Controls.Add(parameterTable);

            existingGroup.Text = "سبدهای موجود"; existingGroup.Dock = DockStyle.Fill; existingGroup.Padding = new Padding(10);
            var existingLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes };
            existingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); existingLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            portfoliosListBox.Dock = DockStyle.Fill; portfoliosListBox.IntegralHeight = false; existingLayout.Controls.Add(portfoliosListBox, 0, 0);
            deletePortfoliosButton.Text = "حذف سبدهای انتخاب شده"; deletePortfoliosButton.Width = 205; deletePortfoliosButton.Height = 36; deletePortfoliosButton.Margin = new Padding(4);
            var existingButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes }; existingButtons.Controls.Add(deletePortfoliosButton); existingLayout.Controls.Add(existingButtons, 0, 1); existingGroup.Controls.Add(existingLayout);
            rootTable.Controls.Add(upper, 0, 1);

            symbolsGroup.Text = "نمادهای سبد"; symbolsGroup.Dock = DockStyle.Fill; symbolsGroup.Padding = new Padding(10);
            var symbolLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes };
            symbolLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); symbolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            symbolsGrid.Dock = DockStyle.Fill; symbolsGrid.AllowUserToAddRows = false; symbolsGrid.AllowUserToDeleteRows = false; symbolsGrid.ReadOnly = true; symbolsGrid.AutoGenerateColumns = false; symbolsGrid.RowHeadersVisible = false; symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; symbolsGrid.RightToLeft = RightToLeft.Yes; symbolsGrid.BackgroundColor = SystemColors.Window;
            symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ردیف", Width = 75 }); symbolsGrid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "انتخاب", Width = 85 }); symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", Width = 140 }); symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام نمایشی", Width = 180 }); symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "آخرین معامله", Width = 150 }); symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "حجم", Width = 130 }); symbolsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "قیمت پایانی", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            symbolLayout.Controls.Add(symbolsGrid, 0, 0);
            deleteSymbolsButton.Text = "حذف نمادهای انتخاب شده"; deleteSymbolsButton.Width = 245; deleteSymbolsButton.Height = 36; deleteSymbolsButton.Margin = new Padding(4);
            var symbolButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes }; symbolButtons.Controls.Add(deleteSymbolsButton); symbolLayout.Controls.Add(symbolButtons, 0, 1);
            symbolsGroup.Controls.Add(symbolLayout); rootTable.Controls.Add(symbolsGroup, 0, 2);

            footerPanel.Dock = DockStyle.Fill; footerPanel.BorderStyle = BorderStyle.FixedSingle;
            statusLabel.Text = "هیچ سبدی وجود ندارد."; statusLabel.Dock = DockStyle.Right; statusLabel.Width = 320; statusLabel.TextAlign = ContentAlignment.MiddleRight; statusLabel.Padding = new Padding(4);
            closeButton.Text = "بستن"; closeButton.Width = 125; closeButton.Height = 36; closeButton.Location = new Point(12, 10);
            footerPanel.Controls.Add(statusLabel); footerPanel.Controls.Add(closeButton); rootTable.Controls.Add(footerPanel, 0, 3);
            Controls.Add(rootTable);
            ResumeLayout(false);
        }

        private static void AddField(TableLayoutPanel table, string labelText, int labelColumn, int row)
        {
            table.Controls.Add(new Label { Text = labelText, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4) }, labelColumn, row);
            table.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, labelColumn + 1, row);
        }
    }
}
