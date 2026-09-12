namespace Trade.It
{
    public partial class SettingsForm
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // The settings now contain several independent groups. Give the form
            // enough room so that all controls, including the OK/Cancel buttons,
            // are visible without being clipped by the old designer size.
            ClientSize = new Size(760, 920);
            MinimumSize = new Size(760, 920);
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}
