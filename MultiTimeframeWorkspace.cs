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
            if (items.Count > 0)
                SetActiveChart(items[0].Chart);
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
                var chart = items[i].Chart;
                grid.Controls.Add(chart, i % columns, i / columns);
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
