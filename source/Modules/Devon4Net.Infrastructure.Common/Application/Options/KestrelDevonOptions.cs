namespace Devon4Net.Infrastructure.Common.Application.Options
{
    public class KestrelDevonOptions
    {
        public bool UseHttps { get; set; }
        public int ApplicationPort { get; set; }
        public string HttpProtocol { get; set; }
        public string SslProtocol { get; set; }
    }
}