namespace Trade.It
{
    public partial class MainForm
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            resetMenuItem.Visible = false;
        }
    }
}
