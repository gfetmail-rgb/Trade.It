namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private static Color GetExtraDrawingColor(ExtraDrawingTool tool) => tool switch
        {
            ExtraDrawingTool.Pitchfork => ChartAppearanceSettings.PitchforkColor,
            ExtraDrawingTool.FibonacciExtension => ChartAppearanceSettings.FibonacciExtensionColor,
            ExtraDrawingTool.Measure => ChartAppearanceSettings.MeasureColor,
            _ => ChartAppearanceSettings.PitchforkColor
        };

        private void RenderExtraDrawings(Graphics g)
        {
            RenderConfiguredAdvancedOverlay(g);
            SyncExtraDrawingData();
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var d = extraDrawings[i];
                var color = GetExtraDrawingColor(d.Tool);
                using var pen = new Pen(color, i == selectedExtraDrawingIndex ? 2.2f : 1.3f);

                if (d.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(g, pen, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawThreePointFibonacci(g, pen, labelBrush, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Count > 0)
            {
                var previewColor = GetExtraDrawingColor(activeExtraDrawingTool);
                using var previewPen = new Pen(previewColor, 1.2f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                    DrawMeasurePreview(g, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot, visibleCountForDrawing, min, max);
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                    DrawPitchforkPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawFibonacciExtensionPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
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
                if (labelX + size.Width > plot.Right) labelX = Math.Max(plot.Left, leftX - size.Width - 5f);
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
            }

            if (selected)
            {
                DrawAdvancedHandle(g, a);
                DrawAdvancedHandle(g, b);
                DrawAdvancedHandle(g, c);
            }
        }
    }
}
