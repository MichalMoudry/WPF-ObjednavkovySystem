using Newtonsoft.Json;
using ObjednavkovySystem.Helpers;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.ViewModels;
using RestSharp;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System;

namespace ObjednavkovySystem.Services
{
    internal class SyncService
    {
        private static SyncService _instance;

        //Local.
        private Car _localCar;
        private Customer _localCustomer;
        private Employee _localEmployee;
        private Transactions _localOrder;

        //Remote.
        private List<Car> _remoteCars = new List<Car>();
        private List<Customer> _remoteCustomers = new List<Customer>();
        private List<Employee> _remoteEmployees = new List<Employee>();
        private List<Transactions> _remoteOrders = new List<Transactions>();
        private Transactions _remoteTransaction;
        private Employee _remoteEmployee;
        private Customer _remoteCustomer;
        private Car _remoteCar;
        
        private bool _objectComparsion;
        private List<SyncContext> _syncContext;

        protected SyncService()
        {
        }

        public static SyncService Instance()
        {
            if (_instance == null)
            {
                _instance = new SyncService();
            }
            return _instance;
        }

        public async Task SyncAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                await PullAsync();
                await PushAsync();
            }
        }

        private async Task CompareCars()
        {
            foreach (Car remoteCar in _remoteCars)
            {
                _localCar = await CarViewModel.Instance().GetCarByID(remoteCar.ID);
                if (_localCar == null)
                {
                    await CarViewModel.Instance().InsertCar(remoteCar, false);
                }
                else
                {
                    _objectComparsion = _localCar.Equals(remoteCar);
                    if (_objectComparsion)
                    {
                        if (remoteCar.LastUpdated > _localCar.LastUpdated)
                        {
                            _localCar = remoteCar;
                            await CarViewModel.Instance().UpdateCar(_localCar, false);
                        }
                    }
                    else if (!_objectComparsion && remoteCar.ID == _localCar.ID)
                    {
                        _localCar = await CarViewModel.Instance().GetLastEntity();
                        remoteCar.ID = _localCar.ID + 1;
                        _remoteCar = await CarViewModel.Instance().GetCarByID(remoteCar.ID);
                        if (_remoteCar == null)
                        {
                            IncrementRemoteEntity(remoteCar);
                            await CarViewModel.Instance().InsertCar(remoteCar, false);
                        }
                    }
                }
            }
        }

        private async Task CompareCollections()
        {
            await CompareEmployees();
            await CompareCars();
            await CompareCustomers();
            await CompareOrders();
        }

        private async Task CompareCustomers()
        {
            foreach (Customer remoteCustomer in _remoteCustomers)
            {
                _localCustomer = await CustomerViewModel.Instance().GetCustomerByID(remoteCustomer.ID);
                if (_localCustomer == null)
                {
                    await CustomerViewModel.Instance().InsertCustomer(remoteCustomer, false);
                }
                else
                {
                    _objectComparsion = _localCustomer.Equals(remoteCustomer);
                    if (_objectComparsion)
                    {
                        if (remoteCustomer.LastUpdated > _localCustomer.LastUpdated)
                        {
                            _localCustomer = remoteCustomer;
                            await CustomerViewModel.Instance().UpdateCustomer(_localCustomer, false);
                        }
                    }
                    else if (!_objectComparsion && remoteCustomer.ID == _localCustomer.ID)
                    {
                        _localCustomer = await CustomerViewModel.Instance().GetLastEntity();
                        remoteCustomer.ID = _localCustomer.ID + 1;
                        _remoteCustomer = await CustomerViewModel.Instance().GetCustomerByID(remoteCustomer.ID);
                        if (_remoteCustomer == null)
                        {
                            IncrementRemoteEntity(remoteCustomer);
                            await CustomerViewModel.Instance().InsertCustomer(remoteCustomer, false);
                        }
                    }
                }
            }
        }

        private async Task CompareEmployees()
        {
            foreach (Employee remoteEmployee in _remoteEmployees)
            {
                _localEmployee = await EmployeeViewModel.Instance().GetEmployeeByID(remoteEmployee.ID);
                if (_localEmployee == null)
                {
                    await EmployeeViewModel.Instance().InsertEmployee(remoteEmployee, false);
                }
                else
                {
                    _objectComparsion = _localEmployee.Equals(remoteEmployee);
                    if (_objectComparsion)
                    {
                        if (remoteEmployee.LastUpdated > _localEmployee.LastUpdated)
                        {
                            _localEmployee = remoteEmployee;
                            await EmployeeViewModel.Instance().UpdateEmployee(_localEmployee, false);
                        } 
                    }
                    else if (!_objectComparsion && remoteEmployee.ID == _localEmployee.ID)
                    {
                        _localEmployee = await EmployeeViewModel.Instance().GetLastEntity();
                        remoteEmployee.ID = _localEmployee.ID + 1;
                        _remoteEmployee = await EmployeeViewModel.Instance().GetEmployeeByID(remoteEmployee.ID);
                        if (_remoteEmployee == null)
                        {
                            IncrementRemoteEntity(remoteEmployee);
                            await EmployeeViewModel.Instance().InsertEmployee(remoteEmployee, false);
                        }
                    }
                }
            }
        }

        private async Task CompareOrders()
        {
            foreach (Transactions remoteOrder in _remoteOrders)
            {
                _localOrder = await TransactionsViewModel.Instance().GetOrderByID(remoteOrder.ID);
                if (_localOrder == null)
                {
                    await TransactionsViewModel.Instance().InsertOrder(remoteOrder, false);
                }
                else
                {
                    _objectComparsion = _localOrder.Equals(remoteOrder);
                    if (_objectComparsion)
                    {
                        if (remoteOrder.LastUpdated > _localOrder.LastUpdated)
                        {
                            _localOrder = remoteOrder;
                            await TransactionsViewModel.Instance().UpdateOrder(_localOrder, false);
                        }
                    }
                    else if (!_objectComparsion && remoteOrder.ID == _localOrder.ID)
                    {
                        _localOrder = await TransactionsViewModel.Instance().GetLastEntity();
                        remoteOrder.ID = _localOrder.ID + 1;
                        _remoteTransaction = await TransactionsViewModel.Instance().GetOrderByID(remoteOrder.ID);
                        if (_remoteTransaction == null)
                        {
                            IncrementRemoteEntity(remoteOrder);
                            await TransactionsViewModel.Instance().InsertOrder(remoteOrder, false);
                        }
                    }
                }
            }
        }

        private void IncrementRemoteEntity<T>(T entity)
        {
            RestRequest request = new RestRequest(Method.POST);
            string name = "";
            if (entity is Transactions)
            {
                name = "TransactionIncrement";
            }
            else if (entity is Customer)
            {
                name = "UserIncrement";
            }
            else if (entity is Car)
            {
                name = "CarIncrement";
            }
            else if (entity is Employee)
            {
                name = "EmployeeIncrement";
            }
            request.AddParameter(name, JsonConvert.SerializeObject(entity));
            IRestResponse response = RESTHelper.Instance().Client.Execute(request);
            request.Parameters.Clear();
        }

        private async void ExecutePushOp(SyncContext contextEntry)
        {
            RestRequest request = new RestRequest(Method.POST);
            IRestResponse response = null;
            string value = null;
            if (contextEntry.EntityType.Equals("Employee"))
            {
                _localEmployee = await EmployeeViewModel.Instance().GetEmployeeByID(contextEntry.EntityID);
                value = $"{JsonConvert.SerializeObject(_localEmployee)}";
            }
            else if (contextEntry.EntityType.Equals("Customer"))
            {
                _localCustomer = await CustomerViewModel.Instance().GetCustomerByID(contextEntry.EntityID);
                value = $"{JsonConvert.SerializeObject(_localCustomer)}";
            }
            else if (contextEntry.EntityType.Equals("Car"))
            {
                _localCar = await CarViewModel.Instance().GetCarByID(contextEntry.EntityID);
                value = $"{JsonConvert.SerializeObject(_localCar)}";
            }
            else if (contextEntry.EntityType.Equals("Transaction"))
            {
                _localOrder = await TransactionsViewModel.Instance().GetOrderByID(contextEntry.EntityID);
                value = $"{JsonConvert.SerializeObject(_localOrder)}";
            }
            request.AddParameter(await GetRequestParam(contextEntry), value);
            response = RESTHelper.Instance().Client.Execute(request);
            request.Parameters.Clear();
        }

        private async Task ExecuteAdditionalOps(SyncContext contextEntry)
        {
            if (contextEntry.EntityType.Equals("Employee"))
            {
                await EmployeeViewModel.Instance().DeleteEmployee(_localEmployee, false, true);
            }
            else if (contextEntry.EntityType.Equals("Customer"))
            {
                await CustomerViewModel.Instance().DeleteCustomer(_localCustomer, false, true);
            }
            else if (contextEntry.EntityType.Equals("Car"))
            {
                await CarViewModel.Instance().DeleteCar(_localCar, false, true);
            }
            else if (contextEntry.EntityType.Equals("Transaction"))
            {
                await TransactionsViewModel.Instance().DeleteOrder(_localOrder, false, true);
            }
        }

        private async Task<string> GetRequestParam(SyncContext contextEntry)
        {
            string param = "";
            if (contextEntry.EntityType.Equals("Customer"))
            {
                contextEntry.EntityType = "User";
            }
            if (contextEntry.Operation.Equals("Create"))
            {
                param = contextEntry.EntityType;
            }
            else if (contextEntry.Operation.Equals("Delete"))
            {
                param = $"{contextEntry.EntityType}{contextEntry.Operation}";
                await ExecuteAdditionalOps(contextEntry);
            }
            else
            {
                param = $"{contextEntry.EntityType}{contextEntry.Operation}";
            }
            return param;
        }

        private async Task PullAsync()
        {
            _remoteCars.Clear();
            _remoteCustomers.Clear();
            _remoteEmployees.Clear();
            _remoteOrders.Clear();
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = null;
            foreach (string resource in RESTHelper.Instance().GetResources())
            {
                request.Resource = resource;
                response = RESTHelper.Instance().Client.Execute(request);
                if (response.IsSuccessful)
                {
                    if (resource.Equals("/Employees"))
                    {
                        _remoteEmployees = await JsonParserer.ParseStringAsync<List<Employee>>(response.Content);
                    }
                    else if (resource.Equals("/Cars"))
                    {
                        _remoteCars = await JsonParserer.ParseStringAsync<List<Car>>(response.Content);
                    }
                    else if (resource.Equals("/Users"))
                    {
                        _remoteCustomers = await JsonParserer.ParseStringAsync<List<Customer>>(response.Content);
                    }
                    else if (resource.Equals("/JoinTableEntries"))
                    {
                        _remoteOrders = await JsonParserer.ParseStringAsync<List<Transactions>>(response.Content);
                    }
                }
            }
            await CompareCollections();
        }

        private async Task PushAsync()
        {
            _syncContext = await SyncContextViewModel.Instance().GetSyncContext();
            foreach (SyncContext contextEntry in _syncContext)
            {
                ExecutePushOp(contextEntry);
                await SyncContextViewModel.Instance().DeleteContextEntry(contextEntry);
            }
        }
    }
}