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
                //.ToString("yyyy-MM-dd HH:mm:ss")
                string sql = $"INSERT INTO Order(ID, UserID, CarID, Rented, EmployeeID, IsDone, Added, LastUpdated, IsDeleted) VALUES ('{entity.ID}', '{entity.UserID}', '{entity.CarID}', '{entity.Rented.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.EmployeeID}', '{entity.IsDone}', '{entity.Added.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.IsDeleted}')";
                await database.QueryAsync<Order>(sql);
            }
        }

        public async Task UpdateEntity(Order entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}