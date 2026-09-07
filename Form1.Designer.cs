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
        private System.Windows.Forms.DataGridViewTextBoxColumn rowColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastTradeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectColumn;
        private System.Windows.Forms.Panel stockControlsPanel;
        private System.Windows.Forms.FlowLayoutPanel stockButtonsPanel;
        private System.Windows.Forms.FlowLayoutPanel stockOptionsPanel;
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
        private System.Windows.Forms.Button fullscreenChartButton;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage chartTabPage;
        private System.Windows.Forms.Panel chartInfoPanel;
        private System.Windows.Forms.Label symbolInfoLabel;
        private System.Windows.Forms.Label dateTimeInfoLabel;
        private System.Windows.Forms.Label ohlcvInfoLabel;
        private System.Windows.Forms.Label chartPlaceholderLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            portfolioDefinitionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            portfolioManagementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mainSplitContainer = new System.Windows.Forms.SplitContainer();
            controlTabControl = new System.Windows.Forms.TabControl();
            stocksTabPage = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            tabPage3 = new System.Windows.Forms.TabPage();
            portfolioComboBox = new System.Windows.Forms.ComboBox();
            portfolioLabel = new System.Windows.Forms.Label();
            stocksDataGridView = new System.Windows.Forms.DataGridView();
            rowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            symbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lastTradeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            selectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            stockControlsPanel = new System.Windows.Forms.Panel();
            stockButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            stockOptionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            navigationButton = new System.Windows.Forms.Button();
            newPortfolioButton = new System.Windows.Forms.Button();
            refreshButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            selectAllCheckBox = new System.Windows.Forms.CheckBox();
            selectNoneCheckBox = new System.Windows.Forms.CheckBox();
            speedLabel = new System.Windows.Forms.Label();
            navigationSpeedTextBox = new System.Windows.Forms.TextBox();
            chartPanel = new System.Windows.Forms.Panel();
            chartToolbarPanel = new System.Windows.Forms.Panel();
            chartTypeComboBox = new System.Windows.Forms.ComboBox();
            gridButton = new System.Windows.Forms.Button();
            crossButton = new System.Windows.Forms.Button();
            zoomInButton = new System.Windows.Forms.Button();
            zoomOutButton = new System.Windows.Forms.Button();
            resetChartButton = new System.Windows.Forms.Button();
            hideChartButton = new System.Windows.Forms.Button();
            hideToolsButton = new System.Windows.Forms.Button();
            printChartButton = new System.Windows.Forms.Button();
            snapshotChartButton = new System.Windows.Forms.Button();
            fullscreenChartButton = new System.Windows.Forms.Button();
            chartTabControl = new System.Windows.Forms.TabControl();
            chartTabPage = new System.Windows.Forms.TabPage();
            chartInfoPanel = new System.Windows.Forms.Panel();
            symbolInfoLabel = new System.Windows.Forms.Label();
            dateTimeInfoLabel = new System.Windows.Forms.Label();
            ohlcvInfoLabel = new System.Windows.Forms.Label();
            chartPlaceholderLabel = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).BeginInit();
            mainMenuStrip.SuspendLayout();
            stockControlsPanel.SuspendLayout();
            stockButtonsPanel.SuspendLayout();
            stockOptionsPanel.SuspendLayout();
            chartPanel.SuspendLayout();
            chartToolbarPanel.SuspendLayout();
            chartTabControl.SuspendLayout();
            chartTabPage.SuspendLayout();
            chartInfoPanel.SuspendLayout();
            SuspendLayout();

            // Main menu
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Size = new System.Drawing.Size(1200, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            portfolioDefinitionMenuItem.Text = "تعریف سبد";
            portfolioManagementMenuItem.Text = "مدیریت سبد";
            settingsMenuItem.Text = "تنظیمات";

            // Main split: chart on the right, controls on the left.
            mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            mainSplitContainer.Panel1.Controls.Add(chartPanel);
            mainSplitContainer.Panel2.Controls.Add(controlTabControl);
            mainSplitContainer.Size = new System.Drawing.Size(1200, 676);
            mainSplitContainer.SplitterDistance = 810;
            mainSplitContainer.TabIndex = 1;

            // Left control area
            controlTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            controlTabControl.Controls.Add(stocksTabPage);
            controlTabControl.Controls.Add(tabPage2);
            controlTabControl.Controls.Add(tabPage3);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.SelectedIndex = 0;

            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(stockControlsPanel);
            stocksTabPage.Controls.Add(portfolioComboBox);
            stocksTabPage.Controls.Add(portfolioLabel);
            stocksTabPage.Text = "سهام";
            stocksTabPage.Padding = new System.Windows.Forms.Padding(8);

            portfolioLabel.AutoSize = true;
            portfolioLabel.Location = new System.Drawing.Point(270, 12);
            portfolioLabel.Text = "سبد:";
            portfolioComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            portfolioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            portfolioComboBox.Location = new System.Drawing.Point(8, 8);
            portfolioComboBox.Size = new System.Drawing.Size(250, 23);

            // Reduced grid height so the controls below remain visible.
            stocksDataGridView.AllowUserToAddRows = false;
            stocksDataGridView.AllowUserToDeleteRows = false;
            stocksDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            stocksDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            stocksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stocksDataGridView.Location = new System.Drawing.Point(8, 42);
            stocksDataGridView.Size = new System.Drawing.Size(350, 465);
            stocksDataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            stocksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { rowColumn, symbolColumn, lastTradeColumn, selectColumn });
            rowColumn.HeaderText = "ردیف";
            symbolColumn.HeaderText = "نماد";
            lastTradeColumn.HeaderText = "آخرین معامله";
            selectColumn.HeaderText = "انتخاب سهم";
            selectColumn.TrueValue = true;
            selectColumn.FalseValue = false;

            // Bottom stock controls are docked and therefore remain visible at different DPI/scales.
            stockControlsPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            stockControlsPanel.Location = new System.Drawing.Point(8, 512);
            stockControlsPanel.Size = new System.Drawing.Size(350, 105);
            stockButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            stockButtonsPanel.Height = 38;
            stockButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            stockButtonsPanel.WrapContents = false;
            stockButtonsPanel.Controls.Add(navigationButton);
            stockButtonsPanel.Controls.Add(newPortfolioButton);
            stockButtonsPanel.Controls.Add(refreshButton);
            stockButtonsPanel.Controls.Add(deleteButton);
            stockOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            stockOptionsPanel.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            stockOptionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            stockOptionsPanel.WrapContents = false;
            stockOptionsPanel.Controls.Add(selectAllCheckBox);
            stockOptionsPanel.Controls.Add(selectNoneCheckBox);
            stockOptionsPanel.Controls.Add(speedLabel);
            stockOptionsPanel.Controls.Add(navigationSpeedTextBox);
            stockControlsPanel.Controls.Add(stockOptionsPanel);
            stockControlsPanel.Controls.Add(stockButtonsPanel);

            navigationButton.Text = "پیمایش";
            navigationButton.Size = new System.Drawing.Size(75, 30);
            newPortfolioButton.Text = "سبد جدید";
            newPortfolioButton.Size = new System.Drawing.Size(75, 30);
            refreshButton.Text = "تازه‌سازی";
            refreshButton.Size = new System.Drawing.Size(75, 30);
            deleteButton.Text = "حذف";
            deleteButton.Size = new System.Drawing.Size(60, 30);
            selectAllCheckBox.Text = "همه";
            selectAllCheckBox.AutoSize = true;
            selectNoneCheckBox.Text = "هیچکدام";
            selectNoneCheckBox.AutoSize = true;
            speedLabel.Text = "سرعت:";
            speedLabel.AutoSize = true;
            navigationSpeedTextBox.Size = new System.Drawing.Size(65, 23);
            navigationSpeedTextBox.Text = "1000";

            tabPage2.Text = "فیلترها";
            tabPage3.Text = "سایر";

            // Shared chart toolbar.
            chartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            chartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            chartPanel.Controls.Add(chartTabControl);
            chartPanel.Controls.Add(chartToolbarPanel);

            chartToolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            chartToolbarPanel.Height = 42;
            chartToolbarPanel.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            chartToolbarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            chartToolbarPanel.Controls.Add(fullscreenChartButton);

            chartTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            chartTypeComboBox.Items.AddRange(new object[] { "شمعی", "خطی", "میله ای" });
            chartTypeComboBox.SelectedIndex = 0;
            chartTypeComboBox.Location = new System.Drawing.Point(6, 8);
            chartTypeComboBox.Size = new System.Drawing.Size(100, 23);
            gridButton.Text = "گرید";
            gridButton.Location = new System.Drawing.Point(112, 7);
            gridButton.Size = new System.Drawing.Size(58, 26);
            crossButton.Text = "Cross";
            crossButton.Location = new System.Drawing.Point(176, 7);
            crossButton.Size = new System.Drawing.Size(62, 26);
            zoomInButton.Text = "+";
            zoomInButton.Location = new System.Drawing.Point(244, 7);
            zoomInButton.Size = new System.Drawing.Size(32, 26);
            zoomOutButton.Text = "−";
            zoomOutButton.Location = new System.Drawing.Point(282, 7);
            zoomOutButton.Size = new System.Drawing.Size(32, 26);
            resetChartButton.Text = "Reset";
            resetChartButton.Location = new System.Drawing.Point(320, 7);
            resetChartButton.Size = new System.Drawing.Size(58, 26);
            hideChartButton.Text = "مخفی چارت";
            hideChartButton.Location = new System.Drawing.Point(384, 7);
            hideChartButton.Size = new System.Drawing.Size(82, 26);
            hideToolsButton.Text = "مخفی ابزار";
            hideToolsButton.Location = new System.Drawing.Point(472, 7);
            hideToolsButton.Size = new System.Drawing.Size(82, 26);
            printChartButton.Text = "چاپ";
            printChartButton.Location = new System.Drawing.Point(560, 7);
            printChartButton.Size = new System.Drawing.Size(55, 26);
            snapshotChartButton.Text = "عکس";
            snapshotChartButton.Location = new System.Drawing.Point(621, 7);
            snapshotChartButton.Size = new System.Drawing.Size(55, 26);
            fullscreenChartButton.Text = "تمام‌صفحه";
            fullscreenChartButton.Location = new System.Drawing.Point(682, 7);
            fullscreenChartButton.Size = new System.Drawing.Size(82, 26);

            // One chart per tab.
            chartTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            chartTabControl.Name = "chartTabControl";
            chartTabControl.Controls.Add(chartTabPage);
            chartTabControl.SelectedIndex = 0;
            chartTabPage.Text = "نماد";
            chartTabPage.Padding = new System.Windows.Forms.Padding(3);
            chartTabPage.Controls.Add(chartPlaceholderLabel);
            chartTabPage.Controls.Add(chartInfoPanel);

            // Chart information overlay: visual UI only for now. Later it will follow the mouse cursor.
            chartInfoPanel.Location = new System.Drawing.Point(12, 12);
            chartInfoPanel.Size = new System.Drawing.Size(285, 82);
            chartInfoPanel.BackColor = System.Drawing.SystemColors.Control;
            chartInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            symbolInfoLabel.AutoSize = true;
            symbolInfoLabel.Location = new System.Drawing.Point(8, 7);
            symbolInfoLabel.Text = "نماد: —";
            dateTimeInfoLabel.AutoSize = true;
            dateTimeInfoLabel.Location = new System.Drawing.Point(8, 29);
            dateTimeInfoLabel.Text = "تاریخ/زمان: —";
            ohlcvInfoLabel.AutoSize = true;
            ohlcvInfoLabel.Location = new System.Drawing.Point(8, 51);
            ohlcvInfoLabel.Text = "O: —   H: —   L: —   C: —   V: —";
            chartInfoPanel.Controls.Add(symbolInfoLabel);
            chartInfoPanel.Controls.Add(dateTimeInfoLabel);
            chartInfoPanel.Controls.Add(ohlcvInfoLabel);

            chartPlaceholderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            chartPlaceholderLabel.Text = "ناحیه رسم چارت";
            chartPlaceholderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            chartPlaceholderLabel.Font = new System.Drawing.Font("Segoe UI", 14F);

            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new System.Drawing.Size(900, 600);
            Name = "Form1";
            RightToLeft = System.Windows.Forms.RightToLeft.No;
            RightToLeftLayout = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Trade.It";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;

            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).EndInit();
            stockOptionsPanel.ResumeLayout(false);
            stockOptionsPanel.PerformLayout();
            stockButtonsPanel.ResumeLayout(false);
            stockControlsPanel.ResumeLayout(false);
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            chartInfoPanel.ResumeLayout(false);
            chartInfoPanel.PerformLayout();
            chartTabPage.ResumeLayout(false);
            chartTabControl.ResumeLayout(false);
            chartToolbarPanel.ResumeLayout(false);
            chartPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
