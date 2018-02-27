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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ObjednavkovySystem.ViewModels;
using ObjednavkovySystem.Models;

namespace ObjednavkovySystem.Views.Pages
{
    /// <summary>
    /// Interakční logika pro LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await ProcessLogin();
        }

        private async Task ProcessLogin()
        {
            loadingControl.Visibility = Visibility.Visible;
            await Task.Delay(200);
            if (ValidateForm())
            {
                Employee authRes = await EmployeeViewModel.Instance().AuthAsync(new Employee() { Name = userName.Text, Password = userPass.Password });
                if (authRes != null)
                {
                    NavigationService.Navigate(new ShellPage(authRes.Role));
                }
                else
                {
                    inAppNotification.Show("Ověření neproběhlo úspěšně", "#cc291a");
                }
            }
            else
            {
                inAppNotification.Show("Formulář nebyl vyplněn správně", "#cc291a");
            }
            loadingControl.Visibility = Visibility.Collapsed;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(userName.Text).Equals(false) &&
                string.IsNullOrEmpty(userPass.Password).Equals(false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void UserPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                await ProcessLogin();
            }
        }
    }
}
