namespace Trade.It
{
    public partial class SettingsForm
    {
        private static readonly HashSet<SettingsForm> initialCandleCountForms = new();
        private NumericUpDown? initialVisibleCandleCountNumeric;

        public int InitialVisibleCandleCount
        {
            get => initialVisibleCandleCountNumeric?.Value is decimal value
                ? (int)value
                : ChartAppearanceSettings.InitialVisibleCandleCount;
            set
            {
                if (initialVisibleCandleCountNumeric == null)
                    return;

                initialVisibleCandleCountNumeric.Value = Math.Clamp(value, 10, 5000);
            }
        }

        static SettingsForm()
        {
            Application.Idle += InitializeInitialCandleCountControls;
        }

        private static void InitializeInitialCandleCountControls(object? sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is not SettingsForm settingsForm || settingsForm.IsDisposed)
                    continue;

                if (!initialCandleCountForms.Add(settingsForm))
                    continue;

                settingsForm.AddInitialCandleCountControl();
                settingsForm.FormClosed += settingsForm.InitialCandleCountFormClosed;
            }

            initialCandleCountForms.RemoveWhere(form => form.IsDisposed);
        }

        private void AddInitialCandleCountControl()
        {
            if (initialVisibleCandleCountNumeric != null)
                return;

            const int shift = 32;

            chartMarginGroupBox.Height += shift;
            chartColorsGroupBox.Top += shift;
            drawingColorsGroupBox.Top += shift;
            crosshairGridGroupBox.Top += shift;
            okButton.Top += shift;
            cancelButton.Top += shift;
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + shift);

            var label = new Label
            {
                AutoSize = true,
                Location = new Point(500, 126),
                RightToLeft = RightToLeft.Yes,
                Text = "تعداد کندل‌های نمایش داده شده در شروع چارت:"
            };

            initialVisibleCandleCountNumeric = new NumericUpDown
            {
                Location = new Point(390, 122),
                Size = new Size(90, 33),
                Minimum = 10,
                Maximum = 5000,
                Increment = 10,
                Value = Math.Clamp(ChartAppearanceSettings.InitialVisibleCandleCount, 10, 5000),
                TextAlign = HorizontalAlignment.Center,
                RightToLeft = RightToLeft.No
            };

            var hint = new Label
            {
                AutoSize = true,
                Location = new Point(70, 155),
                RightToLeft = RightToLeft.Yes,
                Text = "بین ۱۰ تا ۵۰۰۰ کندل. مقدار پیش‌فرض: ۲۰۰"
            };

            chartMarginGroupBox.Controls.Add(label);
            chartMarginGroupBox.Controls.Add(initialVisibleCandleCountNumeric);
            chartMarginGroupBox.Controls.Add(hint);
            chartMarginGroupBox.BringToFront();
        }

        private void InitialCandleCountFormClosed(object? sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK || initialVisibleCandleCountNumeric == null)
                return;

            ChartAppearanceSettings.SetInitialVisibleCandleCount((int)initialVisibleCandleCountNumeric.Value);
            ChartAppearanceSettings.Save();
        }
    }
}