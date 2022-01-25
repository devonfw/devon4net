using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using static Amazon.CDK.AWS.ElasticLoadBalancingV2.CfnLoadBalancer;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager 
    {
        public INetworkTargetGroup AddNetworkTargetGroup(string id, string name, double port, IVpc vpc, int healthCheckCount)
        {
            return  HandlerResources.AwsCdkElbHandler.CreateNetworkTargetGroup(id, name, port, vpc, healthCheckCount);
        }
        public INetworkLoadBalancer CreateNetworkLoadBalancer(string loadBalancerId, bool crossZoneEnabled, bool deletionProtection, bool internetFacing, string loadBalancerName, IVpc vpc, ISubnet[] subnets = null, ISubnetMappingProperty[] subnetMappingProperties = null)
        {
            return HandlerResources.AwsCdkNetworkLoadBalancerHandler.Create(loadBalancerId, crossZoneEnabled, deletionProtection, internetFacing, loadBalancerName, vpc, subnets, subnetMappingProperties);
        }
    }
}
