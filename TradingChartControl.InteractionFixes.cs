namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        // Field initializers cannot call instance methods. Keep this initialization
        // static and attach the per-control handler when the application is idle.
        private static readonly bool extraToolOutsideClickFixInitialized = InitializeExtraToolOutsideClickFix();
        private static readonly HashSet<TradingChartControl> extraToolOutsideClickFixControls = new();

        private static bool InitializeExtraToolOutsideClickFix()
        {
            Application.Idle += AttachExtraToolOutsideClickFixes;
            return true;
        }

        private static void AttachExtraToolOutsideClickFixes(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
                AttachExtraToolOutsideClickFixes(form);
        }

        private static void AttachExtraToolOutsideClickFixes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TradingChartControl chart && extraToolOutsideClickFixControls.Add(chart))
                    chart.MouseDown += chart.ExtraToolOutsideClickFix_MouseDown;

                if (control.HasChildren)
                    AttachExtraToolOutsideClickFixes(control);
            }
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
        // Field initializers cannot call instance methods. Use a static idle hook
        // so the wait cursor is cleared without changing the Designer constructor.
        private static readonly bool waitCursorFixInitialized = InitializeWaitCursorFix();

        private static bool InitializeWaitCursorFix()
        {
            Application.Idle += MainForm_ResetStuckWaitCursor;
            return true;
        }

        private static void MainForm_ResetStuckWaitCursor(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is not MainForm mainForm || mainForm.IsDisposed)
                    continue;

                if (mainForm.UseWaitCursor)
                    mainForm.UseWaitCursor = false;

                if (mainForm.Cursor == Cursors.WaitCursor)
                    mainForm.Cursor = Cursors.Default;

                if (Cursor.Current == Cursors.WaitCursor)
                    Cursor.Current = Cursors.Default;
            }
        }
    }
}