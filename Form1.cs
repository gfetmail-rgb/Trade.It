namespace Trade.It
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var identifierButtonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(4),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                RightToLeft = RightToLeft.Yes
            };

            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "جدید",
                Width = 58,
                Height = 34,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "ذخیره",
                Width = 65,
                Height = 34,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "حذف",
                Width = 58,
                Height = 34,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "حذف همه",
                Width = 78,
                Height = 34,
                Margin = new Padding(3)
            });
            identifierButtonsPanel.Controls.Add(new Button
            {
                Text = "ورود از اکسل",
                Width = 105,
                Height = 34,
                Margin = new Padding(3)
            });

            identifierMainGroup.Controls.Add(identifierButtonsPanel);
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
