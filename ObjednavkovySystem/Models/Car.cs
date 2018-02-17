using ObjednavkovySystem.Models.Extensions;

namespace ObjednavkovySystem.Models
{
    public class Car : Entity
    {
        public string Name { get; set; }

        public int IsLent { get; set; }
    }
}