namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private int drawingSyncFirstIndex;
        private bool drawingSyncInitialized;

        private void SyncDrawingCoordinatesToView()
        {
            if (!drawingSyncInitialized)
            {
                drawingSyncFirstIndex = firstIndex;
                drawingSyncInitialized = true;
                return;
            }

            var delta = drawingSyncFirstIndex - firstIndex;
            if (delta == 0)
                return;

            foreach (var drawing in drawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
                drawing.X3 += delta;
            }

            foreach (var drawing in advancedDrawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
            }

            foreach (var drawing in extraDrawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
                drawing.X3 += delta;
            }

            drawingSyncFirstIndex = firstIndex;
        }
    }
}
