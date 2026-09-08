namespace Trade.It
{
    public partial class PortfolioManagementForm : Form
    {
        public PortfolioManagementForm()
        {
            InitializeComponent();
            InitializeParameterRows();
        }

        private void InitializeParameterRows()
        {
            parameterTable.RowStyles.Clear();
            for (int i = 0; i < 6; i++)
            {
                parameterTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            }
        }
    }
}
