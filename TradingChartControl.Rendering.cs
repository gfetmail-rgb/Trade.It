using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            if (points.Count == 0)
            {
                base.OnPaint(e);
                return;
            }

            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0)
            {
                base.OnPaint(e);
                return;
            }
            GetVerticalRange(visible, out var min, out var max);

            using var gridPen = new Pen(Color.FromArgb(225, 225, 225), 1);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, 1.2f);
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, 1.2f);
            using var linePen = new Pen(ChartAppearanceSettings.LineChartColor, 1.6f);
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

            // Clip every chart object to the plot rectangle. This prevents panning,
            // trend lines and other drawings from ever painting over the price/time axes.
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
                using var previewPen = new Pen(GetDrawingColor(activeDrawingTool), 1.5f) { DashStyle = DashStyle.Dash };
                DrawDrawingPreview(e.Graphics, previewPen, plot, visible.Count, min, max);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var x = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);
                using var crosshairPen = new Pen(Color.FromArgb(120, 120, 120), 1) { DashStyle = DashStyle.Dot };
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

            DrawAdvancedTextLabels(e.Graphics, plot, visible.Count, min, max);

            // Paint-event overlays (axis labels/background and extra drawings) must
            // execute after the chart content, not before it.
            base.OnPaint(e);
        }

        private void DrawDrawings(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            for (var i = 0; i < drawings.Count; i++)
            {
                var selected = i == selectedDrawingIndex;
                var color = GetDrawingColor(drawings[i].Tool);
                using var drawingPen = new Pen(color, selected ? 3.2f : 1.8f);
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
                    using (var rectanglePen = new Pen(pen.Color, 1.0f)) g.DrawRectangle(rectanglePen, rect.X, rect.Y, rect.Width, rect.Height);
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
                    using (var rectanglePen = new Pen(pen.Color, 1.0f)) g.DrawRectangle(rectanglePen, left, top, width, height);
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
            if (drawing.Tool == ChartDrawingTool.HorizontalDoubleArrow)
            {
                var y = PriceToScreen(drawing.Y1, plot, min, max);
                start = new PointF(plot.Left, y); end = new PointF(plot.Right, y); return;
            }
            if (drawing.Tool == ChartDrawingTool.VerticalDoubleArrow)
            {
                var x = DataToScreen(drawing.X1, 0, plot, visibleCountForDrawing, min, max).X;
                start = new PointF(x, plot.Top); end = new PointF(x, plot.Bottom); return;
            }
            start = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
            end = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
        }

        private int HitTestDrawingHandle(Point location, Rectangle plot, out int handle)
        {
            handle = 0;
            if (drawings.Count == 0) return -1;
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visibleCountForDrawing = Math.Max(1, endIndex - firstIndex);
            var visible = points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();
            if (visible.Count == 0) return -1;
            GetVerticalRange(visible, out var min, out var max);
            const double tolerance = 9.0;
            for (var i = drawings.Count - 1; i >= 0; i--)
            {
                var handles = GetScreenHandles(drawings[i], plot, visibleCountForDrawing, min, max);
                for (var h = 0; h < handles.Count; h++)
                {
                    if (DistanceToPoint(location, handles[h]) <= tolerance)
                    {
                        handle = h + 1; return i;
                    }
                }
            }
            return -1;
        }

        private int HitTestDrawing(Point location, Rectangle plot)
        {
            if (drawings.Count == 0) return -1;
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visibleCountForDrawing = Math.Max(1, endIndex - firstIndex);
            var visible = points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();
            if (visible.Count == 0) return -1;
            GetVerticalRange(visible, out var min, out var max);
            const double tolerance = 7.0;
            for (var i = drawings.Count - 1; i >= 0; i--)
            {
                var drawing = drawings[i];
                GetDrawingScreenPoints(drawing, plot, visibleCountForDrawing, min, max, out var start, out var end);
                switch (drawing.Tool)
                {
                    case ChartDrawingTool.TrendLine:
                    case ChartDrawingTool.TrendLineWithArrow:
                    case ChartDrawingTool.HorizontalDoubleArrow:
                    case ChartDrawingTool.VerticalDoubleArrow:
                        if (DistanceToSegment(location, start, end) <= tolerance) return i;
                        break;
                    case ChartDrawingTool.TrendChannel:
                        var first = DataToScreen(drawing.X1, drawing.Y1, plot, visibleCountForDrawing, min, max);
                        var second = DataToScreen(drawing.X2, drawing.Y2, plot, visibleCountForDrawing, min, max);
                        var third = DataToScreen(drawing.X3, drawing.Y3, plot, visibleCountForDrawing, min, max);
                        if (IsPointNearChannel(location, plot, first, second, third, tolerance)) return i;
                        break;
                    case ChartDrawingTool.HorizontalRay:
                        var direction = end.X >= start.X ? 1f : -1f;
                        var rayEnd = new PointF(direction > 0 ? plot.Right : plot.Left, start.Y);
                        if (DistanceToSegment(location, start, rayEnd) <= tolerance) return i;
                        break;
                    case ChartDrawingTool.Rectangle:
                        var left = Math.Min(start.X, end.X);
                        var right = Math.Max(start.X, end.X);
                        var top = Math.Min(start.Y, end.Y);
                        var bottom = Math.Max(start.Y, end.Y);
                        var rect = new RectangleF(left, top, right - left, bottom - top);
                        if (rect.Contains(location) ||
                            DistanceToSegment(location, new PointF(left, top), new PointF(right, top)) <= tolerance ||
                            DistanceToSegment(location, new PointF(right, top), new PointF(right, bottom)) <= tolerance ||
                            DistanceToSegment(location, new PointF(right, bottom), new PointF(left, bottom)) <= tolerance ||
                            DistanceToSegment(location, new PointF(left, bottom), new PointF(left, top)) <= tolerance) return i;
                        break;
                }
            }
            return -1;
        }

        private static bool IsPointNearChannel(Point location, Rectangle plot, PointF first, PointF second, PointF third, double tolerance)
        {
            var dx = second.X - first.X;
            var dy = second.Y - first.Y;
            if (Math.Abs(dx) < 0.001f)
            {
                var offsetX = third.X - first.X;
                var parallelFirst = new PointF(first.X + offsetX, first.Y);
                var parallelSecond = new PointF(second.X + offsetX, second.Y);
                return DistanceToSegment(location, first, second) <= tolerance || DistanceToSegment(location, parallelFirst, parallelSecond) <= tolerance;
            }
            var slope = dy / dx;
            var offset = third.Y - (first.Y + slope * (third.X - first.X));
            var parallelFirstPoint = new PointF(first.X, first.Y + offset);
            var parallelSecondPoint = new PointF(second.X, second.Y + offset);
            return DistanceToSegment(location, first, second) <= tolerance || DistanceToSegment(location, parallelFirstPoint, parallelSecondPoint) <= tolerance;
        }

        private void MoveOrResizeDrawing(int index, int handle, Point location)
        {
            if (index < 0 || index >= drawings.Count) return;
            var plot = GetPlotRectangle();
            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList();
            if (visible.Count == 0) return;
            GetVerticalRange(visible, out var min, out var max);
            var drawing = drawings[index];
            if (drawing.Tool == ChartDrawingTool.HorizontalDoubleArrow)
            {
                drawing.Y1 = ScreenToPrice(location.Y, plot, min, max);
                drawing.Y2 = drawing.Y1; return;
            }
            if (drawing.Tool == ChartDrawingTool.VerticalDoubleArrow)
            {
                var x = ScreenToDataX(location.X, plot, visible.Count);
                drawing.X1 = x; drawing.X2 = x; return;
            }
            if (drawing.Tool == ChartDrawingTool.TrendChannel)
            {
                if (handle == 1) { drawing.X1 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y1 = ScreenToPrice(location.Y, plot, min, max); return; }
                if (handle == 2) { drawing.X2 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y2 = ScreenToPrice(location.Y, plot, min, max); return; }
                if (handle == 3) { drawing.X3 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y3 = ScreenToPrice(location.Y, plot, min, max); return; }
                var deltaX = ScreenToDataX(location.X, plot, visible.Count) - ScreenToDataX(draggingLastPoint.X, plot, visible.Count);
                var deltaY = ScreenToPrice(location.Y, plot, min, max) - ScreenToPrice(draggingLastPoint.Y, plot, min, max);
                drawing.X1 += deltaX; drawing.X2 += deltaX; drawing.X3 += deltaX;
                drawing.Y1 += deltaY; drawing.Y2 += deltaY; drawing.Y3 += deltaY; return;
            }
            if (drawing.Tool == ChartDrawingTool.Rectangle && handle > 0)
            {
                var x = ScreenToDataX(location.X, plot, visible.Count);
                var y = ScreenToPrice(location.Y, plot, min, max);
                switch (handle)
                {
                    case 1: drawing.X1 = x; drawing.Y1 = y; break;
                    case 2: drawing.X2 = x; drawing.Y1 = y; break;
                    case 3: drawing.X2 = x; drawing.Y2 = y; break;
                    case 4: drawing.X1 = x; drawing.Y2 = y; break;
                }
                return;
            }
            if (handle == 1) { drawing.X1 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y1 = ScreenToPrice(location.Y, plot, min, max); return; }
            if (handle == 2) { drawing.X2 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y2 = ScreenToPrice(location.Y, plot, min, max); return; }
            var moveX = ScreenToDataX(location.X, plot, visible.Count) - ScreenToDataX(draggingLastPoint.X, plot, visible.Count);
            var moveY = ScreenToPrice(location.Y, plot, min, max) - ScreenToPrice(draggingLastPoint.Y, plot, min, max);
            drawing.X1 += moveX; drawing.X2 += moveX;
            drawing.Y1 += moveY; drawing.Y2 += moveY;
        }

        private static double DistanceToPoint(PointF point, PointF target)
        {
            var dx = point.X - target.X;
            var dy = point.Y - target.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private static double DistanceToSegment(PointF point, PointF start, PointF end)
        {
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            if (Math.Abs(dx) < 0.001 && Math.Abs(dy) < 0.001) return DistanceToPoint(point, start);
            var t = ((point.X - start.X) * dx + (point.Y - start.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0f, 1f);
            var nearestX = start.X + t * dx;
            var nearestY = start.Y + t * dy;
            return Math.Sqrt(Math.Pow(point.X - nearestX, 2) + Math.Pow(point.Y - nearestY, 2));
        }

        private static void DrawArrowHead(Graphics g, Pen basePen, PointF tip, PointF from)
        {
            var dx = tip.X - from.X;
            var dy = tip.Y - from.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length < 0.001) return;
            const float size = 9f;
            var ux = (float)(dx / length);
            var uy = (float)(dy / length);
            var px = -uy;
            var py = ux;
            var left = new PointF(tip.X - ux * size + px * size * 0.45f, tip.Y - uy * size + py * size * 0.45f);
            var right = new PointF(tip.X - ux * size - px * size * 0.45f, tip.Y - uy * size - py * size * 0.45f);
            g.DrawLine(basePen, tip, left);
            g.DrawLine(basePen, tip, right);
        }

        private Rectangle GetPlotRectangle()
        {
            var left = 55;
            var top = 15;
            var right = Math.Max(left + 1, Width - 15);
            var bottom = Math.Max(top + 1, Height - 35);
            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        private void GetVerticalRange(List<TradingChartPoint> visible, out double min, out double max)
        {
            min = visible.Min(x => x.Low);
            max = visible.Max(x => x.High);
            var range = Math.Max(max - min, Math.Max(Math.Abs(max), 1.0) * 0.01);
            var center = (min + max) / 2.0 + verticalPanOffset;
            var adjustedRange = range / verticalZoom;
            min = center - adjustedRange / 2.0;
            max = center + adjustedRange / 2.0;
        }

        private float PriceToScreen(double price, Rectangle plot, double min, double max)
        {
            if (Math.Abs(max - min) < 1e-12) return plot.Top + plot.Height / 2f;
            return (float)(plot.Bottom - (price - min) / (max - min) * plot.Height);
        }

        private PointF DataToScreen(double x, double y, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var initialOffset = -plot.Width * 0.25;
            var screenX = plot.Left + step * (x + 0.5) + initialOffset + horizontalPanOffset;
            return new PointF((float)screenX, PriceToScreen(y, plot, min, max));
        }

        private double ScreenToDataX(float screenX, Rectangle plot, int visibleCountForDrawing)
        {
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var initialOffset = -plot.Width * 0.25;
            return (screenX - plot.Left - initialOffset - horizontalPanOffset) / step - 0.5;
        }

        private double ScreenToPrice(float screenY, Rectangle plot, double min, double max)
        {
            if (plot.Height <= 0) return min;
            var ratio = (plot.Bottom - screenY) / (double)plot.Height;
            return min + ratio * (max - min);
        }

        private bool IsInsidePlot(PointF point) => GetPlotRectangle().Contains(Point.Round(point));
    }
}
