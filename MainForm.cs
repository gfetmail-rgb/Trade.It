using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Trade.It
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, PortfolioDefinition> loadedPortfolios = new(StringComparer.OrdinalIgnoreCase);
        private bool internalPortfolioUpdate;
        private string? displayedPortfolioName;
        private bool tradingStatusFilterInitialized;
        private bool resettingFilters;
        private bool comparisonFiltersInitialized;
        private bool comparisonFilterEventsAttached;
        private bool ohlcChangeFilterEventsAttached;
        private int latestTradeDateLoadVersion;
        private TextBox textBox1;

        public MainForm()
        {
            InitializeComponent();

            // The WinForms designer creates MainForm inside the design-tools process.
            // Runtime-only initialization must not execute while the designer is loading.
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            HandleCreated += MainForm_HandleCreatedForChartTabs;

            mainMenuStrip.RightToLeft = RightToLeft.Yes;

            portfolioDefinitionMenuItem.Click += (_, _) =>
            {
                using var form = new PortfolioDefinitionForm();
                form.ShowDialog(this);
                RefreshPortfolioListAndClearSelection();
            };

            portfolioManagementMenuItem.Click += (_, _) =>
            {
                using var form = new PortfolioManagementForm();
                form.ShowDialog(this);
                RefreshPortfolioListAndClearSelection();
            };


            portfolioComboBox.SelectedIndexChanged += PortfolioComboBox_SelectedIndexChanged;
            newPortfolioButton.Click += NewPortfolioButton_Click;
            refreshButton.Click += RefreshStocksButton_Click;
            refreshButtonPortfolio.Click += RefreshPortfolioButton_Click;
            deleteButton.Click += DeleteButton_Click;
            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChanged;
            selectNoneCheckBox.CheckedChanged += SelectNoneCheckBox_CheckedChanged;
            stocksDataGridView.CurrentCellDirtyStateChanged += StocksDataGridView_CurrentCellDirtyStateChanged;
            stocksDataGridView.CellValueChanged += StocksDataGridView_CellValueChanged;
            Load += MainForm_Portfolios_Load;
            fullScreenChartButton.Click += FullScreenChartButton_Click;

            AttachOhlcChangeFilterEvents();
            InitializeFilterComboEmptyOptions();
        }

        private void InitializeFilterComboEmptyOptions()
        {
            var filterCombos = new[]
            {
                nameComboBox,
                volumeRatioOperatorComboBox,
                pastDaysStatusComboBox,
                comparisonFirstComboBox1, comparisonOperatorComboBox1, comparisonSecondComboBox1,
                comparisonFirstComboBox2, comparisonOperatorComboBox2, comparisonSecondComboBox2,
                comparisonFirstComboBox3, comparisonOperatorComboBox3, comparisonSecondComboBox3,
                ohlcChangeFieldComboBox, ohlcChangeDirectionComboBox
            };

            foreach (var comboBox in filterCombos)
            {
                if (comboBox.Items.Count == 0 || !string.IsNullOrEmpty(comboBox.Items[0]?.ToString()))
                    comboBox.Items.Insert(0, string.Empty);
                comboBox.SelectedIndex = -1;
            }