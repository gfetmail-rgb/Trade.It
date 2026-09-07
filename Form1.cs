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

            var closeAllChartsMenuItem = new ToolStripMenuItem
            {
                Name = "closeAllChartsMenuItem",
                Text = "بستن همه چارتها"
            };
            mainMenuStrip.Items.Add(closeAllChartsMenuItem);

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
    }
}
