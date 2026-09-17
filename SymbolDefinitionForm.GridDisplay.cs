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

        if (symbolsDataGridView.Rows[e.RowIndex].Tag is not SymbolDefinition item)
            return;

        if (e.ColumnIndex == symbolsDataGridView.Columns["fundTypeColumn"].Index)
            e.Value = string.IsNullOrWhiteSpace(item.FundType) ? SymbolDefinitionRules.EmptyOption : item.FundType;
        else if (e.ColumnIndex == symbolsDataGridView.Columns["industryGroupColumn"].Index)
            e.Value = string.IsNullOrWhiteSpace(item.IndustryGroup) ? SymbolDefinitionRules.EmptyOption : item.IndustryGroup;
    }
}
