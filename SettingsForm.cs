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
        private NumericUpDown chartLineWidthNumeric = null!;
        private ComboBox chartLineStyleCombo = null!;
        private NumericUpDown drawingLineWidthNumeric = null!;
        private ComboBox drawingLineStyleCombo = null!;
        private Button crosshairColorButton = null!;
        private NumericUpDown crosshairLineWidthNumeric = null!;
        private ComboBox crosshairLineStyleCombo = null!;
        private Button gridColorButton = null!;
        private NumericUpDown gridLineWidthNumeric = null!;
        private ComboBox gridLineStyleCombo = null!;

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
            CreateLineSettingsControls();
            okButton.Click += okButton_Click;
            cancelButton.Click += cancelButton_Click;
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
                RefreshOwnerCharts();
            };

            resetDrawingColorsButton.Click += (_, _) =>
            {
                ChartAppearanceSettings.ResetDrawingColors();
                LoadColorButtons();
                RefreshOwnerCharts();
            };
        }

        private void CreateLineSettingsControls()
        {
            chartColorsGroupBox.Text = "رنگ، ضخامت و استایل نمودار";
            drawingColorsGroupBox.Text = "رنگ، ضخامت و استایل شکل‌ها و ابزارهای رسم";

            chartColorsGroupBox.Height = 145;
            drawingColorsGroupBox.Location = new Point(16, 381);
            drawingColorsGroupBox.Height = 250;

            AddLineSettingLabel(chartColorsGroupBox, "ضخامت:", 470, 78);
            chartLineWidthNumeric = CreateWidthControl(chartColorsGroupBox, 395, 75, LineAppearanceSettings.ChartLineWidth);
            AddLineSettingLabel(chartColorsGroupBox, "استایل:", 250, 78);
            chartLineStyleCombo = CreateStyleControl(chartColorsGroupBox, 155, 75, LineAppearanceSettings.ChartLineStyle);

            AddLineSettingLabel(drawingColorsGroupBox, "ضخامت:", 470, 205);
            drawingLineWidthNumeric = CreateWidthControl(drawingColorsGroupBox, 395, 202, LineAppearanceSettings.DrawingLineWidth);
            AddLineSettingLabel(drawingColorsGroupBox, "استایل:", 250, 205);
            drawingLineStyleCombo = CreateStyleControl(drawingColorsGroupBox, 155, 202, LineAppearanceSettings.DrawingLineStyle);

            var crosshairGroup = new GroupBox
            {
                Name = "crosshairGridGroupBox",
                Text = "رنگ، ضخامت و استایل کراس و گرید",
                Location = new Point(16, 641),
                Size = new Size(660, 160),
                RightToLeft = RightToLeft.Yes,
                TabStop = false
            };
            Controls.Add(crosshairGroup);

            crosshairColorButton = new Button { Text = "رنگ کراس", Size = new Size(130, 34), Location = new Point(500, 32), UseVisualStyleBackColor = false };
            SetColorButton(crosshairColorButton, LineAppearanceSettings.CrosshairColor);
            crosshairColorButton.Click += (_, _) => PickLineColor(crosshairColorButton, true);
            crosshairGroup.Controls.Add(crosshairColorButton);
            AddLineSettingLabel(crosshairGroup, "ضخامت:", 390, 39);
            crosshairLineWidthNumeric = CreateWidthControl(crosshairGroup, 315, 36, LineAppearanceSettings.CrosshairLineWidth);
            AddLineSettingLabel(crosshairGroup, "استایل:", 205, 39);
            crosshairLineStyleCombo = CreateStyleControl(crosshairGroup, 105, 36, LineAppearanceSettings.CrosshairLineStyle);

            gridColorButton = new Button { Text = "رنگ گرید", Size = new Size(130, 34), Location = new Point(500, 92), UseVisualStyleBackColor = false };
            SetColorButton(gridColorButton, LineAppearanceSettings.GridColor);
            gridColorButton.Click += (_, _) => PickLineColor(gridColorButton, false);
            crosshairGroup.Controls.Add(gridColorButton);
            AddLineSettingLabel(crosshairGroup, "ضخامت:", 390, 99);
            gridLineWidthNumeric = CreateWidthControl(crosshairGroup, 315, 96, LineAppearanceSettings.GridLineWidth);
            AddLineSettingLabel(crosshairGroup, "استایل:", 205, 99);
            gridLineStyleCombo = CreateStyleControl(crosshairGroup, 105, 96, LineAppearanceSettings.GridLineStyle);

            foreach (var control in new Control[] { chartLineWidthNumeric, chartLineStyleCombo, drawingLineWidthNumeric, drawingLineStyleCombo, crosshairLineWidthNumeric, crosshairLineStyleCombo, gridLineWidthNumeric, gridLineStyleCombo })
            {
                if (control is NumericUpDown numeric)
                    numeric.ValueChanged += (_, _) => ApplyLineSettingsFromControls();
                else if (control is ComboBox combo)
                    combo.SelectedIndexChanged += (_, _) => ApplyLineSettingsFromControls();
            }

            okButton.Location = new Point(500, 815);
            cancelButton.Location = new Point(590, 815);
            ClientSize = new Size(692, 865);
        }

        private static void AddLineSettingLabel(Control parent, string text, int x, int y)
        {
            parent.Controls.Add(new Label
            {
                AutoSize = true,
                Text = text,
                Location = new Point(x, y),
                RightToLeft = RightToLeft.Yes
            });
        }

        private static NumericUpDown CreateWidthControl(Control parent, int x, int y, float value)
        {
            var control = new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(70, 27),
                Minimum = 0.5m,
                Maximum = 8.0m,
                Increment = 0.1m,
                DecimalPlaces = 1,
                Value = (decimal)Math.Clamp(value, 0.5f, 8.0f),
                TextAlign = HorizontalAlignment.Center
            };
            parent.Controls.Add(control);
            return control;
        }

        private static ComboBox CreateStyleControl(Control parent, int x, int y, DashStyle style)
        {
            var combo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(120, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                RightToLeft = RightToLeft.Yes
            };
            combo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            combo.SelectedIndex = StyleToIndex(style);
            parent.Controls.Add(combo);
            return combo;
        }

        private static int StyleToIndex(DashStyle style) => style switch
        {
            DashStyle.Dash => 1,
            DashStyle.Dot => 2,
            DashStyle.DashDot => 3,
            DashStyle.DashDotDot => 4,
            _ => 0
        };

        private static DashStyle IndexToStyle(int index) => index switch
        {
            1 => DashStyle.Dash,
            2 => DashStyle.Dot,
            3 => DashStyle.DashDot,
            4 => DashStyle.DashDotDot,
            _ => DashStyle.Solid
        };

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
            if (crosshair)
                LineAppearanceSettings.SetCrosshair(dialog.Color, LineAppearanceSettings.CrosshairLineWidth, LineAppearanceSettings.CrosshairLineStyle);
            else
                LineAppearanceSettings.SetGrid(dialog.Color, LineAppearanceSettings.GridLineWidth, LineAppearanceSettings.GridLineStyle);
            RefreshOwnerCharts();
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

                RefreshOwnerCharts();
            }
        }

        private void PickDrawingColor(Button button, string key)
        {
            using var dialog = new ColorDialog { Color = GetDrawingColor(key), FullOpen = true };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ChartAppearanceSettings.SetDrawingColor(key, dialog.Color);
                SetColorButton(button, dialog.Color);
                RefreshOwnerCharts();
            }
        }

        private void RefreshOwnerCharts()
        {
            if (Owner is MainForm mainForm && !mainForm.IsDisposed)
                mainForm.Refresh();
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

            ApplyLineSettingsFromControls();
            ChartAppearanceSettings.SetChartRightEmptyPercent(ChartRightEmptyPercent);
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
