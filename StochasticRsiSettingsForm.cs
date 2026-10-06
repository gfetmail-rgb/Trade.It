namespace Trade.It
{
    public partial class StochasticRsiSettingsForm : Form
    {
        public bool ShowK => showKCheckBox.Checked;
        public bool ShowD => showDCheckBox.Checked;
        public bool Show20 => show20CheckBox.Checked;
        public bool Show80 => show80CheckBox.Checked;
        public Color KColor => kColorButton.BackColor;
        public Color DColor => dColorButton.BackColor;
        public Color C20Color => c20ColorButton.BackColor;
        public Color C80Color => c80ColorButton.BackColor;

        internal StochasticRsiSettingsForm(ChartIndicator indicator)
        {
            InitializeComponent();
            showKCheckBox.Checked = indicator.ShowStochasticRsiK;
            showDCheckBox.Checked = indicator.ShowStochasticRsiD;
            show20CheckBox.Checked = indicator.ShowStochasticRsi20;
            show80CheckBox.Checked = indicator.ShowStochasticRsi80;
            kColorButton.BackColor = indicator.StochasticRsiKColor;
            dColorButton.BackColor = indicator.StochasticRsiDColor;
            c20ColorButton.BackColor = indicator.StochasticRsi20Color;
            c80ColorButton.BackColor = indicator.StochasticRsi80Color;
        }

        private void ChooseColor(Button button)
        {
            using var dialog = new ColorDialog { Color = button.BackColor };
            if (dialog.ShowDialog(this) == DialogResult.OK)
                button.BackColor = dialog.Color;
        }

        private void kColorButton_Click(object sender, EventArgs e) => ChooseColor(kColorButton);
        private void dColorButton_Click(object sender, EventArgs e) => ChooseColor(dColorButton);
        private void c20ColorButton_Click(object sender, EventArgs e) => ChooseColor(c20ColorButton);
        private void c80ColorButton_Click(object sender, EventArgs e) => ChooseColor(c80ColorButton);
    }
}