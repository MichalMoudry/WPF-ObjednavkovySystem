using ObjednavkovySystem.Models.Extensions;
using System;

namespace ObjednavkovySystem.Models
{
    public class Transactions : Entity, IEquatable<Transactions>
    {
        public int UserID { get; set; }

        public int CarID { get; set; }

        public DateTime Rented { get; set; }

        public int EmployeeID { get; set; }

        public int IsDone { get; set; }

        public bool Equals(Transactions transaction)
        {
            if (transaction != null)
            {
                DateTime tempDate1 = Added.AddTicks(-Added.Ticks % TimeSpan.TicksPerSecond);
                DateTime tempDate2 = transaction.Added.AddTicks(-transaction.Added.Ticks % TimeSpan.TicksPerSecond);
                if (ID == transaction.ID && tempDate1 == tempDate2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}