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
            okButton = new Button();
            cancelButton = new Button();
            chartDisplayGroupBox.SuspendLayout();
            chartMarginGroupBox.SuspendLayout();
            chartColorsGroupBox.SuspendLayout();
            drawingColorsGroupBox.SuspendLayout();
            drawingColorsPanel.SuspendLayout();
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
            chartColorsGroupBox.Controls.Add(resetChartColorsButton);
            chartColorsGroupBox.Location = new Point(16, 226);
            chartColorsGroupBox.Name = "chartColorsGroupBox";
            chartColorsGroupBox.RightToLeft = RightToLeft.Yes;
            chartColorsGroupBox.Size = new Size(660, 105);
            chartColorsGroupBox.TabStop = false;
            chartColorsGroupBox.Text = "رنگ نمودار";
            ConfigureColorButton(risingColorButton, "رنگ کندل و میله صعودی", 470, 30);
            ConfigureColorButton(fallingColorButton, "رنگ کندل و میله نزولی", 280, 30);
            ConfigureColorButton(lineColorButton, "رنگ چارت خطی", 90, 30);
            resetChartColorsButton.Location = new Point(16, 30);
            resetChartColorsButton.Size = new Size(65, 32);
            resetChartColorsButton.Text = "پیش‌فرض";
            resetChartColorsButton.UseVisualStyleBackColor = true;
            // drawingColorsGroupBox
            drawingColorsGroupBox.Controls.Add(drawingColorsPanel);
            drawingColorsGroupBox.Controls.Add(resetDrawingColorsButton);
            drawingColorsGroupBox.Location = new Point(16, 341);
            drawingColorsGroupBox.Name = "drawingColorsGroupBox";
            drawingColorsGroupBox.RightToLeft = RightToLeft.Yes;
            drawingColorsGroupBox.Size = new Size(660, 210);
            drawingColorsGroupBox.TabStop = false;
            drawingColorsGroupBox.Text = "رنگ شکل‌ها و ابزارهای رسم";
            drawingColorsPanel.Location = new Point(15, 28);
            drawingColorsPanel.Size = new Size(555, 165);
            drawingColorsPanel.FlowDirection = FlowDirection.RightToLeft;
            drawingColorsPanel.WrapContents = true;
            drawingColorsPanel.RightToLeft = RightToLeft.Yes;
            drawingColorsPanel.AutoScroll = true;
            AddDrawingColorButton(trendLineColorButton, "خط روند");
            AddDrawingColorButton(trendChannelColorButton, "کانال روند");
            AddDrawingColorButton(horizontalDoubleArrowColorButton, "فلش افقی دو سر");
            AddDrawingColorButton(verticalDoubleArrowColorButton, "فلش عمودی دو سر");
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
            // buttons
            okButton.Location = new Point(500, 565);
            okButton.Size = new Size(80, 32);
            okButton.Text = "تأیید";
            cancelButton.Location = new Point(590, 565);
            cancelButton.Size = new Size(80, 32);
            cancelButton.Text = "انصراف";
            AcceptButton = okButton;
            CancelButton = cancelButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 615);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
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
            drawingColorsGroupBox.ResumeLayout(false);
            drawingColorsPanel.ResumeLayout(false);
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

        private void AddDrawingColorButton(Button button, string text)
        {
            button.Size = new Size(170, 34);
            button.Margin = new Padding(4);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
            drawingColorsPanel.Controls.Add(button);
        }
    }
}
