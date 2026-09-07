namespace Trade.It
{
    partial class Form1
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyFilterLayout();
        }

        private void ApplyFilterLayout()
        {
            tabPage2.AutoScroll = true;

            tradingStatusGroup.Dock = DockStyle.Top;
            nameFilterGroup.Dock = DockStyle.Top;
            volumeRatioGroup.Dock = DockStyle.Top;
            pastDaysGroup.Dock = DockStyle.Top;
            comparisonGroup.Dock = DockStyle.Top;

            tradingStatusGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameFilterGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeRatioGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pastDaysGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comparisonGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            nameComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            statusAllRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statusPositiveRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statusNegativeRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeRatioOperatorComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeRatioTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            pastDaysTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pastDaysStatusComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            comparisonFirstComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonFirstTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonOperatorComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comparisonSecondTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void pastDaysStatusComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }
    }
}