using MahApps.Metro.Controls;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.ViewModels;
using System.Windows;
using System;
using CryptSharp;

namespace ObjednavkovySystem.Views.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro UpdateEntityDialog.xaml
    /// </summary>
    public partial class UpdateEntityDialog : MetroWindow
    {
        private int entityID;
        private string entityType;

        public UpdateEntityDialog(Transactions order)
        {
            InitializeComponent();
            entityType = "Order";
            entityID = order.ID;
            SetContentForOrderEntity(order);
        }

        public UpdateEntityDialog(Employee employee)
        {
            InitializeComponent();
            entityType = "Employee";
            entityID = employee.ID;
            SetContentForEmployeeEntity(employee);
        }

        public UpdateEntityDialog(Customer customer)
        {
            InitializeComponent();
            entityType = "Customer";
            entityID = customer.ID;
            SetContentForCustomerEntity(customer);
        }

        public UpdateEntityDialog(Car car)
        {
            InitializeComponent();
            entityType = "Car";
            entityID = car.ID;
            SetContentForCarEntity(car);
        }

        public async void SetContentForOrderEntity(Transactions transaction)
        {
            OrderPanel.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            Title = "objednávku";
            Customer customer = await CustomerViewModel.Instance().GetCustomerByID(transaction.UserID);
            Employee employee = await EmployeeViewModel.Instance().GetEmployeeByID(transaction.EmployeeID);
            Car tempCar = await CarViewModel.Instance().GetCarByID(transaction.CarID);
            transactionTextBlock.Text = $"č. {transaction.ID}";
            customerTextBlock.Text = $"{customer.Name}";
            employeeTextBlock.Text = $"{employee.Name}";
            carTextBlock.Text = $"{tempCar.Name}";
            AddedTextBlock.Text += $"{transaction.Added.ToString("yyyy/MM/dd")}";
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Car car;
            switch (entityType)
            {
                case "Order":
                    Transactions transaction = await TransactionsViewModel.Instance().GetOrderByID(entityID);
                    car = await CarViewModel.Instance().GetCarByID(transaction.CarID);
                    car.IsLent = 0;
                    await CarViewModel.Instance().UpdateCar(car);
                    await TransactionsViewModel.Instance().DeleteOrder(transaction);
                    break;

                case "Customer":
                    await CustomerViewModel.Instance().DeleteCustomer(await CustomerViewModel.Instance().GetCustomerByID(entityID));
                    break;

                case "Employee":
                    await EmployeeViewModel.Instance().DeleteEmployee(await EmployeeViewModel.Instance().GetEmployeeByID(entityID));
                    break;

                case "Car":
                    car = await CarViewModel.Instance().GetCarByID(entityID);
                    await CarViewModel.Instance().DeleteCar(car);
                    break;

                default:
                    break;
            }
            Hide();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (entityType.Equals("Car"))
            {
                if (ValidateCarPanelForm())
                {
                    Car car = await CarViewModel.Instance().GetCarByID(entityID);
                    car.LastUpdated = DateTime.Now;
                    car.Name = carNameTextBlock.Text;
                    await CarViewModel.Instance().UpdateCar(car);
                }
            }
            else if (entityType.Equals("Employee"))
            {
                if (ValidateEmployeePanelForm())
                {
                    Employee employee = await EmployeeViewModel.Instance().GetEmployeeByID(entityID);
                    employee.LastUpdated = DateTime.Now;
                    employee.Name = employeeNameTextBox.Text;
                    if (string.IsNullOrEmpty(employeePass.Password).Equals(false))
                    {
                        employee.Password = Crypter.Blowfish.Crypt(employeePass.Password);
                    }
                    await EmployeeViewModel.Instance().UpdateEmployee(employee);
                }
            }
            else if (entityType.Equals("Customer"))
            {
                if (ValidateCustomerPanelForm())
                {
                    Customer customer = await CustomerViewModel.Instance().GetCustomerByID(entityID);
                    customer.Name = customerNameTextBlock.Text;
                    customer.LastUpdated = DateTime.Now;
                    await CustomerViewModel.Instance().UpdateCustomer(customer);
                }
            }
            Hide();
        }

        private void SetContentForCarEntity(Car car)
        {
            Title += "auto";
            CarPanel.Visibility = Visibility.Visible;
            carNameTextBlock.Text = car.Name;
            AddedTextBlock.Text += $"{car.Added.ToString("yyyy/MM/dd")}";
            lentCheckBox.IsChecked = Convert.ToBoolean(car.IsLent);
        }

        private void SetContentForCustomerEntity(Customer customer)
        {
            Title += "zákazníka";
            CustomerPanel.Visibility = Visibility.Visible;
            customerNameTextBlock.Text = customer.Name;
            AddedTextBlock.Text += $"{customer.Added.ToString("yyyy/MM/dd")}";
        }

        private void SetContentForEmployeeEntity(Employee employee)
        {
            Title += "zaměstnance";
            EmployeePanel.Visibility = Visibility.Visible;
            employeeNameTextBox.Text = employee.Name;
            AddedTextBlock.Text += $"{employee.Added.ToString("yyyy/MM/dd")}";
        }

        private bool ValidateCarPanelForm()
        {
            if (string.IsNullOrEmpty(carNameTextBlock.Text).Equals(false))
            {
                return true;
            }
            return false;
        }

        private bool ValidateCustomerPanelForm()
        {
            if (string.IsNullOrEmpty(customerNameTextBlock.Text).Equals(false))
            {
                return true;
            }
            return false;
        }

        private bool ValidateEmployeePanelForm()
        {
            if (string.IsNullOrEmpty(employeeNameTextBox.Text).Equals(false))
            {
                return true;
            }
            return false;
        }
    }
}