namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private bool extraDrawingSafetyInitialized;
        private bool extraSafetyWasActive;
        private bool extraSafetyWasInProgress;
        private int extraSafetyPointCount;

        public void EnableExtraDrawingMouseSafety()
        {
            if (!extraDrawingSafetyInitialized)
            {
                extraDrawingSafetyInitialized = true;
                MouseDown += ExtraDrawingSafety_MouseDown;
            }

            extraSafetyWasActive = ExtraDrawingActive;
            extraSafetyWasInProgress = extraDrawingInProgress;
            extraSafetyPointCount = extraDrawingPoints.Count;
        }

        private void ExtraDrawingSafety_MouseDown(object? sender, MouseEventArgs e)
        {
            // ExtraDrawing_MouseDown runs before TradingChartControl.OnMouseDown.
            // When the third point completes a three-point tool, CancelExtraDrawing()
            // clears extraInputHandled, allowing OnMouseDown to fall through into
            // the normal chart/ruler state machine. Consume that click explicitly.
            if (e.Button == MouseButtons.Left &&
                extraSafetyWasActive &&
                extraSafetyWasInProgress &&
                extraSafetyPointCount > 0)
            {
                extraInputHandled = true;
            }

            extraSafetyWasActive = ExtraDrawingActive;
            extraSafetyWasInProgress = extraDrawingInProgress;
            extraSafetyPointCount = extraDrawingPoints.Count;
        }
    }
}
