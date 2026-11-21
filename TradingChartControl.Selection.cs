namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private enum DrawingSelectionKind
        {
            Standard,
            Advanced,
            Extra
        }

        private readonly HashSet<(DrawingSelectionKind Kind, int Index)> selectedDrawings = new();
        private bool drawingSelectionInputHandled;

        private bool IsDrawingSelected(DrawingSelectionKind kind, int index) =>
            selectedDrawings.Contains((kind, index));

        private void ClearDrawingSelection()
        {
            selectedDrawings.Clear();
            selectedDrawingIndex = -1;
            selectedAdvancedDrawingIndex = -1;
            selectedExtraDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingAdvancedDrawingIndex = -1;
            extraDraggingDrawingIndex = -1;
            draggingHandle = 0;
            draggingAdvancedHandle = 0;
            extraDraggingHandle = 0;
            extraDraggingHandleActive = false;
            Capture = false;
        }

        private void SelectDrawing(DrawingSelectionKind kind, int index, bool toggle)
        {
            if (!toggle && !IsDrawingSelected(kind, index))
                ClearDrawingSelection();

            var key = (kind, index);
            if (toggle && selectedDrawings.Contains(key))
                selectedDrawings.Remove(key);
            else
                selectedDrawings.Add(key);

            selectedDrawingIndex = selectedDrawings
                .Where(x => x.Kind == DrawingSelectionKind.Standard)
                .Select(x => x.Index).DefaultIfEmpty(-1).Max();
            selectedAdvancedDrawingIndex = selectedDrawings
                .Where(x => x.Kind == DrawingSelectionKind.Advanced)
                .Select(x => x.Index).DefaultIfEmpty(-1).Max();
            selectedExtraDrawingIndex = selectedDrawings
                .Where(x => x.Kind == DrawingSelectionKind.Extra)
                .Select(x => x.Index).DefaultIfEmpty(-1).Max();
        }

        private void DeleteSelectedDrawings()
        {
            if (selectedDrawings.Count == 0)
                return;

            foreach (var index in selectedDrawings
                         .Where(x => x.Kind == DrawingSelectionKind.Standard)
                         .Select(x => x.Index).Where(x => x >= 0 && x < drawings.Count)
                         .OrderByDescending(x => x))
                drawings.RemoveAt(index);

            foreach (var index in selectedDrawings
                         .Where(x => x.Kind == DrawingSelectionKind.Advanced)
                         .Select(x => x.Index).Where(x => x >= 0 && x < advancedDrawings.Count)
                         .OrderByDescending(x => x))
                advancedDrawings.RemoveAt(index);

            foreach (var index in selectedDrawings
                         .Where(x => x.Kind == DrawingSelectionKind.Extra)
                         .Select(x => x.Index).Where(x => x >= 0 && x < extraDrawings.Count)
                         .OrderByDescending(x => x))
                extraDrawings.RemoveAt(index);

            ClearDrawingSelection();
            extraDraggingHandleActive = false;
            extraInputHandled = false;
            Cursor = Cursors.Default;
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }
}