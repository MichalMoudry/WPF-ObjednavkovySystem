using ObjednavkovySystem.Models.Extensions;
using System;

namespace ObjednavkovySystem.Models
{
    public class Order : Entity
    {
        public int UserID { get; set; }

        public int CarID { get; set; }

        public DateTime Rented { get; set; }

        public int EmployeeID { get; set; }

        public int IsDone { get; set; }
    }
}