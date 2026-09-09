namespace Trade.It
{
    partial class PortfolioManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button reloadButton;
        private Label portfolioNameCaption;
        private Label portfolioNameLabel;
        private Label sourceTypeCaption;
        private Label sourceTypeLabel;
        private Label dataPathCaption;
        private Label dataPathLabel;
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
            reloadButton = new Button();
            portfolioNameCaption = new Label();
            portfolioNameLabel = new Label();
            sourceTypeCaption = new Label();
            sourceTypeLabel = new Label();
            dataPathCaption = new Label();
            dataPathLabel = new Label();
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
            existingTitleLabel = new Label();
            portfoliosListBox = new ListBox();
            deletePortfoliosButton = new Button();
            symbolsTitleLabel = new Label();
            symbolsGrid = new DataGridView();
            deleteSymbolsButton = new Button();
            statusLabel = new Label();
            closeButton = new Button();
            groupBox1 = new GroupBox();
            NoDateTimeLabel = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)symbolsGrid).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // reloadButton
            // 
            reloadButton.Location = new Point(581, 769);
            reloadButton.Name = "reloadButton";
            reloadButton.Size = new Size(110, 41);
            reloadButton.TabIndex = 1;
            reloadButton.Text = "بازنشانی";
            // 
            // portfolioNameCaption
            // 
            portfolioNameCaption.Location = new Point(765, 51);
            portfolioNameCaption.Name = "portfolioNameCaption";
            portfolioNameCaption.Size = new Size(100, 34);
            portfolioNameCaption.TabIndex = 3;
            portfolioNameCaption.Text = "نام سبد:";
            portfolioNameCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // portfolioNameLabel
            // 
            portfolioNameLabel.BorderStyle = BorderStyle.FixedSingle;
            portfolioNameLabel.Location = new Point(459, 51);
            portfolioNameLabel.Name = "portfolioNameLabel";
            portfolioNameLabel.Size = new Size(300, 34);
            portfolioNameLabel.TabIndex = 4;
            portfolioNameLabel.Text = "—";
            portfolioNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sourceTypeCaption
            // 
            sourceTypeCaption.Location = new Point(765, 169);
            sourceTypeCaption.Name = "sourceTypeCaption";
            sourceTypeCaption.Size = new Size(135, 34);
            sourceTypeCaption.TabIndex = 5;
            sourceTypeCaption.Text = "نوع فایل داده:";
            sourceTypeCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // sourceTypeLabel
            // 
            sourceTypeLabel.BorderStyle = BorderStyle.FixedSingle;
            sourceTypeLabel.Location = new Point(459, 169);
            sourceTypeLabel.Name = "sourceTypeLabel";
            sourceTypeLabel.Size = new Size(300, 34);
            sourceTypeLabel.TabIndex = 6;
            sourceTypeLabel.Text = "—";
            sourceTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataPathCaption
            // 
            dataPathCaption.Location = new Point(765, 91);
            dataPathCaption.Name = "dataPathCaption";
            dataPathCaption.Size = new Size(100, 34);
            dataPathCaption.TabIndex = 7;
            dataPathCaption.Text = "مسیر داده:";
            dataPathCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataPathLabel
            // 
            dataPathLabel.AutoEllipsis = true;
            dataPathLabel.BorderStyle = BorderStyle.FixedSingle;
            dataPathLabel.Location = new Point(459, 91);
            dataPathLabel.Name = "dataPathLabel";
            dataPathLabel.Size = new Size(300, 34);
            dataPathLabel.TabIndex = 8;
            dataPathLabel.Text = "—";
            dataPathLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // symbolSourceCaption
            // 
            symbolSourceCaption.Location = new Point(764, 130);
            symbolSourceCaption.Name = "symbolSourceCaption";
            symbolSourceCaption.Size = new Size(129, 34);
            symbolSourceCaption.TabIndex = 11;
            symbolSourceCaption.Text = "منبع نام نماد:";
            symbolSourceCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // symbolSourceLabel
            // 
            symbolSourceLabel.BorderStyle = BorderStyle.FixedSingle;
            symbolSourceLabel.Location = new Point(459, 130);
            symbolSourceLabel.Name = "symbolSourceLabel";
            symbolSourceLabel.Size = new Size(300, 34);
            symbolSourceLabel.TabIndex = 12;
            symbolSourceLabel.Text = "—";
            symbolSourceLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // separatorCaption
            // 
            separatorCaption.Location = new Point(272, 51);
            separatorCaption.Name = "separatorCaption";
            separatorCaption.Size = new Size(100, 34);
            separatorCaption.TabIndex = 13;
            separatorCaption.Text = "جداکننده:";
            separatorCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // separatorLabel
            // 
            separatorLabel.BorderStyle = BorderStyle.FixedSingle;
            separatorLabel.Location = new Point(25, 51);
            separatorLabel.Name = "separatorLabel";
            separatorLabel.Size = new Size(233, 34);
            separatorLabel.TabIndex = 14;
            separatorLabel.Text = "—";
            separatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timeFormatCaption
            // 
            timeFormatCaption.Location = new Point(272, 208);
            timeFormatCaption.Name = "timeFormatCaption";
            timeFormatCaption.Size = new Size(111, 34);
            timeFormatCaption.TabIndex = 15;
            timeFormatCaption.Text = "فرمت زمان:";
            timeFormatCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // timeFormatLabel
            // 
            timeFormatLabel.BorderStyle = BorderStyle.FixedSingle;
            timeFormatLabel.Location = new Point(25, 208);
            timeFormatLabel.Name = "timeFormatLabel";
            timeFormatLabel.Size = new Size(233, 34);
            timeFormatLabel.TabIndex = 16;
            timeFormatLabel.Text = "—";
            timeFormatLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dateFormatCaption
            // 
            dateFormatCaption.Location = new Point(272, 168);
            dateFormatCaption.Name = "dateFormatCaption";
            dateFormatCaption.Size = new Size(122, 34);
            dateFormatCaption.TabIndex = 17;
            dateFormatCaption.Text = "فرمت تاریخ:";
            dateFormatCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateFormatLabel
            // 
            dateFormatLabel.BorderStyle = BorderStyle.FixedSingle;
            dateFormatLabel.Location = new Point(25, 168);
            dateFormatLabel.Name = "dateFormatLabel";
            dateFormatLabel.Size = new Size(233, 34);
            dateFormatLabel.TabIndex = 18;
            dateFormatLabel.Text = "—";
            dateFormatLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // headerCaption
            // 
            headerCaption.Location = new Point(765, 208);
            headerCaption.Name = "headerCaption";
            headerCaption.Size = new Size(100, 34);
            headerCaption.TabIndex = 19;
            headerCaption.Text = "Header:";
            headerCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // headerLabel
            // 
            headerLabel.BorderStyle = BorderStyle.FixedSingle;
            headerLabel.Location = new Point(459, 208);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(300, 34);
            headerLabel.TabIndex = 20;
            headerLabel.Text = "—";
            headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // calendarCaption
            // 
            calendarCaption.Location = new Point(275, 128);
            calendarCaption.Name = "calendarCaption";
            calendarCaption.Size = new Size(100, 34);
            calendarCaption.TabIndex = 21;
            calendarCaption.Text = "تقویم:";
            calendarCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // calendarLabel
            // 
            calendarLabel.BorderStyle = BorderStyle.FixedSingle;
            calendarLabel.Location = new Point(25, 128);
            calendarLabel.Name = "calendarLabel";
            calendarLabel.Size = new Size(233, 34);
            calendarLabel.TabIndex = 22;
            calendarLabel.Text = "—";
            calendarLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // symbolCountCaption
            // 
            symbolCountCaption.Location = new Point(764, 251);
            symbolCountCaption.Name = "symbolCountCaption";
            symbolCountCaption.Size = new Size(100, 34);
            symbolCountCaption.TabIndex = 23;
            symbolCountCaption.Text = "تعداد نماد:";
            symbolCountCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // symbolCountLabel
            // 
            symbolCountLabel.BorderStyle = BorderStyle.FixedSingle;
            symbolCountLabel.Location = new Point(459, 251);
            symbolCountLabel.Name = "symbolCountLabel";
            symbolCountLabel.Size = new Size(300, 34);
            symbolCountLabel.TabIndex = 24;
            symbolCountLabel.Text = "۰";
            symbolCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // existingTitleLabel
            // 
            existingTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            existingTitleLabel.Location = new Point(20, 12);
            existingTitleLabel.Name = "existingTitleLabel";
            existingTitleLabel.Size = new Size(180, 34);
            existingTitleLabel.TabIndex = 25;
            existingTitleLabel.Text = "سبدهای موجود";
            existingTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // portfoliosListBox
            // 
            portfoliosListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            portfoliosListBox.IntegralHeight = false;
            portfoliosListBox.ItemHeight = 28;
            portfoliosListBox.Location = new Point(54, 49);
            portfoliosListBox.Name = "portfoliosListBox";
            portfoliosListBox.Size = new Size(410, 269);
            portfoliosListBox.TabIndex = 26;
            // 
            // deletePortfoliosButton
            // 
            deletePortfoliosButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deletePortfoliosButton.Location = new Point(354, 323);
            deletePortfoliosButton.Name = "deletePortfoliosButton";
            deletePortfoliosButton.Size = new Size(110, 40);
            deletePortfoliosButton.TabIndex = 27;
            deletePortfoliosButton.Text = "حذف سبد";
            // 
            // symbolsTitleLabel
            // 
            symbolsTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            symbolsTitleLabel.Location = new Point(-13, 352);
            symbolsTitleLabel.Name = "symbolsTitleLabel";
            symbolsTitleLabel.Size = new Size(136, 34);
            symbolsTitleLabel.TabIndex = 28;
            symbolsTitleLabel.Text = "نمادهای سبد";
            symbolsTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // symbolsGrid
            // 
            symbolsGrid.AllowUserToAddRows = false;
            symbolsGrid.AllowUserToDeleteRows = false;
            symbolsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            symbolsGrid.BackgroundColor = SystemColors.Window;
            symbolsGrid.ColumnHeadersHeight = 34;
            symbolsGrid.Location = new Point(20, 390);
            symbolsGrid.Name = "symbolsGrid";
            symbolsGrid.ReadOnly = true;
            symbolsGrid.RightToLeft = RightToLeft.Yes;
            symbolsGrid.RowHeadersVisible = false;
            symbolsGrid.RowHeadersWidth = 62;
            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            symbolsGrid.Size = new Size(1368, 373);
            symbolsGrid.TabIndex = 29;
            // 
            // deleteSymbolsButton
            // 
            deleteSymbolsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteSymbolsButton.Location = new Point(1263, 766);
            deleteSymbolsButton.Name = "deleteSymbolsButton";
            deleteSymbolsButton.Size = new Size(125, 40);
            deleteSymbolsButton.TabIndex = 30;
            deleteSymbolsButton.Text = "حذف نماد";
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.Font = new Font("Segoe UI", 9F);
            statusLabel.Location = new Point(54, 322);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(294, 34);
            statusLabel.TabIndex = 31;
            statusLabel.Text = "هیچ سبدی انتخاب نشده است.";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Location = new Point(715, 769);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(125, 41);
            closeButton.TabIndex = 32;
            closeButton.Text = "بستن";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(NoDateTimeLabel);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dataPathCaption);
            groupBox1.Controls.Add(symbolCountCaption);
            groupBox1.Controls.Add(headerCaption);
            groupBox1.Controls.Add(timeFormatCaption);
            groupBox1.Controls.Add(dateFormatLabel);
            groupBox1.Controls.Add(dateFormatCaption);
            groupBox1.Controls.Add(separatorLabel);
            groupBox1.Controls.Add(separatorCaption);
            groupBox1.Controls.Add(calendarLabel);
            groupBox1.Controls.Add(calendarCaption);
            groupBox1.Controls.Add(sourceTypeLabel);
            groupBox1.Controls.Add(sourceTypeCaption);
            groupBox1.Controls.Add(portfolioNameLabel);
            groupBox1.Controls.Add(portfolioNameCaption);
            groupBox1.Controls.Add(symbolSourceCaption);
            groupBox1.Controls.Add(dataPathLabel);
            groupBox1.Controls.Add(symbolCountLabel);
            groupBox1.Controls.Add(headerLabel);
            groupBox1.Controls.Add(timeFormatLabel);
            groupBox1.Controls.Add(symbolSourceLabel);
            groupBox1.Location = new Point(478, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(910, 306);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "پارامترهای سبد:";
            // 
            // NoDateTimeLabel
            // 
            NoDateTimeLabel.BorderStyle = BorderStyle.FixedSingle;
            NoDateTimeLabel.Location = new Point(25, 91);
            NoDateTimeLabel.Name = "NoDateTimeLabel";
            NoDateTimeLabel.Size = new Size(233, 34);
            NoDateTimeLabel.TabIndex = 26;
            NoDateTimeLabel.Text = "—";
            NoDateTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(272, 91);
            label2.Name = "label2";
            label2.Size = new Size(176, 34);
            label2.TabIndex = 25;
            label2.Text = "فاقد تاریخ و زمان؟:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PortfolioManagementForm
            // 
            ClientSize = new Size(1400, 820);
            Controls.Add(groupBox1);
            Controls.Add(reloadButton);
            Controls.Add(portfoliosListBox);
            Controls.Add(existingTitleLabel);
            Controls.Add(deletePortfoliosButton);
            Controls.Add(symbolsTitleLabel);
            Controls.Add(symbolsGrid);
            Controls.Add(deleteSymbolsButton);
            Controls.Add(statusLabel);
            Controls.Add(closeButton);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 700);
            Name = "PortfolioManagementForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "مدیریت سبدها";
            ((System.ComponentModel.ISupportInitialize)symbolsGrid).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }
        private GroupBox groupBox1;
        private Label NoDateTimeLabel;
        private Label label2;
    }
}