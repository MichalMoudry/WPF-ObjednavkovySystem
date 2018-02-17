using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class CustomerViewModel
    {
        private static CustomerViewModel _instance;
        private ObservableCollection<Customer> _obervableCollCustomers;
        private CustomerDatabase customerDatabase;

        protected CustomerViewModel()
        {
            customerDatabase = new CustomerDatabase("CustomerDatabase.db3");
        }

        public static CustomerViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CustomerViewModel();
            }
            return _instance;
        }

        public async Task DeleteCustomer(Customer customer, bool toSyncContext = true)
        {
            customer.IsDeleted = 1;
            await customerDatabase.UpdateEntity(customer);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = customer.ID, EntityType = "Customer", Operation = "Delete", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_obervableCollCustomers != null)
            {
                _obervableCollCustomers.Remove(customer);
            }
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await customerDatabase.GetEntityByID(id);
        }

        public async Task<List<Customer>> GetCustomersAsList()
        {
            return await customerDatabase.GetEntitesAsList();
        }

        public async Task<ObservableCollection<Customer>> GetCustomersAsObservable()
        {
            _obervableCollCustomers = new ObservableCollection<Customer>(await GetCustomersAsList());
            return _obervableCollCustomers;
        }

        public async Task InsertCustomer(Customer customer, bool toSyncContext = true)
        {
            await customerDatabase.SaveEntity(customer);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = customer.ID, EntityType = "Customer", Operation = "Create", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_obervableCollCustomers != null)
            {
                _obervableCollCustomers.Add(customer);
            }
        }

        public async Task UpdateCutomer(Customer customer, bool toSyncContext = true)
        {
            await customerDatabase.UpdateEntity(customer);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = customer.ID, EntityType = "Customer", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
        }
    }
}