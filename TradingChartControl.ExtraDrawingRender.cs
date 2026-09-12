namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private bool extraDrawingLegacyPaintDetached;

        private static Color GetExtraDrawingColor(ExtraDrawingTool tool) => tool switch
        {
            ExtraDrawingTool.Pitchfork => ChartAppearanceSettings.PitchforkColor,
            ExtraDrawingTool.FibonacciExtension => ChartAppearanceSettings.FibonacciExtensionColor,
            ExtraDrawingTool.Measure => ChartAppearanceSettings.MeasureColor,
            _ => ChartAppearanceSettings.PitchforkColor
        };

        private void DetachLegacyExtraDrawingPaint()
        {
            if (extraDrawingLegacyPaintDetached)
                return;

            Paint -= ExtraDrawing_Paint;
            extraDrawingLegacyPaintDetached = true;
        }

        private void RenderExtraDrawings(Graphics g)
        {
            // ExtraDrawingTools.cs contains an older Paint handler. Detach it so the
            // Fibonacci-extension renderer below is the single source of rendering.
            // The legacy handler drew the three-point connecting trend lines.
            DetachLegacyExtraDrawingPaint();

            RenderConfiguredAdvancedOverlay(g);
            SyncExtraDrawingData();
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var d = extraDrawings[i];
                var color = GetExtraDrawingColor(d.Tool);
                var width = i == selectedExtraDrawingIndex ? LineAppearanceSettings.DrawingLineWidth + 0.9f : LineAppearanceSettings.DrawingLineWidth;
                using var pen = new Pen(color, width) { DashStyle = LineAppearanceSettings.DrawingLineStyle };

                if (d.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(g, pen, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawThreePointFibonacci(g, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Count > 0)
            {
                var previewColor = GetExtraDrawingColor(activeExtraDrawingTool);
                using var previewPen = new Pen(previewColor, LineAppearanceSettings.DrawingLineWidth) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                    DrawMeasurePreview(g, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                    DrawPitchforkPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawThreePointFibonacciPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            }
        }

        private void DrawThreePointFibonacci(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2f, 2.618f };
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);

            // Deliberately do NOT draw A-B or B-C. This is a three-point
            // Fibonacci extension, not a trend-line tool.
            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, leftX, y, rightX, y);
                var text = level switch
                {
                    0f => "0%",
                    0.382f => "38.2%",
                    0.618f => "61.8%",
                    1f => "100%",
                    1.272f => "127.2%",
                    1.618f => "161.8%",
                    2f => "200%",
                    _ => "261.8%"
                };
                var size = g.MeasureString(text, SystemFonts.DefaultFont);
                var labelX = rightX + 5f;
                if (labelX + size.Width > plot.Right)
                    labelX = Math.Max(plot.Left, leftX - size.Width - 5f);
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
            }

            if (selected)
            {
                DrawAdvancedHandle(g, a);
                DrawAdvancedHandle(g, b);
                DrawAdvancedHandle(g, c);
            }
        }

        private static void DrawThreePointFibonacciPreview(Graphics g, Pen pen, List<Point> points, Point current, Rectangle plot)
        {
            if (points.Count == 0)
                return;

            var a = points[0];
            var b = points.Count > 1 ? points[1] : current;
            var c = points.Count > 2 ? points[2] : current;
            var dy = b.Y - a.Y;
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);

            // Preview also contains levels only; no connecting trend lines.
            foreach (var level in new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2f, 2.618f })
            {
                var y = c.Y + dy * level;
                if (y >= plot.Top - 1 && y <= plot.Bottom + 1)
                    g.DrawLine(pen, leftX, y, rightX, y);
            }
        }
    }
}
