namespace Trade.It
{
    internal sealed partial class OnFuMoradiSettingsForm
    {
        private System.ComponentModel.IContainer? components = null;
        private Label trendWeightLabel = null!;
        private Label momentumWeightLabel = null!;
        private Label meanReversionWeightLabel = null!;
        private Label volatilityLimitLabel = null!;
        private Label wickPatternWeightLabel = null!;
        private Label forecastCandleCountLabel = null!;
        private Label trendDescriptionLabel = null!;
        private Label momentumDescriptionLabel = null!;
        private Label meanReversionDescriptionLabel = null!;
        private Label volatilityDescriptionLabel = null!;
        private Label wickDescriptionLabel = null!;
        private Label forecastCandleCountDescriptionLabel = null!;
        private NumericUpDown trendWeightNumeric = null!;
        private NumericUpDown momentumWeightNumeric = null!;
        private NumericUpDown meanReversionWeightNumeric = null!;
        private NumericUpDown volatilityLimitNumeric = null!;
        private NumericUpDown wickPatternWeightNumeric = null!;
        private NumericUpDown forecastCandleCountNumeric = null!;
        private Button defaultButton = null!;
        private Button okButton = null!;
        private Button cancelButton = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            trendWeightLabel = new Label();
            momentumWeightLabel = new Label();
            meanReversionWeightLabel = new Label();
            volatilityLimitLabel = new Label();
            wickPatternWeightLabel = new Label();
            forecastCandleCountLabel = new Label();
            trendDescriptionLabel = new Label();
            momentumDescriptionLabel = new Label();
            meanReversionDescriptionLabel = new Label();
            volatilityDescriptionLabel = new Label();
            wickDescriptionLabel = new Label();
            forecastCandleCountDescriptionLabel = new Label();
            trendWeightNumeric = CreateNumeric(0m, 2m, 0.55m);
            momentumWeightNumeric = CreateNumeric(0m, 2m, 0.25m);
            meanReversionWeightNumeric = CreateNumeric(0m, 2m, 0.20m);
            volatilityLimitNumeric = CreateNumeric(0.05m, 2m, 0.35m);
            wickPatternWeightNumeric = CreateNumeric(0m, 2m, 0.15m);
            forecastCandleCountNumeric = CreateIntegerNumeric(1, 100, 13);
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();

            ConfigureLabel(trendWeightLabel, "وزن اثر روند", 390, 24, 115);
            ConfigureLabel(momentumWeightLabel, "وزن شتاب حرکت", 390, 88, 115);
            ConfigureLabel(meanReversionWeightLabel, "وزن بازگشت به میانگین", 390, 152, 115);
            ConfigureLabel(volatilityLimitLabel, "ضریب محدودیت نوسان", 390, 216, 115);
            ConfigureLabel(wickPatternWeightLabel, "ضریب الگوی سایه‌ها", 390, 280, 115);
            ConfigureLabel(forecastCandleCountLabel, "تعداد کندل آینده", 390, 344, 115);

            ConfigureDescription(trendDescriptionLabel, "اثر شیب روند قیمت بر مسیر پیش‌بینی", 15, 22);
            ConfigureDescription(momentumDescriptionLabel, "اثر تغییرات و شتاب اخیر قیمت", 15, 86);
            ConfigureDescription(meanReversionDescriptionLabel, "تمایل قیمت پیش‌بینی‌شده به میانگین متحرک", 15, 150);
            ConfigureDescription(volatilityDescriptionLabel, "حداکثر شدت حرکت پیش‌بینی نسبت به نوسان اخیر", 15, 214);
            ConfigureDescription(wickDescriptionLabel, "اثر نسبت سایه‌های بالایی و پایینی کندل‌ها", 15, 278);
            ConfigureDescription(forecastCandleCountDescriptionLabel, "تعداد کندل‌هایی که در آینده رسم می‌شوند (۱ تا ۱۰۰)", 15, 342);

            ConfigureNumeric(trendWeightNumeric, 255, 20);
            ConfigureNumeric(momentumWeightNumeric, 255, 84);
            ConfigureNumeric(meanReversionWeightNumeric, 255, 148);
            ConfigureNumeric(volatilityLimitNumeric, 255, 212);
            ConfigureNumeric(wickPatternWeightNumeric, 255, 276);
            ConfigureNumeric(forecastCandleCountNumeric, 255, 340);

            defaultButton.Location = new Point(338, 418);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(112, 38);
            defaultButton.TabIndex = 10;
            defaultButton.Text = "مقادیر پیش‌فرض";
            defaultButton.UseVisualStyleBackColor = true;

            okButton.Location = new Point(230, 418);
            okButton.Name = "okButton";
            okButton.Size = new Size(96, 38);
            okButton.TabIndex = 11;
            okButton.Text = "تأیید";
            okButton.UseVisualStyleBackColor = true;

            cancelButton.Location = new Point(122, 418);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(96, 38);
            cancelButton.TabIndex = 12;
            cancelButton.Text = "انصراف";
            cancelButton.UseVisualStyleBackColor = true;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 474);
            Controls.Add(trendWeightLabel);
            Controls.Add(momentumWeightLabel);
            Controls.Add(meanReversionWeightLabel);
            Controls.Add(volatilityLimitLabel);
            Controls.Add(wickPatternWeightLabel);
            Controls.Add(forecastCandleCountLabel);
            Controls.Add(trendDescriptionLabel);
            Controls.Add(momentumDescriptionLabel);
            Controls.Add(meanReversionDescriptionLabel);
            Controls.Add(volatilityDescriptionLabel);
            Controls.Add(wickDescriptionLabel);
            Controls.Add(forecastCandleCountDescriptionLabel);
            Controls.Add(trendWeightNumeric);
            Controls.Add(momentumWeightNumeric);
            Controls.Add(meanReversionWeightNumeric);
            Controls.Add(volatilityLimitNumeric);
            Controls.Add(wickPatternWeightNumeric);
            Controls.Add(forecastCandleCountNumeric);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            AcceptButton = okButton;
            CancelButton = cancelButton;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OnFuMoradiSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات پیش‌بینی OnFuMoradi";
            ResumeLayout(false);
            PerformLayout();
        }

        private static NumericUpDown CreateNumeric(decimal minimum, decimal maximum, decimal value)
        {
            return new NumericUpDown
            {
                DecimalPlaces = 2,
                Increment = 0.05m,
                Minimum = minimum,
                Maximum = maximum,
                Value = value,
                ThousandsSeparator = false,
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(105, 30)
            };
        }


        private static NumericUpDown CreateIntegerNumeric(int minimum, int maximum, int value)
        {
            return new NumericUpDown
            {
                DecimalPlaces = 0,
                Increment = 1,
                Minimum = minimum,
                Maximum = maximum,
                Value = value,
                ThousandsSeparator = false,
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(105, 30)
            };
        }

        private static void ConfigureLabel(Label label, string text, int x, int y, int width)
        {
            label.AutoSize = false;
            label.Location = new Point(x, y);
            label.Size = new Size(width, 48);
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.RightToLeft = RightToLeft.Yes;
        }

        private static void ConfigureDescription(Label label, string text, int x, int y)
        {
            label.AutoSize = false;
            label.Location = new Point(x, y);
            label.Size = new Size(225, 48);
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.RightToLeft = RightToLeft.Yes;
            label.ForeColor = SystemColors.GrayText;
        }

        private static void ConfigureNumeric(NumericUpDown numeric, int x, int y)
        {
            numeric.Location = new Point(x, y + 8);
            numeric.Size = new Size(105, 30);
        }
    }
}