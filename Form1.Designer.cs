namespace Trade.It
{
    partial class Form1
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox portfolioComboBox;
        private System.Windows.Forms.Label portfolioLabel;
        private System.Windows.Forms.DataGridView stocksDataGridView;
        private System.Windows.Forms.FlowLayoutPanel stocksBottomPanel;
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
            mainSplitContainer = new SplitContainer();
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
            controlTabControl = new TabControl();
            stocksTabPage = new TabPage();
            refreshButtonPortfolio = new Button();
            stocksDataGridView = new DataGridView();
            rowColumn = new DataGridViewTextBoxColumn();
            symbolColumn = new DataGridViewTextBoxColumn();
            lastTradeColumn = new DataGridViewTextBoxColumn();
            selectColumn = new DataGridViewCheckBoxColumn();
            stocksBottomPanel = new FlowLayoutPanel();
            refreshButton = new Button();
            navigationSpeedTextBox = new TextBox();
            speedLabel = new Label();
            navigationButton = new Button();
            newPortfolioButton = new Button();
            deleteButton = new Button();
            selectNoneCheckBox = new CheckBox();
            selectAllCheckBox = new CheckBox();
            portfolioComboBox = new ComboBox();
            portfolioLabel = new Label();
            tabPage2 = new TabPage();
            comparisonGroup = new GroupBox();
            comparisonFirstComboBox = new ComboBox();
            comparisonOperatorComboBox = new ComboBox();
            comparisonSecondComboBox = new ComboBox();
            pastDaysGroup = new GroupBox();
            pastDaysStatusComboBox = new ComboBox();
            pastDaysTextBox = new TextBox();
            volumeRatioGroup = new GroupBox();
            volumeRatioOperatorComboBox = new ComboBox();
            volumeRatioTextBox = new TextBox();
            nameFilterGroup = new GroupBox();
            tradingStatusGroup = new GroupBox();
            statusAllRadio = new RadioButton();
            statusPositiveRadio = new RadioButton();
            statusNegativeRadio = new RadioButton();
            tabPage3 = new TabPage();
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
            nameComboBox = new ComboBox();
            nameTextBox = new TextBox();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            comparisonFirstTextBox = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            comparisonSecondTextBox = new TextBox();
            mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            chartPanel.SuspendLayout();
            chartTabControl.SuspendLayout();
            chartTabPage.SuspendLayout();
            chartInfoPanel.SuspendLayout();
            chartToolbarPanel.SuspendLayout();
            controlTabControl.SuspendLayout();
            stocksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).BeginInit();
            stocksBottomPanel.SuspendLayout();
            tabPage2.SuspendLayout();
            comparisonGroup.SuspendLayout();
            pastDaysGroup.SuspendLayout();
            volumeRatioGroup.SuspendLayout();
            nameFilterGroup.SuspendLayout();
            tradingStatusGroup.SuspendLayout();
            tabPage3.SuspendLayout();
            identifierMainGroup.SuspendLayout();
            identifierLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.RightToLeft = RightToLeft.No;
            mainMenuStrip.Size = new Size(1288, 36);
            mainMenuStrip.TabIndex = 0;
            // 
            // portfolioDefinitionMenuItem
            // 
            portfolioDefinitionMenuItem.Name = "portfolioDefinitionMenuItem";
            portfolioDefinitionMenuItem.Size = new Size(121, 32);
            portfolioDefinitionMenuItem.Text = "تعریف سبد";
            // 
            // portfolioManagementMenuItem
            // 
            portfolioManagementMenuItem.Name = "portfolioManagementMenuItem";
            portfolioManagementMenuItem.Size = new Size(129, 32);
            portfolioManagementMenuItem.Text = "مدیریت سبد";
            // 
            // settingsMenuItem
            // 
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new Size(99, 32);
            settingsMenuItem.Text = "تنظیمات";
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 36);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(chartPanel);
            mainSplitContainer.Panel1MinSize = 500;
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(controlTabControl);
            mainSplitContainer.Panel2MinSize = 300;
            mainSplitContainer.Size = new Size(1288, 744);
            mainSplitContainer.SplitterDistance = 869;
            mainSplitContainer.TabIndex = 1;
            // 
            // chartPanel
            // 
            chartPanel.BorderStyle = BorderStyle.FixedSingle;
            chartPanel.Controls.Add(chartTabControl);
            chartPanel.Controls.Add(chartToolbarPanel);
            chartPanel.Dock = DockStyle.Fill;
            chartPanel.Location = new Point(0, 0);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(869, 744);
            chartPanel.TabIndex = 0;
            // 
            // chartTabControl
            // 
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Location = new Point(0, 51);
            chartTabControl.Name = "chartTabControl";
            chartTabControl.SelectedIndex = 0;
            chartTabControl.Size = new Size(867, 691);
            chartTabControl.TabIndex = 0;
            // 
            // chartTabPage
            // 
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Location = new Point(4, 34);
            chartTabPage.Name = "chartTabPage";
            chartTabPage.Padding = new Padding(3);
            chartTabPage.Size = new Size(859, 653);
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
            chartInfoPanel.Size = new Size(853, 30);
            chartInfoPanel.TabIndex = 0;
            // 
            // chartInfoLabel
            // 
            chartInfoLabel.Dock = DockStyle.Fill;
            chartInfoLabel.Location = new Point(0, 0);
            chartInfoLabel.Name = "chartInfoLabel";
            chartInfoLabel.Padding = new Padding(8, 0, 0, 0);
            chartInfoLabel.Size = new Size(853, 30);
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
            chartPlaceholderLabel.Size = new Size(853, 647);
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
            chartToolbarPanel.Size = new Size(867, 51);
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
            zoomInButton.Size = new Size(53, 34);
            zoomInButton.TabIndex = 3;
            zoomInButton.Text = "+";
            // 
            // zoomOutButton
            // 
            zoomOutButton.Location = new Point(265, 7);
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(53, 34);
            zoomOutButton.TabIndex = 4;
            zoomOutButton.Text = "−";
            // 
            // resetChartButton
            // 
            resetChartButton.Location = new Point(320, 7);
            resetChartButton.Name = "resetChartButton";
            resetChartButton.Size = new Size(66, 34);
            resetChartButton.TabIndex = 5;
            resetChartButton.Text = "Reset";
            // 
            // hideChartButton
            // 
            hideChartButton.Location = new Point(389, 7);
            hideChartButton.Name = "hideChartButton";
            hideChartButton.Size = new Size(115, 34);
            hideChartButton.TabIndex = 6;
            hideChartButton.Text = "مخفی چارت";
            // 
            // hideToolsButton
            // 
            hideToolsButton.Location = new Point(507, 7);
            hideToolsButton.Name = "hideToolsButton";
            hideToolsButton.Size = new Size(104, 34);
            hideToolsButton.TabIndex = 7;
            hideToolsButton.Text = "مخفی ابزار";
            // 
            // printChartButton
            // 
            printChartButton.Location = new Point(617, 7);
            printChartButton.Name = "printChartButton";
            printChartButton.Size = new Size(62, 34);
            printChartButton.TabIndex = 8;
            printChartButton.Text = "چاپ";
            // 
            // snapshotChartButton
            // 
            snapshotChartButton.Location = new Point(685, 7);
            snapshotChartButton.Name = "snapshotChartButton";
            snapshotChartButton.Size = new Size(62, 34);
            snapshotChartButton.TabIndex = 9;
            snapshotChartButton.Text = "عکس";
            // 
            // fullScreenChartButton
            // 
            fullScreenChartButton.Location = new Point(753, 7);
            fullScreenChartButton.Name = "fullScreenChartButton";
            fullScreenChartButton.Size = new Size(103, 34);
            fullScreenChartButton.TabIndex = 10;
            fullScreenChartButton.Text = "تمام‌صفحه";
            // 
            // controlTabControl
            // 
            controlTabControl.Controls.Add(stocksTabPage);
            controlTabControl.Controls.Add(tabPage2);
            controlTabControl.Controls.Add(tabPage3);
            controlTabControl.Dock = DockStyle.Fill;
            controlTabControl.Location = new Point(0, 0);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.RightToLeft = RightToLeft.Yes;
            controlTabControl.RightToLeftLayout = true;
            controlTabControl.SelectedIndex = 0;
            controlTabControl.Size = new Size(415, 744);
            controlTabControl.TabIndex = 0;
            // 
            // stocksTabPage
            // 
            stocksTabPage.Controls.Add(refreshButtonPortfolio);
            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(stocksBottomPanel);
            stocksTabPage.Controls.Add(portfolioComboBox);
            stocksTabPage.Controls.Add(portfolioLabel);
            stocksTabPage.Location = new Point(4, 34);
            stocksTabPage.Name = "stocksTabPage";
            stocksTabPage.Padding = new Padding(8);
            stocksTabPage.Size = new Size(407, 706);
            stocksTabPage.TabIndex = 0;
            stocksTabPage.Text = "سهام";
            // 
            // refreshButtonPortfolio
            // 
            refreshButtonPortfolio.Location = new Point(15, 7);
            refreshButtonPortfolio.Name = "refreshButtonPortfolio";
            refreshButtonPortfolio.Size = new Size(91, 34);
            refreshButtonPortfolio.TabIndex = 4;
            refreshButtonPortfolio.Text = "تازه‌سازی";
            // 
            // stocksDataGridView
            // 
            stocksDataGridView.AllowUserToAddRows = false;
            stocksDataGridView.AllowUserToDeleteRows = false;
            stocksDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            stocksDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stocksDataGridView.Columns.AddRange(new DataGridViewColumn[] { rowColumn, symbolColumn, lastTradeColumn, selectColumn });
            stocksDataGridView.Location = new Point(8, 47);
            stocksDataGridView.Name = "stocksDataGridView";
            stocksDataGridView.RightToLeft = RightToLeft.Yes;
            stocksDataGridView.RowHeadersVisible = false;
            stocksDataGridView.RowHeadersWidth = 51;
            stocksDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            stocksDataGridView.Size = new Size(391, 548);
            stocksDataGridView.TabIndex = 0;
            // 
            // rowColumn
            // 
            rowColumn.FillWeight = 121.334824F;
            rowColumn.HeaderText = "ردیف";
            rowColumn.MinimumWidth = 6;
            rowColumn.Name = "rowColumn";
            // 
            // symbolColumn
            // 
            symbolColumn.FillWeight = 149.73262F;
            symbolColumn.HeaderText = "نماد";
            symbolColumn.MinimumWidth = 6;
            symbolColumn.Name = "symbolColumn";
            // 
            // lastTradeColumn
            // 
            lastTradeColumn.FillWeight = 87.110054F;
            lastTradeColumn.HeaderText = "آخرین معامله";
            lastTradeColumn.MinimumWidth = 6;
            lastTradeColumn.Name = "lastTradeColumn";
            // 
            // selectColumn
            // 
            selectColumn.FalseValue = false;
            selectColumn.FillWeight = 41.8225136F;
            selectColumn.HeaderText = "انتخاب";
            selectColumn.MinimumWidth = 6;
            selectColumn.Name = "selectColumn";
            selectColumn.TrueValue = true;
            // 
            // stocksBottomPanel
            // 
            stocksBottomPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            stocksBottomPanel.Controls.Add(refreshButton);
            stocksBottomPanel.Controls.Add(navigationSpeedTextBox);
            stocksBottomPanel.Controls.Add(speedLabel);
            stocksBottomPanel.Controls.Add(navigationButton);
            stocksBottomPanel.Controls.Add(newPortfolioButton);
            stocksBottomPanel.Controls.Add(deleteButton);
            stocksBottomPanel.Controls.Add(selectNoneCheckBox);
            stocksBottomPanel.Controls.Add(selectAllCheckBox);
            stocksBottomPanel.FlowDirection = FlowDirection.RightToLeft;
            stocksBottomPanel.Location = new Point(8, 601);
            stocksBottomPanel.Name = "stocksBottomPanel";
            stocksBottomPanel.Padding = new Padding(4);
            stocksBottomPanel.Size = new Size(391, 97);
            stocksBottomPanel.TabIndex = 1;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(7, 7);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(91, 34);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "تازه‌سازی";
            // 
            // navigationSpeedTextBox
            // 
            navigationSpeedTextBox.Location = new Point(104, 10);
            navigationSpeedTextBox.Margin = new Padding(2, 6, 3, 3);
            navigationSpeedTextBox.Name = "navigationSpeedTextBox";
            navigationSpeedTextBox.Size = new Size(62, 31);
            navigationSpeedTextBox.TabIndex = 7;
            navigationSpeedTextBox.Text = "1000";
            // 
            // speedLabel
            // 
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(170, 13);
            speedLabel.Margin = new Padding(7, 9, 2, 3);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(64, 25);
            speedLabel.TabIndex = 6;
            speedLabel.Text = "سرعت:";
            // 
            // navigationButton
            // 
            navigationButton.Location = new Point(244, 7);
            navigationButton.Name = "navigationButton";
            navigationButton.Size = new Size(81, 34);
            navigationButton.TabIndex = 0;
            navigationButton.Text = "پیمایش";
            // 
            // newPortfolioButton
            // 
            newPortfolioButton.Location = new Point(7, 47);
            newPortfolioButton.Name = "newPortfolioButton";
            newPortfolioButton.Size = new Size(91, 35);
            newPortfolioButton.TabIndex = 1;
            newPortfolioButton.Text = "سبد جدید";
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(104, 47);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(92, 35);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "حذف";
            // 
            // selectNoneCheckBox
            // 
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new Point(202, 53);
            selectNoneCheckBox.Margin = new Padding(7, 9, 3, 3);
            selectNoneCheckBox.Name = "selectNoneCheckBox";
            selectNoneCheckBox.Size = new Size(64, 29);
            selectNoneCheckBox.TabIndex = 5;
            selectNoneCheckBox.Text = "هیچ";
            // 
            // selectAllCheckBox
            // 
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(276, 53);
            selectAllCheckBox.Margin = new Padding(7, 9, 3, 3);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(66, 29);
            selectAllCheckBox.TabIndex = 4;
            selectAllCheckBox.Text = "همه";
            // 
            // portfolioComboBox
            // 
            portfolioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new Point(108, 8);
            portfolioComboBox.Name = "portfolioComboBox";
            portfolioComboBox.Size = new Size(287, 33);
            portfolioComboBox.TabIndex = 2;
            // 
            // portfolioLabel
            // 
            portfolioLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new Point(497, 12);
            portfolioLabel.Name = "portfolioLabel";
            portfolioLabel.Size = new Size(46, 25);
            portfolioLabel.TabIndex = 3;
            portfolioLabel.Text = "سبد:";
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(comparisonGroup);
            tabPage2.Controls.Add(pastDaysGroup);
            tabPage2.Controls.Add(volumeRatioGroup);
            tabPage2.Controls.Add(nameFilterGroup);
            tabPage2.Controls.Add(tradingStatusGroup);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(8);
            tabPage2.Size = new Size(407, 706);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "فیلترها";
            // 
            // comparisonGroup
            // 
            comparisonGroup.Controls.Add(label8);
            comparisonGroup.Controls.Add(comparisonSecondTextBox);
            comparisonGroup.Controls.Add(label7);
            comparisonGroup.Controls.Add(label6);
            comparisonGroup.Controls.Add(comparisonFirstTextBox);
            comparisonGroup.Controls.Add(label5);
            comparisonGroup.Controls.Add(comparisonFirstComboBox);
            comparisonGroup.Controls.Add(comparisonOperatorComboBox);
            comparisonGroup.Controls.Add(comparisonSecondComboBox);
            comparisonGroup.Location = new Point(11, 388);
            comparisonGroup.Name = "comparisonGroup";
            comparisonGroup.Size = new Size(388, 133);
            comparisonGroup.TabIndex = 0;
            comparisonGroup.TabStop = false;
            comparisonGroup.Text = "5. مقایسه قیمت:";
            // 
            // comparisonFirstComboBox
            // 
            comparisonFirstComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox.Location = new Point(260, 36);
            comparisonFirstComboBox.Name = "comparisonFirstComboBox";
            comparisonFirstComboBox.Size = new Size(59, 33);
            comparisonFirstComboBox.TabIndex = 1;
            // 
            // comparisonOperatorComboBox
            // 
            comparisonOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox.Location = new Point(11, 44);
            comparisonOperatorComboBox.Name = "comparisonOperatorComboBox";
            comparisonOperatorComboBox.Size = new Size(73, 33);
            comparisonOperatorComboBox.TabIndex = 2;
            // 
            // comparisonSecondComboBox
            // 
            comparisonSecondComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox.Location = new Point(197, 89);
            comparisonSecondComboBox.Name = "comparisonSecondComboBox";
            comparisonSecondComboBox.Size = new Size(61, 33);
            comparisonSecondComboBox.TabIndex = 3;
            // 
            // pastDaysGroup
            // 
            pastDaysGroup.Controls.Add(label4);
            pastDaysGroup.Controls.Add(label3);
            pastDaysGroup.Controls.Add(pastDaysTextBox);
            pastDaysGroup.Controls.Add(pastDaysStatusComboBox);
            pastDaysGroup.Location = new Point(8, 280);
            pastDaysGroup.Name = "pastDaysGroup";
            pastDaysGroup.Size = new Size(391, 102);
            pastDaysGroup.TabIndex = 1;
            pastDaysGroup.TabStop = false;
            pastDaysGroup.Text = "۴ ـ وضعیت معامله در روزهای گذشته:";
            // 
            // pastDaysStatusComboBox
            // 
            pastDaysStatusComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.Items.AddRange(new object[] { "معامله داشته‌", "معامله نداشته‌" });
            pastDaysStatusComboBox.Location = new Point(40, 30);
            pastDaysStatusComboBox.Name = "pastDaysStatusComboBox";
            pastDaysStatusComboBox.Size = new Size(149, 33);
            pastDaysStatusComboBox.TabIndex = 0;
            pastDaysStatusComboBox.SelectedIndexChanged += pastDaysStatusComboBox_SelectedIndexChanged;
            // 
            // pastDaysTextBox
            // 
            pastDaysTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pastDaysTextBox.Location = new Point(280, 32);
            pastDaysTextBox.Name = "pastDaysTextBox";
            pastDaysTextBox.Size = new Size(57, 31);
            pastDaysTextBox.TabIndex = 1;
            pastDaysTextBox.Text = "5";
            pastDaysTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // volumeRatioGroup
            // 
            volumeRatioGroup.Controls.Add(label2);
            volumeRatioGroup.Controls.Add(label1);
            volumeRatioGroup.Controls.Add(textBox1);
            volumeRatioGroup.Controls.Add(volumeRatioTextBox);
            volumeRatioGroup.Controls.Add(volumeRatioOperatorComboBox);
            volumeRatioGroup.Dock = DockStyle.Top;
            volumeRatioGroup.Location = new Point(8, 170);
            volumeRatioGroup.Name = "volumeRatioGroup";
            volumeRatioGroup.Size = new Size(391, 102);
            volumeRatioGroup.TabIndex = 2;
            volumeRatioGroup.TabStop = false;
            volumeRatioGroup.Text = "3. سهامی که نسبت حجم آخرین کندل به:";
            // 
            // volumeRatioOperatorComboBox
            // 
            volumeRatioOperatorComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.Items.AddRange(new object[] { "بزرگتر از", "مساوی", "کوچکتر از", "بزرگتر یا مساوی", "کوچکتر یا مساوی" });
            volumeRatioOperatorComboBox.Location = new Point(181, 63);
            volumeRatioOperatorComboBox.Name = "volumeRatioOperatorComboBox";
            volumeRatioOperatorComboBox.Size = new Size(119, 33);
            volumeRatioOperatorComboBox.TabIndex = 0;
            // 
            // volumeRatioTextBox
            // 
            volumeRatioTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeRatioTextBox.Location = new Point(120, 63);
            volumeRatioTextBox.Name = "volumeRatioTextBox";
            volumeRatioTextBox.Size = new Size(55, 31);
            volumeRatioTextBox.TabIndex = 1;
            volumeRatioTextBox.Text = "1";
            volumeRatioTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // nameFilterGroup
            // 
            nameFilterGroup.Controls.Add(nameComboBox);
            nameFilterGroup.Controls.Add(nameTextBox);
            nameFilterGroup.Dock = DockStyle.Top;
            nameFilterGroup.Location = new Point(8, 85);
            nameFilterGroup.Name = "nameFilterGroup";
            nameFilterGroup.Size = new Size(391, 85);
            nameFilterGroup.TabIndex = 3;
            nameFilterGroup.TabStop = false;
            nameFilterGroup.Text = "2. نمایش سهامی که عبارت:";
            // 
            // tradingStatusGroup
            // 
            tradingStatusGroup.Controls.Add(statusAllRadio);
            tradingStatusGroup.Controls.Add(statusPositiveRadio);
            tradingStatusGroup.Controls.Add(statusNegativeRadio);
            tradingStatusGroup.Dock = DockStyle.Top;
            tradingStatusGroup.Location = new Point(8, 8);
            tradingStatusGroup.Name = "tradingStatusGroup";
            tradingStatusGroup.Size = new Size(391, 77);
            tradingStatusGroup.TabIndex = 4;
            tradingStatusGroup.TabStop = false;
            tradingStatusGroup.Text = "1. نمایش سهامی که امروز معامله:";
            // 
            // statusAllRadio
            // 
            statusAllRadio.AutoSize = true;
            statusAllRadio.Checked = true;
            statusAllRadio.Location = new Point(62, 33);
            statusAllRadio.Margin = new Padding(6);
            statusAllRadio.Name = "statusAllRadio";
            statusAllRadio.Size = new Size(65, 29);
            statusAllRadio.TabIndex = 0;
            statusAllRadio.TabStop = true;
            statusAllRadio.Text = "همه";
            // 
            // statusPositiveRadio
            // 
            statusPositiveRadio.AutoSize = true;
            statusPositiveRadio.Location = new Point(139, 33);
            statusPositiveRadio.Margin = new Padding(6);
            statusPositiveRadio.Name = "statusPositiveRadio";
            statusPositiveRadio.Size = new Size(77, 29);
            statusPositiveRadio.TabIndex = 1;
            statusPositiveRadio.Text = "داشته";
            // 
            // statusNegativeRadio
            // 
            statusNegativeRadio.AutoSize = true;
            statusNegativeRadio.Location = new Point(228, 33);
            statusNegativeRadio.Margin = new Padding(6);
            statusNegativeRadio.Name = "statusNegativeRadio";
            statusNegativeRadio.Size = new Size(83, 29);
            statusNegativeRadio.TabIndex = 2;
            statusNegativeRadio.Text = "نداشته‌";
            // 
            // tabPage3
            // 
            tabPage3.AutoScroll = true;
            tabPage3.Controls.Add(identifierMainGroup);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(8);
            tabPage3.Size = new Size(407, 706);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "شناسه";
            // 
            // identifierMainGroup
            // 
            identifierMainGroup.Controls.Add(identifierLayout);
            identifierMainGroup.Dock = DockStyle.Top;
            identifierMainGroup.Location = new Point(8, 8);
            identifierMainGroup.Name = "identifierMainGroup";
            identifierMainGroup.Size = new Size(391, 330);
            identifierMainGroup.TabIndex = 0;
            identifierMainGroup.TabStop = false;
            identifierMainGroup.Text = "اطلاعات شناسه";
            // 
            // identifierLayout
            // 
            identifierLayout.ColumnCount = 2;
            identifierLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            identifierLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            identifierLayout.Controls.Add(identifierSymbolLabel, 0, 0);
            identifierLayout.Controls.Add(identifierSymbolTextBox, 1, 0);
            identifierLayout.Controls.Add(identifierNameLabel, 0, 1);
            identifierLayout.Controls.Add(identifierNameTextBox, 1, 1);
            identifierLayout.Controls.Add(identifierTsetmcLabel, 0, 2);
            identifierLayout.Controls.Add(identifierTsetmcTextBox, 1, 2);
            identifierLayout.Controls.Add(identifierMarketLabel, 0, 3);
            identifierLayout.Controls.Add(identifierMarketComboBox, 1, 3);
            identifierLayout.Controls.Add(identifierGroupLabel, 0, 4);
            identifierLayout.Controls.Add(identifierGroupTextBox, 1, 4);
            identifierLayout.Controls.Add(identifierDescriptionLabel, 0, 5);
            identifierLayout.Controls.Add(identifierDescriptionTextBox, 1, 5);
            identifierLayout.Dock = DockStyle.Fill;
            identifierLayout.Location = new Point(3, 27);
            identifierLayout.Name = "identifierLayout";
            identifierLayout.Padding = new Padding(8);
            identifierLayout.RowCount = 6;
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            identifierLayout.Size = new Size(385, 300);
            identifierLayout.TabIndex = 0;
            // 
            // identifierSymbolLabel
            // 
            identifierSymbolLabel.Dock = DockStyle.Fill;
            identifierSymbolLabel.Location = new Point(275, 8);
            identifierSymbolLabel.Name = "identifierSymbolLabel";
            identifierSymbolLabel.Size = new Size(99, 20);
            identifierSymbolLabel.TabIndex = 0;
            identifierSymbolLabel.Text = "نماد:";
            identifierSymbolLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierSymbolTextBox
            // 
            identifierSymbolTextBox.Dock = DockStyle.Fill;
            identifierSymbolTextBox.Location = new Point(11, 11);
            identifierSymbolTextBox.Name = "identifierSymbolTextBox";
            identifierSymbolTextBox.Size = new Size(258, 31);
            identifierSymbolTextBox.TabIndex = 1;
            // 
            // identifierNameLabel
            // 
            identifierNameLabel.Dock = DockStyle.Fill;
            identifierNameLabel.Location = new Point(275, 28);
            identifierNameLabel.Name = "identifierNameLabel";
            identifierNameLabel.Size = new Size(99, 20);
            identifierNameLabel.TabIndex = 2;
            identifierNameLabel.Text = "نام شرکت:";
            identifierNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierNameTextBox
            // 
            identifierNameTextBox.Dock = DockStyle.Fill;
            identifierNameTextBox.Location = new Point(11, 31);
            identifierNameTextBox.Name = "identifierNameTextBox";
            identifierNameTextBox.Size = new Size(258, 31);
            identifierNameTextBox.TabIndex = 3;
            // 
            // identifierTsetmcLabel
            // 
            identifierTsetmcLabel.Dock = DockStyle.Fill;
            identifierTsetmcLabel.Location = new Point(275, 48);
            identifierTsetmcLabel.Name = "identifierTsetmcLabel";
            identifierTsetmcLabel.Size = new Size(99, 20);
            identifierTsetmcLabel.TabIndex = 4;
            identifierTsetmcLabel.Text = "کد TSETMC:";
            identifierTsetmcLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierTsetmcTextBox
            // 
            identifierTsetmcTextBox.Dock = DockStyle.Fill;
            identifierTsetmcTextBox.Location = new Point(11, 51);
            identifierTsetmcTextBox.Name = "identifierTsetmcTextBox";
            identifierTsetmcTextBox.Size = new Size(258, 31);
            identifierTsetmcTextBox.TabIndex = 5;
            // 
            // identifierMarketLabel
            // 
            identifierMarketLabel.Dock = DockStyle.Fill;
            identifierMarketLabel.Location = new Point(275, 68);
            identifierMarketLabel.Name = "identifierMarketLabel";
            identifierMarketLabel.Size = new Size(99, 20);
            identifierMarketLabel.TabIndex = 6;
            identifierMarketLabel.Text = "بازار:";
            identifierMarketLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierMarketComboBox
            // 
            identifierMarketComboBox.Dock = DockStyle.Fill;
            identifierMarketComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            identifierMarketComboBox.Items.AddRange(new object[] { "بورس", "فرابورس", "بورس کالا", "سایر" });
            identifierMarketComboBox.Location = new Point(11, 71);
            identifierMarketComboBox.Name = "identifierMarketComboBox";
            identifierMarketComboBox.Size = new Size(258, 33);
            identifierMarketComboBox.TabIndex = 7;
            // 
            // identifierGroupLabel
            // 
            identifierGroupLabel.Dock = DockStyle.Fill;
            identifierGroupLabel.Location = new Point(275, 88);
            identifierGroupLabel.Name = "identifierGroupLabel";
            identifierGroupLabel.Size = new Size(99, 20);
            identifierGroupLabel.TabIndex = 8;
            identifierGroupLabel.Text = "گروه صنعت:";
            identifierGroupLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierGroupTextBox
            // 
            identifierGroupTextBox.Dock = DockStyle.Fill;
            identifierGroupTextBox.Location = new Point(11, 91);
            identifierGroupTextBox.Name = "identifierGroupTextBox";
            identifierGroupTextBox.Size = new Size(258, 31);
            identifierGroupTextBox.TabIndex = 9;
            // 
            // identifierDescriptionLabel
            // 
            identifierDescriptionLabel.Dock = DockStyle.Fill;
            identifierDescriptionLabel.Location = new Point(275, 108);
            identifierDescriptionLabel.Name = "identifierDescriptionLabel";
            identifierDescriptionLabel.Size = new Size(99, 184);
            identifierDescriptionLabel.TabIndex = 10;
            identifierDescriptionLabel.Text = "توضیحات:";
            identifierDescriptionLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierDescriptionTextBox
            // 
            identifierDescriptionTextBox.Dock = DockStyle.Fill;
            identifierDescriptionTextBox.Location = new Point(11, 111);
            identifierDescriptionTextBox.Multiline = true;
            identifierDescriptionTextBox.Name = "identifierDescriptionTextBox";
            identifierDescriptionTextBox.Size = new Size(258, 178);
            identifierDescriptionTextBox.TabIndex = 11;
            // 
            // nameComboBox
            // 
            nameComboBox.FormattingEnabled = true;
            nameComboBox.Items.AddRange(new object[] { "هر جای نام باشد", "در ابتدای نام باشد", "در انتهای نام باشد", "در میانه نام باشد", "در نام نباشد" });
            nameComboBox.Location = new Point(14, 41);
            nameComboBox.Name = "nameComboBox";
            nameComboBox.Size = new Size(238, 33);
            nameComboBox.TabIndex = 0;
            nameComboBox.Text = "هر جای نام باشد";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(258, 42);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(119, 31);
            nameTextBox.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(228, 29);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(59, 31);
            textBox1.TabIndex = 2;
            textBox1.Text = "1";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(117, 33);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 3;
            label1.Text = "کندل گذشته";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 66);
            label2.Name = "label2";
            label2.Size = new Size(47, 25);
            label2.TabIndex = 4;
            label2.Text = "باشد";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(343, 35);
            label3.Name = "label3";
            label3.Size = new Size(29, 25);
            label3.TabIndex = 5;
            label3.Text = "در";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(186, 35);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 6;
            label4.Text = "روز گذشته";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(319, 36);
            label5.Name = "label5";
            label5.Size = new Size(55, 25);
            label5.TabIndex = 5;
            label5.Text = "قیمت";
            // 
            // comparisonFirstTextBox
            // 
            comparisonFirstTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonFirstTextBox.Location = new Point(197, 38);
            comparisonFirstTextBox.Name = "comparisonFirstTextBox";
            comparisonFirstTextBox.Size = new Size(57, 31);
            comparisonFirstTextBox.TabIndex = 6;
            comparisonFirstTextBox.Text = "5";
            comparisonFirstTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(90, 44);
            label6.Name = "label6";
            label6.Size = new Size(101, 25);
            label6.TabIndex = 7;
            label6.Text = "کندل گذشته";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(264, 89);
            label7.Name = "label7";
            label7.Size = new Size(55, 25);
            label7.TabIndex = 8;
            label7.Text = "قیمت";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(27, 94);
            label8.Name = "label8";
            label8.Size = new Size(101, 25);
            label8.TabIndex = 10;
            label8.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox
            // 
            comparisonSecondTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonSecondTextBox.Location = new Point(134, 91);
            comparisonSecondTextBox.Name = "comparisonSecondTextBox";
            comparisonSecondTextBox.Size = new Size(57, 31);
            comparisonSecondTextBox.TabIndex = 9;
            comparisonSecondTextBox.Text = "5";
            comparisonSecondTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Form1
            // 
            ClientSize = new Size(1288, 780);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new Size(900, 600);
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trade.It";
            WindowState = FormWindowState.Maximized;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            chartPanel.ResumeLayout(false);
            chartTabControl.ResumeLayout(false);
            chartTabPage.ResumeLayout(false);
            chartInfoPanel.ResumeLayout(false);
            chartToolbarPanel.ResumeLayout(false);
            controlTabControl.ResumeLayout(false);
            stocksTabPage.ResumeLayout(false);
            stocksTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).EndInit();
            stocksBottomPanel.ResumeLayout(false);
            stocksBottomPanel.PerformLayout();
            tabPage2.ResumeLayout(false);
            comparisonGroup.ResumeLayout(false);
            comparisonGroup.PerformLayout();
            pastDaysGroup.ResumeLayout(false);
            pastDaysGroup.PerformLayout();
            volumeRatioGroup.ResumeLayout(false);
            volumeRatioGroup.PerformLayout();
            nameFilterGroup.ResumeLayout(false);
            nameFilterGroup.PerformLayout();
            tradingStatusGroup.ResumeLayout(false);
            tradingStatusGroup.PerformLayout();
            tabPage3.ResumeLayout(false);
            identifierMainGroup.ResumeLayout(false);
            identifierLayout.ResumeLayout(false);
            identifierLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox nameComboBox;
        private TextBox nameTextBox;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private Label label4;
        private Label label3;
        private Label label8;
        private TextBox comparisonSecondTextBox;
        private Label label7;
        private Label label6;
        private TextBox comparisonFirstTextBox;
        private Label label5;
    }
}
