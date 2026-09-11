namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartDrawingToolsInitialized;
        private readonly System.Windows.Forms.Timer drawingStateTimer = new();

        private void InitializeChartDrawingTools()
        {
            if (chartDrawingToolsInitialized)
                return;

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            chartDrawingToolsInitialized = true;

            ConfigureDrawingIcon(drawTrendChannelButton, DrawingIcon.TrendChannel);
            ConfigureDrawingIcon(drawRectangleButton, DrawingIcon.Rectangle);
            ConfigureDrawingIcon(drawFibonacciButton, DrawingIcon.Fibonacci);
            ConfigureDrawingIcon(drawTextButton, DrawingIcon.Text);
            ConfigureDrawingIcon(drawPitchforkButton, DrawingIcon.Pitchfork);
            ConfigureDrawingIcon(drawFibonacciExtensionButton, DrawingIcon.FibonacciExtension);
            ConfigureDrawingIcon(drawMeasureButton, DrawingIcon.Measure);

            drawTrendLineButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLine, drawTrendLineButton);
            drawTrendChannelButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendChannel, drawTrendChannelButton);
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalDoubleArrow, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalDoubleArrow, drawVerticalDoubleButton);
            drawHorizontalRayButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalRay, drawHorizontalRayButton);
            drawTrendLineArrowButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLineWithArrow, drawTrendLineArrowButton);
            drawRectangleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.Rectangle, drawRectangleButton);
            drawFibonacciButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Fibonacci, drawFibonacciButton);
            drawTextButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Text, drawTextButton);
            drawPitchforkButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.Pitchfork, drawPitchforkButton);
            drawFibonacciExtensionButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.FibonacciExtension, drawFibonacciExtensionButton);
            drawMeasureButton.Click += (_, _) => ActivateExtraDrawingTool(ExtraDrawingSelection.Measure, drawMeasureButton);

            chartTabControl.SelectedIndexChanged += ChartDrawingTabChanged;
            closeAllChartsMenuItem.Click += (_, _) => ResetDrawingToolButtons();
            drawingStateTimer.Interval = 100;
            drawingStateTimer.Tick += DrawingStateTimer_Tick;
            drawingStateTimer.Start();
            ResetDrawingToolButtons();
        }

        private enum DrawingIcon
        {
            TrendChannel,
            Rectangle,
            Fibonacci,
            Text,
            Pitchfork,
            FibonacciExtension,
            Measure
        }

        private enum AdvancedDrawingSelection
        {
            Fibonacci,
            Text
        }

        private enum ExtraDrawingSelection
        {
            Pitchfork,
            FibonacciExtension,
            Measure
        }

        private void ConfigureDrawingIcon(Button button, DrawingIcon icon)
        {
            button.Text = string.Empty;
            button.Tag = icon;
            button.Paint += DrawingIconButton_Paint;
            button.AccessibleName = icon switch
            {
                DrawingIcon.TrendChannel => "کانال روند",
                DrawingIcon.Rectangle => "مستطیل",
                DrawingIcon.Fibonacci => "فیبوناچی",
                DrawingIcon.Pitchfork => "چنگال اندروز",
                DrawingIcon.FibonacciExtension => "فیبوناچی اکسپنشن",
                DrawingIcon.Measure => "خط کش اندازه گیری",
                _ => "متن"
            };
        }

        private void DrawingIconButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button button || button.Tag is not DrawingIcon icon)
                return;

            var bounds = button.ClientRectangle;
            var centerX = bounds.Width / 2;
            var centerY = bounds.Height / 2;
            using var pen = new Pen(button.Enabled ? SystemColors.ControlText : SystemColors.GrayText, 2.2f)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            if (icon == DrawingIcon.Rectangle)
            {
                var rect = new RectangleF(centerX - 20, centerY - 10, 40, 20);
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                return;
            }

            if (icon == DrawingIcon.Fibonacci)
            {
                e.Graphics.DrawLine(pen, centerX - 22, centerY + 8, centerX + 22, centerY - 8);
                e.Graphics.DrawLine(pen, centerX - 19, centerY - 8, centerX + 22, centerY - 8);
                e.Graphics.DrawLine(pen, centerX - 19, centerY - 2, centerX + 22, centerY - 2);
                e.Graphics.DrawLine(pen, centerX - 19, centerY + 4, centerX + 22, centerY + 4);
                e.Graphics.DrawLine(pen, centerX - 19, centerY + 10, centerX + 22, centerY + 10);
                return;
            }

            if (icon == DrawingIcon.Text)
            {
                using var font = new Font(button.Font.FontFamily, 15f, FontStyle.Bold);
                using var brush = new SolidBrush(button.Enabled ? SystemColors.ControlText : SystemColors.GrayText);
                var text = "T";
                var size = e.Graphics.MeasureString(text, font);
                e.Graphics.DrawString(text, font, brush, centerX - size.Width / 2f, centerY - size.Height / 2f - 1);
                return;
            }

            if (icon == DrawingIcon.Pitchfork)
            {
                e.Graphics.DrawLine(pen, centerX - 20, centerY + 9, centerX + 20, centerY - 9);
                e.Graphics.DrawLine(pen, centerX - 12, centerY + 5, centerX + 16, centerY + 12);
                e.Graphics.DrawLine(pen, centerX - 12, centerY + 5, centerX + 16, centerY - 2);
                e.Graphics.DrawLine(pen, centerX - 20, centerY + 9, centerX - 12, centerY + 5);
                return;
            }

            if (icon == DrawingIcon.FibonacciExtension)
            {
                e.Graphics.DrawLine(pen, centerX - 20, centerY + 9, centerX - 4, centerY - 8);
                e.Graphics.DrawLine(pen, centerX - 4, centerY - 8, centerX + 8, centerY + 3);
                e.Graphics.DrawLine(pen, centerX + 8, centerY - 8, centerX + 21, centerY - 8);
                e.Graphics.DrawLine(pen, centerX + 8, centerY + 2, centerX + 21, centerY + 2);
                e.Graphics.DrawLine(pen, centerX + 8, centerY + 10, centerX + 21, centerY + 10);
                return;
            }

            if (icon == DrawingIcon.Measure)
            {
                e.Graphics.DrawLine(pen, centerX - 21, centerY + 8, centerX + 21, centerY - 8);
                e.Graphics.DrawLine(pen, centerX - 18, centerY + 3, centerX - 24, centerY + 13);
                e.Graphics.DrawLine(pen, centerX + 18, centerY - 13, centerX + 24, centerY - 3);
                return;
            }

            e.Graphics.DrawLine(pen, centerX - 24, centerY + 9, centerX + 23, centerY - 8);
            e.Graphics.DrawLine(pen, centerX - 24, centerY + 17, centerX + 23, centerY);
        }

        private void ActivateDrawingTool(ChartDrawingTool tool, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelAdvancedDrawing();
            chart.CancelExtraDrawing();

            if (chart.ActiveDrawingTool == tool)
            {
                chart.CancelDrawing();
                ResetDrawingToolButtons();
                return;
            }

            chart.CancelDrawing();
            chart.SetDrawingTool(tool);
            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, true);
        }

        private void ActivateAdvancedDrawingTool(AdvancedDrawingSelection selection, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelDrawing();
            chart.CancelExtraDrawing();

            if (selection == AdvancedDrawingSelection.Fibonacci)
                chart.ActivateFibonacciRetracement();
            else
                chart.ActivateTextLabel();

            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, chart.AdvancedDrawingActive);
        }

        private void ActivateExtraDrawingTool(ExtraDrawingSelection selection, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelDrawing();
            chart.CancelAdvancedDrawing();

            if (selection == ExtraDrawingSelection.Pitchfork)
                chart.ActivatePitchfork();
            else if (selection == ExtraDrawingSelection.FibonacciExtension)
                chart.ActivateFibonacciExtension();
            else
                chart.ActivateMeasureTool();

            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, chart.ExtraDrawingActive);
        }

        private void DrawingStateTimer_Tick(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null ||
                (chart.ActiveDrawingTool == ChartDrawingTool.None &&
                 !chart.AdvancedDrawingActive &&
                 !chart.ExtraDrawingActive))
                ResetDrawingToolButtons();
        }

        private void ChartDrawingTabChanged(object? sender, EventArgs e)
        {
            GetActiveChart()?.CancelDrawing();
            GetActiveChart()?.CancelAdvancedDrawing();
            GetActiveChart()?.CancelExtraDrawing();
            ResetDrawingToolButtons();
        }

        private void ResetDrawingToolButtons()
        {
            SetToggleButtonState(drawTrendLineButton, false);
            SetToggleButtonState(drawTrendChannelButton, false);
            SetToggleButtonState(drawHorizontalDoubleButton, false);
            SetToggleButtonState(drawVerticalDoubleButton, false);
            SetToggleButtonState(drawHorizontalRayButton, false);
            SetToggleButtonState(drawTrendLineArrowButton, false);
            SetToggleButtonState(drawRectangleButton, false);
            SetToggleButtonState(drawFibonacciButton, false);
            SetToggleButtonState(drawTextButton, false);
            SetToggleButtonState(drawPitchforkButton, false);
            SetToggleButtonState(drawFibonacciExtensionButton, false);
            SetToggleButtonState(drawMeasureButton, false);
        }
    }
}
