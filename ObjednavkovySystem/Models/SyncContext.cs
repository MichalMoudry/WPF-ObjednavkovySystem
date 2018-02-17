using ObjednavkovySystem.Models.Extensions;

namespace ObjednavkovySystem.Models
{
    public class SyncContext : Entity
    {
        public int EntityID { get; set; }

        public string EntityType { get; set; }

        public string Operation { get; set; }
    }
}