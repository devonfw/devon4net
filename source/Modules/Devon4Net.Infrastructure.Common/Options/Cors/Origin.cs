namespace Devon4Net.Infrastructure.Common.Options.Cors
{
    public class Origin
    {
        public string CorsPolicy { get; set; }
        public string Origins { get; set; }
        public string Headers { get; set; }
        public string Methods { get; set; }
        public bool AllowCredentials { get; set; }

        public List<string> GetOriginsList()
        {
            return Origins.Split(',').ToList();
        }

        public List<string> GetMethodsList()
        {
            return Methods.Split(',').ToList();
        }

        public List<string> GetHeadersList()
        {
            return Headers.Split(',').ToList();
        }
    }
}