using ObjednavkovySystem.Models.Extensions;

namespace ObjednavkovySystem.Models
{
    public class Employee : Person
    {
        public string Password { get; set; }

        public string Role { get; set; }
    }
}