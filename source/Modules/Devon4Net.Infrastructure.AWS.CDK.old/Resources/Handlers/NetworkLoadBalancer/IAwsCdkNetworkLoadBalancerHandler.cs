using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.NetworkLoadBalancer
{
    public interface IAwsCdkNetworkLoadBalancerHandler
    {
        INetworkLoadBalancer Create(string loadBalancerId, bool crossZoneEnabled, bool deletionProtection, bool internetFacing, string loadBalancerName, IVpc vpc, ISubnet[] subnets = null, CfnLoadBalancer.ISubnetMappingProperty[] subnetMappingProperties = null); //NOSONAR number of params
    }
}