namespace Trade.It
{
    public partial class PortfolioManagementForm
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // در فرم راست‌به‌چپ، Control Box (بستن/بیشینه/کمینه) در سمت چپ قرار بگیرد.
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }
    }
}
