namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private void RenderConfiguredAdvancedOverlay(Graphics g)
        {
            if (advancedDrawings.Count == 0)
                return;

            if (!TryGetAdvancedContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return;

            using var labelBrush = new SolidBrush(Color.FromArgb(35, 35, 35));
            using var labelBack = new SolidBrush(Color.FromArgb(245, 248, 252));

            foreach (var drawing in advancedDrawings)
            {
                var color = drawing.Tool == AdvancedDrawingTool.FibonacciRetracement
                    ? ChartAppearanceSettings.FibonacciRetracementColor
                    : ChartAppearanceSettings.TextLabelColor;

                var penWidth = LineAppearanceSettings.DrawingLineWidth;
                using var pen = new Pen(color, penWidth) { DashStyle = LineAppearanceSettings.DrawingLineStyle };

                if (drawing.Tool == AdvancedDrawingTool.FibonacciRetracement)
                {
                    var start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
                    var end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
                    DrawFibonacciLevels(g, pen, labelBrush, start, end);
                }
                else if (drawing.Tool == AdvancedDrawingTool.TextLabel)
                {
                    var point = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
                    using var font = new Font(Font.FontFamily, Math.Max(8f, Font.Size), FontStyle.Regular);
                    var text = drawing.Text ?? string.Empty;
                    var size = g.MeasureString(text, font);
                    var rect = new RectangleF(point.X, point.Y - size.Height, size.Width + 8f, size.Height + 6f);
                    g.FillRectangle(labelBack, rect);
                    g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    g.DrawString(text, font, labelBrush, point.X + 4f, point.Y - size.Height + 3f);
                }
            }
        }
    }
}
