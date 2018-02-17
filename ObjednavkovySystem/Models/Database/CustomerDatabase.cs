using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class CustomerDatabase
    {
        private SQLiteAsyncConnection database;

        public CustomerDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Customer>().Wait();
        }

        public async Task DeleteEntity(Customer entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<List<Customer>> GetEntitesAsList()
        {
            return await database.Table<Customer>().ToListAsync();
        }

        public async Task<Customer> GetEntityByID(int id)
        {
            return await database.Table<Customer>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task SaveEntity(Customer entity)
        {
            if (entity.ID == 0)
            {
                await database.InsertAsync(entity);
            }
            else
            {
                string sql = $"INSERT INTO Customer(ID, Added, LastUpdated, Name) VALUES ({entity.ID}, '{entity.Added.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss")}', '{entity.Name}')";
                await database.QueryAsync<Customer>(sql);
            }
        }

        public async Task UpdateEntity(Customer entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}