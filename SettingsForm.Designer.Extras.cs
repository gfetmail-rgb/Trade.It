namespace Trade.It
{
    partial class SettingsForm
    {
        private System.Windows.Forms.Label chartTopEmptyPercentLabel;
        private System.Windows.Forms.TextBox chartTopEmptyPercentTextBox;
        private System.Windows.Forms.Label chartTopEmptyPercentHintLabel;
        private System.Windows.Forms.Label initialVisibleCandleCountLabel;
        private System.Windows.Forms.NumericUpDown initialVisibleCandleCountNumeric;
        private System.Windows.Forms.Label initialVisibleCandleCountHintLabel;

        private void InitializeChartViewDesignerControls()
        {
            chartTopEmptyPercentLabel = new Label();
            chartTopEmptyPercentTextBox = new TextBox();
            chartTopEmptyPercentHintLabel = new Label();
            initialVisibleCandleCountLabel = new Label();
            initialVisibleCandleCountNumeric = new NumericUpDown();
            initialVisibleCandleCountHintLabel = new Label();

            ((System.ComponentModel.ISupportInitialize)initialVisibleCandleCountNumeric).BeginInit();

            chartMarginGroupBox.Controls.Add(chartTopEmptyPercentLabel);
            chartMarginGroupBox.Controls.Add(chartTopEmptyPercentTextBox);
            chartMarginGroupBox.Controls.Add(chartTopEmptyPercentHintLabel);
            chartMarginGroupBox.Controls.Add(initialVisibleCandleCountLabel);
            chartMarginGroupBox.Controls.Add(initialVisibleCandleCountNumeric);
            chartMarginGroupBox.Controls.Add(initialVisibleCandleCountHintLabel);

            chartMarginGroupBox.Height = 150;

            chartTopEmptyPercentLabel.AutoSize = true;
            chartTopEmptyPercentLabel.Location = new Point(500, 62);
            chartTopEmptyPercentLabel.Name = "chartTopEmptyPercentLabel";
            chartTopEmptyPercentLabel.Size = new Size(255, 25);
            chartTopEmptyPercentLabel.Text = "درصد فضای خالی بالای نمودار:";

            chartTopEmptyPercentTextBox.Location = new Point(404, 59);
            chartTopEmptyPercentTextBox.Name = "chartTopEmptyPercentTextBox";
            chartTopEmptyPercentTextBox.Size = new Size(90, 33);
            chartTopEmptyPercentTextBox.TextAlign = HorizontalAlignment.Center;

            chartTopEmptyPercentHintLabel.AutoSize = true;
            chartTopEmptyPercentHintLabel.Location = new Point(70, 91);
            chartTopEmptyPercentHintLabel.Name = "chartTopEmptyPercentHintLabel";
            chartTopEmptyPercentHintLabel.Size = new Size(467, 25);
            chartTopEmptyPercentHintLabel.Text = "۰ تا ۵۰ درصد؛ فضای خالی بالای ناحیه قیمت.";

            initialVisibleCandleCountLabel.AutoSize = true;
            initialVisibleCandleCountLabel.Location = new Point(500, 119);
            initialVisibleCandleCountLabel.Name = "initialVisibleCandleCountLabel";
            initialVisibleCandleCountLabel.Size = new Size(255, 25);
            initialVisibleCandleCountLabel.Text = "تعداد کندل اولیه قابل مشاهده:";

            initialVisibleCandleCountNumeric.Location = new Point(404, 116);
            initialVisibleCandleCountNumeric.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            initialVisibleCandleCountNumeric.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            initialVisibleCandleCountNumeric.Name = "initialVisibleCandleCountNumeric";
            initialVisibleCandleCountNumeric.Size = new Size(90, 33);
            initialVisibleCandleCountNumeric.TextAlign = HorizontalAlignment.Center;
            initialVisibleCandleCountNumeric.Value = new decimal(new int[] { 200, 0, 0, 0 });

            initialVisibleCandleCountHintLabel.AutoSize = true;
            initialVisibleCandleCountHintLabel.Location = new Point(70, 148);
            initialVisibleCandleCountHintLabel.Name = "initialVisibleCandleCountHintLabel";
            initialVisibleCandleCountHintLabel.Size = new Size(467, 25);
            initialVisibleCandleCountHintLabel.Text = "۱۰ تا ۵۰۰۰ کندل؛ مقدار اولیه نمایش داده‌شده هنگام بازنشانی چارت.";

            chartMarginGroupBox.Text = "حاشیه و نمای اولیه چارت";
            ((System.ComponentModel.ISupportInitialize)initialVisibleCandleCountNumeric).EndInit();
        }
    }
}