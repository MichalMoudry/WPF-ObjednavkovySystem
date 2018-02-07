using RestSharp;

namespace ObjednavkovySystem.Helpers
{
    internal class RESTHelper
    {
        public RestClient Client = new RestClient("https://student.sps-prosek.cz/~moudrmi14/Prg/API/TestApi/api.php");
        private static RESTHelper _instance;

        protected RESTHelper()
        {
        }

        public static RESTHelper Instance()
        {
            if (_instance == null)
            {
                _instance = new RESTHelper();
            }
            return _instance;
        }
    }
}