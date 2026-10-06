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
            var displayedCount = testMode && testEndIndex >= firstIndex
                ? Math.Clamp(testEndIndex - firstIndex + 1, 0, visible.Count)
                : visible.Count;
            if (displayedCount <= 0)
                return;

            // در حالت تست، کندل‌های آینده نباید در تعیین مقیاس عمودی اثر داشته باشند.
            var scaledVisible = displayedCount == visible.Count
                ? visible
                : visible.Take(displayedCount).ToList();
            GetVerticalRange(scaledVisible, out var min, out var max);

            using var gridPen = new Pen(LineAppearanceSettings.GridColor, LineAppearanceSettings.GridLineWidth) { DashStyle = LineAppearanceSettings.GridLineStyle };
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1);
            using var textBrush = new SolidBrush(Color.FromArgb(70, 70, 70));
            using var risingBrush = new SolidBrush(ChartAppearanceSettings.RisingCandleColor);
            using var fallingBrush = new SolidBrush(ChartAppearanceSettings.FallingCandleColor);
            using var risingPen = new Pen(ChartAppearanceSettings.RisingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var fallingPen = new Pen(ChartAppearanceSettings.FallingCandleColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var linePen = new Pen(ChartAppearanceSettings.LineChartColor, LineAppearanceSettings.ChartLineWidth) { DashStyle = LineAppearanceSettings.ChartLineStyle };
            using var axisTextFont = new Font(Font.FontFamily, Math.Max(7.0f, Font.Size - 2.0f), Font.Style);
            using var priceAxisTextFont = new Font(Font.FontFamily, Math.Max(6.0f, Font.Size - 3.0f), Font.Style);
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

            DrawIndicators(e.Graphics, plot, layoutCount, min, max, displayedCount, step, initialOffset);

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
                var x = (float)(plot.Left + step * ((crosshairPosition >= 0 ? crosshairPosition : crosshairIndex) + 0.5) + initialOffset + horizontalPanOffset);
                using var crosshairPen = new Pen(LineAppearanceSettings.CrosshairColor, LineAppearanceSettings.CrosshairLineWidth) { DashStyle = LineAppearanceSettings.CrosshairLineStyle };
                e.Graphics.DrawLine(crosshairPen, plot.Left, crosshairPoint.Y, plot.Right, crosshairPoint.Y);
            }

            RenderPriceIndicatorTitles(e.Graphics, plot, min, max, visible.Count, step, initialOffset);
            e.Graphics.Restore(chartState);

            var panelVisible = visible.Take(displayedCount).ToList();

            var rsiPlot = GetRsiPlotRectangle();
            if (rsiPlot != Rectangle.Empty)
                RenderClippedPanel(e.Graphics, rsiPlot, g => RenderRsiPanel(g, rsiPlot, panelVisible, step, initialOffset));

            var macdPlot = GetMacdPlotRectangle();
            if (macdPlot != Rectangle.Empty)
                RenderClippedPanel(e.Graphics, macdPlot, g => RenderMacdPanel(g, macdPlot, panelVisible, step, initialOffset));

            var stochasticPlot = GetStochasticPlotRectangle();
            if (stochasticPlot != Rectangle.Empty && HasStochasticIndicator)
                RenderClippedPanel(e.Graphics, stochasticPlot, g => RenderStochasticPanel(g, stochasticPlot, panelVisible, step, initialOffset));

            var atrPlot = GetAtrPlotRectangle();
            if (atrPlot != Rectangle.Empty && HasAtrIndicator)
                RenderClippedPanel(e.Graphics, atrPlot, g => RenderAtrPanel(g, atrPlot, panelVisible, step, initialOffset));

            var adxPlot = GetAdxPlotRectangle();
            if (adxPlot != Rectangle.Empty && HasAdxIndicator)
                RenderClippedPanel(e.Graphics, adxPlot, g => RenderAdxPanel(g, adxPlot, panelVisible, step, initialOffset));

            var stochasticRsiPlot = GetStochasticRsiPlotRectangle();
            if (stochasticRsiPlot != Rectangle.Empty && HasStochasticRsiIndicator)
                RenderClippedPanel(e.Graphics, stochasticRsiPlot, g => RenderStochasticRsiPanel(g, stochasticRsiPlot, panelVisible, step, initialOffset));

            var obvPlot = GetObvPlotRectangle();
            if (obvPlot != Rectangle.Empty && HasObvIndicator)
                RenderClippedPanel(e.Graphics, obvPlot, g => RenderObvPanel(g, obvPlot, panelVisible, step, initialOffset));

            var volumePlot = GetVolumePlotRectangle();
            if (volumePlot != Rectangle.Empty)
                RenderClippedPanel(e.Graphics, volumePlot, g => RenderVolumePanel(g, volumePlot, panelVisible, step, initialOffset));

            // خطوط جداکننده پنل‌ها نقش Splitter را نیز به‌صورت بصری مشخص می‌کنند.
            using (var splitterPen = new Pen(Color.FromArgb(145, 145, 145), 1f))
            {
                if (rsiPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, rsiPlot.Top - 1, plot.Right, rsiPlot.Top - 1);

                if (atrPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, atrPlot.Top - 1, plot.Right, atrPlot.Top - 1);

                if (obvPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, obvPlot.Top - 1, plot.Right, obvPlot.Top - 1);

                if (volumePlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, volumePlot.Top - 1, plot.Right, volumePlot.Top - 1);

                if (adxPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, adxPlot.Top - 1, plot.Right, adxPlot.Top - 1);

                if (macdPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, macdPlot.Top - 1, plot.Right, macdPlot.Top - 1);

                if (stochasticRsiPlot != Rectangle.Empty)
                    e.Graphics.DrawLine(splitterPen, plot.Left, stochasticRsiPlot.Top - 1, plot.Right, stochasticRsiPlot.Top - 1);
            }

            // محور زمان: بر اساس تاریخ واقعی کندل‌ها، با تعداد Tick متناسب با فضای موجود.
            // در محورهای مصنوعی/بدون تاریخ، این بخش عمداً چیزی رسم نمی‌کند تا NoDateAxis مسئول نمایش شماره کندل بماند.
            if (!IsSyntheticNoDateAxis())
                DrawTimeAxis(e.Graphics, plot, volumePlot, visible, displayedCount, step, initialOffset, axisTextFont, axisPen, textBrush);

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var crosshairX = (float)(plot.Left + step * ((crosshairPosition >= 0 ? crosshairPosition : crosshairIndex) + 0.5) + initialOffset + horizontalPanOffset);
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

            var priceDecimalPlaces = GetPriceDecimalPlaces(visible);
            for (var i = 0; i <= 5; i++)
            {
                var value = max - (max - min) * i / 5.0;
                var y = PriceToScreen(value, plot, min, max);
                var text = FormatPrice(value, priceDecimalPlaces);
                var size = e.Graphics.MeasureString(text, priceAxisTextFont);

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
                    priceAxisTextFont,
                    textBrush,
                    axisLabelRect,
                    axisLabelFormat);
            }

            if (showCrosshair && crosshairIndex >= 0 && crosshairIndex < visible.Count)
            {
                var crosshairX = (float)(plot.Left + step * ((crosshairPosition >= 0 ? crosshairPosition : crosshairIndex) + 0.5) + initialOffset + horizontalPanOffset);
                var crosshairY = Math.Clamp(crosshairPoint.Y, plot.Top, plot.Bottom);
                var crosshairPrice = max - ((crosshairY - plot.Top) / (double)Math.Max(1, plot.Height)) * (max - min);
                crosshairPrice = Math.Clamp(crosshairPrice, min, max);
                var priceText = FormatPrice(crosshairPrice, priceDecimalPlaces);
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
                ? $"{chartTimeFrame}    O: {headerPoint.Open:0.##}   H: {headerPoint.High:0.##}   L: {headerPoint.Low:0.##}   C: {headerPoint.Close:0.##}   V: {headerPoint.Volume:N0}"
                : $"{chartSymbol}   {chartTimeFrame}    O: {headerPoint.Open:0.##}   H: {headerPoint.High:0.##}   L: {headerPoint.Low:0.##}   C: {headerPoint.Close:0.##}   V: {headerPoint.Volume:N0}";
            using (var headerBrush = new SolidBrush(Color.FromArgb(45, 45, 45)))
                e.Graphics.DrawString(headerText, headerFont, headerBrush, plot.Left + 16f, 2f);

            DrawAdvancedTextLabels(e.Graphics, plot, layoutCount, min, max);
        }


        private Rectangle GetLowestPanelRectangle(Rectangle pricePlot, Rectangle volumePlot)
        {
            if (volumePlot != Rectangle.Empty)
                return volumePlot;

            var atrPlot = GetAtrPlotRectangle();
            if (atrPlot != Rectangle.Empty)
                return atrPlot;

            var stochasticRsiPlot = GetStochasticRsiPlotRectangle();
            if (stochasticRsiPlot != Rectangle.Empty)
                return stochasticRsiPlot;

            var stochasticPlot = GetStochasticPlotRectangle();
            if (stochasticPlot != Rectangle.Empty)
                return stochasticPlot;

            var macdPlot = GetMacdPlotRectangle();
            if (macdPlot != Rectangle.Empty)
                return macdPlot;

            var rsiPlot = GetRsiPlotRectangle();
            if (rsiPlot != Rectangle.Empty)
                return rsiPlot;

            return pricePlot;
        }

        private void DrawTimeAxis(
            Graphics g,
            Rectangle plot,
            Rectangle volumePlot,
            List<TradingChartPoint> visible,
            int displayedCount,
            double step,
            double initialOffset,
            Font labelFont,
            Pen axisPen,
            Brush textBrush)
        {
            if (displayedCount <= 0 || plot.Width <= 0)
                return;

            var axisPanel = GetLowestPanelRectangle(plot, volumePlot);
            var axisBottom = axisPanel.Bottom;
            var axisY = axisBottom - 1f;
            g.DrawLine(axisPen, plot.Left, axisBottom, plot.Right, axisBottom);

            var sample = visible.Take(displayedCount).ToList();
            if (sample.Count == 0)
                return;

            var calendar = new PersianCalendar();
            var hasIntradayTime = sample.Any(p => p.Date.TimeOfDay != TimeSpan.Zero);
            var totalSpan = sample[^1].Date - sample[0].Date;

            string FormatTime(DateTime date)
            {
                if (!hasIntradayTime)
                    return $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00}";

                if (totalSpan.TotalDays <= 1.5)
                    return $"{calendar.GetHour(date):00}:{calendar.GetMinute(date):00}";

                if (totalSpan.TotalDays <= 7)
                    return $"{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {calendar.GetHour(date):00}:{calendar.GetMinute(date):00}";

                return $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00}";
            }

            var firstLabel = FormatTime(sample[0].Date);
            var measured = g.MeasureString(firstLabel, labelFont);
            var minSpacing = Math.Max(70f, measured.Width + 18f);
            var maxTicks = Math.Max(2, (int)(plot.Width / minSpacing) + 1);
            var tickCount = Math.Min(sample.Count, maxTicks);

            if (tickCount == 1 && sample.Count > 1)
                tickCount = 2;

            var usedX = new List<float>();
            for (var t = 0; t < tickCount; t++)
            {
                var index = tickCount == 1
                    ? 0
                    : (int)Math.Round(t * (sample.Count - 1) / (double)(tickCount - 1));

                var x = (float)(plot.Left + step * (index + 0.5) + initialOffset + horizontalPanOffset);
                if (x < plot.Left || x > plot.Right)
                    continue;

                var label = FormatTime(sample[index].Date);
                var size = g.MeasureString(label, labelFont);

                // اگر دو برچسب در اثر گرد شدن شاخص‌ها به هم نزدیک شدند، Tick دوم حذف می‌شود.
                if (usedX.Any(previous => Math.Abs(previous - x) < Math.Max(45f, size.Width + 10f)))
                    continue;

                usedX.Add(x);

                g.DrawLine(axisPen, x, axisBottom, x, axisBottom + 5f);

                var labelX = Math.Clamp(
                    x - size.Width / 2f,
                    plot.Left,
                    Math.Max(plot.Left, plot.Right - size.Width));

                g.DrawString(
                    label,
                    labelFont,
                    textBrush,
                    labelX,
                    axisBottom + 4f);
            }
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
            using var handlePen = new Pen(GetDrawingColor(drawing.Tool), 1.5f);            const float radius = 4f;
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
                var third = DataToScreen(drawing.X3 - firstIndex, drawing.Y3, plot, visibleCountForDrawing, min, max);
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
            var first = DataToScreen(drawing.X1 - firstIndex, drawing.Y1, plot, visibleCountForDrawing, min, max);
            var second = DataToScreen(drawing.X2 - firstIndex, drawing.Y2, plot, visibleCountForDrawing, min, max);
            var third = DataToScreen(drawing.X3 - firstIndex, drawing.Y3, plot, visibleCountForDrawing, min, max);
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
                var x = DataToScreen(drawing.X1 - firstIndex, 0, plot, visibleCountForDrawing, min, max).X;
                start = new PointF(x, plot.Top); end = new PointF(x, plot.Bottom); return;
            }
            start = DataToScreen(drawing.X1 - firstIndex, drawing.Y1, plot, visibleCountForDrawing, min, max);
            end = DataToScreen(drawing.X2 - firstIndex, drawing.Y2, plot, visibleCountForDrawing, min, max);
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
                        var first = DataToScreen(drawing.X1 - firstIndex, drawing.Y1, plot, visibleCountForDrawing, min, max);
                        var second = DataToScreen(drawing.X2 - firstIndex, drawing.Y2, plot, visibleCountForDrawing, min, max);
                        var third = DataToScreen(drawing.X3 - firstIndex, drawing.Y3, plot, visibleCountForDrawing, min, max);
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
                var parallelFirst = new PointF(first.X + offsetX, first.Y);                var parallelSecond = new PointF(second.X + offsetX, second.Y);
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
                var x = ScreenToDataX(location.X, plot, GetDrawingLayoutCount()) + firstIndex;
                drawing.X1 = x; drawing.X2 = x;
                drawing.Date1 = GetPointDate((int)Math.Round(x));
                drawing.Date2 = drawing.Date1;
                return;
            }
            if (drawing.Tool == ChartDrawingTool.TrendChannel)
            {
                if (handle == 1) { drawing.X1 = ScreenToDataX(location.X, plot, visible.Count) + firstIndex; drawing.Y1 = ScreenToPrice(location.Y, plot, min, max); drawing.Date1 = GetPointDate((int)Math.Round(drawing.X1)); return; }
                if (handle == 2) { drawing.X2 = ScreenToDataX(location.X, plot, visible.Count) + firstIndex; drawing.Y2 = ScreenToPrice(location.Y, plot, min, max); drawing.Date2 = GetPointDate((int)Math.Round(drawing.X2)); return; }
                if (handle == 3) { drawing.X3 = ScreenToDataX(location.X, plot, visible.Count) + firstIndex; drawing.Y3 = ScreenToPrice(location.Y, plot, min, max); drawing.Date3 = GetPointDate((int)Math.Round(drawing.X3)); return; }
                var deltaX = ScreenToDataX(location.X, plot, visible.Count) - ScreenToDataX(draggingLastPoint.X, plot, GetDrawingLayoutCount());
                var deltaY = ScreenToPrice(location.Y, plot, min, max) - ScreenToPrice(draggingLastPoint.Y, plot, min, max);
                drawing.X1 += deltaX; drawing.X2 += deltaX; drawing.X3 += deltaX;
                drawing.Y1 += deltaY; drawing.Y2 += deltaY; drawing.Y3 += deltaY;
                UpdateDrawingDates(drawing);
                return;
            }
            if (drawing.Tool == ChartDrawingTool.Rectangle && handle > 0)
            {
                var x = ScreenToDataX(location.X, plot, visible.Count) + firstIndex;
                var y = ScreenToPrice(location.Y, plot, min, max);
                switch (handle)
                {
                    case 1: drawing.X1 = x; drawing.Y1 = y; break;
                    case 2: drawing.X2 = x; drawing.Y1 = y; break;
                    case 3: drawing.X2 = x; drawing.Y2 = y; break;
                    case 4: drawing.X1 = x; drawing.Y2 = y; break;
                }
                UpdateDrawingDates(drawing);
                return;
            }
            if (handle == 1) { drawing.X1 = ScreenToDataX(location.X, plot, visible.Count) + firstIndex; drawing.Y1 = ScreenToPrice(location.Y, plot, min, max); drawing.Date1 = GetPointDate((int)Math.Round(drawing.X1)); return; }
            if (handle == 2) { drawing.X2 = ScreenToDataX(location.X, plot, visible.Count) + firstIndex; drawing.Y2 = ScreenToPrice(location.Y, plot, min, max); drawing.Date2 = GetPointDate((int)Math.Round(drawing.X2)); return; }
            var moveX = ScreenToDataX(location.X, plot, visible.Count) - ScreenToDataX(draggingLastPoint.X, plot, visible.Count);
            var moveY = ScreenToPrice(location.Y, plot, min, max) - ScreenToPrice(draggingLastPoint.Y, plot, min, max);
            drawing.X1 += moveX; drawing.X2 += moveX;
            drawing.Y1 += moveY; drawing.Y2 += moveY;
            UpdateDrawingDates(drawing);
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

        private static string FormatPrice(double value, int decimalPlaces)
        {
            decimalPlaces = Math.Clamp(decimalPlaces, 0, 10);
            return value.ToString("F" + decimalPlaces, CultureInfo.InvariantCulture);
        }

        private static int GetPriceDecimalPlaces(IEnumerable<TradingChartPoint> data)
        {
            var maxDecimals = 0;

            foreach (var point in data)
            {
                maxDecimals = Math.Max(maxDecimals, CountDecimalPlaces(point.Open));
                maxDecimals = Math.Max(maxDecimals, CountDecimalPlaces(point.High));
                maxDecimals = Math.Max(maxDecimals, CountDecimalPlaces(point.Low));
                maxDecimals = Math.Max(maxDecimals, CountDecimalPlaces(point.Close));
                if (maxDecimals >= 10)
                    return 10;
            }

            return maxDecimals;
        }

        private static int CountDecimalPlaces(double value)
        {
            if (!double.IsFinite(value))
                return 0;

            var text = value.ToString("0.##################", CultureInfo.InvariantCulture);
            var separator = text.IndexOf('.');
            return separator < 0 ? 0 : text.Length - separator - 1;
        }

        private readonly struct LowerPanelLayout
        {
            public Rectangle Price { get; init; }
            public Rectangle Rsi { get; init; }
            public Rectangle Macd { get; init; }
            public Rectangle Stochastic { get; init; }
            public Rectangle StochasticRsi { get; init; }
            public Rectangle Atr { get; init; }
            public Rectangle Adx { get; init; }
            public Rectangle Obv { get; init; }
            public Rectangle Volume { get; init; }
            public int OverallBottom { get; init; }
        }

        private LowerPanelLayout GetLowerPanelLayout()
        {
            var left = 70;
            var right = Math.Max(left + 1, Width - 15);

            var topMargin = Math.Max(0, Height * ChartTopEmptyPercent / 100.0);
            var top = Math.Clamp(
                (int)Math.Round(topMargin),
                0,
                Math.Max(0, Height - 120));

            var overallBottom = Math.Max(top + 1, Height - 55);
            var totalHeight = Math.Max(1, overallBottom - top);
            var gap = Math.Clamp(volumePanelGap, 2, 30);

            var rsiHeight = HasRsiIndicator
                ? Math.Clamp(
                    (int)Math.Round(totalHeight * rsiPanelRatio),
                    60,
                    Math.Max(60, totalHeight / 2))
                : 0;

            var macdHeight = HasMacdIndicator
                ? Math.Clamp(
                    (int)Math.Round(totalHeight * macdPanelRatio),
                    60,
                    Math.Max(60, totalHeight / 2))
                : 0;

            var stochasticHeight = HasStochasticIndicator ? Math.Clamp((int)Math.Round(totalHeight * stochasticPanelRatio), 60, Math.Max(60, totalHeight / 2)) : 0;
            var stochasticRsiHeight = HasStochasticRsiIndicator ? Math.Clamp((int)Math.Round(totalHeight * stochasticRsiPanelRatio), 60, Math.Max(60, totalHeight / 2)) : 0;
            var atrHeight = HasAtrIndicator ? Math.Clamp((int)Math.Round(totalHeight * atrPanelRatio), 60, Math.Max(60, totalHeight / 2)) : 0;
            var adxHeight = HasAdxIndicator ? Math.Clamp((int)Math.Round(totalHeight * adxPanelRatio), 60, Math.Max(60, totalHeight / 2)) : 0;
            var obvHeight = HasObvIndicator ? Math.Clamp((int)Math.Round(totalHeight * obvPanelRatio), 60, Math.Max(60, totalHeight / 2)) : 0;

            var volumeHeight = volumePanelVisible
                ? Math.Clamp(
                    (int)Math.Round(totalHeight * volumePanelRatio),
                    45,
                    Math.Max(45, totalHeight / 2))
                : 0;

            var reserved = (rsiHeight > 0 ? rsiHeight + gap : 0) +
                           (macdHeight > 0 ? macdHeight + gap : 0) +
                           (stochasticHeight > 0 ? stochasticHeight + gap : 0) +
                           (stochasticRsiHeight > 0 ? stochasticRsiHeight + gap : 0) +
                           (atrHeight > 0 ? atrHeight + gap : 0) +
                           (adxHeight > 0 ? adxHeight + gap : 0) +
                           (obvHeight > 0 ? obvHeight + gap : 0) +
                           (volumeHeight > 0 ? volumeHeight + gap : 0);

            var priceBottom = Math.Max(
                top + 80,
                overallBottom - reserved);

            // در صورت کوچک شدن شدید کنترل، اولویت با حداقل فضای نمودار قیمت است.
            // پنل‌های پایین تا حد ممکن کوچک می‌شوند اما هیچ‌گاه روی هم نمی‌افتند.
            var availableLower = Math.Max(0, overallBottom - priceBottom);
            var lowerRequested = (rsiHeight > 0 ? rsiHeight + gap : 0) +
                                 (macdHeight > 0 ? macdHeight + gap : 0) +
                                 (stochasticHeight > 0 ? stochasticHeight + gap : 0) +
                                 (stochasticRsiHeight > 0 ? stochasticRsiHeight + gap : 0) +
                           (atrHeight > 0 ? atrHeight + gap : 0) +
                           (adxHeight > 0 ? adxHeight + gap : 0) +
                                 (obvHeight > 0 ? obvHeight + gap : 0) +
                                 (volumeHeight > 0 ? volumeHeight + gap : 0);

            if (lowerRequested > availableLower && lowerRequested > 0)
            {
                var scale = availableLower / (double)lowerRequested;
                rsiHeight = (int)Math.Floor(rsiHeight * scale);
                macdHeight = (int)Math.Floor(macdHeight * scale);
                stochasticHeight = (int)Math.Floor(stochasticHeight * scale);
                stochasticRsiHeight = (int)Math.Floor(stochasticRsiHeight * scale);
                 atrHeight = (int)Math.Floor(atrHeight * scale);
                adxHeight = (int)Math.Floor(adxHeight * scale);
                obvHeight = (int)Math.Floor(obvHeight * scale);
                volumeHeight = (int)Math.Floor(volumeHeight * scale);
            }

            var price = Rectangle.FromLTRB(left, top, right, Math.Max(top + 1, priceBottom));
            var cursor = price.Bottom;

            Rectangle rsi = Rectangle.Empty;
            if (rsiHeight > 0)
            {
                var rsiTop = Math.Min(overallBottom - 1, cursor + gap);
                var rsiBottom = Math.Min(overallBottom - 1, rsiTop + rsiHeight);
                rsi = Rectangle.FromLTRB(left, rsiTop, right, Math.Max(rsiTop + 1, rsiBottom));
                cursor = rsi.Bottom;
            }

            Rectangle macd = Rectangle.Empty;
            if (macdHeight > 0)
            {
                var macdTop = Math.Min(overallBottom - 1, cursor + gap);
                var macdBottom = Math.Min(overallBottom - 1, macdTop + macdHeight);
                macd = Rectangle.FromLTRB(left, macdTop, right, Math.Max(macdTop + 1, macdBottom));
                cursor = macd.Bottom;
            }

            Rectangle stochastic = Rectangle.Empty;
            if (stochasticHeight > 0)
            {
                var stochasticTop = Math.Min(overallBottom - 1, cursor + gap);
                var stochasticBottom = Math.Min(overallBottom - 1, stochasticTop + stochasticHeight);
                stochastic = Rectangle.FromLTRB(left, stochasticTop, right, Math.Max(stochasticTop + 1, stochasticBottom));
                cursor = stochastic.Bottom;
            }

            Rectangle stochasticRsi = Rectangle.Empty;
            if (stochasticRsiHeight > 0)
            {
                var stochasticRsiTop = Math.Min(overallBottom - 1, cursor + gap);
                var stochasticRsiBottom = Math.Min(overallBottom - 1, stochasticRsiTop + stochasticRsiHeight);
                stochasticRsi = Rectangle.FromLTRB(left, stochasticRsiTop, right, Math.Max(stochasticRsiTop + 1, stochasticRsiBottom));
                cursor = stochasticRsi.Bottom;
            }

            Rectangle atr = Rectangle.Empty;
            if (atrHeight > 0)
            {
                var atrTop = Math.Min(overallBottom - 1, cursor + gap);
                var atrBottom = Math.Min(overallBottom - 1, atrTop + atrHeight);
                atr = Rectangle.FromLTRB(left, atrTop, right, Math.Max(atrTop + 1, atrBottom));
                cursor = atr.Bottom;
            }

            Rectangle adx = Rectangle.Empty;
            if (adxHeight > 0)
            {
                var adxTop = Math.Min(overallBottom - 1, cursor + gap);
                var adxBottom = Math.Min(overallBottom - 1, adxTop + adxHeight);
                adx = Rectangle.FromLTRB(left, adxTop, right, Math.Max(adxTop + 1, adxBottom));
                cursor = adx.Bottom;
            }

            Rectangle obv = Rectangle.Empty;
            if (obvHeight > 0)
            {
                var obvTop = Math.Min(overallBottom - 1, cursor + gap);
                var obvBottom = Math.Min(overallBottom - 1, obvTop + obvHeight);
                obv = Rectangle.FromLTRB(left, obvTop, right, Math.Max(obvTop + 1, obvBottom));
                cursor = obv.Bottom;
            }

            Rectangle volume = Rectangle.Empty;
            if (volumeHeight > 0)
            {
                var volumeTop = Math.Min(overallBottom - 1, cursor + gap);
                volume = Rectangle.FromLTRB(left, volumeTop, right, overallBottom);
            }

            return new LowerPanelLayout
            {
                Price = price,
                Rsi = rsi,
                Macd = macd,
                Stochastic = stochastic,
                StochasticRsi = stochasticRsi,
                Atr = atr,
                Adx = adx,
                Obv = obv,
                Volume = volume,
                OverallBottom = overallBottom
            };
        }

        private Rectangle GetPlotRectangle() => GetLowerPanelLayout().Price;

        private Rectangle GetRsiPlotRectangle() => GetLowerPanelLayout().Rsi;

        private Rectangle GetMacdPlotRectangle() => GetLowerPanelLayout().Macd;

        private Rectangle GetStochasticPlotRectangle() => GetLowerPanelLayout().Stochastic;

        private Rectangle GetStochasticRsiPlotRectangle() => GetLowerPanelLayout().StochasticRsi;

        private Rectangle GetAtrPlotRectangle() => GetLowerPanelLayout().Atr;
        private Rectangle GetAdxPlotRectangle() => GetLowerPanelLayout().Adx;
        private Rectangle GetObvPlotRectangle() => GetLowerPanelLayout().Obv;

        private Rectangle GetVolumePlotRectangle() => GetLowerPanelLayout().Volume;

        private static void RenderClippedPanel(Graphics g, Rectangle plot, Action<Graphics> renderer)
        {
            var state = g.Save();
            g.SetClip(plot);
            try { renderer(g); }
            finally { g.Restore(state); }
        }

        private void RenderRsiPanel(
            Graphics g,
            Rectangle rsiPlot,
            List<TradingChartPoint> visible,
            double step,
            double initialOffset)
        {
            if (visible.Count == 0 || rsiPlot.Width <= 0 || rsiPlot.Height <= 0)
                return;

            using var separatorPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var textBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);

            g.DrawLine(separatorPen, rsiPlot.Left, rsiPlot.Top, rsiPlot.Right, rsiPlot.Top);
            g.DrawLine(axisPen, rsiPlot.Left, rsiPlot.Bottom, rsiPlot.Right, rsiPlot.Bottom);
            g.DrawLine(axisPen, rsiPlot.Left, rsiPlot.Top, rsiPlot.Left, rsiPlot.Bottom);

            var indicatorsToDraw = indicators
                .Where(x => x.Type == ChartIndicatorType.RelativeStrengthIndex)
                .ToList();

            foreach (var indicator in indicatorsToDraw)
            {
                if (indicator.ShowRsi30)
                {
                    using var pen30 = new Pen(indicator.Rsi30Color, Math.Max(1f, LineAppearanceSettings.ChartLineWidth));
                    var y30 = (float)RsiValueToScreenY(30, rsiPlot);
                    g.DrawLine(pen30, rsiPlot.Left, y30, rsiPlot.Right, y30);
                    g.DrawString("30", labelFont, textBrush, rsiPlot.Left + 4f, y30 - labelFont.GetHeight(g));
                }

                if (indicator.ShowRsi70)
                {
                    using var pen70 = new Pen(indicator.Rsi70Color, Math.Max(1f, LineAppearanceSettings.ChartLineWidth));
                    var y70 = (float)RsiValueToScreenY(70, rsiPlot);
                    g.DrawLine(pen70, rsiPlot.Left, y70, rsiPlot.Right, y70);
                    g.DrawString("70", labelFont, textBrush, rsiPlot.Left + 4f, y70 - labelFont.GetHeight(g));
                }

                if (!indicator.ShowRsiLine)
                    continue;

                var values = CalculateRsi(indicator.Period);
                using var linePen = new Pen(indicator.RsiLineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
                PointF? previous = null;

                for (var i = 0; i < visible.Count; i++)
                {
                    var absoluteIndex = firstIndex + i;
                    if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                    {
                        previous = null;
                        continue;
                    }

                    var current = RsiValueToScreen(values[absoluteIndex], rsiPlot, step, i, initialOffset);
                    if (previous.HasValue)
                        g.DrawLine(linePen, previous.Value, current);
                    previous = current;
                }

                if (previous.HasValue)
                {
                    // نام RSI فقط در عنوان بالای سمت راست پنل نمایش داده می‌شود.
                }
            }

            var crossIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count
                ? crosshairIndex
                : visible.Count - 1;

            foreach (var indicator in indicatorsToDraw)
            {
                var values = CalculateRsi(indicator.Period);
                if (crossIndex < 0)
                    continue;

                var absoluteIndex = firstIndex + crossIndex;
                if (absoluteIndex >= 0 && absoluteIndex < values.Length && !double.IsNaN(values[absoluteIndex]))
                {
                    DrawIndicatorPanelTitle(g, rsiPlot, $"RSI({indicator.Period})", titleFont, indicator.RsiLineColor);
                }
            }
        }

        private void RenderMacdPanel(
            Graphics g,
            Rectangle macdPlot,
            List<TradingChartPoint> visible,
            double step,
            double initialOffset)
        {
            if (visible.Count == 0 || macdPlot.Width <= 0 || macdPlot.Height <= 0 || !HasMacdIndicator)
                return;

            var macdIndicator = indicators.First(x => x.Type == ChartIndicatorType.MovingAverageConvergenceDivergence);
            EnsureMacdCache(macdIndicator);
            var maxAbs = GetMacdScaleMax(macdLineCache!, macdSignalCache!, macdHistogramCache!, firstIndex, visible.Count);

            using var separatorPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var textBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);

            g.DrawLine(separatorPen, macdPlot.Left, macdPlot.Top, macdPlot.Right, macdPlot.Top);
            g.DrawLine(axisPen, macdPlot.Left, macdPlot.Bottom, macdPlot.Right, macdPlot.Bottom);
            g.DrawLine(axisPen, macdPlot.Left, macdPlot.Top, macdPlot.Left, macdPlot.Bottom);

            var indicatorsToDraw = indicators
                .Where(x => x.Type == ChartIndicatorType.MovingAverageConvergenceDivergence)
                .ToList();

            foreach (var indicator in indicatorsToDraw)
            {
                if (indicator.ShowMacdZero)
                {
                    using var zeroPen = new Pen(indicator.MacdZeroColor, Math.Max(1f, LineAppearanceSettings.ChartLineWidth));
                    var zeroY = (float)MacdValueToScreen(0, macdPlot, maxAbs);
                    g.DrawLine(zeroPen, macdPlot.Left, zeroY, macdPlot.Right, zeroY);
                    g.DrawString("0", titleFont, textBrush, macdPlot.Left + 4f, zeroY - titleFont.GetHeight(g));
                }

                if (indicator.ShowMacdHistogram)
                {
                    var zeroY = (float)MacdValueToScreen(0, macdPlot, maxAbs);
                    var barWidth = Math.Max(2f, (float)(step * 0.65));
                    for (var i = 0; i < visible.Count; i++)
                    {
                        var absoluteIndex = firstIndex + i;
                        if (absoluteIndex < 0 || absoluteIndex >= macdHistogramCache!.Length ||
                            double.IsNaN(macdHistogramCache[absoluteIndex]))
                            continue;

                        var value = macdHistogramCache[absoluteIndex];
                        var y = (float)MacdValueToScreen(value, macdPlot, maxAbs);
                        var top = Math.Min(zeroY, y);
                        var bottom = Math.Max(zeroY, y);
                        var x = (float)(macdPlot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset);
                        var rect = RectangleF.FromLTRB(
                            x - barWidth / 2f,
                            top,
                            x + barWidth / 2f,
                            Math.Max(top + 1f, bottom));

                        var histogramColor = value >= 0
                            ? indicator.MacdBullishHistogramColor
                            : indicator.MacdBearishHistogramColor;
                        using var brush = new SolidBrush(histogramColor);
                        using var pen = new Pen(histogramColor, 1f);
                        g.FillRectangle(brush, rect);
                        g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }

                DrawMacdLine(g, macdPlot, macdLineCache!, visible.Count, step, initialOffset,
                    maxAbs, indicator.ShowMacdLine, indicator.MacdLineColor);
                DrawMacdLine(g, macdPlot, macdSignalCache!, visible.Count, step, initialOffset,
                    maxAbs, indicator.ShowMacdSignal, indicator.MacdSignalColor);

                var crossIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count
                    ? crosshairIndex
                    : visible.Count - 1;
                if (crossIndex >= 0)
                {
                    var absoluteIndex = firstIndex + crossIndex;
                    if (absoluteIndex >= 0 && absoluteIndex < macdHistogramCache!.Length &&
                        !double.IsNaN(macdHistogramCache[absoluteIndex]))
                    {
                        DrawIndicatorPanelTitle(g, macdPlot, $"MACD({indicator.MacdFastPeriod},{indicator.MacdSlowPeriod},{indicator.MacdSignalPeriod})", titleFont, indicator.MacdLineColor);
                    }
                }
            }
        }

        private void DrawMacdLine(
            Graphics g,
            Rectangle plot,
            double[] values,
            int displayedCount,
            double step,
            double initialOffset,
            double maxAbs,
            bool show,
            Color color)
        {
            if (!show)
                return;

            using var pen = new Pen(color, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= values.Length || double.IsNaN(values[absoluteIndex]))
                {
                    previous = null;
                    continue;
                }

                var current = new PointF(
                    (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                    (float)MacdValueToScreen(values[absoluteIndex], plot, maxAbs));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, current);

                previous = current;
            }
        }

        private void RenderAtrPanel(Graphics g, Rectangle plot, List<TradingChartPoint> visible, double step, double initialOffset)
        {
            if (visible.Count == 0 || plot.Width <= 0 || plot.Height <= 0 || !HasAtrIndicator)
                return;

            var indicator = indicators.First(x => x.Type == ChartIndicatorType.AverageTrueRange);
            EnsureAtrCache(indicator);
            var maxValue = GetAtrScaleMax(atrCache!, firstIndex, visible.Count);

            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Right, plot.Top);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            using var linePen = new Pen(indicator.LineColor, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;
            for (var i = 0; i < visible.Count; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= atrCache!.Length || double.IsNaN(atrCache[absoluteIndex]))
                {
                    previous = null;
                    continue;
                }

                var current = new PointF(
                    (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                    (float)AtrValueToScreen(atrCache[absoluteIndex], plot, maxValue));

                if (previous.HasValue)
                    g.DrawLine(linePen, previous.Value, current);
                previous = current;
            }

            var crossIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count ? crosshairIndex : visible.Count - 1;
            var crosshairAbsoluteIndex = firstIndex + crossIndex;
            var atrValueText = crosshairAbsoluteIndex >= 0 && crosshairAbsoluteIndex < atrCache!.Length && !double.IsNaN(atrCache[crosshairAbsoluteIndex]) && !double.IsInfinity(atrCache[crosshairAbsoluteIndex])
                ? atrCache[crosshairAbsoluteIndex].ToString("0.########", CultureInfo.InvariantCulture)
                : "—";
            DrawIndicatorPanelTitle(g, plot, $"ATR({indicator.Period})={atrValueText}", titleFont, indicator.LineColor);
        }

        private static double GetAtrScaleMax(double[] values, int firstIndex, int displayedCount)
        {
            var max = 0.0;
            var end = Math.Min(values.Length, firstIndex + displayedCount);
            for (var i = Math.Max(0, firstIndex); i < end; i++)
                if (!double.IsNaN(values[i]) && !double.IsInfinity(values[i]))
                    max = Math.Max(max, values[i]);
            return Math.Max(max * 1.15, 1e-9);
        }

        private static double AtrValueToScreen(double value, Rectangle plot, double maxValue)
        {
            if (maxValue <= 1e-12)
                return plot.Bottom - plot.Height / 2.0;
            return plot.Bottom - (value / maxValue) * plot.Height;
        }

        private void RenderStochasticPanel(Graphics g, Rectangle plot, List<TradingChartPoint> visible, double step, double initialOffset)
        {
            if (visible.Count == 0 || plot.Width <= 0 || plot.Height <= 0 || !HasStochasticIndicator) return;
            var indicator = indicators.First(x => x.Type == ChartIndicatorType.Stochastic);
            EnsureStochasticCache(indicator);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var font = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);
            using var labelBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Right, plot.Top);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);
            if (indicator.ShowStochastic20) { using var p = new Pen(indicator.Stochastic20Color); var y=(float)IndicatorPercentToScreen(20, plot); g.DrawLine(p,plot.Left,y,plot.Right,y); g.DrawString("20", labelFont, labelBrush, plot.Left + 4f, y - labelFont.GetHeight(g)); }
            if (indicator.ShowStochastic80) { using var p = new Pen(indicator.Stochastic80Color); var y=(float)IndicatorPercentToScreen(80, plot); g.DrawLine(p,plot.Left,y,plot.Right,y); g.DrawString("80", labelFont, labelBrush, plot.Left + 4f, y - labelFont.GetHeight(g)); }
            DrawStochasticLine(g,plot,visible.Count,step,initialOffset,stochasticKCache!,indicator.ShowStochasticK,indicator.StochasticKColor);
            DrawStochasticLine(g,plot,visible.Count,step,initialOffset,stochasticDCache!,indicator.ShowStochasticD,indicator.StochasticDColor);
            DrawIndicatorPanelTitle(g, plot, $"Stochastic({indicator.StochasticPeriod},{indicator.StochasticKPeriod},{indicator.StochasticDPeriod})", font, indicator.StochasticKColor);
        }

        private void DrawStochasticLine(Graphics g, Rectangle plot, int count, double step, double initialOffset, double[] values, bool show, Color color)
        {
            if(!show) return; using var pen=new Pen(color,Math.Max(1.2f,LineAppearanceSettings.ChartLineWidth)); PointF? previous=null;
            for(var i=0;i<count;i++){var ai=firstIndex+i;if(ai<0||ai>=values.Length||double.IsNaN(values[ai])){previous=null;continue;} var current=new PointF((float)(plot.Left+step*(i+.5)+initialOffset+horizontalPanOffset),(float)IndicatorPercentToScreen(values[ai], plot));if(previous.HasValue)g.DrawLine(pen,previous.Value,current);previous=current;}
        }

        private static double IndicatorPercentToScreen(double value, Rectangle plot)
        {
            const double padding = 0.05;
            var innerHeight = plot.Height * (1.0 - 2.0 * padding);
            var clamped = Math.Clamp(value, 0.0, 100.0);
            return plot.Bottom - plot.Height * padding - (clamped / 100.0 * innerHeight);
        }

        private void RenderAdxPanel(Graphics g, Rectangle plot, List<TradingChartPoint> visible, double step, double initialOffset)
        {
            if (visible.Count == 0 || plot.Width <= 0 || plot.Height <= 0 || !HasAdxIndicator)
                return;

            var indicator = indicators.First(x => x.Type == ChartIndicatorType.AverageDirectionalIndex);
            EnsureAdxCache(indicator);

            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);
            using var labelBrush = new SolidBrush(Color.FromArgb(85, 85, 85));

            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Right, plot.Top);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            if (indicator.ShowAdx25)
            {
                using var p = new Pen(indicator.Adx25Color);
                var y = (float)AdxValueToScreen(25, plot);
                g.DrawLine(p, plot.Left, y, plot.Right, y);
                g.DrawString("25", labelFont, labelBrush, plot.Left + 4f, y - labelFont.GetHeight(g));
            }

            DrawAdxLine(g, plot, visible.Count, step, initialOffset, adxCache!, indicator.ShowAdxLine, indicator.AdxLineColor);
            DrawAdxLine(g, plot, visible.Count, step, initialOffset, adxPlusDiCache!, indicator.ShowAdxPlusDi, indicator.AdxPlusDiColor);
            DrawAdxLine(g, plot, visible.Count, step, initialOffset, adxMinusDiCache!, indicator.ShowAdxMinusDi, indicator.AdxMinusDiColor);

            var crossIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count ? crosshairIndex : visible.Count - 1;
            var absoluteIndex = firstIndex + crossIndex;
            var valueText = absoluteIndex >= 0 && absoluteIndex < adxCache!.Length &&
                            !double.IsNaN(adxCache[absoluteIndex]) && !double.IsInfinity(adxCache[absoluteIndex])
                ? adxCache[absoluteIndex].ToString("0.##", CultureInfo.InvariantCulture)
                : "—";

            DrawIndicatorPanelTitle(g, plot, $"ADX({indicator.Period})={valueText}", titleFont, indicator.AdxLineColor);
        }

        private void DrawAdxLine(Graphics g, Rectangle plot, int displayedCount, double step, double initialOffset, double[] values, bool show, Color color)
        {
            if (!show) return;

            using var pen = new Pen(color, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;

            for (var i = 0; i < displayedCount; i++)
            {
                var absoluteIndex = firstIndex + i;
                if (absoluteIndex < 0 || absoluteIndex >= values.Length ||
                    double.IsNaN(values[absoluteIndex]) || double.IsInfinity(values[absoluteIndex]))
                {
                    previous = null;
                    continue;
                }

                var current = new PointF(
                    (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                    (float)AdxValueToScreen(values[absoluteIndex], plot));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, current);

                previous = current;
            }
        }

        private static double AdxValueToScreen(double value, Rectangle plot)
        {
            const double maxValue = 100.0;
            value = Math.Clamp(value, 0, maxValue);
            return plot.Bottom - (value / maxValue) * plot.Height;
        }

        private void RenderStochasticRsiPanel(Graphics g, Rectangle plot, List<TradingChartPoint> visible, double step, double initialOffset)
        {
            if (visible.Count == 0 || plot.Width <= 0 || plot.Height <= 0 || !HasStochasticRsiIndicator)
                return;

            var indicator = indicators.First(x => x.Type == ChartIndicatorType.StochasticRelativeStrengthIndex);
            EnsureStochasticRsiCache(indicator);
            using var axisPen = new Pen(Color.FromArgb(150, 150, 150), 1f);
            using var font = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);
            using var labelBrush = new SolidBrush(Color.FromArgb(85, 85, 85));

            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Right, plot.Top);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

            if (indicator.ShowStochasticRsi20)
            {
                using var p = new Pen(indicator.StochasticRsi20Color);
                var y = (float)IndicatorPercentToScreen(20, plot);
                g.DrawLine(p, plot.Left, y, plot.Right, y);
                g.DrawString("20", labelFont, labelBrush, plot.Left + 4f, y - labelFont.GetHeight(g));
            }

            if (indicator.ShowStochasticRsi80)
            {
                using var p = new Pen(indicator.StochasticRsi80Color);
                var y = (float)IndicatorPercentToScreen(80, plot);
                g.DrawLine(p, plot.Left, y, plot.Right, y);
                g.DrawString("80", labelFont, labelBrush, plot.Left + 4f, y - labelFont.GetHeight(g));
            }

            DrawStochasticRsiLine(g, plot, visible.Count, step, initialOffset, stochasticRsiKCache!, indicator.ShowStochasticRsiK, indicator.StochasticRsiKColor);
            DrawStochasticRsiLine(g, plot, visible.Count, step, initialOffset, stochasticRsiDCache!, indicator.ShowStochasticRsiD, indicator.StochasticRsiDColor);

            DrawIndicatorPanelTitle(g, plot, $"Stochastic RSI({indicator.StochasticRsiRsiPeriod},{indicator.StochasticRsiPeriod},{indicator.StochasticRsiKPeriod},{indicator.StochasticRsiDPeriod})", font, indicator.StochasticRsiKColor);
        }

        private void DrawStochasticRsiLine(Graphics g, Rectangle plot, int count, double step, double initialOffset, double[] values, bool show, Color color)
        {
            if (!show)
                return;

            using var pen = new Pen(color, Math.Max(1.2f, LineAppearanceSettings.ChartLineWidth));
            PointF? previous = null;

            for (var i = 0; i < count; i++)
            {
                var ai = firstIndex + i;
                if (ai < 0 || ai >= values.Length || double.IsNaN(values[ai]))
                {
                    previous = null;
                    continue;
                }

                var current = new PointF(
                    (float)(plot.Left + step * (i + 0.5) + initialOffset + horizontalPanOffset),
                    (float)IndicatorPercentToScreen(values[ai], plot));

                if (previous.HasValue)
                    g.DrawLine(pen, previous.Value, current);
                previous = current;
            }
        }

        private void RenderPriceIndicatorTitles(Graphics g, Rectangle plot, double min, double max, int displayedCount, double step, double initialOffset)
        {
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            var xRight = (float)plot.Right;
            var y = plot.Top + 1f;
            foreach (var indicator in indicators)
            {
                string? title = null;
                Color color = indicator.LineColor;
                if (indicator.Type == ChartIndicatorType.MovingAverage)
                    title = $"MA({indicator.Period})";
                else if (indicator.Type == ChartIndicatorType.ExponentialMovingAverage)
                    title = $"EMA({indicator.Period})";
                else if (indicator.Type == ChartIndicatorType.Ichimoku)
                    title = $"Ichimoku({indicator.IchimokuTenkanPeriod},{indicator.IchimokuKijunPeriod},{indicator.IchimokuSpanBPeriod},{indicator.IchimokuDisplacement})";
                if (title == null) continue;
                var size = g.MeasureString(title, titleFont);
                xRight -= size.Width + 4f;
                using var brush = new SolidBrush(color);
                g.DrawString(title, titleFont, brush, xRight, y);
            }
        }

        private void DrawIndicatorPanelTitle(Graphics g, Rectangle plot, string title, Font font, Color color)
        {
            using var boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
            using var brush = new SolidBrush(color);
            var size = g.MeasureString(title, boldFont);
            var x = plot.Right - size.Width - 4f;
            var y = plot.Top + 1f;
            g.DrawString(title, boldFont, brush, x, y);
        }

        private void RenderObvPanel(Graphics g, Rectangle plot, List<TradingChartPoint> visible, double step, double initialOffset)
        {
            if (visible.Count == 0 || plot.Width <= 0 || plot.Height <= 0 || !HasObvIndicator) return;
            EnsureObvCache();
            using var axisPen = new Pen(Color.FromArgb(150,150,150),1f);
            using var textBrush = new SolidBrush(Color.FromArgb(85,85,85));
            using var titleFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Bold);
            g.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            g.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);
            var start = Math.Max(0, firstIndex);
            var end = Math.Min(obvCache!.Length, firstIndex + visible.Count);
            var min = double.MaxValue; var max = double.MinValue;
            for (var i=start;i<end;i++) if(double.IsFinite(obvCache[i])) { min=Math.Min(min,obvCache[i]); max=Math.Max(max,obvCache[i]); }
            if (min == double.MaxValue) return;
            var range=Math.Max(1e-12,max-min); var pad=range*0.08; min-=pad; max+=pad;
            double Y(double v)=>plot.Bottom-((v-min)/(max-min))*plot.Height;
            foreach(var indicator in indicators.Where(x=>x.Type==ChartIndicatorType.OnBalanceVolume))
            {
                if(indicator.ShowObvZero && min<=0 && max>=0){using var zp=new Pen(indicator.ObvZeroColor,1f);var zy=(float)Y(0);g.DrawLine(zp,plot.Left,zy,plot.Right,zy);}
                if(!indicator.ShowObvLine) continue;
                using var pen=new Pen(indicator.ObvLineColor,Math.Max(1.2f,LineAppearanceSettings.ChartLineWidth));
                PointF? prev=null;
                for(var i=0;i<visible.Count;i++){var ai=firstIndex+i;if(ai<0||ai>=obvCache.Length||!double.IsFinite(obvCache[ai])){prev=null;continue;}var pt=new PointF((float)(plot.Left+step*(i+.5)+initialOffset+horizontalPanOffset),(float)Y(obvCache[ai]));if(prev.HasValue)g.DrawLine(pen,prev.Value,pt);prev=pt;}
                var obvCrossIndex = crosshairIndex >= 0 && crosshairIndex < visible.Count ? crosshairIndex : visible.Count - 1;
                var obvAbsoluteIndex = firstIndex + obvCrossIndex;
                var obvValueText = obvAbsoluteIndex >= 0 && obvAbsoluteIndex < obvCache.Length && double.IsFinite(obvCache[obvAbsoluteIndex])
                    ? obvCache[obvAbsoluteIndex].ToString("0.##", CultureInfo.InvariantCulture)
                    : "—";
                DrawIndicatorPanelTitle(g, plot, "OBV=" + obvValueText, titleFont, indicator.ObvLineColor);
            }
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
            using var textBrush = new SolidBrush(Color.FromArgb(85, 85, 85));            using var labelFont = new Font(Font.FontFamily, Math.Max(7f, Font.Size - 2f), FontStyle.Regular);

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

            var minText = minVolume.ToString("N0");
            g.DrawString(minText, labelFont, textBrush, volumePlot.Left + 4f, volumePlot.Bottom - labelFont.GetHeight(g) - 1f);
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

        private double DateToDataX(DateTime? date, double fallbackAbsoluteIndex)
        {
            if (!date.HasValue || points.Count == 0)
                return fallbackAbsoluteIndex;

            var target = date.Value;
            if (target <= points[0].Date)
                return 0;

            if (target >= points[^1].Date)
                return points.Count - 1;

            var lo = 0;
            var hi = points.Count - 1;
            while (lo + 1 < hi)
            {
                var mid = lo + (hi - lo) / 2;
                if (points[mid].Date <= target)
                    lo = mid;
                else
                    hi = mid;
            }

            var leftDate = points[lo].Date;
            var rightDate = points[hi].Date;
            var span = (rightDate - leftDate).TotalSeconds;
            if (span <= 0)
                return lo;

            var fraction = (target - leftDate).TotalSeconds / span;
            return lo + Math.Clamp(fraction, 0.0, 1.0);
        }

        private PointF DateToScreen(
            DateTime? date,
            double fallbackAbsoluteIndex,
            double y,
            Rectangle plot,
            int visibleCountForDrawing,
            double min,
            double max)
        {
            var absoluteX = DateToDataX(date, fallbackAbsoluteIndex);
            return DataToScreen(
                absoluteX - firstIndex,
                y,
                plot,
                visibleCountForDrawing,
                min,
                max);
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