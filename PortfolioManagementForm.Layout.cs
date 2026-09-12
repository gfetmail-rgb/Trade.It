namespace Trade.It
{
    public partial class PortfolioManagementForm
    {
        protected override void OnCreateControl(EventArgs e)
        {
            base.OnCreateControl(e);

            // Move the portfolio-parameter GroupBox from the right side to the left.
            groupBox1.Location = new Point(20, 12);

            // Move the existing-portfolios ListBox and its title to the right so the two areas do not overlap.
            existingTitleLabel.Location = new Point(1050, 12);
            portfoliosListBox.Location = new Point(1045, 49);
        }
    }
}
