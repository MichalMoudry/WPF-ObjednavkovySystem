using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class CarDatabase
    {
        private SQLiteAsyncConnection database;

        public CarDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Car>().Wait();
        }

        public async Task DeleteEntity(Car entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<List<Car>> GetEntitesAsList()
        {
            return await database.Table<Car>().ToListAsync();
        }

        public async Task<Car> GetEntityByID(int id)
        {
            return await database.Table<Car>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task SaveEntity(Car entity)
        {
            if (entity.ID == 0)
            {
                await database.InsertAsync(entity);
            }
            else
            {
                string sql = $"INSERT INTO Car(ID, Added, LastUpdated, Name, IsLent) VALUES ({entity.ID}, '{entity.Added.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.Name}', {entity.IsLent})";
                await database.QueryAsync<Car>(sql);
            }
        }

        public async Task UpdateEntity(Car entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}