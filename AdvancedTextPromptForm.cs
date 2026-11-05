namespace Trade.It
{
    internal sealed partial class AdvancedTextPromptForm : Form
    {
        public AdvancedTextPromptForm()
        {
            InitializeComponent();
            Shown += (_, _) => textBox.Focus();
        }

        public string InputText => textBox.Text;
    }
}
