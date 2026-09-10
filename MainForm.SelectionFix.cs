namespace Trade.It
{
    public partial class MainForm
    {
        private bool selectionStateFixAttached;

        private void AttachSelectionStateFix()
        {
            if (selectionStateFixAttached || IsDisposed)
                return;

            selectionStateFixAttached = true;
            stocksDataGridView.CellValueChanged += SelectionGridCellValueChanged;
            stocksDataGridView.CurrentCellDirtyStateChanged += SelectionGridCurrentCellDirtyStateChanged;
            UpdateSelectionControls();
        }

        private void SelectionGridCurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (stocksDataGridView.IsCurrentCellDirty &&
                stocksDataGridView.CurrentCell?.OwningColumn == selectColumn)
            {
                stocksDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void SelectionGridCellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (internalPortfolioUpdate || e.RowIndex < 0 ||
                e.ColumnIndex != selectColumn.Index)
                return;

            UpdateSelectionControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AttachSelectionStateFix();
        }

        private void UpdateSelectionControls()
        {
            if (stocksDataGridView.Rows.Count == 0)
            {
                selectAllCheckBox.Checked = false;
                selectNoneCheckBox.Checked = false;
                return;
            }

            var selected = stocksDataGridView.Rows.Cast<DataGridViewRow>()
                .Count(row => Convert.ToBoolean(row.Cells[selectColumn.Index].Value ?? false));

            var allSelected = selected == stocksDataGridView.Rows.Count;
            var noneSelected = selected == 0;

            internalPortfolioUpdate = true;
            try
            {
                selectAllCheckBox.Checked = allSelected;
                selectNoneCheckBox.Checked = noneSelected;
            }
            finally
            {
                internalPortfolioUpdate = false;
            }
        }
    }
}
