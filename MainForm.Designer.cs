namespace Trade.It
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem portfolioDefinitionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolioManagementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllChartsMenuItem;

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TabControl controlTabControl;
        private System.Windows.Forms.TabPage stocksTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage marketsTabPage;

        private System.Windows.Forms.ComboBox portfolioComboBox;
        private System.Windows.Forms.Label portfolioLabel;
        private System.Windows.Forms.DataGridView stocksDataGridView;
        private System.Windows.Forms.Button navigationButton;
        private System.Windows.Forms.Button newPortfolioButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
        private System.Windows.Forms.CheckBox selectNoneCheckBox;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TextBox navigationSpeedTextBox;
        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.Panel chartToolbarPanel;
        private System.Windows.Forms.ComboBox chartTypeComboBox;
        private System.Windows.Forms.Button gridButton;
        private System.Windows.Forms.Button crossButton;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.Button resetChartButton;
        private System.Windows.Forms.Button hideChartButton;
        private System.Windows.Forms.Button hideToolsButton;
        private System.Windows.Forms.Button printChartButton;
        private System.Windows.Forms.Button snapshotChartButton;
        private System.Windows.Forms.Button fullScreenChartButton;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage chartTabPage;
        private System.Windows.Forms.Panel chartInfoPanel;
        private System.Windows.Forms.Label chartInfoLabel;
        private System.Windows.Forms.Label chartPlaceholderLabel;
        private System.Windows.Forms.Button refreshButtonPortfolio;
        private System.Windows.Forms.Label filterCountLabel;

        private System.Windows.Forms.Button filterTradingClearButton;
        private System.Windows.Forms.Button filterNameClearButton;
        private System.Windows.Forms.Button filterVolumeClearButton;
        private System.Windows.Forms.Button filterPastDaysClearButton;
        private System.Windows.Forms.Button filterComparison7ClearButton;
        private System.Windows.Forms.Button filterComparison8ClearButton;
        private System.Windows.Forms.Button filterComparison9ClearButton;
        private System.Windows.Forms.Button filterOhlcChangeClearButton;

        private System.Windows.Forms.GroupBox tradingStatusGroup;
        private System.Windows.Forms.RadioButton statusAllRadio;
        private System.Windows.Forms.RadioButton statusPositiveRadio;
        private System.Windows.Forms.RadioButton statusNegativeRadio;
        private System.Windows.Forms.GroupBox nameFilterGroup;
        private System.Windows.Forms.ComboBox nameComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox volumeRatioGroup;
        private System.Windows.Forms.TextBox volumeRatioTextBox;
        private System.Windows.Forms.ComboBox volumeRatioOperatorComboBox;
        private System.Windows.Forms.GroupBox pastDaysGroup;
        private System.Windows.Forms.TextBox pastDaysTextBox;
        private System.Windows.Forms.ComboBox pastDaysStatusComboBox;

        private System.Windows.Forms.GroupBox comparisonGroup8;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox2;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox1;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox2;
        private System.Windows.Forms.TextBox comparisonFirstTextBox1;
        private System.Windows.Forms.TextBox comparisonSecondTextBox1;
        private System.Windows.Forms.Label label5_8;
        private System.Windows.Forms.Label label6_8;
        private System.Windows.Forms.Label label8_8;

        private System.Windows.Forms.GroupBox ohlcChangeFilterGroup;
        private System.Windows.Forms.ComboBox ohlcChangeFieldComboBox;
        private System.Windows.Forms.TextBox ohlcChangeDaysTextBox;
        private System.Windows.Forms.TextBox ohlcChangePercentTextBox;
        private System.Windows.Forms.ComboBox ohlcChangeDirectionComboBox;
        private System.Windows.Forms.Label ohlcChangeFieldLabel;
        private System.Windows.Forms.Label ohlcChangeDaysLabel;
        private System.Windows.Forms.Label ohlcChangePercentLabel;

        private System.Windows.Forms.GroupBox identifierMainGroup;
        private System.Windows.Forms.TableLayoutPanel identifierLayout;
        private System.Windows.Forms.Label identifierSymbolLabel;
        private System.Windows.Forms.TextBox identifierSymbolTextBox;
        private System.Windows.Forms.Label identifierNameLabel;
        private System.Windows.Forms.TextBox identifierNameTextBox;
        private System.Windows.Forms.Label identifierTsetmcLabel;
        private System.Windows.Forms.TextBox identifierTsetmcTextBox;
        private System.Windows.Forms.Label identifierMarketLabel;
        private System.Windows.Forms.ComboBox identifierMarketComboBox;
        private System.Windows.Forms.Label identifierGroupLabel;
        private System.Windows.Forms.TextBox identifierGroupTextBox;
        private System.Windows.Forms.Label identifierDescriptionLabel;
        private System.Windows.Forms.TextBox identifierDescriptionTextBox;
        private System.Windows.Forms.Label identifierPersianSymbolLabel;
        private System.Windows.Forms.TextBox identifierPersianSymbolTextBox;
        private System.Windows.Forms.Label identifierPersian30SymbolLabel;
        private System.Windows.Forms.TextBox identifierPersian30SymbolTextBox;
        private System.Windows.Forms.Label identifierCompanyCode12Label;
        private System.Windows.Forms.TextBox identifierCompanyCode12TextBox;
        private System.Windows.Forms.Label identifierBoardCodeLabel;
        private System.Windows.Forms.TextBox identifierBoardCodeTextBox;
        private System.Windows.Forms.Label identifierIndustryCodeLabel;
        private System.Windows.Forms.TextBox identifierIndustryCodeTextBox;
        private System.Windows.Forms.Label identifierIndustryLabel;
        private System.Windows.Forms.TextBox identifierIndustryTextBox;
        private System.Windows.Forms.Label identifierSubIndustryCodeLabel;
        private System.Windows.Forms.TextBox identifierSubIndustryCodeTextBox;
        private System.Windows.Forms.Label identifierSubIndustryLabel;
        private System.Windows.Forms.TextBox identifierSubIndustryTextBox;
        private System.Windows.Forms.FlowLayoutPanel identifierButtonsPanel;
        private System.Windows.Forms.Button identifierNewButton;
        private System.Windows.Forms.Button identifierSaveButton;
        private System.Windows.Forms.Button identifierDeleteButton;
        private System.Windows.Forms.Button identifierDeleteAllButton;
        private System.Windows.Forms.Button identifierImportExcelButton;

        private System.Windows.Forms.Label marketTypeLabel;
        private System.Windows.Forms.TextBox marketTypeTextBox;
        private System.Windows.Forms.Label marketNameLabel;
        private System.Windows.Forms.TextBox marketNameTextBox;
        private System.Windows.Forms.Label marketBoardLabel;
        private System.Windows.Forms.TextBox marketBoardTextBox;
        private System.Windows.Forms.Label marketIndustryGroupLabel;
        private System.Windows.Forms.TextBox marketIndustryGroupTextBox;
        private System.Windows.Forms.Label marketSubIndustryGroupLabel;
        private System.Windows.Forms.TextBox marketSubIndustryGroupTextBox;
        private System.Windows.Forms.GroupBox marketsMainGroup;
        private System.Windows.Forms.TableLayoutPanel marketsLayout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainMenuStrip = new MenuStrip();
            portfolioDefinitionMenuItem = new ToolStripMenuItem();
            portfolioManagementMenuItem = new ToolStripMenuItem();
            settingsMenuItem = new ToolStripMenuItem();
            resetMenuItem = new ToolStripMenuItem();
            closeAllChartsMenuItem = new ToolStripMenuItem();
            mainSplitContainer = new SplitContainer();
            controlTabControl = new TabControl();
            stocksTabPage = new TabPage();
            groupBox2 = new GroupBox();
            navigationButton = new Button();
            selectAllCheckBox = new CheckBox();
            selectNoneCheckBox = new CheckBox();
            speedLabel = new Label();
            refreshButton = new Button();
            deleteButton = new Button();
            newPortfolioButton = new Button();
            navigationSpeedTextBox = new TextBox();
            stocksDataGridView = new DataGridView();
            rowColumn = new DataGridViewTextBoxColumn();
            symbolColumn = new DataGridViewTextBoxColumn();
            lastTradeColumn = new DataGridViewTextBoxColumn();
            selectColumn = new DataGridViewCheckBoxColumn();
            portfolioComboBox = new ComboBox();
            portfolioLabel = new Label();
            tabPage2 = new TabPage();
            groupBox3 = new GroupBox();
            label7 = new Label();
            comparisonSecondTextBox3 = new TextBox();
            label8 = new Label();
            comparisonFirstTextBox3 = new TextBox();
            label9 = new Label();
            comparisonFirstComboBox3 = new ComboBox();
            comparisonOperatorComboBox3 = new ComboBox();
            comparisonSecondComboBox3 = new ComboBox();
            clearFiltersButton = new Button();
            filterCountLabel = new Label();
            filterTradingClearButton = new Button();
            filterNameClearButton = new Button();
            filterVolumeClearButton = new Button();
            filterPastDaysClearButton = new Button();
            filterComparison7ClearButton = new Button();
            filterComparison8ClearButton = new Button();
            filterComparison9ClearButton = new Button();
            filterOhlcChangeClearButton = new Button();
            ohlcChangeFilterGroup = new GroupBox();
            ohlcChangeFieldLabel = new Label();
            ohlcChangeFieldComboBox = new ComboBox();
            ohlcChangeDaysLabel = new Label();
            ohlcChangeDaysTextBox = new TextBox();
            ohlcChangePercentLabel = new Label();
            ohlcChangePercentTextBox = new TextBox();
            ohlcChangeDirectionComboBox = new ComboBox();
            comparisonGroup8 = new GroupBox();
            label8_8 = new Label();
            comparisonSecondTextBox1 = new TextBox();
            label6_8 = new Label();
            comparisonFirstTextBox1 = new TextBox();
            label5_8 = new Label();
            comparisonFirstComboBox2 = new ComboBox();
            comparisonOperatorComboBox1 = new ComboBox();
            comparisonSecondComboBox2 = new ComboBox();
            comparisonGroup7 = new GroupBox();
            label1 = new Label();
            comparisonSecondTextBox2 = new TextBox();
            label5 = new Label();
            comparisonFirstTextBox2 = new TextBox();
            label6 = new Label();
            comparisonFirstComboBox1 = new ComboBox();
            comparisonOperatorComboBox2 = new ComboBox();
            comparisonSecondComboBox1 = new ComboBox();
            pastDaysGroup = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            pastDaysTextBox = new TextBox();
            pastDaysStatusComboBox = new ComboBox();
            label5_7 = new Label();
            volumeRatioGroup = new GroupBox();
            label2 = new Label();
            volumeRatioOperatorComboBox = new ComboBox();
            textBox1 = new TextBox();
            volumeRatioTextBox = new TextBox();
            nameFilterGroup = new GroupBox();
            nameComboBox = new ComboBox();
            nameTextBox = new TextBox();
            tradingStatusGroup = new GroupBox();
            statusAllRadio = new RadioButton();
            statusPositiveRadio = new RadioButton();
            statusNegativeRadio = new RadioButton();
            tabPage3 = new TabPage();
            marketsTabPage = new TabPage();
            chartPanel = new Panel();
            chartTabControl = new TabControl();
            chartTabPage = new TabPage();
            chartInfoPanel = new Panel();
            chartInfoLabel = new Label();
            chartPlaceholderLabel = new Label();
            chartToolbarPanel = new Panel();
            chartTypeComboBox = new ComboBox();
            gridButton = new Button();
            crossButton = new Button();
            zoomInButton = new Button();
            zoomOutButton = new Button();
            resetChartButton = new Button();
            hideChartButton = new Button();
            hideToolsButton = new Button();
            printChartButton = new Button();
            snapshotChartButton = new Button();
            fullScreenChartButton = new Button();
            groupBox1 = new GroupBox();
            refreshButtonPortfolio = new Button();
            identifierMainGroup = new GroupBox();
            identifierLayout = new TableLayoutPanel();
            identifierSymbolLabel = new Label();
            identifierSymbolTextBox = new TextBox();
            identifierNameLabel = new Label();
            identifierNameTextBox = new TextBox();
            identifierTsetmcLabel = new Label();
            identifierTsetmcTextBox = new TextBox();
            identifierMarketLabel = new Label();
            identifierMarketComboBox = new ComboBox();
            identifierGroupLabel = new Label();
            identifierGroupTextBox = new TextBox();
            identifierDescriptionLabel = new Label();
            identifierDescriptionTextBox = new TextBox();
            identifierPersianSymbolLabel = new Label();
            identifierPersianSymbolTextBox = new TextBox();
            identifierPersian30SymbolLabel = new Label();
            identifierPersian30SymbolTextBox = new TextBox();
            identifierCompanyCode12Label = new Label();
            identifierCompanyCode12TextBox = new TextBox();
            identifierBoardCodeLabel = new Label();
            identifierBoardCodeTextBox = new TextBox();
            identifierIndustryCodeLabel = new Label();
            identifierIndustryCodeTextBox = new TextBox();
            identifierIndustryLabel = new Label();
            identifierIndustryTextBox = new TextBox();
            identifierSubIndustryCodeLabel = new Label();
            identifierSubIndustryCodeTextBox = new TextBox();
            identifierSubIndustryLabel = new Label();
            identifierSubIndustryTextBox = new TextBox();
            identifierButtonsPanel = new FlowLayoutPanel();
            identifierNewButton = new Button();
            identifierSaveButton = new Button();
            identifierDeleteButton = new Button();
            identifierDeleteAllButton = new Button();
            identifierImportExcelButton = new Button();
            marketTypeLabel = new Label();
            marketTypeTextBox = new TextBox();
            marketNameLabel = new Label();
            marketNameTextBox = new TextBox();
            marketBoardLabel = new Label();
            marketBoardTextBox = new TextBox();
            marketIndustryGroupLabel = new Label();
            marketIndustryGroupTextBox = new TextBox();
            marketSubIndustryGroupLabel = new Label();
            marketSubIndustryGroupTextBox = new TextBox();
            marketsMainGroup = new GroupBox();
            marketsLayout = new TableLayoutPanel();
            mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            controlTabControl.SuspendLayout();
            stocksTabPage.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).BeginInit();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            ohlcChangeFilterGroup.SuspendLayout();
            comparisonGroup8.SuspendLayout();
            comparisonGroup7.SuspendLayout();
            pastDaysGroup.SuspendLayout();
            volumeRatioGroup.SuspendLayout();
            nameFilterGroup.SuspendLayout();
            tradingStatusGroup.SuspendLayout();
            chartPanel.SuspendLayout();
            chartTabControl.SuspendLayout();
            chartTabPage.SuspendLayout();
            chartInfoPanel.SuspendLayout();
            chartToolbarPanel.SuspendLayout();
            identifierMainGroup.SuspendLayout();
            identifierButtonsPanel.SuspendLayout();
            marketsMainGroup.SuspendLayout();
            marketsLayout.SuspendLayout();
            SuspendLayout();
            //
            // mainMenuStrip
            //
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem, resetMenuItem, closeAllChartsMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.RightToLeft = RightToLeft.No;
            mainMenuStrip.Size = new Size(1643, 33);
            mainMenuStrip.TabIndex = 0;
            //
            // portfolioDefinitionMenuItem
            //
            portfolioDefinitionMenuItem.Name = "portfolioDefinitionMenuItem";
            portfolioDefinitionMenuItem.Size = new Size(114, 29);
            portfolioDefinitionMenuItem.Text = "تعریف سبد";
            //
            // portfolioManagementMenuItem
            //
            portfolioManagementMenuItem.Name = "portfolioManagementMenuItem";
            portfolioManagementMenuItem.Size = new Size(122, 29);
            portfolioManagementMenuItem.Text = "مدیریت سبد";
            //
            // settingsMenuItem
            //
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new Size(94, 29);
            settingsMenuItem.Text = "تنظیمات";
            //
            // resetMenuItem
            //
            resetMenuItem.Name = "resetMenuItem";
            resetMenuItem.Size = new Size(92, 29);
            resetMenuItem.Text = "بازنشانی";
            //
            // closeAllChartsMenuItem
            //
            closeAllChartsMenuItem.Name = "closeAllChartsMenuItem";
            closeAllChartsMenuItem.Size = new Size(104, 29);
            closeAllChartsMenuItem.Text = "بستن همه";
            //
            // mainSplitContainer
            //
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 33);
            mainSplitContainer.Name = "mainSplitContainer";
            //
            // mainSplitContainer.Panel1
            //
            mainSplitContainer.Panel1.Controls.Add(controlTabControl);
            mainSplitContainer.Panel1MinSize = 300;
            //
            // mainSplitContainer.Panel2
            //
            mainSplitContainer.Panel2.Controls.Add(chartPanel);
            mainSplitContainer.Panel2MinSize = 700;
            mainSplitContainer.RightToLeft = RightToLeft.No;
            mainSplitContainer.Size = new Size(1643, 933);
            mainSplitContainer.SplitterDistance = 400;
            mainSplitContainer.TabIndex = 1;
            //
            // controlTabControl
            //
            controlTabControl.Controls.Add(stocksTabPage);
            controlTabControl.Controls.Add(tabPage2);
            controlTabControl.Controls.Add(tabPage3);
            controlTabControl.Controls.Add(marketsTabPage);
            controlTabControl.Dock = DockStyle.Fill;
            controlTabControl.Location = new Point(0, 0);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.RightToLeft = RightToLeft.Yes;
            controlTabControl.RightToLeftLayout = true;
            controlTabControl.SelectedIndex = 0;
            controlTabControl.Size = new Size(400, 933);
            controlTabControl.TabIndex = 0;
            //
            // stocksTabPage
            //
            stocksTabPage.Controls.Add(groupBox2);
            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(portfolioComboBox);
            stocksTabPage.Controls.Add(portfolioLabel);
            stocksTabPage.Location = new Point(4, 34);
            stocksTabPage.Name = "stocksTabPage";
            stocksTabPage.Padding = new Padding(8);
            stocksTabPage.RightToLeft = RightToLeft.Yes;
            stocksTabPage.Size = new Size(392, 895);
            stocksTabPage.TabIndex = 0;
            stocksTabPage.Text = "سبد";
            //
            // groupBox2
            //
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(navigationButton);
            groupBox2.Controls.Add(selectAllCheckBox);
            groupBox2.Controls.Add(selectNoneCheckBox);
            groupBox2.Controls.Add(speedLabel);
            groupBox2.Controls.Add(refreshButton);
            groupBox2.Controls.Add(deleteButton);
            groupBox2.Controls.Add(newPortfolioButton);
            groupBox2.Controls.Add(navigationSpeedTextBox);
            groupBox2.Location = new Point(8, 720);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(376, 170);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            //
            // navigationButton
            //
            navigationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            navigationButton.Location = new Point(274, 33);
            navigationButton.Name = "navigationButton";
            navigationButton.Size = new Size(87, 34);
            navigationButton.TabIndex = 0;
            navigationButton.Text = "پیمایش";
            //
            // selectAllCheckBox
            //
            selectAllCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(265, 78);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(70, 29);
            selectAllCheckBox.TabIndex = 0;
            selectAllCheckBox.Text = "همه";
            //
            // selectNoneCheckBox
            //
            selectNoneCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new Point(53, 78);
            selectNoneCheckBox.Name = "selectNoneCheckBox";
            selectNoneCheckBox.Size = new Size(101, 29);
            selectNoneCheckBox.TabIndex = 1;
            selectNoneCheckBox.Text = "هیچکدام";
            //
            // speedLabel
            //
            speedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(112, 36);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(64, 25);
            speedLabel.TabIndex = 2;
            speedLabel.Text = "سرعت:";
            //
            // refreshButton
            //
            refreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            refreshButton.Location = new Point(114, 113);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(159, 34);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "تازه";
            //
            // deleteButton
            //
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.Location = new Point(288, 113);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(73, 34);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "حذف";
            //
            // newPortfolioButton
            //
            newPortfolioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            newPortfolioButton.Location = new Point(24, 113);
            newPortfolioButton.Name = "newPortfolioButton";
            newPortfolioButton.Size = new Size(69, 34);
            newPortfolioButton.TabIndex = 1;
            newPortfolioButton.Text = "جدید";
            //
            // navigationSpeedTextBox
            //
            navigationSpeedTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navigationSpeedTextBox.Location = new Point(24, 35);
            navigationSpeedTextBox.Name = "navigationSpeedTextBox";
            navigationSpeedTextBox.Size = new Size(87, 31);
            navigationSpeedTextBox.TabIndex = 3;
            navigationSpeedTextBox.Text = "1000";
            navigationSpeedTextBox.TextAlign = HorizontalAlignment.Center;
            //
            // stocksDataGridView
            //
            stocksDataGridView.AllowUserToAddRows = false;
            stocksDataGridView.AllowUserToDeleteRows = false;
            stocksDataGridView.AllowUserToOrderColumns = true;
            stocksDataGridView.AllowUserToResizeColumns = true;
            stocksDataGridView.AllowUserToResizeRows = false;
            stocksDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            stocksDataGridView.ColumnHeadersHeight = 34;
            stocksDataGridView.Columns.AddRange(new DataGridViewColumn[] { rowColumn, symbolColumn, lastTradeColumn, selectColumn });
            stocksDataGridView.Location = new Point(11, 47);
            stocksDataGridView.Name = "stocksDataGridView";
            stocksDataGridView.ReadOnly = false;
            stocksDataGridView.RowHeadersVisible = false;
            stocksDataGridView.RowHeadersWidth = 62;
            stocksDataGridView.Size = new Size(373, 672);
            stocksDataGridView.TabIndex = 0;
            //
            // rowColumn
            //
            rowColumn.HeaderText = "ردیف";
            rowColumn.MinimumWidth = 8;
            rowColumn.Name = "rowColumn";
            rowColumn.ReadOnly = true;
            rowColumn.Width = 87;
            //
            // symbolColumn
            //
            symbolColumn.HeaderText = "نماد";
            symbolColumn.MinimumWidth = 8;
            symbolColumn.Name = "symbolColumn";
            symbolColumn.ReadOnly = true;
            symbolColumn.Width = 79;
            //
            // lastTradeColumn
            //
            lastTradeColumn.HeaderText = "آخرین معامله";
            lastTradeColumn.MinimumWidth = 8;
            lastTradeColumn.Name = "lastTradeColumn";
            lastTradeColumn.ReadOnly = true;
            lastTradeColumn.Width = 150;
            //
            // selectColumn
            //
            selectColumn.HeaderText = "انتخاب";
            selectColumn.MinimumWidth = 8;
            selectColumn.Name = "selectColumn";
            selectColumn.ReadOnly = false;
            selectColumn.Width = 68;
            //
            // portfolioComboBox
            //
            portfolioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new Point(8, 3);
            portfolioComboBox.Name = "portfolioComboBox";
            portfolioComboBox.Size = new Size(312, 33);
            portfolioComboBox.TabIndex = 2;
            //
            // portfolioLabel
            //
            portfolioLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new Point(318, 6);
            portfolioLabel.Name = "portfolioLabel";
            portfolioLabel.Size = new Size(63, 25);
            portfolioLabel.TabIndex = 3;
            portfolioLabel.Text = "سبدها:";
            portfolioLabel.TextAlign = ContentAlignment.MiddleRight;
            //
            // tabPage2
            //
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(clearFiltersButton);
            tabPage2.Controls.Add(filterCountLabel);
            tabPage2.Controls.Add(ohlcChangeFilterGroup);
            tabPage2.Controls.Add(comparisonGroup8);
            tabPage2.Controls.Add(comparisonGroup7);
            tabPage2.Controls.Add(pastDaysGroup);
            tabPage2.Controls.Add(volumeRatioGroup);
            tabPage2.Controls.Add(nameFilterGroup);
            tabPage2.Controls.Add(tradingStatusGroup);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(8);
            tabPage2.RightToLeft = RightToLeft.Yes;
            tabPage2.Size = new Size(392, 895);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "فیلترها";
            //
            // filterComparison9ClearButton
            //
            filterComparison9ClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterComparison9ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison9ClearButton.Location = new Point(8, 2);
            filterComparison9ClearButton.Name = "filterComparison9ClearButton";
            filterComparison9ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison9ClearButton.Size = new Size(42, 24);
            filterComparison9ClearButton.TabIndex = 0;
            filterComparison9ClearButton.TabStop = false;
            filterComparison9ClearButton.Tag = "comparison9";
            filterComparison9ClearButton.Text = "پاک";
            filterComparison9ClearButton.UseVisualStyleBackColor = true;
            filterComparison9ClearButton.Click += IndividualFilterClearButton_Click;
            // groupBox3
            //
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(comparisonSecondTextBox3);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(comparisonFirstTextBox3);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(comparisonFirstComboBox3);
            groupBox3.Controls.Add(comparisonOperatorComboBox3);
            groupBox3.Controls.Add(comparisonSecondComboBox3);
            groupBox3.Location = new Point(8, 743);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(376, 111);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "9. مقایسه قیمت:";
            //
            // label7
            //
            label7.AutoSize = true;
            label7.Location = new Point(80, 73);
            label7.Name = "label7";
            label7.Size = new Size(101, 25);
            label7.TabIndex = 0;
            label7.Text = "کندل گذشته";
            //
            // comparisonSecondTextBox3
            //
            comparisonSecondTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox3.Location = new Point(182, 69);
            comparisonSecondTextBox3.Name = "comparisonSecondTextBox3";
            comparisonSecondTextBox3.Size = new Size(57, 31);
            comparisonSecondTextBox3.TabIndex = 1;
            comparisonSecondTextBox3.Text = "5";
            comparisonSecondTextBox3.TextAlign = HorizontalAlignment.Center;
            //
            // label8
            //
            label8.AutoSize = true;
            label8.Location = new Point(83, 37);
            label8.Name = "label8";
            label8.Size = new Size(101, 25);
            label8.TabIndex = 3;
            label8.Text = "کندل گذشته";
            //
            // comparisonFirstTextBox3
            //
            comparisonFirstTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox3.Location = new Point(182, 32);
            comparisonFirstTextBox3.Name = "comparisonFirstTextBox3";
            comparisonFirstTextBox3.Size = new Size(57, 31);
            comparisonFirstTextBox3.TabIndex = 4;
            comparisonFirstTextBox3.Text = "5";
            comparisonFirstTextBox3.TextAlign = HorizontalAlignment.Center;
            //
            // label9
            //
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(316, 35);
            label9.Name = "label9";
            label9.Size = new Size(55, 25);
            label9.TabIndex = 5;
            label9.Text = "قیمت";
            //
            // comparisonFirstComboBox3
            //
            comparisonFirstComboBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonFirstComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox3.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox3.Location = new Point(241, 29);
            comparisonFirstComboBox3.Name = "comparisonFirstComboBox3";
            comparisonFirstComboBox3.Size = new Size(75, 33);
            comparisonFirstComboBox3.TabIndex = 6;
            //
            // comparisonOperatorComboBox3
            //
            comparisonOperatorComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox3.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox3.Location = new Point(8, 35);
            comparisonOperatorComboBox3.Name = "comparisonOperatorComboBox3";
            comparisonOperatorComboBox3.Size = new Size(73, 33);
            comparisonOperatorComboBox3.TabIndex = 7;
            //
            // comparisonSecondComboBox3
            //
            comparisonSecondComboBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox3.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox3.Location = new Point(241, 68);
            comparisonSecondComboBox3.Name = "comparisonSecondComboBox3";
            comparisonSecondComboBox3.Size = new Size(75, 33);
            comparisonSecondComboBox3.TabIndex = 8;
            //
            // clearFiltersButton
            //
            clearFiltersButton.Location = new Point(8, 858);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(138, 36);
            clearFiltersButton.TabIndex = 9;
            clearFiltersButton.Text = "پاک کردن";
            clearFiltersButton.UseVisualStyleBackColor = true;
            //
            // filterCountLabel
            //
            filterCountLabel.AutoSize = false;
            filterCountLabel.BorderStyle = BorderStyle.None;
            filterCountLabel.Font = new Font("Segoe UI", 9F);
            filterCountLabel.Location = new Point(150, 858);
            filterCountLabel.Name = "filterCountLabel";
            filterCountLabel.RightToLeft = RightToLeft.Yes;
            filterCountLabel.Size = new Size(205, 36);
            filterCountLabel.TabIndex = 11;
            filterCountLabel.Text = "کل: ۰    پیدا شده: ۰";
            filterCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            //
            // filterOhlcChangeClearButton
            //
            filterOhlcChangeClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterOhlcChangeClearButton.Font = new Font("Segoe UI", 8.5F);
            filterOhlcChangeClearButton.Location = new Point(8, 2);
            filterOhlcChangeClearButton.Name = "filterOhlcChangeClearButton";
            filterOhlcChangeClearButton.RightToLeft = RightToLeft.Yes;
            filterOhlcChangeClearButton.Size = new Size(42, 24);
            filterOhlcChangeClearButton.TabIndex = 0;
            filterOhlcChangeClearButton.TabStop = false;
            filterOhlcChangeClearButton.Tag = "ohlcChange";
            filterOhlcChangeClearButton.Text = "پاک";
            filterOhlcChangeClearButton.UseVisualStyleBackColor = true;
            filterOhlcChangeClearButton.Click += IndividualFilterClearButton_Click;
            // ohlcChangeFilterGroup
            //
            ohlcChangeFilterGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeFieldLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeFieldComboBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDaysLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDaysTextBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangePercentLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangePercentTextBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDirectionComboBox);
            ohlcChangeFilterGroup.Location = new Point(8, 638);
            ohlcChangeFilterGroup.Name = "ohlcChangeFilterGroup";
            ohlcChangeFilterGroup.Size = new Size(376, 105);
            ohlcChangeFilterGroup.TabIndex = 8;
            ohlcChangeFilterGroup.TabStop = false;
            ohlcChangeFilterGroup.Text = "10. درصد رشد";
            //
            // ohlcChangeFieldLabel
            //
            ohlcChangeFieldLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ohlcChangeFieldLabel.AutoSize = true;
            ohlcChangeFieldLabel.Location = new Point(289, 31);
            ohlcChangeFieldLabel.Name = "ohlcChangeFieldLabel";
            ohlcChangeFieldLabel.Size = new Size(55, 25);
            ohlcChangeFieldLabel.TabIndex = 0;
            ohlcChangeFieldLabel.Text = "قیمت";
            //
            // ohlcChangeFieldComboBox
            //
            ohlcChangeFieldComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ohlcChangeFieldComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ohlcChangeFieldComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "پایانی" });
            ohlcChangeFieldComboBox.Location = new Point(206, 27);
            ohlcChangeFieldComboBox.Name = "ohlcChangeFieldComboBox";
            ohlcChangeFieldComboBox.Size = new Size(79, 33);
            ohlcChangeFieldComboBox.TabIndex = 1;
            //
            // ohlcChangeDaysLabel
            //
            ohlcChangeDaysLabel.AutoSize = true;
            ohlcChangeDaysLabel.Location = new Point(123, 31);
            ohlcChangeDaysLabel.Name = "ohlcChangeDaysLabel";
            ohlcChangeDaysLabel.Size = new Size(80, 25);
            ohlcChangeDaysLabel.TabIndex = 2;
            ohlcChangeDaysLabel.Text = "کندل قبل";
            //
            // ohlcChangeDaysTextBox
            //
            ohlcChangeDaysTextBox.Location = new Point(60, 28);
            ohlcChangeDaysTextBox.Name = "ohlcChangeDaysTextBox";
            ohlcChangeDaysTextBox.Size = new Size(57, 31);
            ohlcChangeDaysTextBox.TabIndex = 3;
            ohlcChangeDaysTextBox.Text = "5";
            ohlcChangeDaysTextBox.TextAlign = HorizontalAlignment.Center;
            //
            // ohlcChangePercentLabel
            //
            ohlcChangePercentLabel.AutoSize = true;
            ohlcChangePercentLabel.Location = new Point(74, 68);
            ohlcChangePercentLabel.Name = "ohlcChangePercentLabel";
            ohlcChangePercentLabel.Size = new Size(27, 25);
            ohlcChangePercentLabel.TabIndex = 4;
            ohlcChangePercentLabel.Text = "%";
            //
            // ohlcChangePercentTextBox
            //
            ohlcChangePercentTextBox.Location = new Point(111, 65);
            ohlcChangePercentTextBox.Name = "ohlcChangePercentTextBox";
            ohlcChangePercentTextBox.Size = new Size(60, 31);
            ohlcChangePercentTextBox.TabIndex = 5;
            ohlcChangePercentTextBox.Text = "5";
            ohlcChangePercentTextBox.TextAlign = HorizontalAlignment.Center;
            //
            // ohlcChangeDirectionComboBox
            //
            ohlcChangeDirectionComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ohlcChangeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ohlcChangeDirectionComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            ohlcChangeDirectionComboBox.Location = new Point(195, 66);
            ohlcChangeDirectionComboBox.Name = "ohlcChangeDirectionComboBox";
            ohlcChangeDirectionComboBox.Size = new Size(142, 33);
            ohlcChangeDirectionComboBox.TabIndex = 6;
            //
            // filterComparison8ClearButton
            //
            filterComparison8ClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterComparison8ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison8ClearButton.Location = new Point(8, 2);
            filterComparison8ClearButton.Name = "filterComparison8ClearButton";
            filterComparison8ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison8ClearButton.Size = new Size(42, 24);
            filterComparison8ClearButton.TabIndex = 0;
            filterComparison8ClearButton.TabStop = false;
            filterComparison8ClearButton.Tag = "comparison8";
            filterComparison8ClearButton.Text = "پاک";
            filterComparison8ClearButton.UseVisualStyleBackColor = true;
            filterComparison8ClearButton.Click += IndividualFilterClearButton_Click;
            // comparisonGroup8
            //
            comparisonGroup8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonGroup8.Controls.Add(label8_8);
            comparisonGroup8.Controls.Add(comparisonSecondTextBox1);
            comparisonGroup8.Controls.Add(label6_8);
            comparisonGroup8.Controls.Add(comparisonFirstTextBox1);
            comparisonGroup8.Controls.Add(label5_8);
            comparisonGroup8.Controls.Add(comparisonFirstComboBox2);
            comparisonGroup8.Controls.Add(comparisonOperatorComboBox1);
            comparisonGroup8.Controls.Add(comparisonSecondComboBox2);
            comparisonGroup8.Location = new Point(8, 519);
            comparisonGroup8.Name = "comparisonGroup8";
            comparisonGroup8.Size = new Size(376, 119);
            comparisonGroup8.TabIndex = 7;
            comparisonGroup8.TabStop = false;
            comparisonGroup8.Text = "8. مقایسه قیمت:";
            //
            // label8_8
            //
            label8_8.AutoSize = true;
            label8_8.Location = new Point(80, 74);
            label8_8.Name = "label8_8";
            label8_8.Size = new Size(101, 25);
            label8_8.TabIndex = 0;
            label8_8.Text = "کندل گذشته";
            //
            // comparisonSecondTextBox1
            //
            comparisonSecondTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox1.Location = new Point(182, 70);
            comparisonSecondTextBox1.Name = "comparisonSecondTextBox1";
            comparisonSecondTextBox1.Size = new Size(57, 31);
            comparisonSecondTextBox1.TabIndex = 1;
            comparisonSecondTextBox1.Text = "5";
            comparisonSecondTextBox1.TextAlign = HorizontalAlignment.Center;
            //
            // label6_8
            //
            label6_8.AutoSize = true;
            label6_8.Location = new Point(83, 38);
            label6_8.Name = "label6_8";
            label6_8.Size = new Size(101, 25);
            label6_8.TabIndex = 3;
            label6_8.Text = "کندل گذشته";
            //
            // comparisonFirstTextBox1
            //
            comparisonFirstTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox1.Location = new Point(182, 33);
            comparisonFirstTextBox1.Name = "comparisonFirstTextBox1";
            comparisonFirstTextBox1.Size = new Size(57, 31);
            comparisonFirstTextBox1.TabIndex = 4;
            comparisonFirstTextBox1.Text = "5";
            comparisonFirstTextBox1.TextAlign = HorizontalAlignment.Center;
            //
            // label5_8
            //
            label5_8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5_8.AutoSize = true;
            label5_8.Location = new Point(316, 36);
            label5_8.Name = "label5_8";
            label5_8.Size = new Size(55, 25);
            label5_8.TabIndex = 5;
            label5_8.Text = "قیمت";
            //
            // comparisonFirstComboBox2
            //
            comparisonFirstComboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonFirstComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox2.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox2.Location = new Point(241, 30);
            comparisonFirstComboBox2.Name = "comparisonFirstComboBox2";
            comparisonFirstComboBox2.Size = new Size(75, 33);
            comparisonFirstComboBox2.TabIndex = 6;
            //
            // comparisonOperatorComboBox1
            //
            comparisonOperatorComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox1.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox1.Location = new Point(8, 36);
            comparisonOperatorComboBox1.Name = "comparisonOperatorComboBox1";
            comparisonOperatorComboBox1.Size = new Size(73, 33);
            comparisonOperatorComboBox1.TabIndex = 7;
            //
            // comparisonSecondComboBox2
            //
            comparisonSecondComboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox2.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox2.Location = new Point(241, 69);
            comparisonSecondComboBox2.Name = "comparisonSecondComboBox2";
            comparisonSecondComboBox2.Size = new Size(75, 33);
            comparisonSecondComboBox2.TabIndex = 8;
            //
            // filterComparison7ClearButton
            //
            filterComparison7ClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterComparison7ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison7ClearButton.Location = new Point(8, 2);
            filterComparison7ClearButton.Name = "filterComparison7ClearButton";
            filterComparison7ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison7ClearButton.Size = new Size(42, 24);
            filterComparison7ClearButton.TabIndex = 0;
            filterComparison7ClearButton.TabStop = false;
            filterComparison7ClearButton.Tag = "comparison7";
            filterComparison7ClearButton.Text = "پاک";
            filterComparison7ClearButton.UseVisualStyleBackColor = true;
            filterComparison7ClearButton.Click += IndividualFilterClearButton_Click;
            // comparisonGroup7
            //
            comparisonGroup7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonGroup7.Controls.Add(label1);
            comparisonGroup7.Controls.Add(comparisonSecondTextBox2);
            comparisonGroup7.Controls.Add(label5);
            comparisonGroup7.Controls.Add(comparisonFirstTextBox2);
            comparisonGroup7.Controls.Add(label6);
            comparisonGroup7.Controls.Add(comparisonFirstComboBox1);
            comparisonGroup7.Controls.Add(comparisonOperatorComboBox2);
            comparisonGroup7.Controls.Add(comparisonSecondComboBox1);
            comparisonGroup7.Location = new Point(8, 400);
            comparisonGroup7.Name = "comparisonGroup7";
            comparisonGroup7.Size = new Size(376, 119);
            comparisonGroup7.TabIndex = 6;
            comparisonGroup7.TabStop = false;
            comparisonGroup7.Text = "7. مقایسه قیمت:";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(83, 77);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 9;
            label1.Text = "کندل گذشته";
            //
            // comparisonSecondTextBox2
            //
            comparisonSecondTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox2.Location = new Point(185, 73);
            comparisonSecondTextBox2.Name = "comparisonSecondTextBox2";
            comparisonSecondTextBox2.Size = new Size(57, 31);
            comparisonSecondTextBox2.TabIndex = 10;
            comparisonSecondTextBox2.Text = "5";
            comparisonSecondTextBox2.TextAlign = HorizontalAlignment.Center;
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new Point(86, 41);
            label5.Name = "label5";
            label5.Size = new Size(101, 25);
            label5.TabIndex = 11;
            label5.Text = "کندل گذشته";
            //
            // comparisonFirstTextBox2
            //
            comparisonFirstTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox2.Location = new Point(185, 36);
            comparisonFirstTextBox2.Name = "comparisonFirstTextBox2";
            comparisonFirstTextBox2.Size = new Size(57, 31);
            comparisonFirstTextBox2.TabIndex = 12;
            comparisonFirstTextBox2.Text = "5";
            comparisonFirstTextBox2.TextAlign = HorizontalAlignment.Center;
            //
            // label6
            //
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(319, 39);
            label6.Name = "label6";
            label6.Size = new Size(55, 25);
            label6.TabIndex = 13;
            label6.Text = "قیمت";
            //
            // comparisonFirstComboBox1
            //
            comparisonFirstComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonFirstComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox1.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox1.Location = new Point(244, 33);
            comparisonFirstComboBox1.Name = "comparisonFirstComboBox1";
            comparisonFirstComboBox1.Size = new Size(75, 33);
            comparisonFirstComboBox1.TabIndex = 14;
            //
            // comparisonOperatorComboBox2
            //
            comparisonOperatorComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox2.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox2.Location = new Point(11, 39);
            comparisonOperatorComboBox2.Name = "comparisonOperatorComboBox2";
            comparisonOperatorComboBox2.Size = new Size(73, 33);
            comparisonOperatorComboBox2.TabIndex = 15;
            //
            // comparisonSecondComboBox1
            //
            comparisonSecondComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox1.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox1.Location = new Point(244, 72);
            comparisonSecondComboBox1.Name = "comparisonSecondComboBox1";
            comparisonSecondComboBox1.Size = new Size(75, 33);
            comparisonSecondComboBox1.TabIndex = 16;
            //
            // filterPastDaysClearButton
            //
            filterPastDaysClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterPastDaysClearButton.Font = new Font("Segoe UI", 8.5F);
            filterPastDaysClearButton.Location = new Point(8, 2);
            filterPastDaysClearButton.Name = "filterPastDaysClearButton";
            filterPastDaysClearButton.RightToLeft = RightToLeft.Yes;
            filterPastDaysClearButton.Size = new Size(42, 24);
            filterPastDaysClearButton.TabIndex = 0;
            filterPastDaysClearButton.TabStop = false;
            filterPastDaysClearButton.Tag = "pastDays";
            filterPastDaysClearButton.Text = "پاک";
            filterPastDaysClearButton.UseVisualStyleBackColor = true;
            filterPastDaysClearButton.Click += IndividualFilterClearButton_Click;
            // pastDaysGroup
            //
            pastDaysGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pastDaysGroup.Controls.Add(label4);
            pastDaysGroup.Controls.Add(label3);
            pastDaysGroup.Controls.Add(pastDaysTextBox);
            pastDaysGroup.Controls.Add(pastDaysStatusComboBox);
            pastDaysGroup.Controls.Add(label5_7);
            pastDaysGroup.Location = new Point(8, 281);
            pastDaysGroup.Name = "pastDaysGroup";
            pastDaysGroup.Size = new Size(376, 119);
            pastDaysGroup.TabIndex = 3;
            pastDaysGroup.TabStop = false;
            pastDaysGroup.Text = "4. وضعیت معامله در روزهای گذشته:";
            //
            // label4
            //
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(323, 45);
            label4.Name = "label4";
            label4.Size = new Size(40, 33);
            label4.TabIndex = 0;
            label4.Text = "در";
            //
            // label3
            //
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(129, 45);
            label3.Name = "label3";
            label3.Size = new Size(128, 34);
            label3.TabIndex = 1;
            label3.Text = "گذشته معامله";
            //
            // pastDaysTextBox
            //
            pastDaysTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pastDaysTextBox.Location = new Point(263, 44);
            pastDaysTextBox.Name = "pastDaysTextBox";
            pastDaysTextBox.Size = new Size(63, 31);
            pastDaysTextBox.TabIndex = 2;
            //
            // pastDaysStatusComboBox
            //
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.Items.AddRange(new object[] { "داشته", "نداشته" });
            pastDaysStatusComboBox.Location = new Point(28, 42);
            pastDaysStatusComboBox.Name = "pastDaysStatusComboBox";
            pastDaysStatusComboBox.Size = new Size(95, 33);
            pastDaysStatusComboBox.TabIndex = 3;
            //
            // label5_7
            //
            label5_7.Location = new Point(46, 56);
            label5_7.Name = "label5_7";
            label5_7.Size = new Size(100, 23);
            label5_7.TabIndex = 4;
            //
            // filterVolumeClearButton
            //
            filterVolumeClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterVolumeClearButton.Font = new Font("Segoe UI", 8.5F);
            filterVolumeClearButton.Location = new Point(8, 2);
            filterVolumeClearButton.Name = "filterVolumeClearButton";
            filterVolumeClearButton.RightToLeft = RightToLeft.Yes;
            filterVolumeClearButton.Size = new Size(42, 24);
            filterVolumeClearButton.TabIndex = 0;
            filterVolumeClearButton.TabStop = false;
            filterVolumeClearButton.Tag = "volume";
            filterVolumeClearButton.Text = "پاک";
            filterVolumeClearButton.UseVisualStyleBackColor = true;
            filterVolumeClearButton.Click += IndividualFilterClearButton_Click;
            // volumeRatioGroup
            //
            volumeRatioGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeRatioGroup.Controls.Add(label2);
            volumeRatioGroup.Controls.Add(volumeRatioOperatorComboBox);
            volumeRatioGroup.Controls.Add(textBox1);
            volumeRatioGroup.Controls.Add(volumeRatioTextBox);
            volumeRatioGroup.Location = new Point(8, 196);
            volumeRatioGroup.Name = "volumeRatioGroup";
            volumeRatioGroup.Size = new Size(376, 85);
            volumeRatioGroup.TabIndex = 2;
            volumeRatioGroup.TabStop = false;
            volumeRatioGroup.Text = "3. نسبت حجم آخرین روز به میانگین: ";
            //
            // label2
            //
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(226, 32);
            label2.Name = "label2";
            label2.Size = new Size(69, 31);
            label2.TabIndex = 0;
            label2.Text = "روز قبل";
            //
            // volumeRatioOperatorComboBox
            //
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            volumeRatioOperatorComboBox.Location = new Point(104, 30);
            volumeRatioOperatorComboBox.Name = "volumeRatioOperatorComboBox";
            volumeRatioOperatorComboBox.Size = new Size(121, 33);
            volumeRatioOperatorComboBox.TabIndex = 4;
            //
            // textBox1
            //
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(298, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(59, 31);
            textBox1.TabIndex = 2;
            //
            // volumeRatioTextBox
            //
            volumeRatioTextBox.Location = new Point(14, 32);
            volumeRatioTextBox.Name = "volumeRatioTextBox";
            volumeRatioTextBox.Size = new Size(86, 31);
            volumeRatioTextBox.TabIndex = 3;
            //
            // filterNameClearButton
            //
            filterNameClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterNameClearButton.Font = new Font("Segoe UI", 8.5F);
            filterNameClearButton.Location = new Point(8, 2);
            filterNameClearButton.Name = "filterNameClearButton";
            filterNameClearButton.RightToLeft = RightToLeft.Yes;
            filterNameClearButton.Size = new Size(42, 24);
            filterNameClearButton.TabIndex = 0;
            filterNameClearButton.TabStop = false;
            filterNameClearButton.Tag = "name";
            filterNameClearButton.Text = "پاک";
            filterNameClearButton.UseVisualStyleBackColor = true;
            filterNameClearButton.Click += IndividualFilterClearButton_Click;
            // nameFilterGroup
            //
            nameFilterGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameFilterGroup.Controls.Add(nameComboBox);
            nameFilterGroup.Controls.Add(nameTextBox);
            nameFilterGroup.Location = new Point(8, 111);
            nameFilterGroup.Name = "nameFilterGroup";
            nameFilterGroup.Size = new Size(376, 85);
            nameFilterGroup.TabIndex = 1;
            nameFilterGroup.TabStop = false;
            nameFilterGroup.Text = "2. جستجو در نام نماد:";
            //
            // nameComboBox
            //
            nameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            nameComboBox.Items.AddRange(new object[] { "در ابتدا", "در انتها", "در میانه", "در هرجا", "در هیچ جا" });
            nameComboBox.Location = new Point(20, 28);
            nameComboBox.Name = "nameComboBox";
            nameComboBox.Size = new Size(188, 33);
            nameComboBox.TabIndex = 0;
            //
            // nameTextBox
            //
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nameTextBox.Location = new Point(214, 30);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(149, 31);
            nameTextBox.TabIndex = 1;
            //
            // filterTradingClearButton
            //
            filterTradingClearButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            filterTradingClearButton.Font = new Font("Segoe UI", 8.5F);
            filterTradingClearButton.Location = new Point(8, 2);
            filterTradingClearButton.Name = "filterTradingClearButton";
            filterTradingClearButton.RightToLeft = RightToLeft.Yes;
            filterTradingClearButton.Size = new Size(42, 24);
            filterTradingClearButton.TabIndex = 0;
            filterTradingClearButton.TabStop = false;
            filterTradingClearButton.Tag = "trading";
            filterTradingClearButton.Text = "پاک";
            filterTradingClearButton.UseVisualStyleBackColor = true;
            filterTradingClearButton.Click += IndividualFilterClearButton_Click;
            // tradingStatusGroup
            //
            tradingStatusGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tradingStatusGroup.Controls.Add(statusAllRadio);
            tradingStatusGroup.Controls.Add(statusPositiveRadio);
            tradingStatusGroup.Controls.Add(statusNegativeRadio);
            tradingStatusGroup.Location = new Point(8, 8);
            tradingStatusGroup.Name = "tradingStatusGroup";
            tradingStatusGroup.Size = new Size(376, 103);
            tradingStatusGroup.TabIndex = 0;
            tradingStatusGroup.TabStop = false;
            tradingStatusGroup.Text = "1. وضعیت معامله امروز:";
            //
            // statusAllRadio
            //
            statusAllRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statusAllRadio.Location = new Point(226, 49);
            statusAllRadio.Name = "statusAllRadio";
            statusAllRadio.Size = new Size(90, 29);
            statusAllRadio.TabIndex = 0;
            statusAllRadio.Text = "همه";
            //
            // statusPositiveRadio
            //
            statusPositiveRadio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusPositiveRadio.Location = new Point(154, 47);
            statusPositiveRadio.Name = "statusPositiveRadio";
            statusPositiveRadio.Size = new Size(71, 33);
            statusPositiveRadio.TabIndex = 1;
            statusPositiveRadio.Text = "دارد";
            //
            // statusNegativeRadio
            //
            statusNegativeRadio.Location = new Point(63, 48);
            statusNegativeRadio.Name = "statusNegativeRadio";
            statusNegativeRadio.Size = new Size(81, 33);
            statusNegativeRadio.TabIndex = 2;
            statusNegativeRadio.Text = "ندارد";
            //
            // tabPage3
            //
            tabPage3.Controls.Add(identifierMainGroup);
            tabPage3.Controls.Add(identifierButtonsPanel);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(8);
            tabPage3.Size = new Size(392, 895);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "شناسه";
            //
            // marketsTabPage
            //
            marketsTabPage.Controls.Add(marketsMainGroup);
            marketsTabPage.Location = new Point(4, 34);
            marketsTabPage.Name = "marketsTabPage";
            marketsTabPage.Padding = new Padding(8);
            marketsTabPage.Size = new Size(392, 895);
            marketsTabPage.TabIndex = 3;
            marketsTabPage.Text = "بازارها";
            //
            // chartPanel
            //
            chartPanel.BorderStyle = BorderStyle.FixedSingle;
            chartPanel.Controls.Add(chartTabControl);
            chartPanel.Controls.Add(chartToolbarPanel);
            chartPanel.Dock = DockStyle.Fill;
            chartPanel.Location = new Point(0, 0);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(1239, 933);
            chartPanel.TabIndex = 0;
            //
            // chartTabControl
            //
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Location = new Point(0, 51);
            chartTabControl.Name = "chartTabControl";
            chartTabControl.SelectedIndex = 0;
            chartTabControl.Size = new Size(1237, 880);
            chartTabControl.TabIndex = 0;
            //
            // chartTabPage
            //
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Location = new Point(4, 34);
            chartTabPage.Name = "chartTabPage";
            chartTabPage.Padding = new Padding(3);
            chartTabPage.Size = new Size(1229, 842);
            chartTabPage.TabIndex = 0;
            chartTabPage.Text = "نماد";
            //
            // chartInfoPanel
            //
            chartInfoPanel.BackColor = SystemColors.Control;
            chartInfoPanel.Controls.Add(chartInfoLabel);
            chartInfoPanel.Dock = DockStyle.Top;
            chartInfoPanel.Location = new Point(3, 3);
            chartInfoPanel.Name = "chartInfoPanel";
            chartInfoPanel.Size = new Size(1223, 30);
            chartInfoPanel.TabIndex = 0;
            //
            // chartInfoLabel
            //
            chartInfoLabel.Dock = DockStyle.Fill;
            chartInfoLabel.Location = new Point(0, 0);
            chartInfoLabel.Name = "chartInfoLabel";
            chartInfoLabel.Padding = new Padding(8, 0, 0, 0);
            chartInfoLabel.Size = new Size(1223, 30);
            chartInfoLabel.TabIndex = 0;
            chartInfoLabel.Text = "O: —    H: —    L: —    C: —    V: — تاریخ/زمان : —    ";
            chartInfoLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // chartPlaceholderLabel
            //
            chartPlaceholderLabel.Dock = DockStyle.Fill;
            chartPlaceholderLabel.Font = new Font("Segoe UI", 14F);
            chartPlaceholderLabel.Location = new Point(3, 3);
            chartPlaceholderLabel.Name = "chartPlaceholderLabel";
            chartPlaceholderLabel.Size = new Size(1223, 836);
            chartPlaceholderLabel.TabIndex = 1;
            chartPlaceholderLabel.Text = "ناحیه رسم چارت";
            chartPlaceholderLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // chartToolbarPanel
            //
            chartToolbarPanel.BorderStyle = BorderStyle.FixedSingle;
            chartToolbarPanel.Controls.Add(chartTypeComboBox);
            chartToolbarPanel.Controls.Add(gridButton);
            chartToolbarPanel.Controls.Add(crossButton);
            chartToolbarPanel.Controls.Add(zoomInButton);
            chartToolbarPanel.Controls.Add(zoomOutButton);
            chartToolbarPanel.Controls.Add(resetChartButton);
            chartToolbarPanel.Controls.Add(hideChartButton);
            chartToolbarPanel.Controls.Add(hideToolsButton);
            chartToolbarPanel.Controls.Add(printChartButton);
            chartToolbarPanel.Controls.Add(snapshotChartButton);
            chartToolbarPanel.Controls.Add(fullScreenChartButton);
            chartToolbarPanel.Dock = DockStyle.Top;
            chartToolbarPanel.Location = new Point(0, 0);
            chartToolbarPanel.Name = "chartToolbarPanel";
            chartToolbarPanel.Padding = new Padding(6, 5, 6, 5);
            chartToolbarPanel.RightToLeft = RightToLeft.Yes;
            chartToolbarPanel.Size = new Size(1237, 51);
            chartToolbarPanel.TabIndex = 1;
            //
            // chartTypeComboBox
            //
            chartTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            chartTypeComboBox.Items.AddRange(new object[] { "شمعی", "خطی", "میله ای" });
            chartTypeComboBox.Location = new Point(6, 8);
            chartTypeComboBox.Name = "chartTypeComboBox";
            chartTypeComboBox.Size = new Size(92, 33);
            chartTypeComboBox.TabIndex = 0;
            //
            // gridButton
            //
            gridButton.Location = new Point(99, 7);
            gridButton.Name = "gridButton";
            gridButton.Size = new Size(54, 34);
            gridButton.TabIndex = 1;
            gridButton.Text = "گرید";
            //
            // crossButton
            //
            crossButton.Location = new Point(154, 7);
            crossButton.Name = "crossButton";
            crossButton.Size = new Size(58, 34);
            crossButton.TabIndex = 2;
            crossButton.Text = "Cross";
            //
            // zoomInButton
            //
            zoomInButton.Location = new Point(211, 7);
            zoomInButton.Name = "zoomInButton";
            zoomInButton.Size = new Size(54, 34);
            zoomInButton.TabIndex = 3;
            zoomInButton.Text = "+";
            //
            // zoomOutButton
            //
            zoomOutButton.Location = new Point(266, 7);
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(54, 34);
            zoomOutButton.TabIndex = 4;
            zoomOutButton.Text = "-";
            //
            // resetChartButton
            //
            resetChartButton.Location = new Point(321, 7);
            resetChartButton.Name = "resetChartButton";
            resetChartButton.Size = new Size(72, 34);
            resetChartButton.TabIndex = 5;
            resetChartButton.Text = "بازنشانی";
            //
            // hideChartButton
            //
            hideChartButton.Location = new Point(394, 7);
            hideChartButton.Name = "hideChartButton";
            hideChartButton.Size = new Size(66, 34);
            hideChartButton.TabIndex = 6;
            hideChartButton.Text = "پنهان";
            //
            // hideToolsButton
            //
            hideToolsButton.Location = new Point(461, 7);
            hideToolsButton.Name = "hideToolsButton";
            hideToolsButton.Size = new Size(70, 34);
            hideToolsButton.TabIndex = 7;
            hideToolsButton.Text = "ابزارها";
            //
            // printChartButton
            //
            printChartButton.Location = new Point(532, 7);
            printChartButton.Name = "printChartButton";
            printChartButton.Size = new Size(70, 34);
            printChartButton.TabIndex = 8;
            printChartButton.Text = "چاپ";
            //
            // snapshotChartButton
            //
            snapshotChartButton.Location = new Point(603, 7);
            snapshotChartButton.Name = "snapshotChartButton";
            snapshotChartButton.Size = new Size(86, 34);
            snapshotChartButton.TabIndex = 9;
            snapshotChartButton.Text = "تصویر";
            //
            // fullScreenChartButton
            //
            fullScreenChartButton.Location = new Point(690, 7);
            fullScreenChartButton.Name = "fullScreenChartButton";
            fullScreenChartButton.Size = new Size(95, 34);
            fullScreenChartButton.TabIndex = 10;
            fullScreenChartButton.Text = "تمام صفحه";
            //
            // groupBox1
            //
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            //
            // refreshButtonPortfolio
            //
            refreshButtonPortfolio.Location = new Point(0, 0);
            refreshButtonPortfolio.Name = "refreshButtonPortfolio";
            refreshButtonPortfolio.Size = new Size(75, 23);
            refreshButtonPortfolio.TabIndex = 0;
            //
            // identifierMainGroup
            //
            identifierMainGroup.AutoSize = true;
            identifierMainGroup.Controls.Add(identifierLayout);
            identifierMainGroup.Dock = DockStyle.Top;
            identifierMainGroup.Location = new Point(8, 60);
            identifierMainGroup.Name = "identifierMainGroup";
            identifierMainGroup.Padding = new Padding(10);
            identifierMainGroup.RightToLeft = RightToLeft.Yes;
            identifierMainGroup.Size = new Size(360, 620);
            identifierMainGroup.TabIndex = 0;
            identifierMainGroup.TabStop = false;
            identifierMainGroup.Text = "اطلاعات شناسه";
            //
            // identifierLayout
            //
            identifierLayout.AutoSize = true;
            identifierLayout.ColumnCount = 2;
            identifierLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            identifierLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            identifierLayout.Dock = DockStyle.Top;
            identifierLayout.Location = new Point(13, 30);
            identifierLayout.Name = "identifierLayout";
            identifierLayout.Padding = new Padding(6);
            identifierLayout.RightToLeft = RightToLeft.Yes;
            identifierLayout.RowCount = 14;
            identifierLayout.Size = new Size(334, 600);
            identifierLayout.TabIndex = 0;
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierSymbolLabel.Dock = DockStyle.Fill;
            identifierSymbolLabel.Font = new Font("Segoe UI", 10F);
            identifierSymbolLabel.Margin = new Padding(4);
            identifierSymbolLabel.Name = "identifierSymbolLabel";
            identifierSymbolLabel.RightToLeft = RightToLeft.Yes;
            identifierSymbolLabel.Size = new Size(155, 32);
            identifierSymbolLabel.TabIndex = 0;
            identifierSymbolLabel.Text = "کد ۱۲ رقمی نماد";
            identifierSymbolLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierSymbolLabel, 0, 0);
            identifierSymbolTextBox.Dock = DockStyle.Fill;
            identifierSymbolTextBox.Margin = new Padding(4);
            identifierSymbolTextBox.Name = "identifierSymbolTextBox";
            identifierSymbolTextBox.RightToLeft = RightToLeft.Yes;
            identifierSymbolTextBox.Size = new Size(159, 31);
            identifierSymbolTextBox.TabIndex = 0;
            identifierLayout.Controls.Add(identifierSymbolTextBox, 1, 0);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierNameLabel.Dock = DockStyle.Fill;
            identifierNameLabel.Font = new Font("Segoe UI", 10F);
            identifierNameLabel.Margin = new Padding(4);
            identifierNameLabel.Name = "identifierNameLabel";
            identifierNameLabel.RightToLeft = RightToLeft.Yes;
            identifierNameLabel.Size = new Size(155, 32);
            identifierNameLabel.TabIndex = 1;
            identifierNameLabel.Text = "کد ۵ رقمی نماد";
            identifierNameLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierNameLabel, 0, 1);
            identifierNameTextBox.Dock = DockStyle.Fill;
            identifierNameTextBox.Margin = new Padding(4);
            identifierNameTextBox.Name = "identifierNameTextBox";
            identifierNameTextBox.RightToLeft = RightToLeft.Yes;
            identifierNameTextBox.Size = new Size(159, 31);
            identifierNameTextBox.TabIndex = 1;
            identifierLayout.Controls.Add(identifierNameTextBox, 1, 1);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierTsetmcLabel.Dock = DockStyle.Fill;
            identifierTsetmcLabel.Font = new Font("Segoe UI", 10F);
            identifierTsetmcLabel.Margin = new Padding(4);
            identifierTsetmcLabel.Name = "identifierTsetmcLabel";
            identifierTsetmcLabel.RightToLeft = RightToLeft.Yes;
            identifierTsetmcLabel.Size = new Size(155, 32);
            identifierTsetmcLabel.TabIndex = 2;
            identifierTsetmcLabel.Text = "نام لاتین شرکت";
            identifierTsetmcLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierTsetmcLabel, 0, 2);
            identifierTsetmcTextBox.Dock = DockStyle.Fill;
            identifierTsetmcTextBox.Margin = new Padding(4);
            identifierTsetmcTextBox.Name = "identifierTsetmcTextBox";
            identifierTsetmcTextBox.RightToLeft = RightToLeft.Yes;
            identifierTsetmcTextBox.Size = new Size(159, 31);
            identifierTsetmcTextBox.TabIndex = 2;
            identifierLayout.Controls.Add(identifierTsetmcTextBox, 1, 2);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierGroupLabel.Dock = DockStyle.Fill;
            identifierGroupLabel.Font = new Font("Segoe UI", 10F);
            identifierGroupLabel.Margin = new Padding(4);
            identifierGroupLabel.Name = "identifierGroupLabel";
            identifierGroupLabel.RightToLeft = RightToLeft.Yes;
            identifierGroupLabel.Size = new Size(155, 32);
            identifierGroupLabel.TabIndex = 3;
            identifierGroupLabel.Text = "کد ۴ رقمی شرکت";
            identifierGroupLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierGroupLabel, 0, 3);
            identifierGroupTextBox.Dock = DockStyle.Fill;
            identifierGroupTextBox.Margin = new Padding(4);
            identifierGroupTextBox.Name = "identifierGroupTextBox";
            identifierGroupTextBox.RightToLeft = RightToLeft.Yes;
            identifierGroupTextBox.Size = new Size(159, 31);
            identifierGroupTextBox.TabIndex = 3;
            identifierLayout.Controls.Add(identifierGroupTextBox, 1, 3);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierDescriptionLabel.Dock = DockStyle.Fill;
            identifierDescriptionLabel.Font = new Font("Segoe UI", 10F);
            identifierDescriptionLabel.Margin = new Padding(4);
            identifierDescriptionLabel.Name = "identifierDescriptionLabel";
            identifierDescriptionLabel.RightToLeft = RightToLeft.Yes;
            identifierDescriptionLabel.Size = new Size(155, 32);
            identifierDescriptionLabel.TabIndex = 4;
            identifierDescriptionLabel.Text = "نام شرکت";
            identifierDescriptionLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierDescriptionLabel, 0, 4);
            identifierDescriptionTextBox.Dock = DockStyle.Fill;
            identifierDescriptionTextBox.Margin = new Padding(4);
            identifierDescriptionTextBox.Name = "identifierDescriptionTextBox";
            identifierDescriptionTextBox.RightToLeft = RightToLeft.Yes;
            identifierDescriptionTextBox.Size = new Size(159, 31);
            identifierDescriptionTextBox.TabIndex = 4;
            identifierLayout.Controls.Add(identifierDescriptionTextBox, 1, 4);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierPersianSymbolLabel.Dock = DockStyle.Fill;
            identifierPersianSymbolLabel.Font = new Font("Segoe UI", 10F);
            identifierPersianSymbolLabel.Margin = new Padding(4);
            identifierPersianSymbolLabel.Name = "identifierPersianSymbolLabel";
            identifierPersianSymbolLabel.RightToLeft = RightToLeft.Yes;
            identifierPersianSymbolLabel.Size = new Size(155, 32);
            identifierPersianSymbolLabel.TabIndex = 5;
            identifierPersianSymbolLabel.Text = "نماد فارسی";
            identifierPersianSymbolLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierPersianSymbolLabel, 0, 5);
            identifierPersianSymbolTextBox.Dock = DockStyle.Fill;
            identifierPersianSymbolTextBox.Margin = new Padding(4);
            identifierPersianSymbolTextBox.Name = "identifierPersianSymbolTextBox";
            identifierPersianSymbolTextBox.RightToLeft = RightToLeft.Yes;
            identifierPersianSymbolTextBox.Size = new Size(159, 31);
            identifierPersianSymbolTextBox.TabIndex = 5;
            identifierLayout.Controls.Add(identifierPersianSymbolTextBox, 1, 5);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierPersian30SymbolLabel.Dock = DockStyle.Fill;
            identifierPersian30SymbolLabel.Font = new Font("Segoe UI", 10F);
            identifierPersian30SymbolLabel.Margin = new Padding(4);
            identifierPersian30SymbolLabel.Name = "identifierPersian30SymbolLabel";
            identifierPersian30SymbolLabel.RightToLeft = RightToLeft.Yes;
            identifierPersian30SymbolLabel.Size = new Size(155, 32);
            identifierPersian30SymbolLabel.TabIndex = 6;
            identifierPersian30SymbolLabel.Text = "نماد ۳۰ رقمی فارسی";
            identifierPersian30SymbolLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierPersian30SymbolLabel, 0, 6);
            identifierPersian30SymbolTextBox.Dock = DockStyle.Fill;
            identifierPersian30SymbolTextBox.Margin = new Padding(4);
            identifierPersian30SymbolTextBox.Name = "identifierPersian30SymbolTextBox";
            identifierPersian30SymbolTextBox.RightToLeft = RightToLeft.Yes;
            identifierPersian30SymbolTextBox.Size = new Size(159, 31);
            identifierPersian30SymbolTextBox.TabIndex = 6;
            identifierLayout.Controls.Add(identifierPersian30SymbolTextBox, 1, 6);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierCompanyCode12Label.Dock = DockStyle.Fill;
            identifierCompanyCode12Label.Font = new Font("Segoe UI", 10F);
            identifierCompanyCode12Label.Margin = new Padding(4);
            identifierCompanyCode12Label.Name = "identifierCompanyCode12Label";
            identifierCompanyCode12Label.RightToLeft = RightToLeft.Yes;
            identifierCompanyCode12Label.Size = new Size(155, 32);
            identifierCompanyCode12Label.TabIndex = 7;
            identifierCompanyCode12Label.Text = "کد ۱۲ رقمی شرکت";
            identifierCompanyCode12Label.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierCompanyCode12Label, 0, 7);
            identifierCompanyCode12TextBox.Dock = DockStyle.Fill;
            identifierCompanyCode12TextBox.Margin = new Padding(4);
            identifierCompanyCode12TextBox.Name = "identifierCompanyCode12TextBox";
            identifierCompanyCode12TextBox.RightToLeft = RightToLeft.Yes;
            identifierCompanyCode12TextBox.Size = new Size(159, 31);
            identifierCompanyCode12TextBox.TabIndex = 7;
            identifierLayout.Controls.Add(identifierCompanyCode12TextBox, 1, 7);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierMarketLabel.Dock = DockStyle.Fill;
            identifierMarketLabel.Font = new Font("Segoe UI", 10F);
            identifierMarketLabel.Margin = new Padding(4);
            identifierMarketLabel.Name = "identifierMarketLabel";
            identifierMarketLabel.RightToLeft = RightToLeft.Yes;
            identifierMarketLabel.Size = new Size(155, 32);
            identifierMarketLabel.TabIndex = 8;
            identifierMarketLabel.Text = "بازار";
            identifierMarketLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierMarketLabel, 0, 8);
            identifierMarketComboBox.Dock = DockStyle.Fill;
            identifierMarketComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            identifierMarketComboBox.Margin = new Padding(4);
            identifierMarketComboBox.Name = "identifierMarketComboBox";
            identifierMarketComboBox.RightToLeft = RightToLeft.Yes;
            identifierMarketComboBox.Size = new Size(159, 31);
            identifierMarketComboBox.TabIndex = 8;
            identifierLayout.Controls.Add(identifierMarketComboBox, 1, 8);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierBoardCodeLabel.Dock = DockStyle.Fill;
            identifierBoardCodeLabel.Font = new Font("Segoe UI", 10F);
            identifierBoardCodeLabel.Margin = new Padding(4);
            identifierBoardCodeLabel.Name = "identifierBoardCodeLabel";
            identifierBoardCodeLabel.RightToLeft = RightToLeft.Yes;
            identifierBoardCodeLabel.Size = new Size(155, 32);
            identifierBoardCodeLabel.TabIndex = 9;
            identifierBoardCodeLabel.Text = "کد تابلو";
            identifierBoardCodeLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierBoardCodeLabel, 0, 9);
            identifierBoardCodeTextBox.Dock = DockStyle.Fill;
            identifierBoardCodeTextBox.Margin = new Padding(4);
            identifierBoardCodeTextBox.Name = "identifierBoardCodeTextBox";
            identifierBoardCodeTextBox.RightToLeft = RightToLeft.Yes;
            identifierBoardCodeTextBox.Size = new Size(159, 31);
            identifierBoardCodeTextBox.TabIndex = 9;
            identifierLayout.Controls.Add(identifierBoardCodeTextBox, 1, 9);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierIndustryCodeLabel.Dock = DockStyle.Fill;
            identifierIndustryCodeLabel.Font = new Font("Segoe UI", 10F);
            identifierIndustryCodeLabel.Margin = new Padding(4);
            identifierIndustryCodeLabel.Name = "identifierIndustryCodeLabel";
            identifierIndustryCodeLabel.RightToLeft = RightToLeft.Yes;
            identifierIndustryCodeLabel.Size = new Size(155, 32);
            identifierIndustryCodeLabel.TabIndex = 10;
            identifierIndustryCodeLabel.Text = "کد گروه صنعت";
            identifierIndustryCodeLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierIndustryCodeLabel, 0, 10);
            identifierIndustryCodeTextBox.Dock = DockStyle.Fill;
            identifierIndustryCodeTextBox.Margin = new Padding(4);
            identifierIndustryCodeTextBox.Name = "identifierIndustryCodeTextBox";
            identifierIndustryCodeTextBox.RightToLeft = RightToLeft.Yes;
            identifierIndustryCodeTextBox.Size = new Size(159, 31);
            identifierIndustryCodeTextBox.TabIndex = 10;
            identifierLayout.Controls.Add(identifierIndustryCodeTextBox, 1, 10);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierIndustryLabel.Dock = DockStyle.Fill;
            identifierIndustryLabel.Font = new Font("Segoe UI", 10F);
            identifierIndustryLabel.Margin = new Padding(4);
            identifierIndustryLabel.Name = "identifierIndustryLabel";
            identifierIndustryLabel.RightToLeft = RightToLeft.Yes;
            identifierIndustryLabel.Size = new Size(155, 32);
            identifierIndustryLabel.TabIndex = 11;
            identifierIndustryLabel.Text = "گروه صنعت";
            identifierIndustryLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierIndustryLabel, 0, 11);
            identifierIndustryTextBox.Dock = DockStyle.Fill;
            identifierIndustryTextBox.Margin = new Padding(4);
            identifierIndustryTextBox.Name = "identifierIndustryTextBox";
            identifierIndustryTextBox.RightToLeft = RightToLeft.Yes;
            identifierIndustryTextBox.Size = new Size(159, 31);
            identifierIndustryTextBox.TabIndex = 11;
            identifierLayout.Controls.Add(identifierIndustryTextBox, 1, 11);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierSubIndustryCodeLabel.Dock = DockStyle.Fill;
            identifierSubIndustryCodeLabel.Font = new Font("Segoe UI", 10F);
            identifierSubIndustryCodeLabel.Margin = new Padding(4);
            identifierSubIndustryCodeLabel.Name = "identifierSubIndustryCodeLabel";
            identifierSubIndustryCodeLabel.RightToLeft = RightToLeft.Yes;
            identifierSubIndustryCodeLabel.Size = new Size(155, 32);
            identifierSubIndustryCodeLabel.TabIndex = 12;
            identifierSubIndustryCodeLabel.Text = "کد زیر گروه صنعت";
            identifierSubIndustryCodeLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierSubIndustryCodeLabel, 0, 12);
            identifierSubIndustryCodeTextBox.Dock = DockStyle.Fill;
            identifierSubIndustryCodeTextBox.Margin = new Padding(4);
            identifierSubIndustryCodeTextBox.Name = "identifierSubIndustryCodeTextBox";
            identifierSubIndustryCodeTextBox.RightToLeft = RightToLeft.Yes;
            identifierSubIndustryCodeTextBox.Size = new Size(159, 31);
            identifierSubIndustryCodeTextBox.TabIndex = 12;
            identifierLayout.Controls.Add(identifierSubIndustryCodeTextBox, 1, 12);
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            identifierSubIndustryLabel.Dock = DockStyle.Fill;
            identifierSubIndustryLabel.Font = new Font("Segoe UI", 10F);
            identifierSubIndustryLabel.Margin = new Padding(4);
            identifierSubIndustryLabel.Name = "identifierSubIndustryLabel";
            identifierSubIndustryLabel.RightToLeft = RightToLeft.Yes;
            identifierSubIndustryLabel.Size = new Size(155, 32);
            identifierSubIndustryLabel.TabIndex = 13;
            identifierSubIndustryLabel.Text = "زیر گروه صنعت";
            identifierSubIndustryLabel.TextAlign = ContentAlignment.MiddleRight;
            identifierLayout.Controls.Add(identifierSubIndustryLabel, 0, 13);
            identifierSubIndustryTextBox.Dock = DockStyle.Fill;
            identifierSubIndustryTextBox.Margin = new Padding(4);
            identifierSubIndustryTextBox.Name = "identifierSubIndustryTextBox";
            identifierSubIndustryTextBox.RightToLeft = RightToLeft.Yes;
            identifierSubIndustryTextBox.Size = new Size(159, 31);
            identifierSubIndustryTextBox.TabIndex = 13;
            identifierLayout.Controls.Add(identifierSubIndustryTextBox, 1, 13);
            //
            // identifierButtonsPanel
            //
            identifierButtonsPanel.Dock = DockStyle.Top;
            identifierButtonsPanel.FlowDirection = FlowDirection.RightToLeft;
            identifierButtonsPanel.Height = 52;
            identifierButtonsPanel.Location = new Point(8, 8);
            identifierButtonsPanel.Margin = new Padding(0);
            identifierButtonsPanel.Name = "identifierButtonsPanel";
            identifierButtonsPanel.Padding = new Padding(4);
            identifierButtonsPanel.RightToLeft = RightToLeft.Yes;
            identifierButtonsPanel.Size = new Size(376, 52);
            identifierButtonsPanel.TabIndex = 0;
            identifierButtonsPanel.WrapContents = false;
            identifierButtonsPanel.Controls.Add(identifierImportExcelButton);
            identifierButtonsPanel.Controls.Add(identifierDeleteAllButton);
            identifierButtonsPanel.Controls.Add(identifierDeleteButton);
            identifierButtonsPanel.Controls.Add(identifierSaveButton);
            identifierButtonsPanel.Controls.Add(identifierNewButton);
            // identifierNewButton
            identifierNewButton.Margin = new Padding(3);
            identifierNewButton.Name = "identifierNewButton";
            identifierNewButton.Size = new Size(62, 36);
            identifierNewButton.TabIndex = 0;
            identifierNewButton.Text = "جدید";
            identifierNewButton.UseVisualStyleBackColor = true;
            // identifierSaveButton
            identifierSaveButton.Margin = new Padding(3);
            identifierSaveButton.Name = "identifierSaveButton";
            identifierSaveButton.Size = new Size(70, 36);
            identifierSaveButton.TabIndex = 1;
            identifierSaveButton.Text = "ذخیره";
            identifierSaveButton.UseVisualStyleBackColor = true;
            // identifierDeleteButton
            identifierDeleteButton.Margin = new Padding(3);
            identifierDeleteButton.Name = "identifierDeleteButton";
            identifierDeleteButton.Size = new Size(62, 36);
            identifierDeleteButton.TabIndex = 2;
            identifierDeleteButton.Text = "حذف";
            identifierDeleteButton.UseVisualStyleBackColor = true;
            // identifierDeleteAllButton
            identifierDeleteAllButton.Margin = new Padding(3);
            identifierDeleteAllButton.Name = "identifierDeleteAllButton";
            identifierDeleteAllButton.Size = new Size(80, 36);
            identifierDeleteAllButton.TabIndex = 3;
            identifierDeleteAllButton.Text = "حذف همه";
            identifierDeleteAllButton.UseVisualStyleBackColor = true;
            // identifierImportExcelButton
            identifierImportExcelButton.Margin = new Padding(3);
            identifierImportExcelButton.Name = "identifierImportExcelButton";
            identifierImportExcelButton.Size = new Size(105, 36);
            identifierImportExcelButton.TabIndex = 4;
            identifierImportExcelButton.Text = "ورود از اکسل";
            identifierImportExcelButton.UseVisualStyleBackColor = true;
            // marketTypeLabel
            //
            marketTypeLabel.Location = new Point(0, 0);
            marketTypeLabel.Name = "marketTypeLabel";
            marketTypeLabel.Size = new Size(100, 23);
            marketTypeLabel.TabIndex = 0;
            //
            // marketTypeTextBox
            //
            marketTypeTextBox.Location = new Point(0, 0);
            marketTypeTextBox.Name = "marketTypeTextBox";
            marketTypeTextBox.Size = new Size(100, 31);
            marketTypeTextBox.TabIndex = 0;
            //
            // marketNameLabel
            //
            marketNameLabel.Location = new Point(0, 0);
            marketNameLabel.Name = "marketNameLabel";
            marketNameLabel.Size = new Size(100, 23);
            marketNameLabel.TabIndex = 0;
            //
            // marketNameTextBox
            //
            marketNameTextBox.Location = new Point(0, 0);
            marketNameTextBox.Name = "marketNameTextBox";
            marketNameTextBox.Size = new Size(100, 31);
            marketNameTextBox.TabIndex = 0;
            //
            // marketBoardLabel
            //
            marketBoardLabel.Location = new Point(0, 0);
            marketBoardLabel.Name = "marketBoardLabel";
            marketBoardLabel.Size = new Size(100, 23);
            marketBoardLabel.TabIndex = 0;
            //
            // marketBoardTextBox
            //
            marketBoardTextBox.Location = new Point(0, 0);
            marketBoardTextBox.Name = "marketBoardTextBox";
            marketBoardTextBox.Size = new Size(100, 31);
            marketBoardTextBox.TabIndex = 0;
            //
            // marketIndustryGroupLabel
            //
            marketIndustryGroupLabel.Location = new Point(0, 0);
            marketIndustryGroupLabel.Name = "marketIndustryGroupLabel";
            marketIndustryGroupLabel.Size = new Size(100, 23);
            marketIndustryGroupLabel.TabIndex = 0;
            //
            // marketIndustryGroupTextBox
            //
            marketIndustryGroupTextBox.Location = new Point(0, 0);
            marketIndustryGroupTextBox.Name = "marketIndustryGroupTextBox";
            marketIndustryGroupTextBox.Size = new Size(100, 31);
            marketIndustryGroupTextBox.TabIndex = 0;
            //
            // marketSubIndustryGroupLabel
            //
            marketSubIndustryGroupLabel.Location = new Point(0, 0);
            marketSubIndustryGroupLabel.Name = "marketSubIndustryGroupLabel";
            marketSubIndustryGroupLabel.Size = new Size(100, 23);
            marketSubIndustryGroupLabel.TabIndex = 0;
            //
            // marketSubIndustryGroupTextBox
            //
            marketSubIndustryGroupTextBox.Location = new Point(0, 0);
            marketSubIndustryGroupTextBox.Name = "marketSubIndustryGroupTextBox";
            marketSubIndustryGroupTextBox.Size = new Size(100, 31);
            marketSubIndustryGroupTextBox.TabIndex = 0;
            //
            tradingStatusGroup.Controls.Add(filterTradingClearButton);
            nameFilterGroup.Controls.Add(filterNameClearButton);
            volumeRatioGroup.Controls.Add(filterVolumeClearButton);
            pastDaysGroup.Controls.Add(filterPastDaysClearButton);
            comparisonGroup7.Controls.Add(filterComparison7ClearButton);
            comparisonGroup8.Controls.Add(filterComparison8ClearButton);
            groupBox3.Controls.Add(filterComparison9ClearButton);
            ohlcChangeFilterGroup.Controls.Add(filterOhlcChangeClearButton);
            // MainForm
            //
            ClientSize = new Size(1643, 966);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "Trade.It";
            WindowState = FormWindowState.Maximized;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            controlTabControl.ResumeLayout(false);
            stocksTabPage.ResumeLayout(false);
            stocksTabPage.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).EndInit();
            tabPage2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ohlcChangeFilterGroup.ResumeLayout(false);
            ohlcChangeFilterGroup.PerformLayout();
            comparisonGroup8.ResumeLayout(false);
            comparisonGroup8.PerformLayout();
            comparisonGroup7.ResumeLayout(false);
            comparisonGroup7.PerformLayout();
            pastDaysGroup.ResumeLayout(false);
            pastDaysGroup.PerformLayout();
            volumeRatioGroup.ResumeLayout(false);
            volumeRatioGroup.PerformLayout();
            nameFilterGroup.ResumeLayout(false);
            nameFilterGroup.PerformLayout();
            tradingStatusGroup.ResumeLayout(false);
            chartPanel.ResumeLayout(false);
            chartTabControl.ResumeLayout(false);
            chartTabPage.ResumeLayout(false);
            chartInfoPanel.ResumeLayout(false);
            chartToolbarPanel.ResumeLayout(false);
            identifierMainGroup.ResumeLayout(false);
            identifierButtonsPanel.ResumeLayout(false);
            marketsLayout.ResumeLayout(false);
            marketsMainGroup.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private GroupBox groupBox2;
        private DataGridViewTextBoxColumn rowColumn;
        private DataGridViewTextBoxColumn symbolColumn;
        private DataGridViewTextBoxColumn lastTradeColumn;
        private DataGridViewCheckBoxColumn selectColumn;
        private GroupBox comparisonGroup7;
        private Label label1;
        private TextBox comparisonSecondTextBox2;
        private Label label5;
        private TextBox comparisonFirstTextBox2;
        private Label label6;
        private ComboBox comparisonFirstComboBox1;
        private ComboBox comparisonOperatorComboBox2;
        private ComboBox comparisonSecondComboBox1;
        private Label label5_7;
        private Button clearFiltersButton;
        private GroupBox groupBox3;
        private Label label7;
        private TextBox comparisonSecondTextBox3;
        private Label label8;
        private TextBox comparisonFirstTextBox3;
        private Label label9;
        private ComboBox comparisonFirstComboBox3;
        private ComboBox comparisonOperatorComboBox3;
        private ComboBox comparisonSecondComboBox3;
    }
}
