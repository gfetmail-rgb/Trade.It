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
                MinimumSize = new Size(760, 760);
                Size = new Size(860, 980);
                Font = new Font("Segoe UI", 10F);

                var mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 11,
                    Padding = new Padding(14),
                    AutoScroll = true,
                    RightToLeft = RightToLeft.Yes
                };

                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));

                mainPanel.Controls.Add(CreateLabeledTextBox("نام سبد:", 0), 0, 0);
                mainPanel.Controls.Add(CreateLabeledTextBox("منبع نام نماد", 1), 0, 1);
                mainPanel.Controls.Add(CreateLabeledTextBox("نام فایل", 2), 0, 2);
                mainPanel.Controls.Add(CreateLabeledTextBox("داخل فایل", 3), 0, 3);

                var dataSourceLabel = new Label
                {
                    Text = "منبع داده: فایل‌های موجود در مسیر انتخاب‌شده",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(4)
                };
                mainPanel.Controls.Add(dataSourceLabel, 0, 4);

                var folderButtonPanel = new Panel { Dock = DockStyle.Fill };
                folderButtonPanel.Controls.Add(new Button
                {
                    Text = "انتخاب پوشه...",
                    Width = 145,
                    Height = 36,
                    Location = new Point(0, 5)
                });
                mainPanel.Controls.Add(folderButtonPanel, 0, 5);

                var selectionButtons = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes,
                    Padding = new Padding(0, 4, 0, 0)
                };
                selectionButtons.Controls.Add(CreateButton("انتخاب سهم", 125));
                selectionButtons.Controls.Add(CreateButton("انتخاب همه", 125));
                mainPanel.Controls.Add(selectionButtons, 0, 6);

                var sourceGridGroup = new GroupBox
                {
                    Text = "سهام موجود در پوشه",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8)
                };
                var sourceGrid = new DataGridView
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
                sourceGrid.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    HeaderText = "انتخاب",
                    Width = 70,
                    Name = "selectColumn"
                });
                sourceGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "نماد",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    Name = "symbolColumn"
                });
                sourceGridGroup.Controls.Add(sourceGrid);
                mainPanel.Controls.Add(sourceGridGroup, 0, 7);

                var previewGridGroup = new GroupBox
                {
                    Text = "پیش‌نمایش سهام پوشه",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8)
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
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "نماد",
                    Width = 120,
                    Name = "previewSymbolColumn"
                });
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "نام شرکت",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    Name = "previewNameColumn"
                });
                previewGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "وضعیت",
                    Width = 110,
                    Name = "previewStatusColumn"
                });
                previewGridGroup.Controls.Add(previewGrid);
                mainPanel.Controls.Add(previewGridGroup, 0, 8);

                var mappingGroup = new GroupBox
                {
                    Text = "Mapping ستون‌های داده",
                    Dock = DockStyle.Fill,
                    Padding = new Padding(8)
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
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ستون فایل",
                    Width = 180,
                    Name = "fileColumn"
                });
                mappingGrid.Columns.Add(new DataGridViewComboBoxColumn
                {
                    HeaderText = "ستون استاندارد داده",
                    Width = 220,
                    Name = "standardColumn"
                });
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "نوع داده",
                    Width = 130,
                    Name = "dataTypeColumn"
                });
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "توضیحات",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    Name = "mappingDescriptionColumn"
                });
                mappingGroup.Controls.Add(mappingGrid);
                mainPanel.Controls.Add(mappingGroup, 0, 9);

                var bottomPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    RightToLeft = RightToLeft.Yes,
                    Padding = new Padding(0, 8, 0, 0)
                };
                bottomPanel.Controls.Add(CreateButton("لغو", 90));
                bottomPanel.Controls.Add(CreateButton("ذخیره سبد", 120));
                bottomPanel.Controls.Add(CreateButton("Mapping", 110));
                bottomPanel.Controls.Add(CreateButton("تست", 90));
                mainPanel.Controls.Add(bottomPanel, 0, 10);

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
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                panel.Controls.Add(new Label
                {
                    Text = text,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(4)
                }, 0, 0);
                panel.Controls.Add(new TextBox
                {
                    Dock = DockStyle.Fill,
                    TabIndex = tabIndex,
                    Margin = new Padding(4)
                }, 1, 0);
                return panel;
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
