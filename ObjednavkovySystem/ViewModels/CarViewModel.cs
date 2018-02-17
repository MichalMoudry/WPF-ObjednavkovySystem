using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace ObjednavkovySystem.ViewModels
{
    internal class CarViewModel
    {
        private static CarViewModel _instance;
        private ObservableCollection<Car> _observableCollCars;
        private List<Car> _cars;
        private CarDatabase carDatabase;

        protected CarViewModel()
        {
            carDatabase = new CarDatabase("CarDatabase.db3");
        }

        public static CarViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CarViewModel();
            }
            return _instance;
        }

        public async Task DeleteCar(Car car, bool toSyncContext = true)
        {
            car.IsDeleted = 1;
            await carDatabase.UpdateEntity(car);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = car.ID, EntityType = "Car", Operation = "Delete", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollCars != null)
            {
                _observableCollCars.Remove(car);
            }
        }

        public async Task<Car> GetCarByID(int id)
        {
            return await carDatabase.GetEntityByID(id);
        }

        public async Task<List<Car>> GetCarsAsList()
        {
            _cars = await carDatabase.GetEntitesAsList();
            return _cars.Where(i => i.IsDeleted == 0).ToList();
        }

        public async Task<ObservableCollection<Car>> GetCarsAsObservable()
        {
            _observableCollCars = new ObservableCollection<Car>(await GetCarsAsList());
            return _observableCollCars;
        }

        public async Task InsertCar(Car car, bool toSyncContext = true)
        {
            await carDatabase.SaveEntity(car);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = car.ID, EntityType = "Car", Operation = "Create", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
            if (_observableCollCars != null)
            {
                _observableCollCars.Add(car);
            }
        }

        public async Task UpdateCar(Car car, bool toSyncContext = true)
        {
            await carDatabase.UpdateEntity(car);
            if (toSyncContext)
            {
                await SyncContextViewModel.Instance().InsertEntry(new SyncContext() { EntityID = car.ID, EntityType = "Car", Operation = "Update", Added = DateTime.Now, LastUpdated = DateTime.Now });
            }
        }
    }
}