using ObjednavkovySystem.Models;
using ObjednavkovySystem.Views.Windows.Dialogs;

namespace ObjednavkovySystem.Services
{
    internal class DialogService
    {
        private static DialogService _instance;
        private AddEntityDialog _addEntityDialog;

        protected DialogService()
        {
        }

        public static DialogService Instance()
        {
            if (_instance == null)
            {
                _instance = new DialogService();
            }
            return _instance;
        }

        public void ShowAddEntityDialog(Order order)
        {
            _addEntityDialog = new AddEntityDialog(order);
            _addEntityDialog.ShowDialog();
        }

        public void ShowAddEntityDialog(Car car)
        {
            _addEntityDialog = new AddEntityDialog(car);
            _addEntityDialog.ShowDialog();
        }

        public void ShowAddEntityDialog(Customer customer)
        {
            _addEntityDialog = new AddEntityDialog(customer);
            _addEntityDialog.ShowDialog();
        }

        public void ShowAddEntityDialog(Employee employee)
        {
            _addEntityDialog = new AddEntityDialog(employee);
            _addEntityDialog.ShowDialog();
        }
    }
}