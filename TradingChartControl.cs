using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal enum TradingChartType
    {
        Candlestick,
        Line,
        Bar
    }

    internal enum ChartDrawingTool
    {
        None,
        TrendLine,
        HorizontalDoubleArrow,
        VerticalDoubleArrow,
        HorizontalRay,
        TrendLineWithArrow,
        Rectangle
    }

    internal sealed class TradingChartPoint
    {
        public DateTime Date { get; init; }
        public double Open { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Close { get; init; }
        public double Volume { get; init; }
    }

    internal sealed partial class TradingChartControl : Control
    {
        private readonly List<TradingChartPoint> points = new();
        private readonly List<ChartDrawing> drawings = new();
        private TradingChartType chartType = TradingChartType.Candlestick;
        private int visibleCount;
        private int firstIndex;
        private int selectedDrawingIndex = -1;
        private bool panning;
        private Point panStartPoint;
        private int panStartFirstIndex;
        private double panStartVerticalPanOffset;
        private double panStartVerticalRange;
        private double panStartHorizontalOffset;
        private bool horizontalAxisDrag;
        private Point horizontalAxisStartPoint;
        private int horizontalAxisStartVisibleCount;
        private double horizontalAxisCenterIndex;
        private bool verticalAxisDrag;
        private Point verticalAxisStartPoint;
        private double verticalAxisStartZoom;
        private bool showGrid;
        private bool showCrosshair = true;
        private Point crosshairPoint;
        private int crosshairIndex = -1;
        private double verticalZoom = 1.0;
        private double verticalPanOffset;
        private double horizontalPanOffset;
        private ChartDrawingTool activeDrawingTool;
        private bool drawingInProgress;
        private Point drawingStartPoint;
        private Point drawingCurrentPoint;
        private int draggingDrawingIndex = -1;
        private int draggingHandle = 0;
        private Point draggingLastPoint;

        private sealed class ChartDrawing
        {
            public ChartDrawingTool Tool { get; init; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
        }

        public TradingChartControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ForeColor = Color.Black;
            ResizeRedraw = true;
            MinimumSize = new Size(200, 150);
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        public void SetData(IEnumerable<TradingChartPoint> data)
        {
            points.Clear(); points.AddRange(data.OrderBy(x => x.Date));
            visibleCount = Math.Min(200, Math.Max(1, points.Count)); firstIndex = Math.Max(0, points.Count - visibleCount);
            verticalZoom = 1.0; verticalPanOffset = 0; horizontalPanOffset = 0; showCrosshair = true; crosshairIndex = -1;
            CancelDrawing(); drawings.Clear(); selectedDrawingIndex = -1; draggingDrawingIndex = -1; draggingHandle = 0; Invalidate();
        }
        public void SetChartType(TradingChartType type) { chartType = type; Invalidate(); }
        public void ToggleGrid() { showGrid = !showGrid; Invalidate(); }
        public bool GridVisible => showGrid;
        public void ToggleCrosshair() { showCrosshair = !showCrosshair; Invalidate(); }
        public bool CrosshairVisible => showCrosshair;
        public ChartDrawingTool ActiveDrawingTool => activeDrawingTool;
        public bool DrawingInProgress => drawingInProgress;
        public void SetDrawingTool(ChartDrawingTool tool) { activeDrawingTool = tool; drawingInProgress = false; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty; Focus(); Cursor = tool == ChartDrawingTool.None ? Cursors.Default : Cursors.Cross; Invalidate(); }
        public void CancelDrawing() { activeDrawingTool = ChartDrawingTool.None; drawingInProgress = false; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty; Cursor = Cursors.Default; Invalidate(); }
        public void ResetView() { visibleCount = Math.Min(200, Math.Max(1, points.Count)); firstIndex = Math.Max(0, points.Count - visibleCount); verticalZoom = 1; verticalPanOffset = 0; horizontalPanOffset = 0; crosshairIndex = -1; Invalidate(); }
        public void ZoomX(double factor) { if (points.Count < 2) return; var oldCount = Math.Max(2, visibleCount); var newCount = Math.Clamp((int)Math.Round(oldCount * factor), 2, points.Count); if (newCount == oldCount) return; visibleCount = newCount; firstIndex = Math.Max(0, points.Count - newCount); crosshairIndex = -1; Invalidate(); }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedDrawingIndex >= 0 && selectedDrawingIndex < drawings.Count) { drawings.RemoveAt(selectedDrawingIndex); selectedDrawingIndex = -1; draggingDrawingIndex = -1; draggingHandle = 0; Invalidate(); e.Handled = true; e.SuppressKeyPress = true; return; }
            if (e.KeyCode == Keys.Escape && activeDrawingTool != ChartDrawingTool.None) { CancelDrawing(); e.Handled = true; e.SuppressKeyPress = true; return; }
            base.OnKeyDown(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e) { base.OnMouseWheel(e); ZoomX(e.Delta > 0 ? 0.80 : 1.25); }
        protected override void OnMouseDoubleClick(MouseEventArgs e) { base.OnMouseDoubleClick(e); if (e.Button == MouseButtons.Left && e.X <= 55 && e.Y <= Height - 35) FitVerticalRange(); }
        private void FitVerticalRange() { if (points.Count == 0) return; verticalZoom = 1; verticalPanOffset = 0; Invalidate(); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right) { if (activeDrawingTool != ChartDrawingTool.None || drawingInProgress) CancelDrawing(); return; }
            if (e.Button != MouseButtons.Left) return;
            if (activeDrawingTool != ChartDrawingTool.None) { BeginOrCompleteDrawing(e.Location); return; }
            var plot = GetPlotRectangle(); var handleIndex = HitTestDrawingHandle(e.Location, plot, out var handle);
            if (handleIndex >= 0) { selectedDrawingIndex = handleIndex; draggingDrawingIndex = handleIndex; draggingHandle = handle; draggingLastPoint = e.Location; Focus(); Capture = true; Cursor = Cursors.SizeAll; Invalidate(); return; }
            var hitIndex = HitTestDrawing(e.Location, plot);
            if (hitIndex >= 0) { selectedDrawingIndex = hitIndex; draggingDrawingIndex = hitIndex; draggingHandle = 0; draggingLastPoint = e.Location; Focus(); Capture = true; Cursor = Cursors.SizeAll; Invalidate(); return; }
            selectedDrawingIndex = -1; draggingDrawingIndex = -1; draggingHandle = 0;
            var plotLeft = 55; var plotBottom = Height - 35; horizontalAxisDrag = e.Y >= plotBottom && e.X >= plotLeft; verticalAxisDrag = e.X <= plotLeft && e.Y <= plotBottom;
            if (horizontalAxisDrag) { panning = false; horizontalAxisStartPoint = e.Location; horizontalAxisStartVisibleCount = Math.Max(2, visibleCount); horizontalAxisCenterIndex = firstIndex + horizontalAxisStartVisibleCount / 2.0; Capture = true; Cursor = Cursors.SizeWE; return; }
            if (verticalAxisDrag) { panning = false; verticalAxisStartPoint = e.Location; verticalAxisStartZoom = verticalZoom; Capture = true; Cursor = Cursors.SizeNS; return; }
            if (points.Count > 1) { panning = true; panStartPoint = e.Location; panStartFirstIndex = firstIndex; panStartVerticalPanOffset = verticalPanOffset; panStartHorizontalOffset = horizontalPanOffset; var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount)); var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList(); panStartVerticalRange = visible.Count > 0 ? Math.Max(visible.Max(x => x.High) - visible.Min(x => x.Low), 1e-9) / verticalZoom * 1.10 : 1.0; Capture = true; Cursor = Cursors.SizeAll; } Focus();
        }

        private void BeginOrCompleteDrawing(Point location)
        {
            if (!IsInsidePlot(location)) return;
            if (activeDrawingTool == ChartDrawingTool.HorizontalDoubleArrow || activeDrawingTool == ChartDrawingTool.VerticalDoubleArrow) { AddDrawing(location, location); drawingInProgress = false; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty; activeDrawingTool = ChartDrawingTool.None; Cursor = Cursors.Default; Invalidate(); return; }
            if (!drawingInProgress) { drawingStartPoint = location; drawingCurrentPoint = location; drawingInProgress = true; Invalidate(); return; }
            drawingCurrentPoint = location; AddDrawing(drawingStartPoint, drawingCurrentPoint); drawingInProgress = false; drawingStartPoint = Point.Empty; drawingCurrentPoint = Point.Empty; activeDrawingTool = ChartDrawingTool.None; Cursor = Cursors.Default; Invalidate();
        }
        private bool IsInsidePlot(Point point) => GetPlotRectangle().Contains(point);
        private void AddDrawing(Point start, Point end)
        {
            var plot = GetPlotRectangle(); var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount)); var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList(); if (visible.Count == 0) return; GetVerticalRange(visible, out var min, out var max);
            var x1 = ScreenToDataX(start.X, plot, visible.Count); var y1 = ScreenToPrice(start.Y, plot, min, max); var x2 = ScreenToDataX(end.X, plot, visible.Count); var y2 = ScreenToPrice(end.Y, plot, min, max);
            switch (activeDrawingTool) { case ChartDrawingTool.TrendLine: case ChartDrawingTool.TrendLineWithArrow: case ChartDrawingTool.Rectangle: drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 }); break; case ChartDrawingTool.HorizontalDoubleArrow: drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 }); break; case ChartDrawingTool.VerticalDoubleArrow: drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x1, Y2 = y2 }); break; case ChartDrawingTool.HorizontalRay: drawings.Add(new ChartDrawing { Tool = activeDrawingTool, X1 = x1, Y1 = y1, X2 = x2, Y2 = y1 }); break; }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (showCrosshair) { var plotLeft = 55; var plotRight = Math.Max(plotLeft, Width - 15); var plotTop = 15; var plotBottom = Math.Max(plotTop, Height - 35); var plotWidth = Math.Max(1, plotRight - plotLeft); var step = plotWidth / (double)Math.Max(1, visibleCount); var initialOffset = -plotWidth * 0.25; var relativeX = e.X - plotLeft - initialOffset - horizontalPanOffset; var nearest = (int)Math.Round(relativeX / step - 0.5); crosshairIndex = Math.Clamp(nearest, 0, Math.Max(0, visibleCount - 1)); crosshairPoint = new Point((int)Math.Round(plotLeft + step * (crosshairIndex + 0.5) + initialOffset + horizontalPanOffset), Math.Clamp(e.Y, plotTop, plotBottom)); Invalidate(); }
            if (drawingInProgress && activeDrawingTool != ChartDrawingTool.None) { drawingCurrentPoint = e.Location; Invalidate(); return; }
            if (draggingDrawingIndex >= 0 && draggingDrawingIndex < drawings.Count && Capture) { MoveOrResizeDrawing(draggingDrawingIndex, draggingHandle, e.Location); draggingLastPoint = e.Location; Invalidate(); return; }
            if (horizontalAxisDrag && Capture && points.Count > 1) { var delta = e.X - horizontalAxisStartPoint.X; var factor = Math.Exp(-delta / 300.0); var newCount = Math.Clamp((int)Math.Round(horizontalAxisStartVisibleCount * factor), 2, points.Count); visibleCount = newCount; firstIndex = Math.Clamp((int)Math.Round(horizontalAxisCenterIndex - newCount / 2.0), 0, Math.Max(0, points.Count - newCount)); crosshairIndex = -1; Invalidate(); return; }
            if (verticalAxisDrag && Capture && points.Count > 1) { var delta = verticalAxisStartPoint.Y - e.Y; verticalZoom = Math.Clamp(verticalAxisStartZoom * Math.Exp(delta / 700.0), 0.15, 8.0); Invalidate(); return; }
            if (panning && Capture && points.Count > 1) { var horizontalDelta = e.X - panStartPoint.X; horizontalPanOffset = Math.Clamp(panStartHorizontalOffset + horizontalDelta, -(double)Math.Max(1, Width - 70), (double)Math.Max(1, Width - 70)); var verticalDelta = e.Y - panStartPoint.Y; if (Math.Abs(verticalDelta) >= 0.5) { var plotHeight = Math.Max(1, Height - 50); verticalPanOffset = panStartVerticalPanOffset + verticalDelta * panStartVerticalRange / plotHeight; } Invalidate(); }
        }
        protected override void OnMouseUp(MouseEventArgs e) { base.OnMouseUp(e); if (e.Button == MouseButtons.Left) { panning = false; horizontalAxisDrag = false; verticalAxisDrag = false; if (draggingDrawingIndex >= 0) { draggingDrawingIndex = -1; draggingHandle = 0; } Capture = false; if (!drawingInProgress && activeDrawingTool == ChartDrawingTool.None) Cursor = Cursors.Default; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; e.Graphics.Clear(BackColor); if (points.Count == 0) return;
            var plot = GetPlotRectangle(); var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount)); var visible = points.Skip(firstIndex).Take(endIndex - firstIndex).ToList(); if (visible.Count == 0) return; GetVerticalRange(visible, out var min, out var max);
            using var gridPen = new Pen(Color.FromArgb(225,225,225),1); using var axisPen = new Pen(Color.FromArgb(150,150,150),1); using var textBrush = new SolidBrush(Color.FromArgb(70,70,70)); using var risingBrush = new SolidBrush(Color.FromArgb(35,150,80)); using var fallingBrush = new SolidBrush(Color.FromArgb(205,70,70)); using var linePen = new Pen(Color.FromArgb(35,90,160),1.6f); using var axisTextFont = new Font(Font.FontFamily,Math.Max(7.0f,Font.Size-2.0f),Font.Style); using var crosshairLabelBackBrush = new SolidBrush(Color.FromArgb(45,45,45)); using var crosshairLabelTextBrush = new SolidBrush(Color.White);
            if (showGrid) { for (var i=1;i<=5;i++){var y=plot.Top+plot.Height*i/6f;e.Graphics.DrawLine(gridPen,plot.Left,y,plot.Right,y);} for(var i=1;i<=8;i++){var x=plot.Left+plot.Width*i/9f;e.Graphics.DrawLine(gridPen,x,plot.Top,x,plot.Bottom);} }
            e.Graphics.DrawLine(axisPen,plot.Left,plot.Bottom,plot.Right,plot.Bottom); e.Graphics.DrawLine(axisPen,plot.Left,plot.Top,plot.Left,plot.Bottom);
            var step=plot.Width/(double)Math.Max(1,visible.Count); var initialOffset=-plot.Width*0.25;
            if(chartType==TradingChartType.Line){var linePoints=new List<PointF>();for(var i=0;i<visible.Count;i++){var x=(float)(plot.Left+step*(i+0.5)+initialOffset+horizontalPanOffset);var y=PriceToScreen(visible[i].Close,plot,min,max);linePoints.Add(new PointF(x,y));}if(linePoints.Count>1)e.Graphics.DrawLines(linePen,linePoints.ToArray());}
            else{var candleWidth=Math.Max(2f,(float)(step*0.65));for(var i=0;i<visible.Count;i++){var item=visible[i];var x=(float)(plot.Left+step*(i+0.5)+initialOffset+horizontalPanOffset);var high=PriceToScreen(item.High,plot,min,max);var low=PriceToScreen(item.Low,plot,min,max);var open=PriceToScreen(item.Open,plot,min,max);var close=PriceToScreen(item.Close,plot,min,max);var rising=item.Close>=item.Open;var brush=rising?risingBrush:fallingBrush;e.Graphics.DrawLine(linePen,x,high,x,low);if(chartType==TradingChartType.Candlestick){var top=Math.Min(open,close);var bottom=Math.Max(open,close);var rect=RectangleF.FromLTRB(x-candleWidth/2,top,x+candleWidth/2,Math.Max(top+1,bottom));e.Graphics.FillRectangle(brush,rect);e.Graphics.DrawRectangle(linePen,rect.X,rect.Y,rect.Width,rect.Height);}else{var barLength=rising?candleWidth:-candleWidth;e.Graphics.DrawLine(linePen,x,close,x+barLength,close);e.Graphics.DrawLine(linePen,x,open,x-barLength,open);}}}
            DrawDrawings(e.Graphics,plot,visible.Count,min,max);
            if(drawingInProgress&&activeDrawingTool!=ChartDrawingTool.None&&IsInsidePlot(drawingCurrentPoint)){using var previewPen=new Pen(Color.FromArgb(30,90,160),1.5f){DashStyle=DashStyle.Dash};DrawSingleDrawing(e.Graphics,previewPen,new ChartDrawingPreview(activeDrawingTool,drawingStartPoint,drawingCurrentPoint),plot,visible.Count,min,max);}
            if(showCrosshair&&crosshairIndex>=0&&crosshairIndex<visible.Count){var x=(float)(plot.Left+step*(crosshairIndex+0.5)+initialOffset+horizontalPanOffset);using var crosshairPen=new Pen(Color.FromArgb(120,120,120),1){DashStyle=DashStyle.Dot};e.Graphics.DrawLine(crosshairPen,x,plot.Top,x,plot.Bottom);e.Graphics.DrawLine(crosshairPen,plot.Left,crosshairPoint.Y,plot.Right,crosshairPoint.Y);}
            for(var i=0;i<=5;i++){var value=max-(max-min)*i/5.0;var y=PriceToScreen(value,plot,min,max);e.Graphics.DrawString(value.ToString("0.##"),axisTextFont,textBrush,plot.Right+4,y-axisTextFont.Height/2f);}
        }

        private sealed class ChartDrawingPreview { public ChartDrawingTool Tool { get; } public Point Start { get; } public Point End { get; } public ChartDrawingPreview(ChartDrawingTool tool,Point start,Point end){Tool=tool;Start=start;End=end;} }
        private void DrawDrawings(Graphics g,Rectangle plot,int visibleCountForDrawing,double min,double max){using var drawingPen=new Pen(Color.FromArgb(30,90,160),1.8f);using var selectedPen=new Pen(Color.FromArgb(30,90,160),3.2f);for(var i=0;i<drawings.Count;i++){var selected=i==selectedDrawingIndex;DrawSingleDrawing(g,selected?selectedPen:drawingPen,drawings[i],plot,visibleCountForDrawing,min,max);if(selected)DrawSelectionHandles(g,drawings[i],plot,visibleCountForDrawing,min,max);}}
        private void DrawSelectionHandles(Graphics g,ChartDrawing drawing,Rectangle plot,int visibleCountForDrawing,double min,double max){GetDrawingScreenPoints(drawing,plot,visibleCountForDrawing,min,max,out var start,out var end);using var handleBrush=new SolidBrush(Color.White);using var handlePen=new Pen(Color.FromArgb(30,90,160),1.5f);const float radius=4f;g.FillEllipse(handleBrush,start.X-radius,start.Y-radius,radius*2,radius*2);g.DrawEllipse(handlePen,start.X-radius,start.Y-radius,radius*2,radius*2);g.FillEllipse(handleBrush,end.X-radius,end.Y-radius,radius*2,radius*2);g.DrawEllipse(handlePen,end.X-radius,end.Y-radius,radius*2,radius*2);}
        private void DrawSingleDrawing(Graphics g,Pen pen,ChartDrawing drawing,Rectangle plot,int visibleCountForDrawing,double min,double max)
        {
            GetDrawingScreenPoints(drawing,plot,visibleCountForDrawing,min,max,out var start,out var end);
            switch(drawing.Tool){case ChartDrawingTool.TrendLine:g.DrawLine(pen,start,end);break;case ChartDrawingTool.TrendLineWithArrow:g.DrawLine(pen,start,end);DrawArrowHead(g,pen,end,start);break;case ChartDrawingTool.HorizontalDoubleArrow:g.DrawLine(pen,plot.Left,start.Y,plot.Right,start.Y);break;case ChartDrawingTool.VerticalDoubleArrow:g.DrawLine(pen,start.X,plot.Top,start.X,plot.Bottom);break;case ChartDrawingTool.HorizontalRay:var direction=end.X>=start.X?1f:-1f;var rayEnd=new PointF(direction>0?plot.Right:plot.Left,start.Y);g.DrawLine(pen,start,rayEnd);break;case ChartDrawingTool.Rectangle:var left=Math.Min(start.X,end.X);var top=Math.Min(start.Y,end.Y);var right=Math.Max(start.X,end.X);var bottom=Math.Max(start.Y,end.Y);var rect=RectangleF.FromLTRB(left,top,right,bottom);using(var fill=new SolidBrush(Color.FromArgb(225,240,255)))g.FillRectangle(fill,rect);g.DrawRectangle(pen,rect.X,rect.Y,rect.Width,rect.Height);break;}
        }
        private void DrawSingleDrawing(Graphics g,Pen pen,ChartDrawingPreview preview,Rectangle plot,int visibleCountForDrawing,double min,double max){var start=preview.Start;var end=preview.End;switch(preview.Tool){case ChartDrawingTool.TrendLine:g.DrawLine(pen,start,end);break;case ChartDrawingTool.TrendLineWithArrow:g.DrawLine(pen,start,end);DrawArrowHead(g,pen,end,start);break;case ChartDrawingTool.HorizontalDoubleArrow:g.DrawLine(pen,plot.Left,start.Y,plot.Right,start.Y);break;case ChartDrawingTool.VerticalDoubleArrow:g.DrawLine(pen,start.X,plot.Top,start.X,plot.Bottom);break;case ChartDrawingTool.HorizontalRay:var direction=end.X>=start.X?1f:-1f;var rayEnd=new PointF(direction>0?plot.Right:plot.Left,start.Y);g.DrawLine(pen,start,rayEnd);break;case ChartDrawingTool.Rectangle:var left=Math.Min(start.X,end.X);var top=Math.Min(start.Y,end.Y);var width=Math.Abs(end.X-start.X);var height=Math.Abs(end.Y-start.Y);using(var fill=new SolidBrush(Color.FromArgb(225,240,255)))g.FillRectangle(fill,left,top,width,height);g.DrawRectangle(pen,left,top,width,height);break;}}
        private void GetDrawingScreenPoints(ChartDrawing drawing,Rectangle plot,int visibleCountForDrawing,double min,double max,out PointF start,out PointF end){if(drawing.Tool==ChartDrawingTool.HorizontalDoubleArrow){var y=PriceToScreen(drawing.Y1,plot,min,max);start=new PointF(plot.Left,y);end=new PointF(plot.Right,y);return;}if(drawing.Tool==ChartDrawingTool.VerticalDoubleArrow){var x=DataToScreen(drawing.X1,0,plot,visibleCountForDrawing,min,max).X;start=new PointF(x,plot.Top);end=new PointF(x,plot.Bottom);return;}start=DataToScreen(drawing.X1,drawing.Y1,plot,visibleCountForDrawing,min,max);end=DataToScreen(drawing.X2,drawing.Y2,plot,visibleCountForDrawing,min,max);}
        private int HitTestDrawingHandle(Point location,Rectangle plot,out int handle){handle=0;if(drawings.Count==0)return -1;var endIndex=Math.Min(points.Count,firstIndex+Math.Max(1,visibleCount));var visibleCountForDrawing=Math.Max(1,endIndex-firstIndex);var visible=points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();if(visible.Count==0)return -1;GetVerticalRange(visible,out var min,out var max);const double tolerance=9.0;for(var i=drawings.Count-1;i>=0;i--){GetDrawingScreenPoints(drawings[i],plot,visibleCountForDrawing,min,max,out var start,out var end);if(DistanceToPoint(location,start)<=tolerance){handle=1;return i;}if(DistanceToPoint(location,end)<=tolerance){handle=2;return i;}}return -1;}
        private int HitTestDrawing(Point location,Rectangle plot){if(drawings.Count==0)return -1;var endIndex=Math.Min(points.Count,firstIndex+Math.Max(1,visibleCount));var visibleCountForDrawing=Math.Max(1,endIndex-firstIndex);var visible=points.Skip(firstIndex).Take(visibleCountForDrawing).ToList();if(visible.Count==0)return -1;GetVerticalRange(visible,out var min,out var max);const double tolerance=7.0;for(var i=drawings.Count-1;i>=0;i--){var drawing=drawings[i];GetDrawingScreenPoints(drawing,plot,visibleCountForDrawing,min,max,out var start,out var end);switch(drawing.Tool){case ChartDrawingTool.TrendLine:case ChartDrawingTool.TrendLineWithArrow:case ChartDrawingTool.HorizontalDoubleArrow:case ChartDrawingTool.VerticalDoubleArrow:if(DistanceToSegment(location,start,end)<=tolerance)return i;break;case ChartDrawingTool.HorizontalRay:var direction=end.X>=start.X?1f:-1f;var rayEnd=new PointF(direction>0?plot.Right:plot.Left,start.Y);if(DistanceToSegment(location,start,rayEnd)<=tolerance)return i;break;case ChartDrawingTool.Rectangle:var left=Math.Min(start.X,end.X);var right=Math.Max(start.X,end.X);var top=Math.Min(start.Y,end.Y);var bottom=Math.Max(start.Y,end.Y);var rect=new RectangleF(left,top,right-left,bottom-top);if(rect.Contains(location)||DistanceToSegment(location,new PointF(left,top),new PointF(right,top))<=tolerance||DistanceToSegment(location,new PointF(right,top),new PointF(right,bottom))<=tolerance||DistanceToSegment(location,new PointF(right,bottom),new PointF(left,bottom))<=tolerance||DistanceToSegment(location,new PointF(left,bottom),new PointF(left,top))<=tolerance)return i;break;}}return -1;}
        private void MoveOrResizeDrawing(int index,int handle,Point location){if(index<0||index>=drawings.Count)return;var plot=GetPlotRectangle();var endIndex=Math.Min(points.Count,firstIndex+Math.Max(1,visibleCount));var visible=points.Skip(firstIndex).Take(endIndex-firstIndex).ToList();if(visible.Count==0)return;GetVerticalRange(visible,out var min,out var max);var drawing=drawings[index];if(drawing.Tool==ChartDrawingTool.HorizontalDoubleArrow){drawing.Y1=ScreenToPrice(location.Y,plot,min,max);drawing.Y2=drawing.Y1;return;}if(drawing.Tool==ChartDrawingTool.VerticalDoubleArrow){var x=ScreenToDataX(location.X,plot,visible.Count);drawing.X1=x;drawing.X2=x;return;}if(handle==1){drawing.X1=ScreenToDataX(location.X,plot,visible.Count);drawing.Y1=ScreenToPrice(location.Y,plot,min,max);return;}if(handle==2){drawing.X2=ScreenToDataX(location.X,plot,visible.Count);drawing.Y2=ScreenToPrice(location.Y,plot,min,max);return;}var deltaX=ScreenToDataX(location.X,plot,visible.Count)-ScreenToDataX(draggingLastPoint.X,plot,visible.Count);var deltaY=ScreenToPrice(location.Y,plot,min,max)-ScreenToPrice(draggingLastPoint.Y,plot,min,max);drawing.X1+=deltaX;drawing.X2+=deltaX;drawing.Y1+=deltaY;drawing.Y2+=deltaY;}
        private static double DistanceToPoint(PointF point,PointF target){var dx=point.X-target.X;var dy=point.Y-target.Y;return Math.Sqrt(dx*dx+dy*dy);}
        private static double DistanceToSegment(PointF point,PointF start,PointF end){var dx=end.X-start.X;var dy=end.Y-start.Y;if(Math.Abs(dx)<0.001&&Math.Abs(dy)<0.001)return Math.Sqrt(Math.Pow(point.X-start.X,2)+Math.Pow(point.Y-start.Y,2));var t=((point.X-start.X)*dx+(point.Y-start.Y)*dy)/(dx*dx+dy*dy);t=Math.Clamp(t,0f,1f);var nearestX=start.X+t*dx;var nearestY=start.Y+t*dy;return Math.Sqrt(Math.Pow(point.X-nearestX,2)+Math.Pow(point.Y-nearestY,2));}
        private static void DrawArrowHead(Graphics g,Pen basePen,PointF tip,PointF from){var dx=tip.X-from.X;var dy=tip.Y-from.Y;var length=Math.Sqrt(dx*dx+dy*dy);if(length<0.001)return;const float size=9f;var ux=(float)(dx/length);var uy=(float)(dy/length);var px=-uy;var py=ux;var left=new PointF(tip.X-ux*size+px*size*0.45f,tip.Y-uy*size+py*size*0.45f);var right=new PointF(tip.X-ux*size-px*size*0.45f,tip.Y-uy*size-py*size*0.45f);g.DrawLine(basePen,tip,left);g.DrawLine(basePen,tip,right);}
        private Rectangle GetPlotRectangle(){var left=55;var top=15;var right=Math.Max(left+1,Width-15);var bottom=Math.Max(top+1,Height-35);return Rectangle.FromLTRB(left,top,right,bottom);}
        private void GetVerticalRange(List<TradingChartPoint> visible,out double min,out double max){min=visible.Min(x=>x.Low);max=visible.Max(x=>x.High);var range=Math.Max(max-min,Math.Max(Math.Abs(max),1.0)*0.01);var center=(min+max)/2.0+verticalPanOffset;var adjustedRange=range/verticalZoom;min=center-adjustedRange/2.0;max=center+adjustedRange/2.0;}
        private float PriceToScreen(double price,Rectangle plot,double min,double max){if(Math.Abs(max-min)<1e-12)return plot.Top+plot.Height/2f;return(float)(plot.Bottom-(price-min)/(max-min)*plot.Height);}
        private PointF DataToScreen(double x,double y,Rectangle plot,int visibleCountForDrawing,double min,double max){var step=plot.Width/(double)Math.Max(1,visibleCountForDrawing);var initialOffset=-plot.Width*0.25;var screenX=plot.Left+step*(x+0.5)+initialOffset+horizontalPanOffset;return new PointF((float)screenX,PriceToScreen(y,plot,min,max));}
        private double ScreenToDataX(float screenX,Rectangle plot,int visibleCountForDrawing){var step=plot.Width/(double)Math.Max(1,visibleCountForDrawing);var initialOffset=-plot.Width*0.25;return(screenX-plot.Left-initialOffset-horizontalPanOffset)/step-0.5;}
        private double ScreenToPrice(float screenY,Rectangle plot,double min,double max){if(plot.Height<=0)return min;var ratio=(plot.Bottom-screenY)/(double)plot.Height;return min+ratio*(max-min);}
        private bool IsInsidePlot(PointF point)=>GetPlotRectangle().Contains(Point.Round(point));
    }
}