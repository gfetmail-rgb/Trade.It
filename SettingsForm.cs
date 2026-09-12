namespace Trade.It
{
    public enum ChartDisplayMode
    {
        SeparateTabs,
        SingleTab
    }

    public partial class SettingsForm : Form
    {
        public ChartDisplayMode ChartDisplayMode
        {
            get => separateTabsRadioButton.Checked ? ChartDisplayMode.SeparateTabs : ChartDisplayMode.SingleTab;
            set
            {
                separateTabsRadioButton.Checked = value == ChartDisplayMode.SeparateTabs;
                singleTabRadioButton.Checked = value == ChartDisplayMode.SingleTab;
            }
        }

        public double ChartRightEmptyPercent
        {
            get => double.TryParse(chartRightEmptyPercentTextBox.Text.Trim(), out var value) ? value : 25.0;
            set => chartRightEmptyPercentTextBox.Text = value.ToString("0.##");
        }

        public SettingsForm(ChartDisplayMode currentMode, double chartRightEmptyPercent = 25.0)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ChartDisplayMode = currentMode;
            ChartRightEmptyPercent = chartRightEmptyPercent;
        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            if (ChartRightEmptyPercent < 0 || ChartRightEmptyPercent > 90)
            {
                MessageBox.Show(this, "درصد فضای خالی سمت راست باید بین ۰ تا ۹۰ باشد.", "تنظیمات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chartRightEmptyPercentTextBox.Focus();
                chartRightEmptyPercentTextBox.SelectAll();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
