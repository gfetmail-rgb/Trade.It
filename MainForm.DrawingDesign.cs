namespace Trade.It
{
    public partial class MainForm
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitializeChartDrawingTools();
        }
    }
}
