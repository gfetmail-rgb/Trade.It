using System.Drawing.Drawing2D;

namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private double chartPanCompensation;

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_RBUTTONDOWN = 0x0204;
            const int WM_SIZE = 0x0005;

            if (m.Msg == WM_SIZE || m.Msg == WM_PAINT || m.Msg == WM_MOUSEMOVE ||
                m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONUP)
            {
                EnsureChartPanCompensation();
                EnsureInitialTopMargin();
            }

            var location = GetMousePointFromMessage(m);

            // Extra drawing tools own their mouse input completely. Do not pass
            // these messages to Control.WndProc because the normal chart handler
            // would interpret the same click as pan/axis-drag input.
            if (ExtraDrawingActive || extraDrawingInProgress || extraDraggingHandleActive)
            {
                if (m.Msg == WM_LBUTTONDOWN)
                {
                    ExtraDrawing_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, location.X, location.Y, 0));
                    return;
                }

                if (m.Msg == WM_RBUTTONDOWN)
                {
                    ExtraDrawing_MouseDown(this, new MouseEventArgs(MouseButtons.Right, 1, location.X, location.Y, 0));
                    return;
                }

                if (m.Msg == WM_MOUSEMOVE)
                {
                    ExtraDrawing_MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, location.X, location.Y, 0));
                    return;
                }

                if (m.Msg == WM_LBUTTONUP)
                {
                    ExtraDrawing_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, location.X, location.Y, 0));
                    return;
                }
            }

            // Once an extra drawing is completed, clicks on the drawing or while
            // it is selected must also be handled before the normal chart handler.
            // A blank click then cleanly deselects it instead of starting a pan.
            if (m.Msg == WM_LBUTTONDOWN && !extraInputHandled &&
                (selectedExtraDrawingIndex >= 0 || IsExtraDrawingHit(location)))
            {
                ExtraDrawing_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, location.X, location.Y, 0));
                return;
            }

            // ExtraDrawing_MouseDown currently marks every left-click as handled
            // whenever at least one extra drawing exists, even when the click is
            // on an ordinary chart drawing or on an empty chart area. In that case
            // the normal OnMouseDown logic must receive the message so that regular
            // drawings remain selectable/movable and new drawing tools can be used.
            // Completed Extra drawings must remain in the control while the normal
            // mouse event is dispatched. In particular, TextLabel can open a modal
            // dialog from that dispatch; temporarily clearing extraDrawings would
            // make Pitchfork/FibonacciExtension disappear until the dialog closes.
            base.WndProc(ref m);
                }
                finally
                {
                    extraDrawings.AddRange(savedExtraDrawings);
                }
                return;
            }

            base.WndProc(ref m);

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

        private bool IsExtraToolBlankArea(Point location)
        {
            var plot = GetPlotRectangle();
            if (!plot.Contains(location) || points.Count == 0)
                return false;

            var endIndex = Math.Min(points.Count, firstIndex + Math.Max(1, visibleCount));
            var visibleCountForDrawing = Math.Max(1, endIndex - firstIndex);
            var step = plot.Width / (double)Math.Max(1, visibleCountForDrawing);
            var lastCandleX = plot.Left + step * (visibleCountForDrawing - 0.5) + (-plot.Width * 0.25) + horizontalPanOffset;
            var candleWidth = Math.Max(2.0, step * 0.65);

            return location.X > lastCandleX + candleWidth / 2.0;
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

                if (d.Tool == ExtraDrawingTool.Pitchfork &&
                    IsExtraDrawingBodyHit(location, d, plot, visibleCountForDrawing, min, max))
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