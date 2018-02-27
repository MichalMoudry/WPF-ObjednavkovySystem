using ObjednavkovySystem.Helpers;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.ViewModels;
using RestSharp;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace ObjednavkovySystem.Services
{
    internal class SyncService
    {
        private static SyncService _instance;
        private List<SyncContext> syncContext;
        //Remote.
        private List<Car> _remoteCars;
        private List<Customer> _remoteCustomers;
        private List<Employee> _remoteEmployees;
        private List<Order> _remoteOrders;
        private Employee remoteEmployee;
        //Local.
        private List<Employee> _localEmployees;
        private Car localCar;
        private Customer localCustomer;
        private Employee localEmployee;
        private Order localOrder;

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
                localCar = await CarViewModel.Instance().GetCarByID(remoteCar.ID);
                if (localCar == null)
                {
                    await CarViewModel.Instance().InsertCar(remoteCar, false);
                }
                else if (remoteCar.LastUpdated > localCar.LastUpdated)
                {
                    localCar = remoteCar;
                    await CarViewModel.Instance().UpdateCar(localCar, false);
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
                localCustomer = await CustomerViewModel.Instance().GetCustomerByID(remoteCustomer.ID);
                if (localCustomer == null)
                {
                    await CustomerViewModel.Instance().InsertCustomer(remoteCustomer, false);
                }
                else if (remoteCustomer.LastUpdated > localCustomer.LastUpdated)
                {
                    localCustomer = remoteCustomer;
                    await CustomerViewModel.Instance().UpdateCutomer(localCustomer, false);
                }
            }
        }

        private async Task CompareEmployees()
        {
            _localEmployees = await EmployeeViewModel.Instance().GetEmployeesAsList();
            foreach (Employee remoteEmployee in _remoteEmployees)
            {
                localEmployee = await EmployeeViewModel.Instance().GetEmployeeByID(remoteEmployee.ID);
                if (localEmployee == null)
                {
                    await EmployeeViewModel.Instance().InsertEmployee(remoteEmployee, false);
                }
                else if (remoteEmployee.LastUpdated > localEmployee.LastUpdated)
                {
                    localEmployee = remoteEmployee;
                    await EmployeeViewModel.Instance().UpdateEmployee(localEmployee, false);
                }
            }
        }

        private async Task CompareOrders()
        {
            foreach (Order remoteOrder in _remoteOrders)
            {
                localOrder = await OrderViewModel.Instance().GetOrderByID(remoteOrder.ID);
                if (localOrder == null)
                {
                    await OrderViewModel.Instance().InsertOrder(remoteOrder, false);
                }
                else if (remoteOrder.LastUpdated > localOrder.LastUpdated)
                {
                    localOrder = remoteOrder;
                    await OrderViewModel.Instance().UpdateOrder(localOrder, false);
                }
            }
        }

        private async Task PullAsync()
        {
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
                        _remoteOrders = await JsonParserer.ParseStringAsync<List<Order>>(response.Content);
                    }
                }
            }
            await CompareCollections();
        }

        private async Task PushAsync()
        {
            syncContext = await SyncContextViewModel.Instance().GetSyncContext();
            foreach (SyncContext contextEntry in syncContext)
            {
                ExecutePushOp(contextEntry);
                await SyncContextViewModel.Instance().DeleteContextEntry(contextEntry);
            }
        }

        private async void ExecutePushOp(SyncContext contextEntry)
        {
            System.Diagnostics.Debug.WriteLine(contextEntry.EntityType);
            RestRequest request = new RestRequest(Method.POST);
            IRestResponse response = null;
            string value = null;
            if (contextEntry.EntityType.Equals("Employee"))
            {
                value = $"{JsonConvert.SerializeObject(await EmployeeViewModel.Instance().GetEmployeeByID(contextEntry.EntityID))}";
            }
            else if (contextEntry.EntityType.Equals("Customer"))
            {
                value = $"{JsonConvert.SerializeObject(await CustomerViewModel.Instance().GetCustomerByID(contextEntry.EntityID))}";
            }
            else if (contextEntry.EntityType.Equals("Car"))
            {
                value = $"{JsonConvert.SerializeObject(await CarViewModel.Instance().GetCarByID(contextEntry.EntityID))}";
            }
            else if (contextEntry.EntityType.Equals("Transaction"))
            {
                value = $"{JsonConvert.SerializeObject(await OrderViewModel.Instance().GetOrderByID(contextEntry.EntityID))}";
            }
            request.AddParameter(GetRequestParam(contextEntry.Operation, contextEntry.EntityType), value);
            response = RESTHelper.Instance().Client.Execute(request);
            request.Parameters.Clear();
        }

        private string GetRequestParam(string op, string entity)
        {
            string param = "";
            if (entity.Equals("Order"))
            {
                entity = "Transaction";
            }
            else if (entity.Equals("Customer"))
            {
                entity = "User";
            }
            if (op.Equals("Create"))
            {
                param = entity;
            }
            else
            {
                param = $"{entity}{op}";
            }
            return param;
        }
    }
}