using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class TransactionsViewModel
    {
        private static TransactionsViewModel _instance;
        private ObservableCollection<Transactions> _observableCollOrders;
        private TransactionsDatabase orderDatabase;

        protected TransactionsViewModel()
        {
            orderDatabase = new TransactionsDatabase("TransactionDatabase.db3");
        }

        public static TransactionsViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new TransactionsViewModel();
            }
            return _instance;
        }

        public async Task DeleteOrder(Transactions order, bool toSyncContext = true, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                await orderDatabase.DeleteEntity(order);
            }
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Transaction", Operation = "Delete", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollOrders != null)
            {
                _observableCollOrders.Remove(order);
            }
        }

        public async Task<Transactions> GetLastEntity()
        {
            List<Transactions> list = await GetOrdersAsList();
            return list.Last();
        }

        public async Task<Transactions> GetOrderByID(int id)
        {
            return await orderDatabase.GetEntityByID(id);
        }

        public async Task<List<Transactions>> GetOrdersAsList()
        {
            return await orderDatabase.GetEntitesAsList();
        }

        public async Task<ObservableCollection<Transactions>> GetOrdersAsObservable()
        {
            _observableCollOrders = new ObservableCollection<Transactions>(await GetOrdersAsList());
            return _observableCollOrders;
        }

        public async Task InsertOrder(Transactions order, bool toSyncContext = true)
        {
            await orderDatabase.SaveEntity(order);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Transaction", Operation = "Create", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollOrders != null)
            {
                _observableCollOrders.Add(order);
            }
        }

        public async Task UpdateOrder(Transactions order, bool toSyncContext = true)
        {
            await orderDatabase.UpdateEntity(order);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Transaction", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
        }
    }
}