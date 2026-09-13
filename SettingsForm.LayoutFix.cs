namespace Trade.It
{
    public partial class SettingsForm
    {
        private static readonly bool marginLayoutFixInitialized = InitializeMarginLayoutFix();

        private static bool InitializeMarginLayoutFix()
        {
            Application.Idle += FixMarginLayoutOnIdle;
            return true;
        }

        private static void FixMarginLayoutOnIdle(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is not SettingsForm settings || settings.IsDisposed)
                    continue;

                if (settings.chartMarginGroupBox != null && settings.chartTopEmptyPercentTextBox != null)
                    settings.chartRightEmptyPercentHintLabel.Visible = false;
            }
        }
    }
}