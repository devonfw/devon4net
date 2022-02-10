using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;
using static Amazon.CDK.AWS.ElasticLoadBalancingV2.CfnLoadBalancer;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.NetworkLoadBalancer
{
    public class AwsCdkNetworkLoadBalancerHandler : AwsCdkBaseHandler, IAwsCdkNetworkLoadBalancerHandler
    {
        public AwsCdkNetworkLoadBalancerHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public INetworkLoadBalancer Create(string loadBalancerId, bool crossZoneEnabled, bool deletionProtection, bool internetFacing, string loadBalancerName, IVpc vpc, ISubnet[] subnets = null, ISubnetMappingProperty[] subnetMappingProperties = null)
        {
            var nlb = CreateNLB(loadBalancerId, new NetworkLoadBalancerEntity
            {
                CrossZoneEnabled = crossZoneEnabled,
                DeletionProtection = deletionProtection,
                InternetFacing = internetFacing,
                LoadBalancerName = loadBalancerName,
                Subnets = new SubnetSelection { Subnets = subnets },
                Vpc = vpc
            });
            if (subnetMappingProperties != null)
            {
                // Due to the current limitations in the AWS CDK we cannot implement it yet, this should be changes as soon as possible.
                var cfnNlb = (CfnLoadBalancer)nlb.Node.DefaultChild;
                cfnNlb.SubnetMappings = subnetMappingProperties;

                // The Subnets value is being assigned to null due to it needs to be removed if SubnetMappings are defined.
                // By default the Subnets are set to a default value when we define the SubnetMappings.
                cfnNlb.Subnets = null;
            }

            return nlb;
        }

        private INetworkLoadBalancer CreateNLB(string loadBalancerId, NetworkLoadBalancerEntity loadBalancerEntity)
        {
            return new Amazon.CDK.AWS.ElasticLoadBalancingV2.NetworkLoadBalancer(Scope, loadBalancerId, new NetworkLoadBalancerProps
            {
                CrossZoneEnabled = loadBalancerEntity.CrossZoneEnabled,
                DeletionProtection = loadBalancerEntity.DeletionProtection,
                InternetFacing = loadBalancerEntity.InternetFacing,
                LoadBalancerName = loadBalancerEntity.LoadBalancerName,
                Vpc = loadBalancerEntity.Vpc,
                VpcSubnets = loadBalancerEntity.Subnets
            });
        }
    }
}
