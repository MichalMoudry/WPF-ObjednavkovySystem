using SQLite;
using System;

namespace ObjednavkovySystem.Models.Extensions
{
    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime Added { get; set; }

        public DateTime LastUpdated { get; set; }

        public int IsDeleted { get; set; }
    }
}