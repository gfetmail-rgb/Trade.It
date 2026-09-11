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
        private readonly System.Windows.Forms.Timer drawingStateTimer = new();

        private void InitializeChartDrawingTools()
        {
            if (chartDrawingToolsInitialized)
                return;

            chartDrawingToolsInitialized = true;

            drawTrendLineButton = CreateDrawingToolButton("خط روند", new Point(790, 7), 82);
            drawTrendChannelButton = CreateDrawingToolButton("کانال روند", new Point(877, 7), 82);
            drawHorizontalDoubleButton = CreateDrawingToolButton("افقی دو سر", new Point(964, 7), 82);
            drawVerticalDoubleButton = CreateDrawingToolButton("عمودی دو سر", new Point(1051, 7), 82);
            drawHorizontalRayButton = CreateDrawingToolButton("نیم خط افقی", new Point(1138, 7), 82);
            drawTrendLineArrowButton = CreateDrawingToolButton("خط روند فلش", new Point(1225, 7), 82);
            drawRectangleButton = CreateDrawingToolButton("مستطیل", new Point(1312, 7), 82);

            ConfigureDrawingIcon(drawTrendChannelButton, DrawingIcon.TrendChannel);
            ConfigureDrawingIcon(drawRectangleButton, DrawingIcon.Rectangle);

            chartToolbarPanel.Controls.Add(drawTrendLineButton);
            chartToolbarPanel.Controls.Add(drawTrendChannelButton);
            chartToolbarPanel.Controls.Add(drawHorizontalDoubleButton);
            chartToolbarPanel.Controls.Add(drawVerticalDoubleButton);
            chartToolbarPanel.Controls.Add(drawHorizontalRayButton);
            chartToolbarPanel.Controls.Add(drawTrendLineArrowButton);
            chartToolbarPanel.Controls.Add(drawRectangleButton);

            drawTrendLineButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLine, drawTrendLineButton);
            drawTrendChannelButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendChannel, drawTrendChannelButton);
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalDoubleArrow, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalDoubleArrow, drawVerticalDoubleButton);
            drawHorizontalRayButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalRay, drawHorizontalRayButton);
            drawTrendLineArrowButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLineWithArrow, drawTrendLineArrowButton);
            drawRectangleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.Rectangle, drawRectangleButton);

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
            Rectangle
        }

        private void ConfigureDrawingIcon(Button button, DrawingIcon icon)
        {
            button.Text = string.Empty;
            button.Tag = icon;
            button.Paint += DrawingIconButton_Paint;
            button.AccessibleName = icon == DrawingIcon.TrendChannel ? "کانال روند" : "مستطیل";
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

        private void DrawingStateTimer_Tick(object? sender, EventArgs e)
        {
            var chart = GetActiveChart();
            if (chart == null || chart.ActiveDrawingTool == ChartDrawingTool.None)
                ResetDrawingToolButtons();
        }

        private void ChartDrawingTabChanged(object? sender, EventArgs e)
        {
            GetActiveChart()?.CancelDrawing();
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
        }
    }
}
