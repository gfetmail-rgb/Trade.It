namespace Trade.It;

public partial class MainForm
{
    private bool marketEventsAttached;

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (marketEventsAttached)
            return;

        marketEventsAttached = true;
        marketsTabPage.Enter += MarketsTabPage_Enter;
        controlTabControl.SelectedIndexChanged += MarketsTabControl_SelectedIndexChanged;

        if (controlTabControl.SelectedTab == marketsTabPage)
            RestoreAppliedMarketFilters();
    }

    private void MarketsTabPage_Enter(object? sender, EventArgs e)
    {
        RestoreAppliedMarketFilters();
    }

    private void MarketsTabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (controlTabControl.SelectedTab == marketsTabPage)
            RestoreAppliedMarketFilters();
    }
}
