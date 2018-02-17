using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace ObjednavkovySystem.ViewModels
{
    internal class OrderViewModel
    {
        private static OrderViewModel _instance;
        private ObservableCollection<Order> _observableCollOrders;
        private OrderDatabase orderDatabase;

        protected OrderViewModel()
        {
            orderDatabase = new OrderDatabase("OrderDatabase.db3");
        }

        public static OrderViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new OrderViewModel();
            }
            return _instance;
        }

        public async Task DeleteOrder(Order order, bool toSyncContext = true)
        {
            order.IsDeleted = 1;
            await orderDatabase.UpdateEntity(order);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Order", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollOrders != null)
            {
                _observableCollOrders.Remove(order);
            }
        }

        public async Task<Order> GetOrderByID(int id)
        {
            return await orderDatabase.GetEntityByID(id);
        }

        public async Task<List<Order>> GetOrdersAsList()
        {
            return await orderDatabase.GetEntitesAsList();
        }

        public async Task<ObservableCollection<Order>> GetOrdersAsObservable()
        {
            _observableCollOrders = new ObservableCollection<Order>(await GetOrdersAsList());
            return _observableCollOrders;
        }

        public async Task InsertOrder(Order order, bool toSyncContext = true)
        {
            await orderDatabase.SaveEntity(order);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Order", Operation = "Create", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollOrders != null)
            {
                _observableCollOrders.Add(order);
            }
        }

        public async Task UpdateOrder(Order order, bool toSyncContext = true)
        {
            await orderDatabase.UpdateEntity(order);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = order.ID, EntityType = "Order", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
        }
    }
}