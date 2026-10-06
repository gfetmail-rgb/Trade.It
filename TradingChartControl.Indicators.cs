namespace Trade.It
{
    internal enum ChartIndicatorType
    {
        MovingAverage,
        ExponentialMovingAverage,
        Ichimoku
    }

    internal sealed class ChartIndicator
    {
        public ChartIndicatorType Type { get; init; }
        public int Period { get; set; }
        public Color LineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color BackgroundColor { get; set; } = Color.White;
        public bool ShowTenkan { get; set; } = true;
        public bool ShowKijun { get; set; } = true;
        public bool ShowSpanA { get; set; } = true;
        public bool ShowSpanB { get; set; } = true;
        public bool ShowChikou { get; set; } = true;
        public bool ShowBullishCloud { get; set; } = true;
        public bool ShowBearishCloud { get; set; } = true;
        public Color TenkanColor { get; set; } = Color.FromArgb(220, 80, 80);
        public Color KijunColor { get; set; } = Color.FromArgb(80, 100, 220);
        public Color SpanAColor { get; set; } = Color.FromArgb(50, 150, 80);
        public Color SpanBColor { get; set; } = Color.FromArgb(180, 100, 60);
        public Color ChikouColor { get; set; } = Color.FromArgb(150, 80, 180);
        public Color BullishCloudColor { get; set; } = Color.FromArgb(130, 200, 130);
        public Color BearishCloudColor { get; set; } = Color.FromArgb(230, 150, 150);
    }

    internal sealed partial class TradingChartControl
    {
        private readonly List<ChartIndicator> indicators = new();
        private int selectedIndicatorIndex = -1;

        public IReadOnlyList<ChartIndicator> Indicators => indicators;
        public int SelectedIndicatorIndex => selectedIndicatorIndex;

        public void AddMovingAverage(int period = 20)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.MovingAverage,
                Period = period,
                BackgroundColor = BackColor
            });
            Invalidate();
        }

        public void AddExponentialMovingAverage(int period = 20)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.ExponentialMovingAverage,
                Period = period,
                BackgroundColor = BackColor
            });
            Invalidate();
        }

        public void AddIchimoku()
        {
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.Ichimoku,
                Period = 9,
                BackgroundColor = BackColor
            });
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool RemoveIndicatorAt(int index)
        {
            if (index < 0 || index >= indicators.Count)
                return false;

            indicators.RemoveAt(index);
            selectedIndicatorIndex = -1;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool SetIndicatorPeriod(int index, int period)
        {
            if (index < 0 || index >= indicators.Count || points.Count < 2)
                return false;

            period = Math.Clamp(period, 2, points.Count);
            indicators[index].Period = period;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool ApplyIndicatorSettings(int index, int period, Color lineColor, Color backgroundColor)
        {
            if (index < 0 || index >= indicators.Count || points.Count < 2)
                return false;

            period = Math.Clamp(period, 2, points.Count);

            // Transparent is not supported as TradingChartControl.BackColor.
            // If an older indicator instance contains Transparent, keep the
            // chart's current background instead of assigning an invalid value.
            if (backgroundColor == Color.Transparent)
                backgroundColor = BackColor == Color.Transparent
                    ? Color.White
                    : BackColor;

            var indicator = indicators[index];
            indicator.Period = period;
            indicator.LineColor = lineColor;
            indicator.BackgroundColor = backgroundColor;
            BackColor = backgroundColor;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool ApplyIchimokuSettings(
            int index,
            bool showTenkan,
            bool showKijun,
            bool showSpanA,
            bool showSpanB,
            bool showChikou,
            bool showBullishCloud,
            bool showBearishCloud,
            Color tenkanColor,
            Color kijunColor,
            Color spanAColor,
            Color spanBColor,
            Color chikouColor,
            Color bullishCloudColor,
            Color bearishCloudColor)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.Ichimoku)
                return false;

            var indicator = indicators[index];
            indicator.ShowTenkan = showTenkan;
            indicator.ShowKijun = showKijun;
            indicator.ShowSpanA = showSpanA;
            indicator.ShowSpanB = showSpanB;
            indicator.ShowChikou = showChikou;
            indicator.ShowBullishCloud = showBullishCloud;
            indicator.ShowBearishCloud = showBearishCloud;
            indicator.TenkanColor = tenkanColor;
            indicator.KijunColor = kijunColor;
            indicator.SpanAColor = spanAColor;
            indicator.SpanBColor = spanBColor;
            indicator.ChikouColor = chikouColor;
            indicator.BullishCloudColor = bullishCloudColor;
            indicator.BearishCloudColor = bearishCloudColor;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void RemoveAllIndicators()
        {
            indicators.Clear();
            selectedIndicatorIndex = -1;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        internal int HitTestIndicator(Point location)
        {
            if (indicators.Count == 0 || points.Count == 0 || !GetPlotRectangle().Contains(location))
                return -1;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var displayedCount = endIndex - firstIndex;
            if (displayedCount <= 0)
                return -1;

            var visible = points.Skip(firstIndex).Take(displayedCount).ToList();
            if (visible.Count == 0)
                return -1;

            GetVerticalRange(visible, out var min, out var max);
            var step = plot.Width / (double)Math.Max(1, visibleCount);
            var initialOffset = -plot.Width * 0.25;
            const double tolerance = 8.0;

            var bestIndex = -1;
            var bestDistance = double.MaxValue;

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type == ChartIndicatorType.Ichimoku)
                {
                    if (HitTestIchimoku(location, indicator, plot, min, max, displayedCount, step, initialOffset, tolerance, out var ichimokuDistance))
                    {
                        if (ichimokuDistance < bestDistance)
                        {
                            bestDistance = ichimokuDistance;
                            bestIndex = indicatorIndex;
                        }
                    }
                    continue;
                }

                var values = indicator.Type == ChartIndicatorType.MovingAverage
                    ? CalculateMovingAverage(indicator.Period)
                    : CalculateExponentialMovingAverage(indicator.Period);

                PointF? previous = null;
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = new PointF(
                        (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                        (float)PriceToScreen(values[absoluteIndex], plot, min, max));

                    if (previous.HasValue)
                    {
                        var distance = DistanceToIndicatorSegment(location, previous.Value, current);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = indicatorIndex;
                        }
                    }

                    previous = current;
                }
            }

            selectedIndicatorIndex = bestDistance <= tolerance ? bestIndex : -1;
            return selectedIndicatorIndex;
        }

        private bool HitTestIchimoku(
            Point location,
            ChartIndicator indicator,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double tolerance,
            out double bestDistance)
        {
            bestDistance = double.MaxValue;
            var tenkan = CalculateIchimokuTenkan();
            var kijun = CalculateIchimokuKijun();
            var spanA = CalculateIchimokuSpanA(tenkan, kijun);
            var spanB = CalculateIchimokuSpanB();
            var chikou = points.Select(p => p.Close).ToArray();

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= points.Count) continue;

                Check(tenkan, absoluteIndex, 0, indicator.ShowTenkan, indicator.TenkanColor);
                Check(kijun, absoluteIndex, 0, indicator.ShowKijun, indicator.KijunColor);
                Check(spanA, absoluteIndex, 26, indicator.ShowSpanA, indicator.SpanAColor);
                Check(spanB, absoluteIndex, 26, indicator.ShowSpanB, indicator.SpanBColor);
                Check(chikou, absoluteIndex, -26, indicator.ShowChikou, indicator.ChikouColor);
            }

            return bestDistance < double.MaxValue;

            void Check(double[] values, int sourceIndex, int shift, bool show, Color color)
            {
                if (!show || double.IsNaN(values[sourceIndex])) return;
                var screenIndex = sourceIndex - firstIndex + shift;
                var current = new PointF(
                    (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                    PriceToScreen(values[sourceIndex], plot, min, max));

                if (screenIndex < 0 || screenIndex > displayedCount) return;

                var distance = DistanceToIndicatorSegment(location, current, current);
                if (distance < bestDistance) bestDistance = distance;
            }
        }

        private static double DistanceToIndicatorSegment(Point p, PointF a, PointF b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            if (dx == 0 && dy == 0)
                return Math.Sqrt(Math.Pow(p.X - a.X, 2) + Math.Pow(p.Y - a.Y, 2));

            var t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0.0f, 1.0f);

            var x = a.X + t * dx;
            var y = a.Y + t * dy;
            return Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2));
        }

        private void DrawIndicators(
            Graphics g,
            Rectangle plot,
            int layoutCount,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset)
        {
            if (indicators.Count == 0 || displayedCount <= 0 || points.Count == 0)
                return;

            foreach (var indicator in indicators)
            {
                if (indicator.Type == ChartIndicatorType.Ichimoku)
                {
                    DrawIchimoku(g, plot, min, max, displayedCount, step, initialOffset, indicator);
                    continue;
                }

                var values = indicator.Type == ChartIndicatorType.MovingAverage
                    ? CalculateMovingAverage(indicator.Period)
                    : CalculateExponentialMovingAverage(indicator.Period);

                using var pen = new Pen(indicator.LineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
                var linePoints = new List<PointF>();
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                        continue;
                    var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                    linePoints.Add(new PointF(x, PriceToScreen(values[absoluteIndex], plot, min, max)));
                }

                if (linePoints.Count > 1)
                    g.DrawLines(pen, linePoints.ToArray());

                var label = indicator.Type == ChartIndicatorType.MovingAverage ? $"MA({indicator.Period})" : $"EMA({indicator.Period})";
                if (linePoints.Count > 0)
                {
                    using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
                    using var labelBrush = new SolidBrush(pen.Color);
                    var p = linePoints[^1];
                    g.DrawString(label, labelFont, labelBrush, p.X + 4f, Math.Clamp(p.Y - 10f, plot.Top, plot.Bottom - 14f));
                }
            }
        }

        private void DrawIchimoku(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            ChartIndicator indicator)
        {
            var tenkan = CalculateIchimokuTenkan();
            var kijun = CalculateIchimokuKijun();
            var spanA = CalculateIchimokuSpanA(tenkan, kijun);
            var spanB = CalculateIchimokuSpanB();
            var chikou = points.Select(p => p.Close).ToArray();

            DrawIchimokuCloud(g, plot, min, max, displayedCount, step, initialOffset,
                spanA, spanB, indicator.ShowBullishCloud, indicator.ShowBearishCloud,
                indicator.BullishCloudColor, indicator.BearishCloudColor);

            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, tenkan, 0, indicator.ShowTenkan, indicator.TenkanColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, kijun, 0, indicator.ShowKijun, indicator.KijunColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, spanA, 26, indicator.ShowSpanA, indicator.SpanAColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, spanB, 26, indicator.ShowSpanB, indicator.SpanBColor);
            DrawIchimokuLine(g, plot, min, max, displayedCount, step, initialOffset, chikou, -26, indicator.ShowChikou, indicator.ChikouColor);
        }

        private void DrawIchimokuLine(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double[] values,
            int shift,
            bool show,
            Color color)
        {
            if (!show) return;

            using var pen = new Pen(color, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;

            for (var sourceIndex = 0; sourceIndex < values.Length; sourceIndex++)
            {
                if (double.IsNaN(values[sourceIndex]))
                {
                    previous = null;
                    continue;
                }

                var screenIndex = sourceIndex - firstIndex + shift;
                if (screenIndex < -1 || screenIndex > displayedCount)
                {
                    previous = null;
                    continue;
                }

                var point = new PointF(
                    (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                    PriceToScreen(values[sourceIndex], plot, min, max));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, point);

                previous = point;
            }
        }

        private void DrawIchimokuCloud(
            Graphics g,
            Rectangle plot,
            double min,
            double max,
            int displayedCount,
            double step,
            double initialOffset,
            double[] spanA,
            double[] spanB,
            bool showBullish,
            bool showBearish,
            Color bullishColor,
            Color bearishColor)
        {
            for (var sourceIndex = 0; sourceIndex < points.Count - 1; sourceIndex++)
            {
                if (double.IsNaN(spanA[sourceIndex]) || double.IsNaN(spanB[sourceIndex]) ||
                    double.IsNaN(spanA[sourceIndex + 1]) || double.IsNaN(spanB[sourceIndex + 1]))
                    continue;

                var screenIndex = sourceIndex - firstIndex + 26;
                var nextScreenIndex = screenIndex + 1;
                if (nextScreenIndex < 0 || screenIndex > displayedCount)
                    continue;

                var bullish = spanA[sourceIndex] >= spanB[sourceIndex];
                if ((bullish && !showBullish) || (!bullish && !showBearish))
                    continue;

                var x1 = (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset);
                var x2 = (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset);
                var yA1 = PriceToScreen(spanA[sourceIndex], plot, min, max);
                var yB1 = PriceToScreen(spanB[sourceIndex], plot, min, max);
                var yA2 = PriceToScreen(spanA[sourceIndex + 1], plot, min, max);
                var yB2 = PriceToScreen(spanB[sourceIndex + 1], plot, min, max);

                using var brush = new SolidBrush(Color.FromArgb(45, bullish ? bullishColor : bearishColor));
                g.FillPolygon(brush, new[]
                {
                    new PointF(x1, yA1), new PointF(x2, yA2),
                    new PointF(x2, yB2), new PointF(x1, yB1)
                });
            }
        }

        private double[] CalculateMovingAverage(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count < period)
                return result;

            double sum = 0;
            for (var i = 0; i < points.Count; i++)
            {
                sum += points[i].Close;

                if (i >= period)
                    sum -= points[i - period].Close;

                if (i >= period - 1)
                    result[i] = sum / period;
            }

            return result;
        }

        private double[] CalculateExponentialMovingAverage(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count < period)
                return result;

            double sum = 0;
            for (var i = 0; i < period; i++)
                sum += points[i].Close;

            var ema = sum / period;
            result[period - 1] = ema;

            var multiplier = 2.0 / (period + 1.0);
            for (var i = period; i < points.Count; i++)
            {
                ema = ((points[i].Close - ema) * multiplier) + ema;
                result[i] = ema;
            }

            return result;
        }
    }
}