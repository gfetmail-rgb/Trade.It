namespace Trade.It
{
    public partial class MainForm
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (stocksDataGridView.ContainsFocus)
            {
                var keyCode = keyData & Keys.KeyCode;
                if (keyCode == Keys.Up ||
                    keyCode == Keys.Down ||
                    keyCode == Keys.Enter ||
                    keyCode == Keys.Space)
                {
                    NavigateStockGridByKeyboard(keyCode);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NavigateStockGridByKeyboard(Keys keyCode)
        {
            if (stocksDataGridView.Rows.Count == 0)
                return;

            var currentRow = stocksDataGridView.CurrentCell?.RowIndex
                ?? stocksDataGridView.CurrentRow?.Index
                ?? -1;

            if (currentRow < 0 || currentRow >= stocksDataGridView.Rows.Count)
                return;

            var targetRow = keyCode == Keys.Up
                ? Math.Max(0, currentRow - 1)
                : Math.Min(stocksDataGridView.Rows.Count - 1, currentRow + 1);

            var targetCell = stocksDataGridView.Rows[targetRow].Cells[symbolColumn.Index];
            stocksDataGridView.CurrentCell = targetCell;
            stocksDataGridView.ClearSelection();
            stocksDataGridView.Rows[targetRow].Selected = true;

            var symbol = Convert.ToString(targetCell.Value)?.Trim();
            if (!string.IsNullOrWhiteSpace(symbol))
                ShowSymbolChart(symbol);
        }
    }
}
