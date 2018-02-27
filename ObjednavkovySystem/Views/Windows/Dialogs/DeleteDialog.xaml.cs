using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace ObjednavkovySystem.Views.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : MetroWindow
    {
        public DeleteDialog(string entityName)
        {
            InitializeComponent();
            Title = $"Smazat '{entityName}'";
        }
    }
}
