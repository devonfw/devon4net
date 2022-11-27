namespace Devon4Net.Infrastructure.Cors.Options
{
    public class Origin
    {
        public string CorsPolicy { get; set; }
        public string Origins { get; set; }
        public string Headers { get; set; }
        public string Methods { get; set; }
        public bool AllowCredentials { get; set; }
        public string ExposedHeaders { get; set; }

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

        public List<string> GetExposedHeadersList()
        {
            return ExposedHeaders.Split(',').ToList();
        }
    }
}