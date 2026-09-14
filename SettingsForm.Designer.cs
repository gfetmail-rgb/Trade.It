namespace Trade.It
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox chartDisplayGroupBox;
        private System.Windows.Forms.RadioButton separateTabsRadioButton;
        private System.Windows.Forms.RadioButton singleTabRadioButton;
        private System.Windows.Forms.GroupBox chartMarginGroupBox;
        private System.Windows.Forms.Label chartRightEmptyPercentLabel;
        private System.Windows.Forms.TextBox chartRightEmptyPercentTextBox;
        private System.Windows.Forms.Label chartRightEmptyPercentHintLabel;
        private System.Windows.Forms.GroupBox chartColorsGroupBox;
        private System.Windows.Forms.Button risingColorButton;
        private System.Windows.Forms.Button fallingColorButton;
        private System.Windows.Forms.Button lineColorButton;
        private System.Windows.Forms.Button resetChartColorsButton;
        private System.Windows.Forms.NumericUpDown chartLineWidthNumeric;
        private System.Windows.Forms.ComboBox chartLineStyleCombo;
        private System.Windows.Forms.Label chartLineWidthLabel;
        private System.Windows.Forms.Label chartLineStyleLabel;
        private System.Windows.Forms.GroupBox drawingColorsGroupBox;
        private System.Windows.Forms.Button trendLineColorButton;
        private System.Windows.Forms.Button trendChannelColorButton;
        private System.Windows.Forms.Button horizontalDoubleArrowColorButton;
        private System.Windows.Forms.Button verticalDoubleArrowColorButton;
        private System.Windows.Forms.Button horizontalRayColorButton;
        private System.Windows.Forms.Button trendLineWithArrowColorButton;
        private System.Windows.Forms.Button rectangleColorButton;
        private System.Windows.Forms.Button fibonacciRetracementColorButton;
        private System.Windows.Forms.Button textLabelColorButton;
        private System.Windows.Forms.Button pitchforkColorButton;
        private System.Windows.Forms.Button fibonacciExtensionColorButton;
        private System.Windows.Forms.Button measureColorButton;
        private System.Windows.Forms.Button resetDrawingColorsButton;
        private System.Windows.Forms.NumericUpDown drawingLineWidthNumeric;
        private System.Windows.Forms.ComboBox drawingLineStyleCombo;
        private System.Windows.Forms.Label drawingLineWidthLabel;
        private System.Windows.Forms.Label drawingLineStyleLabel;
        private System.Windows.Forms.GroupBox crosshairGridGroupBox;
        private System.Windows.Forms.Button crosshairColorButton;
        private System.Windows.Forms.NumericUpDown crosshairLineWidthNumeric;
        private System.Windows.Forms.ComboBox crosshairLineStyleCombo;
        private System.Windows.Forms.Label crosshairLineWidthLabel;
        private System.Windows.Forms.Label crosshairLineStyleLabel;
        private System.Windows.Forms.Button gridColorButton;
        private System.Windows.Forms.NumericUpDown gridLineWidthNumeric;
        private System.Windows.Forms.ComboBox gridLineStyleCombo;
        private System.Windows.Forms.Label gridLineWidthLabel;
        private System.Windows.Forms.Label gridLineStyleLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            chartDisplayGroupBox = new GroupBox();
            separateTabsRadioButton = new RadioButton();
            singleTabRadioButton = new RadioButton();
            chartMarginGroupBox = new GroupBox();
            chartRightEmptyPercentLabel = new Label();
            chartRightEmptyPercentTextBox = new TextBox();
            chartRightEmptyPercentHintLabel = new Label();
            chartColorsGroupBox = new GroupBox();
            risingColorButton = new Button();
            fallingColorButton = new Button();
            lineColorButton = new Button();
            resetChartColorsButton = new Button();
            chartLineWidthNumeric = new NumericUpDown();
            chartLineStyleCombo = new ComboBox();
            chartLineWidthLabel = new Label();
            chartLineStyleLabel = new Label();
            drawingColorsGroupBox = new GroupBox();
            trendLineColorButton = new Button();
            trendChannelColorButton = new Button();
            horizontalDoubleArrowColorButton = new Button();
            verticalDoubleArrowColorButton = new Button();
            horizontalRayColorButton = new Button();
            trendLineWithArrowColorButton = new Button();
            rectangleColorButton = new Button();
            fibonacciRetracementColorButton = new Button();
            textLabelColorButton = new Button();
            pitchforkColorButton = new Button();
            fibonacciExtensionColorButton = new Button();
            measureColorButton = new Button();
            resetDrawingColorsButton = new Button();
            drawingLineWidthNumeric = new NumericUpDown();
            drawingLineStyleCombo = new ComboBox();
            drawingLineWidthLabel = new Label();
            drawingLineStyleLabel = new Label();
            crosshairGridGroupBox = new GroupBox();
            crosshairColorButton = new Button();
            crosshairLineWidthLabel = new Label();
            crosshairLineWidthNumeric = new NumericUpDown();
            crosshairLineStyleLabel = new Label();
            crosshairLineStyleCombo = new ComboBox();
            gridColorButton = new Button();
            gridLineWidthLabel = new Label();
            gridLineWidthNumeric = new NumericUpDown();
            gridLineStyleLabel = new Label();
            gridLineStyleCombo = new ComboBox();
            okButton = new Button();
            cancelButton = new Button();
            chartDisplayGroupBox.SuspendLayout();
            chartMarginGroupBox.SuspendLayout();
            chartColorsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).BeginInit();
            drawingColorsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).BeginInit();
            crosshairGridGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).BeginInit();
            SuspendLayout();
            // 
            // chartDisplayGroupBox
            // 
            chartDisplayGroupBox.Controls.Add(separateTabsRadioButton);
            chartDisplayGroupBox.Controls.Add(singleTabRadioButton);
            chartDisplayGroupBox.Location = new Point(16, 16);
            chartDisplayGroupBox.Name = "chartDisplayGroupBox";
            chartDisplayGroupBox.RightToLeft = RightToLeft.Yes;
            chartDisplayGroupBox.Size = new Size(760, 82);
            chartDisplayGroupBox.TabIndex = 6;
            chartDisplayGroupBox.TabStop = false;
            chartDisplayGroupBox.Text = "نحوه نمایش چارت‌ها";
            // 
            // separateTabsRadioButton
            // 
            separateTabsRadioButton.AutoSize = true;
            separateTabsRadioButton.Location = new Point(475, 34);
            separateTabsRadioButton.Name = "separateTabsRadioButton";
            separateTabsRadioButton.RightToLeft = RightToLeft.Yes;
            separateTabsRadioButton.Size = new Size(243, 29);
            separateTabsRadioButton.TabIndex = 0;
            separateTabsRadioButton.Text = "هر چارت در یک تب جداگانه";
            // 
            // singleTabRadioButton
            // 
            singleTabRadioButton.AutoSize = true;
            singleTabRadioButton.Location = new Point(205, 34);
            singleTabRadioButton.Name = "singleTabRadioButton";
            singleTabRadioButton.RightToLeft = RightToLeft.Yes;
            singleTabRadioButton.Size = new Size(253, 29);
            singleTabRadioButton.TabIndex = 1;
            singleTabRadioButton.Text = "همه چارت‌ها در یک تب واحد";
            // 
            // chartMarginGroupBox
            // 
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentLabel);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentTextBox);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentHintLabel);
            chartMarginGroupBox.Location = new Point(16, 108);
            chartMarginGroupBox.Name = "chartMarginGroupBox";
            chartMarginGroupBox.RightToLeft = RightToLeft.Yes;
            chartMarginGroupBox.Size = new Size(760, 88);
            chartMarginGroupBox.TabIndex = 5;
            chartMarginGroupBox.TabStop = false;
            chartMarginGroupBox.Text = "حاشیه خالی سمت راست چارت";
            // 
            // chartRightEmptyPercentLabel
            // 
            chartRightEmptyPercentLabel.AutoSize = true;
            chartRightEmptyPercentLabel.Location = new Point(500, 32);
            chartRightEmptyPercentLabel.Name = "chartRightEmptyPercentLabel";
            chartRightEmptyPercentLabel.Size = new Size(255, 25);
            chartRightEmptyPercentLabel.TabIndex = 0;
            chartRightEmptyPercentLabel.Text = "درصد فضای خالی سمت راست:";
            // 
            // chartRightEmptyPercentTextBox
            // 
            chartRightEmptyPercentTextBox.Location = new Point(404, 29);
            chartRightEmptyPercentTextBox.Name = "chartRightEmptyPercentTextBox";
            chartRightEmptyPercentTextBox.Size = new Size(90, 33);
            chartRightEmptyPercentTextBox.TabIndex = 1;
            chartRightEmptyPercentTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // chartRightEmptyPercentHintLabel
            // 
            chartRightEmptyPercentHintLabel.AutoSize = true;
            chartRightEmptyPercentHintLabel.Location = new Point(70, 61);
            chartRightEmptyPercentHintLabel.Name = "chartRightEmptyPercentHintLabel";
            chartRightEmptyPercentHintLabel.Size = new Size(467, 25);
            chartRightEmptyPercentHintLabel.TabIndex = 2;
            chartRightEmptyPercentHintLabel.Text = "۰ تا ۹۰ درصد؛ مثلاً 25 یعنی یک‌چهارم عرض چارت خالی بماند.";
            // 
            // chartColorsGroupBox
            // 
            chartColorsGroupBox.Controls.Add(risingColorButton);
            chartColorsGroupBox.Controls.Add(fallingColorButton);
            chartColorsGroupBox.Controls.Add(lineColorButton);
            chartColorsGroupBox.Controls.Add(resetChartColorsButton);
            chartColorsGroupBox.Controls.Add(chartLineWidthNumeric);
            chartColorsGroupBox.Controls.Add(chartLineStyleCombo);
            chartColorsGroupBox.Controls.Add(chartLineWidthLabel);
            chartColorsGroupBox.Controls.Add(chartLineStyleLabel);
            chartColorsGroupBox.Location = new Point(16, 206);
            chartColorsGroupBox.Name = "chartColorsGroupBox";
            chartColorsGroupBox.RightToLeft = RightToLeft.Yes;
            chartColorsGroupBox.Size = new Size(760, 128);
            chartColorsGroupBox.TabIndex = 4;
            chartColorsGroupBox.TabStop = false;
            chartColorsGroupBox.Text = "رنگ، ضخامت و استایل نمودار";
            // 
            // risingColorButton
            // 
            risingColorButton.Location = new Point(570, 28);
            risingColorButton.Name = "risingColorButton";
            risingColorButton.Size = new Size(175, 50);
            risingColorButton.TabIndex = 0;
            risingColorButton.Text = "رنگ کندل و میله صعودی";
            risingColorButton.UseVisualStyleBackColor = false;
            // 
            // fallingColorButton
            // 
            fallingColorButton.Location = new Point(380, 28);
            fallingColorButton.Name = "fallingColorButton";
            fallingColorButton.Size = new Size(175, 50);
            fallingColorButton.TabIndex = 1;
            fallingColorButton.Text = "رنگ کندل و میله نزولی";
            fallingColorButton.UseVisualStyleBackColor = false;
            // 
            // lineColorButton
            // 
            lineColorButton.Location = new Point(190, 28);
            lineColorButton.Name = "lineColorButton";
            lineColorButton.Size = new Size(175, 50);
            lineColorButton.TabIndex = 2;
            lineColorButton.Text = "رنگ چارت خطی";
            lineColorButton.UseVisualStyleBackColor = false;
            // 
            // resetChartColorsButton
            // 
            resetChartColorsButton.Location = new Point(20, 22);
            resetChartColorsButton.Name = "resetChartColorsButton";
            resetChartColorsButton.Size = new Size(145, 46);
            resetChartColorsButton.TabIndex = 3;
            resetChartColorsButton.Text = "بازنشانی ";
            resetChartColorsButton.UseVisualStyleBackColor = true;
            // 
            // chartLineWidthNumeric
            // 
            chartLineWidthNumeric.DecimalPlaces = 1;
            chartLineWidthNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            chartLineWidthNumeric.Location = new Point(505, 84);
            chartLineWidthNumeric.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            chartLineWidthNumeric.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            chartLineWidthNumeric.Name = "chartLineWidthNumeric";
            chartLineWidthNumeric.Size = new Size(70, 33);
            chartLineWidthNumeric.TabIndex = 4;
            chartLineWidthNumeric.TextAlign = HorizontalAlignment.Center;
            chartLineWidthNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chartLineStyleCombo
            // 
            chartLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            chartLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            chartLineStyleCombo.Location = new Point(250, 84);
            chartLineStyleCombo.Name = "chartLineStyleCombo";
            chartLineStyleCombo.RightToLeft = RightToLeft.Yes;
            chartLineStyleCombo.Size = new Size(130, 33);
            chartLineStyleCombo.TabIndex = 5;
            // 
            // chartLineWidthLabel
            // 
            chartLineWidthLabel.AutoSize = true;
            chartLineWidthLabel.Location = new Point(590, 88);
            chartLineWidthLabel.Name = "chartLineWidthLabel";
            chartLineWidthLabel.Size = new Size(79, 25);
            chartLineWidthLabel.TabIndex = 6;
            chartLineWidthLabel.Text = "ضخامت:";
            // 
            // chartLineStyleLabel
            // 
            chartLineStyleLabel.AutoSize = true;
            chartLineStyleLabel.Location = new Point(395, 88);
            chartLineStyleLabel.Name = "chartLineStyleLabel";
            chartLineStyleLabel.Size = new Size(67, 25);
            chartLineStyleLabel.TabIndex = 7;
            chartLineStyleLabel.Text = "استایل:";
            // 
            // drawingColorsGroupBox
            // 
            drawingColorsGroupBox.Controls.Add(trendLineColorButton);
            drawingColorsGroupBox.Controls.Add(trendChannelColorButton);
            drawingColorsGroupBox.Controls.Add(horizontalDoubleArrowColorButton);
            drawingColorsGroupBox.Controls.Add(verticalDoubleArrowColorButton);
            drawingColorsGroupBox.Controls.Add(horizontalRayColorButton);
            drawingColorsGroupBox.Controls.Add(trendLineWithArrowColorButton);
            drawingColorsGroupBox.Controls.Add(rectangleColorButton);
            drawingColorsGroupBox.Controls.Add(fibonacciRetracementColorButton);
            drawingColorsGroupBox.Controls.Add(textLabelColorButton);
            drawingColorsGroupBox.Controls.Add(pitchforkColorButton);
            drawingColorsGroupBox.Controls.Add(fibonacciExtensionColorButton);
            drawingColorsGroupBox.Controls.Add(measureColorButton);
            drawingColorsGroupBox.Controls.Add(resetDrawingColorsButton);
            drawingColorsGroupBox.Controls.Add(drawingLineWidthNumeric);
            drawingColorsGroupBox.Controls.Add(drawingLineStyleCombo);
            drawingColorsGroupBox.Controls.Add(drawingLineWidthLabel);
            drawingColorsGroupBox.Controls.Add(drawingLineStyleLabel);
            drawingColorsGroupBox.Location = new Point(16, 344);
            drawingColorsGroupBox.Name = "drawingColorsGroupBox";
            drawingColorsGroupBox.RightToLeft = RightToLeft.Yes;
            drawingColorsGroupBox.Size = new Size(760, 286);
            drawingColorsGroupBox.TabIndex = 3;
            drawingColorsGroupBox.TabStop = false;
            drawingColorsGroupBox.Text = "رنگ، ضخامت و استایل شکل‌ها و ابزارهای رسم";
            // 
            // trendLineColorButton
            // 
            trendLineColorButton.Location = new Point(625, 51);
            trendLineColorButton.Name = "trendLineColorButton";
            trendLineColorButton.Size = new Size(115, 45);
            trendLineColorButton.TabIndex = 0;
            trendLineColorButton.Text = "خط روند";
            // 
            // trendChannelColorButton
            // 
            trendChannelColorButton.Location = new Point(504, 51);
            trendChannelColorButton.Name = "trendChannelColorButton";
            trendChannelColorButton.Size = new Size(115, 45);
            trendChannelColorButton.TabIndex = 1;
            trendChannelColorButton.Text = "کانال";
            // 
            // horizontalDoubleArrowColorButton
            // 
            horizontalDoubleArrowColorButton.Location = new Point(383, 51);
            horizontalDoubleArrowColorButton.Name = "horizontalDoubleArrowColorButton";
            horizontalDoubleArrowColorButton.Size = new Size(115, 45);
            horizontalDoubleArrowColorButton.TabIndex = 2;
            horizontalDoubleArrowColorButton.Text = "خط افق";
            // 
            // verticalDoubleArrowColorButton
            // 
            verticalDoubleArrowColorButton.Location = new Point(264, 51);
            verticalDoubleArrowColorButton.Name = "verticalDoubleArrowColorButton";
            verticalDoubleArrowColorButton.Size = new Size(115, 45);
            verticalDoubleArrowColorButton.TabIndex = 3;
            verticalDoubleArrowColorButton.Text = "خط عمود";
            // 
            // horizontalRayColorButton
            // 
            horizontalRayColorButton.Location = new Point(143, 51);
            horizontalRayColorButton.Name = "horizontalRayColorButton";
            horizontalRayColorButton.Size = new Size(115, 45);
            horizontalRayColorButton.TabIndex = 4;
            horizontalRayColorButton.Text = "نیم خط";
            // 
            // trendLineWithArrowColorButton
            // 
            trendLineWithArrowColorButton.Location = new Point(20, 51);
            trendLineWithArrowColorButton.Name = "trendLineWithArrowColorButton";
            trendLineWithArrowColorButton.Size = new Size(115, 45);
            trendLineWithArrowColorButton.TabIndex = 5;
            trendLineWithArrowColorButton.Text = "فلش";
            // 
            // rectangleColorButton
            // 
            rectangleColorButton.Location = new Point(625, 112);
            rectangleColorButton.Name = "rectangleColorButton";
            rectangleColorButton.Size = new Size(115, 46);
            rectangleColorButton.TabIndex = 6;
            rectangleColorButton.Text = "مستطیل";
            // 
            // fibonacciRetracementColorButton
            // 
            fibonacciRetracementColorButton.Location = new Point(504, 112);
            fibonacciRetracementColorButton.Name = "fibonacciRetracementColorButton";
            fibonacciRetracementColorButton.Size = new Size(115, 47);
            fibonacciRetracementColorButton.TabIndex = 7;
            fibonacciRetracementColorButton.Text = " R فیبو";
            // 
            // textLabelColorButton
            // 
            textLabelColorButton.Location = new Point(383, 111);
            textLabelColorButton.Name = "textLabelColorButton";
            textLabelColorButton.Size = new Size(115, 47);
            textLabelColorButton.TabIndex = 8;
            textLabelColorButton.Text = "متن";
            // 
            // pitchforkColorButton
            // 
            pitchforkColorButton.Location = new Point(264, 111);
            pitchforkColorButton.Name = "pitchforkColorButton";
            pitchforkColorButton.Size = new Size(116, 47);
            pitchforkColorButton.TabIndex = 9;
            pitchforkColorButton.Text = "چنگال";
            // 
            // fibonacciExtensionColorButton
            // 
            fibonacciExtensionColorButton.Location = new Point(143, 111);
            fibonacciExtensionColorButton.Name = "fibonacciExtensionColorButton";
            fibonacciExtensionColorButton.Size = new Size(115, 47);
            fibonacciExtensionColorButton.TabIndex = 10;
            fibonacciExtensionColorButton.Text = "X فیبو";
            // 
            // measureColorButton
            // 
            measureColorButton.Location = new Point(20, 111);
            measureColorButton.Name = "measureColorButton";
            measureColorButton.Size = new Size(115, 47);
            measureColorButton.TabIndex = 11;
            measureColorButton.Text = "خط کش";
            // 
            // resetDrawingColorsButton
            // 
            resetDrawingColorsButton.Location = new Point(39, 198);
            resetDrawingColorsButton.Name = "resetDrawingColorsButton";
            resetDrawingColorsButton.Size = new Size(145, 54);
            resetDrawingColorsButton.TabIndex = 12;
            resetDrawingColorsButton.Text = "بازنشانی";
            resetDrawingColorsButton.UseVisualStyleBackColor = true;
            // 
            // drawingLineWidthNumeric
            // 
            drawingLineWidthNumeric.DecimalPlaces = 1;
            drawingLineWidthNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            drawingLineWidthNumeric.Location = new Point(529, 209);
            drawingLineWidthNumeric.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            drawingLineWidthNumeric.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            drawingLineWidthNumeric.Name = "drawingLineWidthNumeric";
            drawingLineWidthNumeric.Size = new Size(70, 33);
            drawingLineWidthNumeric.TabIndex = 13;
            drawingLineWidthNumeric.TextAlign = HorizontalAlignment.Center;
            drawingLineWidthNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // drawingLineStyleCombo
            // 
            drawingLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            drawingLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            drawingLineStyleCombo.Location = new Point(274, 209);
            drawingLineStyleCombo.Name = "drawingLineStyleCombo";
            drawingLineStyleCombo.RightToLeft = RightToLeft.Yes;
            drawingLineStyleCombo.Size = new Size(130, 33);
            drawingLineStyleCombo.TabIndex = 14;
            // 
            // drawingLineWidthLabel
            // 
            drawingLineWidthLabel.AutoSize = true;
            drawingLineWidthLabel.Location = new Point(614, 213);
            drawingLineWidthLabel.Name = "drawingLineWidthLabel";
            drawingLineWidthLabel.Size = new Size(79, 25);
            drawingLineWidthLabel.TabIndex = 15;
            drawingLineWidthLabel.Text = "ضخامت:";
            // 
            // drawingLineStyleLabel
            // 
            drawingLineStyleLabel.AutoSize = true;
            drawingLineStyleLabel.Location = new Point(419, 213);
            drawingLineStyleLabel.Name = "drawingLineStyleLabel";
            drawingLineStyleLabel.Size = new Size(67, 25);
            drawingLineStyleLabel.TabIndex = 16;
            drawingLineStyleLabel.Text = "استایل:";
            // 
            // crosshairGridGroupBox
            // 
            crosshairGridGroupBox.Controls.Add(crosshairColorButton);
            crosshairGridGroupBox.Controls.Add(crosshairLineWidthLabel);
            crosshairGridGroupBox.Controls.Add(crosshairLineWidthNumeric);
            crosshairGridGroupBox.Controls.Add(crosshairLineStyleLabel);
            crosshairGridGroupBox.Controls.Add(crosshairLineStyleCombo);
            crosshairGridGroupBox.Controls.Add(gridColorButton);
            crosshairGridGroupBox.Controls.Add(gridLineWidthLabel);
            crosshairGridGroupBox.Controls.Add(gridLineWidthNumeric);
            crosshairGridGroupBox.Controls.Add(gridLineStyleLabel);
            crosshairGridGroupBox.Controls.Add(gridLineStyleCombo);
            crosshairGridGroupBox.Location = new Point(16, 640);
            crosshairGridGroupBox.Name = "crosshairGridGroupBox";
            crosshairGridGroupBox.RightToLeft = RightToLeft.Yes;
            crosshairGridGroupBox.Size = new Size(760, 160);
            crosshairGridGroupBox.TabIndex = 2;
            crosshairGridGroupBox.TabStop = false;
            crosshairGridGroupBox.Text = "رنگ، ضخامت و استایل کراس و گرید";
            // 
            // crosshairColorButton
            // 
            crosshairColorButton.Location = new Point(570, 28);
            crosshairColorButton.Name = "crosshairColorButton";
            crosshairColorButton.Size = new Size(175, 34);
            crosshairColorButton.TabIndex = 0;
            crosshairColorButton.Text = "رنگ کراس";
            crosshairColorButton.UseVisualStyleBackColor = false;
            // 
            // crosshairLineWidthLabel
            // 
            crosshairLineWidthLabel.AutoSize = true;
            crosshairLineWidthLabel.Location = new Point(432, 33);
            crosshairLineWidthLabel.Name = "crosshairLineWidthLabel";
            crosshairLineWidthLabel.Size = new Size(127, 25);
            crosshairLineWidthLabel.TabIndex = 1;
            crosshairLineWidthLabel.Text = "ضخامت کراس:";
            // 
            // crosshairLineWidthNumeric
            // 
            crosshairLineWidthNumeric.DecimalPlaces = 1;
            crosshairLineWidthNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            crosshairLineWidthNumeric.Location = new Point(347, 29);
            crosshairLineWidthNumeric.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            crosshairLineWidthNumeric.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            crosshairLineWidthNumeric.Name = "crosshairLineWidthNumeric";
            crosshairLineWidthNumeric.Size = new Size(70, 33);
            crosshairLineWidthNumeric.TabIndex = 2;
            crosshairLineWidthNumeric.TextAlign = HorizontalAlignment.Center;
            crosshairLineWidthNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // crosshairLineStyleLabel
            // 
            crosshairLineStyleLabel.AutoSize = true;
            crosshairLineStyleLabel.Location = new Point(242, 33);
            crosshairLineStyleLabel.Name = "crosshairLineStyleLabel";
            crosshairLineStyleLabel.Size = new Size(115, 25);
            crosshairLineStyleLabel.TabIndex = 3;
            crosshairLineStyleLabel.Text = "استایل کراس:";
            // 
            // crosshairLineStyleCombo
            // 
            crosshairLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            crosshairLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            crosshairLineStyleCombo.Location = new Point(97, 29);
            crosshairLineStyleCombo.Name = "crosshairLineStyleCombo";
            crosshairLineStyleCombo.RightToLeft = RightToLeft.Yes;
            crosshairLineStyleCombo.Size = new Size(130, 33);
            crosshairLineStyleCombo.TabIndex = 4;
            // 
            // gridColorButton
            // 
            gridColorButton.Location = new Point(570, 88);
            gridColorButton.Name = "gridColorButton";
            gridColorButton.Size = new Size(175, 34);
            gridColorButton.TabIndex = 5;
            gridColorButton.Text = "رنگ گرید";
            gridColorButton.UseVisualStyleBackColor = false;
            // 
            // gridLineWidthLabel
            // 
            gridLineWidthLabel.AutoSize = true;
            gridLineWidthLabel.Location = new Point(435, 92);
            gridLineWidthLabel.Name = "gridLineWidthLabel";
            gridLineWidthLabel.Size = new Size(120, 25);
            gridLineWidthLabel.TabIndex = 6;
            gridLineWidthLabel.Text = "ضخامت گرید:";
            // 
            // gridLineWidthNumeric
            // 
            gridLineWidthNumeric.DecimalPlaces = 1;
            gridLineWidthNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            gridLineWidthNumeric.Location = new Point(350, 88);
            gridLineWidthNumeric.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            gridLineWidthNumeric.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            gridLineWidthNumeric.Name = "gridLineWidthNumeric";
            gridLineWidthNumeric.Size = new Size(70, 33);
            gridLineWidthNumeric.TabIndex = 7;
            gridLineWidthNumeric.TextAlign = HorizontalAlignment.Center;
            gridLineWidthNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // gridLineStyleLabel
            // 
            gridLineStyleLabel.AutoSize = true;
            gridLineStyleLabel.Location = new Point(245, 92);
            gridLineStyleLabel.Name = "gridLineStyleLabel";
            gridLineStyleLabel.Size = new Size(108, 25);
            gridLineStyleLabel.TabIndex = 8;
            gridLineStyleLabel.Text = "استایل گرید:";
            // 
            // gridLineStyleCombo
            // 
            gridLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            gridLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            gridLineStyleCombo.Location = new Point(100, 88);
            gridLineStyleCombo.Name = "gridLineStyleCombo";
            gridLineStyleCombo.RightToLeft = RightToLeft.Yes;
            gridLineStyleCombo.Size = new Size(130, 33);
            gridLineStyleCombo.TabIndex = 9;
            // 
            // okButton
            // 
            okButton.Location = new Point(276, 816);
            okButton.Name = "okButton";
            okButton.Size = new Size(98, 43);
            okButton.TabIndex = 0;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(389, 816);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(98, 43);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(792, 875);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(crosshairGridGroupBox);
            Controls.Add(drawingColorsGroupBox);
            Controls.Add(chartColorsGroupBox);
            Controls.Add(chartMarginGroupBox);
            Controls.Add(chartDisplayGroupBox);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات";
            chartDisplayGroupBox.ResumeLayout(false);
            chartDisplayGroupBox.PerformLayout();
            chartMarginGroupBox.ResumeLayout(false);
            chartMarginGroupBox.PerformLayout();
            chartColorsGroupBox.ResumeLayout(false);
            chartColorsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).EndInit();
            drawingColorsGroupBox.ResumeLayout(false);
            drawingColorsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).EndInit();
            crosshairGridGroupBox.ResumeLayout(false);
            crosshairGridGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).EndInit();
            ResumeLayout(false);
        }

        private static void ConfigureDrawingButton(Button button, string text, int x, int y)
        {
            button.Location = new Point(x, y);
            button.Name = button.Name;
            button.Size = new Size(175, 36);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
        }
    }
}
