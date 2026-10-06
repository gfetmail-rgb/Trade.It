namespace Trade.It
{
    internal enum ChartIndicatorType
    {
        MovingAverage,
        ExponentialMovingAverage
    }

    internal sealed class ChartIndicator
    {
        public ChartIndicatorType Type { get; init; }
        public int Period { get; init; }
    }

    internal sealed partial class TradingChartControl
    {
        private readonly List<ChartIndicator> indicators = new();

        public IReadOnlyList<ChartIndicator> Indicators => indicators;

        public void AddMovingAverage(int period = 20)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.MovingAverage,
                Period = period
            });
            Invalidate();
        }

        public void AddExponentialMovingAverage(int period = 20)
        {
            period = Math.Clamp(period, 2, Math.Max(2, points.Count));
            indicators.Add(new ChartIndicator
            {
                Type = ChartIndicatorType.ExponentialMovingAverage,
                Period = period
            });
            Invalidate();
        }

        public bool RemoveOneIndicator(ChartIndicatorType type)
        {
            for (var i = indicators.Count - 1; i >= 0; i--)
            {
                if (indicators[i].Type != type)
                    continue;

                indicators.RemoveAt(i);
                Invalidate();
                return true;
            }

            return false;
        }

        public void RemoveAllIndicators()
        {
            indicators.Clear();
            Invalidate();
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
                var values = indicator.Type == ChartIndicatorType.MovingAverage
                    ? CalculateMovingAverage(indicator.Period)
                    : CalculateExponentialMovingAverage(indicator.Period);

                using var pen = new Pen(
                    indicator.Type == ChartIndicatorType.MovingAverage
                        ? Color.FromArgb(30, 100, 220)
                        : Color.FromArgb(210, 80, 30),
                    Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));

                var linePoints = new List<PointF>();
                for (var i = 0; i < displayedCount; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                        continue;

                    var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                    var y = PriceToScreen(values[absoluteIndex], plot, min, max);
                    linePoints.Add(new PointF(x, y));
                }

                if (linePoints.Count > 1)
                    g.DrawLines(pen, linePoints.ToArray());

                var label = indicator.Type == ChartIndicatorType.MovingAverage
                    ? $"MA({indicator.Period})"
                    : $"EMA({indicator.Period})";

                if (linePoints.Count > 0)
                {
                    using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
                    using var labelBrush = new SolidBrush(pen.Color);
                    var p = linePoints[^1];
                    g.DrawString(label, labelFont, labelBrush, p.X + 4f, Math.Clamp(p.Y - 10f, plot.Top, plot.Bottom - 14f));
                }
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