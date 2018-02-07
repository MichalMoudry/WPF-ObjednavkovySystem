using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ObjednavkovySystem.Helpers
{
    internal class JsonParserer
    {
        public static async Task<T> ParseStringAsync<T>(string response)
        {
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response));
        }
    }
}