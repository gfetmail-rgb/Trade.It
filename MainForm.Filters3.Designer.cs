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

            maFilterGroup3 = CreateFilterGroup("میانگین متحرک", 8, 8, 350, 112);
            maFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            maFilterMode3 = CreateComboBox(new Point(12, 48), new Size(320, 25),
                "قیمت پایانی بالاتر از میانگین", "میانگین کوتاه بالاتر از میانگین بلند");
            maFilterPeriod1_3 = CreateTextBox(new Point(12, 79), "20");
            maFilterPeriod2_3 = CreateTextBox(new Point(105, 79), "50");
            maFilterGroup3.Controls.AddRange(new Control[] {
                maFilterEnabled3, maFilterMode3,
                CreateLabel("دوره اول", 12, 61), CreateLabel("دوره دوم (فقط حالت دوم)", 105, 61),
                maFilterPeriod1_3, maFilterPeriod2_3
            });
            maFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(maFilterEnabled3, maFilterMode3, maFilterPeriod1_3, maFilterPeriod2_3);

            rsiFilterGroup3 = CreateFilterGroup("شاخص قدرت نسبی (RSI)", 8, 128, 350, 112);
            rsiFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            rsiPeriod3 = CreateTextBox(new Point(12, 48), "14");
            rsiOperator3 = CreateComboBox(new Point(105, 48), new Size(105, 25), "بین", "کمتر از", "بیشتر از");
            rsiMinimum3 = CreateTextBox(new Point(218, 48), "40");
            rsiMaximum3 = CreateTextBox(new Point(275, 48), "70");
            rsiFilterGroup3.Controls.AddRange(new Control[] {
                rsiFilterEnabled3, CreateLabel("دوره", 12, 29), CreateLabel("شرط", 105, 29),
                CreateLabel("حد پایین", 218, 29), CreateLabel("حد بالا", 275, 29),
                rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3
            });
            rsiFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(rsiFilterEnabled3, rsiPeriod3, rsiOperator3, rsiMinimum3, rsiMaximum3);

            distanceFilterGroup3 = CreateFilterGroup("فاصله قیمت از سقف و کف اخیر", 8, 248, 350, 132);
            distanceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            distanceHighPeriod3 = CreateTextBox(new Point(12, 48), "20");
            distanceHighMax3 = CreateTextBox(new Point(105, 48), "8");
            distanceLowPeriod3 = CreateTextBox(new Point(12, 91), "20");
            distanceLowMin3 = CreateTextBox(new Point(105, 91), "2");
            distanceFilterGroup3.Controls.AddRange(new Control[] {
                distanceFilterEnabled3,
                CreateLabel("دوره سقف", 12, 29), CreateLabel("حداکثر فاصله از سقف ٪", 105, 29),
                CreateLabel("دوره کف", 12, 72), CreateLabel("حداقل فاصله از کف ٪", 105, 72),
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3
            });
            distanceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(distanceFilterEnabled3,
                distanceHighPeriod3, distanceHighMax3, distanceLowPeriod3, distanceLowMin3);

            volumePriceFilterGroup3 = CreateFilterGroup("افزایش حجم همراه با تغییر قیمت", 8, 388, 350, 112);
            volumePriceFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            volumePricePeriod3 = CreateTextBox(new Point(12, 48), "20");
            volumePriceRatio3 = CreateTextBox(new Point(105, 48), "1.3");
            volumePriceDirection3 = CreateComboBox(new Point(218, 48), new Size(115, 25), "مثبت", "منفی", "بدون شرط");
            volumePriceFilterGroup3.Controls.AddRange(new Control[] {
                volumePriceFilterEnabled3,
                CreateLabel("دوره میانگین حجم", 12, 29), CreateLabel("حداقل نسبت حجم", 105, 29),
                CreateLabel("جهت تغییر قیمت", 218, 29),
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3
            });
            volumePriceFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(volumePriceFilterEnabled3,
                volumePricePeriod3, volumePriceRatio3, volumePriceDirection3);

            trendStrengthFilterGroup3 = CreateFilterGroup("قدرت روند (ADX)", 8, 508, 350, 100);
            trendStrengthFilterEnabled3 = CreateEnableCheckBox("فعال", 265, 20);
            adxPeriod3 = CreateTextBox(new Point(12, 48), "14");
            adxMinimum3 = CreateTextBox(new Point(105, 48), "20");
            trendStrengthFilterGroup3.Controls.AddRange(new Control[] {
                trendStrengthFilterEnabled3,
                CreateLabel("دوره ADX", 12, 29), CreateLabel("حداقل قدرت روند", 105, 29),
                adxPeriod3, adxMinimum3
            });
            trendStrengthFilterEnabled3.CheckedChanged += (_, _) => SetFilterInputsEnabled(trendStrengthFilterEnabled3, adxPeriod3, adxMinimum3);

            filterApplyButton3 = new Button();
            filterApplyButton3.Name = "filterApplyButton3";
            filterApplyButton3.Text = "تایید";
            filterApplyButton3.Size = new Size(100, 34);
            filterApplyButton3.Location = new Point(246, 620);
            filterApplyButton3.Click += FilterApplyButton_Click;

            clearFiltersButton3 = new Button();
            clearFiltersButton3.Name = "clearFiltersButton3";
            clearFiltersButton3.Text = "پاک";
            clearFiltersButton3.Size = new Size(75, 34);
            clearFiltersButton3.Location = new Point(165, 620);
            clearFiltersButton3.Click += ClearFiltersButton_Click;

            filterCountLabel3 = new Label();
            filterCountLabel3.Name = "filterCountLabel3";
            filterCountLabel3.AutoSize = false;
            filterCountLabel3.TextAlign = ContentAlignment.MiddleRight;
            filterCountLabel3.Size = new Size(330, 25);
            filterCountLabel3.Location = new Point(8, 660);

            tabPageFilters3.Controls.AddRange(new Control[] {
                maFilterGroup3, rsiFilterGroup3, distanceFilterGroup3,
                volumePriceFilterGroup3, trendStrengthFilterGroup3,
                filterApplyButton3, clearFiltersButton3, filterCountLabel3
            });

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
