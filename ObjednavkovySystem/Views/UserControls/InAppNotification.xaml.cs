using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ObjednavkovySystem.Views.UserControls
{
    /// <summary>
    /// Interakční logika pro InAppNotification.xaml
    /// </summary>
    public partial class InAppNotification : UserControl
    {
        private DispatcherTimer _timer;

        public InAppNotification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for collapsing notification.
        /// </summary>
        public void Hide()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Method for displaying In-app notification.
        /// </summary>
        /// <param name="content">Content to be displayed.</param>
        /// <param name="color">Color of the notification.</param>
        public void Show(string content, string color)
        {
            Visibility = Visibility.Visible;
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 6)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            RootGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            NotificationContent.Text = content;
        }

        /// <summary>
        /// CloseButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Tick event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments</param>
        private void Timer_Tick(object sender, object e)
        {
            Hide();
            (sender as DispatcherTimer).Stop();
        }
    }
}