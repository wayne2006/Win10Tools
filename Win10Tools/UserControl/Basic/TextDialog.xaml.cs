namespace Win10Tools.UserControl
{
    public partial class TextDialog
    {
        public string Text { get; set; }
        public TextDialog(string text)
        {
            Text = text;
            InitializeComponent();
            
        }
    }
}
