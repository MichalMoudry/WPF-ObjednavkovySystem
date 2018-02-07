using CryptSharp;
using Newtonsoft.Json;
using ObjednavkovySystem.Helpers;
using ObjednavkovySystem.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ObjednavkovySystem.ViewModels
{
    internal class EmployeeViewModel
    {
        private static EmployeeViewModel _instance;
        private List<Employee> _employees;

        public static EmployeeViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeViewModel();
            }
            return _instance;
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            await Task.Delay(10);
            string salt = Crypter.Blowfish.GenerateSalt(4);
            string password = Crypter.Blowfish.Crypt(employee.Password, salt);
            employee.Password = password;
            employee.Role = "Employee";
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Employee", $"{JsonConvert.SerializeObject(employee)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            _employees.Add(employee);
            return res;
        }

        public async Task<Employee> AuthEmployee(Employee employee)
        {
            List<Employee> employees = await Instance().GetEmployees(false);
            Employee matchEmployee = null;
            foreach (Employee item in employees)
            {
                if (Crypter.CheckPassword(employee.Password, item.Password) && employee.Name.Equals(item.Name))
                {
                    matchEmployee = item;
                }
            }

            return matchEmployee;
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            await Task.Delay(10);
            bool res = false;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("EmployeeDelete", $"{JsonConvert.SerializeObject(employee)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            _employees.Remove(employee);
            return res;
        }

        public async Task<List<Employee>> GetEmployee(int id)
        {
            RestRequest request = new RestRequest($"/Employees/{id}", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _employees = await JsonParserer.ParseStringAsync<List<Employee>>(response.Content);
            }
            return _employees;
        }

        public async Task<List<Employee>> GetEmployees(bool removeAdmin = true)
        {
            RestRequest request = new RestRequest("/Employees", Method.GET);
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                _employees = await JsonParserer.ParseStringAsync<List<Employee>>(response.Content);
            }
            if (removeAdmin)
            {
                Employee admin = _employees.FirstOrDefault(i => i.Name.Equals("Admin"));
                _employees.Remove(admin);
            }
            return _employees;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            await Task.Delay(10);
            bool res = false;
            string password = Crypter.Blowfish.Crypt(employee.Password, Crypter.Blowfish.GenerateSalt(4));
            employee.Password = password;
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("EmployeeUpdate", $"{JsonConvert.SerializeObject(employee)}");
            var response = RESTHelper.Instance().Client.Execute(request);
            if (response.IsSuccessful)
            {
                res = true;
            }
            return res;
        }
    }
}