using Newtonsoft.Json;
using ObjednavkovySystem.Helpers;
using ObjednavkovySystem.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class CarPersonJoinTableViewModel
    {
        private List<CarPersonJoinTable> _joinTableEntries;

        private static CarPersonJoinTableViewModel _instance;

        public static CarPersonJoinTableViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CarPersonJoinTableViewModel();
            }
            return _instance;
        }

        protected CarPersonJoinTableViewModel()
        {
        }

        public async Task<List<CarPersonJoinTable>> GetJoinTableEntries()
        {
            RestRequest request = new RestRequest("/JoinTableEntries", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _joinTableEntries = await JsonParserer.ParseStringAsync<List<CarPersonJoinTable>>(response.Content);
            }
            return _joinTableEntries;
        }

        public async Task<bool> AddRent(CarPersonJoinTable rent)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Transaction", $"{JsonConvert.SerializeObject(rent)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> UpdateRent(CarPersonJoinTable rent)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("TransactionUpdate", $"{JsonConvert.SerializeObject(rent)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> DeleteRent(CarPersonJoinTable rent)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("TransactionDelete", $"{JsonConvert.SerializeObject(rent)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }
    }
}