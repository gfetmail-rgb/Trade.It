namespace Trade.It
{
    internal enum ChartIndicatorType
    {
        MovingAverage,
        ExponentialMovingAverage,
        Ichimoku,
        RelativeStrengthIndex
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
        public bool ShowRsiLine { get; set; } = true;
        public bool ShowRsi30 { get; set; } = true;
        public bool ShowRsi70 { get; set; } = true;
        public Color RsiLineColor { get; set; } = Color.FromArgb(30, 100, 220);
        public Color Rsi30Color { get; set; } = Color.FromArgb(150, 150, 150);
        public Color Rsi70Color { get; set; } = Color.FromArgb(150, 150, 150);
    }

    internal sealed partial class TradingChartControl
    {
        private readonly List<ChartIndicator> indicators = new();
        private int selectedIndicatorIndex = -1;

        // Ichimoku values are independent of the current viewport. Cache them so
        // panning/zooming only redraws the existing values instead of recalculating
        // all 9/26/52-period windows on every Paint.
        private double[]? ichimokuTenkanCache;
        private double[]? ichimokuKijunCache;
        private double[]? ichimokuSpanACache;
        private double[]? ichimokuSpanBCache;
        private double[]? ichimokuChikouCache;

        public IReadOnlyList<ChartIndicator> Indicators => indicators;
        internal bool HasRsiIndicator => indicators.Any(x => x.Type == ChartIndicatorType.RelativeStrengthIndex);
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

        public void AddRelativeStrengthIndex(int period = 14)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.RelativeStrengthIndex,
                Period = period,
                RsiLineColor = Color.FromArgb(30, 100, 220),
                Rsi30Color = Color.FromArgb(150, 150, 150),
                Rsi70Color = Color.FromArgb(150, 150, 150)
            });
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ApplyRsiSettings(
            int index,
            int period,
            bool showRsiLine,
            bool showRsi30,
            bool showRsi70,
            Color rsiLineColor,
            Color rsi30Color,
            Color rsi70Color)
        {
            if (index < 0 || index >= indicators.Count ||
                indicators[index].Type != ChartIndicatorType.RelativeStrengthIndex ||
                points.Count < 2)
                return false;

            var indicator = indicators[index];
            indicator.Period = Math.Clamp(period, 2, points.Count);
            indicator.ShowRsiLine = showRsiLine;
            indicator.ShowRsi30 = showRsi30;
            indicator.ShowRsi70 = showRsi70;
            indicator.RsiLineColor = rsiLineColor;
            indicator.Rsi30Color = rsi30Color;
            indicator.Rsi70Color = rsi70Color;
            selectedIndicatorIndex = index;
            Invalidate();
            AnalysisChanged?.Invoke(this, EventArgs.Empty);
            return true;
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
            if (indicators.Count == 0 || points.Count == 0)
                return -1;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var displayedCount = endIndex - firstIndex;
            if (displayedCount <= 0)
                return -1;

            var visible = points.Skip(firstIndex).Take(displayedCount).ToList();
            if (visible.Count == 0)
                return -1;

            var rsiPlot = GetRsiPlotRectangle();
            if (rsiPlot != Rectangle.Empty && rsiPlot.Contains(location))
            {
                var rsiIndex = HitTestRsi(location, rsiPlot, displayedCount, step: rsiPlot.Width / (double)Math.Max(1, visibleCount), initialOffset: -rsiPlot.Width * 0.25);
                selectedIndicatorIndex = rsiIndex;
                return selectedIndicatorIndex;
            }

            if (!GetPlotRectangle().Contains(location))
            {
                selectedIndicatorIndex = -1;
                return -1;
            }

            GetVerticalRange(visible, out var min, out var max);
            var step = plot.Width / (double)Math.Max(1, visibleCount);
            var initialOffset = -plot.Width * 0.25;
            const double tolerance = 8.0;

            var bestIndex = -1;
            var bestDistance = double.MaxValue;

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type == ChartIndicatorType.RelativeStrengthIndex)
                    continue;

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

        private int HitTestRsi(Point location, Rectangle rsiPlot, int displayedCount, double step, double initialOffset)
        {
            var bestIndex = -1;
            var bestDistance = 8.0;

            for (var indicatorIndex = 0; indicatorIndex < indicators.Count; indicatorIndex++)
            {
                var indicator = indicators[indicatorIndex];
                if (indicator.Type != ChartIndicatorType.RelativeStrengthIndex)
                    continue;

                var values = CalculateRsi(indicator.Period);
                PointF? previous = null;
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = RsiValueToScreen(values[absoluteIndex], rsiPlot, step, i, initialOffset);
                    if (previous.HasValue)
                    {
                        var distance = DistanceToIndicatorSegment(location, previous.Value, current);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = indicatorIndex;
                        }
                    }

                    if (indicator.ShowRsi30)
                    {
                        var y30 = RsiValueToScreen(30, rsiPlot, step, i, initialOffset).Y;
                        if (Math.Abs(location.Y - y30) <= bestDistance)
                        {
                            bestDistance = Math.Abs(location.Y - y30);
                            bestIndex = indicatorIndex;
                        }
                    }

                    if (indicator.ShowRsi70)
                    {
                        var y70 = RsiValueToScreen(70, rsiPlot, step, i, initialOffset).Y;
                        if (Math.Abs(location.Y - y70) <= bestDistance)
                        {
                            bestDistance = Math.Abs(location.Y - y70);
                            bestIndex = indicatorIndex;
                        }
                    }

                    previous = current;
                }
            }

            if (bestIndex >= 0)
                return bestIndex;

            return indicators.FindIndex(x => x.Type == ChartIndicatorType.RelativeStrengthIndex);
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
            var localBestDistance = double.MaxValue;
            EnsureIchimokuCache();
            var tenkan = ichimokuTenkanCache!;
            var kijun = ichimokuKijunCache!;
            var spanA = ichimokuSpanACache!;
            var spanB = ichimokuSpanBCache!;
            var chikou = ichimokuChikouCache!;

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= points.Count) continue;

                Check(tenkan, absoluteIndex, 0, indicator.ShowTenkan, indicator.TenkanColor);
                Check(kijun, absoluteIndex, 0, indicator.ShowKijun, indicator.KijunColor);
                Check(spanA, absoluteIndex, 26, indicator.ShowSpanA, indicator.SpanAColor);
                Check(spanB, absoluteIndex, 26, indicator.ShowSpanB, indicator.SpanBColor);
                Check(chikou, absoluteIndex, -26, indicator.ShowChikou, indicator.ChikouColor);
                CheckCloud(absoluteIndex, spanA, spanB, indicator);
            }

            bestDistance = localBestDistance;
            return localBestDistance < double.MaxValue;

            void CheckCloud(int sourceIndex, double[] valuesA, double[] valuesB, ChartIndicator currentIndicator)
            {
                if (sourceIndex < 0 || sourceIndex + 1 >= valuesA.Length ||
                    double.IsNaN(valuesA[sourceIndex]) || double.IsNaN(valuesB[sourceIndex]) ||
                    double.IsNaN(valuesA[sourceIndex + 1]) || double.IsNaN(valuesB[sourceIndex + 1]))
                    return;

                var bullish = valuesA[sourceIndex] >= valuesB[sourceIndex];
                if ((bullish && !currentIndicator.ShowBullishCloud) ||
                    (!bullish && !currentIndicator.ShowBearishCloud))
                    return;

                var screenIndex = sourceIndex - firstIndex + 26;
                var nextScreenIndex = screenIndex + 1;
                if (nextScreenIndex < 0 || screenIndex > displayedCount)
                    return;

                var polygon = new[]
                {
                    new PointF(
                        (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesA[sourceIndex], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesA[sourceIndex + 1], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (nextScreenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesB[sourceIndex + 1], plot, min, max)),
                    new PointF(
                        (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                        PriceToScreen(valuesB[sourceIndex], plot, min, max))
                };

                if (PointInPolygon(location, polygon))
                    localBestDistance = 0;
            }

            void Check(double[] values, int sourceIndex, int shift, bool show, Color color)
            {
                if (!show || double.IsNaN(values[sourceIndex])) return;
                var screenIndex = sourceIndex - firstIndex + shift;
                var current = new PointF(
                    (float)(plot.Left + step * (screenIndex + 0.5) + initialOffset + horizontalPanOffset),
                    PriceToScreen(values[sourceIndex], plot, min, max));

                if (screenIndex < 0 || screenIndex > displayedCount) return;

                var distance = DistanceToIndicatorSegment(location, current, current);
                if (distance < localBestDistance) localBestDistance = distance;
            }
        }

        private static bool PointInPolygon(Point point, IReadOnlyList<PointF> polygon)
        {
            var inside = false;
            for (var i = 0; i < polygon.Count; i++)
            {
                var j = i == 0 ? polygon.Count - 1 : i - 1;
                var pi = polygon[i];
                var pj = polygon[j];

                if ((pi.Y > point.Y) != (pj.Y > point.Y) &&
                    point.X < (pj.X - pi.X) * (point.Y - pi.Y) / (pj.Y - pi.Y) + pi.X)
                {
                    inside = !inside;
                }
            }

            return inside;
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
                if (indicator.Type == ChartIndicatorType.RelativeStrengthIndex)
                    continue;

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
            EnsureIchimokuCache();
            var tenkan = ichimokuTenkanCache!;
            var kijun = ichimokuKijunCache!;
            var spanA = ichimokuSpanACache!;
            var spanB = ichimokuSpanBCache!;
            var chikou = ichimokuChikouCache!;

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

        private void EnsureIchimokuCache()
        {
            if (ichimokuTenkanCache != null &&
                ichimokuKijunCache != null &&
                ichimokuSpanACache != null &&
                ichimokuSpanBCache != null &&
                ichimokuChikouCache != null &&
                ichimokuTenkanCache.Length == points.Count &&
                ichimokuKijunCache.Length == points.Count &&
                ichimokuSpanACache.Length == points.Count &&
                ichimokuSpanBCache.Length == points.Count &&
                ichimokuChikouCache.Length == points.Count)
            {
                return;
            }

            ichimokuTenkanCache = CalculateIchimokuMidpoint(9);
            ichimokuKijunCache = CalculateIchimokuMidpoint(26);
            ichimokuSpanACache = CalculateIchimokuSpanA(ichimokuTenkanCache, ichimokuKijunCache);
            ichimokuSpanBCache = CalculateIchimokuMidpoint(52);
            ichimokuChikouCache = points.Select(p => p.Close).ToArray();
        }

        internal void InvalidateIchimokuCache()
        {
            ichimokuTenkanCache = null;
            ichimokuKijunCache = null;
            ichimokuSpanACache = null;
            ichimokuSpanBCache = null;
            ichimokuChikouCache = null;
        }

        private double[] CalculateIchimokuTenkan()
        {
            return CalculateIchimokuMidpoint(9);
        }

        private double[] CalculateIchimokuKijun()
        {
            return CalculateIchimokuMidpoint(26);
        }

        private double[] CalculateIchimokuSpanB()
        {
            return CalculateIchimokuMidpoint(52);
        }

        private double[] CalculateIchimokuMidpoint(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            for (var i = period - 1; i < points.Count; i++)
            {
                var highest = double.MinValue;
                var lowest = double.MaxValue;
                for (var j = i - period + 1; j <= i; j++)
                {
                    highest = Math.Max(highest, points[j].High);
                    lowest = Math.Min(lowest, points[j].Low);
                }
                result[i] = (highest + lowest) / 2.0;
            }
            return result;
        }

        private double[] CalculateRsi(int period)
        {
            var result = Enumerable.Repeat(double.NaN, points.Count).ToArray();
            if (period <= 0 || points.Count <= period)
                return result;

            double gainSum = 0;
            double lossSum = 0;
            for (var i = 1; i <= period; i++)
            {
                var change = points[i].Close - points[i - 1].Close;
                if (change >= 0)
                    gainSum += change;
                else
                    lossSum -= change;
            }

            var averageGain = gainSum / period;
            var averageLoss = lossSum / period;
            result[period] = RsiFromAverages(averageGain, averageLoss);

            for (var i = period + 1; i < points.Count; i++)
            {
                var change = points[i].Close - points[i - 1].Close;
                var gain = Math.Max(0, change);
                var loss = Math.Max(0, -change);
                averageGain = ((averageGain * (period - 1)) + gain) / period;
                averageLoss = ((averageLoss * (period - 1)) + loss) / period;
                result[i] = RsiFromAverages(averageGain, averageLoss);
            }

            return result;
        }

        private static double RsiFromAverages(double averageGain, double averageLoss)
        {
            if (averageLoss <= 1e-12)
                return averageGain <= 1e-12 ? 50.0 : 100.0;
            var relativeStrength = averageGain / averageLoss;
            return 100.0 - (100.0 / (1.0 + relativeStrength));
        }

        private static double RsiValueToScreenY(double value, Rectangle plot)
        {
            return plot.Bottom - (value / 100.0 * plot.Height);
        }

        private PointF RsiValueToScreen(double value, Rectangle plot, double step, int relativeIndex, double initialOffset)
        {
            var x = (float)(plot.Left + step * (relativeIndex + 0.5) + initialOffset + horizontalPanOffset);
            var y = (float)RsiValueToScreenY(value, plot);
            return new PointF(x, y);
        }

        private static double[] CalculateIchimokuSpanA(double[] tenkan, double[] kijun)
        {
            var result = Enumerable.Repeat(double.NaN, tenkan.Length).ToArray();
            for (var i = 0; i < result.Length; i++)
            {
                if (!double.IsNaN(tenkan[i]) && !double.IsNaN(kijun[i]))
                    result[i] = (tenkan[i] + kijun[i]) / 2.0;
            }
            return result;
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