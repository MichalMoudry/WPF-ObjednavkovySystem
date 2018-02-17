using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class OrderDatabase
    {
        private SQLiteAsyncConnection database;

        public OrderDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Order>().Wait();
        }

        public async Task DeleteEntity(Order entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<List<Order>> GetEntitesAsList()
        {
            return await database.Table<Order>().ToListAsync();
        }

        public async Task<Order> GetEntityByID(int id)
        {
            return await database.Table<Order>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task SaveEntity(Order entity)
        {
            if (entity.ID == 0)
            {
                await database.InsertAsync(entity);
            }
            else
            {
                string sql = $"INSERT INTO Order(ID, Added, LastUpdated, UserID, CarID, Rented, EmployeeID, IsDone) VALUES ({entity.ID}, '{entity.Added.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss")}', {entity.UserID}, {entity.CarID}, '{entity.Rented}', {entity.EmployeeID}, {entity.IsDone})";
                await database.QueryAsync<Order>(sql);
            }
        }

        public async Task UpdateEntity(Order entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}