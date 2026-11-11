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

        private void InitializeFilters3Designer()
        {
            tabPageFilters3 = new TabPage();
            tabPageFilters3.Text = "فیلترها 3";
            tabPageFilters3.Name = "tabPageFilters3";
            tabPageFilters3.Padding = new Padding(8);
            tabPageFilters3.RightToLeft = RightToLeft.Yes;
            tabPageFilters3.AutoScroll = true;

            maFilterGroup3 = CreateFilterGroup("میانگین متحرک", 8, 8, 365, 122);
            maFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            maFilterMode3 = CreateComboBox(new Point(12, 48), new Size(335, 25),
                "قیمت پایانی بالاتر از میانگین", "میانگین کوتاه بالاتر از میانگین بلند");
            maFilterPeriod1_3 = CreateTextBox(new Point(12, 79), "20");
            maFilterPeriod2_3 = CreateTextBox(new Point(105, 79), "50");
            var maPeriod1Label = CreateLabel("دوره اول", 12, 61);
            maPeriod1Label.Size = new Size(82, 18);
            var maPeriod2Label = CreateLabel("دوره دوم", 105, 61);
            maPeriod2Label.Size = new Size(82, 18);
            maFilterGroup3.Controls.AddRange(new Control[] {
                maFilterEnabled3, maFilterMode3,
                maPeriod1Label, maPeriod2Label,
                maFilterPeriod1_3, maFilterPeriod2_3
            });
            maFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(maFilterEnabled3, maFilterMode3, maFilterPeriod1_3, maFilterPeriod2_3);

            rsiFilterGroup3 = CreateFilterGroup("شاخص قدرت نسبی (RSI)", 8, 138, 365, 132);
            rsiFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            rsiPeriod3 = CreateTextBox(new Point(12, 66), "14");
            rsiPeriod3.Size = new Size(65, 25);
            rsiOperator3 = CreateComboBox(new Point(92, 66), new Size(112, 25), "بین", "کمتر از", "بیشتر از");
            rsiMinimum3 = CreateTextBox(new Point(218, 66), "30");
            rsiMinimum3.Size = new Size(55, 25);
            rsiMaximum3 = CreateTextBox(new Point(290, 66), "70");
            rsiMaximum3.Size = new Size(55, 25);
            var rsiPeriodLabel = CreateLabel("دوره", 12, 43);
            rsiPeriodLabel.Size = new Size(65, 18);
            var rsiConditionLabel = CreateLabel("شرط", 92, 43);
            rsiConditionLabel.Size = new Size(112, 18);
            var rsiMinimumLabel = CreateLabel("حد پایین", 218, 43);
            rsiMinimumLabel.Size = new Size(55, 18);
            var rsiMaximumLabel = CreateLabel("حد بالا", 290, 43);
            rsiMaximumLabel.Size = new Size(55, 18);
            rsiFilterGroup3.Controls.AddRange(new Control[] {
                rsiFilterEnabled3, rsiPeriodLabel, rsiConditionLabel,
                rsiMinimumLabel, rsiMaximumLabel,
                rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3
            });
            rsiFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(rsiFilterEnabled3, rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3);

            distanceFilterGroup3 = CreateFilterGroup("فاصله قیمت از سقف و کف اخیر", 8, 278, 365, 142);
            distanceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            distanceHighPeriod3 = CreateTextBox(new Point(12, 52), "20");
            distanceHighMax3 = CreateTextBox(new Point(190, 52), "8");
            distanceLowPeriod3 = CreateTextBox(new Point(12, 103), "20");
            distanceLowMin3 = CreateTextBox(new Point(190, 103), "2");
            var distanceHighPeriodLabel = CreateLabel("دوره سقف", 12, 29);
            distanceHighPeriodLabel.Size = new Size(150, 18);
            var distanceHighMaxLabel = CreateLabel("حداکثر فاصله از سقف ٪", 190, 29);
            distanceHighMaxLabel.Size = new Size(155, 18);
            var distanceLowPeriodLabel = CreateLabel("دوره کف", 12, 80);
            distanceLowPeriodLabel.Size = new Size(150, 18);
            var distanceLowMinLabel = CreateLabel("حداقل فاصله از کف ٪", 190, 80);
            distanceLowMinLabel.Size = new Size(155, 18);
            distanceFilterGroup3.Controls.AddRange(new Control[] {
                distanceFilterEnabled3,
                distanceHighPeriodLabel, distanceHighMaxLabel,
                distanceLowPeriodLabel, distanceLowMinLabel,
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3
            });
            distanceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(distanceFilterEnabled3,
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3);

            volumePriceFilterGroup3 = CreateFilterGroup("افزایش حجم همراه با تغییر قیمت", 8, 428, 365, 122);
            volumePriceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            volumePricePeriod3 = CreateTextBox(new Point(12, 66), "20");
            volumePriceRatio3 = CreateTextBox(new Point(132, 66), "1.3");
            volumePriceDirection3 = CreateComboBox(new Point(245, 66), new Size(100, 25), "مثبت", "منفی", "بدون شرط");
            var volumePeriodLabel = CreateLabel("دوره میانگین حجم", 12, 43);
            volumePeriodLabel.Size = new Size(110, 18);
            var volumeRatioLabel = CreateLabel("حداقل نسبت حجم", 132, 43);
            volumeRatioLabel.Size = new Size(105, 18);
            var volumeDirectionLabel = CreateLabel("جهت تغییر قیمت", 245, 43);
            volumeDirectionLabel.Size = new Size(100, 18);
            volumePriceFilterGroup3.Controls.AddRange(new Control[] {
                volumePriceFilterEnabled3,
                volumePeriodLabel, volumeRatioLabel, volumeDirectionLabel,
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3
            });
            volumePriceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(volumePriceFilterEnabled3,
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3);

            trendStrengthFilterGroup3 = CreateFilterGroup("قدرت روند صعودی (ADX)", 8, 558, 365, 112);
            trendStrengthFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            adxPeriod3 = CreateTextBox(new Point(12, 66), "14");
            adxMinimum3 = CreateTextBox(new Point(190, 66), "20");
            var adxPeriodLabel = CreateLabel("دوره ADX", 12, 43);
            adxPeriodLabel.Size = new Size(150, 18);
            var adxMinimumLabel = CreateLabel("حداقل قدرت روند", 190, 43);
            adxMinimumLabel.Size = new Size(155, 18);
            trendStrengthFilterGroup3.Controls.AddRange(new Control[] {
                trendStrengthFilterEnabled3, adxPeriodLabel, adxMinimumLabel,
                adxPeriod3, adxMinimum3
            });
            trendStrengthFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(trendStrengthFilterEnabled3, adxPeriod3, adxMinimum3);

            filterApplyButton3 = new Button();
            filterApplyButton3.Name = "filterApplyButton3";
            filterApplyButton3.Text = "تایید";
            filterApplyButton3.Size = new Size(100, 34);
            filterApplyButton3.Location = new Point(270, 684);
            filterApplyButton3.Click += FilterApplyButton_Click;

            clearFiltersButton3 = new Button();
            clearFiltersButton3.Name = "clearFiltersButton3";
            clearFiltersButton3.Text = "پاک";
            clearFiltersButton3.Size = new Size(75, 34);
            clearFiltersButton3.Location = new Point(185, 684);
            clearFiltersButton3.Click += ClearFiltersButton_Click;

            filterCountLabel3 = new Label();
            filterCountLabel3.Name = "filterCountLabel3";
            filterCountLabel3.AutoSize = false;
            filterCountLabel3.TextAlign = ContentAlignment.MiddleRight;
            filterCountLabel3.Size = new Size(350, 25);
            filterCountLabel3.Location = new Point(8, 725);

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
                Size = new Size(100, 18),
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
