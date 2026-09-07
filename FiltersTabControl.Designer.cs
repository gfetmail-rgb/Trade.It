namespace Trade.It
{
    partial class FiltersTabControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.GroupBox tradingStatusGroup;
        private System.Windows.Forms.FlowLayoutPanel tradingStatusPanel;
        private System.Windows.Forms.RadioButton statusAllRadio;
        private System.Windows.Forms.RadioButton statusPositiveRadio;
        private System.Windows.Forms.RadioButton statusNegativeRadio;
        private System.Windows.Forms.GroupBox nameFilterGroup;
        private System.Windows.Forms.TableLayoutPanel nameFilterLayout;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox volumeRatioGroup;
        private System.Windows.Forms.TableLayoutPanel volumeRatioLayout;
        private System.Windows.Forms.Label volumeRatioLabel;
        private System.Windows.Forms.TextBox volumeRatioTextBox;
        private System.Windows.Forms.ComboBox volumeRatioOperatorComboBox;
        private System.Windows.Forms.GroupBox pastDaysGroup;
        private System.Windows.Forms.TableLayoutPanel pastDaysLayout;
        private System.Windows.Forms.Label pastDaysLabel;
        private System.Windows.Forms.TextBox pastDaysTextBox;
        private System.Windows.Forms.ComboBox pastDaysStatusComboBox;
        private System.Windows.Forms.GroupBox comparisonGroup;
        private System.Windows.Forms.TableLayoutPanel comparisonLayout;
        private System.Windows.Forms.Label comparisonFirstLabel;
        private System.Windows.Forms.ComboBox comparisonFirstComboBox;
        private System.Windows.Forms.ComboBox comparisonOperatorComboBox;
        private System.Windows.Forms.ComboBox comparisonSecondComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainLayout = new TableLayoutPanel();
            tradingStatusGroup = new GroupBox();
            tradingStatusPanel = new FlowLayoutPanel();
            statusAllRadio = new RadioButton();
            statusPositiveRadio = new RadioButton();
            statusNegativeRadio = new RadioButton();
            nameFilterGroup = new GroupBox();
            nameFilterLayout = new TableLayoutPanel();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            volumeRatioGroup = new GroupBox();
            volumeRatioLayout = new TableLayoutPanel();
            volumeRatioLabel = new Label();
            volumeRatioTextBox = new TextBox();
            volumeRatioOperatorComboBox = new ComboBox();
            pastDaysGroup = new GroupBox();
            pastDaysLayout = new TableLayoutPanel();
            pastDaysLabel = new Label();
            pastDaysTextBox = new TextBox();
            pastDaysStatusComboBox = new ComboBox();
            comparisonGroup = new GroupBox();
            comparisonLayout = new TableLayoutPanel();
            comparisonFirstLabel = new Label();
            comparisonFirstComboBox = new ComboBox();
            comparisonOperatorComboBox = new ComboBox();
            comparisonSecondComboBox = new ComboBox();
            SuspendLayout();
            // mainLayout
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 5;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(8);
            mainLayout.RightToLeft = RightToLeft.Yes;
            mainLayout.Controls.Add(tradingStatusGroup, 0, 0);
            mainLayout.Controls.Add(nameFilterGroup, 0, 1);
            mainLayout.Controls.Add(volumeRatioGroup, 0, 2);
            mainLayout.Controls.Add(pastDaysGroup, 0, 3);
            mainLayout.Controls.Add(comparisonGroup, 0, 4);
            // tradingStatusGroup
            tradingStatusGroup.Dock = DockStyle.Fill;
            tradingStatusGroup.Text = "وضعیت معامله امروز";
            tradingStatusGroup.Controls.Add(tradingStatusPanel);
            tradingStatusPanel.Dock = DockStyle.Fill;
            tradingStatusPanel.FlowDirection = FlowDirection.RightToLeft;
            tradingStatusPanel.WrapContents = false;
            tradingStatusPanel.Padding = new Padding(8, 8, 8, 0);
            tradingStatusPanel.Controls.Add(statusAllRadio);
            tradingStatusPanel.Controls.Add(statusPositiveRadio);
            tradingStatusPanel.Controls.Add(statusNegativeRadio);
            statusAllRadio.AutoSize = true;
            statusAllRadio.Text = "همه";
            statusAllRadio.Checked = true;
            statusPositiveRadio.AutoSize = true;
            statusPositiveRadio.Text = "مثبت";
            statusNegativeRadio.AutoSize = true;
            statusNegativeRadio.Text = "منفی";
            // nameFilterGroup
            nameFilterGroup.Dock = DockStyle.Fill;
            nameFilterGroup.Text = "فیلتر نام سهم";
            nameFilterGroup.Controls.Add(nameFilterLayout);
            nameFilterLayout.Dock = DockStyle.Fill;
            nameFilterLayout.ColumnCount = 2;
            nameFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            nameFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nameFilterLayout.RowCount = 1;
            nameFilterLayout.Padding = new Padding(8);
            nameFilterLayout.Controls.Add(nameLabel, 0, 0);
            nameFilterLayout.Controls.Add(nameTextBox, 1, 0);
            nameLabel.Text = "نام / نماد:";
            nameLabel.TextAlign = ContentAlignment.MiddleRight;
            nameTextBox.Dock = DockStyle.Fill;
            // volumeRatioGroup
            volumeRatioGroup.Dock = DockStyle.Fill;
            volumeRatioGroup.Text = "نسبت حجم آخرین کندل به میانگین X کندل قبل";
            volumeRatioGroup.Controls.Add(volumeRatioLayout);
            volumeRatioLayout.Dock = DockStyle.Fill;
            volumeRatioLayout.ColumnCount = 3;
            volumeRatioLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            volumeRatioLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            volumeRatioLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            volumeRatioLayout.RowCount = 1;
            volumeRatioLayout.Padding = new Padding(8);
            volumeRatioLayout.Controls.Add(volumeRatioOperatorComboBox, 0, 0);
            volumeRatioLayout.Controls.Add(volumeRatioTextBox, 1, 0);
            volumeRatioLayout.Controls.Add(volumeRatioLabel, 2, 0);
            volumeRatioOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            volumeRatioOperatorComboBox.Items.AddRange(new object[] { "بزرگتر یا مساوی", "کوچکتر یا مساوی", "برابر" });
            volumeRatioOperatorComboBox.SelectedIndex = 0;
            volumeRatioTextBox.Text = "1";
            volumeRatioLabel.Text = "ضریب حجم:";
            volumeRatioLabel.TextAlign = ContentAlignment.MiddleRight;
            // pastDaysGroup
            pastDaysGroup.Dock = DockStyle.Fill;
            pastDaysGroup.Text = "وضعیت معامله در X روز گذشته";
            pastDaysGroup.Controls.Add(pastDaysLayout);
            pastDaysLayout.Dock = DockStyle.Fill;
            pastDaysLayout.ColumnCount = 3;
            pastDaysLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            pastDaysLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            pastDaysLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pastDaysLayout.RowCount = 1;
            pastDaysLayout.Padding = new Padding(8);
            pastDaysLayout.Controls.Add(pastDaysStatusComboBox, 0, 0);
            pastDaysLayout.Controls.Add(pastDaysTextBox, 1, 0);
            pastDaysLayout.Controls.Add(pastDaysLabel, 2, 0);
            pastDaysStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pastDaysStatusComboBox.Items.AddRange(new object[] { "همه روزها مثبت", "همه روزها منفی", "حداقل یک روز مثبت", "حداقل یک روز منفی" });
            pastDaysStatusComboBox.SelectedIndex = 0;
            pastDaysTextBox.Text = "5";
            pastDaysLabel.Text = "تعداد روز:";
            pastDaysLabel.TextAlign = ContentAlignment.MiddleRight;
            // comparisonGroup
            comparisonGroup.Dock = DockStyle.Fill;
            comparisonGroup.Text = "مقایسه O / H / L / C / V / FINAL / FEE بین دو روز";
            comparisonGroup.Controls.Add(comparisonLayout);
            comparisonLayout.Dock = DockStyle.Fill;
            comparisonLayout.ColumnCount = 4;
            comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            comparisonLayout.RowCount = 1;
            comparisonLayout.Padding = new Padding(8);
            comparisonLayout.Controls.Add(comparisonFirstLabel, 0, 0);
            comparisonLayout.Controls.Add(comparisonFirstComboBox, 1, 0);
            comparisonLayout.Controls.Add(comparisonOperatorComboBox, 2, 0);
            comparisonLayout.Controls.Add(comparisonSecondComboBox, 3, 0);
            comparisonFirstLabel.Text = "فیلد:";
            comparisonFirstLabel.TextAlign = ContentAlignment.MiddleRight;
            comparisonFirstComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonFirstComboBox.Items.AddRange(new object[] { "O", "H", "L", "C", "V", "FINAL", "FEE" });
            comparisonFirstComboBox.SelectedIndex = 0;
            comparisonOperatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonOperatorComboBox.Items.AddRange(new object[] { ">", "<", "=", ">=", "<=" });
            comparisonOperatorComboBox.SelectedIndex = 0;
            comparisonSecondComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonSecondComboBox.Items.AddRange(new object[] { "روز قبل", "دو روز قبل", "سه روز قبل" });
            comparisonSecondComboBox.SelectedIndex = 0;
            // FiltersTabControl
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Name = "FiltersTabControl";
            RightToLeft = RightToLeft.Yes;
            Size = new Size(400, 650);
            ResumeLayout(false);
        }
    }
}
