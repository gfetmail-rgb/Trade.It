namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private void RenderExtraDrawings(Graphics g)
        {
            SyncExtraDrawingData();
            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var normalPen = new Pen(Color.FromArgb(155, 80, 45), 1.3f);
            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(248, 248, 248));
            using var previewPen = new Pen(Color.FromArgb(155, 80, 45), 1.2f) { System.Drawing.Drawing2D.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

            for (var i = 0; i < extraDrawings.Count; i++)
            {
                var d = extraDrawings[i];
                if (d.Tool == ExtraDrawingTool.Pitchfork)
                    DrawPitchfork(g, normalPen, d, plot, visibleCountForDrawing, min, max);
                else if (d.Tool == ExtraDrawingTool.FibonacciExtension)
                    DrawThreePointFibonacci(g, normalPen, labelBrush, d, plot, visibleCountForDrawing, min, max);
                else
                    DrawMeasure(g, normalPen, labelBrush, labelBack, d, plot, visibleCountForDrawing, min, max);
            }

            if (extraDrawingInProgress && ExtraDrawingActive && extraDrawingPoints.Count > 0)
            {
                if (activeExtraDrawingTool == ExtraDrawingTool.Measure)
                    DrawMeasurePreview(g, previewPen, labelBrush, extraDrawingPoints[0], extraDrawingCurrentPoint, plot);
                else if (activeExtraDrawingTool == ExtraDrawingTool.Pitchfork)
                    DrawPitchforkPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawFibonacciExtensionPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            }
        }

        private static void DrawThreePointFibonacci(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var levels = new[] { 0f, 0.382f, 0.618f, 1f, 1.272f, 1.618f, 2.618f };

            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, c);

            foreach (var level in levels)
            {
                var y = c.Y + dy * level;
                g.DrawLine(pen, c.X, y, plot.Right, y);
                var text = level switch
                {
                    0f => "0%",
                    0.382f => "38.2%",
                    0.618f => "61.8%",
                    1f => "100%",
                    1.272f => "127.2%",
                    1.618f => "161.8%",
                    _ => "261.8%"
                };
                g.DrawString(text, SystemFonts.DefaultFont, labelBrush, Math.Min(c.X + 4f, plot.Right - 48f), y - 8f);
            }
        }
    }
}