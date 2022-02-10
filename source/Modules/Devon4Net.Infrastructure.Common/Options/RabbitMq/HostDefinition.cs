namespace Devon4Net.Infrastructure.Common.Options.RabbitMq
{
    public class HostDefinition
    {
        public string Host { get; set; }
        public int? Port { get; set; }
        public bool Ssl { get; set; }
        public string SslServerName { get; set; }
        public string SslCertPath { get; set; }
        public string SslCertPassPhrase { get; set; }
        public string SslPolicyErrors { get; set; }
    }
}