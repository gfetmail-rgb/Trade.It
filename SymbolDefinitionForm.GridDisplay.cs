namespace Trade.It;

public sealed partial class SymbolDefinitionForm
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        symbolsDataGridView.CellFormatting += SymbolsDataGridView_CellFormatting;
    }

    private void SymbolsDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= symbolsDataGridView.Rows.Count)
            return;

        if (symbolsDataGridView.Rows[e.RowIndex].Tag is not SymbolDefinition)
            return;
    }
}
