namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class Kestrel
    {
        public bool UseHttps { get; set; }
        public int ApplicationPort { get; set; }
        public Servercertificate ServerCertificate { get; set; }
        public Clientcertificate ClientCertificate { get; set; }
    }
}