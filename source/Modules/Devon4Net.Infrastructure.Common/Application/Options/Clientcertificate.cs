namespace Devon4Net.Infrastructure.Common.Application.Options
{
    public class Clientcertificate
    {
        public bool EnableClientCertificateCheck { get; set; }
        public bool RequireClientCertificate { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public Clientcertificates ClientCertificates { get; set; }
    }
}