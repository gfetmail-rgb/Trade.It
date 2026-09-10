namespace Trade.It
{
    public partial class MainForm
    {
        private readonly bool stockGridEditingInitialization = InitializeStockGridEditing();

        private bool InitializeStockGridEditing()
        {
            Load += (_, _) =>
            {
                stocksDataGridView.ReadOnly = false;
                selectColumn.ReadOnly = false;
            };

            return true;
        }
    }
}
