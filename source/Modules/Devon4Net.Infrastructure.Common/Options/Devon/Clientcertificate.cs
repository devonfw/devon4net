namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class Clientcertificate
    {
        public bool EnableClientCertificateCheck { get; set; }
        public bool RequireClientCertificate { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public Clientcertificates ClientCertificates { get; set; }
    }
}