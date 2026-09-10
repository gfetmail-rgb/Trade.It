from pathlib import Path

p = Path("MainForm.cs")
s = p.read_text(encoding="utf-8-sig")

if "private void InitializeIndividualFilterClearButtons()" in s:
    raise SystemExit("already patched")

anchor = "            AttachOhlcChangeFilterEvents();\n"
if anchor not in s:
    raise SystemExit("constructor anchor not found")
s = s.replace(anchor, anchor + "            InitializeIndividualFilterClearButtons();\n", 1)

method = '''        private void InitializeIndividualFilterClearButtons()
        {
            AddIndividualFilterClearButton(tradingStatusGroup, "trading");
            AddIndividualFilterClearButton(nameFilterGroup, "name");
            AddIndividualFilterClearButton(volumeRatioGroup, "volume");
            AddIndividualFilterClearButton(pastDaysGroup, "pastDays");
            AddIndividualFilterClearButton(comparisonGroup7, "comparison7");
            AddIndividualFilterClearButton(comparisonGroup8, "comparison8");
            AddIndividualFilterClearButton(groupBox3, "comparison9");
            AddIndividualFilterClearButton(ohlcChangeFilterGroup, "ohlcChange");
        }

        private void AddIndividualFilterClearButton(Control parent, string filterKey)
        {
            var button = new Button
            {
                Text = "پاک",
                Size = new Size(42, 24),
                Location = new Point(8, 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                TabStop = false,
                RightToLeft = RightToLeft.Yes,
                Font = new Font("Segoe UI", 8.5F)
            };
            button.Click += (_, _) => ClearIndividualFilter(filterKey);
            parent.Controls.Add(button);
            button.BringToFront();
        }

        private void ClearIndividualFilter(string filterKey)
        {
            resettingFilters = true;
            try
            {
                switch (filterKey)
                {
                    case "trading":
                        statusAllRadio.Checked = true;
                        statusPositiveRadio.Checked = false;
                        statusNegativeRadio.Checked = false;
                        break;
                    case "name":
                        nameTextBox.Clear();
                        nameComboBox.SelectedIndex = -1;
                        break;
                    case "volume":
                        volumeRatioTextBox.Clear();
                        volumeRatioOperatorComboBox.SelectedIndex = -1;
                        textBox1.Clear();
                        break;
                    case "pastDays":
                        pastDaysTextBox.Clear();
                        pastDaysStatusComboBox.SelectedIndex = -1;
                        break;
                    case "comparison7":
                        ClearComparisonFilterControls(comparisonFirstComboBox1, comparisonFirstTextBox2, comparisonOperatorComboBox2, comparisonSecondComboBox1, comparisonSecondTextBox2);
                        break;
                    case "comparison8":
                        ClearComparisonFilterControls(comparisonFirstComboBox2, comparisonFirstTextBox1, comparisonOperatorComboBox1, comparisonSecondComboBox2, comparisonSecondTextBox1);
                        break;
                    case "comparison9":
                        ClearComparisonFilterControls(comparisonFirstComboBox3, comparisonFirstTextBox3, comparisonOperatorComboBox3, comparisonSecondComboBox3, comparisonSecondTextBox3);
                        break;
                    case "ohlcChange":
                        ohlcChangeFieldComboBox.SelectedIndex = -1;
                        ohlcChangeDaysTextBox.Clear();
                        ohlcChangePercentTextBox.Clear();
                        ohlcChangeDirectionComboBox.SelectedIndex = -1;
                        break;
                }
            }
            finally
            {
                resettingFilters = false;
            }

            NameFilterChanged(this, EventArgs.Empty);
            UpdateFilterControlAvailability();
        }

        private static void ClearComparisonFilterControls(ComboBox firstFieldComboBox, TextBox firstOffsetTextBox, ComboBox operatorComboBox, ComboBox secondFieldComboBox, TextBox secondOffsetTextBox)
        {
            firstFieldComboBox.SelectedIndex = -1;
            firstOffsetTextBox.Clear();
            operatorComboBox.SelectedIndex = -1;
            secondFieldComboBox.SelectedIndex = -1;
            secondOffsetTextBox.Clear();
        }

'''

method_anchor = "        private void FullScreenChartButton_Click(object? sender, EventArgs e)\n"
if method_anchor not in s:
    raise SystemExit("method anchor not found")
s = s.replace(method_anchor, method + method_anchor, 1)
p.write_text(s, encoding="utf-8-sig")
