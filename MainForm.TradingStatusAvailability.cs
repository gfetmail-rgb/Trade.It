namespace Trade.It
{
    public partial class MainForm
    {
        private bool tradingStatusAvailabilityInitialized;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (tradingStatusAvailabilityInitialized)
                return;

            tradingStatusAvailabilityInitialized = true;
            portfolioComboBox.SelectedIndexChanged += TradingStatusAvailabilityChanged;
            refreshButton.Click += TradingStatusAvailabilityChanged;
            UpdateTradingStatusFilterAvailabilityOnly();
        }

        private void TradingStatusAvailabilityChanged(object? sender, EventArgs e)
        {
            UpdateTradingStatusFilterAvailabilityOnly();
        }

        private void UpdateTradingStatusFilterAvailabilityOnly()
        {
            var hasDate = false;

            if (!string.IsNullOrWhiteSpace(displayedPortfolioName) &&
                loadedPortfolios.TryGetValue(displayedPortfolioName, out var definition))
            {
                hasDate = !definition.NoDateTime &&
                          (GetMappingColumn(definition, "تاریخ") > 0 ||
                           GetMappingColumn(definition, "تاریخ لاتین") > 0);
            }

            tradingStatusGroup.Enabled = hasDate;
        }
    }
}
