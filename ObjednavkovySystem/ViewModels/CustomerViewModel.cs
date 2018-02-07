using Newtonsoft.Json;
using ObjednavkovySystem.Helpers;
using ObjednavkovySystem.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class CustomerViewModel
    {
        private List<Customer> _customers;

        private static CustomerViewModel _instance;

        public static CustomerViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CustomerViewModel();
            }
            return _instance;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            RestRequest request = new RestRequest("/Users", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _customers = await JsonParserer.ParseStringAsync<List<Customer>>(response.Content);
            }
            return _customers;
        }

        public async Task<List<Customer>> GetCustomer(int id)
        {
            RestRequest request = new RestRequest($"/Users/{id}", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _customers = await JsonParserer.ParseStringAsync<List<Customer>>(response.Content);
            }
            return _customers;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("User", $"{JsonConvert.SerializeObject(customer)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("UserDelete", $"{JsonConvert.SerializeObject(customer)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("UserUpdate", $"{JsonConvert.SerializeObject(customer)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }
    }
}