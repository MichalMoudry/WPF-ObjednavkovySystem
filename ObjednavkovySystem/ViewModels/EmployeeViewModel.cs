using CryptSharp;
using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class EmployeeViewModel
    {
        private static EmployeeViewModel _instance;
        private ObservableCollection<Employee> _observableCollEmployees;
        private EmployeeDatabase employeeDatabase;

        protected EmployeeViewModel()
        {
            employeeDatabase = new EmployeeDatabase("EmployeeDatabase.db3");
        }

        public static EmployeeViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeViewModel();
            }
            return _instance;
        }

        public async Task<Employee> AuthAsync(Employee employee)
        {
            Employee matchEmployee = null;
            foreach (Employee item in await GetEmployeesAsList(false))
            {
                if (Crypter.CheckPassword(employee.Password, item.Password) && employee.Name.Equals(item.Name))
                {
                    matchEmployee = item;
                }
            }
            return matchEmployee;
        }

        public async Task DeleteEmployee(Employee employee, bool toSyncContext = true)
        {
            employee.IsDeleted = 1;
            await employeeDatabase.UpdateEntity(employee);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = employee.ID, EntityType = "Employee", Operation = "Delete", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollEmployees != null)
            {
                _observableCollEmployees.Remove(employee);
            }
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
            return await employeeDatabase.GetEntityByID(id);
        }

        public async Task<List<Employee>> GetEmployeesAsList(bool removeAdmin = true)
        {
            List<Employee> list = await employeeDatabase.GetEntitesAsList();
            if (removeAdmin)
            {
                list.Remove(list.FirstOrDefault(i => i.Name.Equals("Admin")));
            }
            return list;
        }

        public async Task<ObservableCollection<Employee>> GetEmployeesAsObservable(bool removeAdmin = true)
        {
            _observableCollEmployees = new ObservableCollection<Employee>(await GetEmployeesAsList(removeAdmin));
            return _observableCollEmployees;
        }

        public async Task InsertEmployee(Employee employee, bool toSyncContext = true)
        {
            employee.Password = Crypter.Blowfish.Crypt(employee.Password);
            await employeeDatabase.SaveEntity(employee);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = employee.ID, EntityType = "Employee", Operation = "Create", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollEmployees != null)
            {
                _observableCollEmployees.Add(employee);
            }
        }

        public async Task UpdateEmployee(Employee employee, bool toSyncContext = true)
        {
            await employeeDatabase.UpdateEntity(employee);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = employee.ID, EntityType = "Employee", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
        }
    }
}