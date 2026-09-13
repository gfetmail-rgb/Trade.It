namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        // Chart appearance/settings support.
        internal static double ChartRightEmptyPercent
        {
            get => ChartAppearanceSettings.ChartRightEmptyPercent;
            set => ChartAppearanceSettings.SetChartRightEmptyPercent(value);
        }

        internal static void LoadChartAppearanceSettings() => ChartAppearanceSettings.Load();
        internal static void SaveChartAppearanceSettings() => ChartAppearanceSettings.Save();

        private static Color GetDrawingColor(ChartDrawingTool tool) => tool switch
        {
            ChartDrawingTool.TrendLine => ChartAppearanceSettings.TrendLineColor,
            ChartDrawingTool.TrendChannel => ChartAppearanceSettings.TrendChannelColor,
            ChartDrawingTool.HorizontalDoubleArrow => ChartAppearanceSettings.HorizontalDoubleArrowColor,
            ChartDrawingTool.VerticalDoubleArrow => ChartAppearanceSettings.VerticalDoubleArrowColor,
            ChartDrawingTool.HorizontalRay => ChartAppearanceSettings.HorizontalRayColor,
            ChartDrawingTool.TrendLineWithArrow => ChartAppearanceSettings.TrendLineWithArrowColor,
            ChartDrawingTool.Rectangle => ChartAppearanceSettings.RectangleColor,
            _ => ChartAppearanceSettings.TrendLineColor
        };

        private static Color DarkenColor(Color color, float factor = 0.72f)
        {
            return Color.FromArgb(
                color.A,
                (int)(color.R * factor),
                (int)(color.G * factor),
                (int)(color.B * factor));
        }

        // Configured advanced drawing overlay.
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

        // Keep drawing coordinates synchronized when the visible data window moves.
        private int drawingSyncFirstIndex;
        private bool drawingSyncInitialized;
        private bool drawingSyncPaintInitialized;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!drawingSyncPaintInitialized)
            {
                drawingSyncPaintInitialized = true;
                Paint += DrawingSync_Paint;
            }
            drawingSyncFirstIndex = firstIndex;
            drawingSyncInitialized = true;
        }

        private void DrawingSync_Paint(object? sender, PaintEventArgs e)
        {
            SyncDrawingCoordinatesToView();
        }

        private void SyncDrawingCoordinatesToView()
        {
            if (!drawingSyncInitialized)
            {
                drawingSyncFirstIndex = firstIndex;
                drawingSyncInitialized = true;
                return;
            }

            var delta = drawingSyncFirstIndex - firstIndex;
            if (delta == 0)
                return;

            foreach (var drawing in drawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
                drawing.X3 += delta;
            }

            foreach (var drawing in advancedDrawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
            }

            foreach (var drawing in extraDrawings)
            {
                drawing.X1 += delta;
                drawing.X2 += delta;
                drawing.X3 += delta;
            }

            drawingSyncFirstIndex = firstIndex;
        }

        // Extra drawing mouse safety.
        private bool extraDrawingSafetyInitialized;
        private bool extraSafetyWasActive;
        private bool extraSafetyWasInProgress;
        private int extraSafetyPointCount;

        public void EnableExtraDrawingMouseSafety()
        {
            if (!extraDrawingSafetyInitialized)
            {
                extraDrawingSafetyInitialized = true;
                MouseDown += ExtraDrawingSafety_MouseDown;
            }

            extraSafetyWasActive = ExtraDrawingActive;
            extraSafetyWasInProgress = extraDrawingInProgress;
            extraSafetyPointCount = extraDrawingPoints.Count;
        }

        private void ExtraDrawingSafety_MouseDown(object? sender, MouseEventArgs e)
        {
            // ExtraDrawing_MouseDown runs before TradingChartControl.OnMouseDown.
            // When the third point completes a three-point tool, CancelExtraDrawing()
            // clears extraInputHandled, allowing OnMouseDown to fall through into
            // the normal chart/ruler state machine. Consume that click explicitly.
            if (e.Button == MouseButtons.Left &&
                extraSafetyWasActive &&
                extraSafetyWasInProgress &&
                extraSafetyPointCount > 0)
            {
                extraInputHandled = true;
            }

            extraSafetyWasActive = ExtraDrawingActive;
            extraSafetyWasInProgress = extraDrawingInProgress;
            extraSafetyPointCount = extraDrawingPoints.Count;
        }
    }
}
