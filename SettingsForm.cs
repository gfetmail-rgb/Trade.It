namespace Trade.It
{
    public enum ChartDisplayMode
    {
        SeparateTabs,
        SingleTab
    }

    public partial class SettingsForm : Form
    {
        private readonly Dictionary<Button, string> drawingColorKeys = new();

        public ChartDisplayMode ChartDisplayMode
        {
            get => separateTabsRadioButton.Checked ? ChartDisplayMode.SeparateTabs : ChartDisplayMode.SingleTab;
            set
            {
                separateTabsRadioButton.Checked = value == ChartDisplayMode.SeparateTabs;
                singleTabRadioButton.Checked = value == ChartDisplayMode.SingleTab;
            }
        }

        public double ChartRightEmptyPercent
        {
            get => double.TryParse(chartRightEmptyPercentTextBox.Text.Trim(), out var value) ? value : 25.0;
            set => chartRightEmptyPercentTextBox.Text = value.ToString("0.##");
        }

        public SettingsForm(ChartDisplayMode currentMode, double chartRightEmptyPercent = 25.0)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ChartDisplayMode = currentMode;
            ChartRightEmptyPercent = chartRightEmptyPercent;
            LoadColorButtons();
            AttachColorEvents();
        }

        private void LoadColorButtons()
        {
            SetColorButton(risingColorButton, ChartAppearanceSettings.RisingCandleColor);
            SetColorButton(fallingColorButton, ChartAppearanceSettings.FallingCandleColor);
            SetColorButton(lineColorButton, ChartAppearanceSettings.LineChartColor);

            drawingColorKeys.Clear();
            drawingColorKeys[trendLineColorButton] = nameof(ChartAppearanceSettings.TrendLineColor);
            drawingColorKeys[trendChannelColorButton] = nameof(ChartAppearanceSettings.TrendChannelColor);
            drawingColorKeys[horizontalDoubleArrowColorButton] = nameof(ChartAppearanceSettings.HorizontalDoubleArrowColor);
            drawingColorKeys[verticalDoubleArrowColorButton] = nameof(ChartAppearanceSettings.VerticalDoubleArrowColor);
            drawingColorKeys[horizontalRayColorButton] = nameof(ChartAppearanceSettings.HorizontalRayColor);
            drawingColorKeys[trendLineWithArrowColorButton] = nameof(ChartAppearanceSettings.TrendLineWithArrowColor);
            drawingColorKeys[rectangleColorButton] = nameof(ChartAppearanceSettings.RectangleColor);
            drawingColorKeys[fibonacciRetracementColorButton] = nameof(ChartAppearanceSettings.FibonacciRetracementColor);
            drawingColorKeys[textLabelColorButton] = nameof(ChartAppearanceSettings.TextLabelColor);
            drawingColorKeys[pitchforkColorButton] = nameof(ChartAppearanceSettings.PitchforkColor);
            drawingColorKeys[fibonacciExtensionColorButton] = nameof(ChartAppearanceSettings.FibonacciExtensionColor);
            drawingColorKeys[measureColorButton] = nameof(ChartAppearanceSettings.MeasureColor);

            foreach (var pair in drawingColorKeys)
                SetColorButton(pair.Key, GetDrawingColor(pair.Value));
        }

        private void AttachColorEvents()
        {
            risingColorButton.Click += (_, _) => PickColor(risingColorButton, ChartAppearanceSettings.RisingCandleColor);
            fallingColorButton.Click += (_, _) => PickColor(fallingColorButton, ChartAppearanceSettings.FallingCandleColor);
            lineColorButton.Click += (_, _) => PickColor(lineColorButton, ChartAppearanceSettings.LineChartColor);

            foreach (var pair in drawingColorKeys)
                pair.Key.Click += (_, _) => PickDrawingColor(pair.Key, pair.Value);

            resetChartColorsButton.Click += (_, _) =>
            {
                ChartAppearanceSettings.ResetChartColors();
                LoadColorButtons();
            };

            resetDrawingColorsButton.Click += (_, _) =>
            {
                ChartAppearanceSettings.ResetDrawingColors();
                LoadColorButtons();
            };
        }

        private void PickColor(Button button, Color current)
        {
            using var dialog = new ColorDialog { Color = current, FullOpen = true };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SetColorButton(button, dialog.Color);
                if (button == risingColorButton)
                    ChartAppearanceSettings.SetChartColors(dialog.Color, ChartAppearanceSettings.FallingCandleColor, ChartAppearanceSettings.LineChartColor);
                else if (button == fallingColorButton)
                    ChartAppearanceSettings.SetChartColors(ChartAppearanceSettings.RisingCandleColor, dialog.Color, ChartAppearanceSettings.LineChartColor);
                else
                    ChartAppearanceSettings.SetChartColors(ChartAppearanceSettings.RisingCandleColor, ChartAppearanceSettings.FallingCandleColor, dialog.Color);
            }
        }

        private void PickDrawingColor(Button button, string key)
        {
            using var dialog = new ColorDialog { Color = GetDrawingColor(key), FullOpen = true };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ChartAppearanceSettings.SetDrawingColor(key, dialog.Color);
                SetColorButton(button, dialog.Color);
            }
        }

        private static void SetColorButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
        }

        private static Color GetDrawingColor(string key) => key switch
        {
            nameof(ChartAppearanceSettings.TrendLineColor) => ChartAppearanceSettings.TrendLineColor,
            nameof(ChartAppearanceSettings.TrendChannelColor) => ChartAppearanceSettings.TrendChannelColor,
            nameof(ChartAppearanceSettings.HorizontalDoubleArrowColor) => ChartAppearanceSettings.HorizontalDoubleArrowColor,
            nameof(ChartAppearanceSettings.VerticalDoubleArrowColor) => ChartAppearanceSettings.VerticalDoubleArrowColor,
            nameof(ChartAppearanceSettings.HorizontalRayColor) => ChartAppearanceSettings.HorizontalRayColor,
            nameof(ChartAppearanceSettings.TrendLineWithArrowColor) => ChartAppearanceSettings.TrendLineWithArrowColor,
            nameof(ChartAppearanceSettings.RectangleColor) => ChartAppearanceSettings.RectangleColor,
            nameof(ChartAppearanceSettings.FibonacciRetracementColor) => ChartAppearanceSettings.FibonacciRetracementColor,
            nameof(ChartAppearanceSettings.TextLabelColor) => ChartAppearanceSettings.TextLabelColor,
            nameof(ChartAppearanceSettings.PitchforkColor) => ChartAppearanceSettings.PitchforkColor,
            nameof(ChartAppearanceSettings.FibonacciExtensionColor) => ChartAppearanceSettings.FibonacciExtensionColor,
            nameof(ChartAppearanceSettings.MeasureColor) => ChartAppearanceSettings.MeasureColor,
            _ => Color.Black
        };

        private void okButton_Click(object? sender, EventArgs e)
        {
            if (ChartRightEmptyPercent < 0 || ChartRightEmptyPercent > 90)
            {
                MessageBox.Show(this, "درصد فضای خالی سمت راست باید بین ۰ تا ۹۰ باشد.", "تنظیمات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chartRightEmptyPercentTextBox.Focus();
                chartRightEmptyPercentTextBox.SelectAll();
                return;
            }

            ChartAppearanceSettings.SetChartRightEmptyPercent(ChartRightEmptyPercent);
            ChartAppearanceSettings.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
