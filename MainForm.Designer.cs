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
        private System.Windows.Forms.TabPage tabPage3;
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
            groupBox1 = new GroupBox();
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
            tabPage2.SuspendLayout();
            comparisonGroup8.SuspendLayout();
            comparisonGroup7.SuspendLayout();
            comparisonGroup6.SuspendLayout();
            comparisonGroup.SuspendLayout();
            pastDaysGroup.SuspendLayout();
            volumeRatioGroup.SuspendLayout();
            nameFilterGroup.SuspendLayout();
            tradingStatusGroup.SuspendLayout();
            tabPage3.SuspendLayout();
            identifierMainGroup.SuspendLayout();
            identifierLayout.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.RightToLeft = RightToLeft.No;
            mainMenuStrip.Size = new Size(1452, 33);
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
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 33);
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
            mainSplitContainer.Size = new Size(1452, 825);
            mainSplitContainer.SplitterDistance = 1084;
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
            chartPanel.Size = new Size(1084, 825);
            chartPanel.TabIndex = 0;
            // 
            // chartTabControl
            // 
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Location = new Point(0, 51);
            chartTabControl.Name = "chartTabControl";
            chartTabControl.SelectedIndex = 0;
            chartTabControl.Size = new Size(1082, 772);
            chartTabControl.TabIndex = 0;
            // 
            // chartTabPage
            // 
            chartTabPage.Controls.Add(chartInfoPanel);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Location = new Point(4, 34);
            chartTabPage.Name = "chartTabPage";
            chartTabPage.Padding = new Padding(3);
            chartTabPage.Size = new Size(1074, 734);
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
            chartInfoPanel.Size = new Size(1068, 30);
            chartInfoPanel.TabIndex = 0;
            // 
            // chartInfoLabel
            // 
            chartInfoLabel.Dock = DockStyle.Fill;
            chartInfoLabel.Location = new Point(0, 0);
            chartInfoLabel.Name = "chartInfoLabel";
            chartInfoLabel.Padding = new Padding(8, 0, 0, 0);
            chartInfoLabel.Size = new Size(1068, 30);
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
            chartPlaceholderLabel.Size = new Size(1068, 728);
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
            chartToolbarPanel.Size = new Size(1082, 51);
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
            controlTabControl.Location = new Point(0, 0);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.RightToLeft = RightToLeft.Yes;
            controlTabControl.RightToLeftLayout = true;
            controlTabControl.SelectedIndex = 0;
            controlTabControl.Size = new Size(364, 825);
            controlTabControl.TabIndex = 0;
            // 
            // stocksTabPage
            // 
            stocksTabPage.Controls.Add(groupBox1);
            stocksTabPage.Controls.Add(refreshButtonPortfolio);
            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(portfolioComboBox);
            stocksTabPage.Controls.Add(portfolioLabel);
            stocksTabPage.Location = new Point(4, 34);
            stocksTabPage.Name = "stocksTabPage";
            stocksTabPage.Padding = new Padding(8);
            stocksTabPage.Size = new Size(356, 787);
            stocksTabPage.TabIndex = 0;
            stocksTabPage.Text = "سهام";
            // 
            // refreshButtonPortfolio
            // 
            refreshButtonPortfolio.Location = new Point(10, 7);
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
            stocksDataGridView.Size = new Size(340, 579);
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
            // refreshButton
            // 
            refreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            refreshButton.Location = new Point(112, 64);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(91, 34);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "تازه‌سازی";
            // 
            // navigationSpeedTextBox
            // 
            navigationSpeedTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navigationSpeedTextBox.Location = new Point(14, 27);
            navigationSpeedTextBox.Margin = new Padding(2, 6, 3, 3);
            navigationSpeedTextBox.Name = "navigationSpeedTextBox";
            navigationSpeedTextBox.Size = new Size(88, 31);
            navigationSpeedTextBox.TabIndex = 7;
            navigationSpeedTextBox.Text = "1000";
            navigationSpeedTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // speedLabel
            // 
            speedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(112, 27);
            speedLabel.Margin = new Padding(7, 9, 2, 3);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(93, 25);
            speedLabel.TabIndex = 6;
            speedLabel.Text = ">> سرعت:";
            // 
            // navigationButton
            // 
            navigationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            navigationButton.Location = new Point(209, 25);
            navigationButton.Name = "navigationButton";
            navigationButton.Size = new Size(91, 34);
            navigationButton.TabIndex = 0;
            navigationButton.Text = "پیمایش";
            // 
            // newPortfolioButton
            // 
            newPortfolioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            newPortfolioButton.Location = new Point(209, 64);
            newPortfolioButton.Name = "newPortfolioButton";
            newPortfolioButton.Size = new Size(91, 35);
            newPortfolioButton.TabIndex = 1;
            newPortfolioButton.Text = "سبد جدید";
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteButton.Location = new Point(14, 64);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(92, 35);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "حذف";
            // 
            // selectNoneCheckBox
            // 
            selectNoneCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new Point(87, 110);
            selectNoneCheckBox.Margin = new Padding(7, 9, 3, 3);
            selectNoneCheckBox.Name = "selectNoneCheckBox";
            selectNoneCheckBox.Size = new Size(68, 29);
            selectNoneCheckBox.TabIndex = 5;
            selectNoneCheckBox.Text = "هیچ";
            // 
            // selectAllCheckBox
            // 
            selectAllCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(165, 110);
            selectAllCheckBox.Margin = new Padding(7, 9, 3, 3);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(70, 29);
            selectAllCheckBox.TabIndex = 4;
            selectAllCheckBox.Text = "همه";
            // 
            // portfolioComboBox
            // 
            portfolioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new Point(108, 8);
            portfolioComboBox.Name = "portfolioComboBox";
            portfolioComboBox.Size = new Size(236, 33);
            portfolioComboBox.TabIndex = 2;
            // 
            // portfolioLabel
            // 
            portfolioLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new Point(446, 12);
            portfolioLabel.Name = "portfolioLabel";
            portfolioLabel.Size = new Size(46, 25);
            portfolioLabel.TabIndex = 3;
            portfolioLabel.Text = "سبد:";
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
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
            tabPage2.Size = new Size(356, 787);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "فیلترها";
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
            comparisonGroup8.Size = new Size(314, 119);
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
            label5_8.Location = new Point(319, 36);
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
            comparisonGroup7.Location = new Point(8, 624);
            comparisonGroup7.Name = "comparisonGroup7";
            comparisonGroup7.Size = new Size(314, 133);
            comparisonGroup7.TabIndex = 6;
            comparisonGroup7.TabStop = false;
            comparisonGroup7.Text = "7. مقایسه قیمت:";
            // 
            // label8_7
            // 
            label8_7.AutoSize = true;
            label8_7.Location = new Point(70, 80);
            label8_7.Name = "label8_7";
            label8_7.Size = new Size(101, 25);
            label8_7.TabIndex = 0;
            label8_7.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox7
            // 
            comparisonSecondTextBox7.Location = new Point(177, 74);
            comparisonSecondTextBox7.Name = "comparisonSecondTextBox7";
            comparisonSecondTextBox7.Size = new Size(57, 31);
            comparisonSecondTextBox7.TabIndex = 1;
            comparisonSecondTextBox7.Text = "5";
            comparisonSecondTextBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // label6_7
            // 
            label6_7.AutoSize = true;
            label6_7.Location = new Point(72, 40);
            label6_7.Name = "label6_7";
            label6_7.Size = new Size(101, 25);
            label6_7.TabIndex = 3;
            label6_7.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox7
            // 
            comparisonFirstTextBox7.Location = new Point(177, 36);
            comparisonFirstTextBox7.Name = "comparisonFirstTextBox7";
            comparisonFirstTextBox7.Size = new Size(57, 31);
            comparisonFirstTextBox7.TabIndex = 4;
            comparisonFirstTextBox7.Text = "5";
            comparisonFirstTextBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // label5_7
            // 
            label5_7.AutoSize = true;
            label5_7.Location = new Point(319, 36);
            label5_7.Name = "label5_7";
            label5_7.Size = new Size(55, 25);
            label5_7.TabIndex = 5;
            label5_7.Text = "قیمت";
            // 
            // comparisonFirstComboBox7
            // 
            comparisonFirstComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox7.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox7.Location = new Point(236, 33);
            comparisonFirstComboBox7.Name = "comparisonFirstComboBox7";
            comparisonFirstComboBox7.Size = new Size(72, 33);
            comparisonFirstComboBox7.TabIndex = 6;
            // 
            // comparisonOperatorComboBox7
            // 
            comparisonOperatorComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox7.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox7.Location = new Point(1, 38);
            comparisonOperatorComboBox7.Name = "comparisonOperatorComboBox7";
            comparisonOperatorComboBox7.Size = new Size(73, 33);
            comparisonOperatorComboBox7.TabIndex = 7;
            // 
            // comparisonSecondComboBox7
            // 
            comparisonSecondComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox7.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox7.Location = new Point(236, 72);
            comparisonSecondComboBox7.Name = "comparisonSecondComboBox7";
            comparisonSecondComboBox7.Size = new Size(72, 33);
            comparisonSecondComboBox7.TabIndex = 8;
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
            comparisonGroup6.Location = new Point(8, 491);
            comparisonGroup6.Name = "comparisonGroup6";
            comparisonGroup6.Size = new Size(314, 133);
            comparisonGroup6.TabIndex = 5;
            comparisonGroup6.TabStop = false;
            comparisonGroup6.Text = "6. مقایسه قیمت:";
            // 
            // label8_6
            // 
            label8_6.AutoSize = true;
            label8_6.Location = new Point(74, 83);
            label8_6.Name = "label8_6";
            label8_6.Size = new Size(101, 25);
            label8_6.TabIndex = 0;
            label8_6.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox6
            // 
            comparisonSecondTextBox6.Location = new Point(177, 77);
            comparisonSecondTextBox6.Name = "comparisonSecondTextBox6";
            comparisonSecondTextBox6.Size = new Size(57, 31);
            comparisonSecondTextBox6.TabIndex = 1;
            comparisonSecondTextBox6.Text = "5";
            comparisonSecondTextBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // label6_6
            // 
            label6_6.AutoSize = true;
            label6_6.Location = new Point(75, 41);
            label6_6.Name = "label6_6";
            label6_6.Size = new Size(101, 25);
            label6_6.TabIndex = 3;
            label6_6.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox6
            // 
            comparisonFirstTextBox6.Location = new Point(177, 38);
            comparisonFirstTextBox6.Name = "comparisonFirstTextBox6";
            comparisonFirstTextBox6.Size = new Size(57, 31);
            comparisonFirstTextBox6.TabIndex = 4;
            comparisonFirstTextBox6.Text = "5";
            comparisonFirstTextBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // label5_6
            // 
            label5_6.AutoSize = true;
            label5_6.Location = new Point(319, 36);
            label5_6.Name = "label5_6";
            label5_6.Size = new Size(55, 25);
            label5_6.TabIndex = 5;
            label5_6.Text = "قیمت";
            // 
            // comparisonFirstComboBox6
            // 
            comparisonFirstComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox6.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox6.Location = new Point(236, 36);
            comparisonFirstComboBox6.Name = "comparisonFirstComboBox6";
            comparisonFirstComboBox6.Size = new Size(72, 33);
            comparisonFirstComboBox6.TabIndex = 6;
            // 
            // comparisonOperatorComboBox6
            // 
            comparisonOperatorComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox6.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox6.Location = new Point(3, 38);
            comparisonOperatorComboBox6.Name = "comparisonOperatorComboBox6";
            comparisonOperatorComboBox6.Size = new Size(73, 33);
            comparisonOperatorComboBox6.TabIndex = 7;
            // 
            // comparisonSecondComboBox6
            // 
            comparisonSecondComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox6.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox6.Location = new Point(236, 75);
            comparisonSecondComboBox6.Name = "comparisonSecondComboBox6";
            comparisonSecondComboBox6.Size = new Size(72, 33);
            comparisonSecondComboBox6.TabIndex = 8;
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
            comparisonGroup.Location = new Point(8, 358);
            comparisonGroup.Name = "comparisonGroup";
            comparisonGroup.Size = new Size(314, 133);
            comparisonGroup.TabIndex = 0;
            comparisonGroup.TabStop = false;
            comparisonGroup.Text = "5. مقایسه قیمت:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(74, 93);
            label8.Name = "label8";
            label8.Size = new Size(101, 25);
            label8.TabIndex = 0;
            label8.Text = "کندل گذشته";
            // 
            // comparisonSecondTextBox
            // 
            comparisonSecondTextBox.Location = new Point(177, 88);
            comparisonSecondTextBox.Name = "comparisonSecondTextBox";
            comparisonSecondTextBox.Size = new Size(57, 31);
            comparisonSecondTextBox.TabIndex = 1;
            comparisonSecondTextBox.Text = "5";
            comparisonSecondTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(79, 44);
            label6.Name = "label6";
            label6.Size = new Size(101, 25);
            label6.TabIndex = 3;
            label6.Text = "کندل گذشته";
            // 
            // comparisonFirstTextBox
            // 
            comparisonFirstTextBox.Location = new Point(177, 41);
            comparisonFirstTextBox.Name = "comparisonFirstTextBox";
            comparisonFirstTextBox.Size = new Size(57, 31);
            comparisonFirstTextBox.TabIndex = 4;
            comparisonFirstTextBox.Text = "5";
            comparisonFirstTextBox.TextAlign = HorizontalAlignment.Center;
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
            // comparisonFirstComboBox
            // 
            comparisonFirstComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonFirstComboBox.Location = new Point(236, 40);
            comparisonFirstComboBox.Name = "comparisonFirstComboBox";
            comparisonFirstComboBox.Size = new Size(76, 33);
            comparisonFirstComboBox.TabIndex = 1;
            // 
            // comparisonOperatorComboBox
            // 
            comparisonOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=", "!=" });
            comparisonOperatorComboBox.Location = new Point(3, 42);
            comparisonOperatorComboBox.Name = "comparisonOperatorComboBox";
            comparisonOperatorComboBox.Size = new Size(73, 33);
            comparisonOperatorComboBox.TabIndex = 2;
            // 
            // comparisonSecondComboBox
            // 
            comparisonSecondComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL FEE" });
            comparisonSecondComboBox.Location = new Point(236, 86);
            comparisonSecondComboBox.Name = "comparisonSecondComboBox";
            comparisonSecondComboBox.Size = new Size(76, 33);
            comparisonSecondComboBox.TabIndex = 3;
            // 
            // pastDaysGroup
            // 
            pastDaysGroup.Controls.Add(label4);
            pastDaysGroup.Controls.Add(label3);
            pastDaysGroup.Controls.Add(pastDaysTextBox);
            pastDaysGroup.Controls.Add(pastDaysStatusComboBox);
            pastDaysGroup.Dock = DockStyle.Top;
            pastDaysGroup.Location = new Point(8, 272);
            pastDaysGroup.Name = "pastDaysGroup";
            pastDaysGroup.Size = new Size(314, 86);
            pastDaysGroup.TabIndex = 1;
            pastDaysGroup.TabStop = false;
            pastDaysGroup.Text = "۴ ـ وضعیت معامله در روزهای گذشته:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(160, 38);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 0;
            label4.Text = "روز گذشته";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(343, 35);
            label3.Name = "label3";
            label3.Size = new Size(29, 25);
            label3.TabIndex = 1;
            label3.Text = "در";
            // 
            // pastDaysTextBox
            // 
            pastDaysTextBox.Location = new Point(243, 35);
            pastDaysTextBox.Name = "pastDaysTextBox";
            pastDaysTextBox.Size = new Size(57, 31);
            pastDaysTextBox.TabIndex = 1;
            pastDaysTextBox.Text = "5";
            pastDaysTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pastDaysStatusComboBox
            // 
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.Items.AddRange(new object[] { "معامله داشته‌", "معامله نداشته‌" });
            pastDaysStatusComboBox.Location = new Point(10, 35);
            pastDaysStatusComboBox.Name = "pastDaysStatusComboBox";
            pastDaysStatusComboBox.Size = new Size(149, 33);
            pastDaysStatusComboBox.TabIndex = 0;
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
            volumeRatioGroup.Size = new Size(314, 102);
            volumeRatioGroup.TabIndex = 2;
            volumeRatioGroup.TabStop = false;
            volumeRatioGroup.Text = "3. سهامی که نسبت حجم آخرین کندل به:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 66);
            label2.Name = "label2";
            label2.Size = new Size(47, 25);
            label2.TabIndex = 0;
            label2.Text = "باشد";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(78, 29);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 1;
            label1.Text = "کندل گذشته";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(186, 28);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(84, 31);
            textBox1.TabIndex = 2;
            textBox1.Text = "1";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // volumeRatioTextBox
            // 
            volumeRatioTextBox.Location = new Point(90, 63);
            volumeRatioTextBox.Name = "volumeRatioTextBox";
            volumeRatioTextBox.Size = new Size(85, 31);
            volumeRatioTextBox.TabIndex = 1;
            volumeRatioTextBox.Text = "1";
            volumeRatioTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // volumeRatioOperatorComboBox
            // 
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.Items.AddRange(new object[] { "بزرگتر از", "مساوی", "کوچکتر از", "بزرگتر یا مساوی", "کوچکتر یا مساوی" });
            volumeRatioOperatorComboBox.Location = new Point(181, 63);
            volumeRatioOperatorComboBox.Name = "volumeRatioOperatorComboBox";
            volumeRatioOperatorComboBox.Size = new Size(119, 33);
            volumeRatioOperatorComboBox.TabIndex = 0;
            // 
            // nameFilterGroup
            // 
            nameFilterGroup.Controls.Add(nameComboBox);
            nameFilterGroup.Controls.Add(nameTextBox);
            nameFilterGroup.Dock = DockStyle.Top;
            nameFilterGroup.Location = new Point(8, 85);
            nameFilterGroup.Name = "nameFilterGroup";
            nameFilterGroup.Size = new Size(314, 85);
            nameFilterGroup.TabIndex = 3;
            nameFilterGroup.TabStop = false;
            nameFilterGroup.Text = "2. نمایش سهامی که عبارت:";
            // 
            // nameComboBox
            // 
            nameComboBox.FormattingEnabled = true;
            nameComboBox.Items.AddRange(new object[] { "هر جای نام باشد", "در ابتدای نام باشد", "در انتهای نام باشد", "در میانه نام باشد", "در نام نباشد" });
            nameComboBox.Location = new Point(14, 41);
            nameComboBox.Name = "nameComboBox";
            nameComboBox.Size = new Size(206, 33);
            nameComboBox.TabIndex = 0;
            nameComboBox.Text = "هر جای نام باشد";
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameTextBox.Location = new Point(228, 42);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(80, 31);
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
            tradingStatusGroup.Size = new Size(314, 77);
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
            statusAllRadio.Size = new Size(69, 29);
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
            statusPositiveRadio.Size = new Size(81, 29);
            statusPositiveRadio.TabIndex = 1;
            statusPositiveRadio.Text = "داشته";
            // 
            // statusNegativeRadio
            // 
            statusNegativeRadio.AutoSize = true;
            statusNegativeRadio.Location = new Point(227, 33);
            statusNegativeRadio.Margin = new Padding(6);
            statusNegativeRadio.Name = "statusNegativeRadio";
            statusNegativeRadio.Size = new Size(87, 29);
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
            tabPage3.Size = new Size(356, 787);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "شناسه";
            // 
            // identifierMainGroup
            // 
            identifierMainGroup.Controls.Add(identifierLayout);
            identifierMainGroup.Dock = DockStyle.Fill;
            identifierMainGroup.Location = new Point(8, 8);
            identifierMainGroup.Name = "identifierMainGroup";
            identifierMainGroup.Size = new Size(340, 771);
            identifierMainGroup.TabIndex = 0;
            identifierMainGroup.TabStop = false;
            identifierMainGroup.Text = "اطلاعات شناسه";
            identifierMainGroup.Enter += identifierMainGroup_Enter;
            // 
            // identifierLayout
            // 
            identifierLayout.ColumnCount = 2;
            identifierLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
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
            identifierLayout.Size = new Size(334, 741);
            identifierLayout.TabIndex = 0;
            // 
            // identifierSymbolLabel
            // 
            identifierSymbolLabel.Dock = DockStyle.Fill;
            identifierSymbolLabel.Location = new Point(217, 8);
            identifierSymbolLabel.Name = "identifierSymbolLabel";
            identifierSymbolLabel.Size = new Size(106, 20);
            identifierSymbolLabel.TabIndex = 0;
            identifierSymbolLabel.Text = "نماد:";
            identifierSymbolLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierSymbolTextBox
            // 
            identifierSymbolTextBox.Dock = DockStyle.Fill;
            identifierSymbolTextBox.Location = new Point(11, 11);
            identifierSymbolTextBox.Name = "identifierSymbolTextBox";
            identifierSymbolTextBox.Size = new Size(200, 31);
            identifierSymbolTextBox.TabIndex = 1;
            // 
            // identifierNameLabel
            // 
            identifierNameLabel.Dock = DockStyle.Fill;
            identifierNameLabel.Location = new Point(217, 28);
            identifierNameLabel.Name = "identifierNameLabel";
            identifierNameLabel.Size = new Size(106, 20);
            identifierNameLabel.TabIndex = 2;
            identifierNameLabel.Text = "نام شرکت:";
            identifierNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierNameTextBox
            // 
            identifierNameTextBox.Dock = DockStyle.Fill;
            identifierNameTextBox.Location = new Point(11, 31);
            identifierNameTextBox.Name = "identifierNameTextBox";
            identifierNameTextBox.Size = new Size(200, 31);
            identifierNameTextBox.TabIndex = 3;
            // 
            // identifierTsetmcLabel
            // 
            identifierTsetmcLabel.Dock = DockStyle.Fill;
            identifierTsetmcLabel.Location = new Point(217, 48);
            identifierTsetmcLabel.Name = "identifierTsetmcLabel";
            identifierTsetmcLabel.Size = new Size(106, 20);
            identifierTsetmcLabel.TabIndex = 4;
            identifierTsetmcLabel.Text = "کد TSETMC:";
            identifierTsetmcLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierTsetmcTextBox
            // 
            identifierTsetmcTextBox.Dock = DockStyle.Fill;
            identifierTsetmcTextBox.Location = new Point(11, 51);
            identifierTsetmcTextBox.Name = "identifierTsetmcTextBox";
            identifierTsetmcTextBox.Size = new Size(200, 31);
            identifierTsetmcTextBox.TabIndex = 5;
            // 
            // identifierMarketLabel
            // 
            identifierMarketLabel.Dock = DockStyle.Fill;
            identifierMarketLabel.Location = new Point(217, 68);
            identifierMarketLabel.Name = "identifierMarketLabel";
            identifierMarketLabel.Size = new Size(106, 20);
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
            identifierMarketComboBox.Size = new Size(200, 33);
            identifierMarketComboBox.TabIndex = 7;
            // 
            // identifierGroupLabel
            // 
            identifierGroupLabel.Dock = DockStyle.Fill;
            identifierGroupLabel.Location = new Point(217, 88);
            identifierGroupLabel.Name = "identifierGroupLabel";
            identifierGroupLabel.Size = new Size(106, 20);
            identifierGroupLabel.TabIndex = 8;
            identifierGroupLabel.Text = "گروه صنعت:";
            identifierGroupLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierGroupTextBox
            // 
            identifierGroupTextBox.Dock = DockStyle.Fill;
            identifierGroupTextBox.Location = new Point(11, 91);
            identifierGroupTextBox.Name = "identifierGroupTextBox";
            identifierGroupTextBox.Size = new Size(200, 31);
            identifierGroupTextBox.TabIndex = 9;
            // 
            // identifierDescriptionLabel
            // 
            identifierDescriptionLabel.Dock = DockStyle.Fill;
            identifierDescriptionLabel.Location = new Point(217, 108);
            identifierDescriptionLabel.Name = "identifierDescriptionLabel";
            identifierDescriptionLabel.Size = new Size(106, 625);
            identifierDescriptionLabel.TabIndex = 10;
            identifierDescriptionLabel.Text = "توضیحات:";
            identifierDescriptionLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // identifierDescriptionTextBox
            // 
            identifierDescriptionTextBox.Location = new Point(46, 111);
            identifierDescriptionTextBox.Multiline = true;
            identifierDescriptionTextBox.Name = "identifierDescriptionTextBox";
            identifierDescriptionTextBox.Size = new Size(165, 170);
            identifierDescriptionTextBox.TabIndex = 11;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(navigationButton);
            groupBox1.Controls.Add(refreshButton);
            groupBox1.Controls.Add(selectNoneCheckBox);
            groupBox1.Controls.Add(speedLabel);
            groupBox1.Controls.Add(deleteButton);
            groupBox1.Controls.Add(selectAllCheckBox);
            groupBox1.Controls.Add(navigationSpeedTextBox);
            groupBox1.Controls.Add(newPortfolioButton);
            groupBox1.Location = new Point(19, 635);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(329, 144);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // MainForm
            // 
            ClientSize = new Size(1452, 858);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
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
            tabPage2.ResumeLayout(false);
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
            tradingStatusGroup.PerformLayout();
            tabPage3.ResumeLayout(false);
            identifierMainGroup.ResumeLayout(false);
            identifierLayout.ResumeLayout(false);
            identifierLayout.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private Label label6;
        private TextBox comparisonFirstTextBox;
        private Label label5;
        private GroupBox groupBox1;
    }
}