using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal enum TradingChartType
    {
        Candlestick,
        Line,
        Bar
    }

    internal sealed class TradingChartPoint
    {
        public DateTime Date { get; init; }
        public double Open { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Close { get; init; }
    }

    internal sealed class TradingChartControl : Control
    {
        private readonly List<TradingChartPoint> points = new();
        private TradingChartType chartType = TradingChartType.Candlestick;
        private int visibleCount;
        private int firstIndex;

        private bool panning;
        private Point panStartPoint;
        private int panStartFirstIndex;
        private double panStartVerticalPanOffset;
        private double panStartVerticalRange;

        private bool horizontalAxisDrag;
        private Point horizontalAxisStartPoint;
        private int horizontalAxisStartVisibleCount;
        private double horizontalAxisCenterIndex;

        private bool verticalAxisDrag;
        private Point verticalAxisStartPoint;

        private bool showGrid;
        private bool showCrosshair = true;
        private Point crosshairPoint;
        private double verticalZoom = 1.0;
        private double verticalPanOffset;

        public TradingChartControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ForeColor = Color.Black;
            ResizeRedraw = true;
            MinimumSize = new Size(200, 150);
            SetStyle(ControlStyles.Selectable, true);
        }

        public void SetData(IEnumerable<TradingChartPoint> data)
        {
            points.Clear();
            points.AddRange(data.OrderBy(x => x.Date));
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            showCrosshair = true;
            Invalidate();
        }

        public void SetChartType(TradingChartType type)
        {
            chartType = type;
            Invalidate();
        }

        public void ToggleGrid()
        {
            showGrid = !showGrid;
            Invalidate();
        }

        public bool GridVisible => showGrid;

        public void ToggleCrosshair()
        {
            showCrosshair = !showCrosshair;
            Invalidate();
        }

        public bool CrosshairVisible => showCrosshair;

        public void ResetView()
        {
            visibleCount = Math.Min(200, Math.Max(1, points.Count));
            firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            Invalidate();
        }

        // X zoom keeps the right edge (latest data) fixed and opens/closes only from the left.
        public void ZoomX(double factor)
        {
            if (points.Count < 2)
                return;

            var oldCount = Math.Max(2, visibleCount);
            var newCount = Math.Clamp((int)Math.Round(oldCount * factor), 2, points.Count);
            if (newCount == oldCount)
                return;

            visibleCount = newCount;
            firstIndex = Math.Max(0, points.Count - newCount);
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ZoomX(e.Delta > 0 ? 0.80 : 1.25);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            var plotLeft = 55;
            var plotBottom = Height - 35;
            if (e.Button == MouseButtons.Left && e.X <= plotLeft && e.Y <= plotBottom)
            {
                FitVerticalRange();
            }
        }

        private void FitVerticalRange()
        {
            if (points.Count == 0)
                return;

            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;

            verticalZoom = 1.0;
            verticalPanOffset = 0.0;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;

            var plotLeft = 55;
            var plotBottom = Height - 35;
            horizontalAxisDrag = e.Y >= plotBottom && e.X >= plotLeft;
            verticalAxisDrag = e.X <= plotLeft && e.Y <= plotBottom;

            if (horizontalAxisDrag)
            {
                panning = false;
                horizontalAxisStartPoint = e.Location;
                horizontalAxisStartVisibleCount = Math.Max(2, visibleCount);
                horizontalAxisCenterIndex = firstIndex + horizontalAxisStartVisibleCount / 2.0;
                Capture = true;
                Cursor = Cursors.SizeWE;
                return;
            }

            if (verticalAxisDrag)
            {
                panning = false;
                verticalAxisStartPoint = e.Location;
                Capture = true;
                Cursor = Cursors.SizeNS;
                return;
            }

            if (points.Count > 1)
            {
                panning = true;
                panStartPoint = e.Location;
                panStartFirstIndex = firstIndex;
                panStartVerticalPanOffset = verticalPanOffset;

                var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
                var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
                if (visible.Count > 0)
                {
                    var rawRange = visible.Max(x => x.High) - visible.Min(x => x.Low);
                    panStartVerticalRange = Math.Max(rawRange, 1e-9) / verticalZoom * 1.10;
                }
                else
                {
                    panStartVerticalRange = 1.0;
                }

                Capture = true;
                Cursor = Cursors.SizeAll;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (showCrosshair)
            {
                crosshairPoint = e.Location;
                Invalidate();
            }

            if (horizontalAxisDrag && Capture && points.Count > 1)
            {
                // Horizontal-axis drag zooms around the exact middle of the view.
                // The center stays fixed while the visible range opens/closes in both directions.
                var delta = e.X - horizontalAxisStartPoint.X;
                var factor = Math.Exp(-delta / 300.0);
                var newCount = Math.Clamp(
                    (int)Math.Round(horizontalAxisStartVisibleCount * factor),
                    2,
                    points.Count);

                visibleCount = newCount;
                firstIndex = Math.Clamp(
                    (int)Math.Round(horizontalAxisCenterIndex - newCount / 2.0),
                    0,
                    Math.Max(0, points.Count - newCount));

                Invalidate();
                return;
            }

            if (verticalAxisDrag && Capture && points.Count > 1)
            {
                var delta = verticalAxisStartPoint.Y - e.Y;
                verticalZoom = Math.Clamp(
                    verticalZoom * Math.Exp(-delta / 260.0),
                    0.15,
                    8.0);
                Invalidate();
                return;
            }

            if (panning && Capture && points.Count > 1)
            {
                // Horizontal movement pans through the candles; it does not zoom.
                var horizontalDelta = e.X - panStartPoint.X;
                var step = (int)Math.Round(
                    horizontalDelta * visibleCount /
                    (double)Math.Max(1, Width - 70));
                firstIndex = Math.Clamp(
                    panStartFirstIndex - step,
                    0,
                    Math.Max(0, points.Count - visibleCount));

                // Vertical movement translates the price range; it does not change zoom.
                var verticalDelta = panStartPoint.Y - e.Y;
                if (Math.Abs(verticalDelta) >= 0.5)
                {
                    var plotHeight = Math.Max(1, Height - 50);
                    verticalPanOffset = panStartVerticalPanOffset +
                        verticalDelta * panStartVerticalRange / plotHeight;
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                panning = false;
                horizontalAxisDrag = false;
                verticalAxisDrag = false;
                Capture = false;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);

            if (points.Count == 0)
                return;

            var left = 55;
            var top = 15;
            var bottom = 35;
            // Keep 25% of the usable horizontal area empty on the right side of the chart.
            var availableWidth = Math.Max(1, Width - left - 15);
            var right = 15 + (int)Math.Round(availableWidth * 0.25);
            var plot = new Rectangle(left, top, Math.Max(1, Width - left - right), Math.Max(1, Height - top - bottom));
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;

            var min = visible.Min(x => x.Low);
            var max = visible.Max(x => x.High);
            if (max <= min)
            {
                max += 1;
                min -= 1;
            }

            var center = (max + min) / 2.0 + verticalPanOffset;
            var halfRange = (max - min) / 2.0 / verticalZoom;
            min = center - halfRange;
            max = center + halfRange;
            var margin = (max - min) * 0.05;
            min -= margin;
            max += margin;

            using var gridPen = new Pen(Color.FromArgb(225, 225, 225), 1);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(Color.FromArgb(35, 150, 80));
            using var fallingBrush = new SolidBrush(Color.FromArgb(205, 70, 70));
            using var linePen = new Pen(Color.FromArgb(35, 90, 160), 1.6f);

            if (showGrid)
            {
                for (var i = 0; i <= 5; i++)
                {
                    var y = plot.Top + plot.Height * i / 5f;
                    e.Graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                }

                var verticalGridCount = Math.Min(10, Math.Max(2, visible.Count));
                for (var i = 0; i <= verticalGridCount; i++)
                {
                    var x = plot.Left + plot.Width * i / (float)verticalGridCount;
                    e.Graphics.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
                }
            }

            for (var i = 0; i <= 5; i++)
            {
                var y = plot.Top + plot.Height * i / 5f;
                var value = max - (max - min) * i / 5.0;
                e.Graphics.DrawString(value.ToString("0.##"), Font, textBrush, 4, y - Font.Height / 2f);
            }

            e.Graphics.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            var step = plot.Width / (double)Math.Max(1, visible.Count);
            double X(int index) => plot.Left + step * (index + 0.5);
            double Y(double value) => plot.Bottom - (value - min) / (max - min) * plot.Height;

            if (chartType == TradingChartType.Line)
            {
                for (var i = 1; i < visible.Count; i++)
                    e.Graphics.DrawLine(linePen, (float)X(i - 1), (float)Y(visible[i - 1].Close), (float)X(i), (float)Y(visible[i].Close));
            }
            else if (chartType == TradingChartType.Bar)
            {
                var tick = Math.Max(2, (int)Math.Round(step * 0.25));
                using var upPen = new Pen(Color.FromArgb(35, 150, 80), 1.2f);
                using var downPen = new Pen(Color.FromArgb(205, 70, 70), 1.2f);
                for (var i = 0; i < visible.Count; i++)
                {
                    var p = visible[i];
                    var x = (float)X(i);
                    var pen = p.Close >= p.Open ? upPen : downPen;
                    e.Graphics.DrawLine(pen, x, (float)Y(p.High), x, (float)Y(p.Low));
                    e.Graphics.DrawLine(pen, x - tick, (float)Y(p.Open), x, (float)Y(p.Open));
                    e.Graphics.DrawLine(pen, x, (float)Y(p.Close), x + tick, (float)Y(p.Close));
                }
            }
            else
            {
                var bodyWidth = Math.Max(3, Math.Min(16, step * 0.65));
                using var wickUpPen = new Pen(Color.FromArgb(35, 150, 80), 1.2f);
                using var wickDownPen = new Pen(Color.FromArgb(205, 70, 70), 1.2f);
                for (var i = 0; i < visible.Count; i++)
                {
                    var p = visible[i];
                    var x = (float)X(i);
                    var rising = p.Close >= p.Open;
                    var yHigh = (float)Y(p.High);
                    var yLow = (float)Y(p.Low);
                    var yOpen = (float)Y(p.Open);
                    var yClose = (float)Y(p.Close);
                    var topBody = Math.Min(yOpen, yClose);
                    var bodyHeight = Math.Max(1, Math.Abs(yClose - yOpen));
                    var rect = new RectangleF((float)(x - bodyWidth / 2), topBody, (float)bodyWidth, bodyHeight);
                    e.Graphics.DrawLine(rising ? wickUpPen : wickDownPen, x, yHigh, x, yLow);
                    e.Graphics.FillRectangle(rising ? risingBrush : fallingBrush, rect);
                    e.Graphics.DrawRectangle(rising ? wickUpPen : wickDownPen, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }

            var labelCount = Math.Min(6, visible.Count);
            for (var i = 0; i < labelCount; i++)
            {
                var index = labelCount == 1 ? 0 : (int)Math.Round(i * (visible.Count - 1.0) / (labelCount - 1));
                var x = (float)X(index);
                var text = visible[index].Date.ToString("yyyy/MM/dd");
                var size = e.Graphics.MeasureString(text, Font);
                e.Graphics.DrawString(text, Font, textBrush, x - size.Width / 2, plot.Bottom + 7);
            }

            if (showCrosshair)
            {
                using var crossPen = new Pen(Color.FromArgb(100, 80, 80, 80), 1) { DashStyle = DashStyle.Dash };
                var x = Math.Clamp(crosshairPoint.X, plot.Left, plot.Right);
                var y = Math.Clamp(crosshairPoint.Y, plot.Top, plot.Bottom);
                e.Graphics.DrawLine(crossPen, plot.Left, y, plot.Right, y);
                e.Graphics.DrawLine(crossPen, x, plot.Top, x, plot.Bottom);
            }
        }
    }
}
