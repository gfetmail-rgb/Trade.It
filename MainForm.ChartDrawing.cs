namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartDrawingToolsInitialized;
        private Button? drawTrendLineButton;
        private Button? drawHorizontalDoubleButton;
        private Button? drawVerticalDoubleButton;
        private Button? drawHorizontalRayButton;
        private Button? drawTrendLineArrowButton;

        private void InitializeChartDrawingTools()
        {
            if (chartDrawingToolsInitialized)
                return;

            chartDrawingToolsInitialized = true;

            drawTrendLineButton = CreateDrawingToolButton("خط روند", new Point(790, 7), 82);
            drawHorizontalDoubleButton = CreateDrawingToolButton("افقی دو سر", new Point(877, 7), 82);
            drawVerticalDoubleButton = CreateDrawingToolButton("عمودی دو سر", new Point(964, 7), 82);
            drawHorizontalRayButton = CreateDrawingToolButton("نیم خط افقی", new Point(1051, 7), 82);
            drawTrendLineArrowButton = CreateDrawingToolButton("خط روند فلش", new Point(1138, 7), 82);

            chartToolbarPanel.Controls.Add(drawTrendLineButton);
            chartToolbarPanel.Controls.Add(drawHorizontalDoubleButton);
            chartToolbarPanel.Controls.Add(drawVerticalDoubleButton);
            chartToolbarPanel.Controls.Add(drawHorizontalRayButton);
            chartToolbarPanel.Controls.Add(drawTrendLineArrowButton);

            drawTrendLineButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLine, drawTrendLineButton);
            drawHorizontalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalDoubleArrow, drawHorizontalDoubleButton);
            drawVerticalDoubleButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.VerticalDoubleArrow, drawVerticalDoubleButton);
            drawHorizontalRayButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.HorizontalRay, drawHorizontalRayButton);
            drawTrendLineArrowButton.Click += (_, _) => ActivateDrawingTool(ChartDrawingTool.TrendLineWithArrow, drawTrendLineArrowButton);

            chartTabControl.SelectedIndexChanged += ChartDrawingTabChanged;
            ResetDrawingToolButtons();
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

        private void ChartDrawingTabChanged(object? sender, EventArgs e)
        {
            GetActiveChart()?.CancelDrawing();
            ResetDrawingToolButtons();
        }

        private void ResetDrawingToolButtons()
        {
            if (drawTrendLineButton != null)
                SetToggleButtonState(drawTrendLineButton, false);
            if (drawHorizontalDoubleButton != null)
                SetToggleButtonState(drawHorizontalDoubleButton, false);
            if (drawVerticalDoubleButton != null)
                SetToggleButtonState(drawVerticalDoubleButton, false);
            if (drawHorizontalRayButton != null)
                SetToggleButtonState(drawHorizontalRayButton, false);
            if (drawTrendLineArrowButton != null)
                SetToggleButtonState(drawTrendLineArrowButton, false);
        }
    }
}
