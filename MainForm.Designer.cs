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
        private System.Windows.Forms.DataGridViewTextBoxColumn rowColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastTradeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectColumn;
        private System.Windows.Forms.Button refreshButtonPortfolio;

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
        private System.Windows.Forms.GroupBox comparisonGroup;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox;
        private System.Windows.Forms.TextBox comparisonFirstTextBox;
        private System.Windows.Forms.TextBox comparisonSecondTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.GroupBox comparisonGroup6;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox6;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox6;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox6;
        private System.Windows.Forms.TextBox comparisonFirstTextBox6;
        private System.Windows.Forms.TextBox comparisonSecondTextBox6;
        private System.Windows.Forms.Label label5_6;
        private System.Windows.Forms.Label label6_6;
        private System.Windows.Forms.Label label8_6;

        private System.Windows.Forms.GroupBox comparisonGroup7;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox7;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox7;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox7;
        private System.Windows.Forms.TextBox comparisonFirstTextBox7;
        private System.Windows.Forms.TextBox comparisonSecondTextBox7;
        private System.Windows.Forms.Label label5_7;
        private System.Windows.Forms.Label label6_7;
        private System.Windows.Forms.Label label8_7;

        private System.Windows.Forms.GroupBox comparisonGroup8;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox8;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox8;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox8;
        private System.Windows.Forms.TextBox comparisonFirstTextBox8;
        private System.Windows.Forms.TextBox comparisonSecondTextBox8;
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

        private System.Windows.Forms.Button clearFiltersButton;

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
            stocksDataGridView = new DataGridView();
            selectAllCheckBox = new CheckBox();
            selectNoneCheckBox = new CheckBox();
            speedLabel = new Label();
            navigationSpeedTextBox = new TextBox();
            navigationButton = new Button();
            newPortfolioButton = new Button();
            refreshButton = new Button();
            deleteButton = new Button();
            portfolioComboBox = new ComboBox();
            portfolioLabel = new Label();
            tabPage2 = new TabPage();
            clearFiltersButton = new Button();
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
            comparisonSecondTextBox8 = new TextBox();
            label6_8 = new Label();
            comparisonFirstTextBox8 = new TextBox();
            label5_8 = new Label();
            comparisonFirstComboBox8 = new ComboBox();
            comparisonOperatorComboBox8 = new ComboBox();
            comparisonSecondComboBox8 = new ComboBox();
            comparisonGroup7 = new GroupBox();
            label8_7 = new Label();
            comparisonSecondTextBox7 = new TextBox();
            label6_7 = new Label();
            comparisonFirstTextBox7 = new TextBox();
            label5_7 = new Label();
            comparisonFirstComboBox7 = new ComboBox();
            comparisonOperatorComboBox7 = new ComboBox();
            comparisonSecondComboBox7 = new ComboBox();
            comparisonGroup6 = new GroupBox();
            label8_6 = new Label();
            comparisonSecondTextBox6 = new TextBox();
            label6_6 = new Label();
            comparisonFirstTextBox6 = new TextBox();
            label5_6 = new Label();
            comparisonFirstComboBox6 = new ComboBox();
            comparisonOperatorComboBox6 = new ComboBox();
            comparisonSecondComboBox6 = new ComboBox();
            comparisonGroup = new GroupBox();
            label8 = new Label();
            comparisonSecondTextBox = new TextBox();
            label6 = new Label();
            comparisonFirstTextBox = new TextBox();
            label5 = new Label();
            comparisonFirstComboBox = new ComboBox();
            comparisonOperatorComboBox = new ComboBox();
            comparisonSecondComboBox = new ComboBox();
            pastDaysGroup = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            pastDaysTextBox = new TextBox();
            pastDaysStatusComboBox = new ComboBox();
            volumeRatioGroup = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            volumeRatioTextBox = new TextBox();
            volumeRatioOperatorComboBox = new ComboBox();
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
            rowColumn = new DataGridViewTextBoxColumn();
            symbolColumn = new DataGridViewTextBoxColumn();
            lastTradeColumn = new DataGridViewTextBoxColumn();
            selectColumn = new DataGridViewCheckBoxColumn();
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
            groupBox2 = new GroupBox();
            mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            controlTabControl.SuspendLayout();
            stocksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).BeginInit();
            tabPage2.SuspendLayout();
            ohlcChangeFilterGroup.SuspendLayout();
            comparisonGroup8.SuspendLayout();
            comparisonGroup7.SuspendLayout();
            comparisonGroup6.SuspendLayout();
            comparisonGroup.SuspendLayout();
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
            groupBox2.SuspendLayout();
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
            mainSplitContainer.SplitterDistance = 300;
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
            controlTabControl.Size = new Size(300, 933);
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
            stocksTabPage.Size = new Size(292, 895);
            stocksTabPage.TabIndex = 0;
            stocksTabPage.Text = "سبد";
            // 
            // stocksDataGridView
            // 
            stocksDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksDataGridView.ColumnHeadersHeight = 34;
            stocksDataGridView.Location = new Point(3, 47);
            stocksDataGridView.Name = "stocksDataGridView";
            stocksDataGridView.RowHeadersWidth = 62;
            stocksDataGridView.Size = new Size(286, 672);
            stocksDataGridView.TabIndex = 0;
            // 
            // selectAllCheckBox
            // 
            selectAllCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(175, 78);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(70, 29);
            selectAllCheckBox.TabIndex = 0;
            selectAllCheckBox.Text = "همه";
            // 
            // selectNoneCheckBox
            // 
            selectNoneCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new Point(69, 78);
            selectNoneCheckBox.Name = "selectNoneCheckBox";
            selectNoneCheckBox.Size = new Size(101, 29);
            selectNoneCheckBox.TabIndex = 1;
            selectNoneCheckBox.Text = "هیچکدام";
            // 
            // speedLabel
            // 
            speedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(104, 33);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(64, 25);
            speedLabel.TabIndex = 2;
            speedLabel.Text = "سرعت:";
            // 
            // navigationSpeedTextBox
            // 
            navigationSpeedTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navigationSpeedTextBox.Location = new Point(24, 32);
            navigationSpeedTextBox.Name = "navigationSpeedTextBox";
            navigationSpeedTextBox.Size = new Size(74, 31);
            navigationSpeedTextBox.TabIndex = 3;
            navigationSpeedTextBox.Text = "1000";
            navigationSpeedTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // navigationButton
            // 
            navigationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            navigationButton.Location = new Point(170, 32);
            navigationButton.Name = "navigationButton";
            navigationButton.Size = new Size(101, 34);
            navigationButton.TabIndex = 0;
            navigationButton.Text = "پیمایش";
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
            // refreshButton
            // 
            refreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            refreshButton.Location = new Point(114, 113);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(69, 34);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "تازه";
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.Location = new Point(198, 113);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(73, 34);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "حذف";
            // 
            // portfolioComboBox
            // 
            portfolioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new Point(8, 3);
            portfolioComboBox.Name = "portfolioComboBox";
            portfolioComboBox.Size = new Size(212, 33);
            portfolioComboBox.TabIndex = 2;
            // 
            // portfolioLabel
            // 
            portfolioLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new Point(226, 6);
            portfolioLabel.Name = "portfolioLabel";
            portfolioLabel.Size = new Size(63, 25);
            portfolioLabel.TabIndex = 3;
            portfolioLabel.Text = "سبدها:";
            portfolioLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(clearFiltersButton);
            tabPage2.Controls.Add(ohlcChangeFilterGroup);
            tabPage2.Controls.Add(comparisonGroup8);
            tabPage2.Controls.Add(comparisonGroup7);
            tabPage2.Controls.Add(comparisonGroup6);
            tabPage2.Controls.Add(comparisonGroup);
            tabPage2.Controls.Add(pastDaysGroup);
            tabPage2.Controls.Add(volumeRatioGroup);
            tabPage2.Controls.Add(nameFilterGroup);
            tabPage2.Controls.Add(tradingStatusGroup);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(8);
            tabPage2.RightToLeft = RightToLeft.Yes;
            tabPage2.Size = new Size(360, 787);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "فیلترها";
            // 
            // clearFiltersButton
            // 
            clearFiltersButton.Location = new Point(8, 981);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(314, 36);
            clearFiltersButton.TabIndex = 9;
            clearFiltersButton.Text = "پاک کردن";
            clearFiltersButton.UseVisualStyleBackColor = true;
            // 
            // ohlcChangeFilterGroup
            // 
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeFieldLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeFieldComboBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDaysLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDaysTextBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangePercentLabel);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangePercentTextBox);
            ohlcChangeFilterGroup.Controls.Add(ohlcChangeDirectionComboBox);
            ohlcChangeFilterGroup.Dock = DockStyle.Top;
            ohlcChangeFilterGroup.Location = new Point(8, 876);
            ohlcChangeFilterGroup.Name = "ohlcChangeFilterGroup";
            ohlcChangeFilterGroup.Size = new Size(318, 105);
            ohlcChangeFilterGroup.TabIndex = 8;
            ohlcChangeFilterGroup.TabStop = false;
            ohlcChangeFilterGroup.Text = "9. رشد/افت OHLC:";
            // 
            // ohlcChangeFieldLabel
            // 
            ohlcChangeFieldLabel.AutoSize = true;
            ohlcChangeFieldLabel.Location = new Point(274, 38);
            ohlcChangeFieldLabel.Name = "ohlcChangeFieldLabel";
            ohlcChangeFieldLabel.Size = new Size(55, 25);
            ohlcChangeFieldLabel.TabIndex = 0;
            ohlcChangeFieldLabel.Text = "قیمت";
            // 
            // ohlcChangeFieldComboBox
            // 
            ohlcChangeFieldComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ohlcChangeFieldComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "پایانی" });
            ohlcChangeFieldComboBox.Location = new Point(210, 34);
            ohlcChangeFieldComboBox.Name = "ohlcChangeFieldComboBox";
            ohlcChangeFieldComboBox.Size = new Size(60, 33);
            ohlcChangeFieldComboBox.TabIndex = 1;
            // 
            // ohlcChangeDaysLabel
            // 
            ohlcChangeDaysLabel.AutoSize = true;
            ohlcChangeDaysLabel.Location = new Point(119, 38);
            ohlcChangeDaysLabel.Name = "ohlcChangeDaysLabel";
            ohlcChangeDaysLabel.Size = new Size(80, 25);
            ohlcChangeDaysLabel.TabIndex = 2;
            ohlcChangeDaysLabel.Text = "کندل قبل";
            // 
            // ohlcChangeDaysTextBox
            // 
            ohlcChangeDaysTextBox.Location = new Point(62, 34);
            ohlcChangeDaysTextBox.Name = "ohlcChangeDaysTextBox";
            ohlcChangeDaysTextBox.Size = new Size(57, 31);
            ohlcChangeDaysTextBox.TabIndex = 3;
            ohlcChangeDaysTextBox.Text = "5";
            ohlcChangeDaysTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ohlcChangePercentLabel
            // 
            ohlcChangePercentLabel.AutoSize = true;
            ohlcChangePercentLabel.Location = new Point(274, 73);
            ohlcChangePercentLabel.Name = "ohlcChangePercentLabel";
            ohlcChangePercentLabel.Size = new Size(27, 25);
            ohlcChangePercentLabel.TabIndex = 4;
            ohlcChangePercentLabel.Text = "%";
            // 
            // ohlcChangePercentTextBox
            // 
            ohlcChangePercentTextBox.Location = new Point(210, 70);
            ohlcChangePercentTextBox.Name = "ohlcChangePercentTextBox";
            ohlcChangePercentTextBox.Size = new Size(60, 31);
            ohlcChangePercentTextBox.TabIndex = 5;
            ohlcChangePercentTextBox.Text = "5";
            ohlcChangePercentTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ohlcChangeDirectionComboBox
            // 
            ohlcChangeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ohlcChangeDirectionComboBox.Items.AddRange(new object[] { "رشد حداقل", "افت حداقل" });
            ohlcChangeDirectionComboBox.Location = new Point(62, 69);
            ohlcChangeDirectionComboBox.Name = "ohlcChangeDirectionComboBox";
            ohlcChangeDirectionComboBox.Size = new Size(142, 33);
            ohlcChangeDirectionComboBox.TabIndex = 6;
            // 
            // comparisonGroup8
            // 
            comparisonGroup8.Controls.Add(label8_8);
            comparisonGroup8.Controls.Add(comparisonSecondTextBox8);
            comparisonGroup8.Controls.Add(label6_8);
            comparisonGroup8.Controls.Add(comparisonFirstTextBox8);
            comparisonGroup8.Controls.Add(label5_8);
            comparisonGroup8.Controls.Add(comparisonFirstComboBox8);
            comparisonGroup8.Controls.Add(comparisonOperatorComboBox8);
            comparisonGroup8.Controls.Add(comparisonSecondComboBox8);
            comparisonGroup8.Dock = DockStyle.Top;
            comparisonGroup8.Location = new Point(8, 757);
            comparisonGroup8.Name = "comparisonGroup8";
            comparisonGroup8.Size = new Size(318, 119);
            comparisonGroup8.TabIndex = 7;
            comparisonGroup8.TabStop = false;
            comparisonGroup8.Text = "8. مقایسه قیمت:";
            // 
            // label8_8
            // 
            label8_8.AutoSize = true;
            label8_8.Location = new Point(75, 80);
            label8_8.Name = "label8_8";
            label8_8.Size = new Size(101, 25);
            label8_8.TabIndex = 0;
            label8_8.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox8
            // 
            comparisonSecondTextBox8.Location = new Point(177, 76);
            comparisonSecondTextBox8.Name = "comparisonSecondTextBox8";
            comparisonSecondTextBox8.Size = new Size(57, 31);
            comparisonSecondTextBox8.TabIndex = 1;
            comparisonSecondTextBox8.Text = "5";
            comparisonSecondTextBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // label6_8
            // 
            label6_8.AutoSize = true;
            label6_8.Location = new Point(79, 45);
            label6_8.Name = "label6_8";
            label6_8.Size = new Size(101, 25);
            label6_8.TabIndex = 3;
            label6_8.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox8
            // 
            comparisonFirstTextBox8.Location = new Point(177, 39);
            comparisonFirstTextBox8.Name = "comparisonFirstTextBox8";
            comparisonFirstTextBox8.Size = new Size(57, 31);
            comparisonFirstTextBox8.TabIndex = 4;
            comparisonFirstTextBox8.Text = "5";
            comparisonFirstTextBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // label5_8
            // 
            label5_8.AutoSize = true;
            label5_8.Location = new Point(250, 36);
            label5_8.Name = "label5_8";
            label5_8.Size = new Size(55, 25);
            label5_8.TabIndex = 5;
            label5_8.Text = "قیمت";
            // 
            // comparisonFirstComboBox8
            // 
            comparisonFirstComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox8.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox8.Location = new Point(236, 36);
            comparisonFirstComboBox8.Name = "comparisonFirstComboBox8";
            comparisonFirstComboBox8.Size = new Size(72, 33);
            comparisonFirstComboBox8.TabIndex = 6;
            // 
            // comparisonOperatorComboBox8
            // 
            comparisonOperatorComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox8.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox8.Location = new Point(3, 42);
            comparisonOperatorComboBox8.Name = "comparisonOperatorComboBox8";
            comparisonOperatorComboBox8.Size = new Size(73, 33);
            comparisonOperatorComboBox8.TabIndex = 7;
            // 
            // comparisonSecondComboBox8
            // 
            comparisonSecondComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox8.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox8.Location = new Point(236, 75);
            comparisonSecondComboBox8.Name = "comparisonSecondComboBox8";
            comparisonSecondComboBox8.Size = new Size(75, 33);
            comparisonSecondComboBox8.TabIndex = 8;
            // 
            // comparisonGroup7
            // 
            comparisonGroup7.Controls.Add(label8_7);
            comparisonGroup7.Controls.Add(comparisonSecondTextBox7);
            comparisonGroup7.Controls.Add(label6_7);
            comparisonGroup7.Controls.Add(comparisonFirstTextBox7);
            comparisonGroup7.Controls.Add(label5_7);
            comparisonGroup7.Controls.Add(comparisonFirstComboBox7);
            comparisonGroup7.Controls.Add(comparisonOperatorComboBox7);
            comparisonGroup7.Controls.Add(comparisonSecondComboBox7);
            comparisonGroup7.Dock = DockStyle.Top;
            comparisonGroup7.Location = new Point(8, 638);
            comparisonGroup7.Name = "comparisonGroup7";
            comparisonGroup7.Size = new Size(318, 119);
            comparisonGroup7.TabIndex = 6;
            comparisonGroup7.TabStop = false;
            comparisonGroup7.Text = "7. مقایسه قیمت:";
            // 
            // label8_7
            // 
            label8_7.Location = new Point(0, 0);
            label8_7.Name = "label8_7";
            label8_7.Size = new Size(100, 23);
            label8_7.TabIndex = 0;
            // 
            // comparisonSecondTextBox7
            // 
            comparisonSecondTextBox7.Location = new Point(0, 0);
            comparisonSecondTextBox7.Name = "comparisonSecondTextBox7";
            comparisonSecondTextBox7.Size = new Size(100, 31);
            comparisonSecondTextBox7.TabIndex = 1;
            // 
            // label6_7
            // 
            label6_7.Location = new Point(0, 0);
            label6_7.Name = "label6_7";
            label6_7.Size = new Size(100, 23);
            label6_7.TabIndex = 2;
            // 
            // comparisonFirstTextBox7
            // 
            comparisonFirstTextBox7.Location = new Point(0, 0);
            comparisonFirstTextBox7.Name = "comparisonFirstTextBox7";
            comparisonFirstTextBox7.Size = new Size(100, 31);
            comparisonFirstTextBox7.TabIndex = 3;
            // 
            // label5_7
            // 
            label5_7.Location = new Point(0, 0);
            label5_7.Name = "label5_7";
            label5_7.Size = new Size(100, 23);
            label5_7.TabIndex = 4;
            // 
            // comparisonFirstComboBox7
            // 
            comparisonFirstComboBox7.Location = new Point(0, 0);
            comparisonFirstComboBox7.Name = "comparisonFirstComboBox7";
            comparisonFirstComboBox7.Size = new Size(121, 33);
            comparisonFirstComboBox7.TabIndex = 5;
            // 
            // comparisonOperatorComboBox7
            // 
            comparisonOperatorComboBox7.Location = new Point(0, 0);
            comparisonOperatorComboBox7.Name = "comparisonOperatorComboBox7";
            comparisonOperatorComboBox7.Size = new Size(121, 33);
            comparisonOperatorComboBox7.TabIndex = 6;
            // 
            // comparisonSecondComboBox7
            // 
            comparisonSecondComboBox7.Location = new Point(0, 0);
            comparisonSecondComboBox7.Name = "comparisonSecondComboBox7";
            comparisonSecondComboBox7.Size = new Size(121, 33);
            comparisonSecondComboBox7.TabIndex = 7;
            // 
            // comparisonGroup6
            // 
            comparisonGroup6.Controls.Add(label8_6);
            comparisonGroup6.Controls.Add(comparisonSecondTextBox6);
            comparisonGroup6.Controls.Add(label6_6);
            comparisonGroup6.Controls.Add(comparisonFirstTextBox6);
            comparisonGroup6.Controls.Add(label5_6);
            comparisonGroup6.Controls.Add(comparisonFirstComboBox6);
            comparisonGroup6.Controls.Add(comparisonOperatorComboBox6);
            comparisonGroup6.Controls.Add(comparisonSecondComboBox6);
            comparisonGroup6.Dock = DockStyle.Top;
            comparisonGroup6.Location = new Point(8, 519);
            comparisonGroup6.Name = "comparisonGroup6";
            comparisonGroup6.Size = new Size(318, 119);
            comparisonGroup6.TabIndex = 5;
            comparisonGroup6.TabStop = false;
            comparisonGroup6.Text = "6. مقایسه قیمت:";
            // 
            // label8_6
            // 
            label8_6.Location = new Point(0, 0);
            label8_6.Name = "label8_6";
            label8_6.Size = new Size(100, 23);
            label8_6.TabIndex = 0;
            // 
            // comparisonSecondTextBox6
            // 
            comparisonSecondTextBox6.Location = new Point(0, 0);
            comparisonSecondTextBox6.Name = "comparisonSecondTextBox6";
            comparisonSecondTextBox6.Size = new Size(100, 31);
            comparisonSecondTextBox6.TabIndex = 1;
            // 
            // label6_6
            // 
            label6_6.Location = new Point(0, 0);
            label6_6.Name = "label6_6";
            label6_6.Size = new Size(100, 23);
            label6_6.TabIndex = 2;
            // 
            // comparisonFirstTextBox6
            // 
            comparisonFirstTextBox6.Location = new Point(0, 0);
            comparisonFirstTextBox6.Name = "comparisonFirstTextBox6";
            comparisonFirstTextBox6.Size = new Size(100, 31);
            comparisonFirstTextBox6.TabIndex = 3;
            // 
            // label5_6
            // 
            label5_6.Location = new Point(0, 0);
            label5_6.Name = "label5_6";
            label5_6.Size = new Size(100, 23);
            label5_6.TabIndex = 4;
            // 
            // comparisonFirstComboBox6
            // 
            comparisonFirstComboBox6.Location = new Point(0, 0);
            comparisonFirstComboBox6.Name = "comparisonFirstComboBox6";
            comparisonFirstComboBox6.Size = new Size(121, 33);
            comparisonFirstComboBox6.TabIndex = 5;
            // 
            // comparisonOperatorComboBox6
            // 
            comparisonOperatorComboBox6.Location = new Point(0, 0);
            comparisonOperatorComboBox6.Name = "comparisonOperatorComboBox6";
            comparisonOperatorComboBox6.Size = new Size(121, 33);
            comparisonOperatorComboBox6.TabIndex = 6;
            // 
            // comparisonSecondComboBox6
            // 
            comparisonSecondComboBox6.Location = new Point(0, 0);
            comparisonSecondComboBox6.Name = "comparisonSecondComboBox6";
            comparisonSecondComboBox6.Size = new Size(121, 33);
            comparisonSecondComboBox6.TabIndex = 7;
            // 
            // comparisonGroup
            // 
            comparisonGroup.Controls.Add(label8);
            comparisonGroup.Controls.Add(comparisonSecondTextBox);
            comparisonGroup.Controls.Add(label6);
            comparisonGroup.Controls.Add(comparisonFirstTextBox);
            comparisonGroup.Controls.Add(label5);
            comparisonGroup.Controls.Add(comparisonFirstComboBox);
            comparisonGroup.Controls.Add(comparisonOperatorComboBox);
            comparisonGroup.Controls.Add(comparisonSecondComboBox);
            comparisonGroup.Dock = DockStyle.Top;
            comparisonGroup.Location = new Point(8, 400);
            comparisonGroup.Name = "comparisonGroup";
            comparisonGroup.Size = new Size(318, 119);
            comparisonGroup.TabIndex = 4;
            comparisonGroup.TabStop = false;
            comparisonGroup.Text = "5. مقایسه قیمت:";
            // 
            // label8
            // 
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(100, 23);
            label8.TabIndex = 0;
            // 
            // comparisonSecondTextBox
            // 
            comparisonSecondTextBox.Location = new Point(0, 0);
            comparisonSecondTextBox.Name = "comparisonSecondTextBox";
            comparisonSecondTextBox.Size = new Size(100, 31);
            comparisonSecondTextBox.TabIndex = 1;
            // 
            // label6
            // 
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 2;
            // 
            // comparisonFirstTextBox
            // 
            comparisonFirstTextBox.Location = new Point(0, 0);
            comparisonFirstTextBox.Name = "comparisonFirstTextBox";
            comparisonFirstTextBox.Size = new Size(100, 31);
            comparisonFirstTextBox.TabIndex = 3;
            // 
            // label5
            // 
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 4;
            // 
            // comparisonFirstComboBox
            // 
            comparisonFirstComboBox.Location = new Point(0, 0);
            comparisonFirstComboBox.Name = "comparisonFirstComboBox";
            comparisonFirstComboBox.Size = new Size(121, 33);
            comparisonFirstComboBox.TabIndex = 5;
            // 
            // comparisonOperatorComboBox
            // 
            comparisonOperatorComboBox.Location = new Point(0, 0);
            comparisonOperatorComboBox.Name = "comparisonOperatorComboBox";
            comparisonOperatorComboBox.Size = new Size(121, 33);
            comparisonOperatorComboBox.TabIndex = 6;
            // 
            // comparisonSecondComboBox
            // 
            comparisonSecondComboBox.Location = new Point(0, 0);
            comparisonSecondComboBox.Name = "comparisonSecondComboBox";
            comparisonSecondComboBox.Size = new Size(121, 33);
            comparisonSecondComboBox.TabIndex = 7;
            // 
            // pastDaysGroup
            // 
            pastDaysGroup.Controls.Add(label4);
            pastDaysGroup.Controls.Add(label3);
            pastDaysGroup.Controls.Add(pastDaysTextBox);
            pastDaysGroup.Controls.Add(pastDaysStatusComboBox);
            pastDaysGroup.Dock = DockStyle.Top;
            pastDaysGroup.Location = new Point(8, 281);
            pastDaysGroup.Name = "pastDaysGroup";
            pastDaysGroup.Size = new Size(318, 119);
            pastDaysGroup.TabIndex = 3;
            pastDaysGroup.TabStop = false;
            pastDaysGroup.Text = "4. روزهای گذشته:";
            // 
            // label4
            // 
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 0;
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 1;
            // 
            // pastDaysTextBox
            // 
            pastDaysTextBox.Location = new Point(0, 0);
            pastDaysTextBox.Name = "pastDaysTextBox";
            pastDaysTextBox.Size = new Size(100, 31);
            pastDaysTextBox.TabIndex = 2;
            // 
            // pastDaysStatusComboBox
            // 
            pastDaysStatusComboBox.Location = new Point(0, 0);
            pastDaysStatusComboBox.Name = "pastDaysStatusComboBox";
            pastDaysStatusComboBox.Size = new Size(121, 33);
            pastDaysStatusComboBox.TabIndex = 3;
            // 
            // volumeRatioGroup
            // 
            volumeRatioGroup.Controls.Add(label2);
            volumeRatioGroup.Controls.Add(label1);
            volumeRatioGroup.Controls.Add(textBox1);
            volumeRatioGroup.Controls.Add(volumeRatioTextBox);
            volumeRatioGroup.Controls.Add(volumeRatioOperatorComboBox);
            volumeRatioGroup.Dock = DockStyle.Top;
            volumeRatioGroup.Location = new Point(8, 196);
            volumeRatioGroup.Name = "volumeRatioGroup";
            volumeRatioGroup.Size = new Size(318, 85);
            volumeRatioGroup.TabIndex = 2;
            volumeRatioGroup.TabStop = false;
            volumeRatioGroup.Text = "3. نسبت حجم:";
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 31);
            textBox1.TabIndex = 2;
            // 
            // volumeRatioTextBox
            // 
            volumeRatioTextBox.Location = new Point(0, 0);
            volumeRatioTextBox.Name = "volumeRatioTextBox";
            volumeRatioTextBox.Size = new Size(100, 31);
            volumeRatioTextBox.TabIndex = 3;
            // 
            // volumeRatioOperatorComboBox
            // 
            volumeRatioOperatorComboBox.Location = new Point(0, 0);
            volumeRatioOperatorComboBox.Name = "volumeRatioOperatorComboBox";
            volumeRatioOperatorComboBox.Size = new Size(121, 33);
            volumeRatioOperatorComboBox.TabIndex = 4;
            // 
            // nameFilterGroup
            // 
            nameFilterGroup.Controls.Add(nameComboBox);
            nameFilterGroup.Controls.Add(nameTextBox);
            nameFilterGroup.Dock = DockStyle.Top;
            nameFilterGroup.Location = new Point(8, 111);
            nameFilterGroup.Name = "nameFilterGroup";
            nameFilterGroup.Size = new Size(318, 85);
            nameFilterGroup.TabIndex = 1;
            nameFilterGroup.TabStop = false;
            nameFilterGroup.Text = "2. نام نماد:";
            // 
            // nameComboBox
            // 
            nameComboBox.Location = new Point(0, 0);
            nameComboBox.Name = "nameComboBox";
            nameComboBox.Size = new Size(121, 33);
            nameComboBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(0, 0);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 31);
            nameTextBox.TabIndex = 1;
            // 
            // tradingStatusGroup
            // 
            tradingStatusGroup.Controls.Add(statusAllRadio);
            tradingStatusGroup.Controls.Add(statusPositiveRadio);
            tradingStatusGroup.Controls.Add(statusNegativeRadio);
            tradingStatusGroup.Dock = DockStyle.Top;
            tradingStatusGroup.Location = new Point(8, 8);
            tradingStatusGroup.Name = "tradingStatusGroup";
            tradingStatusGroup.Size = new Size(318, 103);
            tradingStatusGroup.TabIndex = 0;
            tradingStatusGroup.TabStop = false;
            tradingStatusGroup.Text = "1. وضعیت معامله امروز:";
            // 
            // statusAllRadio
            // 
            statusAllRadio.Location = new Point(0, 0);
            statusAllRadio.Name = "statusAllRadio";
            statusAllRadio.Size = new Size(104, 24);
            statusAllRadio.TabIndex = 0;
            // 
            // statusPositiveRadio
            // 
            statusPositiveRadio.Location = new Point(0, 0);
            statusPositiveRadio.Name = "statusPositiveRadio";
            statusPositiveRadio.Size = new Size(104, 24);
            statusPositiveRadio.TabIndex = 1;
            // 
            // statusNegativeRadio
            // 
            statusNegativeRadio.Location = new Point(0, 0);
            statusNegativeRadio.Name = "statusNegativeRadio";
            statusNegativeRadio.Size = new Size(104, 24);
            statusNegativeRadio.TabIndex = 2;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(8);
            tabPage3.Size = new Size(360, 787);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "شناسه";
            // 
            // marketsTabPage
            // 
            marketsTabPage.Location = new Point(4, 34);
            marketsTabPage.Name = "marketsTabPage";
            marketsTabPage.Padding = new Padding(8);
            marketsTabPage.Size = new Size(360, 787);
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
            chartPanel.Size = new Size(1339, 933);
            chartPanel.TabIndex = 0;
            // 
            // chartTabControl
            // 
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Location = new Point(0, 51);
            chartTabControl.Name = "chartTabControl";
            chartTabControl.SelectedIndex = 0;
            chartTabControl.Size = new Size(1337, 880);
            chartTabControl.TabIndex = 0;
            // 
            // chartTabPage
            // 
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Location = new Point(4, 34);
            chartTabPage.Name = "chartTabPage";
            chartTabPage.Padding = new Padding(3);
            chartTabPage.Size = new Size(1329, 842);
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
            chartInfoPanel.Size = new Size(1323, 30);
            chartInfoPanel.TabIndex = 0;
            // 
            // chartInfoLabel
            // 
            chartInfoLabel.Dock = DockStyle.Fill;
            chartInfoLabel.Location = new Point(0, 0);
            chartInfoLabel.Name = "chartInfoLabel";
            chartInfoLabel.Padding = new Padding(8, 0, 0, 0);
            chartInfoLabel.Size = new Size(1323, 30);
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
            chartPlaceholderLabel.Size = new Size(1323, 836);
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
            chartToolbarPanel.Size = new Size(1337, 51);
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
            // rowColumn
            // 
            rowColumn.HeaderText = "ردیف";
            rowColumn.MinimumWidth = 8;
            rowColumn.Name = "rowColumn";
            rowColumn.Width = 150;
            // 
            // symbolColumn
            // 
            symbolColumn.HeaderText = "نماد";
            symbolColumn.MinimumWidth = 8;
            symbolColumn.Name = "symbolColumn";
            symbolColumn.Width = 150;
            // 
            // lastTradeColumn
            // 
            lastTradeColumn.HeaderText = "آخرین معامله";
            lastTradeColumn.MinimumWidth = 8;
            lastTradeColumn.Name = "lastTradeColumn";
            lastTradeColumn.Width = 150;
            // 
            // selectColumn
            // 
            selectColumn.HeaderText = "انتخاب";
            selectColumn.MinimumWidth = 8;
            selectColumn.Name = "selectColumn";
            selectColumn.Width = 150;
            // 
            // identifierMainGroup
            // 
            identifierMainGroup.Controls.Add(identifierLayout);
            identifierMainGroup.Dock = DockStyle.Top;
            identifierMainGroup.Location = new Point(8, 8);
            identifierMainGroup.Name = "identifierMainGroup";
            identifierMainGroup.Size = new Size(344, 620);
            identifierMainGroup.TabIndex = 0;
            identifierMainGroup.TabStop = false;
            identifierMainGroup.Text = "اطلاعات شناسه";
            // 
            // identifierLayout
            // 
            identifierLayout.ColumnCount = 2;
            identifierLayout.Dock = DockStyle.Fill;
            identifierLayout.Location = new Point(3, 27);
            identifierLayout.Name = "identifierLayout";
            identifierLayout.RowCount = 6;
            identifierLayout.Size = new Size(338, 590);
            identifierLayout.TabIndex = 0;
            // 
            // identifierSymbolLabel
            // 
            identifierSymbolLabel.Location = new Point(0, 0);
            identifierSymbolLabel.Name = "identifierSymbolLabel";
            identifierSymbolLabel.Size = new Size(100, 23);
            identifierSymbolLabel.TabIndex = 0;
            // 
            // identifierSymbolTextBox
            // 
            identifierSymbolTextBox.Location = new Point(0, 0);
            identifierSymbolTextBox.Name = "identifierSymbolTextBox";
            identifierSymbolTextBox.Size = new Size(100, 31);
            identifierSymbolTextBox.TabIndex = 0;
            // 
            // identifierNameLabel
            // 
            identifierNameLabel.Location = new Point(0, 0);
            identifierNameLabel.Name = "identifierNameLabel";
            identifierNameLabel.Size = new Size(100, 23);
            identifierNameLabel.TabIndex = 0;
            // 
            // identifierNameTextBox
            // 
            identifierNameTextBox.Location = new Point(0, 0);
            identifierNameTextBox.Name = "identifierNameTextBox";
            identifierNameTextBox.Size = new Size(100, 31);
            identifierNameTextBox.TabIndex = 0;
            // 
            // identifierTsetmcLabel
            // 
            identifierTsetmcLabel.Location = new Point(0, 0);
            identifierTsetmcLabel.Name = "identifierTsetmcLabel";
            identifierTsetmcLabel.Size = new Size(100, 23);
            identifierTsetmcLabel.TabIndex = 0;
            // 
            // identifierTsetmcTextBox
            // 
            identifierTsetmcTextBox.Location = new Point(0, 0);
            identifierTsetmcTextBox.Name = "identifierTsetmcTextBox";
            identifierTsetmcTextBox.Size = new Size(100, 31);
            identifierTsetmcTextBox.TabIndex = 0;
            // 
            // identifierMarketLabel
            // 
            identifierMarketLabel.Location = new Point(0, 0);
            identifierMarketLabel.Name = "identifierMarketLabel";
            identifierMarketLabel.Size = new Size(100, 23);
            identifierMarketLabel.TabIndex = 0;
            // 
            // identifierMarketComboBox
            // 
            identifierMarketComboBox.Location = new Point(0, 0);
            identifierMarketComboBox.Name = "identifierMarketComboBox";
            identifierMarketComboBox.Size = new Size(121, 33);
            identifierMarketComboBox.TabIndex = 0;
            // 
            // identifierGroupLabel
            // 
            identifierGroupLabel.Location = new Point(0, 0);
            identifierGroupLabel.Name = "identifierGroupLabel";
            identifierGroupLabel.Size = new Size(100, 23);
            identifierGroupLabel.TabIndex = 0;
            // 
            // identifierGroupTextBox
            // 
            identifierGroupTextBox.Location = new Point(0, 0);
            identifierGroupTextBox.Name = "identifierGroupTextBox";
            identifierGroupTextBox.Size = new Size(100, 31);
            identifierGroupTextBox.TabIndex = 0;
            // 
            // identifierDescriptionLabel
            // 
            identifierDescriptionLabel.Location = new Point(0, 0);
            identifierDescriptionLabel.Name = "identifierDescriptionLabel";
            identifierDescriptionLabel.Size = new Size(100, 23);
            identifierDescriptionLabel.TabIndex = 0;
            // 
            // identifierDescriptionTextBox
            // 
            identifierDescriptionTextBox.Location = new Point(0, 0);
            identifierDescriptionTextBox.Name = "identifierDescriptionTextBox";
            identifierDescriptionTextBox.Size = new Size(100, 31);
            identifierDescriptionTextBox.TabIndex = 0;
            // 
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
            groupBox2.Location = new Point(3, 725);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(286, 170);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            // 
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
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).EndInit();
            tabPage2.ResumeLayout(false);
            ohlcChangeFilterGroup.ResumeLayout(false);
            ohlcChangeFilterGroup.PerformLayout();
            comparisonGroup8.ResumeLayout(false);
            comparisonGroup8.PerformLayout();
            comparisonGroup7.ResumeLayout(false);
            comparisonGroup7.PerformLayout();
            comparisonGroup6.ResumeLayout(false);
            comparisonGroup6.PerformLayout();
            comparisonGroup.ResumeLayout(false);
            comparisonGroup.PerformLayout();
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
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox2;
    }
}