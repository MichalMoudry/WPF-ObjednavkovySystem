using MahApps.Metro.Controls;
using ObjednavkovySystem.Services;
using ObjednavkovySystem.Views.Pages;
using System.Windows;

namespace ObjednavkovySystem.Views.Windows
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new LoginPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new LoginPage());
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Refresh();
        }

        private async void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            await SyncService.Instance().SyncAsync();
            mainFrame.Refresh();
        }
    }
}