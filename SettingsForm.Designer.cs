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
            components = new System.ComponentModel.Container();

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
            crosshairLineWidthNumeric = new NumericUpDown();
            crosshairLineStyleCombo = new ComboBox();
            crosshairLineWidthLabel = new Label();
            crosshairLineStyleLabel = new Label();
            gridColorButton = new Button();
            gridLineWidthNumeric = new NumericUpDown();
            gridLineStyleCombo = new ComboBox();
            gridLineWidthLabel = new Label();
            gridLineStyleLabel = new Label();

            okButton = new Button();
            cancelButton = new Button();

            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).BeginInit();
            SuspendLayout();

            // chartDisplayGroupBox
            chartDisplayGroupBox.Location = new Point(16, 16);
            chartDisplayGroupBox.Name = "chartDisplayGroupBox";
            chartDisplayGroupBox.RightToLeft = RightToLeft.Yes;
            chartDisplayGroupBox.Size = new Size(760, 82);
            chartDisplayGroupBox.TabStop = false;
            chartDisplayGroupBox.Text = "نحوه نمایش چارت‌ها";
            chartDisplayGroupBox.Controls.Add(separateTabsRadioButton);
            chartDisplayGroupBox.Controls.Add(singleTabRadioButton);

            separateTabsRadioButton.AutoSize = true;
            separateTabsRadioButton.Location = new Point(475, 34);
            separateTabsRadioButton.Name = "separateTabsRadioButton";
            separateTabsRadioButton.RightToLeft = RightToLeft.Yes;
            separateTabsRadioButton.Text = "هر چارت در یک تب جداگانه";

            singleTabRadioButton.AutoSize = true;
            singleTabRadioButton.Location = new Point(205, 34);
            singleTabRadioButton.Name = "singleTabRadioButton";
            singleTabRadioButton.RightToLeft = RightToLeft.Yes;
            singleTabRadioButton.Text = "همه چارت‌ها در یک تب واحد";

            // chartMarginGroupBox
            chartMarginGroupBox.Location = new Point(16, 108);
            chartMarginGroupBox.Name = "chartMarginGroupBox";
            chartMarginGroupBox.RightToLeft = RightToLeft.Yes;
            chartMarginGroupBox.Size = new Size(760, 88);
            chartMarginGroupBox.TabStop = false;
            chartMarginGroupBox.Text = "حاشیه خالی سمت راست چارت";
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentLabel);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentTextBox);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentHintLabel);

            chartRightEmptyPercentLabel.AutoSize = true;
            chartRightEmptyPercentLabel.Location = new Point(500, 32);
            chartRightEmptyPercentLabel.Name = "chartRightEmptyPercentLabel";
            chartRightEmptyPercentLabel.Text = "درصد فضای خالی سمت راست:";

            chartRightEmptyPercentTextBox.Location = new Point(390, 28);
            chartRightEmptyPercentTextBox.Name = "chartRightEmptyPercentTextBox";
            chartRightEmptyPercentTextBox.Size = new Size(90, 27);
            chartRightEmptyPercentTextBox.TextAlign = HorizontalAlignment.Center;

            chartRightEmptyPercentHintLabel.AutoSize = true;
            chartRightEmptyPercentHintLabel.Location = new Point(70, 61);
            chartRightEmptyPercentHintLabel.Name = "chartRightEmptyPercentHintLabel";
            chartRightEmptyPercentHintLabel.Text = "۰ تا ۹۰ درصد؛ مثلاً 25 یعنی یک‌چهارم عرض چارت خالی بماند.";

            // chartColorsGroupBox
            chartColorsGroupBox.Location = new Point(16, 206);
            chartColorsGroupBox.Name = "chartColorsGroupBox";
            chartColorsGroupBox.RightToLeft = RightToLeft.Yes;
            chartColorsGroupBox.Size = new Size(760, 128);
            chartColorsGroupBox.TabStop = false;
            chartColorsGroupBox.Text = "رنگ، ضخامت و استایل نمودار";
            chartColorsGroupBox.Controls.Add(risingColorButton);
            chartColorsGroupBox.Controls.Add(fallingColorButton);
            chartColorsGroupBox.Controls.Add(lineColorButton);
            chartColorsGroupBox.Controls.Add(resetChartColorsButton);
            chartColorsGroupBox.Controls.Add(chartLineWidthNumeric);
            chartColorsGroupBox.Controls.Add(chartLineStyleCombo);
            chartColorsGroupBox.Controls.Add(chartLineWidthLabel);
            chartColorsGroupBox.Controls.Add(chartLineStyleLabel);

            risingColorButton.Location = new Point(570, 28);
            risingColorButton.Name = "risingColorButton";
            risingColorButton.Size = new Size(175, 34);
            risingColorButton.Text = "رنگ کندل و میله صعودی";
            risingColorButton.BackColor = Color.FromArgb(35, 150, 80);
            risingColorButton.ForeColor = Color.White;
            risingColorButton.UseVisualStyleBackColor = false;

            fallingColorButton.Location = new Point(380, 28);
            fallingColorButton.Name = "fallingColorButton";
            fallingColorButton.Size = new Size(175, 34);
            fallingColorButton.Text = "رنگ کندل و میله نزولی";
            fallingColorButton.BackColor = Color.FromArgb(205, 70, 70);
            fallingColorButton.ForeColor = Color.White;
            fallingColorButton.UseVisualStyleBackColor = false;

            lineColorButton.Location = new Point(190, 28);
            lineColorButton.Name = "lineColorButton";
            lineColorButton.Size = new Size(175, 34);
            lineColorButton.Text = "رنگ چارت خطی";
            lineColorButton.BackColor = Color.FromArgb(35, 90, 160);
            lineColorButton.ForeColor = Color.White;
            lineColorButton.UseVisualStyleBackColor = false;

            resetChartColorsButton.Location = new Point(20, 28);
            resetChartColorsButton.Name = "resetChartColorsButton";
            resetChartColorsButton.Size = new Size(145, 34);
            resetChartColorsButton.Text = "بازنشانی رنگ‌های نمودار";
            resetChartColorsButton.UseVisualStyleBackColor = true;

            chartLineWidthLabel.AutoSize = true;
            chartLineWidthLabel.Location = new Point(590, 83);
            chartLineWidthLabel.Name = "chartLineWidthLabel";
            chartLineWidthLabel.Text = "ضخامت:";

            chartLineWidthNumeric.Location = new Point(505, 79);
            chartLineWidthNumeric.Name = "chartLineWidthNumeric";
            chartLineWidthNumeric.Size = new Size(70, 27);
            chartLineWidthNumeric.Minimum = 0.5m;
            chartLineWidthNumeric.Maximum = 8m;
            chartLineWidthNumeric.Increment = 0.1m;
            chartLineWidthNumeric.DecimalPlaces = 1;
            chartLineWidthNumeric.Value = 1m;
            chartLineWidthNumeric.TextAlign = HorizontalAlignment.Center;

            chartLineStyleLabel.AutoSize = true;
            chartLineStyleLabel.Location = new Point(395, 83);
            chartLineStyleLabel.Name = "chartLineStyleLabel";
            chartLineStyleLabel.Text = "استایل:";

            chartLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            chartLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            chartLineStyleCombo.Location = new Point(250, 79);
            chartLineStyleCombo.Name = "chartLineStyleCombo";
            chartLineStyleCombo.RightToLeft = RightToLeft.Yes;
            chartLineStyleCombo.Size = new Size(130, 28);
            chartLineStyleCombo.SelectedIndex = 0;

            // drawingColorsGroupBox
            // Keep this container LTR so the explicit designer coordinates are identical
            // in the Visual Studio designer and at runtime. Individual controls remain RTL.
            drawingColorsGroupBox.Location = new Point(16, 344);
            drawingColorsGroupBox.Name = "drawingColorsGroupBox";
            drawingColorsGroupBox.RightToLeft = RightToLeft.No;
            drawingColorsGroupBox.Size = new Size(760, 286);
            drawingColorsGroupBox.TabStop = false;
            drawingColorsGroupBox.Text = "رنگ، ضخامت و استایل شکل‌ها و ابزارهای رسم";
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

            ConfigureDrawingButton(trendLineColorButton, "خط روند", 565, 30, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(trendChannelColorButton, "کانال روند", 375, 30, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(horizontalDoubleArrowColorButton, "خط افقی دو سر", 185, 30, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(verticalDoubleArrowColorButton, "خط عمودی دو سر", 0, 30, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(horizontalRayColorButton, "نیم‌خط افقی", 565, 76, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(trendLineWithArrowColorButton, "خط روند با فلش", 375, 76, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(rectangleColorButton, "مستطیل", 185, 76, Color.FromArgb(30, 90, 160), Color.White);
            ConfigureDrawingButton(fibonacciRetracementColorButton, "فیبوناچی اصلاحی", 0, 76, Color.FromArgb(45, 105, 170), Color.White);
            ConfigureDrawingButton(textLabelColorButton, "برچسب متن", 565, 122, Color.FromArgb(45, 105, 170), Color.White);
            ConfigureDrawingButton(pitchforkColorButton, "چنگال", 375, 122, Color.FromArgb(155, 80, 45), Color.White);
            ConfigureDrawingButton(fibonacciExtensionColorButton, "فیبوناچی اکستنشن", 185, 122, Color.FromArgb(155, 80, 45), Color.White);
            ConfigureDrawingButton(measureColorButton, "اندازه‌گیری", 0, 122, Color.FromArgb(155, 80, 45), Color.White);

            resetDrawingColorsButton.Location = new Point(565, 168);
            resetDrawingColorsButton.Name = "resetDrawingColorsButton";
            resetDrawingColorsButton.Size = new Size(175, 34);
            resetDrawingColorsButton.Text = "بازنشانی رنگ‌های ابزارها";
            resetDrawingColorsButton.UseVisualStyleBackColor = true;

            drawingLineWidthLabel.AutoSize = true;
            drawingLineWidthLabel.Location = new Point(495, 219);
            drawingLineWidthLabel.Name = "drawingLineWidthLabel";
            drawingLineWidthLabel.Text = "ضخامت:";

            drawingLineWidthNumeric.Location = new Point(410, 215);
            drawingLineWidthNumeric.Name = "drawingLineWidthNumeric";
            drawingLineWidthNumeric.Size = new Size(70, 27);
            drawingLineWidthNumeric.Minimum = 0.5m;
            drawingLineWidthNumeric.Maximum = 8m;
            drawingLineWidthNumeric.Increment = 0.1m;
            drawingLineWidthNumeric.DecimalPlaces = 1;
            drawingLineWidthNumeric.Value = 1m;
            drawingLineWidthNumeric.TextAlign = HorizontalAlignment.Center;

            drawingLineStyleLabel.AutoSize = true;
            drawingLineStyleLabel.Location = new Point(300, 219);
            drawingLineStyleLabel.Name = "drawingLineStyleLabel";
            drawingLineStyleLabel.Text = "استایل:";

            drawingLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            drawingLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            drawingLineStyleCombo.Location = new Point(155, 215);
            drawingLineStyleCombo.Name = "drawingLineStyleCombo";
            drawingLineStyleCombo.RightToLeft = RightToLeft.Yes;
            drawingLineStyleCombo.Size = new Size(130, 28);
            drawingLineStyleCombo.SelectedIndex = 0;

            // crosshairGridGroupBox
            crosshairGridGroupBox.Location = new Point(16, 640);
            crosshairGridGroupBox.Name = "crosshairGridGroupBox";
            crosshairGridGroupBox.RightToLeft = RightToLeft.No;
            crosshairGridGroupBox.Size = new Size(760, 160);
            crosshairGridGroupBox.TabStop = false;
            crosshairGridGroupBox.Text = "رنگ، ضخامت و استایل کراس و گرید";
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

            crosshairColorButton.Location = new Point(570, 28);
            crosshairColorButton.Name = "crosshairColorButton";
            crosshairColorButton.Size = new Size(175, 34);
            crosshairColorButton.Text = "رنگ کراس";
            crosshairColorButton.BackColor = Color.FromArgb(120, 120, 120);
            crosshairColorButton.ForeColor = Color.White;
            crosshairColorButton.RightToLeft = RightToLeft.Yes;
            crosshairColorButton.UseVisualStyleBackColor = false;

            crosshairLineWidthLabel.AutoSize = true;
            crosshairLineWidthLabel.Location = new Point(490, 80);
            crosshairLineWidthLabel.Name = "crosshairLineWidthLabel";
            crosshairLineWidthLabel.Text = "ضخامت کراس:";

            crosshairLineWidthNumeric.Location = new Point(405, 76);
            crosshairLineWidthNumeric.Name = "crosshairLineWidthNumeric";
            crosshairLineWidthNumeric.Size = new Size(70, 27);
            crosshairLineWidthNumeric.Minimum = 0.5m;
            crosshairLineWidthNumeric.Maximum = 8m;
            crosshairLineWidthNumeric.Increment = 0.1m;
            crosshairLineWidthNumeric.DecimalPlaces = 1;
            crosshairLineWidthNumeric.Value = 1m;
            crosshairLineWidthNumeric.TextAlign = HorizontalAlignment.Center;

            crosshairLineStyleLabel.AutoSize = true;
            crosshairLineStyleLabel.Location = new Point(300, 80);
            crosshairLineStyleLabel.Name = "crosshairLineStyleLabel";
            crosshairLineStyleLabel.Text = "استایل کراس:";

            crosshairLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            crosshairLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            crosshairLineStyleCombo.Location = new Point(155, 76);
            crosshairLineStyleCombo.Name = "crosshairLineStyleCombo";
            crosshairLineStyleCombo.RightToLeft = RightToLeft.Yes;
            crosshairLineStyleCombo.Size = new Size(130, 28);
            crosshairLineStyleCombo.SelectedIndex = 0;

            gridColorButton.Location = new Point(570, 112);
            gridColorButton.Name = "gridColorButton";
            gridColorButton.Size = new Size(175, 34);
            gridColorButton.Text = "رنگ گرید";
            gridColorButton.BackColor = Color.FromArgb(225, 225, 225);
            gridColorButton.ForeColor = Color.Black;
            gridColorButton.RightToLeft = RightToLeft.Yes;
            gridColorButton.UseVisualStyleBackColor = false;

            gridLineWidthLabel.AutoSize = true;
            gridLineWidthLabel.Location = new Point(490, 118);
            gridLineWidthLabel.Name = "gridLineWidthLabel";
            gridLineWidthLabel.Text = "ضخامت گرید:";

            gridLineWidthNumeric.Location = new Point(405, 114);
            gridLineWidthNumeric.Name = "gridLineWidthNumeric";
            gridLineWidthNumeric.Size = new Size(70, 27);
            gridLineWidthNumeric.Minimum = 0.5m;
            gridLineWidthNumeric.Maximum = 8m;
            gridLineWidthNumeric.Increment = 0.1m;
            gridLineWidthNumeric.DecimalPlaces = 1;
            gridLineWidthNumeric.Value = 1m;
            gridLineWidthNumeric.TextAlign = HorizontalAlignment.Center;

            gridLineStyleLabel.AutoSize = true;
            gridLineStyleLabel.Location = new Point(300, 118);
            gridLineStyleLabel.Name = "gridLineStyleLabel";
            gridLineStyleLabel.Text = "استایل گرید:";

            gridLineStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            gridLineStyleCombo.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            gridLineStyleCombo.Location = new Point(155, 114);
            gridLineStyleCombo.Name = "gridLineStyleCombo";
            gridLineStyleCombo.RightToLeft = RightToLeft.Yes;
            gridLineStyleCombo.Size = new Size(130, 28);
            gridLineStyleCombo.SelectedIndex = 0;

            // bottom buttons
            okButton.Location = new Point(610, 820);
            okButton.Name = "okButton";
            okButton.Size = new Size(80, 34);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            cancelButton.Location = new Point(520, 820);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 34);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            // SettingsForm
            AcceptButton = okButton;
            CancelButton = cancelButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 875);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات";

            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(crosshairGridGroupBox);
            Controls.Add(drawingColorsGroupBox);
            Controls.Add(chartColorsGroupBox);
            Controls.Add(chartMarginGroupBox);
            Controls.Add(chartDisplayGroupBox);

            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).EndInit();
            ResumeLayout(false);
        }

        private static void ConfigureDrawingButton(Button button, string text, int x, int y, Color backColor, Color foreColor)
        {
            button.Location = new Point(x, y);
            button.Size = new Size(175, 36);
            button.Name = button.Name;
            button.Text = text;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.RightToLeft = RightToLeft.Yes;
            button.UseVisualStyleBackColor = false;
        }
    }
}