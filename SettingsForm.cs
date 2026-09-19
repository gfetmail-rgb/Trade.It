using System.Drawing.Drawing2D;

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
        private readonly List<CheckBox> fibonacciLevelCheckBoxes = new();
        private GroupBox? fibonacciLevelsGroupBox;

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

        public double ChartTopEmptyPercent
        {
            get => double.TryParse(chartTopEmptyPercentTextBox.Text.Trim(), out var value) ? value : 10.0;
            set => chartTopEmptyPercentTextBox.Text = value.ToString("0.##");
        }

        public int InitialVisibleCandleCount
        {
            get => (int)initialVisibleCandleCountNumeric.Value;
            set => initialVisibleCandleCountNumeric.Value = Math.Clamp(value, 10, 5000);
        }

        public SettingsForm(ChartDisplayMode currentMode, double chartRightEmptyPercent = 25.0, double chartTopEmptyPercent = 10.0, int initialVisibleCandleCount = 200)
        {
            InitializeComponent();
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ChartDisplayMode = currentMode;
            ChartRightEmptyPercent = chartRightEmptyPercent;
            ChartTopEmptyPercent = chartTopEmptyPercent;
            InitialVisibleCandleCount = initialVisibleCandleCount;
            LoadColorButtons();
            InitializeLineSettingsControls();
            InitializeFibonacciLevelControls();
            AttachColorEvents();
            AttachLineSettingsEvents();
            okButton.Click += okButton_Click;
            cancelButton.Click += cancelButton_Click;
        }

        private void InitializeFibonacciLevelControls()
        {
            fibonacciLevelsGroupBox = new GroupBox
            {
                Text = "سطوح فیبوناچی (مشترک برای هر دو فیبو)",
                RightToLeft = RightToLeft.Yes,
                Location = new Point(16, 702),
                Size = new Size(760, 160),
                TabStop = false
            };

            var levels = ChartAppearanceSettings.GetAllFibonacciLevels();
            for (var i = 0; i < levels.Count; i++)
            {
                var row = i / 5;
                var column = i % 5;
                var checkBox = new CheckBox
                {
                    AutoSize = true,
                    Text = levels[i].Text,
                    RightToLeft = RightToLeft.Yes,
                    Location = new Point(585 - column * 145, 35 + row * 42),
                    Size = new Size(120, 28),
                    Checked = (ChartAppearanceSettings.FibonacciLevelsMask & (1 << i)) != 0,
                    TabIndex = i
                };
                fibonacciLevelCheckBoxes.Add(checkBox);
                fibonacciLevelsGroupBox.Controls.Add(checkBox);
            }

            var selectAllButton = new Button
            {
                Text = "انتخاب همه",
                Location = new Point(585, 115),
                Size = new Size(80, 32)
            };
            var clearAllButton = new Button
            {
                Text = "حذف همه",
                Location = new Point(495, 115),
                Size = new Size(80, 32)
            };
            selectAllButton.Click += (_, _) => fibonacciLevelCheckBoxes.ForEach(x => x.Checked = true);
            clearAllButton.Click += (_, _) => fibonacciLevelCheckBoxes.ForEach(x => x.Checked = false);
            fibonacciLevelsGroupBox.Controls.Add(selectAllButton);
            fibonacciLevelsGroupBox.Controls.Add(clearAllButton);

            Controls.Add(fibonacciLevelsGroupBox);
            fibonacciLevelsGroupBox.BringToFront();

            crosshairGridGroupBox.Location = new Point(16, 872);
            okButton.Location = new Point(276, 1048);
            cancelButton.Location = new Point(389, 1048);
            ClientSize = new Size(792, 1107);
        }

        private int GetSelectedFibonacciLevelsMask()
        {
            var mask = 0;
            for (var i = 0; i < fibonacciLevelCheckBoxes.Count; i++)
            {
                if (fibonacciLevelCheckBoxes[i].Checked)
                    mask |= 1 << i;
            }
            return mask;
        }

        private void InitializeLineSettingsControls()
        {
            chartLineWidthNumeric.Value = (decimal)Math.Clamp(LineAppearanceSettings.ChartLineWidth, 0.5f, 8.0f);
            chartLineStyleCombo.SelectedIndex = StyleToIndex(LineAppearanceSettings.ChartLineStyle);
            drawingLineWidthNumeric.Value = (decimal)Math.Clamp(LineAppearanceSettings.DrawingLineWidth, 0.5f, 8.0f);
            drawingLineStyleCombo.SelectedIndex = StyleToIndex(LineAppearanceSettings.DrawingLineStyle);
            crosshairLineWidthNumeric.Value = (decimal)Math.Clamp(LineAppearanceSettings.CrosshairLineWidth, 0.5f, 8.0f);
            crosshairLineStyleCombo.SelectedIndex = StyleToIndex(LineAppearanceSettings.CrosshairLineStyle);
            gridLineWidthNumeric.Value = (decimal)Math.Clamp(LineAppearanceSettings.GridLineWidth, 0.5f, 8.0f);
            gridLineStyleCombo.SelectedIndex = StyleToIndex(LineAppearanceSettings.GridLineStyle);
        }

        private static int StyleToIndex(DashStyle style) => style switch
        {
            DashStyle.Solid => 0,
            DashStyle.Dash => 1,
            DashStyle.Dot => 2,
            DashStyle.DashDot => 3,
            DashStyle.DashDotDot => 4,
            _ => 0
        };

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
            foreach (var pair in drawingColorKeys) SetColorButton(pair.Key, GetDrawingColor(pair.Value));
        }

        private void AttachColorEvents()
        {
            risingColorButton.Click += (_, _) => PickColor(risingColorButton, ChartAppearanceSettings.RisingCandleColor);
            fallingColorButton.Click += (_, _) => PickColor(fallingColorButton, ChartAppearanceSettings.FallingCandleColor);
            lineColorButton.Click += (_, _) => PickColor(lineColorButton, ChartAppearanceSettings.LineChartColor);
            foreach (var pair in drawingColorKeys) pair.Key.Click += (_, _) => PickDrawingColor(pair.Key, pair.Value);
            resetChartColorsButton.Click += (_, _) => { ChartAppearanceSettings.ResetChartColors(); LoadColorButtons(); RefreshOwnerCharts(); };
            resetDrawingColorsButton.Click += (_, _) => { ChartAppearanceSettings.ResetDrawingColors(); LoadColorButtons(); RefreshOwnerCharts(); };
        }

        private void AttachLineSettingsEvents()
        {
            crosshairColorButton.Click += (_, _) => PickLineColor(crosshairColorButton, true);
            gridColorButton.Click += (_, _) => PickLineColor(gridColorButton, false);
            chartLineWidthNumeric.ValueChanged += (_, _) => ApplyLineSettingsFromControls();
            chartLineStyleCombo.SelectedIndexChanged += (_, _) => ApplyLineSettingsFromControls();
            drawingLineWidthNumeric.ValueChanged += (_, _) => ApplyLineSettingsFromControls();
            drawingLineStyleCombo.SelectedIndexChanged += (_, _) => ApplyLineSettingsFromControls();
            crosshairLineWidthNumeric.ValueChanged += (_, _) => ApplyLineSettingsFromControls();
            crosshairLineStyleCombo.SelectedIndexChanged += (_, _) => ApplyLineSettingsFromControls();
            gridLineWidthNumeric.ValueChanged += (_, _) => ApplyLineSettingsFromControls();
            gridLineStyleCombo.SelectedIndexChanged += (_, _) => ApplyLineSettingsFromControls();
        }

        private void ApplyLineSettingsFromControls()
        {
            if (chartLineWidthNumeric == null) return;
            LineAppearanceSettings.SetChartLine((float)chartLineWidthNumeric.Value, IndexToStyle(chartLineStyleCombo.SelectedIndex));
            LineAppearanceSettings.SetDrawingLine((float)drawingLineWidthNumeric.Value, IndexToStyle(drawingLineStyleCombo.SelectedIndex));
            LineAppearanceSettings.SetCrosshair(LineAppearanceSettings.CrosshairColor, (float)crosshairLineWidthNumeric.Value, IndexToStyle(crosshairLineStyleCombo.SelectedIndex));
            LineAppearanceSettings.SetGrid(LineAppearanceSettings.GridColor, (float)gridLineWidthNumeric.Value, IndexToStyle(gridLineStyleCombo.SelectedIndex));
            RefreshOwnerCharts();
        }

        private void PickLineColor(Button button, bool crosshair)
        {
            var current = crosshair ? LineAppearanceSettings.CrosshairColor : LineAppearanceSettings.GridColor;
            using var dialog = new ColorDialog { Color = current, FullOpen = true };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            SetColorButton(button, dialog.Color);
            if (crosshair) LineAppearanceSettings.SetCrosshair(dialog.Color, LineAppearanceSettings.CrosshairLineWidth, LineAppearanceSettings.CrosshairLineStyle);
            else LineAppearanceSettings.SetGrid(dialog.Color, LineAppearanceSettings.GridLineWidth, LineAppearanceSettings.GridLineStyle);
            RefreshOwnerCharts();
        }

        private void PickColor(Button button, Color current)
        {
            using var dialog = new ColorDialog { Color = current, FullOpen = true };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            SetColorButton(button, dialog.Color);
            if (button == risingColorButton) ChartAppearanceSettings.SetChartColors(dialog.Color, ChartAppearanceSettings.FallingCandleColor, ChartAppearanceSettings.LineChartColor);
            else if (button == fallingColorButton) ChartAppearanceSettings.SetChartColors(ChartAppearanceSettings.RisingCandleColor, dialog.Color, ChartAppearanceSettings.LineChartColor);
            else ChartAppearanceSettings.SetChartColors(ChartAppearanceSettings.RisingCandleColor, ChartAppearanceSettings.FallingCandleColor, dialog.Color);
            RefreshOwnerCharts();
        }

        private void PickDrawingColor(Button button, string key)
        {
            using var dialog = new ColorDialog { Color = GetDrawingColor(key), FullOpen = true };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            ChartAppearanceSettings.SetDrawingColor(key, dialog.Color);
            SetColorButton(button, dialog.Color);
            RefreshOwnerCharts();
        }

        private void RefreshOwnerCharts()\n        {\n            if (Owner is MainForm mainForm && !mainForm.IsDisposed) mainForm.Refresh();\n        }\n\n        private static void SetColorButton(Button button, Color color)
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

        private static DashStyle IndexToStyle(int index) => index switch
        {
            1 => DashStyle.Dash,
            2 => DashStyle.Dot,
            3 => DashStyle.DashDot,
            4 => DashStyle.DashDotDot,
            _ => DashStyle.Solid
        };

        private void okButton_Click(object? sender, EventArgs e)
        {
            if (ChartRightEmptyPercent < 0 || ChartRightEmptyPercent > 90)
            {
                MessageBox.Show(this, "درصد فضای خالی سمت راست باید بین ۰ تا ۹۰ باشد.", "تنظیمات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chartRightEmptyPercentTextBox.Focus(); chartRightEmptyPercentTextBox.SelectAll(); return;
            }
            if (ChartTopEmptyPercent < 0 || ChartTopEmptyPercent > 50)
            {
                MessageBox.Show(this, "درصد فضای خالی بالای نمودار باید بین ۰ تا ۵۰ باشد.", "تنظیمات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chartTopEmptyPercentTextBox.Focus(); chartTopEmptyPercentTextBox.SelectAll(); return;
            }
            ApplyLineSettingsFromControls();
            ChartAppearanceSettings.SetChartRightEmptyPercent(ChartRightEmptyPercent);
            ChartAppearanceSettings.SetChartTopEmptyPercent(ChartTopEmptyPercent);
            ChartAppearanceSettings.SetInitialVisibleCandleCount(InitialVisibleCandleCount);
            var fibonacciMask = GetSelectedFibonacciLevelsMask();
            if (fibonacciMask == 0)
            {
                MessageBox.Show(this, "حداقل یک سطح فیبوناچی را انتخاب کنید.", "تنظیمات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ChartAppearanceSettings.SetFibonacciLevelsMask(fibonacciMask);
            var selectedLevels = string.Join(", ", ChartAppearanceSettings.GetEnabledFibonacciLevels().Select(x => x.Text));
            MessageBox.Show(this, $"ماسک فیبوناچی: {ChartAppearanceSettings.FibonacciLevelsMask}\nسطوح فعال: {selectedLevels}", "تشخیص تنظیمات فیبوناچی", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChartAppearanceSettings.Save();
            LineAppearanceSettings.Save();
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