using System.Collections.Generic;

namespace Devon4Net.Infrastructure.Common.Options.RabbitMq
{
    public class RabbitMQOptions
    {
        public List<HostDefnition> Hosts { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Product { get; set; }
        public int? RequestedHeartbeat { get; set; }
        public int? PrefetchCount { get; set; }
        public bool PublisherConfirms { get; set; }
        public bool PersistentMessages { get; set; }
        public string Platform { get; set; }
        public int? Timeout { get; set; }
        public Backup Backup { get; set; }
    }

    public class HostDefnition
    {
        public string Host { get; set; }
        public int? Port { get; set; }
        public bool Ssl { get; set; }
        public string SslServerName { get; set; }
        public string SslCertPath { get; set; }
        public string SslCertPassPhrase { get; set; }
        public string SslPolicyErrors { get; set; }
    }

    public class Backup
    {
        public bool UseSqLite { get; set; }
        public string DatabaseConnectionString { get; set; }
    }
}
