using ObjednavkovySystem.Models.Extensions;
using System;

namespace ObjednavkovySystem.Models
{
    public class Employee : Person, IEquatable<Employee>
    {
        public string Password { get; set; }

        public string Role { get; set; }

        public bool Equals(Employee employee)
        {
            if (employee != null)
            {
                DateTime tempDate1 = Added.AddTicks(-Added.Ticks % TimeSpan.TicksPerSecond);
                DateTime tempDate2 = employee.Added.AddTicks(-employee.Added.Ticks % TimeSpan.TicksPerSecond);
                if (ID == employee.ID && tempDate1 == tempDate2)
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