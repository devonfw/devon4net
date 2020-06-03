using System.Collections.Generic;

namespace Devon4Net.Infrastructure.Common.Options.RabbitMq
{
    public class RabbitMqOptions
    {
        public bool EnableRabbitMq { get; set; }
        public List<HostDefinition> Hosts { get; set; }
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
}
