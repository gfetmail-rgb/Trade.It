using System.Drawing;

namespace Trade.It
{
    internal sealed class MultiTimeframeWorkspace : Panel
    {
        private readonly TableLayoutPanel grid = new();
        private readonly List<(TradingChartControl Chart, string TimeFrame)> items = new();
        private TradingChartControl? activeChart;
        private bool syncing;

        public event EventHandler? ActiveChartChanged;

        public MultiTimeframeWorkspace()
        {
            BackColor = SystemColors.Window;
            Padding = new Padding(2);

            grid.Dock = DockStyle.Fill;
            grid.Margin = new Padding(0);
            grid.Padding = new Padding(0);
            grid.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Controls.Add(grid);
        }

        public IReadOnlyList<TradingChartControl> Charts => items.Select(x => x.Chart).ToList();
        public int ChartCount => items.Count;
        public TradingChartControl? ActiveChart => activeChart;

        public void AddChart(TradingChartControl chart, string timeFrame)
        {
            if (items.Any(x => ReferenceEquals(x.Chart, chart)))
                return;

            chart.Dock = DockStyle.Fill;
            chart.Margin = new Padding(2);
            chart.Tag = timeFrame;

            chart.MouseEnter += Chart_MouseEnter;
            chart.MouseDown += Chart_MouseDown;
            chart.ViewChanged += Chart_ViewChanged;
            chart.CrosshairDateChanged += Chart_CrosshairDateChanged;

            items.Add((chart, timeFrame));
            RebuildLayout();

            if (activeChart == null)
                SetActiveChart(chart);
        }

        public void SelectFirstChart()
        {
            if (items.Count == 0)
                return;

            SetActiveChart(items[0].Chart);
            SynchronizeAllToActiveChart();
        }

        private void Chart_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is TradingChartControl chart)
                SetActiveChart(chart);
        }

        private void Chart_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is TradingChartControl chart)
                SetActiveChart(chart);
        }

        private void SetActiveChart(TradingChartControl chart)
        {
            if (ReferenceEquals(activeChart, chart))
                return;

            activeChart = chart;
            chart.Focus();
            ActiveChartChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RebuildLayout()
        {
            grid.Controls.Clear();
            grid.ColumnStyles.Clear();
            grid.RowStyles.Clear();

            var count = items.Count;
            if (count == 0)
                return;

            var columns = count <= 2 ? count : count <= 4 ? 2 : 3;
            var rows = (int)Math.Ceiling(count / (double)columns);

            grid.ColumnCount = columns;
            grid.RowCount = rows;

            for (var c = 0; c < columns; c++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));

            for (var r = 0; r < rows; r++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));

            for (var i = 0; i < count; i++)
            {
                var item = items[i];
                var host = new Panel
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(2),
                    Padding = new Padding(0),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = SystemColors.Window
                };

                var title = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 26,
                    Text = item.TimeFrame,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = SystemColors.Control,
                    ForeColor = SystemColors.ControlText,
                    Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold),
                    Cursor = Cursors.Default
                };

                item.Chart.Dock = DockStyle.Fill;
                item.Chart.Margin = new Padding(0);
                host.Controls.Add(item.Chart);
                host.Controls.Add(title);
                grid.Controls.Add(host, i % columns, i / columns);
            }
        }

        private void SynchronizeAllToActiveChart()
        {
            if (activeChart == null)
                return;

            var range = activeChart.GetVisibleDateRange();
            if (!range.HasValue)
                return;

            syncing = true;
            try
            {
                foreach (var item in items)
                {
                    if (!ReferenceEquals(item.Chart, activeChart))
                        item.Chart.ApplySyncedDateRange(range.Value.Start, range.Value.End);
                }
            }
            finally
            {
                syncing = false;
            }
        }

        private void Chart_ViewChanged(object? sender, EventArgs e)
        {
            if (syncing || sender is not TradingChartControl source)
                return;

            var range = source.GetVisibleDateRange();
            if (!range.HasValue)
                return;

            syncing = true;
            try
            {
                foreach (var item in items)
                {
                    if (!ReferenceEquals(item.Chart, source))
                        item.Chart.ApplySyncedDateRange(range.Value.Start, range.Value.End);
                }
            }
            finally
            {
                syncing = false;
            }
        }

        private void Chart_CrosshairDateChanged(object? sender, EventArgs e)
        {
            if (syncing || sender is not TradingChartControl source)
                return;

            var date = source.CrosshairDate;
            if (!date.HasValue)
                return;

            syncing = true;
            try
            {
                foreach (var item in items)
                {
                    if (!ReferenceEquals(item.Chart, source))
                        item.Chart.ApplySyncedCrosshairDate(date.Value);
                }
            }
            finally
            {
                syncing = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var item in items)
                {
                    item.Chart.ViewChanged -= Chart_ViewChanged;
                    item.Chart.CrosshairDateChanged -= Chart_CrosshairDateChanged;
                    item.Chart.MouseEnter -= Chart_MouseEnter;
                    item.Chart.MouseDown -= Chart_MouseDown;
                }

                grid.Dispose();
                items.Clear();
                activeChart = null;
            }

            base.Dispose(disposing);
        }
    }
}
