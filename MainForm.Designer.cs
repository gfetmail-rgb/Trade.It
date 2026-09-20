namespace Trade.It
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem portfolioDefinitionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolioManagementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TabControl controlTabControl;
        private System.Windows.Forms.TabPage stocksTabPage;
        private System.Windows.Forms.TabPage tabPage2;
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
        private System.Windows.Forms.Button indicatorPanelButton;
        private System.Windows.Forms.Button testModeButton;
        private System.Windows.Forms.Button testStepBackButton;
        private System.Windows.Forms.Button testStepForwardButton;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.Button resetChartButton;
        private System.Windows.Forms.Button hideChartButton;
        private System.Windows.Forms.Button hideToolsButton;
        private System.Windows.Forms.Button printChartButton;
        private System.Windows.Forms.Button snapshotChartButton;
        private System.Windows.Forms.Button fullScreenChartButton;
        private System.Windows.Forms.Button saveAnalysisButton;
        private System.Windows.Forms.Button drawTrendLineButton;
        private System.Windows.Forms.Button drawTrendChannelButton;
        private System.Windows.Forms.Button drawHorizontalDoubleButton;
        private System.Windows.Forms.Button drawVerticalDoubleButton;
        private System.Windows.Forms.Button drawHorizontalRayButton;
        private System.Windows.Forms.Button drawTrendLineArrowButton;
        private System.Windows.Forms.Button drawRectangleButton;
        private System.Windows.Forms.Button drawFibonacciButton;
        private System.Windows.Forms.Button drawTextButton;
        private System.Windows.Forms.Button drawPitchforkButton;
        private System.Windows.Forms.Button drawFibonacciExtensionButton;
        private System.Windows.Forms.Button drawMeasureButton;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage chartTabPage;
        private System.Windows.Forms.Panel chartInfoPanel;
        private System.Windows.Forms.Label chartInfoLabel;
        private System.Windows.Forms.Label chartPlaceholderLabel;
        private System.Windows.Forms.Button refreshButtonPortfolio;
        private System.Windows.Forms.Label filterCountLabel;
        private System.Windows.Forms.Label marketCountLabel;
        private System.Windows.Forms.Label stocksGridCountLabel;

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
            symbolDefinitionMenuItem = new ToolStripMenuItem();
            mainSplitContainer = new SplitContainer();
            controlTabControl = new TabControl();
            stocksTabPage = new TabPage();
            stocksGridCountLabel = new Label();
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
            filterComparison9ClearButton = new Button();
            clearFiltersButton = new Button();
            filterCountLabel = new Label();
            ohlcChangeFilterGroup = new GroupBox();
            ohlcChangeFieldLabel = new Label();
            ohlcChangeFieldComboBox = new ComboBox();
            ohlcChangeDaysLabel = new Label();
            ohlcChangeDaysTextBox = new TextBox();
            ohlcChangePercentLabel = new Label();
            ohlcChangePercentTextBox = new TextBox();
            ohlcChangeDirectionComboBox = new ComboBox();
            filterOhlcChangeClearButton = new Button();
            comparisonGroup8 = new GroupBox();
            label8_8 = new Label();
            comparisonSecondTextBox1 = new TextBox();
            label6_8 = new Label();
            comparisonFirstTextBox1 = new TextBox();
            label5_8 = new Label();
            comparisonFirstComboBox2 = new ComboBox();
            comparisonOperatorComboBox1 = new ComboBox();
            comparisonSecondComboBox2 = new ComboBox();
            filterComparison8ClearButton = new Button();
            comparisonGroup7 = new GroupBox();
            label1 = new Label();
            comparisonSecondTextBox2 = new TextBox();
            label5 = new Label();
            comparisonFirstTextBox2 = new TextBox();
            label6 = new Label();
            comparisonFirstComboBox1 = new ComboBox();
            comparisonOperatorComboBox2 = new ComboBox();
            comparisonSecondComboBox1 = new ComboBox();
            filterComparison7ClearButton = new Button();
            pastDaysGroup = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            pastDaysTextBox = new TextBox();
            pastDaysStatusComboBox = new ComboBox();
            label5_7 = new Label();
            filterPastDaysClearButton = new Button();
            volumeRatioGroup = new GroupBox();
            label2 = new Label();
            volumeRatioOperatorComboBox = new ComboBox();
            textBox1 = new TextBox();
            volumeRatioTextBox = new TextBox();
            filterVolumeClearButton = new Button();
            nameFilterGroup = new GroupBox();
            nameComboBox = new ComboBox();
            nameTextBox = new TextBox();
            filterNameClearButton = new Button();
            tradingStatusGroup = new GroupBox();
            statusAllRadio = new RadioButton();
            statusPositiveRadio = new RadioButton();
            statusNegativeRadio = new RadioButton();
            filterTradingClearButton = new Button();
            marketsTabPage = new TabPage();
            marketCountLabel = new Label();
            marketClearButton = new Button();
            marketApplyButton = new Button();
            marketIndustryGroupCheckedListBox = new CheckedListBox();
            marketFundTypeCheckedListBox = new CheckedListBox();
            marketAssetCheckedListBox = new CheckedListBox();
            marketBoardCheckedListBox = new CheckedListBox();
            marketTypeCheckedListBox = new CheckedListBox();
            marketExchangeCheckedListBox = new CheckedListBox();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            chartPanel = new Panel();
            chartTabControl = new TabControl();
            chartTabPage = new TabPage();
            chartInfoPanel = new Panel();
            chartInfoLabel = new Label();
            chartPlaceholderLabel = new Label();
            chartToolbarPanel = new Panel();
            label20 = new Label();
            label19 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            closeAllChartsToolbarButton = new Button();
            saveAnalysisButton = new Button();
            chartTypeComboBox = new ComboBox();
            gridButton = new Button();
            crossButton = new Button();
            indicatorPanelButton = new Button();
            testModeButton = new Button();
            testStepBackButton = new Button();
            testStepForwardButton = new Button();
            zoomInButton = new Button();
            zoomOutButton = new Button();
            resetChartButton = new Button();
            hideChartButton = new Button();
            hideToolsButton = new Button();
            printChartButton = new Button();
            snapshotChartButton = new Button();
            fullScreenChartButton = new Button();
            drawTrendLineButton = new Button();
            drawTrendChannelButton = new Button();
            drawHorizontalDoubleButton = new Button();
            drawVerticalDoubleButton = new Button();
            drawHorizontalRayButton = new Button();
            drawTrendLineArrowButton = new Button();
            drawRectangleButton = new Button();
            drawFibonacciButton = new Button();
            drawTextButton = new Button();
            drawPitchforkButton = new Button();
            drawFibonacciExtensionButton = new Button();
            drawMeasureButton = new Button();
            marketTypeComboBox = new ComboBox();
            marketBoardComboBox = new ComboBox();
            marketAssetComboBox = new ComboBox();
            marketFundTypeComboBox = new ComboBox();
            marketIndustryGroupComboBox = new ComboBox();
            marketExchangeComboBox = new ComboBox();
            groupBox1 = new GroupBox();
            refreshButtonPortfolio = new Button();
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
            marketsTabPage.SuspendLayout();
            chartPanel.SuspendLayout();
            chartTabControl.SuspendLayout();
            chartTabPage.SuspendLayout();
            chartInfoPanel.SuspendLayout();
            chartToolbarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem, symbolDefinitionMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.RightToLeft = RightToLeft.Yes;
            mainMenuStrip.Size = new Size(1630, 33);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.TabStop = true;
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
            // symbolDefinitionMenuItem
            // 
            symbolDefinitionMenuItem.Name = "symbolDefinitionMenuItem";
            symbolDefinitionMenuItem.Size = new Size(132, 29);
            symbolDefinitionMenuItem.Text = "تعریف نمادها";
            symbolDefinitionMenuItem.Click += symbolDefinitionMenuItem_Click;
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
            mainSplitContainer.Size = new Size(1630, 933);
            mainSplitContainer.SplitterDistance = 395;
            mainSplitContainer.TabIndex = 1;
            // 
            // controlTabControl
            // 
            controlTabControl.Controls.Add(stocksTabPage);
            controlTabControl.Controls.Add(tabPage2);
            controlTabControl.Controls.Add(marketsTabPage);
            controlTabControl.Dock = DockStyle.Fill;
            controlTabControl.Location = new Point(0, 0);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.RightToLeft = RightToLeft.Yes;
            controlTabControl.RightToLeftLayout = true;
            controlTabControl.SelectedIndex = 0;
            controlTabControl.Size = new Size(395, 933);
            controlTabControl.TabIndex = 0;
            // 
            // stocksTabPage
            // 
            stocksTabPage.Controls.Add(stocksGridCountLabel);
            stocksTabPage.Controls.Add(groupBox2);
            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(portfolioComboBox);
            stocksTabPage.Controls.Add(portfolioLabel);
            stocksTabPage.Location = new Point(4, 34);
            stocksTabPage.Name = "stocksTabPage";
            stocksTabPage.Padding = new Padding(8);
            stocksTabPage.RightToLeft = RightToLeft.Yes;
            stocksTabPage.Size = new Size(387, 895);
            stocksTabPage.TabIndex = 0;
            stocksTabPage.Text = "سبد";
            // 
            // stocksGridCountLabel
            // 
            stocksGridCountLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksGridCountLabel.AutoSize = true;
            stocksGridCountLabel.Location = new Point(148, 727);
            stocksGridCountLabel.Name = "stocksGridCountLabel";
            stocksGridCountLabel.RightToLeft = RightToLeft.Yes;
            stocksGridCountLabel.Size = new Size(77, 25);
            stocksGridCountLabel.TabIndex = 4;
            stocksGridCountLabel.Text = "نمادها: ۰";
            stocksGridCountLabel.TextAlign = ContentAlignment.MiddleCenter;
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
            groupBox2.Location = new Point(8, 748);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(371, 131);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            // 
            // navigationButton
            // 
            navigationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            navigationButton.Location = new Point(269, 16);
            navigationButton.Name = "navigationButton";
            navigationButton.Size = new Size(87, 34);
            navigationButton.TabIndex = 0;
            navigationButton.Text = "پیمایش";
            // 
            // selectAllCheckBox
            // 
            selectAllCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(260, 51);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(70, 29);
            selectAllCheckBox.TabIndex = 0;
            selectAllCheckBox.Text = "همه";
            // 
            // selectNoneCheckBox
            // 
            selectNoneCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new Point(53, 51);
            selectNoneCheckBox.Name = "selectNoneCheckBox";
            selectNoneCheckBox.Size = new Size(101, 29);
            selectNoneCheckBox.TabIndex = 1;
            selectNoneCheckBox.Text = "هیچکدام";
            // 
            // speedLabel
            // 
            speedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(112, 19);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(64, 25);
            speedLabel.TabIndex = 2;
            speedLabel.Text = "سرعت:";
            // 
            // refreshButton
            // 
            refreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            refreshButton.Location = new Point(114, 86);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(154, 34);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "تازه";
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.Location = new Point(283, 86);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(73, 34);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "حذف";
            // 
            // newPortfolioButton
            // 
            newPortfolioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            newPortfolioButton.Location = new Point(24, 86);
            newPortfolioButton.Name = "newPortfolioButton";
            newPortfolioButton.Size = new Size(69, 34);
            newPortfolioButton.TabIndex = 1;
            newPortfolioButton.Text = "جدید";
            // 
            // navigationSpeedTextBox
            // 
            navigationSpeedTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navigationSpeedTextBox.Location = new Point(24, 18);
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
            stocksDataGridView.AllowUserToResizeRows = false;
            stocksDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksDataGridView.ColumnHeadersHeight = 34;
            stocksDataGridView.Columns.AddRange(new DataGridViewColumn[] { rowColumn, symbolColumn, lastTradeColumn, selectColumn });
            stocksDataGridView.Location = new Point(11, 47);
            stocksDataGridView.Name = "stocksDataGridView";
            stocksDataGridView.RowHeadersVisible = false;
            stocksDataGridView.RowHeadersWidth = 62;
            stocksDataGridView.Size = new Size(368, 669);
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
            selectColumn.Width = 68;
            // 
            // portfolioComboBox
            // 
            portfolioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new Point(11, 6);
            portfolioComboBox.Name = "portfolioComboBox";
            portfolioComboBox.Size = new Size(299, 33);
            portfolioComboBox.TabIndex = 2;
            // 
            // portfolioLabel
            // 
            portfolioLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new Point(313, 6);
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
            tabPage2.Size = new Size(387, 895);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "فیلترها";
            // 
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
            groupBox3.Controls.Add(filterComparison9ClearButton);
            groupBox3.Location = new Point(8, 631);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(374, 111);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "9. مقایسه قیمت:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(73, 77);
            label7.Name = "label7";
            label7.Size = new Size(101, 25);
            label7.TabIndex = 0;
            label7.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox3
            // 
            comparisonSecondTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox3.Location = new Point(172, 74);
            comparisonSecondTextBox3.Name = "comparisonSecondTextBox3";
            comparisonSecondTextBox3.Size = new Size(65, 31);
            comparisonSecondTextBox3.TabIndex = 1;
            comparisonSecondTextBox3.Text = "5";
            comparisonSecondTextBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(76, 41);
            label8.Name = "label8";
            label8.Size = new Size(101, 25);
            label8.TabIndex = 3;
            label8.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox3
            // 
            comparisonFirstTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox3.Location = new Point(172, 37);
            comparisonFirstTextBox3.Name = "comparisonFirstTextBox3";
            comparisonFirstTextBox3.Size = new Size(65, 31);
            comparisonFirstTextBox3.TabIndex = 4;
            comparisonFirstTextBox3.Text = "5";
            comparisonFirstTextBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(310, 38);
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
            comparisonFirstComboBox3.Location = new Point(237, 34);
            comparisonFirstComboBox3.Name = "comparisonFirstComboBox3";
            comparisonFirstComboBox3.Size = new Size(75, 33);
            comparisonFirstComboBox3.TabIndex = 6;
            // 
            // comparisonOperatorComboBox3
            // 
            comparisonOperatorComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox3.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox3.Location = new Point(8, 40);
            comparisonOperatorComboBox3.Name = "comparisonOperatorComboBox3";
            comparisonOperatorComboBox3.Size = new Size(65, 33);
            comparisonOperatorComboBox3.TabIndex = 7;
            // 
            // comparisonSecondComboBox3
            // 
            comparisonSecondComboBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox3.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox3.Location = new Point(237, 73);
            comparisonSecondComboBox3.Name = "comparisonSecondComboBox3";
            comparisonSecondComboBox3.Size = new Size(75, 33);
            comparisonSecondComboBox3.TabIndex = 8;
            // 
            // filterComparison9ClearButton
            // 
            filterComparison9ClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterComparison9ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison9ClearButton.Location = new Point(15, 0);
            filterComparison9ClearButton.Name = "filterComparison9ClearButton";
            filterComparison9ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison9ClearButton.Size = new Size(51, 32);
            filterComparison9ClearButton.TabIndex = 0;
            filterComparison9ClearButton.TabStop = false;
            filterComparison9ClearButton.Tag = "comparison9";
            filterComparison9ClearButton.Text = "پاک";
            filterComparison9ClearButton.UseVisualStyleBackColor = false;
            filterComparison9ClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // clearFiltersButton
            // 
            clearFiltersButton.Location = new Point(8, 858);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(100, 36);
            clearFiltersButton.TabIndex = 9;
            clearFiltersButton.Text = "پاک کردن";
            clearFiltersButton.UseVisualStyleBackColor = true;
            // 
            // filterCountLabel
            // 
            filterCountLabel.Font = new Font("Segoe UI", 9F);
            filterCountLabel.Location = new Point(112, 858);
            filterCountLabel.Name = "filterCountLabel";
            filterCountLabel.RightToLeft = RightToLeft.Yes;
            filterCountLabel.Size = new Size(237, 36);
            filterCountLabel.TabIndex = 11;
            filterCountLabel.Text = "کل: ۰    پیدا شده: ۰";
            filterCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
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
            ohlcChangeFilterGroup.Controls.Add(filterOhlcChangeClearButton);
            ohlcChangeFilterGroup.Location = new Point(8, 750);
            ohlcChangeFilterGroup.Name = "ohlcChangeFilterGroup";
            ohlcChangeFilterGroup.Size = new Size(374, 105);
            ohlcChangeFilterGroup.TabIndex = 8;
            ohlcChangeFilterGroup.TabStop = false;
            ohlcChangeFilterGroup.Text = "10. درصد رشد";
            // 
            // ohlcChangeFieldLabel
            // 
            ohlcChangeFieldLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ohlcChangeFieldLabel.AutoSize = true;
            ohlcChangeFieldLabel.Location = new Point(314, 31);
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
            ohlcChangeFieldComboBox.Location = new Point(231, 27);
            ohlcChangeFieldComboBox.Name = "ohlcChangeFieldComboBox";
            ohlcChangeFieldComboBox.Size = new Size(79, 33);
            ohlcChangeFieldComboBox.TabIndex = 1;
            // 
            // ohlcChangeDaysLabel
            // 
            ohlcChangeDaysLabel.AutoSize = true;
            ohlcChangeDaysLabel.Location = new Point(3, 36);
            ohlcChangeDaysLabel.Name = "ohlcChangeDaysLabel";
            ohlcChangeDaysLabel.Size = new Size(80, 25);
            ohlcChangeDaysLabel.TabIndex = 2;
            ohlcChangeDaysLabel.Text = "کندل قبل";
            // 
            // ohlcChangeDaysTextBox
            // 
            ohlcChangeDaysTextBox.Location = new Point(130, 30);
            ohlcChangeDaysTextBox.Name = "ohlcChangeDaysTextBox";
            ohlcChangeDaysTextBox.Size = new Size(57, 31);
            ohlcChangeDaysTextBox.TabIndex = 3;
            ohlcChangeDaysTextBox.Text = "5";
            ohlcChangeDaysTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ohlcChangePercentLabel
            // 
            ohlcChangePercentLabel.AutoSize = true;
            ohlcChangePercentLabel.Location = new Point(109, 73);
            ohlcChangePercentLabel.Name = "ohlcChangePercentLabel";
            ohlcChangePercentLabel.Size = new Size(27, 25);
            ohlcChangePercentLabel.TabIndex = 4;
            ohlcChangePercentLabel.Text = "%";
            // 
            // ohlcChangePercentTextBox
            // 
            ohlcChangePercentTextBox.Location = new Point(148, 70);
            ohlcChangePercentTextBox.Name = "ohlcChangePercentTextBox";
            ohlcChangePercentTextBox.Size = new Size(76, 31);
            ohlcChangePercentTextBox.TabIndex = 5;
            ohlcChangePercentTextBox.Text = "5";
            ohlcChangePercentTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ohlcChangeDirectionComboBox
            // 
            ohlcChangeDirectionComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ohlcChangeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ohlcChangeDirectionComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            ohlcChangeDirectionComboBox.Location = new Point(263, 68);
            ohlcChangeDirectionComboBox.Name = "ohlcChangeDirectionComboBox";
            ohlcChangeDirectionComboBox.Size = new Size(98, 33);
            ohlcChangeDirectionComboBox.TabIndex = 6;
            // 
            // filterOhlcChangeClearButton
            // 
            filterOhlcChangeClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterOhlcChangeClearButton.Font = new Font("Segoe UI", 8.5F);
            filterOhlcChangeClearButton.Location = new Point(15, 1);
            filterOhlcChangeClearButton.Name = "filterOhlcChangeClearButton";
            filterOhlcChangeClearButton.RightToLeft = RightToLeft.Yes;
            filterOhlcChangeClearButton.Size = new Size(53, 32);
            filterOhlcChangeClearButton.TabIndex = 0;
            filterOhlcChangeClearButton.TabStop = false;
            filterOhlcChangeClearButton.Tag = "ohlcChange";
            filterOhlcChangeClearButton.Text = "پاک";
            filterOhlcChangeClearButton.UseVisualStyleBackColor = false;
            filterOhlcChangeClearButton.Click += IndividualFilterClearButton_Click;
            // 
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
            comparisonGroup8.Controls.Add(filterComparison8ClearButton);
            comparisonGroup8.Location = new Point(8, 519);
            comparisonGroup8.Name = "comparisonGroup8";
            comparisonGroup8.Size = new Size(374, 119);
            comparisonGroup8.TabIndex = 7;
            comparisonGroup8.TabStop = false;
            comparisonGroup8.Text = "8. مقایسه قیمت:";
            // 
            // label8_8
            // 
            label8_8.AutoSize = true;
            label8_8.Location = new Point(67, 74);
            label8_8.Name = "label8_8";
            label8_8.Size = new Size(101, 25);
            label8_8.TabIndex = 0;
            label8_8.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox1
            // 
            comparisonSecondTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox1.Location = new Point(172, 70);
            comparisonSecondTextBox1.Name = "comparisonSecondTextBox1";
            comparisonSecondTextBox1.Size = new Size(65, 31);
            comparisonSecondTextBox1.TabIndex = 1;
            comparisonSecondTextBox1.Text = "5";
            comparisonSecondTextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label6_8
            // 
            label6_8.AutoSize = true;
            label6_8.Location = new Point(70, 38);
            label6_8.Name = "label6_8";
            label6_8.Size = new Size(101, 25);
            label6_8.TabIndex = 3;
            label6_8.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox1
            // 
            comparisonFirstTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox1.Location = new Point(172, 33);
            comparisonFirstTextBox1.Name = "comparisonFirstTextBox1";
            comparisonFirstTextBox1.Size = new Size(65, 31);
            comparisonFirstTextBox1.TabIndex = 4;
            comparisonFirstTextBox1.Text = "5";
            comparisonFirstTextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label5_8
            // 
            label5_8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5_8.AutoSize = true;
            label5_8.Location = new Point(311, 31);
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
            comparisonFirstComboBox2.Location = new Point(237, 30);
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
            comparisonOperatorComboBox1.Size = new Size(65, 33);
            comparisonOperatorComboBox1.TabIndex = 7;
            // 
            // comparisonSecondComboBox2
            // 
            comparisonSecondComboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox2.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox2.Location = new Point(237, 69);
            comparisonSecondComboBox2.Name = "comparisonSecondComboBox2";
            comparisonSecondComboBox2.Size = new Size(75, 33);
            comparisonSecondComboBox2.TabIndex = 8;
            // 
            // filterComparison8ClearButton
            // 
            filterComparison8ClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterComparison8ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison8ClearButton.Location = new Point(15, -11);
            filterComparison8ClearButton.Name = "filterComparison8ClearButton";
            filterComparison8ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison8ClearButton.Size = new Size(51, 37);
            filterComparison8ClearButton.TabIndex = 0;
            filterComparison8ClearButton.TabStop = false;
            filterComparison8ClearButton.Tag = "comparison8";
            filterComparison8ClearButton.Text = "پاک";
            filterComparison8ClearButton.UseVisualStyleBackColor = false;
            filterComparison8ClearButton.Click += IndividualFilterClearButton_Click;
            // 
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
            comparisonGroup7.Controls.Add(filterComparison7ClearButton);
            comparisonGroup7.Location = new Point(8, 400);
            comparisonGroup7.Name = "comparisonGroup7";
            comparisonGroup7.Size = new Size(374, 119);
            comparisonGroup7.TabIndex = 6;
            comparisonGroup7.TabStop = false;
            comparisonGroup7.Text = "7. مقایسه قیمت:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 82);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 9;
            label1.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox2
            // 
            comparisonSecondTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox2.Location = new Point(172, 79);
            comparisonSecondTextBox2.Name = "comparisonSecondTextBox2";
            comparisonSecondTextBox2.Size = new Size(68, 31);
            comparisonSecondTextBox2.TabIndex = 10;
            comparisonSecondTextBox2.Text = "5";
            comparisonSecondTextBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(74, 47);
            label5.Name = "label5";
            label5.Size = new Size(101, 25);
            label5.TabIndex = 11;
            label5.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox2
            // 
            comparisonFirstTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox2.Location = new Point(172, 42);
            comparisonFirstTextBox2.Name = "comparisonFirstTextBox2";
            comparisonFirstTextBox2.Size = new Size(68, 31);
            comparisonFirstTextBox2.TabIndex = 12;
            comparisonFirstTextBox2.Text = "5";
            comparisonFirstTextBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(312, 42);
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
            comparisonFirstComboBox1.Location = new Point(240, 39);
            comparisonFirstComboBox1.Name = "comparisonFirstComboBox1";
            comparisonFirstComboBox1.Size = new Size(75, 33);
            comparisonFirstComboBox1.TabIndex = 14;
            // 
            // comparisonOperatorComboBox2
            // 
            comparisonOperatorComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox2.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox2.Location = new Point(11, 45);
            comparisonOperatorComboBox2.Name = "comparisonOperatorComboBox2";
            comparisonOperatorComboBox2.Size = new Size(62, 33);
            comparisonOperatorComboBox2.TabIndex = 15;
            // 
            // comparisonSecondComboBox1
            // 
            comparisonSecondComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox1.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox1.Location = new Point(240, 78);
            comparisonSecondComboBox1.Name = "comparisonSecondComboBox1";
            comparisonSecondComboBox1.Size = new Size(75, 33);
            comparisonSecondComboBox1.TabIndex = 16;
            // 
            // filterComparison7ClearButton
            // 
            filterComparison7ClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterComparison7ClearButton.Font = new Font("Segoe UI", 8.5F);
            filterComparison7ClearButton.Location = new Point(15, 7);
            filterComparison7ClearButton.Name = "filterComparison7ClearButton";
            filterComparison7ClearButton.RightToLeft = RightToLeft.Yes;
            filterComparison7ClearButton.Size = new Size(55, 32);
            filterComparison7ClearButton.TabIndex = 0;
            filterComparison7ClearButton.TabStop = false;
            filterComparison7ClearButton.Tag = "comparison7";
            filterComparison7ClearButton.Text = "پاک";
            filterComparison7ClearButton.UseVisualStyleBackColor = false;
            filterComparison7ClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // pastDaysGroup
            // 
            pastDaysGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pastDaysGroup.Controls.Add(label4);
            pastDaysGroup.Controls.Add(label3);
            pastDaysGroup.Controls.Add(pastDaysTextBox);
            pastDaysGroup.Controls.Add(pastDaysStatusComboBox);
            pastDaysGroup.Controls.Add(label5_7);
            pastDaysGroup.Controls.Add(filterPastDaysClearButton);
            pastDaysGroup.Location = new Point(8, 281);
            pastDaysGroup.Name = "pastDaysGroup";
            pastDaysGroup.Size = new Size(374, 86);
            pastDaysGroup.TabIndex = 3;
            pastDaysGroup.TabStop = false;
            pastDaysGroup.Text = "4. وضعیت معامله در روزهای گذشته:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(321, 45);
            label4.Name = "label4";
            label4.Size = new Size(40, 33);
            label4.TabIndex = 0;
            label4.Text = "در";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(163, 45);
            label3.Name = "label3";
            label3.Size = new Size(94, 34);
            label3.TabIndex = 1;
            label3.Text = "گذشته معامله";
            // 
            // pastDaysTextBox
            // 
            pastDaysTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pastDaysTextBox.Location = new Point(265, 44);
            pastDaysTextBox.Name = "pastDaysTextBox";
            pastDaysTextBox.Size = new Size(63, 31);
            pastDaysTextBox.TabIndex = 2;
            // 
            // pastDaysStatusComboBox
            // 
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.Items.AddRange(new object[] { "داشته", "نداشته" });
            pastDaysStatusComboBox.Location = new Point(67, 44);
            pastDaysStatusComboBox.Name = "pastDaysStatusComboBox";
            pastDaysStatusComboBox.Size = new Size(95, 33);
            pastDaysStatusComboBox.TabIndex = 3;
            // 
            // label5_7
            // 
            label5_7.Location = new Point(172, 79);
            label5_7.Name = "label5_7";
            label5_7.Size = new Size(100, 23);
            label5_7.TabIndex = 4;
            // 
            // filterPastDaysClearButton
            // 
            filterPastDaysClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterPastDaysClearButton.Font = new Font("Segoe UI", 8.5F);
            filterPastDaysClearButton.Location = new Point(15, 17);
            filterPastDaysClearButton.Name = "filterPastDaysClearButton";
            filterPastDaysClearButton.RightToLeft = RightToLeft.Yes;
            filterPastDaysClearButton.Size = new Size(52, 32);
            filterPastDaysClearButton.TabIndex = 0;
            filterPastDaysClearButton.TabStop = false;
            filterPastDaysClearButton.Tag = "pastDays";
            filterPastDaysClearButton.Text = "پاک";
            filterPastDaysClearButton.UseVisualStyleBackColor = false;
            filterPastDaysClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // volumeRatioGroup
            // 
            volumeRatioGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeRatioGroup.Controls.Add(label2);
            volumeRatioGroup.Controls.Add(volumeRatioOperatorComboBox);
            volumeRatioGroup.Controls.Add(textBox1);
            volumeRatioGroup.Controls.Add(volumeRatioTextBox);
            volumeRatioGroup.Controls.Add(filterVolumeClearButton);
            volumeRatioGroup.Location = new Point(8, 196);
            volumeRatioGroup.Name = "volumeRatioGroup";
            volumeRatioGroup.Size = new Size(374, 85);
            volumeRatioGroup.TabIndex = 2;
            volumeRatioGroup.TabStop = false;
            volumeRatioGroup.Text = "3. نسبت حجم آخرین روز به میانگین: ";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(224, 43);
            label2.Name = "label2";
            label2.Size = new Size(69, 31);
            label2.TabIndex = 0;
            label2.Text = "روز قبل";
            // 
            // volumeRatioOperatorComboBox
            // 
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            volumeRatioOperatorComboBox.Location = new Point(104, 41);
            volumeRatioOperatorComboBox.Name = "volumeRatioOperatorComboBox";
            volumeRatioOperatorComboBox.Size = new Size(121, 33);
            volumeRatioOperatorComboBox.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(296, 45);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(59, 31);
            textBox1.TabIndex = 2;
            // 
            // volumeRatioTextBox
            // 
            volumeRatioTextBox.Location = new Point(14, 42);
            volumeRatioTextBox.Name = "volumeRatioTextBox";
            volumeRatioTextBox.Size = new Size(86, 31);
            volumeRatioTextBox.TabIndex = 3;
            // 
            // filterVolumeClearButton
            // 
            filterVolumeClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterVolumeClearButton.Font = new Font("Segoe UI", 8.5F);
            filterVolumeClearButton.Location = new Point(15, 2);
            filterVolumeClearButton.Name = "filterVolumeClearButton";
            filterVolumeClearButton.RightToLeft = RightToLeft.Yes;
            filterVolumeClearButton.Size = new Size(53, 34);
            filterVolumeClearButton.TabIndex = 0;
            filterVolumeClearButton.TabStop = false;
            filterVolumeClearButton.Tag = "volume";
            filterVolumeClearButton.Text = "پاک";
            filterVolumeClearButton.UseVisualStyleBackColor = false;
            filterVolumeClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // nameFilterGroup
            // 
            nameFilterGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameFilterGroup.Controls.Add(nameComboBox);
            nameFilterGroup.Controls.Add(nameTextBox);
            nameFilterGroup.Controls.Add(filterNameClearButton);
            nameFilterGroup.Location = new Point(8, 111);
            nameFilterGroup.Name = "nameFilterGroup";
            nameFilterGroup.Size = new Size(374, 85);
            nameFilterGroup.TabIndex = 1;
            nameFilterGroup.TabStop = false;
            nameFilterGroup.Text = "2. جستجو در نام نماد:";
            // 
            // nameComboBox
            // 
            nameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            nameComboBox.Items.AddRange(new object[] { "در ابتدا", "در انتها", "در میانه", "در هرجا", "در هیچ جا" });
            nameComboBox.Location = new Point(20, 38);
            nameComboBox.Name = "nameComboBox";
            nameComboBox.Size = new Size(167, 33);
            nameComboBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nameTextBox.Location = new Point(224, 40);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(137, 31);
            nameTextBox.TabIndex = 1;
            // 
            // filterNameClearButton
            // 
            filterNameClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterNameClearButton.Font = new Font("Segoe UI", 8.5F);
            filterNameClearButton.Location = new Point(15, 6);
            filterNameClearButton.Name = "filterNameClearButton";
            filterNameClearButton.RightToLeft = RightToLeft.Yes;
            filterNameClearButton.Size = new Size(53, 30);
            filterNameClearButton.TabIndex = 0;
            filterNameClearButton.TabStop = false;
            filterNameClearButton.Tag = "name";
            filterNameClearButton.Text = "پاک";
            filterNameClearButton.UseVisualStyleBackColor = false;
            filterNameClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // tradingStatusGroup
            // 
            tradingStatusGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tradingStatusGroup.Controls.Add(statusAllRadio);
            tradingStatusGroup.Controls.Add(statusPositiveRadio);
            tradingStatusGroup.Controls.Add(statusNegativeRadio);
            tradingStatusGroup.Controls.Add(filterTradingClearButton);
            tradingStatusGroup.Location = new Point(8, 8);
            tradingStatusGroup.Name = "tradingStatusGroup";
            tradingStatusGroup.Size = new Size(374, 103);
            tradingStatusGroup.TabIndex = 0;
            tradingStatusGroup.TabStop = false;
            tradingStatusGroup.Text = "1. وضعیت معامله امروز:";
            // 
            // statusAllRadio
            // 
            statusAllRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statusAllRadio.Location = new Point(263, 49);
            statusAllRadio.Name = "statusAllRadio";
            statusAllRadio.Size = new Size(90, 29);
            statusAllRadio.TabIndex = 0;
            statusAllRadio.Text = "همه";
            // 
            // statusPositiveRadio
            // 
            statusPositiveRadio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusPositiveRadio.Location = new Point(148, 47);
            statusPositiveRadio.Name = "statusPositiveRadio";
            statusPositiveRadio.Size = new Size(109, 33);
            statusPositiveRadio.TabIndex = 1;
            statusPositiveRadio.Text = "دارد";
            // 
            // statusNegativeRadio
            // 
            statusNegativeRadio.Location = new Point(55, 47);
            statusNegativeRadio.Name = "statusNegativeRadio";
            statusNegativeRadio.Size = new Size(81, 33);
            statusNegativeRadio.TabIndex = 2;
            statusNegativeRadio.Text = "ندارد";
            // 
            // filterTradingClearButton
            // 
            filterTradingClearButton.BackColor = SystemColors.GradientInactiveCaption;
            filterTradingClearButton.Font = new Font("Segoe UI", 8.5F);
            filterTradingClearButton.Location = new Point(15, 9);
            filterTradingClearButton.Name = "filterTradingClearButton";
            filterTradingClearButton.RightToLeft = RightToLeft.Yes;
            filterTradingClearButton.Size = new Size(53, 31);
            filterTradingClearButton.TabIndex = 0;
            filterTradingClearButton.TabStop = false;
            filterTradingClearButton.Tag = "trading";
            filterTradingClearButton.Text = "پاک";
            filterTradingClearButton.UseVisualStyleBackColor = false;
            filterTradingClearButton.Click += IndividualFilterClearButton_Click;
            // 
            // marketsTabPage
            // 
            marketsTabPage.Controls.Add(marketCountLabel);
            marketsTabPage.Controls.Add(marketClearButton);
            marketsTabPage.Controls.Add(marketApplyButton);
            marketsTabPage.Controls.Add(marketIndustryGroupCheckedListBox);
            marketsTabPage.Controls.Add(marketFundTypeCheckedListBox);
            marketsTabPage.Controls.Add(marketAssetCheckedListBox);
            marketsTabPage.Controls.Add(marketBoardCheckedListBox);
            marketsTabPage.Controls.Add(marketTypeCheckedListBox);
            marketsTabPage.Controls.Add(marketExchangeCheckedListBox);
            marketsTabPage.Controls.Add(label18);
            marketsTabPage.Controls.Add(label17);
            marketsTabPage.Controls.Add(label16);
            marketsTabPage.Controls.Add(label15);
            marketsTabPage.Controls.Add(label14);
            marketsTabPage.Controls.Add(label13);
            marketsTabPage.Location = new Point(4, 34);
            marketsTabPage.Name = "marketsTabPage";
            marketsTabPage.Padding = new Padding(8);
            marketsTabPage.Size = new Size(387, 895);
            marketsTabPage.TabIndex = 3;
            marketsTabPage.Text = "بازارها";
            // 
            // marketCountLabel
            // 
            marketCountLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            marketCountLabel.Font = new Font("Segoe UI", 9F);
            marketCountLabel.Location = new Point(16, 843);
            marketCountLabel.Name = "marketCountLabel";
            marketCountLabel.RightToLeft = RightToLeft.Yes;
            marketCountLabel.Size = new Size(351, 36);
            marketCountLabel.TabIndex = 20;
            marketCountLabel.Text = "کل: ۰    پیدا شده: ۰";
            marketCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // marketClearButton
            // 
            marketClearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            marketClearButton.Location = new Point(83, 800);
            marketClearButton.Name = "marketClearButton";
            marketClearButton.Size = new Size(85, 35);
            marketClearButton.TabIndex = 13;
            marketClearButton.Text = "حذف";
            marketClearButton.UseVisualStyleBackColor = true;
            // 
            // marketApplyButton
            // 
            marketApplyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            marketApplyButton.Location = new Point(229, 800);
            marketApplyButton.Name = "marketApplyButton";
            marketApplyButton.Size = new Size(85, 35);
            marketApplyButton.TabIndex = 12;
            marketApplyButton.Text = "تایید";
            marketApplyButton.UseVisualStyleBackColor = true;
            // 
            // marketIndustryGroupCheckedListBox
            // 
            marketIndustryGroupCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketIndustryGroupCheckedListBox.CheckOnClick = true;
            marketIndustryGroupCheckedListBox.FormattingEnabled = true;
            marketIndustryGroupCheckedListBox.HorizontalScrollbar = true;
            marketIndustryGroupCheckedListBox.Location = new Point(6, 656);
            marketIndustryGroupCheckedListBox.Name = "marketIndustryGroupCheckedListBox";
            marketIndustryGroupCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketIndustryGroupCheckedListBox.Size = new Size(270, 116);
            marketIndustryGroupCheckedListBox.TabIndex = 11;
            // 
            // marketFundTypeCheckedListBox
            // 
            marketFundTypeCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketFundTypeCheckedListBox.CheckOnClick = true;
            marketFundTypeCheckedListBox.FormattingEnabled = true;
            marketFundTypeCheckedListBox.HorizontalScrollbar = true;
            marketFundTypeCheckedListBox.Location = new Point(6, 532);
            marketFundTypeCheckedListBox.Name = "marketFundTypeCheckedListBox";
            marketFundTypeCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketFundTypeCheckedListBox.Size = new Size(270, 116);
            marketFundTypeCheckedListBox.TabIndex = 10;
            // 
            // marketAssetCheckedListBox
            // 
            marketAssetCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketAssetCheckedListBox.CheckOnClick = true;
            marketAssetCheckedListBox.FormattingEnabled = true;
            marketAssetCheckedListBox.HorizontalScrollbar = true;
            marketAssetCheckedListBox.Location = new Point(6, 409);
            marketAssetCheckedListBox.Name = "marketAssetCheckedListBox";
            marketAssetCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketAssetCheckedListBox.Size = new Size(270, 116);
            marketAssetCheckedListBox.TabIndex = 9;
            // 
            // marketBoardCheckedListBox
            // 
            marketBoardCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketBoardCheckedListBox.CheckOnClick = true;
            marketBoardCheckedListBox.FormattingEnabled = true;
            marketBoardCheckedListBox.HorizontalScrollbar = true;
            marketBoardCheckedListBox.Location = new Point(9, 258);
            marketBoardCheckedListBox.Name = "marketBoardCheckedListBox";
            marketBoardCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketBoardCheckedListBox.Size = new Size(270, 144);
            marketBoardCheckedListBox.TabIndex = 8;
            // 
            // marketTypeCheckedListBox
            // 
            marketTypeCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketTypeCheckedListBox.CheckOnClick = true;
            marketTypeCheckedListBox.FormattingEnabled = true;
            marketTypeCheckedListBox.HorizontalScrollbar = true;
            marketTypeCheckedListBox.Location = new Point(9, 134);
            marketTypeCheckedListBox.Name = "marketTypeCheckedListBox";
            marketTypeCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketTypeCheckedListBox.Size = new Size(270, 116);
            marketTypeCheckedListBox.TabIndex = 7;
            // 
            // marketExchangeCheckedListBox
            // 
            marketExchangeCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketExchangeCheckedListBox.CheckOnClick = true;
            marketExchangeCheckedListBox.FormattingEnabled = true;
            marketExchangeCheckedListBox.HorizontalScrollbar = true;
            marketExchangeCheckedListBox.Location = new Point(9, 11);
            marketExchangeCheckedListBox.Name = "marketExchangeCheckedListBox";
            marketExchangeCheckedListBox.RightToLeft = RightToLeft.Yes;
            marketExchangeCheckedListBox.Size = new Size(270, 116);
            marketExchangeCheckedListBox.TabIndex = 6;
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label18.AutoSize = true;
            label18.Location = new Point(276, 694);
            label18.Name = "label18";
            label18.Size = new Size(106, 25);
            label18.TabIndex = 5;
            label18.Text = "گروه صنعت:";
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label17.AutoSize = true;
            label17.Location = new Point(284, 182);
            label17.Name = "label17";
            label17.Size = new Size(77, 25);
            label17.TabIndex = 4;
            label17.Text = "نوع بازار:";
            label17.Click += label17_Click;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.Location = new Point(281, 455);
            label16.Name = "label16";
            label16.Size = new Size(93, 25);
            label16.TabIndex = 3;
            label16.Text = "نوع دارایی:";
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.Location = new Point(285, 331);
            label15.Name = "label15";
            label15.Size = new Size(79, 25);
            label15.TabIndex = 2;
            label15.Text = "نوع تابلو:";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Location = new Point(278, 581);
            label14.Name = "label14";
            label14.Size = new Size(103, 25);
            label14.TabIndex = 1;
            label14.Text = "نوع صندوق:";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Location = new Point(285, 59);
            label13.Name = "label13";
            label13.Size = new Size(89, 25);
            label13.TabIndex = 0;
            label13.Text = "نوع بورس:";
            // 
            // chartPanel
            // 
            chartPanel.BorderStyle = BorderStyle.FixedSingle;
            chartPanel.Controls.Add(chartTabControl);
            chartPanel.Controls.Add(chartToolbarPanel);
            chartPanel.Dock = DockStyle.Fill;
            chartPanel.Location = new Point(0, 0);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(1231, 933);
            chartPanel.TabIndex = 0;
            // 
            // chartTabControl
            // 
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Location = new Point(0, 88);
            chartTabControl.Name = "chartTabControl";
            chartTabControl.SelectedIndex = 0;
            chartTabControl.Size = new Size(1229, 843);
            chartTabControl.TabIndex = 0;
            // 
            // chartTabPage
            // 
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Location = new Point(4, 34);
            chartTabPage.Name = "chartTabPage";
            chartTabPage.Padding = new Padding(3);
            chartTabPage.Size = new Size(1221, 805);
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
            chartInfoPanel.Size = new Size(1215, 30);
            chartInfoPanel.TabIndex = 0;
            // 
            // chartInfoLabel
            // 
            chartInfoLabel.Dock = DockStyle.Fill;
            chartInfoLabel.Location = new Point(0, 0);
            chartInfoLabel.Name = "chartInfoLabel";
            chartInfoLabel.Padding = new Padding(8, 0, 0, 0);
            chartInfoLabel.Size = new Size(1215, 30);
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
            chartPlaceholderLabel.Size = new Size(1215, 799);
            chartPlaceholderLabel.TabIndex = 1;
            chartPlaceholderLabel.Text = "ناحیه رسم چارت";
            chartPlaceholderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chartToolbarPanel
            // 
            chartToolbarPanel.BorderStyle = BorderStyle.FixedSingle;
            chartToolbarPanel.Controls.Add(label20);
            chartToolbarPanel.Controls.Add(label19);
            chartToolbarPanel.Controls.Add(label12);
            chartToolbarPanel.Controls.Add(label11);
            chartToolbarPanel.Controls.Add(label10);
            chartToolbarPanel.Controls.Add(closeAllChartsToolbarButton);
            chartToolbarPanel.Controls.Add(saveAnalysisButton);
            chartToolbarPanel.Controls.Add(chartTypeComboBox);
            chartToolbarPanel.Controls.Add(gridButton);
            chartToolbarPanel.Controls.Add(crossButton);
            chartToolbarPanel.Controls.Add(indicatorPanelButton);
            chartToolbarPanel.Controls.Add(testModeButton);
            chartToolbarPanel.Controls.Add(testStepBackButton);
            chartToolbarPanel.Controls.Add(testStepForwardButton);
            chartToolbarPanel.Controls.Add(zoomInButton);
            chartToolbarPanel.Controls.Add(zoomOutButton);
            chartToolbarPanel.Controls.Add(resetChartButton);
            chartToolbarPanel.Controls.Add(hideChartButton);
            chartToolbarPanel.Controls.Add(hideToolsButton);
            chartToolbarPanel.Controls.Add(printChartButton);
            chartToolbarPanel.Controls.Add(snapshotChartButton);
            chartToolbarPanel.Controls.Add(fullScreenChartButton);
            chartToolbarPanel.Controls.Add(drawTrendLineButton);
            chartToolbarPanel.Controls.Add(drawTrendChannelButton);
            chartToolbarPanel.Controls.Add(drawHorizontalDoubleButton);
            chartToolbarPanel.Controls.Add(drawVerticalDoubleButton);
            chartToolbarPanel.Controls.Add(drawHorizontalRayButton);
            chartToolbarPanel.Controls.Add(drawTrendLineArrowButton);
            chartToolbarPanel.Controls.Add(drawRectangleButton);
            chartToolbarPanel.Controls.Add(drawFibonacciButton);
            chartToolbarPanel.Controls.Add(drawTextButton);
            chartToolbarPanel.Controls.Add(drawPitchforkButton);
            chartToolbarPanel.Controls.Add(drawFibonacciExtensionButton);
            chartToolbarPanel.Controls.Add(drawMeasureButton);
            chartToolbarPanel.Dock = DockStyle.Top;
            chartToolbarPanel.Location = new Point(0, 0);
            chartToolbarPanel.Name = "chartToolbarPanel";
            chartToolbarPanel.Padding = new Padding(6, 5, 6, 5);
            chartToolbarPanel.RightToLeft = RightToLeft.Yes;
            chartToolbarPanel.Size = new Size(1229, 88);
            chartToolbarPanel.TabIndex = 1;
            // 
            // label20
            // 
            label20.BorderStyle = BorderStyle.FixedSingle;
            label20.Location = new Point(1113, 46);
            label20.Name = "label20";
            label20.Size = new Size(2, 30);
            label20.TabIndex = 26;
            label20.Text = "label20";
            // 
            // label19
            // 
            label19.BorderStyle = BorderStyle.FixedSingle;
            label19.Location = new Point(967, 46);
            label19.Name = "label19";
            label19.Size = new Size(2, 30);
            label19.TabIndex = 25;
            label19.Text = "label19";
            // 
            // label12
            // 
            label12.BorderStyle = BorderStyle.FixedSingle;
            label12.Location = new Point(434, 10);
            label12.Name = "label12";
            label12.Size = new Size(2, 30);
            label12.TabIndex = 24;
            label12.Text = "label12";
            // 
            // label11
            // 
            label11.BorderStyle = BorderStyle.FixedSingle;
            label11.Location = new Point(703, 10);
            label11.Name = "label11";
            label11.Size = new Size(2, 30);
            label11.TabIndex = 23;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.FixedSingle;
            label10.Location = new Point(225, 10);
            label10.Name = "label10";
            label10.Size = new Size(2, 30);
            label10.TabIndex = 22;
            label10.Text = "label10";
            // 
            // closeAllChartsToolbarButton
            // 
            closeAllChartsToolbarButton.Location = new Point(936, 7);
            closeAllChartsToolbarButton.Name = "closeAllChartsToolbarButton";
            closeAllChartsToolbarButton.Size = new Size(110, 34);
            closeAllChartsToolbarButton.TabIndex = 21;
            closeAllChartsToolbarButton.Text = "بستن چارتها";
            closeAllChartsToolbarButton.UseVisualStyleBackColor = true;
            // 
            // saveAnalysisButton
            // 
            saveAnalysisButton.Location = new Point(1116, 46);
            saveAnalysisButton.Name = "saveAnalysisButton";
            saveAnalysisButton.RightToLeft = RightToLeft.Yes;
            saveAnalysisButton.Size = new Size(88, 34);
            saveAnalysisButton.TabIndex = 11;
            saveAnalysisButton.Text = "ذخیره تحلیل";
            saveAnalysisButton.UseVisualStyleBackColor = true;
            // 
            // chartTypeComboBox
            // 
            chartTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            chartTypeComboBox.Items.AddRange(new object[] { "شمعی", "خطی", "میله ای" });
            chartTypeComboBox.Location = new Point(6, 7);
            chartTypeComboBox.Name = "chartTypeComboBox";
            chartTypeComboBox.Size = new Size(84, 33);
            chartTypeComboBox.TabIndex = 0;
            // 
            // gridButton
            // 
            gridButton.Location = new Point(94, 7);
            gridButton.Name = "gridButton";
            gridButton.Size = new Size(60, 34);
            gridButton.TabIndex = 1;
            gridButton.Text = "گرید";
            // 
            // crossButton
            // 
            crossButton.AutoSize = true;
            crossButton.Location = new Point(156, 7);
            crossButton.Name = "crossButton";
            crossButton.Size = new Size(66, 35);
            crossButton.TabIndex = 2;
            crossButton.Text = "Cross";
            // 
            // indicatorPanelButton
            // 
            indicatorPanelButton.Location = new Point(1049, 7);
            indicatorPanelButton.Name = "indicatorPanelButton";
            indicatorPanelButton.Size = new Size(88, 34);
            indicatorPanelButton.TabIndex = 3;
            indicatorPanelButton.Text = "اندیکاتورها";
            indicatorPanelButton.UseVisualStyleBackColor = true;
            // 
            // testModeButton
            // 
            testModeButton.Location = new Point(1011, 46);
            testModeButton.Name = "testModeButton";
            testModeButton.Size = new Size(58, 34);
            testModeButton.TabIndex = 22;
            testModeButton.Text = "تست";
            testModeButton.UseVisualStyleBackColor = true;
            // 
            // testStepBackButton
            // 
            testStepBackButton.Location = new Point(971, 46);
            testStepBackButton.Name = "testStepBackButton";
            testStepBackButton.Size = new Size(34, 34);
            testStepBackButton.TabIndex = 23;
            testStepBackButton.Text = "◀";
            testStepBackButton.UseVisualStyleBackColor = true;
            // 
            // testStepForwardButton
            // 
            testStepForwardButton.Location = new Point(1075, 46);
            testStepForwardButton.Name = "testStepForwardButton";
            testStepForwardButton.Size = new Size(34, 34);
            testStepForwardButton.TabIndex = 24;
            testStepForwardButton.Text = "▶";
            testStepForwardButton.UseVisualStyleBackColor = true;
            // 
            // zoomInButton
            // 
            zoomInButton.Location = new Point(233, 7);
            zoomInButton.Name = "zoomInButton";
            zoomInButton.Size = new Size(54, 34);
            zoomInButton.TabIndex = 4;
            zoomInButton.Text = "+";
            // 
            // zoomOutButton
            // 
            zoomOutButton.Location = new Point(288, 7);
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(54, 34);
            zoomOutButton.TabIndex = 5;
            zoomOutButton.Text = "-";
            // 
            // resetChartButton
            // 
            resetChartButton.AutoSize = true;
            resetChartButton.Location = new Point(343, 7);
            resetChartButton.Name = "resetChartButton";
            resetChartButton.Size = new Size(86, 35);
            resetChartButton.TabIndex = 6;
            resetChartButton.Text = "بازنشانی";
            // 
            // hideChartButton
            // 
            hideChartButton.Location = new Point(829, 7);
            hideChartButton.Name = "hideChartButton";
            hideChartButton.Size = new Size(104, 34);
            hideChartButton.TabIndex = 6;
            hideChartButton.Text = "چارت پنهان";
            hideChartButton.Click += hideChartButton_Click;
            // 
            // hideToolsButton
            // 
            hideToolsButton.Location = new Point(707, 7);
            hideToolsButton.Name = "hideToolsButton";
            hideToolsButton.Size = new Size(120, 34);
            hideToolsButton.TabIndex = 7;
            hideToolsButton.Text = "حذف ابزارها";
            // 
            // printChartButton
            // 
            printChartButton.Location = new Point(441, 7);
            printChartButton.Name = "printChartButton";
            printChartButton.Size = new Size(70, 34);
            printChartButton.TabIndex = 8;
            printChartButton.Text = "چاپ";
            // 
            // snapshotChartButton
            // 
            snapshotChartButton.Location = new Point(514, 7);
            snapshotChartButton.Name = "snapshotChartButton";
            snapshotChartButton.Size = new Size(79, 34);
            snapshotChartButton.TabIndex = 9;
            snapshotChartButton.Text = "تصویر";
            // 
            // fullScreenChartButton
            // 
            fullScreenChartButton.Location = new Point(595, 7);
            fullScreenChartButton.Name = "fullScreenChartButton";
            fullScreenChartButton.Size = new Size(106, 34);
            fullScreenChartButton.TabIndex = 10;
            fullScreenChartButton.Text = "تمام صفحه";
            // 
            // drawTrendLineButton
            // 
            drawTrendLineButton.Location = new Point(8, 46);
            drawTrendLineButton.Name = "drawTrendLineButton";
            drawTrendLineButton.RightToLeft = RightToLeft.Yes;
            drawTrendLineButton.Size = new Size(82, 34);
            drawTrendLineButton.TabIndex = 20;
            drawTrendLineButton.Text = "خط روند";
            drawTrendLineButton.UseVisualStyleBackColor = true;
            // 
            // drawTrendChannelButton
            // 
            drawTrendChannelButton.Location = new Point(91, 46);
            drawTrendChannelButton.Name = "drawTrendChannelButton";
            drawTrendChannelButton.RightToLeft = RightToLeft.Yes;
            drawTrendChannelButton.Size = new Size(68, 34);
            drawTrendChannelButton.TabIndex = 20;
            drawTrendChannelButton.Text = "کانال روند";
            drawTrendChannelButton.UseVisualStyleBackColor = true;
            // 
            // drawHorizontalDoubleButton
            // 
            drawHorizontalDoubleButton.Location = new Point(159, 46);
            drawHorizontalDoubleButton.Name = "drawHorizontalDoubleButton";
            drawHorizontalDoubleButton.RightToLeft = RightToLeft.Yes;
            drawHorizontalDoubleButton.Size = new Size(69, 34);
            drawHorizontalDoubleButton.TabIndex = 20;
            drawHorizontalDoubleButton.Text = "افقی";
            drawHorizontalDoubleButton.UseVisualStyleBackColor = true;
            // 
            // drawVerticalDoubleButton
            // 
            drawVerticalDoubleButton.Location = new Point(227, 46);
            drawVerticalDoubleButton.Name = "drawVerticalDoubleButton";
            drawVerticalDoubleButton.RightToLeft = RightToLeft.Yes;
            drawVerticalDoubleButton.Size = new Size(74, 34);
            drawVerticalDoubleButton.TabIndex = 20;
            drawVerticalDoubleButton.Text = "عمودی";
            drawVerticalDoubleButton.UseVisualStyleBackColor = true;
            // 
            // drawHorizontalRayButton
            // 
            drawHorizontalRayButton.Location = new Point(300, 46);
            drawHorizontalRayButton.Name = "drawHorizontalRayButton";
            drawHorizontalRayButton.RightToLeft = RightToLeft.Yes;
            drawHorizontalRayButton.Size = new Size(76, 34);
            drawHorizontalRayButton.TabIndex = 20;
            drawHorizontalRayButton.Text = "نیم خط افقی";
            drawHorizontalRayButton.UseVisualStyleBackColor = true;
            // 
            // drawTrendLineArrowButton
            // 
            drawTrendLineArrowButton.Location = new Point(375, 46);
            drawTrendLineArrowButton.Name = "drawTrendLineArrowButton";
            drawTrendLineArrowButton.RightToLeft = RightToLeft.Yes;
            drawTrendLineArrowButton.Size = new Size(81, 34);
            drawTrendLineArrowButton.TabIndex = 20;
            drawTrendLineArrowButton.Text = "فلش";
            drawTrendLineArrowButton.UseVisualStyleBackColor = true;
            // 
            // drawRectangleButton
            // 
            drawRectangleButton.Location = new Point(456, 46);
            drawRectangleButton.Name = "drawRectangleButton";
            drawRectangleButton.RightToLeft = RightToLeft.Yes;
            drawRectangleButton.Size = new Size(90, 34);
            drawRectangleButton.TabIndex = 20;
            drawRectangleButton.Text = "مستطیل";
            drawRectangleButton.UseVisualStyleBackColor = true;
            // 
            // drawFibonacciButton
            // 
            drawFibonacciButton.Location = new Point(610, 46);
            drawFibonacciButton.Name = "drawFibonacciButton";
            drawFibonacciButton.RightToLeft = RightToLeft.Yes;
            drawFibonacciButton.Size = new Size(95, 34);
            drawFibonacciButton.TabIndex = 20;
            drawFibonacciButton.Text = "فیبو R";
            drawFibonacciButton.UseVisualStyleBackColor = true;
            // 
            // drawTextButton
            // 
            drawTextButton.Location = new Point(801, 46);
            drawTextButton.Name = "drawTextButton";
            drawTextButton.RightToLeft = RightToLeft.Yes;
            drawTextButton.Size = new Size(82, 34);
            drawTextButton.TabIndex = 20;
            drawTextButton.Text = "متن";
            drawTextButton.UseVisualStyleBackColor = true;
            // 
            // drawPitchforkButton
            // 
            drawPitchforkButton.Location = new Point(545, 46);
            drawPitchforkButton.Name = "drawPitchforkButton";
            drawPitchforkButton.RightToLeft = RightToLeft.Yes;
            drawPitchforkButton.Size = new Size(65, 34);
            drawPitchforkButton.TabIndex = 20;
            drawPitchforkButton.Text = "چنگال";
            drawPitchforkButton.UseVisualStyleBackColor = true;
            // 
            // drawFibonacciExtensionButton
            // 
            drawFibonacciExtensionButton.Location = new Point(705, 46);
            drawFibonacciExtensionButton.Name = "drawFibonacciExtensionButton";
            drawFibonacciExtensionButton.RightToLeft = RightToLeft.Yes;
            drawFibonacciExtensionButton.Size = new Size(96, 34);
            drawFibonacciExtensionButton.TabIndex = 20;
            drawFibonacciExtensionButton.Text = "فیبو X";
            drawFibonacciExtensionButton.UseVisualStyleBackColor = true;
            // 
            // drawMeasureButton
            // 
            drawMeasureButton.Location = new Point(883, 46);
            drawMeasureButton.Name = "drawMeasureButton";
            drawMeasureButton.RightToLeft = RightToLeft.Yes;
            drawMeasureButton.Size = new Size(82, 34);
            drawMeasureButton.TabIndex = 20;
            drawMeasureButton.Text = "خط کش";
            drawMeasureButton.UseVisualStyleBackColor = true;
            // 
            // marketTypeComboBox
            // 
            marketTypeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketTypeComboBox.FormattingEnabled = true;
            marketTypeComboBox.Location = new Point(11, 132);
            marketTypeComboBox.Name = "marketTypeComboBox";
            marketTypeComboBox.Size = new Size(198, 33);
            marketTypeComboBox.TabIndex = 11;
            // 
            // marketBoardComboBox
            // 
            marketBoardComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketBoardComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketBoardComboBox.FormattingEnabled = true;
            marketBoardComboBox.Location = new Point(11, 197);
            marketBoardComboBox.Name = "marketBoardComboBox";
            marketBoardComboBox.Size = new Size(198, 33);
            marketBoardComboBox.TabIndex = 10;
            // 
            // marketAssetComboBox
            // 
            marketAssetComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketAssetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketAssetComboBox.FormattingEnabled = true;
            marketAssetComboBox.Location = new Point(11, 266);
            marketAssetComboBox.Name = "marketAssetComboBox";
            marketAssetComboBox.Size = new Size(198, 33);
            marketAssetComboBox.TabIndex = 9;
            // 
            // marketFundTypeComboBox
            // 
            marketFundTypeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketFundTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketFundTypeComboBox.FormattingEnabled = true;
            marketFundTypeComboBox.Location = new Point(11, 339);
            marketFundTypeComboBox.Name = "marketFundTypeComboBox";
            marketFundTypeComboBox.Size = new Size(198, 33);
            marketFundTypeComboBox.TabIndex = 8;
            // 
            // marketIndustryGroupComboBox
            // 
            marketIndustryGroupComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketIndustryGroupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketIndustryGroupComboBox.FormattingEnabled = true;
            marketIndustryGroupComboBox.Location = new Point(11, 408);
            marketIndustryGroupComboBox.Name = "marketIndustryGroupComboBox";
            marketIndustryGroupComboBox.Size = new Size(198, 33);
            marketIndustryGroupComboBox.TabIndex = 7;
            // 
            // marketExchangeComboBox
            // 
            marketExchangeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            marketExchangeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketExchangeComboBox.FormattingEnabled = true;
            marketExchangeComboBox.Location = new Point(11, 66);
            marketExchangeComboBox.Name = "marketExchangeComboBox";
            marketExchangeComboBox.Size = new Size(198, 33);
            marketExchangeComboBox.TabIndex = 6;
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
            // MainForm
            // 
            ClientSize = new Size(1630, 966);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "Trade.It";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
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
            marketsTabPage.ResumeLayout(false);
            marketsTabPage.PerformLayout();
            chartPanel.ResumeLayout(false);
            chartTabControl.ResumeLayout(false);
            chartTabPage.ResumeLayout(false);
            chartInfoPanel.ResumeLayout(false);
            chartToolbarPanel.ResumeLayout(false);
            chartToolbarPanel.PerformLayout();
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
        private Button closeAllChartsToolbarButton;
        private Label label12;
        private Label label11;
        private Label label10;
        private ToolStripMenuItem symbolDefinitionMenuItem;
        private ComboBox marketTypeComboBox;
        private ComboBox marketBoardComboBox;
        private ComboBox marketAssetComboBox;
        private ComboBox marketFundTypeComboBox;
        private ComboBox marketIndustryGroupComboBox;
        private ComboBox marketExchangeComboBox;
        private CheckedListBox marketExchangeCheckedListBox;
        private CheckedListBox marketTypeCheckedListBox;
        private CheckedListBox marketBoardCheckedListBox;
        private CheckedListBox marketAssetCheckedListBox;
        private CheckedListBox marketFundTypeCheckedListBox;
        private CheckedListBox marketIndustryGroupCheckedListBox;
        private Button marketClearButton;
        private Button marketApplyButton;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label20;
        private Label label19;
    }
}
