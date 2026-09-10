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

        public SettingsForm(ChartDisplayMode currentMode)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ChartDisplayMode = currentMode;
        }

        private void okButton_Click(object? sender, EventArgs e)
        {
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
