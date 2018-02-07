using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjednavkovySystem.Models;
using RestSharp;
using ObjednavkovySystem.Helpers;
using Newtonsoft.Json;

namespace ObjednavkovySystem.ViewModels
{
    class CarViewModel
    {
        private List<Car> _cars;

        private static CarViewModel _instance;
        public static CarViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CarViewModel();
            }
            return _instance;
        }

        protected CarViewModel()
        {

        }

        public async Task<List<Car>> GetCars()
        {
            RestRequest request = new RestRequest("/Cars", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _cars = await JsonParserer.ParseStringAsync<List<Car>>(response.Content);
            }
            return _cars;
        }

        public async Task<List<Car>> GetCar(int id)
        {
            RestRequest request = new RestRequest($"/Cars/{id}", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                return await JsonParserer.ParseStringAsync<List<Car>>(response.Content);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddCar(Car car)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Car", $"{JsonConvert.SerializeObject(car)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> DeleteCar(Car car)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("CarDelete", $"{JsonConvert.SerializeObject(car)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }

        public async Task<bool> UpdateCar(Car car)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("CarUpdate", $"{JsonConvert.SerializeObject(car)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }
    }
}
