using ObjednavkovySystem.Models.Extensions;
using System;

namespace ObjednavkovySystem.Models
{
    public class Customer : Person, IEquatable<Customer>
    {
        public bool Equals(Customer customer)
        {
            if (customer != null)
            {
                DateTime tempDate1 = Added.AddTicks(-Added.Ticks % TimeSpan.TicksPerSecond);
                DateTime tempDate2 = customer.Added.AddTicks(-customer.Added.Ticks % TimeSpan.TicksPerSecond);
                if (ID == customer.ID && tempDate1 == tempDate2)
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