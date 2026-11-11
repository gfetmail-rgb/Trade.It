using System.Drawing;
using System.Windows.Forms;

namespace Trade.It
{
    partial class MainForm
    {
        private TabPage tabPageFilters3 = null!;
        private Button filterApplyButton3 = null!;
        private Button clearFiltersButton3 = null!;
        private Label filterCountLabel3 = null!;

        private GroupBox maFilterGroup3 = null!;
        private CheckBox maFilterEnabled3 = null!;
        private ComboBox maFilterMode3 = null!;
        private ComboBox maPriceMode3 = null!;
        private TextBox maFilterPeriod1_3 = null!;
        private TextBox maFilterPeriod2_3 = null!;

        private GroupBox rsiFilterGroup3 = null!;
        private CheckBox rsiFilterEnabled3 = null!;
        private TextBox rsiPeriod3 = null!;
        private ComboBox rsiOperator3 = null!;
        private TextBox rsiMinimum3 = null!;
        private TextBox rsiMaximum3 = null!;

        private GroupBox distanceFilterGroup3 = null!;
        private CheckBox distanceFilterEnabled3 = null!;
        private TextBox distanceHighPeriod3 = null!;
        private TextBox distanceHighMax3 = null!;
        private TextBox distanceLowPeriod3 = null!;
        private TextBox distanceLowMin3 = null!;

        private GroupBox volumePriceFilterGroup3 = null!;
        private CheckBox volumePriceFilterEnabled3 = null!;
        private TextBox volumePricePeriod3 = null!;
        private TextBox volumePriceRatio3 = null!;
        private ComboBox volumePriceDirection3 = null!;

        private GroupBox trendStrengthFilterGroup3 = null!;
        private CheckBox trendStrengthFilterEnabled3 = null!;
        private TextBox adxPeriod3 = null!;
        private TextBox adxMinimum3 = null!;
        private ComboBox adxDirection3 = null!;

        private void InitializeFilters3Designer()
        {
            tabPageFilters3 = new TabPage();
            tabPageFilters3.Text = "فیلترها 3";
            tabPageFilters3.Name = "tabPageFilters3";
            tabPageFilters3.Padding = new Padding(8);
            tabPageFilters3.RightToLeft = RightToLeft.Yes;
            tabPageFilters3.AutoScroll = true;

            maFilterGroup3 = CreateFilterGroup("میانگین متحرک", 8, 8, 365, 180);
            maFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            maFilterPeriod1_3 = CreateTextBox(new Point(12, 72), "20");
            maFilterPeriod2_3 = CreateTextBox(new Point(105, 72), "50");
            var maPeriod1Label = CreateLabel("کوتاه", 12, 48);
            maPeriod1Label.Size = new Size(82, 24);
            var maPeriod2Label = CreateLabel("بلند", 105, 48);
            maPeriod2Label.Size = new Size(82, 24);
            maFilterMode3 = CreateComboBox(new Point(12, 105), new Size(335, 25),
                "میانگین کوتاه بالاتر از میانگین بلند", "میانگین کوتاه پایین‌تر از میانگین بلند");
            maPriceMode3 = CreateComboBox(new Point(12, 137), new Size(335, 25),
                "قیمت بالاتر از میانگین کوتاه", "قیمت پایین‌تر از میانگین بلند", "قیمت بین دو میانگین", "بدون شرط");
            maFilterGroup3.Controls.AddRange(new Control[] {
                maFilterEnabled3, maFilterMode3, maPriceMode3,
                maPeriod1Label, maPeriod2Label,
                maFilterPeriod1_3, maFilterPeriod2_3
            });
            maFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(maFilterEnabled3, maFilterMode3, maPriceMode3, maFilterPeriod1_3, maFilterPeriod2_3);

            rsiFilterGroup3 = CreateFilterGroup("شاخص قدرت نسبی (RSI)", 8, 206, 365, 145);
            rsiFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            rsiPeriod3 = CreateTextBox(new Point(12, 66), "14");
            rsiPeriod3.Size = new Size(65, 25);
            rsiOperator3 = CreateComboBox(new Point(92, 66), new Size(112, 25), "بین", "کمتر از", "بیشتر از");
            rsiMinimum3 = CreateTextBox(new Point(218, 66), "30");
            rsiMinimum3.Size = new Size(55, 25);
            rsiMaximum3 = CreateTextBox(new Point(290, 66), "70");
            rsiMaximum3.Size = new Size(55, 25);
            var rsiPeriodLabel = CreateLabel("دوره", 12, 39);
            rsiPeriodLabel.Size = new Size(65, 24);
            var rsiConditionLabel = CreateLabel("شرط", 92, 39);
            rsiConditionLabel.Size = new Size(112, 24);
            var rsiMinimumLabel = CreateLabel("حداقل", 218, 94);
            rsiMinimumLabel.Size = new Size(55, 24);
            var rsiMaximumLabel = CreateLabel("حداکثر", 290, 94);
            rsiMaximumLabel.Size = new Size(55, 24);
            rsiFilterGroup3.Controls.AddRange(new Control[] {
                rsiFilterEnabled3, rsiPeriodLabel, rsiConditionLabel,
                rsiMinimumLabel, rsiMaximumLabel,
                rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3
            });
            rsiFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(rsiFilterEnabled3, rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3);

            distanceFilterGroup3 = CreateFilterGroup("فاصله قیمت از سقف و کف اخیر", 8, 369, 365, 175);
            distanceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            distanceHighPeriod3 = CreateTextBox(new Point(12, 58), "20");
            distanceHighMax3 = CreateTextBox(new Point(190, 58), "8");
            distanceLowPeriod3 = CreateTextBox(new Point(12, 119), "20");
            distanceLowMin3 = CreateTextBox(new Point(190, 119), "2");
            var distanceHighPeriodLabel = CreateLabel("دوره سقف", 12, 25);
            distanceHighPeriodLabel.Size = new Size(150, 24);
            var distanceHighMaxLabel = CreateLabel("حداکثر فاصله از سقف ٪", 190, 25);
            distanceHighMaxLabel.Size = new Size(155, 24);
            var distanceLowPeriodLabel = CreateLabel("دوره کف", 12, 90);
            distanceLowPeriodLabel.Size = new Size(150, 24);
            var distanceLowMinLabel = CreateLabel("حداقل فاصله از کف ٪", 190, 90);
            distanceLowMinLabel.Size = new Size(155, 24);
            distanceFilterGroup3.Controls.AddRange(new Control[] {
                distanceFilterEnabled3,
                distanceHighPeriodLabel, distanceHighMaxLabel,
                distanceLowPeriodLabel, distanceLowMinLabel,
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3
            });
            distanceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(distanceFilterEnabled3,
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3);

            volumePriceFilterGroup3 = CreateFilterGroup("افزایش حجم همراه با تغییر قیمت", 8, 562, 365, 145);
            volumePriceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            volumePricePeriod3 = CreateTextBox(new Point(12, 66), "20");
            volumePriceRatio3 = CreateTextBox(new Point(132, 66), "1.3");
            volumePriceDirection3 = CreateComboBox(new Point(245, 66), new Size(100, 25), "مثبت", "منفی", "بدون شرط");
            var volumePeriodLabel = CreateLabel("دوره میانگین حجم", 12, 39);
            volumePeriodLabel.Size = new Size(110, 24);
            var volumeRatioLabel = CreateLabel("حداقل نسبت حجم", 132, 39);
            volumeRatioLabel.Size = new Size(105, 24);
            var volumeDirectionLabel = CreateLabel("جهت تغییر قیمت", 245, 39);
            volumeDirectionLabel.Size = new Size(100, 24);
            volumePriceFilterGroup3.Controls.AddRange(new Control[] {
                volumePriceFilterEnabled3,
                volumePeriodLabel, volumeRatioLabel, volumeDirectionLabel,
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3
            });
            volumePriceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(volumePriceFilterEnabled3,
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3);

            trendStrengthFilterGroup3 = CreateFilterGroup("قدرت روند (ADX)", 8, 725, 365, 140);
            trendStrengthFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            adxPeriod3 = CreateTextBox(new Point(12, 66), "14");
            adxMinimum3 = CreateTextBox(new Point(190, 66), "20");
            adxDirection3 = CreateComboBox(new Point(12, 100), new Size(333, 25), "صعودی", "نزولی");
            var adxPeriodLabel = CreateLabel("دوره ADX", 12, 39);
            adxPeriodLabel.Size = new Size(150, 24);
            var adxMinimumLabel = CreateLabel("حداقل قدرت روند", 190, 39);
            adxMinimumLabel.Size = new Size(155, 24);
            trendStrengthFilterGroup3.Controls.AddRange(new Control[] {
                trendStrengthFilterEnabled3, adxPeriodLabel, adxMinimumLabel,
                adxPeriod3, adxMinimum3, adxDirection3
            });
            trendStrengthFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(trendStrengthFilterEnabled3, adxPeriod3, adxMinimum3, adxDirection3);

            filterApplyButton3 = new Button();
            filterApplyButton3.Name = "filterApplyButton3";
            filterApplyButton3.Text = "تایید";
            filterApplyButton3.Size = new Size(100, 34);
            filterApplyButton3.Location = new Point(270, 882);
            filterApplyButton3.Click += FilterApplyButton_Click;

            clearFiltersButton3 = new Button();
            clearFiltersButton3.Name = "clearFiltersButton3";
            clearFiltersButton3.Text = "پاک";
            clearFiltersButton3.Size = new Size(75, 34);
            clearFiltersButton3.Location = new Point(185, 882);
            clearFiltersButton3.Click += ClearFiltersButton_Click;

            filterCountLabel3 = new Label();
            filterCountLabel3.Name = "filterCountLabel3";
            filterCountLabel3.AutoSize = false;
            filterCountLabel3.TextAlign = ContentAlignment.MiddleRight;
            filterCountLabel3.Size = new Size(350, 25);
            filterCountLabel3.Location = new Point(8, 924);

            SetFilterInputsEnabled(maFilterEnabled3, maFilterMode3, maFilterPeriod1_3, maFilterPeriod2_3);
            SetFilterInputsEnabled(rsiFilterEnabled3, rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3);
            SetFilterInputsEnabled(distanceFilterEnabled3, distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3);
            SetFilterInputsEnabled(volumePriceFilterEnabled3, volumePricePeriod3, volumePriceRatio3, volumePriceDirection3);
            SetFilterInputsEnabled(trendStrengthFilterEnabled3, adxPeriod3, adxMinimum3);

            tabPageFilters3.Controls.AddRange(new Control[] {
                maFilterGroup3, rsiFilterGroup3, distanceFilterGroup3,
                volumePriceFilterGroup3, trendStrengthFilterGroup3,
                filterApplyButton3, clearFiltersButton3, filterCountLabel3
            });
            tabPageFilters3.ResumeLayout(false);
            tabPageFilters3.PerformLayout();

            controlTabControl.Controls.Add(tabPageFilters3);
        }

        private static GroupBox CreateFilterGroup(string title, int x, int y, int width, int height)
        {
            return new GroupBox
            {
                Text = title,
                Location = new Point(x, y),
                Size = new Size(width, height),
                RightToLeft = RightToLeft.Yes
            };
        }

        private static CheckBox CreateEnableCheckBox(string text, int x, int y)
        {
            return new CheckBox
            {
                Text = text,
                Checked = false,
                AutoSize = true,
                Location = new Point(x, y),
                RightToLeft = RightToLeft.Yes
            };
        }

        private static ComboBox CreateComboBox(Point location, Size size, params string[] items)
        {
            var combo = new ComboBox
            {
                Location = location,
                Size = size,
                DropDownStyle = ComboBoxStyle.DropDownList,
                RightToLeft = RightToLeft.Yes
            };
            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;
            return combo;
        }

        private static TextBox CreateTextBox(Point location, string value)
        {
            return new TextBox
            {
                Location = location,
                Size = new Size(82, 25),
                Text = value,
                TextAlign = HorizontalAlignment.Center
            };
        }

        private static Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(100, 24),
                TextAlign = ContentAlignment.MiddleRight
            };
        }

        private static void SetFilterInputsEnabled(CheckBox checkBox, params Control[] controls)
        {
            foreach (var control in controls)
                control.Enabled = checkBox.Checked;
        }
    }
}
