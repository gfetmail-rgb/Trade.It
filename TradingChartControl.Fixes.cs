using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private double chartPanCompensation;

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_SIZE = 0x0005;

            if (m.Msg == WM_SIZE || m.Msg == WM_PAINT || m.Msg == WM_MOUSEMOVE ||
                m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONUP)
            {
                EnsureChartPanCompensation();
                EnsureInitialTopMargin();
            }

            // An active three-point tool must be cancelled deterministically when
            // the user clicks outside the plotting rectangle. Do this at message
            // level, before the normal OnMouseDown path can start panning/axis drag.
            if (m.Msg == WM_LBUTTONDOWN && ExtraDrawingActive)
            {
                var location = GetMousePointFromMessage(m);
                if (!GetPlotRectangle().Contains(location))
                {
                    CancelExtraDrawing();
                    extraInputHandled = true;
                }
            }

            var detachedExtraMouseDown = false;
            if (m.Msg == WM_LBUTTONDOWN && !ExtraDrawingActive && !extraDraggingHandleActive &&
                !IsExtraDrawingHit(GetMousePointFromMessage(m)) && !extraInputHandled)
            {
                MouseDown -= ExtraDrawing_MouseDown;
                detachedExtraMouseDown = true;
                extraInputHandled = false;
            }

            base.WndProc(ref m);

            // All moving chart content, including the crosshair and its labels,
            // is rendered by OnPaint on the control's double-buffered surface.
            // Never call CreateGraphics() after WM_PAINT: that paints directly
            // over the already-presented buffer and causes visible flicker.
            if (detachedExtraMouseDown)
                MouseDown += ExtraDrawing_MouseDown;

            if (m.Msg == WM_MOUSEMOVE && verticalAxisDrag && Capture && points.Count > 1)
            {
                var delta = Cursor.Position.Y - PointToScreen(verticalAxisStartPoint).Y;
                verticalZoom = Math.Clamp(
                    verticalAxisStartZoom * Math.Exp(-delta / 200.0),
                    0.1,
                    20.0);
                initialTopMarginApplied = false;
                Invalidate();
            }
        }

        private Point GetMousePointFromMessage(Message m)
        {
            var x = (short)(long)m.LParam;
            var y = (short)((long)m.LParam >> 16);
            return new Point(x, y);
        }

        private bool IsExtraDrawingHit(Point location)
        {
            if (extraDrawings.Count == 0)
                return false;

            if (!TryGetExtraContext(out var plot, out var visibleCountForDrawing, out var min, out var max))
                return false;

            for (var i = extraDrawings.Count - 1; i >= 0; i--)
            {
                var d = extraDrawings[i];
                var p1 = DataToScreen(d.X1, d.Y1, plot, visibleCountForDrawing, min, max);
                var p2 = DataToScreen(d.X2, d.Y2, plot, visibleCountForDrawing, min, max);
                var p3 = DataToScreen(d.X3, d.Y3, plot, visibleCountForDrawing, min, max);

                if (DistanceToPoint(location, p1) <= 10f ||
                    DistanceToPoint(location, p2) <= 10f ||
                    DistanceToPoint(location, p3) <= 10f)
                    return true;

                if (d.Tool == ExtraDrawingTool.FibonacciExtension &&
                    HitTestFibonacciLevel(location, d, plot, visibleCountForDrawing, min, max))
                    return true;

                if (d.Tool == ExtraDrawingTool.Measure &&
                    DistanceToSegment(location, p1, p2) <= 7f)
                    return true;
            }

            return false;
        }

        private void EnsureChartPanCompensation()
        {
            var plot = GetPlotRectangle();
            var desiredMargin = Math.Clamp(ChartRightEmptyPercent, 0.0, 90.0) / 100.0;
            var desired = plot.Width * (0.25 - desiredMargin);
            var delta = desired - chartPanCompensation;
            if (Math.Abs(delta) < 0.01)
                return;

            horizontalPanOffset += delta;
            chartPanCompensation = desired;
        }

        private bool IsSyntheticNoDateAxis()
        {
            if (points.Count == 0 || points[0].Date != DateTime.UnixEpoch)
                return false;

            for (var i = 1; i < points.Count; i++)
            {
                if (points[i].Date != DateTime.UnixEpoch.AddDays(i))
                    return false;
            }

            return true;
        }

        public void ClearAllDrawings()
        {
            CancelDrawing();
            CancelExtraDrawing();
            advancedDrawings.Clear();
            extraDrawings.Clear();
            drawings.Clear();
            selectedDrawingIndex = -1;
            draggingDrawingIndex = -1;
            draggingHandle = 0;
            Capture = false;
            Cursor = Cursors.Default;
            Invalidate();
        }
    }
}