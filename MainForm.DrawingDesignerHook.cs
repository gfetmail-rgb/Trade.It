namespace Trade.It
{
    public partial class MainForm
    {
        // Attach before InitializeComponent so the existing HandleCreated override can
        // initialize the drawing toolbar after all Designer controls have been created.
        private readonly bool drawingToolsHandleCreatedHook = AttachDrawingToolsHandleCreatedHook();

        private bool AttachDrawingToolsHandleCreatedHook()
        {
            HandleCreated += MainForm_DrawingToolsHandleCreated;
            return true;
        }

        private void MainForm_DrawingToolsHandleCreated(object? sender, EventArgs e)
        {
            InitializeChartDrawingTools();
        }
    }
}
