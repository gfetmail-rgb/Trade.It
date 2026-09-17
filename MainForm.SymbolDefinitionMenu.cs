namespace Trade.It
{
    public partial class MainForm
    {
        private void symbolDefinitionMenuItem_Click(object? sender, EventArgs e)
        {
            using var form = new SymbolDefinitionForm();
            form.ShowDialog(this);
        }
    }
}
