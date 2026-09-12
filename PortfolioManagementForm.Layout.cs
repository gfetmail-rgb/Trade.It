namespace Trade.It
{
    public partial class PortfolioManagementForm
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // فقط جای Control Box را تغییر می‌دهیم؛ جای هیچ کنترل داخلی فرم عوض نمی‌شود.
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }
    }
}
