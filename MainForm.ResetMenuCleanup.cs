namespace Trade.It
{
    public partial class MainForm
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // «بازنشانی» هیچ رویداد یا عملکردی ندارد؛ آن را از منوی قابل مشاهده حذف می‌کنیم.
            resetMenuItem.Visible = false;
        }
    }
}
