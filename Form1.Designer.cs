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
        private System.Windows.Forms.Button navigationButton;
        private System.Windows.Forms.Button newPortfolioButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
        private System.Windows.Forms.CheckBox selectNoneCheckBox;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TextBox navigationSpeedTextBox;
        private System.Windows.Forms.Panel chartPanel;
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
            navigationButton = new System.Windows.Forms.Button();
            newPortfolioButton = new System.Windows.Forms.Button();
            refreshButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            selectAllCheckBox = new System.Windows.Forms.CheckBox();
            selectNoneCheckBox = new System.Windows.Forms.CheckBox();
            speedLabel = new System.Windows.Forms.Label();
            navigationSpeedTextBox = new System.Windows.Forms.TextBox();
            chartPanel = new System.Windows.Forms.Panel();
            chartPlaceholderLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).BeginInit();
            mainMenuStrip.SuspendLayout();
            SuspendLayout();

            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { portfolioDefinitionMenuItem, portfolioManagementMenuItem, settingsMenuItem });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Size = new System.Drawing.Size(1200, 24);
            mainMenuStrip.TabIndex = 0;
            portfolioDefinitionMenuItem.Text = "تعریف سبد";
            portfolioManagementMenuItem.Text = "مدیریت سبد";
            settingsMenuItem.Text = "تنظیمات";

            mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            mainSplitContainer.Panel1.Controls.Add(controlTabControl);
            mainSplitContainer.Panel2.Controls.Add(chartPanel);
            mainSplitContainer.Size = new System.Drawing.Size(1200, 676);
            mainSplitContainer.SplitterDistance = 390;
            mainSplitContainer.TabIndex = 1;

            controlTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            controlTabControl.Controls.Add(stocksTabPage);
            controlTabControl.Controls.Add(tabPage2);
            controlTabControl.Controls.Add(tabPage3);
            controlTabControl.Name = "controlTabControl";
            controlTabControl.SelectedIndex = 0;

            stocksTabPage.Controls.Add(stocksDataGridView);
            stocksTabPage.Controls.Add(navigationButton);
            stocksTabPage.Controls.Add(newPortfolioButton);
            stocksTabPage.Controls.Add(refreshButton);
            stocksTabPage.Controls.Add(deleteButton);
            stocksTabPage.Controls.Add(selectAllCheckBox);
            stocksTabPage.Controls.Add(selectNoneCheckBox);
            stocksTabPage.Controls.Add(speedLabel);
            stocksTabPage.Controls.Add(navigationSpeedTextBox);
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

            stocksDataGridView.AllowUserToAddRows = false;
            stocksDataGridView.AllowUserToDeleteRows = false;
            stocksDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            stocksDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            stocksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stocksDataGridView.Location = new System.Drawing.Point(8, 42);
            stocksDataGridView.Size = new System.Drawing.Size(350, 525);
            stocksDataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            stocksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { rowColumn, symbolColumn, lastTradeColumn, selectColumn });
            rowColumn.HeaderText = "ردیف";
            symbolColumn.HeaderText = "نماد";
            lastTradeColumn.HeaderText = "آخرین معامله";
            selectColumn.HeaderText = "انتخاب سهم";
            selectColumn.TrueValue = true;
            selectColumn.FalseValue = false;

            navigationButton.Text = "پیمایش";
            navigationButton.Location = new System.Drawing.Point(8, 580);
            navigationButton.Size = new System.Drawing.Size(80, 30);
            newPortfolioButton.Text = "سبد جدید";
            newPortfolioButton.Location = new System.Drawing.Point(94, 580);
            newPortfolioButton.Size = new System.Drawing.Size(80, 30);
            refreshButton.Text = "تازه سازی";
            refreshButton.Location = new System.Drawing.Point(180, 580);
            refreshButton.Size = new System.Drawing.Size(80, 30);
            deleteButton.Text = "حذف";
            deleteButton.Location = new System.Drawing.Point(266, 580);
            deleteButton.Size = new System.Drawing.Size(80, 30);

            selectAllCheckBox.Text = "همه";
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new System.Drawing.Point(8, 620);
            selectNoneCheckBox.Text = "هیچکدام";
            selectNoneCheckBox.AutoSize = true;
            selectNoneCheckBox.Location = new System.Drawing.Point(65, 620);
            speedLabel.Text = "سرعت پیمایش:";
            speedLabel.AutoSize = true;
            speedLabel.Location = new System.Drawing.Point(160, 622);
            navigationSpeedTextBox.Location = new System.Drawing.Point(250, 619);
            navigationSpeedTextBox.Size = new System.Drawing.Size(70, 23);
            navigationSpeedTextBox.Text = "1000";

            tabPage2.Text = "فیلترها";
            tabPage3.Text = "سایر";

            chartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            chartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            chartPlaceholderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            chartPlaceholderLabel.Text = "ناحیه رسم چارت";
            chartPlaceholderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            chartPlaceholderLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
            chartPanel.Controls.Add(chartPlaceholderLabel);

            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new System.Drawing.Size(900, 600);
            Name = "Form1";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Trade.It";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;

            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stocksDataGridView).EndInit();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
