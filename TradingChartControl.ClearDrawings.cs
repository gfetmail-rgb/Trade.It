namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        public void ClearAllDrawings()
        {
            CancelDrawing();
            CancelAdvancedDrawing();
            CancelExtraDrawing();

            drawings.Clear();
            advancedDrawings.Clear();
            extraDrawings.Clear();

            selectedDrawingIndex = -1;
            selectedAdvancedDrawingIndex = -1;
            selectedExtraDrawingIndex = -1;

            draggingDrawingIndex = -1;
            draggingHandle = 0;
            draggingAdvancedDrawingIndex = -1;
            draggingAdvancedHandle = 0;
            extraDraggingDrawingIndex = -1;
            extraDraggingHandle = 0;
            extraDraggingHandleActive = false;

            Capture = false;
            Cursor = Cursors.Default;
            Invalidate();
        }
    }
}
