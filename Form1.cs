namespace Trade.It
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mainSplitContainer.Panel1.Controls.Clear();
            mainSplitContainer.Panel2.Controls.Clear();
            mainSplitContainer.Panel1.Controls.Add(controlTabControl);
            mainSplitContainer.Panel2.Controls.Add(chartPanel);
            mainSplitContainer.SplitterDistance = 419;
            mainMenuStrip.RightToLeft = RightToLeft.Yes;
            var closeAllChartsMenuItem = new ToolStripMenuItem { Name = "closeAllChartsMenuItem", Text = "بستن همه چارتها" };
            mainMenuStrip.Items.Add(closeAllChartsMenuItem);
            portfolioDefinitionMenuItem.Click += (_, _) => new PortfolioDefinitionForm().ShowDialog(this);
            portfolioManagementMenuItem.Click += (_, _) => new PortfolioManagementForm().ShowDialog(this);

            tabPage3.Controls.Clear();
            tabPage3.AutoScroll = true;
            var identifierButtonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 52, Padding = new Padding(4), FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes };
            foreach (var buttonInfo in new[] { (Text: "جدید", Width: 62), (Text: "ذخیره", Width: 70), (Text: "حذف", Width: 62), (Text: "حذف همه", Width: 80), (Text: "ورود از اکسل", Width: 105) })
                identifierButtonsPanel.Controls.Add(new Button { Text = buttonInfo.Text, Width = buttonInfo.Width, Height = 36, Margin = new Padding(3) });
            var identifierGroup = new GroupBox { Dock = DockStyle.Top, AutoSize = true, Text = "اطلاعات شناسه", Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
            var identifierFields = new (string Label, bool Combo)[] { ("کد ۱۲ رقمی نماد", false), ("کد ۵ رقمی نماد", false), ("نام لاتین شرکت", false), ("کد ۴ رقمی شرکت", false), ("نام شرکت", false), ("نماد فارسی", false), ("نماد ۳۰ رقمی فارسی", false), ("کد ۱۲ رقمی شرکت", false), ("بازار", true), ("کد تابلو", false), ("کد گروه صنعت", false), ("گروه صنعت", false), ("کد زیر گروه صنعت", false), ("زیر گروه صنعت", false) };
            var identifierTable = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, RowCount = identifierFields.Length, Padding = new Padding(6), RightToLeft = RightToLeft.Yes };
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            foreach (var field in identifierFields)
            {
                identifierTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                identifierTable.Controls.Add(new Label { Text = field.Label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4), Font = new Font("Segoe UI", 10F) });
                Control input = field.Combo ? new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(4) } : new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) };
                identifierTable.Controls.Add(input);
            }
            identifierGroup.Controls.Add(identifierTable);
            tabPage3.Controls.Add(identifierGroup);
            tabPage3.Controls.Add(identifierButtonsPanel);
            identifierButtonsPanel.BringToFront();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e) { }
        private void pastDaysStatusComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private sealed class PortfolioDefinitionForm : Form
        {
            public PortfolioDefinitionForm()
            {
                Text = "تعریف سبد";
                StartPosition = FormStartPosition.CenterParent;
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                MinimumSize = new Size(1150, 760);
                Size = new Size(1536, 988);
                Font = new Font("Segoe UI", 10F);

                var main = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 8, Padding = new Padding(14, 10, 14, 8), RightToLeft = RightToLeft.Yes, AutoScroll = true };
                main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
                main.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
                main.Controls.Add(CreateNamePanel(), 0, 0);
                main.Controls.Add(CreateSourceGroup(), 0, 1);
                main.Controls.Add(new Label { Text = "منبع نام نماد: فایل‌های موجود در مسیر انتخاب شده", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(4) }, 0, 2);
                main.Controls.Add(CreatePathPanel(), 0, 3);
                main.Controls.Add(CreateGeneralOptionsPanel(), 0, 4);
                main.Controls.Add(CreateFormatPanel(), 0, 5);
                main.Controls.Add(CreateSelectionAndMappingArea(), 0, 6);
                main.Controls.Add(CreatePreviewGroup(), 0, 7);

                var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 54, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, RightToLeft = RightToLeft.Yes, Padding = new Padding(0, 4, 0, 0) };
                bottom.Controls.Add(CreateButton("ذخیره سبد", 125));
                bottom.Controls.Add(CreateButton("لغو", 90));
                bottom.Controls.Add(CreateButton("تست Mapping", 135));
                Controls.Add(main);
                Controls.Add(bottom);
                bottom.BringToFront();
            }

            private static Control CreateNamePanel()
            {
                var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                panel.Controls.Add(CreateLabel("نام سبد:"), 0, 0);
                panel.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, 1, 0);
                return panel;
            }

            private static GroupBox CreateSourceGroup()
            {
                var group = new GroupBox { Text = "منبع اطلاعات 1", Dock = DockStyle.Fill, Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
                var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes, Padding = new Padding(12, 10, 12, 0) };
                flow.Controls.Add(CreateRadio("نام فایل", true, 110));
                flow.Controls.Add(CreateRadio("داخل فایل", false, 110));
                group.Controls.Add(flow);
                return group;
            }

            private static Control CreatePathPanel()
            {
                var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, RightToLeft = RightToLeft.Yes };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
                panel.Controls.Add(CreateLabel("مسیر داده:"), 0, 0);
                panel.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, 1, 0);
                panel.Controls.Add(CreateButton("انتخاب پوشه...", 135), 2, 0);
                return panel;
            }

            private static Control CreateGeneralOptionsPanel()
            {
                var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 1, RightToLeft = RightToLeft.Yes };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 185F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                panel.Controls.Add(CreateLabel("جداکننده:"), 0, 0);
                panel.Controls.Add(CreateCombo(new[] { "Comma (,)", "Semicolon (;)", "Tab", "Pipe (|)" }), 1, 0);
                panel.Controls.Add(CreateLabel("تقویم:"), 2, 0);
                panel.Controls.Add(CreateCombo(new[] { "شمسی (Persian)", "لاتین (Gregorian)" }), 3, 0);
                panel.Controls.Add(CreateCheck("سطر اول عنوان ستون‌ها"), 4, 0);
                panel.Controls.Add(CreateCheck("داده فاقد تاریخ/زمان است"), 5, 0);
                return panel;
            }

            private static Control CreateFormatPanel()
            {
                var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, RightToLeft = RightToLeft.Yes };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
                panel.Controls.Add(CreateCheck("فرمت زمان:"), 0, 0);
                panel.Controls.Add(CreateCombo(new[] { "HHMMSS", "HH:MM:SS", "HHMM" }, 190), 1, 0);
                panel.Controls.Add(CreateCheck("فرمت تاریخ:"), 2, 0);
                panel.Controls.Add(CreateCombo(new[] { "YYYYMMDD", "YYYY-MM-DD", "YYYY/MM/DD" }, 190), 3, 0);
                return panel;
            }

            private static Control CreateSelectionAndMappingArea()
            {
                var area = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes, Margin = new Padding(0) };
                area.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
                area.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
                area.Controls.Add(CreateMappingGroup(), 0, 0);
                area.Controls.Add(CreateSymbolSelectionGroup(), 1, 0);
                return area;
            }

            private static GroupBox CreateSymbolSelectionGroup()
            {
                var group = new GroupBox { Text = "انتخاب سهام", Dock = DockStyle.Fill, Padding = new Padding(8), RightToLeft = RightToLeft.Yes };
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes };
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                var top = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, RightToLeft = RightToLeft.Yes };
                top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
                top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
                top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
                top.Controls.Add(CreateButton("عدم انتخاب همه", 120), 0, 0);
                top.Controls.Add(CreateButton("انتخاب همه", 115), 1, 0);
                top.Controls.Add(new TextBox { Dock = DockStyle.Fill, PlaceholderText = "جستجوی نماد...", Margin = new Padding(4) }, 2, 0);
                top.Controls.Add(CreateLabel("انتخاب شده: ۰ از ۰"), 3, 0);
                layout.Controls.Add(top, 0, 0);
                var grid = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoGenerateColumns = false, RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RightToLeft = RightToLeft.Yes, BackgroundColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
                grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "انتخاب", Width = 80, Name = "selectedColumn" });
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Name = "symbolColumn", ReadOnly = true });
                layout.Controls.Add(grid, 0, 1);
                group.Controls.Add(layout);
                return group;
            }

            private static GroupBox CreateMappingGroup()
            {
                var group = new GroupBox { Text = "Mapping ستون‌ها", Dock = DockStyle.Fill, Padding = new Padding(8), RightToLeft = RightToLeft.Yes };
                var grid = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoGenerateColumns = false, RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RightToLeft = RightToLeft.Yes, BackgroundColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle, ScrollBars = ScrollBars.Vertical };
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "فیلد استاندارد", Width = 155, Name = "standardField", ReadOnly = true });
                grid.Columns.Add(new DataGridViewComboBoxColumn { HeaderText = "ستون فایل", Width = 155, Name = "fileColumn" });
                foreach (var field in new[] { "نماد", "تاریخ", "زمان", "Open", "High", "Low", "Close", "حجم", "قیمت پایانی امروز", "قیمت پایانی دیروز", "تعداد معامله", "ارزش معاملات", "تعداد سهم", "ارزش بازار", "نماد انگلیسی" }) grid.Rows.Add(field, "");
                group.Controls.Add(grid);
                return group;
            }

            private static GroupBox CreatePreviewGroup()
            {
                var group = new GroupBox { Text = "پیش‌نمایش داده", Dock = DockStyle.Fill, Padding = new Padding(8), RightToLeft = RightToLeft.Yes };
                var grid = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, AutoGenerateColumns = false, RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RightToLeft = RightToLeft.Yes, BackgroundColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", Width = 120 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام فایل", Width = 220 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ستون‌های موجود در فایل", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                group.Controls.Add(grid);
                return group;
            }

            private static Label CreateLabel(string text) => new Label { Text = text, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4) };
            private static ComboBox CreateCombo(string[] items, int width = 0)
            {
                var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = width == 0 ? DockStyle.Fill : DockStyle.None, Width = width, Height = 32, Margin = new Padding(4) };
                combo.Items.AddRange(items);
                if (combo.Items.Count > 0) combo.SelectedIndex = 0;
                return combo;
            }
            private static CheckBox CreateCheck(string text) => new CheckBox { Text = text, AutoSize = true, Anchor = AnchorStyles.Right, Margin = new Padding(4, 7, 4, 4) };
            private static RadioButton CreateRadio(string text, bool checkedState, int width) => new RadioButton { Text = text, Checked = checkedState, Width = width, Height = 32, Margin = new Padding(8, 4, 8, 4) };
            private static Button CreateButton(string text, int width) => new Button { Text = text, Width = width, Height = 36, Margin = new Padding(4) };
        }

        private sealed class PortfolioManagementForm : Form
        {
            public PortfolioManagementForm()
            {
                Text = "مدیریت سبدها";
                StartPosition = FormStartPosition.CenterParent;
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                MinimumSize = new Size(1100, 700);
                Size = new Size(1536, 983);
                Font = new Font("Segoe UI", 10F);
                var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(6), RightToLeft = RightToLeft.Yes };
                root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                root.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
                root.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
                root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
                var header = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle };
                var title = new Label { Text = "مدیریت سبدها", Dock = DockStyle.Right, Width = 260, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 13F) };
                var reload = CreateButton("بازخوانی", 145); reload.Location = new Point(14, 12); header.Controls.Add(title); header.Controls.Add(reload); root.Controls.Add(header, 0, 0);
                var upper = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, RightToLeft = RightToLeft.Yes };
                upper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F)); upper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
                upper.Controls.Add(CreatePortfolioParametersGroup(), 0, 0); upper.Controls.Add(CreateExistingPortfoliosGroup(), 1, 0); root.Controls.Add(upper, 0, 1);
                root.Controls.Add(CreatePortfolioSymbolsGroup(), 0, 2);
                var footer = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle };
                var status = new Label { Text = "هیچ سبدی وجود ندارد.", Dock = DockStyle.Right, Width = 320, TextAlign = ContentAlignment.MiddleRight, Padding = new Padding(4) };
                var close = CreateButton("بستن", 125); close.Location = new Point(12, 10); footer.Controls.Add(status); footer.Controls.Add(close); root.Controls.Add(footer, 0, 3); Controls.Add(root);
            }
            private static GroupBox CreatePortfolioParametersGroup()
            {
                var group = new GroupBox { Text = "پارامترهای سبد", Dock = DockStyle.Fill, Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
                var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 6, RightToLeft = RightToLeft.Yes };
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                for (int i = 0; i < 6; i++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
                AddField(table, "نام سبد:", 0, 0); AddField(table, "نوع منبع:", 2, 0); AddField(table, "مسیر داده:", 0, 1); AddField(table, "نوع داده:", 0, 2); AddField(table, "منبع نام نماد:", 2, 2); AddField(table, "جداکننده:", 0, 3); AddField(table, "فرمت زمان:", 2, 3); AddField(table, "فرمت تاریخ:", 0, 4); AddField(table, "Header:", 2, 4); AddField(table, "تقویم:", 0, 5); AddField(table, "تعداد نماد:", 2, 5); group.Controls.Add(table); return group;
            }
            private static GroupBox CreateExistingPortfoliosGroup()
            {
                var group = new GroupBox { Text = "سبدهای موجود", Dock = DockStyle.Fill, Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes };
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                layout.Controls.Add(new ListBox { Dock = DockStyle.Fill, RightToLeft = RightToLeft.Yes, IntegralHeight = false }, 0, 0);
                var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes }; buttons.Controls.Add(CreateButton("حذف سبدهای انتخاب شده", 205)); layout.Controls.Add(buttons, 0, 1); group.Controls.Add(layout); return group;
            }
            private static GroupBox CreatePortfolioSymbolsGroup()
            {
                var group = new GroupBox { Text = "نمادهای سبد", Dock = DockStyle.Fill, Padding = new Padding(10), RightToLeft = RightToLeft.Yes };
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, RightToLeft = RightToLeft.Yes }; layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
                var grid = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, AutoGenerateColumns = false, RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RightToLeft = RightToLeft.Yes, BackgroundColor = SystemColors.Window };
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ردیف", Width = 75 }); grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "انتخاب", Width = 85 }); grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", Width = 140 }); grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام نمایشی", Width = 180 }); grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "آخرین معامله", Width = 150 }); grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "حجم", Width = 130 }); grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "قیمت پایانی", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill }); layout.Controls.Add(grid, 0, 0);
                var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, RightToLeft = RightToLeft.Yes }; buttons.Controls.Add(CreateButton("حذف نمادهای انتخاب شده", 245)); layout.Controls.Add(buttons, 0, 1); group.Controls.Add(layout); return group;
            }
            private static void AddField(TableLayoutPanel table, string labelText, int labelColumn, int row)
            {
                table.Controls.Add(new Label { Text = labelText, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(4) }, labelColumn, row);
                table.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, labelColumn + 1, row);
            }
            private static Button CreateButton(string text, int width) => new Button { Text = text, Width = width, Height = 36, Margin = new Padding(4) };
        }
    }
}
