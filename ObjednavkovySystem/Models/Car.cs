using ObjednavkovySystem.Models.Extensions;
using System;

namespace ObjednavkovySystem.Models
{
    public class Car : Entity, IEquatable<Car>
    {
        public int IsLent { get; set; }
        public string Name { get; set; }

        public bool Equals(Car car)
        {
            if (car != null)
            {
                DateTime tempDate1 = Added.AddTicks(-Added.Ticks % TimeSpan.TicksPerSecond);
                DateTime tempDate2 = car.Added.AddTicks(-car.Added.Ticks % TimeSpan.TicksPerSecond);
                if (ID == car.ID && tempDate1 == tempDate2)
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