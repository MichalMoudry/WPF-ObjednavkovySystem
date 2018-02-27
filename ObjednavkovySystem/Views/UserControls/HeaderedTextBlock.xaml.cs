using System.Windows.Controls;
using System.Windows;

namespace ObjednavkovySystem.Views.UserControls
{
    /// <summary>
    /// Interakční logika pro HeaderedTextBlock.xaml
    /// </summary>
    public partial class HeaderedTextBlock : UserControl
    {
        public HeaderedTextBlock(string header, string content, bool edit = false)
        {
            InitializeComponent();
            Header.Text = header;
            if (edit.Equals(false))
            {
                ControlContent.Visibility = Visibility.Visible;
                ControlContent.Text = content;
            }
            else
            {
                TextBoxContent.Visibility = Visibility.Visible;
                TextBoxContent.Text = content;
            }
        }
    }
}