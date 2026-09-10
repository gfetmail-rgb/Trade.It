namespace Trade.It
{
    public partial class MainForm
    {
        private bool ohlcChangeFilterDesignerBridgeAttached;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (ohlcChangeFilterDesignerBridgeAttached)
                return;

            ohlcChangeFilterDesignerBridgeAttached = true;
            InitializeOhlcChangeFilter();
        }
    }
}
