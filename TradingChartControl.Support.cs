namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        internal static double ChartRightEmptyPercent
        {
            get => ChartAppearanceSettings.ChartRightEmptyPercent;
            set => ChartAppearanceSettings.SetChartRightEmptyPercent(value);
        }

        internal static double ChartTopEmptyPercent
        {
            get => ChartAppearanceSettings.ChartTopEmptyPercent;
            set => ChartAppearanceSettings.SetChartTopEmptyPercent(value);
        }

        internal static void LoadChartAppearanceSettings() => ChartAppearanceSettings.Load();
        internal static void SaveChartAppearanceSettings() => ChartAppearanceSettings.Save();

        private static Color GetDrawingColor(ChartDrawingTool tool) => tool switch
        {
            ChartDrawingTool.TrendLine => ChartAppearanceSettings.TrendLineColor,
            ChartDrawingTool.TrendChannel => ChartAppearanceSettings.TrendChannelColor,
            ChartDrawingTool.HorizontalLine => ChartAppearanceSettings.HorizontalDoubleArrowColor,
            ChartDrawingTool.VerticalLine => ChartAppearanceSettings.VerticalDoubleArrowColor,
            ChartDrawingTool.HorizontalRay => ChartAppearanceSettings.HorizontalRayColor,
            ChartDrawingTool.TrendLineWithArrow => ChartAppearanceSettings.TrendLineWithArrowColor,
            ChartDrawingTool.Rectangle => ChartAppearanceSettings.RectangleColor,
            _ => ChartAppearanceSettings.TrendLineColor
        };

        private static Color DarkenColor(Color color, float factor = 0.72f)
        {
            return Color.FromArgb(color.A, (int)(color.R * factor), (int)(color.G * factor), (int)(color.B * factor));
        }

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

        private void DrawingSync_Paint(object? sender, PaintEventArgs e) => SyncDrawingCoordinatesToView();

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
            if (e.Button == MouseButtons.Left && extraSafetyWasActive && extraSafetyWasInProgress && extraSafetyPointCount > 0)
                extraInputHandled = true;
            extraSafetyWasActive = ExtraDrawingActive;
            extraSafetyWasInProgress = extraDrawingInProgress;
            extraSafetyPointCount = extraDrawingPoints.Count;
        }

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
                    DrawPitchforkFixed(g, pen, d, plot, visibleCountForDrawing, min, max, i == selectedExtraDrawingIndex);
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
                    DrawPitchforkPreviewFixed(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
                else
                    DrawThreePointFibonacciPreview(g, previewPen, extraDrawingPoints, extraDrawingCurrentPoint, plot);
            }
        }

        private void DrawPitchforkFixed(Graphics g, Pen pen, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            DrawPitchforkGeometryFixed(g, pen, p1, p2, p3, plot);
            if (selected)
            {
                DrawAdvancedHandle(g, p1);
                DrawAdvancedHandle(g, p2);
                DrawAdvancedHandle(g, p3);
            }
        }

        private static void DrawPitchforkPreviewFixed(Graphics g, Pen pen, List<Point> points, Point current, Rectangle plot)
        {
            if (points.Count == 0)
                return;
            var p1 = points[0];
            var p2 = points.Count > 1 ? points[1] : current;
            var p3 = points.Count > 2 ? points[2] : current;
            DrawPitchforkGeometryFixed(g, pen, p1, p2, p3, plot);
        }

        private static void DrawPitchforkGeometryFixed(Graphics g, Pen pen, PointF p1, PointF p2, PointF p3, Rectangle plot)
        {
            var midpoint = new PointF((p2.X + p3.X) / 2f, (p2.Y + p3.Y) / 2f);
            var dx = midpoint.X - p1.X;
            var dy = midpoint.Y - p1.Y;
            if (Math.Abs(dx) + Math.Abs(dy) < 0.001f)
                return;

            DrawRayToRight(g, pen, p1, dx, dy, plot);
            DrawRayToRight(g, pen, p2, dx, dy, plot);
            DrawRayToRight(g, pen, p3, dx, dy, plot);
        }

        private void DrawThreePointFibonacci(Graphics g, Pen pen, Brush labelBrush, ExtraDrawing d, Rectangle plot, int visibleCount, double min, double max, bool selected)
        {
            var a = DataToScreen(d.X1, d.Y1, plot, visibleCount, min, max);
            var b = DataToScreen(d.X2, d.Y2, plot, visibleCount, min, max);
            var c = DataToScreen(d.X3, d.Y3, plot, visibleCount, min, max);
            var dy = b.Y - a.Y;
            var leftX = Math.Min(a.X, c.X);
            var rightX = Math.Max(a.X, c.X);
            foreach (var level in ChartAppearanceSettings.GetEnabledFibonacciExtensionLevels())
            {
                var y = c.Y + dy * level.Value;
                g.DrawLine(pen, leftX, y, rightX, y);
                var size = g.MeasureString(level.Text, SystemFonts.DefaultFont);
                var labelX = rightX + 5f;
                if (labelX + size.Width > plot.Right)
                    labelX = Math.Max(plot.Left, leftX - size.Width - 5f);
                g.DrawString(level.Text, SystemFonts.DefaultFont, labelBrush, labelX, y - size.Height / 2f);
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
            foreach (var level in ChartAppearanceSettings.GetEnabledFibonacciExtensionLevels())
            {
                var y = c.Y + dy * level.Value;
                if (y >= plot.Top - 1 && y <= plot.Bottom + 1)
                    g.DrawLine(pen, leftX, y, rightX, y);
            }
        }
    }
}