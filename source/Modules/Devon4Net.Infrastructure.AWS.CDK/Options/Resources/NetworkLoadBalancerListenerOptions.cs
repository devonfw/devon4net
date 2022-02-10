using Amazon.CDK.AWS.ElasticLoadBalancingV2;
namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class NetworkLoadBalancerListenerOptions
    {
        public string Id { get; set; }
        public DefaultActionOptions DefaultAction { get; set; }
        public string[] DefaultTargetGroups { get; set; }
        public uint Port { get; set; }
        public Protocol Protocol { get; set; }
    }
}
