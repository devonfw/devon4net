using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{

    public class EcsContainerDefinitionOptions
    {
        public string Id { get; set; }
        public string ImageTag { get; set; }
        public string RepositoryId { get; set; }
        public double? MemoryReservationMiB { get; set; }
        public double? MemoryLimitMiB { get; set; }
        public double? CpuUnits { get; set; }
        public int StartTimeOutMinutes { get; set; }
        public List<EcsPortMappingOptions> TCPPortMapping { get; set; }
        public List<EcsPortMappingOptions> UDPPortMapping { get; set; }
        public Dictionary<string, string> EnvironmentVariables { get; set; }
        public List<string> DnsServers { get; set; }
    }

}
