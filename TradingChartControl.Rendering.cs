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

            if (showGrid)
            {
                for (var i = 1; i <= 5; i++)
                {
                    var y = plot.Top + plot.Height * i / 6f;
                    e.Graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                }
                for (var i = 1; i <= 8; i++)
                {
                    var x = plot.Left + plot.Width * i / 9f;
                    e.Graphics.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
                }
            }

            e.Graphics.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            var chartState = e.Graphics.Save();
            e.Graphics.SetClip(plot, CombineMode.Intersect);

            var step = plot.Width / (double)Math.Max(1, visible.Count);
            var initialOffset = -plot.Width * 0.25;

            if (chartType == TradingChartType.Line)
            {
                var linePoints = new List<PointF>();
                for (var i = 0; i < visible.Count; i++)
                {
                    var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                    var y = PriceToScreen(visible[i].Close, plot, min, max);
                    linePoints.Add(new PointF(x, y));
                }
                if (linePoints.Count > 1)
                    e.Graphics.DrawLines(linePen, linePoints.ToArray());
            }
            else
            {
                var candleWidth = Math.Max(2f, (float)(step * 0.65));
                for (var i = 0; i < visible.Count; i++)
                {
                    var item = visible[i];
                    var x = (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                    var high = PriceToScreen(item.High, plot, min, max);
                    var low = PriceToScreen(item.Low, plot, min, max);
                    var open = PriceToScreen(item.Open, plot, min, max);
                    var close = PriceToScreen(item.Close, plot, min, max);
                    var rising = item.Close >= item.Open;
                    var brush = rising ? risingBrush : fallingBrush;
                    var candlePen = rising ? risingPen : fallingPen;
                    e.Graphics.DrawLine(candlePen, x, high, x, low);

                    if (chartType == TradingChartType.Candlestick)
                    {
                        var top = Math.Min(open, close);
                        var bottom = Math.Max(open, close);
                        var rect = RectangleF.FromLTRB(x - candleWidth / 2, top, x + candleWidth / 2, Math.Max(top + 1, bottom));
                        e.Graphics.FillRectangle(brush, rect);
                        e.Graphics.DrawRectangle(candlePen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                    else
                    {
                        var barLength = rising ? candleWidth : -candleWidth;
                        e.Graphics.DrawLine(candlePen, x, close, x + barLength, close);
                        e.Graphics.DrawLine(candlePen, x, open, x - barLength, open);
                    }
                }
            }

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

            e.Graphics.Restore(chartState);

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
                var timeText = $"\u200E{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}";
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
                    g.DrawLine(pen, drawingStartPoint, drawingCurrentPoint);
                    return;
                }
                DrawScreenTrendChannel(g, pen, plot, drawingStartPoint, drawingSecondPoint, drawingCurrentPoint);
                return;
            }
            DrawSinglePreview(g, pen, activeDrawingTool, drawingStartPoint, drawingCurrentPoint, plot);
        }

        private void DrawSelectionHandles(Graphics g, ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            using var handleBrush = new SolidBrush(Color.White);
            using var handlePen = new Pen(GetDrawingColor(drawing.Tool), 1.5f);
            const float radius = 4f;
            var handles = GetScreenHandles(drawing, plot, visibleCountForDrawing, min, max);
            foreach (var handle in handles)
            {
                g.FillEllipse(handleBrush, handle.X - radius, handle.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(handlePen, handle.X - radius, handle.Y - radius, radius * 2, radius * 2);
            }
        }

        private List<PointF> GetScreenHandles(ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            GetDrawingScreenPoints(drawing, plot, visibleCountForDrawing, min, max, out var start, out var end);
            if (drawing.Tool == ChartDrawingTool.Rectangle)
                return new List<PointF> { new(start.X, start.Y), new(end.X, start.Y), new(end.X, end.Y), new(start.X, end.Y) };
            if (drawing.Tool == ChartDrawingTool.TrendChannel)
            {
                var third = DataToScreen(drawing.X3, drawing.Y3, plot, visibleCountForDrawing, min, max);
                return new List<PointF> { start, end, third };
            }
            return new List<PointF> { start, end };
        }

        private void DrawSingleDrawing(Graphics g, Pen pen, ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            GetDrawingScreenPoints(drawing, plot, visibleCountForDrawing, min, max, out var start, out var end);
            switch (drawing.Tool)
            {
                case ChartDrawingTool.TrendLine:
                    g.DrawLine(pen, start, end); break;
                case ChartDrawingTool.TrendLineWithArrow:
                    g.DrawLine(pen, start, end); DrawArrowHead(g, pen, end, start); break;
                case ChartDrawingTool.TrendChannel:
                    DrawTrendChannel(g, pen, drawing, plot, visibleCountForDrawing, min, max); break;
                case ChartDrawingTool.HorizontalDoubleArrow:
                    g.DrawLine(pen, plot.Left, start.Y, plot.Right, start.Y); break;
                case ChartDrawingTool.VerticalDoubleArrow:
                    g.DrawLine(pen, start.X, plot.Top, start.X, plot.Bottom); break;
                case ChartDrawingTool.HorizontalRay:
                    var direction = end.X >= start.X ? 1f : -1f;
                    var rayEnd = new PointF(direction > 0 ? plot.Right : plot.Left, start.Y);
                    g.DrawLine(pen, start, rayEnd); break;
                case ChartDrawingTool.Rectangle:
                    var left = Math.Min(start.X, end.X);
                    var top = Math.Min(start.Y, end.Y);
                    var right = Math.Max(start.X, end.X);
                    var bottom = Math.Max(start.Y, end.Y);
                    var rect = RectangleF.FromLTRB(left, top, right, bottom);
                    using (var fill = new SolidBrush(Color.FromArgb(242, 248, 255))) g.FillRectangle(fill, rect);
                    using (var rectanglePen = new Pen(pen.Color, pen.Width) { DashStyle = pen.DashStyle }) g.DrawRectangle(rectanglePen, rect.X, rect.Y, rect.Width, rect.Height);
                    break;
            }
        }

        private void DrawSinglePreview(Graphics g, Pen pen, ChartDrawingTool tool, Point start, Point end, Rectangle plot)
        {
            switch (tool)
            {
                case ChartDrawingTool.TrendLine:
                    g.DrawLine(pen, start, end); break;
                case ChartDrawingTool.TrendLineWithArrow:
                    g.DrawLine(pen, start, end); DrawArrowHead(g, pen, end, start); break;
                case ChartDrawingTool.HorizontalDoubleArrow:
                    g.DrawLine(pen, plot.Left, start.Y, plot.Right, start.Y); break;
                case ChartDrawingTool.VerticalDoubleArrow:
                    g.DrawLine(pen, start.X, plot.Top, start.X, plot.Bottom); break;
                case ChartDrawingTool.HorizontalRay:
                    var direction = end.X >= start.X ? 1f : -1f;
                    var rayEnd = new PointF(direction > 0 ? plot.Right : plot.Left, start.Y);
                    g.DrawLine(pen, start, rayEnd); break;
                case ChartDrawingTool.Rectangle:
                    var left = Math.Min(start.X, end.X);
                    var top = Math.Min(start.Y, end.Y);
                    var width = Math.Abs(end.X - start.X);
                    var height = Math.Abs(end.Y - start.Y);
                    using (var fill = new SolidBrush(Color.FromArgb(242, 248, 255))) g.FillRectangle(fill, left, top, width, height);
                    using (var rectanglePen = new Pen(pen.Color, pen.Width) { DashStyle = pen.DashStyle }) g.DrawRectangle(rectanglePen, left, top, width, height);
                    break;
            }
        }

        private void DrawTrendChannel(Graphics g, Pen pen, ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var first = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var second = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
            var third = DataToScreen(drawing.X3, drawing.Y3, plot, visibleCountForDrawing, min, max);
            DrawScreenTrendChannel(g, pen, plot, first, second, third);
        }

        private static void DrawScreenTrendChannel(Graphics g, Pen pen, Rectangle plot, PointF first, PointF second, PointF third)
        {
            var dx = second.X - first.X;
            var dy = second.Y - first.Y;
            if (Math.Abs(dx) < 0.001f)
            {
                var offsetX = third.X - first.X;
                g.DrawLine(pen, first, second);
                g.DrawLine(pen, new PointF(first.X + offsetX, first.Y), new PointF(second.X + offsetX, second.Y));
                return;
            }
            var slope = dy / dx;
            var offset = third.Y - (first.Y + slope * (third.X - first.X));
            g.DrawLine(pen, first, second);
            g.DrawLine(pen, new PointF(first.X, first.Y + offset), new PointF(second.X, second.Y + offset));
        }

        private void GetDrawingScreenPoints(ChartDrawing drawing, Rectangle plot, int visibleCountForDrawing, double min, double max, out PointF start, out PointF end)
        {
            if (drawing.Tool == ChartDrawingTool.HorizontalDoubleArrow || drawing.Tool == ChartDrawingTool.VerticalDoubleArrow)
            {
                start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
                end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
                return;
            }
            start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
        }

        private PointF DataToScreen(double xValue, double yValue, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var x = (float)(plot.Left + step * (xValue + 0.5) + (-plot.Width * 0.25) + horizontalPanOffset);
            var y = PriceToScreen(yValue, plot, min, max);
            return new PointF(x, y);
        }
    }
}
