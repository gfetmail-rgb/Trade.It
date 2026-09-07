namespace Trade.It
{
    partial class Form1
    {
        private void BuildFiltersTab()
        {
            tabPage2.Text = "فیلترها";
            tabPage2.Padding = new Padding(8);
            tabPage2.AutoScroll = true;
            tabPage2.BackColor = SystemColors.Control;

            var root = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1, RowCount = 5, Padding = new Padding(2), RightToLeft = RightToLeft.Yes };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.Controls.Add(CreateTradingTodayGroup(), 0, 0);
            root.Controls.Add(CreateNameFilterGroup(), 0, 1);
            root.Controls.Add(CreateVolumeRatioGroup(), 0, 2);
            root.Controls.Add(CreateDaysTradeGroup(), 0, 3);
            root.Controls.Add(CreatePriceComparisonGroup(), 0, 4);
            tabPage2.Controls.Add(root);
        }

        private GroupBox CreateTradingTodayGroup()
        {
            var group = CreateGroupBox("۱ ـ وضعیت معامله امروز");
            var panel = CreateOptionPanel();
            panel.Controls.Add(CreateRadioButton("همه", true));
            panel.Controls.Add(CreateRadioButton("معامله داشته‌اند"));
            panel.Controls.Add(CreateRadioButton("معامله نداشته‌اند"));
            panel.Controls.Add(CreateDateLabel("مبنای بررسی: تاریخ امروز"));
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreateNameFilterGroup()
        {
            var group = CreateGroupBox("۲ ـ فیلتر نام سهم");
            var panel = CreateOptionPanel();
            panel.Controls.Add(CreateLabel("عبارت:"));
            panel.Controls.Add(new TextBox { Width = 100, Margin = new Padding(4), PlaceholderText = "مثلاً X" });
            panel.Controls.Add(CreateRadioButton("همه", true));
            panel.Controls.Add(CreateRadioButton("دارای عبارت"));
            panel.Controls.Add(CreateRadioButton("شروع با عبارت"));
            panel.Controls.Add(CreateRadioButton("خاتمه با عبارت"));
            panel.Controls.Add(CreateRadioButton("عبارت در وسط نام"));
            panel.Controls.Add(CreateRadioButton("فاقد عبارت"));
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreateVolumeRatioGroup()
        {
            var group = CreateGroupBox("۳ ـ نسبت حجم آخرین کندل به میانگین X کندل قبل");
            var panel = CreateOptionPanel();
            panel.Controls.Add(CreateLabel("آخرین حجم نسبت به میانگین X کندل قبل:"));
            panel.Controls.Add(CreateNumericTextBox("X", 55));
            panel.Controls.Add(CreateLabel("برابر"));
            panel.Controls.Add(CreateComboBox(new[] { "بزرگتر از", "مساوی", "کوچکتر از", "بزرگتر یا مساوی", "کوچکتر یا مساوی" }, 105));
            panel.Controls.Add(CreateNumericTextBox("نسبت", 70));
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreateDaysTradeGroup()
        {
            var group = CreateGroupBox("۴ ـ وضعیت معامله در X روز گذشته");
            var panel = CreateOptionPanel();
            panel.Controls.Add(CreateLabel("در"));
            panel.Controls.Add(CreateNumericTextBox("X", 55));
            panel.Controls.Add(CreateLabel("روز گذشته (براساس تاریخ):"));
            panel.Controls.Add(CreateRadioButton("معامله داشته‌اند", true));
            panel.Controls.Add(CreateRadioButton("معامله نداشته‌اند"));
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreatePriceComparisonGroup()
        {
            var group = CreateGroupBox("۵ ـ مقایسه O / H / L / C / V / FINAL / FEE بین دو روز");
            var panel = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, RowCount = 4, Padding = new Padding(8), RightToLeft = RightToLeft.Yes };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            panel.Controls.Add(CreateLabel("قیمت مورد مقایسه:"), 0, 0);
            panel.Controls.Add(CreateComboBox(new[] { "O", "H", "L", "C", "V", "FINAL", "FEE" }, 130), 1, 0);
            panel.Controls.Add(CreateLabel("روز اول: X روز قبل"), 0, 1);
            panel.Controls.Add(CreateNumericTextBox("X", 55), 1, 1);
            panel.Controls.Add(CreateLabel("روز دوم: Y روز قبل"), 0, 2);
            panel.Controls.Add(CreateNumericTextBox("Y", 55), 1, 2);
            panel.Controls.Add(CreateLabel("رابطه:"), 0, 3);
            panel.Controls.Add(CreateComboBox(new[] { "بزرگتر از", "مساوی", "نامساوی", "کوچکتر از", "بزرگتر یا مساوی", "کوچکتر یا مساوی" }, 150), 1, 3);
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreateGroupBox(string text) => new GroupBox { Text = text, Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(8), Margin = new Padding(2, 2, 2, 8), Font = new Font("Segoe UI", 10F, FontStyle.Bold), RightToLeft = RightToLeft.Yes };
        private FlowLayoutPanel CreateOptionPanel() => new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, WrapContents = true, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(4), RightToLeft = RightToLeft.Yes };
        private RadioButton CreateRadioButton(string text, bool selected = false) => new RadioButton { Text = text, AutoSize = true, Checked = selected, Margin = new Padding(6, 5, 10, 5), Font = new Font("Segoe UI", 9.5F) };
        private Label CreateLabel(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(5, 7, 5, 5), Font = new Font("Segoe UI", 9.5F), TextAlign = ContentAlignment.MiddleCenter };
        private Label CreateDateLabel(string text) => new Label { Text = text, AutoSize = true, ForeColor = SystemColors.GrayText, Margin = new Padding(10, 7, 5, 5), Font = new Font("Segoe UI", 9F, FontStyle.Italic) };
        private TextBox CreateNumericTextBox(string placeholder, int width) => new TextBox { Width = width, Margin = new Padding(4), PlaceholderText = placeholder, TextAlign = HorizontalAlignment.Center };
        private ComboBox CreateComboBox(string[] items, int width) { var combo = new ComboBox { Width = width, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(4) }; combo.Items.AddRange(items); combo.SelectedIndex = 0; return combo; }
    }
}
