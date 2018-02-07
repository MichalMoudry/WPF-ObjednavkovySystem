using System.Windows.Controls;

namespace ObjednavkovySystem.Views.UserControls
{
    /// <summary>
    /// Interakční logika pro HeaderedTextBlock.xaml
    /// </summary>
    public partial class HeaderedTextBlock : UserControl
    {
        public HeaderedTextBlock(string header, string content)
        {
            InitializeComponent();
            Header.Text = header;
            ControlContent.Text = content;
        }
    }
}