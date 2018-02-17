using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class SyncDatabase
    {
        private SQLiteAsyncConnection database;

        public SyncDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SyncContext>().Wait();
        }

        public async Task DeleteEntity(SyncContext entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<List<SyncContext>> GetEntitesAsList()
        {
            return await database.Table<SyncContext>().ToListAsync();
        }

        public async Task SaveEntity(SyncContext entity)
        {
            await database.InsertAsync(entity);
        }

        public async Task UpdateEntity(SyncContext entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}