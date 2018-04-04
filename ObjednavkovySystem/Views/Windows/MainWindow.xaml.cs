using MahApps.Metro.Controls;
using ObjednavkovySystem.Services;
using ObjednavkovySystem.Views.Pages;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using System.Net.NetworkInformation;

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
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            Timer timer = new Timer(300000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            mainFrame.Navigate(new LoginPage());
        }

        private async void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                await SyncService.Instance().SyncAsync();
                await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => RefreshFrame()));
            }
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await SyncService.Instance().SyncAsync();
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => RefreshFrame()));
        }

        private void RefreshFrame()
        {
            mainFrame.Refresh();
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
            RefreshFrame();
        }
    }
}