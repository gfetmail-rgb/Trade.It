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
        private System.Windows.Forms.GroupBox drawingColorsGroupBox;
        private System.Windows.Forms.FlowLayoutPanel drawingColorsPanel;
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
        private System.Windows.Forms.GroupBox crosshairGridGroupBox;
        private System.Windows.Forms.Button crosshairColorButton;
        private System.Windows.Forms.NumericUpDown chartLineWidthNumeric;
        private System.Windows.Forms.ComboBox chartLineStyleCombo;
        private System.Windows.Forms.NumericUpDown drawingLineWidthNumeric;
        private System.Windows.Forms.ComboBox drawingLineStyleCombo;
        private System.Windows.Forms.NumericUpDown crosshairLineWidthNumeric;
        private System.Windows.Forms.ComboBox crosshairLineStyleCombo;
        private System.Windows.Forms.Button gridColorButton;
        private System.Windows.Forms.NumericUpDown gridLineWidthNumeric;
        private System.Windows.Forms.ComboBox gridLineStyleCombo;
        private System.Windows.Forms.Label chartLineWidthLabel;
        private System.Windows.Forms.Label chartLineStyleLabel;
        private System.Windows.Forms.Label drawingLineWidthLabel;
        private System.Windows.Forms.Label drawingLineStyleLabel;
        private System.Windows.Forms.Label crosshairLineWidthLabel;
        private System.Windows.Forms.Label crosshairLineStyleLabel;
        private System.Windows.Forms.Label gridLineWidthLabel;
        private System.Windows.Forms.Label gridLineStyleLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
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
            drawingColorsGroupBox = new GroupBox();
            drawingColorsPanel = new FlowLayoutPanel();
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
            crosshairGridGroupBox = new GroupBox();
            crosshairColorButton = new Button();
            chartLineWidthNumeric = new NumericUpDown();
            chartLineStyleCombo = new ComboBox();
            drawingLineWidthNumeric = new NumericUpDown();
            drawingLineStyleCombo = new ComboBox();
            crosshairLineWidthNumeric = new NumericUpDown();
            crosshairLineStyleCombo = new ComboBox();
            gridColorButton = new Button();
            gridLineWidthNumeric = new NumericUpDown();
            gridLineStyleCombo = new ComboBox();
            chartLineWidthLabel = new Label();
            chartLineStyleLabel = new Label();
            drawingLineWidthLabel = new Label();
            drawingLineStyleLabel = new Label();
            crosshairLineWidthLabel = new Label();
            crosshairLineStyleLabel = new Label();
            gridLineWidthLabel = new Label();
            gridLineStyleLabel = new Label();
            okButton = new Button();
            cancelButton = new Button();

            chartDisplayGroupBox.SuspendLayout();
            chartMarginGroupBox.SuspendLayout();
            chartColorsGroupBox.SuspendLayout();
            drawingColorsGroupBox.SuspendLayout();
            drawingColorsPanel.SuspendLayout();
            crosshairGridGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).BeginInit();
            SuspendLayout();

            // chartDisplayGroupBox
            chartDisplayGroupBox.Controls.Add(singleTabRadioButton);
            chartDisplayGroupBox.Controls.Add(separateTabsRadioButton);
            chartDisplayGroupBox.Location = new Point(16, 16);
            chartDisplayGroupBox.Name = "chartDisplayGroupBox";
            chartDisplayGroupBox.RightToLeft = RightToLeft.Yes;
            chartDisplayGroupBox.Size = new Size(660, 90);
            chartDisplayGroupBox.TabStop = false;
            chartDisplayGroupBox.Text = "نحوه نمایش چارت‌ها";
            separateTabsRadioButton.AutoSize = true;
            separateTabsRadioButton.Location = new Point(330, 34);
            separateTabsRadioButton.Text = "هر چارت در یک تب جداگانه";
            separateTabsRadioButton.RightToLeft = RightToLeft.Yes;
            singleTabRadioButton.AutoSize = true;
            singleTabRadioButton.Location = new Point(20, 34);
            singleTabRadioButton.Text = "همه چارت‌ها در یک تب واحد";
            singleTabRadioButton.RightToLeft = RightToLeft.Yes;

            // chartMarginGroupBox
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentHintLabel);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentTextBox);
            chartMarginGroupBox.Controls.Add(chartRightEmptyPercentLabel);
            chartMarginGroupBox.Location = new Point(16, 116);
            chartMarginGroupBox.Name = "chartMarginGroupBox";
            chartMarginGroupBox.RightToLeft = RightToLeft.Yes;
            chartMarginGroupBox.Size = new Size(660, 100);
            chartMarginGroupBox.TabStop = false;
            chartMarginGroupBox.Text = "حاشیه خالی سمت راست چارت";
            chartRightEmptyPercentLabel.AutoSize = true;
            chartRightEmptyPercentLabel.Location = new Point(420, 32);
            chartRightEmptyPercentLabel.Text = "درصد فضای خالی سمت راست:";
            chartRightEmptyPercentTextBox.Location = new Point(320, 29);
            chartRightEmptyPercentTextBox.Size = new Size(80, 27);
            chartRightEmptyPercentTextBox.TextAlign = HorizontalAlignment.Center;
            chartRightEmptyPercentHintLabel.AutoSize = true;
            chartRightEmptyPercentHintLabel.Location = new Point(150, 65);
            chartRightEmptyPercentHintLabel.Text = "۰ تا ۹۰ درصد؛ مثلاً 25 یعنی یک‌چهارم عرض چارت خالی بماند.";

            // chartColorsGroupBox
            chartColorsGroupBox.Controls.Add(risingColorButton);
            chartColorsGroupBox.Controls.Add(fallingColorButton);
            chartColorsGroupBox.Controls.Add(lineColorButton);
            chartColorsGroupBox.Controls.Add(chartLineWidthLabel);
            chartColorsGroupBox.Controls.Add(chartLineWidthNumeric);
            chartColorsGroupBox.Controls.Add(chartLineStyleLabel);
            chartColorsGroupBox.Controls.Add(chartLineStyleCombo);
            chartColorsGroupBox.Controls.Add(resetChartColorsButton);
            chartColorsGroupBox.Location = new Point(16, 226);
            chartColorsGroupBox.Name = "chartColorsGroupBox";
            chartColorsGroupBox.RightToLeft = RightToLeft.Yes;
            chartColorsGroupBox.Size = new Size(660, 145);
            chartColorsGroupBox.TabStop = false;
            chartColorsGroupBox.Text = "رنگ، ضخامت و استایل نمودار";
            ConfigureColorButton(risingColorButton, "رنگ کندل و میله صعودی", 470, 30);
            ConfigureColorButton(fallingColorButton, "رنگ کندل و میله نزولی", 280, 30);
            ConfigureColorButton(lineColorButton, "رنگ چارت خطی", 90, 30);
            resetChartColorsButton.Location = new Point(16, 30);
            resetChartColorsButton.Size = new Size(65, 32);
            resetChartColorsButton.Text = "پیش‌فرض";
            resetChartColorsButton.UseVisualStyleBackColor = true;
            ConfigureWidthControl(chartLineWidthNumeric, 395, 75, "chartLineWidthNumeric");
            ConfigureStyleControl(chartLineStyleCombo, 155, 75, "chartLineStyleCombo");
            chartLineWidthLabel.AutoSize = true;
            chartLineWidthLabel.Location = new Point(470, 78);
            chartLineWidthLabel.Text = "ضخامت:";
            chartLineStyleLabel.AutoSize = true;
            chartLineStyleLabel.Location = new Point(250, 78);
            chartLineStyleLabel.Text = "استایل:";

            // drawingColorsGroupBox
            drawingColorsGroupBox.Controls.Add(drawingColorsPanel);
            drawingColorsGroupBox.Controls.Add(drawingLineWidthLabel);
            drawingColorsGroupBox.Controls.Add(drawingLineWidthNumeric);
            drawingColorsGroupBox.Controls.Add(drawingLineStyleLabel);
            drawingColorsGroupBox.Controls.Add(drawingLineStyleCombo);
            drawingColorsGroupBox.Controls.Add(resetDrawingColorsButton);
            drawingColorsGroupBox.Location = new Point(16, 381);
            drawingColorsGroupBox.Name = "drawingColorsGroupBox";
            drawingColorsGroupBox.RightToLeft = RightToLeft.Yes;
            drawingColorsGroupBox.Size = new Size(660, 250);
            drawingColorsGroupBox.TabStop = false;
            drawingColorsGroupBox.Text = "رنگ، ضخامت و استایل شکل‌ها و ابزارهای رسم";
            drawingColorsPanel.Location = new Point(15, 28);
            drawingColorsPanel.Size = new Size(555, 155);
            drawingColorsPanel.FlowDirection = FlowDirection.RightToLeft;
            drawingColorsPanel.WrapContents = true;
            drawingColorsPanel.RightToLeft = RightToLeft.Yes;
            drawingColorsPanel.AutoScroll = true;
            AddDrawingColorButton(trendLineColorButton, "خط روند");
            AddDrawingColorButton(trendChannelColorButton, "کانال روند");
            AddDrawingColorButton(horizontalDoubleArrowColorButton, "خط افقی دو سر");
            AddDrawingColorButton(verticalDoubleArrowColorButton, "خط عمودی دو سر");
            AddDrawingColorButton(horizontalRayColorButton, "نیم‌خط افقی");
            AddDrawingColorButton(trendLineWithArrowColorButton, "خط روند با فلش");
            AddDrawingColorButton(rectangleColorButton, "مستطیل");
            AddDrawingColorButton(fibonacciRetracementColorButton, "فیبوناچی اصلاحی");
            AddDrawingColorButton(textLabelColorButton, "برچسب متن");
            AddDrawingColorButton(pitchforkColorButton, "چنگال");
            AddDrawingColorButton(fibonacciExtensionColorButton, "فیبوناچی اکستنشن");
            AddDrawingColorButton(measureColorButton, "اندازه‌گیری");
            resetDrawingColorsButton.Location = new Point(575, 28);
            resetDrawingColorsButton.Size = new Size(70, 32);
            resetDrawingColorsButton.Text = "پیش‌فرض";
            resetDrawingColorsButton.UseVisualStyleBackColor = true;
            ConfigureWidthControl(drawingLineWidthNumeric, 395, 202, "drawingLineWidthNumeric");
            ConfigureStyleControl(drawingLineStyleCombo, 155, 202, "drawingLineStyleCombo");
            drawingLineWidthLabel.AutoSize = true;
            drawingLineWidthLabel.Location = new Point(470, 205);
            drawingLineWidthLabel.Text = "ضخامت:";
            drawingLineStyleLabel.AutoSize = true;
            drawingLineStyleLabel.Location = new Point(250, 205);
            drawingLineStyleLabel.Text = "استایل:";

            // crosshairGridGroupBox
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
            crosshairGridGroupBox.Location = new Point(16, 641);
            crosshairGridGroupBox.Name = "crosshairGridGroupBox";
            crosshairGridGroupBox.RightToLeft = RightToLeft.Yes;
            crosshairGridGroupBox.Size = new Size(660, 160);
            crosshairGridGroupBox.TabStop = false;
            crosshairGridGroupBox.Text = "رنگ، ضخامت و استایل کراس و گرید";
            ConfigureColorButton(crosshairColorButton, "رنگ کراس", 500, 32);
            ConfigureWidthControl(crosshairLineWidthNumeric, 315, 36, "crosshairLineWidthNumeric");
            ConfigureStyleControl(crosshairLineStyleCombo, 105, 36, "crosshairLineStyleCombo");
            crosshairLineWidthLabel.AutoSize = true;
            crosshairLineWidthLabel.Location = new Point(390, 39);
            crosshairLineWidthLabel.Text = "ضخامت:";
            crosshairLineStyleLabel.AutoSize = true;
            crosshairLineStyleLabel.Location = new Point(205, 39);
            crosshairLineStyleLabel.Text = "استایل:";
            ConfigureColorButton(gridColorButton, "رنگ گرید", 500, 92);
            ConfigureWidthControl(gridLineWidthNumeric, 315, 96, "gridLineWidthNumeric");
            ConfigureStyleControl(gridLineStyleCombo, 105, 96, "gridLineStyleCombo");
            gridLineWidthLabel.AutoSize = true;
            gridLineWidthLabel.Location = new Point(390, 99);
            gridLineWidthLabel.Text = "ضخامت:";
            gridLineStyleLabel.AutoSize = true;
            gridLineStyleLabel.Location = new Point(205, 99);
            gridLineStyleLabel.Text = "استایل:";

            // bottom buttons
            okButton.Location = new Point(500, 815);
            okButton.Size = new Size(80, 32);
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;
            cancelButton.Location = new Point(590, 815);
            cancelButton.Size = new Size(80, 32);
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;
            AcceptButton = okButton;
            CancelButton = cancelButton;

            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(crosshairGridGroupBox);
            Controls.Add(drawingColorsGroupBox);
            Controls.Add(chartColorsGroupBox);
            Controls.Add(chartMarginGroupBox);
            Controls.Add(chartDisplayGroupBox);

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 865);
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
            drawingColorsGroupBox.ResumeLayout(false);
            drawingColorsGroupBox.PerformLayout();
            drawingColorsPanel.ResumeLayout(false);
            crosshairGridGroupBox.ResumeLayout(false);
            crosshairGridGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)drawingLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)crosshairLineWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLineWidthNumeric).EndInit();
            ResumeLayout(false);
        }

        private static void ConfigureColorButton(Button button, string text, int x, int y)
        {
            button.Location = new Point(x, y);
            button.Size = new Size(175, 34);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Standard;
        }

        private static void ConfigureWidthControl(NumericUpDown control, int x, int y, string name)
        {
            control.Location = new Point(x, y);
            control.Name = name;
            control.Size = new Size(70, 27);
            control.Minimum = 0.5m;
            control.Maximum = 8.0m;
            control.Increment = 0.1m;
            control.DecimalPlaces = 1;
            control.Value = 1.0m;
            control.TextAlign = HorizontalAlignment.Center;
        }

        private static void ConfigureStyleControl(ComboBox control, int x, int y, string name)
        {
            control.Location = new Point(x, y);
            control.Name = name;
            control.Size = new Size(120, 28);
            control.DropDownStyle = ComboBoxStyle.DropDownList;
            control.RightToLeft = RightToLeft.Yes;
            control.Items.AddRange(new object[] { "یکپارچه", "خط‌چین", "نقطه‌چین", "خط-نقطه", "خط-نقطه-نقطه" });
            control.SelectedIndex = 0;
        }

        private static void AddDrawingColorButton(Button button, string text)
        {
            button.Size = new Size(170, 34);
            button.Margin = new Padding(4);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
            drawingColorsPanelStaticAdd(button);
        }

        private static void drawingColorsPanelStaticAdd(Button button)
        {
            // This helper is intentionally empty; buttons are added explicitly below.
        }
    }
}
