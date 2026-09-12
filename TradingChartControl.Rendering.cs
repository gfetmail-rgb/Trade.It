using System.Drawing.Drawing2D;
using System.Globalization;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            base.OnPaint(e);
            if (points.Count == 0)
                return;

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
                return;
            GetVerticalRange(visible, out var min, out var max);

            using var gridPen = new Pen(LineAppearanceSettings.GridColor, LineAppearanceSettings.GridLineWidth) { DashStyle = LineAppearanceSettings.GridLineStyle };
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var linePen = new Pen(ChartAppearanceSettings.LineChartColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);
            var chartState = e.Graphics.Save();
            var plotHeight = Math.Max(1, plot.Height);
            var step = plot.Width / (double)Math.Max(1, visible.Count);
            var initialOffset = step * 0.5;

            for (var i = 0; i < visible.Count; i++)
            {
                var point = visible[i];
                var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                if (chartType == TradingChartType.Line)
                {
                    if (i > 0)
                    {
                        var previous = visible[i - 1];
                        var previousX = (float)(plot.Left + step * (i - 0.5) + initialOffset + horizontalPanOffset);
                        e.Graphics.DrawLine(linePen, previousX, PriceToScreen(previous.Close, plot, min, max), x, PriceToScreen(point.Close, plot, min, max));
                    }
                }
                else
                {
                    var highY = PriceToScreen(point.High, plot, min, max);
                    var lowY = PriceToScreen(point.Low, plot, min, max);
                    var openY = PriceToScreen(point.Open, plot, min, max);
                    var closeY = PriceToScreen(point.Close, plot, min, max);
                    var rising = point.Close >= point.Open;
                    var pen = rising ? risingPen : fallingPen;
                    var brush = rising ? risingBrush : fallingBrush;
                    e.Graphics.DrawLine(pen, x, highY, x, lowY);
                    if (chartType == TradingChartType.Bar)
                    {
                        e.Graphics.DrawLine(pen, x - 5, openY, x, openY);
                        e.Graphics.DrawLine(pen, x, closeY, x + 5, closeY);
                    }
                    else
                    {
                        var bodyTop = Math.Min(openY, closeY);
                        var bodyHeight = Math.Max(1f, Math.Abs(closeY - openY));
                        var bodyWidth = Math.Max(2f, (float)(step * 0.62));
                        e.Graphics.FillRectangle(brush, x - bodyWidth / 2f, bodyTop, bodyWidth, bodyHeight);
                        e.Graphics.DrawRectangle(pen, x - bodyWidth / 2f, bodyTop, bodyWidth, bodyHeight);
                    }
                }
            }

            if (showGrid)
            {
                for (var i = 1; i < 5; i++)
                {
                    var y = plot.Top + plot.Height * i / 5f;
                    e.Graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                }
            }

            e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);
            e.Graphics.Restore(chartState);

            DrawDrawings(e.Graphics, plot, visible.Count, min, max);
            RenderAdvancedDrawings(e.Graphics);
            RenderExtraDrawings(e.Graphics);

            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None && IsInsidePlot(drawingCurrentPoint))
            {
                using var previewPen = new Pen(GetDrawingColor(activeDrawingTool), LineAppearanceSettings.DrawingLineWidth) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                DrawDrawingPreview(e.Graphics, previewPen, plot, visible.Count, min, max);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var x = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);
                using var crosshairPen = new Pen(LineAppearanceSettings.CrosshairColor, LineAppearanceSettings.CrosshairLineWidth) { DashStyle = LineAppearanceSettings.CrosshairLineStyle };
                e.Graphics.DrawLine(crosshairPen, x, plot.Top, x, plot.Bottom);
                e.Graphics.DrawLine(crosshairPen, plot.Left, crosshairPoint.Y, plot.Right, crosshairPoint.Y);
            }

            for (var i = 0; i <= 5; i++)
            {
                var value = max - (max - min) * i / 5.0;
                var y = PriceToScreen(value, plot, min, max);
                var text = value.ToString("0.##");
                var size = e.Graphics.MeasureString(text, axisTextFont);
                var x = Math.Max(1f, plot.Left - size.Width - 4f);
                e.Graphics.DrawString(text, axisTextFont, textBrush, x, y - size.Height / 2f);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var crosshairX = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);
                var crosshairPrice = max - ((crosshairPoint.Y - plot.Top) / (double)Math.Max(1, plot.Height)) * (max - min);
                var priceText = crosshairPrice.ToString("0.##");
                var priceSize = e.Graphics.MeasureString(priceText, axisTextFont);
                var priceRect = new RectangleF(
                    Math.Max(1f, plot.Left - priceSize.Width - 9f),
                    crosshairPoint.Y - priceSize.Height / 2f - 2f,
                    priceSize.Width + 6f,
                    priceSize.Height + 4f);

                using var crosshairLabelBrush = new SolidBrush(LineAppearanceSettings.CrosshairColor);
                using var crosshairLabelTextBrush = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(crosshairLabelBrush, priceRect);
                e.Graphics.DrawString(priceText, axisTextFont, crosshairLabelTextBrush, priceRect.X + 3f, priceRect.Y + 2f);

                var date = visible[crosshairIndex].Date;
                var calendar = new PersianCalendar();
                var timeText = $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}";
                var timeSize = e.Graphics.MeasureString(timeText, axisTextFont);
                var timeRect = new RectangleF(
                    Math.Clamp(crosshairX - timeSize.Width / 2f - 3f, plot.Left, Math.Max(plot.Left, Width - timeSize.Width - 6f)),
                    plot.Bottom + 2f,
                    timeSize.Width + 6f,
                    timeSize.Height + 4f);

                e.Graphics.FillRectangle(crosshairLabelBrush, timeRect);
                e.Graphics.DrawString(timeText, axisTextFont, crosshairLabelTextBrush, timeRect.X + 3f, timeRect.Y + 2f);
            }

            DrawAdvancedTextLabels(e.Graphics, plot, visible.Count, min, max);
        }

        private void DrawDrawings(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            for (var i = 0; i < drawings.Count; i++)
            {
                var selected = i == selectedDrawingIndex;
                var color = GetDrawingColor(drawings[i].Tool);
                var width = selected ? LineAppearanceSettings.DrawingLineWidth + 1.4f : LineAppearanceSettings.DrawingLineWidth;
                using var drawingPen = new Pen(color, width) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                DrawSingleDrawing(g, drawingPen, drawings[i], plot, visibleCountForDrawing, min, max);
                if (selected)
                    DrawSelectionHandles(g, drawings[i], plot, visibleCountForDrawing, min, max);
            }
        }

        private void DrawDrawingPreview(Graphics g, Pen pen, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            if (activeDrawingTool == ChartDrawingTool.TrendChannel)
            {
                if (drawingStage == 1)
                {
                    var p1 = DrawingPointToScreen(drawingStartPoint, plot, visibleCountForDrawing, min, max);
                    var p2 = drawingCurrentPoint;
                    g.DrawLine(pen, p1, p2);
                }
                else
                {
                    var p1 = DrawingPointToScreen(drawingStartPoint, plot, visibleCountForDrawing, min, max);
                    var p2 = DrawingPointToScreen(drawingSecondPoint, plot, visibleCountForDrawing, min, max);
                    var p3 = drawingCurrentPoint;
                    DrawTrendChannel(g, pen, p1, p2, p3, plot);
                }
                return;
            }

            var start = DrawingPointToScreen(drawingStartPoint, plot, visibleCountForDrawing, min, max);
            DrawTool(g, pen, activeDrawingTool, start, drawingCurrentPoint, plot);
        }

        private void DrawTool(Graphics g, Pen pen, ChartDrawingTool tool, PointF start, PointF end, Rectangle plot)
        {
            switch (tool)
            {
                case ChartDrawingTool.TrendLine:
                    g.DrawLine(pen, start, end);
                    break;
                case ChartDrawingTool.HorizontalDoubleArrow:
                    DrawDoubleEndedLine(g, pen, new PointF(plot.Left, start.Y), new PointF(plot.Right, start.Y), false);
                    break;
                case ChartDrawingTool.VerticalDoubleArrow:
                    DrawDoubleEndedLine(g, pen, new PointF(start.X, plot.Top), new PointF(start.X, plot.Bottom), false);
                    break;
                case ChartDrawingTool.HorizontalRay:
                    g.DrawLine(pen, start, new PointF(plot.Right, start.Y));
                    break;
                case ChartDrawingTool.TrendLineWithArrow:
                    g.DrawLine(pen, start, end);
                    DrawArrowHead(g, pen, start, end);
                    break;
                case ChartDrawingTool.Rectangle:
                    g.DrawRectangle(pen, RectangleF.FromLTRB(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y), Math.Max(start.X, end.X), Math.Max(start.Y, end.Y)));
                    break;
                case ChartDrawingTool.FibonacciRetracement:
                    DrawFibonacciRetracement(g, pen, start, end, plot);
                    break;
                case ChartDrawingTool.TextLabel:
                    g.DrawString("متن", Font, pen.Brush, end);
                    break;
                case ChartDrawingTool.Pitchfork:
                    DrawPitchfork(g, pen, start, end, plot);
                    break;
                case ChartDrawingTool.FibonacciExtension:
                    DrawFibonacciExtension(g, pen, start, end, plot);
                    break;
                case ChartDrawingTool.Measure:
                    DrawMeasure(g, pen, start, end);
                    break;
            }
        }
    }
}
