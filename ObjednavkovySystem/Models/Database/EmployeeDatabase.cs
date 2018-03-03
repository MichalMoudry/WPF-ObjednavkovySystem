using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Models.Database
{
    public class EmployeeDatabase
    {
        private SQLiteAsyncConnection database;

        public EmployeeDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Employee>().Wait();
        }

        public async Task DeleteEntity(Employee entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task<Employee> GetEntityByID(int id)
        {
            return await database.Table<Employee>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEntitesAsList()
        {
            return await database.Table<Employee>().ToListAsync();
        }

        public async Task SaveEntity(Employee entity)
        {
            if (entity.ID == 0)
            {
                await database.InsertAsync(entity);
            }
            else
            {
                string sql = $"INSERT INTO Employee(ID, Name, Added, LastUpdated, Password, Role) VALUES ({entity.ID}, '{entity.Name}', '{entity.Added.Ticks}', '{entity.LastUpdated.Ticks}', '{entity.Password}', '{entity.Role}')";
                await database.QueryAsync<Employee>(sql);
            }
        }

        public async Task UpdateEntity(Employee entity)
        {
            await database.UpdateAsync(entity);
        }
    }
}