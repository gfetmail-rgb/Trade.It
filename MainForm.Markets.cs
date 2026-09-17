namespace Trade.It
{
    public partial class MainForm
    {
        private void RestoreAppliedMarketFilters()
        {
            marketExchangeComboBox.SelectedItem = appliedMarketExchange;
            marketTypeComboBox.SelectedItem = appliedMarketType;
            marketBoardComboBox.SelectedItem = appliedMarketBoard;
            marketAssetComboBox.SelectedItem = appliedMarketAsset;
            marketFundTypeComboBox.SelectedItem = appliedMarketFundType;
            marketIndustryGroupComboBox.SelectedItem = appliedMarketIndustryGroup;
        }
    }
}
