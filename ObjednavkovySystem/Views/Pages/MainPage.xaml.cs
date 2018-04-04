using ObjednavkovySystem.Models;
using ObjednavkovySystem.Services;
using ObjednavkovySystem.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ObjednavkovySystem.Views.Pages
{
    /// <summary>
    /// Interakční logika pro ShellPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(string userRole)
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string buttonName = (sender as Button).Name;
            switch (buttonName)
            {
                case "AddOrderButton":
                    DialogService.Instance().ShowAddEntityDialog((Transactions)OrdersList.SelectedItem);
                    break;

                case "AddCustomerButton":
                    DialogService.Instance().ShowAddEntityDialog((Customer)OrdersList.SelectedItem);
                    break;

                case "AddCarButton":
                    DialogService.Instance().ShowAddEntityDialog((Car)OrdersList.SelectedItem);
                    break;

                case "AddEmployeeButton":
                    DialogService.Instance().ShowAddEntityDialog((Employee)OrdersList.SelectedItem);
                    break;

                default:
                    break;
            }
        }

        private async void List_Loaded(object sender, RoutedEventArgs e)
        {
            string list = (sender as ListView).Name;
            switch (list)
            {
                case "OrdersList":
                    OrdersList.ItemsSource = await TransactionsViewModel.Instance().GetOrdersAsObservable();
                    break;

                case "CustomersList":
                    CustomersList.ItemsSource = await CustomerViewModel.Instance().GetCustomersAsObservable();
                    break;

                case "CarsList":
                    CarsList.ItemsSource = await CarViewModel.Instance().GetCarsAsObservable();
                    break;

                case "EmployeesList":
                    EmployeesList.ItemsSource = await EmployeeViewModel.Instance().GetEmployeesAsObservable();
                    break;

                default:
                    break;
            }
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView senderList = (ListView)sender;
            if (senderList.SelectedItem != null)
            {
                object selectedItem = senderList.SelectedItem;
                if (senderList.Name.Equals("OrdersList"))
                {
                    DialogService.Instance().ShowUpdateDialog((Transactions)selectedItem);
                }
                else if (senderList.Name.Equals("CustomersList"))
                {
                    DialogService.Instance().ShowUpdateDialog((Customer)selectedItem);
                }
                else if (senderList.Name.Equals("CarsList"))
                {
                    DialogService.Instance().ShowUpdateDialog((Car)selectedItem);
                }
                else if (senderList.Name.Equals("EmployeesList"))
                {
                    DialogService.Instance().ShowUpdateDialog((Employee)selectedItem);
                }
                senderList.SelectedIndex = -1;
            }
        }
    }
}