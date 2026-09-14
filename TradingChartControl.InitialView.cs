namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private static readonly Dictionary<TradingChartControl, (int Count, DateTime FirstDate, DateTime LastDate, int CandleCount)> initialViewState = new();
        private static bool initialViewWatcherInitialized;

        private static void EnsureInitialViewWatcher()
        {
            if (initialViewWatcherInitialized)
                return;

            initialViewWatcherInitialized = true;
            Application.Idle += ApplyConfiguredInitialViewToCharts;
        }

        private static void ApplyConfiguredInitialViewToCharts(object? sender, EventArgs e)
        {
            EnsureInitialViewWatcher();

            foreach (Form form in Application.OpenForms)
            {
                if (form.IsDisposed)
                    continue;

                foreach (Control control in EnumerateControls(form))
                {
                    if (control is not TradingChartControl chart || chart.IsDisposed || chart.points.Count == 0)
                        continue;

                    var state = (
                        chart.points.Count,
                        chart.points[0].Date,
                        chart.points[^1].Date,
                        ChartAppearanceSettings.InitialVisibleCandleCount);

                    if (initialViewState.TryGetValue(chart, out var previous) && previous == state)
                        continue;

                    chart.ApplyConfiguredInitialView();
                    initialViewState[chart] = state;
                }
            }

            var disposedCharts = initialViewState.Keys.Where(chart => chart.IsDisposed).ToList();
            foreach (var chart in disposedCharts)
                initialViewState.Remove(chart);
        }

        private static IEnumerable<Control> EnumerateControls(Control root)
        {
            yield return root;
            foreach (Control child in root.Controls)
            {
                foreach (var descendant in EnumerateControls(child))
                    yield return descendant;
            }
        }

        private void ApplyConfiguredInitialView()
        {
            var count = Math.Clamp(ChartAppearanceSettings.InitialVisibleCandleCount, 10, 5000);
            visibleCount = Math.Min(count, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0;
            horizontalPanOffset = 0;
            chartPanCompensation = 0;
            crosshairIndex = -1;
            EnsureChartPanCompensation();
            Invalidate();
        }

        static TradingChartControl()
        {
            EnsureInitialViewWatcher();
        }
    }
}