namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class KestrelDevonOptions
    {
        public bool UseHttps { get; set; }
        public int ApplicationPort { get; set; }
        public string HttpProtocol { get; set; }
        public string SslProtocol { get; set; }
    }
}