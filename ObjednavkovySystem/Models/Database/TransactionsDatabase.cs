using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class TransactionsDatabase
    {
        private SQLiteAsyncConnection database;

        public TransactionsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Transactions>().Wait();
        }

        public async Task DeleteEntity(Transactions entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<List<Transactions>> GetEntitesAsList()
        {
            return await database.Table<Transactions>().ToListAsync();
        }

        public async Task<Transactions> GetEntityByID(int id)
        {
            return await database.Table<Transactions>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task SaveEntity(Transactions entity)
        {
            if (entity.ID == 0)
            {
                await database.InsertAsync(entity);
            }
            else
            {
                string sql = $"INSERT INTO Transactions (ID, UserID, CarID, Rented, EmployeeID, IsDone, Added, LastUpdated) VALUES ({entity.ID}, {entity.UserID}, {entity.CarID}, '{entity.Rented.Ticks}', {entity.EmployeeID}, {entity.IsDone}, '{entity.Added.Ticks}', '{entity.LastUpdated.Ticks}')";
                await database.QueryAsync<Transactions>(sql);
            }
        }

        public async Task UpdateEntity(Transactions entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}