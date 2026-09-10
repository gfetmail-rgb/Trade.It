namespace Trade.It
{
    public partial class MainForm
    {
        private ChartDisplayMode chartDisplayMode = ChartDisplayMode.SeparateTabs;

        private void InitializeSettingsMenu()
        {
            settingsMenuItem.Click += SettingsMenuItem_Click;
        }

        private void SettingsMenuItem_Click(object? sender, EventArgs e)
        {
            using var form = new SettingsForm(chartDisplayMode);
            if (form.ShowDialog(this) == DialogResult.OK)
                chartDisplayMode = form.ChartDisplayMode;
        }
    }
}
