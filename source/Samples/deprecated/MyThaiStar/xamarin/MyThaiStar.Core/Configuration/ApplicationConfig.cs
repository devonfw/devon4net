namespace MyThaiStar.Core.Configuration
{
    public static class ApplicationConfig
    {
        //private const string BaseUrl = "http://192.168.43.42:8081/";
        private const string BaseUrl = "http://10.68.8.169:8081/";
        //private const string BaseUrl = "http://10.0.2.2:8081/";
        //private const string BaseUrl = "http://1.0.195.142:8081/";
        //private const string BaseUrl = "http://192.168.1.38:8081/";
        //private const string BaseUrl = "http://10.68.14.163:8081/";
        //private const string BaseUrl = "http://192.168.1.44:8081/";
        //private const string BaseUrl = "http://1.0.195.182:8081/";
        public static string LoginUrl = $"{BaseUrl}mythaistar/login";
        public static string CurrentUserUrl = $"{BaseUrl}mythaistar/services/rest/security/v1/currentuser";

        #region DishManagement url
        public static string DishSearchUrl = $"{BaseUrl}mythaistar/services/rest/dishmanagement/v1/dish/search";
        #endregion


    }
}
