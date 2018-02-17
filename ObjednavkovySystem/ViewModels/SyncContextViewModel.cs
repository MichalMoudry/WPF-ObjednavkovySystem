using ObjednavkovySystem.Models;
using ObjednavkovySystem.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjednavkovySystem.ViewModels
{
    internal class SyncContextViewModel
    {
        private static SyncContextViewModel _instance;
        private SyncDatabase syncDatabase;

        protected SyncContextViewModel()
        {
            syncDatabase = new SyncDatabase("SyncContextDatabase.db3");
        }

        public static SyncContextViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new SyncContextViewModel();
            }
            return _instance;
        }

        public async Task DeleteContextEntry(SyncContext entry)
        {
            await syncDatabase.DeleteEntity(entry);
        }

        public async Task<List<SyncContext>> GetSyncContext()
        {
            return await syncDatabase.GetEntitesAsList();
        }

        public async Task InsertEntry(SyncContext entry)
        {
            await syncDatabase.SaveEntity(entry);
        }
    }
}