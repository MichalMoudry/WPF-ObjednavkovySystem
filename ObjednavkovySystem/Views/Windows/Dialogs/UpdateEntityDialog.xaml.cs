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
using ObjednavkovySystem.Models;
using ObjednavkovySystem.Views.UserControls;
using ObjednavkovySystem.ViewModels;

namespace ObjednavkovySystem.Views.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro UpdateEntityDialog.xaml
    /// </summary>
    public partial class UpdateEntityDialog : Window
    {
        public UpdateEntityDialog(Order order)
        {
            InitializeComponent();
            SetContentForOrderEntity(order);
        }

        public UpdateEntityDialog(Employee employee)
        {
            InitializeComponent();
            SetContentForEmployeeEntity(employee);
        }

        public UpdateEntityDialog(Customer customer)
        {
            InitializeComponent();
            SetContentForCustomerEntity(customer);
        }

        public UpdateEntityDialog(Car car)
        {
            InitializeComponent();
            SetContentForCarEntity(car);
        }

        private void SetContentForCustomerEntity(Customer customer)
        {
            rootPanel.Children.Add(new StackPanel()
            {
                Children =
                {
                    new HeaderedTextBlock("Jméno zaměstnance", $"{customer.Name}", true),
                    new HeaderedTextBlock("Přidán", $"{customer.Added}")
                }
            });
        }

        private void SetContentForCarEntity(Car car)
        {
            rootPanel.Children.Add(new StackPanel()
            {
                Children =
                {
                    new HeaderedTextBlock("Název auta", $"{car.Name}", true),
                    new HeaderedTextBlock("Přidáno", $"{car.Added}")
                }
            });
        }

        public async void SetContentForOrderEntity(Order order)
        {
            Customer customer = await CustomerViewModel.Instance().GetCustomerByID(order.UserID);
            rootPanel.Children.Add(new StackPanel()
            {
                Children =
                {
                    new HeaderedTextBlock("Číslo objednávky", $"{order.ID}"),
                    new HeaderedTextBlock("Zákazník", $"{customer.Name}"),
                    new HeaderedTextBlock("Záznam vytvořen", $"{order.Rented}")
                }
            });
        }

        private void SetContentForEmployeeEntity(Employee employee)
        {
            rootPanel.Children.Add(new StackPanel()
            {
                Children =
                {
                    new HeaderedTextBlock("Jméno zaměstnance", $"{employee.Name}", true),
                    new HeaderedTextBlock("Role zaměstnance", $"{employee.Role}"),
                    new HeaderedTextBlock("Vytvořen", $"{employee.Added}")
                }
            });
        }
    }
}
