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
            var naturalEndIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visible = points.Skip(firstIndex).Take(Math.Max(1, naturalEndIndex - firstIndex)).ToList();
            if (visible.Count == 0)
                return;

            // Test Mode فقط تعداد کندل‌های نمایش‌داده‌شده را محدود می‌کند؛
            // تمام تبدیل‌های مختصات و مقیاس‌بندی دقیقاً همان حالت عادی است.
            GetVerticalRange(visible, out var min, out var max);
            var displayedCount = testMode && testEndIndex >= firstIndex
                ? Math.Clamp(testEndIndex - firstIndex + 1, 0, visible.Count)
                : visible.Count;

            using var gridPen = new Pen(LineAppearanceSettings.GridColor, LineAppearanceSettings.GridLineWidth) { DashStyle = LineAppearanceSettings.GridLineStyle };
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var linePen = new Pen(ChartAppearanceSettings.LineChartColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);
            using var headerFont = new Font(Font.FontFamily, Math.Max(8.0f, Font.Size), FontStyle.Bold);

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

            var layoutCount = Math.Max(1, visible.Count);
            var step = plot.Width / (double)layoutCount;
            var initialOffset = -plot.Width * 0.25;

            DrawRectangleFillsBehindChart(e.Graphics, plot, layoutCount, min, max);

            if (drawingInProgress && activeDrawingTool == ChartDrawingTool.Rectangle && IsInsidePlot(drawingCurrentPoint))
                DrawRectanglePreviewFillBehindChart(e.Graphics, drawingStartPoint, drawingCurrentPoint);

            if (chartType == TradingChartType.Line)
            {
                var linePoints = new List<PointF>();
                for (var i = 0; i < displayedCount; i++)
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
                for (var i = 0; i < displayedCount; i++)
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

            DrawDrawings(e.Graphics, plot, layoutCount, min, max);
            RenderAdvancedDrawings(e.Graphics);
            // Extra drawingها باید دقیقاً با همان مختصات داده/صفحه‌ای
            // رندر شوند که چارت اصلی در مد تست استفاده می‌کند.
            // استفاده از visible.Count در اینجا باعث جهش Pitchfork و
            // Fibonacci Extension می‌شد چون visible در مد تست کوتاه‌تر است.
            RenderExtraDrawings(e.Graphics, plot, layoutCount, min, max);

            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None && IsInsidePlot(drawingCurrentPoint))
            {
                using var previewPen = new Pen(GetDrawingColor(activeDrawingTool), LineAppearanceSettings.DrawingLineWidth) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                DrawDrawingPreview(e.Graphics, previewPen, plot, layoutCount, min, max);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < displayedCount)
            {
                var x = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);
                using var crosshairPen = new Pen(LineAppearanceSettings.CrosshairColor, LineAppearanceSettings.CrosshairLineWidth) { DashStyle = LineAppearanceSettings.CrosshairLineStyle };
                e.Graphics.DrawLine(crosshairPen, plot.Left, crosshairPoint.Y, plot.Right, crosshairPoint.Y);
            }

            e.Graphics.Restore(chartState);

            var volumePlot = GetVolumePlotRectangle();
            RenderVolumePanel(e.Graphics, volumePlot, visible.Take(displayedCount).ToList(), step, initialOffset);

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var crosshairX = (float)(plot.Left + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset);
                using var fullCrosshairPen = new Pen(LineAppearanceSettings.CrosshairColor, LineAppearanceSettings.CrosshairLineWidth)
                {
                    DashStyle = LineAppearanceSettings.CrosshairLineStyle
                };
                e.Graphics.DrawLine(
                    fullCrosshairPen,
                    crosshairX,
                    plot.Top,
                    crosshairX,
                    volumePlot == Rectangle.Empty ? plot.Bottom : volumePlot.Bottom);
            }

            for (var i = 0; i <= 5; i++)
            {
                var value = max - (max - min) * i / 5.0;
                var y = PriceToScreen(value, plot, min, max);
                var text = value.ToString("0.##");
                var size = e.Graphics.MeasureString(text, axisTextFont);

                // برچسب قیمت باید کاملاً در ناحیه اختصاص‌یافته به محور قیمت
                // قرار بگیرد و هرگز وارد محدوده Plot نشود.
                // راست‌چین کردن متن داخل یک ناحیه ثابت، مشکل سرریز عددهای
                // طولانی را بدون تغییر مقیاس یا مختصات خود نمودار حل می‌کند.
                var axisLabelWidth = Math.Max(1f, plot.Left - 7f);
                var axisLabelRect = new RectangleF(
                    1f,
                    y - size.Height / 2f,
                    axisLabelWidth,
                    size.Height);

                using var axisLabelFormat = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                };

                e.Graphics.DrawString(
                    text,
                    axisTextFont,
                    textBrush,
                    axisLabelRect,
                    axisLabelFormat);
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

                if (!IsSyntheticNoDateAxis() && crosshairIndex >= 0 && crosshairIndex < displayedCount)
                {
                    var date = visible[crosshairIndex].Date;
                    var calendar = new PersianCalendar();
                    var timeText = $"\u200E{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}";
                    var timeSize = e.Graphics.MeasureString(timeText, axisTextFont);
                    var timeRect = new RectangleF(
                        Math.Clamp(crosshairX - timeSize.Width / 2f - 3f, plot.Left, Math.Max(plot.Left, Width - timeSize.Width - 6f)),
                        (volumePlot == Rectangle.Empty ? plot.Bottom : volumePlot.Bottom) + 2f,
                        timeSize.Width + 6f,
                        timeSize.Height + 4f);

                    e.Graphics.FillRectangle(crosshairLabelBrush, timeRect);
                    e.Graphics.DrawString(timeText, axisTextFont, crosshairLabelTextBrush, timeRect.X + 3f, timeRect.Y + 2f);
                }
            }

            // Chart header: symbol and OHLCV for the candle under the crosshair.
            var headerIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count
                ? crosshairIndex
                : visible.Count - 1;
            if (displayedCount == 0)
                return;
            var headerPoint = visible[Math.Clamp(headerIndex, 0, displayedCount - 1)];
            var headerText = string.IsNullOrWhiteSpace(chartSymbol)
                ? $"O: {headerPoint.Open:0.##}   H: {headerPoint.High:0.##}   L: {headerPoint.Low:0.##}   C: {headerPoint.Close:0.##}   V: {headerPoint.Volume:N0}"
                : $"{chartSymbol}    O: {headerPoint.Open:0.##}   H: {headerPoint.High:0.##}   L: {headerPoint.Low:0.##}   C: {headerPoint.Close:0.##}   V: {headerPoint.Volume:N0}";
            using (var headerBrush = new SolidBrush(Color.FromArgb(45, 45, 45)))
                e.Graphics.DrawString(headerText, headerFont, headerBrush, plot.Left + 16f, 2f);

            DrawAdvancedTextLabels(e.Graphics, plot, layoutCount, min, max);
        }

        private void DrawRectangleFillsBehindChart(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            if (drawings.Count == 0)
                return;

            using var fill = new SolidBrush(Color.FromArgb(242, 248, 255));
            foreach (var drawing in drawings)
            {
                if (drawing.Tool != ChartDrawingTool.Rectangle)
                    continue;

                GetDrawingScreenPoints(drawing, plot, visibleCountForDrawing, min, max, out var start, out var end);
                var left = Math.Min(start.X, end.X);
                var top = Math.Min(start.Y, end.Y);
                var right = Math.Max(start.X, end.X);
                var bottom = Math.Max(start.Y, end.Y);
                if (right > left && bottom > top)
                    g.FillRectangle(fill, RectangleF.FromLTRB(left, top, right, bottom));
            }
        }

        private static void DrawRectanglePreviewFillBehindChart(Graphics g, Point start, Point end)
        {
            var left = Math.Min(start.X, end.X);
            var top = Math.Min(start.Y, end.Y);
            var right = Math.Max(start.X, end.X);
            var bottom = Math.Max(start.Y, end.Y);
            if (right <= left || bottom <= top)
                return;

            using var fill = new SolidBrush(Color.FromArgb(242, 248, 255));
            g.FillRectangle(fill, RectangleF.FromLTRB(left, top, right, bottom));
        }

        private void DrawDrawings(Graphics g, Rectangle plot, int visibleCountForDrawing, double min, double max)
        {
            for (var i = 0; i < drawings.Count; i++)
            {
                var selected = i == selectedDrawingIndex;
                var color = GetDrawingColor(drawings[i].Tool);
                var width = selected ? LineAppearanceSettings.DrawingLineWidth + 1.4f : LineAppearanceSettings.DrawingLineWidth;
                using var drawingPen = new Pen(color, width) { DashStyle = LineAppearanceSettings.DrawingLineStyle };
                DrawSingleDrawing(g, drawingPen, drawings[i], plot, visibleCountForDrawing, min, max);                if (selected)
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
                case ChartDrawingTool.HorizontalLine:
                    g.DrawLine(pen, plot.Left, start.Y, plot.Right, start.Y); break;
                case ChartDrawingTool.VerticalLine:
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
                    using (var rectanglePen = new Pen(pen.Color, pen.Width) { DashStyle = pen.DashStyle })
                        g.DrawRectangle(rectanglePen, left, top, right - left, bottom - top);
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
                case ChartDrawingTool.HorizontalLine:
                    g.DrawLine(pen, plot.Left, start.Y, plot.Right, start.Y); break;
                case ChartDrawingTool.VerticalLine:
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
                    using (var rectanglePen = new Pen(pen.Color, pen.Width) { DashStyle = pen.DashStyle })
                        g.DrawRectangle(rectanglePen, left, top, width, height);
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
            if (drawing.Tool == ChartDrawingTool.HorizontalLine)
            {
                var y = PriceToScreen(drawing.Y1, plot, min, max);
                start = new PointF(plot.Left, y); end = new PointF(plot.Right, y); return;
            }
            if (drawing.Tool == ChartDrawingTool.VerticalLine)
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
            var visibleCountForDrawing = GetDrawingLayoutCount();
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
                    case ChartDrawingTool.HorizontalLine:
                    case ChartDrawingTool.VerticalLine:
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
            if (drawing.Tool == ChartDrawingTool.HorizontalLine)
            {
                drawing.Y1 = ScreenToPrice(location.Y, plot, min, max);
                drawing.Y2 = drawing.Y1; return;
            }
            if (drawing.Tool == ChartDrawingTool.VerticalLine)            {
                var x = ScreenToDataX(location.X, plot, GetDrawingLayoutCount());
                drawing.X1 = x; drawing.X2 = x; return;
            }
            if (drawing.Tool == ChartDrawingTool.TrendChannel)
            {
                if (handle == 1) { drawing.X1 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y1 = ScreenToPrice(location.Y, plot, min, max); return; }
                if (handle == 2) { drawing.X2 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y2 = ScreenToPrice(location.Y, plot, min, max); return; }
                if (handle == 3) { drawing.X3 = ScreenToDataX(location.X, plot, visible.Count); drawing.Y3 = ScreenToPrice(location.Y, plot, min, max); return; }
                var deltaX = ScreenToDataX(location.X, plot, visible.Count) - ScreenToDataX(draggingLastPoint.X, plot, GetDrawingLayoutCount());
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
            var overallBottom = Math.Max(top + 1, Height - 35);

            // وقتی پنل اندیکاتور/حجم مخفی است، چارت قیمت باید تمام فضای
            // آزاد تا انتهای ناحیه چارت را در اختیار داشته باشد.
            if (!volumePanelVisible)
                return Rectangle.FromLTRB(left, top, right, overallBottom);

            var gap = Math.Clamp(volumePanelGap, 2, 30);
            var totalHeight = Math.Max(120, overallBottom - top);
            var volumeHeight = Math.Clamp(
                (int)Math.Round(totalHeight * volumePanelRatio),
                60,
                Math.Max(60, totalHeight / 2));

            var bottom = Math.Max(
                top + 80,
                overallBottom - volumeHeight - gap);

            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        private Rectangle GetVolumePlotRectangle()
        {
            if (!volumePanelVisible)
                return Rectangle.Empty;

            var pricePlot = GetPlotRectangle();
            var left = pricePlot.Left;
            var right = pricePlot.Right;
            var overallBottom = Math.Max(pricePlot.Top + 1, Height - 35);
            var top = Math.Min(overallBottom - 1, pricePlot.Bottom + Math.Clamp(volumePanelGap, 2, 30));
            return Rectangle.FromLTRB(left, top, right, overallBottom);
        }

        private void RenderVolumePanel(
            Graphics g,
            Rectangle volumePlot,
            List<TradingChartPoint> visible,
            double step,
            double initialOffset)
        {
            if (visible.Count == 0 || volumePlot.Width <= 0 || volumePlot.Height <= 0)
                return;

            using var separatorPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, LineAppearanceSettings.ChartLineWidth);
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, LineAppearanceSettings.ChartLineWidth);
            using var textBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);

            g.DrawLine(separatorPen, volumePlot.Left, volumePlot.Top, volumePlot.Right, volumePlot.Top);
            g.DrawLine(axisPen, volumePlot.Left, volumePlot.Bottom, volumePlot.Right, volumePlot.Bottom);
            g.DrawLine(axisPen, volumePlot.Left, volumePlot.Top, volumePlot.Left, volumePlot.Bottom);

            var minVolume = visible.Min(x => Math.Max(0.0, x.Volume));
            var maxVolume = visible.Max(x => Math.Max(0.0, x.Volume));
            var topPadding = Math.Max(2f, volumePlot.Height * 0.05f);
            var usableHeight = Math.Max(1f, volumePlot.Height - topPadding);
            var range = maxVolume - minVolume;

            float VolumeToScreen(double volume)
            {
                if (range <= 1e-12)
                    return volumePlot.Bottom - usableHeight * 0.5f;

                var ratio = (Math.Max(minVolume, Math.Min(maxVolume, volume)) - minVolume) / range;
                return (float)(volumePlot.Bottom - ratio * usableHeight);
            }

            var candleWidth = Math.Max(2f, (float)(step * 0.65));
            for (var i = 0; i < visible.Count; i++)
            {
                var item = visible[i];
                var x = (float)(volumePlot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                var y = VolumeToScreen(item.Volume);
                var rising = item.Close >= item.Open;
                var brush = rising ? risingBrush : fallingBrush;
                var pen = rising ? risingPen : fallingPen;

                var top = Math.Min(y, volumePlot.Bottom);
                var height = Math.Max(1f, volumePlot.Bottom - top);
                var rect = RectangleF.FromLTRB(
                    x - candleWidth / 2f,
                    top,
                    x + candleWidth / 2f,
                    volumePlot.Bottom);

                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, Math.Max(1f, height));
            }

            var maxText = maxVolume.ToString("N0");
            var minText = minVolume.ToString("N0");
            g.DrawString(maxText, labelFont, textBrush, volumePlot.Left + 4f, volumePlot.Top + 1f);
            g.DrawString(minText, labelFont, textBrush, volumePlot.Left + 4f, volumePlot.Bottom - labelFont.GetHeight(g) - 1f);

            using var titleBrush = new SolidBrush(Color.FromArgb(90, 90, 90));
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            g.DrawString("حجم", titleFont, titleBrush, volumePlot.Left + 45f, volumePlot.Top + 1f);
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