using ObjednavkovySystem.Models;
using ObjednavkovySystem.Views.Windows.Dialogs;

namespace ObjednavkovySystem.Services
{
    internal class DialogService
    {
        private static DialogService _instance;
        private AddEntityDialog _addEntityDialog;
        private DeleteDialog _deleteDialog;
        private UpdateEntityDialog _updateEntityDialog;

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

        public void ShowDeleteDialog(string title)
        {
            _deleteDialog = new DeleteDialog(title);
            _deleteDialog.ShowDialog();
        }

        public void ShowUpdateDialog(Transactions order)
        {
            _updateEntityDialog = new UpdateEntityDialog(order);
            _updateEntityDialog.ShowDialog();
        }

        public void ShowUpdateDialog(Employee employee)
        {
            _updateEntityDialog = new UpdateEntityDialog(employee);
            _updateEntityDialog.ShowDialog();
        }

        public void ShowUpdateDialog(Customer customer)
        {
            _updateEntityDialog = new UpdateEntityDialog(customer);
            _updateEntityDialog.ShowDialog();
        }

        public void ShowUpdateDialog(Car car)
        {
            _updateEntityDialog = new UpdateEntityDialog(car);
            _updateEntityDialog.ShowDialog();
        }

        public async void ShowAddEntityDialog(Transactions order)
        {
            await SyncService.Instance().SyncAsync();
            _addEntityDialog = new AddEntityDialog(order);
            _addEntityDialog.ShowDialog();
        }

        public async void ShowAddEntityDialog(Car car)
        {
            await SyncService.Instance().SyncAsync();
            _addEntityDialog = new AddEntityDialog(car);
            _addEntityDialog.ShowDialog();
        }

        public async void ShowAddEntityDialog(Customer customer)
        {
            await SyncService.Instance().SyncAsync();
            _addEntityDialog = new AddEntityDialog(customer);
            _addEntityDialog.ShowDialog();
        }

        public async void ShowAddEntityDialog(Employee employee)
        {
            await SyncService.Instance().SyncAsync();
            _addEntityDialog = new AddEntityDialog(employee);
            _addEntityDialog.ShowDialog();
        }
    }
}