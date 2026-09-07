namespace Trade.It
{
    partial class IdentifierTabControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.GroupBox identityGroup;
        private System.Windows.Forms.TableLayoutPanel identityLayout;
        private System.Windows.Forms.Label symbolLabel;
        private System.Windows.Forms.TextBox symbolTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label isinLabel;
        private System.Windows.Forms.TextBox isinTextBox;
        private System.Windows.Forms.Label marketLabel;
        private System.Windows.Forms.ComboBox marketComboBox;
        private System.Windows.Forms.GroupBox codesGroup;
        private System.Windows.Forms.TableLayoutPanel codesLayout;
        private System.Windows.Forms.Label tsetmcLabel;
        private System.Windows.Forms.TextBox tsetmcTextBox;
        private System.Windows.Forms.Label easyTraderLabel;
        private System.Windows.Forms.TextBox easyTraderTextBox;
        private System.Windows.Forms.Label instrumentLabel;
        private System.Windows.Forms.TextBox instrumentTextBox;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.TableLayoutPanel settingsLayout;
        private System.Windows.Forms.Label timeframeLabel;
        private System.Windows.Forms.ComboBox timeframeComboBox;
        private System.Windows.Forms.Label dataSourceLabel;
        private System.Windows.Forms.ComboBox dataSourceComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainLayout = new TableLayoutPanel();
            identityGroup = new GroupBox();
            identityLayout = new TableLayoutPanel();
            symbolLabel = new Label();
            symbolTextBox = new TextBox();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            isinLabel = new Label();
            isinTextBox = new TextBox();
            marketLabel = new Label();
            marketComboBox = new ComboBox();
            codesGroup = new GroupBox();
            codesLayout = new TableLayoutPanel();
            tsetmcLabel = new Label();
            tsetmcTextBox = new TextBox();
            easyTraderLabel = new Label();
            easyTraderTextBox = new TextBox();
            instrumentLabel = new Label();
            instrumentTextBox = new TextBox();
            settingsGroup = new GroupBox();
            settingsLayout = new TableLayoutPanel();
            timeframeLabel = new Label();
            timeframeComboBox = new ComboBox();
            dataSourceLabel = new Label();
            dataSourceComboBox = new ComboBox();
            SuspendLayout();
            // mainLayout
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 175F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(8);
            mainLayout.RightToLeft = RightToLeft.Yes;
            mainLayout.Controls.Add(identityGroup, 0, 0);
            mainLayout.Controls.Add(codesGroup, 0, 1);
            mainLayout.Controls.Add(settingsGroup, 0, 2);
            // identityGroup
            identityGroup.Dock = DockStyle.Fill;
            identityGroup.Text = "مشخصات نماد";
            identityGroup.Controls.Add(identityLayout);
            identityLayout.Dock = DockStyle.Fill;
            identityLayout.ColumnCount = 4;
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            identityLayout.RowCount = 2;
            identityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            identityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            identityLayout.Padding = new Padding(8);
            identityLayout.Controls.Add(symbolLabel, 0, 0);
            identityLayout.Controls.Add(symbolTextBox, 1, 0);
            identityLayout.Controls.Add(nameLabel, 2, 0);
            identityLayout.Controls.Add(nameTextBox, 3, 0);
            identityLayout.Controls.Add(isinLabel, 0, 1);
            identityLayout.Controls.Add(isinTextBox, 1, 1);
            identityLayout.Controls.Add(marketLabel, 2, 1);
            identityLayout.Controls.Add(marketComboBox, 3, 1);
            symbolLabel.Text = "نماد:";
            nameLabel.Text = "نام:";
            isinLabel.Text = "ISIN:";
            marketLabel.Text = "بازار:";
            symbolLabel.TextAlign = nameLabel.TextAlign = isinLabel.TextAlign = marketLabel.TextAlign = ContentAlignment.MiddleRight;
            symbolTextBox.Dock = nameTextBox.Dock = isinTextBox.Dock = DockStyle.Fill;
            marketComboBox.Dock = DockStyle.Fill;
            marketComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            marketComboBox.Items.AddRange(new object[] { "بورس", "فرابورس", "بورس کالا", "سایر" });
            marketComboBox.SelectedIndex = 0;
            // codesGroup
            codesGroup.Dock = DockStyle.Fill;
            codesGroup.Text = "شناسه‌ها و کدها";
            codesGroup.Controls.Add(codesLayout);
            codesLayout.Dock = DockStyle.Fill;
            codesLayout.ColumnCount = 2;
            codesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            codesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            codesLayout.RowCount = 3;
            codesLayout.Padding = new Padding(8);
            codesLayout.Controls.Add(tsetmcLabel, 0, 0);
            codesLayout.Controls.Add(tsetmcTextBox, 1, 0);
            codesLayout.Controls.Add(easyTraderLabel, 0, 1);
            codesLayout.Controls.Add(easyTraderTextBox, 1, 1);
            codesLayout.Controls.Add(instrumentLabel, 0, 2);
            codesLayout.Controls.Add(instrumentTextBox, 1, 2);
            tsetmcLabel.Text = "کد TSETMC:";
            easyTraderLabel.Text = "کد EasyTrader:";
            instrumentLabel.Text = "کد ابزار:";
            tsetmcLabel.TextAlign = easyTraderLabel.TextAlign = instrumentLabel.TextAlign = ContentAlignment.MiddleRight;
            tsetmcTextBox.Dock = easyTraderTextBox.Dock = instrumentTextBox.Dock = DockStyle.Fill;
            // settingsGroup
            settingsGroup.Dock = DockStyle.Fill;
            settingsGroup.Text = "تنظیمات نمایش";
            settingsGroup.Controls.Add(settingsLayout);
            settingsLayout.Dock = DockStyle.Fill;
            settingsLayout.ColumnCount = 4;
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            settingsLayout.RowCount = 1;
            settingsLayout.Padding = new Padding(8);
            settingsLayout.Controls.Add(timeframeLabel, 0, 0);
            settingsLayout.Controls.Add(timeframeComboBox, 1, 0);
            settingsLayout.Controls.Add(dataSourceLabel, 2, 0);
            settingsLayout.Controls.Add(dataSourceComboBox, 3, 0);
            timeframeLabel.Text = "تایم‌فریم:";
            dataSourceLabel.Text = "منبع داده:";
            timeframeLabel.TextAlign = dataSourceLabel.TextAlign = ContentAlignment.MiddleRight;
            timeframeComboBox.Dock = dataSourceComboBox.Dock = DockStyle.Fill;
            timeframeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            timeframeComboBox.Items.AddRange(new object[] { "روزانه", "هفتگی", "ماهانه", "ساعتی" });
            timeframeComboBox.SelectedIndex = 0;
            dataSourceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dataSourceComboBox.Items.AddRange(new object[] { "TSETMC", "دستی" });
            dataSourceComboBox.SelectedIndex = 0;
            // IdentifierTabControl
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Name = "IdentifierTabControl";
            RightToLeft = RightToLeft.Yes;
            Size = new Size(400, 650);
            ResumeLayout(false);
        }
    }
}
