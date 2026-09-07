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

            var closeAllChartsMenuItem = new ToolStripMenuItem
            {
                Name = "closeAllChartsMenuItem",
                Text = "بستن همه چارتها"
            };
            mainMenuStrip.Items.Add(closeAllChartsMenuItem);

            portfolioDefinitionMenuItem.Click += (_, _) => new PortfolioDefinitionForm().ShowDialog(this);

            tabPage3.Controls.Clear();
            tabPage3.AutoScroll = true;

            var identifierButtonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(4),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                RightToLeft = RightToLeft.Yes
            };

            foreach (var buttonInfo in new[]
            {
                (Text: "جدید", Width: 62),
                (Text: "ذخیره", Width: 70),
                (Text: "حذف", Width: 62),
                (Text: "حذف همه", Width: 80),
                (Text: "ورود از اکسل", Width: 105)
            })
            {
                identifierButtonsPanel.Controls.Add(new Button
                {
                    Text = buttonInfo.Text,
                    Width = buttonInfo.Width,
                    Height = 36,
                    Margin = new Padding(3)
                });
            }

            var identifierGroup = new GroupBox
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Text = "اطلاعات شناسه",
                Padding = new Padding(10),
                RightToLeft = RightToLeft.Yes
            };

            var identifierFields = new (string Label, bool Combo)[]
            {
                ("کد ۱۲ رقمی نماد", false),
                ("کد ۵ رقمی نماد", false),
                ("نام لاتین شرکت", false),
                ("کد ۴ رقمی شرکت", false),
                ("نام شرکت", false),
                ("نماد فارسی", false),
                ("نماد ۳۰ رقمی فارسی", false),
                ("کد ۱۲ رقمی شرکت", false),
                ("بازار", true),
                ("کد تابلو", false),
                ("کد گروه صنعت", false),
                ("گروه صنعت", false),
                ("کد زیر گروه صنعت", false),
                ("زیر گروه صنعت", false)
            };

            var identifierTable = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = identifierFields.Length,
                Padding = new Padding(6),
                RightToLeft = RightToLeft.Yes
            };
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            identifierTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            foreach (var field in identifierFields)
            {
                var label = new Label
                {
                    Text = field.Label,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(4),
                    Font = new Font("Segoe UI", 10F)
                };

                Control input;
                if (field.Combo)
                {
                    input = new ComboBox
                    {
                        Dock = DockStyle.Fill,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Font = new Font("Segoe UI", 10F),
                        Margin = new Padding(4)
                    };
                }
                else
                {
                    input = new TextBox
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 10F),
                        Margin = new Padding(4)
                    };
                }

                identifierTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                identifierTable.Controls.Add(label);
                identifierTable.Controls.Add(input);
            }

            identifierGroup.Controls.Add(identifierTable);
            tabPage3.Controls.Add(identifierGroup);
            tabPage3.Controls.Add(identifierButtonsPanel);
            identifierButtonsPanel.BringToFront();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void pastDaysStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private sealed class PortfolioDefinitionForm : Form
        {
            public PortfolioDefinitionForm()
            {
                Text = "تعریف سبد";
                StartPosition = FormStartPosition.CenterParent;
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                MinimumSize = new Size(900, 850);
                Size = new Size(1050, 980);
                Font = new Font("Segoe UI", 10F);

                var mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 8,
                    Padding = new Padding(14),
                    AutoScroll = true,
                    RightToLeft = RightToLeft.Yes
                };

                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 360F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

                mainPanel.Controls.Add(CreateLabeledTextBox("نام سبد", 0), 0, 0);

                var sourceGroup = new GroupBox
                {
                    Text = "منبع اطلاعات 1",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    RightToLeft = RightToLeft.Yes
                };
                var sourceFlow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes
                };
                sourceFlow.Controls.Add(CreateRadio("نام فایل", true));
                sourceFlow.Controls.Add(CreateRadio("داخل فایل", false));
                sourceGroup.Controls.Add(sourceFlow);
                mainPanel.Controls.Add(sourceGroup, 0, 1);

                var dataSettingsGroup = new GroupBox
                {
                    Text = "تنظیمات منبع داده",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    RightToLeft = RightToLeft.Yes
                };
                var dataSettings = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 4,
                    RowCount = 2,
                    RightToLeft = RightToLeft.Yes
                };
                dataSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
                dataSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                dataSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
                dataSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                dataSettings.Controls.Add(CreateLabel("مسیر داده"), 0, 0);
                dataSettings.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, 1, 0);
                dataSettings.Controls.Add(CreateLabel("جداکننده"), 2, 0);
                dataSettings.Controls.Add(CreateCombo(new[] { ",", ";", "Tab", "|" }), 3, 0);
                dataSettings.Controls.Add(CreateLabel("نوع تقویم"), 0, 1);
                dataSettings.Controls.Add(CreateCombo(new[] { "شمسی", "لاتین" }), 1, 1);
                dataSettings.Controls.Add(CreateCheck("سطر اول عنوان ستون‌ها"), 2, 1);
                dataSettings.Controls.Add(CreateCheck("داده فاقد تاریخ و زمان"), 3, 1);
                dataSettingsGroup.Controls.Add(dataSettings);
                mainPanel.Controls.Add(dataSettingsGroup, 0, 2);

                var formatPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes,
                    Padding = new Padding(0, 5, 0, 0)
                };
                formatPanel.Controls.Add(CreateCheck("فرمت زمان"));
                formatPanel.Controls.Add(CreateCheck("فرمت تاریخ"));
                formatPanel.Controls.Add(CreateButton("انتخاب پوشه...", 135));
                mainPanel.Controls.Add(formatPanel, 0, 2);

                var symbolGroup = new GroupBox
                {
                    Text = "انتخاب سهام موجود در پوشه",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8),
                    RightToLeft = RightToLeft.Yes
                };
                var symbolLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    RightToLeft = RightToLeft.Yes
                };
                symbolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                symbolLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                symbolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
                var searchPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RightToLeft = RightToLeft.Yes
                };
                searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
                searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                searchPanel.Controls.Add(CreateLabel("جستجو"), 0, 0);
                searchPanel.Controls.Add(new TextBox { Dock = DockStyle.Fill, Margin = new Padding(4) }, 1, 0);
                symbolLayout.Controls.Add(searchPanel, 0, 0);

                var symbolGrid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoGenerateColumns = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    RightToLeft = RightToLeft.Yes
                };
                symbolGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ردیف", Width = 70, Name = "rowColumn", ReadOnly = true });
                symbolGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Name = "symbolColumn", ReadOnly = true });
                symbolGrid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "انتخاب", Width = 80, Name = "selectedColumn" });
                symbolLayout.Controls.Add(symbolGrid, 0, 1);

                var symbolButtons = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes
                };
                symbolButtons.Controls.Add(CreateButton("انتخاب همه", 120));
                symbolButtons.Controls.Add(CreateButton("عدم انتخاب همه", 140));
                symbolLayout.Controls.Add(symbolButtons, 0, 2);
                symbolGroup.Controls.Add(symbolLayout);
                mainPanel.Controls.Add(symbolGroup, 0, 3);

                var previewGroup = new GroupBox
                {
                    Text = "پیش‌نمایش داده",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8),
                    RightToLeft = RightToLeft.Yes
                };
                var previewGrid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    AutoGenerateColumns = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    RightToLeft = RightToLeft.Yes
                };
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نماد", Width = 120, Name = "previewSymbol" });
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام فایل", Width = 220, Name = "previewFile" });
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ستون‌های موجود در فایل", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Name = "previewColumns" });
                previewGroup.Controls.Add(previewGrid);
                mainPanel.Controls.Add(previewGroup, 0, 4);

                var mappingGroup = new GroupBox
                {
                    Text = "Mapping ستون‌های داده",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8),
                    RightToLeft = RightToLeft.Yes
                };
                var mappingGrid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = false,
                    AutoGenerateColumns = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    RightToLeft = RightToLeft.Yes
                };
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "فیلد استاندارد", Width = 240, Name = "standardField", ReadOnly = true });
                mappingGrid.Columns.Add(new DataGridViewComboBoxColumn { HeaderText = "ستون فایل", Width = 220, Name = "fileColumn" });
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "توضیحات", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Name = "mappingDescription" });
                foreach (var field in new[]
                {
                    "نماد", "تاریخ", "زمان", "Open", "High", "Low", "Close", "حجم",
                    "قیمت پایانی امروز", "قیمت پایانی دیروز", "تعداد معامله", "ارزش معاملات",
                    "تعداد سهم", "ارزش بازار", "نماد انگلیسی"
                })
                {
                    mappingGrid.Rows.Add(field, "", "");
                }
                mappingGroup.Controls.Add(mappingGrid);
                mainPanel.Controls.Add(mappingGroup, 0, 5);

                var mappingTestPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes,
                    Padding = new Padding(0, 7, 0, 0)
                };
                mappingTestPanel.Controls.Add(CreateButton("تست Mapping", 130));
                mainPanel.Controls.Add(mappingTestPanel, 0, 6);

                var bottomPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes,
                    Padding = new Padding(0, 7, 0, 0)
                };
                bottomPanel.Controls.Add(CreateButton("ذخیره سبد", 125));
                bottomPanel.Controls.Add(CreateButton("لغو", 90));
                mainPanel.Controls.Add(bottomPanel, 0, 7);

                Controls.Add(mainPanel);
            }

            private static Control CreateLabeledTextBox(string text, int tabIndex)
            {
                var panel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    RightToLeft = RightToLeft.Yes
                };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                panel.Controls.Add(CreateLabel(text), 0, 0);
                panel.Controls.Add(new TextBox { Dock = DockStyle.Fill, TabIndex = tabIndex, Margin = new Padding(4) }, 1, 0);
                return panel;
            }

            private static Label CreateLabel(string text)
            {
                return new Label
                {
                    Text = text,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(4)
                };
            }

            private static RadioButton CreateRadio(string text, bool isChecked)
            {
                return new RadioButton
                {
                    Text = text,
                    AutoSize = true,
                    Checked = isChecked,
                    Margin = new Padding(10, 8, 10, 4)
                };
            }

            private static CheckBox CreateCheck(string text)
            {
                return new CheckBox
                {
                    Text = text,
                    AutoSize = true,
                    Margin = new Padding(8, 8, 8, 4)
                };
            }

            private static ComboBox CreateCombo(string[] items)
            {
                var combo = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Margin = new Padding(4)
                };
                combo.Items.AddRange(items);
                return combo;
            }

            private static Button CreateButton(string text, int width)
            {
                return new Button
                {
                    Text = text,
                    Width = width,
                    Height = 36,
                    Margin = new Padding(4)
                };
            }
        }
    }
}
