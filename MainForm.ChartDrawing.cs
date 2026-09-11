namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartDrawingToolsInitialized;
        private Button? drawTrendLineButton;
        private Button? drawTrendChannelButton;
        private Button? drawHorizontalDoubleButton;
        private Button? drawVerticalDoubleButton;
        private Button? drawHorizontalRayButton;
        private Button? drawTrendLineArrowButton;
        private Button? drawRectangleButton;
        private Button? drawFibonacciButton;
        private Button? drawTextButton;
        private readonly System.Windows.Forms.Timer drawingStateTimer = new();

        private void InitializeChartDrawingTools()
        {
            if (chartDrawingToolsInitialized)
                return;

            chartDrawingToolsInitialized = true;

            drawTrendLineButton = CreateDrawingToolButton("خط روند", new Point(0, 0), 82);
            drawTrendChannelButton = CreateDrawingToolButton("کانال روند", new Point(0, 0), 82);
            drawHorizontalDoubleButton = CreateDrawingToolButton("افقی دو سر", new Point(0, 0), 82);
            drawVerticalDoubleButton = CreateDrawingToolButton("عمودی دو سر", new Point(0, 0), 82);
            drawHorizontalRayButton = CreateDrawingToolButton("نیم خط افقی", new Point(0, 0), 82);
            drawTrendLineArrowButton = CreateDrawingToolButton("خط روند فلش", new Point(0, 0), 82);
            drawRectangleButton = CreateDrawingToolButton("مستطیل", new Point(0, 0), 82);
            drawFibonacciButton = CreateDrawingToolButton("فیبوناچی", new Point(0, 0), 82);
            drawTextButton = CreateDrawingToolButton("متن", new Point(0, 0), 82);

            ConfigureDrawingIcon(drawTrendChannelButton, DrawingIcon.TrendChannel);
            ConfigureDrawingIcon(drawRectangleButton, DrawingIcon.Rectangle);
            ConfigureDrawingIcon(drawFibonacciButton, DrawingIcon.Fibonacci);
            ConfigureDrawingIcon(drawTextButton, DrawingIcon.Text);

            chartToolbarPanel.Controls.Add(drawTrendLineButton);
            chartToolbarPanel.Controls.Add(drawTrendChannelButton);
            chartToolbarPanel.Controls.Add(drawHorizontalDoubleButton);
            chartToolbarPanel.Controls.Add(drawVerticalDoubleButton);
            chartToolbarPanel.Controls.Add(drawHorizontalRayButton);
            chartToolbarPanel.Controls.Add(drawTrendLineArrowButton);
            chartToolbarPanel.Controls.Add(drawRectangleButton);
            chartToolbarPanel.Controls.Add(drawFibonacciButton);
            chartToolbarPanel.Controls.Add(drawTextButton);

            ArrangeDrawingToolButtons();
            chartToolbarPanel.Resize += (_, _) => ArrangeDrawingToolButtons();

            drawTrendLineButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLine, drawTrendLineButton);
            drawTrendChannelButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendChannel, drawTrendChannelButton);
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalDoubleArrow, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalDoubleArrow, drawVerticalDoubleButton);
            drawHorizontalRayButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalRay, drawHorizontalRayButton);
            drawTrendLineArrowButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLineWithArrow, drawTrendLineArrowButton);
            drawRectangleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.Rectangle, drawRectangleButton);
            drawFibonacciButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Fibonacci, drawFibonacciButton);
            drawTextButton.Click += (_, _) => ActivateAdvancedDrawingTool(AdvancedDrawingSelection.Text, drawTextButton);

            chartTabControl.SelectedIndexChanged += ChartDrawingTabChanged;
            closeAllChartsMenuItem.Click += (_, _) => ResetDrawingToolButtons();
            drawingStateTimer.Interval = 100;
            drawingStateTimer.Tick += DrawingStateTimer_Tick;
            drawingStateTimer.Start();
            ResetDrawingToolButtons();
        }

        private void ArrangeDrawingToolButtons()
        {
            if (drawTrendLineButton == null || drawTrendChannelButton == null ||
                drawHorizontalDoubleButton == null || drawVerticalDoubleButton == null ||
                drawHorizontalRayButton == null || drawTrendLineArrowButton == null ||
                drawRectangleButton == null || drawFibonacciButton == null || drawTextButton == null)
                return;

            var buttons = new[]
            {
                drawTrendLineButton,
                drawTrendChannelButton,
                drawHorizontalDoubleButton,
                drawVerticalDoubleButton,
                drawHorizontalRayButton,
                drawTrendLineArrowButton,
                drawRectangleButton,
                drawFibonacciButton,
                drawTextButton
            };

            const int startX = 790;
            const int rowY = 7;
            const int secondRowY = 51;
            const int buttonWidth = 82;
            const int gap = 5;
            const int horizontalPadding = 6;

            var oneLineEnd = startX + buttons.Length * buttonWidth + (buttons.Length - 1) * gap;
            var oneLine = chartToolbarPanel.ClientSize.Width >= oneLineEnd + horizontalPadding;
            var x = oneLine ? startX : horizontalPadding;
            var y = oneLine ? rowY : secondRowY;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Location = new Point(x, y);
                x += buttonWidth + gap;
            }

            chartToolbarPanel.Height = oneLine ? 51 : 94;
        }

        private enum DrawingIcon
        {
            TrendChannel,
            Rectangle,
            Fibonacci,
            Text
        }

        private enum AdvancedDrawingSelection
        {
            Fibonacci,
            Text
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

            e.Graphics.DrawLine(pen, centerX - 24, centerY + 9, centerX + 23, centerY - 8);
            e.Graphics.DrawLine(pen, centerX - 24, centerY + 17, centerX + 23, centerY);
        }

        private Button CreateDrawingToolButton(string text, Point location, int width)
        {
            return new Button
            {
                Location = location,
                Name = "draw" + Guid.NewGuid().ToString("N"),
                Size = new Size(width, 34),
                TabIndex = 20,
                Text = text,
                UseVisualStyleBackColor = true,
                RightToLeft = RightToLeft.Yes
            };
        }

        private void ActivateDrawingTool(ChartDrawingTool tool, Button selectedButton)
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            chart.CancelAdvancedDrawing();

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

            if (selection == AdvancedDrawingSelection.Fibonacci)
                chart.ActivateFibonacciRetracement();
            else
                chart.ActivateTextLabel();

            ResetDrawingToolButtons();
            SetToggleButtonState(selectedButton, chart.AdvancedDrawingActive);
        }

        private void DrawingStateTimer_Tick(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null || (chart.ActiveDrawingTool == ChartDrawingTool.None && !chart.AdvancedDrawingActive))
                ResetDrawingToolButtons();
        }

        private void ChartDrawingTabChanged(object? sender, EventArgs e)
        {
            GetActiveChart()?.CancelDrawing();
            GetActiveChart()?.CancelAdvancedDrawing();
            ResetDrawingToolButtons();
        }

        private void ResetDrawingToolButtons()
        {
            if (drawTrendLineButton != null)
                SetToggleButtonState(drawTrendLineButton, false);
            if (drawTrendChannelButton != null)
                SetToggleButtonState(drawTrendChannelButton, false);
            if (drawHorizontalDoubleButton != null)
                SetToggleButtonState(drawHorizontalDoubleButton, false);
            if (drawVerticalDoubleButton != null)
                SetToggleButtonState(drawVerticalDoubleButton, false);
            if (drawHorizontalRayButton != null)
                SetToggleButtonState(drawHorizontalRayButton, false);
            if (drawTrendLineArrowButton != null)
                SetToggleButtonState(drawTrendLineArrowButton, false);
            if (drawRectangleButton != null)
                SetToggleButtonState(drawRectangleButton, false);
            if (drawFibonacciButton != null)
                SetToggleButtonState(drawFibonacciButton, false);
            if (drawTextButton != null)
                SetToggleButtonState(drawTextButton, false);
        }
    }
}
