using CryptSharp;
using MahApps.Metro.Controls;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ObjednavkovySystem.Views.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro AddEntityDialog.xaml
    /// </summary>
    public partial class AddEntityDialog : MetroWindow
    {
        private string entityType;

        public AddEntityDialog(Transactions order)
        {
            InitializeComponent();
            entityType = "Order";
            SetUIForOrders();
        }

        public AddEntityDialog(Customer customer)
        {
            InitializeComponent();
            entityType = "Customer";
            CustomerBlock.Visibility = Visibility.Visible;
        }

        public AddEntityDialog(Car car)
        {
            InitializeComponent();
            entityType = "Car";
            CarBlock.Visibility = Visibility.Visible;
        }

        public AddEntityDialog(Employee employee)
        {
            InitializeComponent();
            entityType = "Employee";
            EmployeeBlock.Visibility = Visibility.Visible;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (entityType.Equals("Order"))
            {
                if (ValidateOrderBlockForm())
                {
                    Transactions newOrder = new Transactions() { Added = DateTime.Now, IsDone = 0, Rented = DateTime.Now, LastUpdated = DateTime.Now };
                    newOrder.UserID = (CustomerPicker.SelectedItem as Customer).ID;
                    newOrder.CarID = (CarPicker.SelectedItem as Car).ID;
                    newOrder.EmployeeID = (EmployeePicker.SelectedItem as Employee).ID;
                    Car tempCar = await CarViewModel.Instance().GetCarByID(newOrder.CarID);
                    tempCar.IsLent = 1;
                    await CarViewModel.Instance().UpdateCar(tempCar);
                    await TransactionsViewModel.Instance().InsertOrder(newOrder);
                }
            }
            else if (entityType.Equals("Customer"))
            {
                if (ValidateCustomerBlockForm())
                {
                    Customer customer = new Customer() { Added = DateTime.Now, LastUpdated = DateTime.Now };
                    customer.Name = CustomerName.Text;
                    await CustomerViewModel.Instance().InsertCustomer(customer);
                }
            }
            else if (entityType.Equals("Car"))
            {
                if (ValidateCarBlockForm())
                {
                    Car car = new Car() { Added = DateTime.Now, LastUpdated = DateTime.Now, IsLent = 0 };
                    car.Name = CarName.Text;
                    await CarViewModel.Instance().InsertCar(car);
                }
            }
            else if (entityType.Equals("Employee"))
            {
                if (ValidateEmployeeBlockForm())
                {
                    Employee employee = new Employee() { Added = DateTime.Now, LastUpdated = DateTime.Now, Role = "Zaměstnanec" };
                    employee.Name = EmployeeName.Text;
                    employee.Password = Crypter.Blowfish.Crypt(EmployeePass.Password);
                    await EmployeeViewModel.Instance().InsertEmployee(employee);
                }
            }
            Hide();
        }

        private async void SetUIForOrders()
        {
            OrdersBlock.Visibility = Visibility.Visible;
            List<Car> cars = await CarViewModel.Instance().GetCarsAsList();
            CustomerPicker.ItemsSource = await CustomerViewModel.Instance().GetCustomersAsList();
            CarPicker.ItemsSource = cars.Where(i => i.IsLent == 0);
            EmployeePicker.ItemsSource = await EmployeeViewModel.Instance().GetEmployeesAsList();
        }

        private bool ValidateCarBlockForm()
        {
            if (string.IsNullOrEmpty(CarName.Text).Equals(false))
            {
                return true;
            }
            return false;
        }

        private bool ValidateCustomerBlockForm()
        {
            if (string.IsNullOrEmpty(CustomerName.Text).Equals(false))
            {
                return true;
            }
            return false;
        }

        private bool ValidateEmployeeBlockForm()
        {
            if (string.IsNullOrEmpty(EmployeeName.Text).Equals(false) && string.IsNullOrEmpty(EmployeePass.Password).Equals(false))
            {
            }
            return false;
        }

        private bool ValidateOrderBlockForm()
        {
            if (CustomerPicker.SelectedItem != null && CarPicker.SelectedItem != null && EmployeePicker.SelectedItem != null)
            {
                return true;
            }
            return false;
        }
    }
}