using MahApps.Metro.Controls;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System;

namespace ObjednavkovySystem.Views.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro AddEntityDialog.xaml
    /// </summary>
    public partial class AddEntityDialog : MetroWindow
    {
        private Thickness _itemThickness = new Thickness(0, 5, 0, 0);
        private string entityType;

        public AddEntityDialog(Order order)
        {
            InitializeComponent();
            entityType = "Order";
            SetUIForOrders();
        }

        public AddEntityDialog(Customer customer)
        {
            InitializeComponent();
            entityType = "Customer";
            SetUIForCustomers();
        }

        public AddEntityDialog(Car car)
        {
            InitializeComponent();
            entityType = "Car";
            SetUIForCars();
        }

        public AddEntityDialog(Employee employee)
        {
            InitializeComponent();
            entityType = "Employee";
            SetUIForEmployees();
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            switch (entityType)
            {
                case "Order":
                    Order newOrder = new Order() { Added = DateTime.Now, IsDone = 0, Rented = DateTime.Now, LastUpdated = DateTime.Now, IsDeleted = 0 };
                    newOrder.UserID = (CustomerPicker.SelectedItem as Customer).ID;
                    newOrder.CarID = (CarPicker.SelectedItem as Car).ID;
                    newOrder.EmployeeID = (EmployeePicker.SelectedItem as Employee).ID;
                    await OrderViewModel.Instance().InsertOrder(newOrder);
                    break;

                case "Customer":
                    Customer customer = new Customer() { Added = DateTime.Now, LastUpdated = DateTime.Now, IsDeleted = 0 };
                    customer.Name = CustomerName.Text;
                    await CustomerViewModel.Instance().InsertCustomer(customer);
                    break;

                case "Car":
                    Car car = new Car() { Added = DateTime.Now, LastUpdated = DateTime.Now, IsDeleted = 0, IsLent = 0 };
                    car.Name = CarName.Text;
                    await CarViewModel.Instance().InsertCar(car);
                    break;

                case "Employee":
                    Employee employee = new Employee() { Added = DateTime.Now, LastUpdated = DateTime.Now, IsDeleted = 0, Role = "Zaměstnanec" };
                    employee.Name = EmployeeName.Text;
                    employee.Password = EmployeePass.Password;
                    await EmployeeViewModel.Instance().InsertEmployee(employee);
                    break;

                default:
                    break;
            }
            Hide();
        }

        private async void SetUIForOrders()
        {
            OrdersBlock.Visibility = Visibility.Visible;
            CustomerPicker.ItemsSource = await CustomerViewModel.Instance().GetCustomersAsList();
            CarPicker.ItemsSource = await CarViewModel.Instance().GetCarsAsList();
            EmployeePicker.ItemsSource = await EmployeeViewModel.Instance().GetEmployeesAsList();
        }

        private void SetUIForEmployees()
        {
            EmployeeBlock.Visibility = Visibility.Visible;
        }

        private void SetUIForCars()
        {
            CarBlock.Visibility = Visibility.Visible;
        }

        private void SetUIForCustomers()
        {
            CustomerBlock.Visibility = Visibility.Visible;
        }
    }
}