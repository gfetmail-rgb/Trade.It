namespace Trade.It
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tabPage3.Controls.Clear();

            identifierMainGroup.Dock = DockStyle.Fill;
            identifierMainGroup.Location = new Point(8, 56);
            identifierMainGroup.Size = new Size(tabPage3.ClientSize.Width - 16, tabPage3.ClientSize.Height - 64);
            identifierMainGroup.Text = "اطلاعات شناسه";

            identifierLayout.Dock = DockStyle.Fill;
            identifierLayout.Padding = new Padding(12);
            identifierLayout.RowCount = 6;
            identifierLayout.RowStyles.Clear();
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            identifierLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            identifierSymbolLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            identifierNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            identifierTsetmcLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            identifierMarketLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            identifierGroupLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            identifierDescriptionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            identifierSymbolTextBox.Font = new Font("Segoe UI", 10F);
            identifierNameTextBox.Font = new Font("Segoe UI", 10F);
            identifierTsetmcTextBox.Font = new Font("Segoe UI", 10F);
            identifierMarketComboBox.Font = new Font("Segoe UI", 10F);
            identifierGroupTextBox.Font = new Font("Segoe UI", 10F);
            identifierDescriptionTextBox.Font = new Font("Segoe UI", 10F);

            var identifierButtonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(4),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                RightToLeft = RightToLeft.Yes
            };

            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "جدید",
                Width = 62,
                Height = 36,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "ذخیره",
                Width = 70,
                Height = 36,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "حذف",
                Width = 62,
                Height = 36,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "حذف همه",
                Width = 80,
                Height = 36,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "ورود از اکسل",
                Width = 105,
                Height = 36,
                Margin = new Padding(3)
            });

            tabPage3.Controls.Add(identifierMainGroup);
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
