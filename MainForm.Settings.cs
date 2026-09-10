namespace Trade.It
{
    public partial class MainForm
    {
        private ChartDisplayMode chartDisplayMode = ChartDisplayMode.SeparateTabs;
        private bool settingsMenuInitialized;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (settingsMenuInitialized)
                return;

            settingsMenuInitialized = true;
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
