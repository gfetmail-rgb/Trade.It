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
        TrendChannel,
        HorizontalDoubleArrow,
        VerticalDoubleArrow,
        HorizontalRay,
        TrendLineWithArrow,
        Rectangle
    }

    internal sealed class TradingChartPoint
    {
        public DateTime Date { get; init; }
        public bool HasRealDate { get; init; }
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
        private int drawingStage;
        private Point drawingStartPoint;
        private Point drawingCurrentPoint;
        private Point drawingSecondPoint;
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
            public double X3 { get; set; }
            public double Y3 { get; set; }
        }
