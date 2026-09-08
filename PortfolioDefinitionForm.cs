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
            // Ensure the grid has its columns before adding rows.
            // This also keeps the form safe if the WinForms designer recreates
            // the control without serializing its column collection.
            if (mappingGrid.Columns.Count == 0)
            {
                mappingGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "فیلد استاندارد",
                    Width = 155,
                    Name = "standardField",
                    ReadOnly = true
                });

                mappingGrid.Columns.Add(new DataGridViewComboBoxColumn
                {
                    HeaderText = "ستون فایل",
                    Width = 155,
                    Name = "fileColumn"
                });
            }

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
