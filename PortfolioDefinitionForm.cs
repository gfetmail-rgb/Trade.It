namespace Trade.It
{
    public partial class PortfolioDefinitionForm : Form
    {
        public PortfolioDefinitionForm()
        {
            InitializeComponent();
            InitializeMappingRows();
        }

        private void InitializeMappingRows()
        {
            mappingGrid.Rows.Clear();
            foreach (var field in new[]
            {
                "نماد", "تاریخ", "زمان", "Open", "High", "Low", "Close", "حجم",
                "قیمت پایانی امروز", "قیمت پایانی دیروز", "تعداد معامله", "ارزش معاملات",
                "تعداد سهم", "ارزش بازار", "نماد انگلیسی"
            })
            {
                mappingGrid.Rows.Add(field, "");
            }
        }
    }
}
