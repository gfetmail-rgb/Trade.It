namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        // This field initializer runs before the constructor body and lets us
        // attach an additional mouse handler without changing the main control
        // constructor. The existing extra-tool handler remains responsible for
        // placing points; this handler only cancels an active tool when the user
        // clicks outside the plot.
        private readonly bool extraToolOutsideClickFixInitialized = InitializeExtraToolOutsideClickFix();

        private bool InitializeExtraToolOutsideClickFix()
        {
            MouseDown += ExtraToolOutsideClickFix_MouseDown;
            return true;
        }

        private void ExtraToolOutsideClickFix_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !ExtraDrawingActive)
                return;

            if (!GetPlotRectangle().Contains(e.Location))
                CancelExtraDrawing();
        }
    }

    public partial class MainForm
    {
        // The existing portfolio handler briefly enables UseWaitCursor while
        // filling the grid. Latest-date loading is already asynchronous, so a
        // persistent wait cursor after the grid is populated is misleading.
        // Reset it on the next application idle cycle.
        private readonly bool waitCursorFixInitialized = InitializeWaitCursorFix();

        private bool InitializeWaitCursorFix()
        {
            Application.Idle += MainForm_ResetStuckWaitCursor;
            return true;
        }

        private void MainForm_ResetStuckWaitCursor(object? sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (UseWaitCursor)
                UseWaitCursor = false;

            if (Cursor == Cursors.WaitCursor)
                Cursor = Cursors.Default;

            if (Cursor.Current == Cursors.WaitCursor)
                Cursor.Current = Cursors.Default;
        }
    }
}