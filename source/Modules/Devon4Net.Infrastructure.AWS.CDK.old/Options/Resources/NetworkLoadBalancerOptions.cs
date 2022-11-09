using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class NetworkLoadBalancerOptions
    {
        public string Id { get; set; }
        public bool CrossZoneEnabled { get; set; }
        public bool DeletionProtection { get; set; }
        public bool InternetFacing { get; set; }
        public string LoadBalancerName { get; set; }
        public string VpcId { get; set; }
        public List<string> Subnets { get; set; }
        public List<SubnetMappingOptions> SubnetMappings { get; set; }
        public List<NetworkLoadBalancerListenerOptions> Listeners { get; set; }
    }
}
