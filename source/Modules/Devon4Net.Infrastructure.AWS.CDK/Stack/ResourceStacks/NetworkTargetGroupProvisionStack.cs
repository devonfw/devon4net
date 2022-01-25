using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using System.Linq;
using System;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using static Amazon.CDK.AWS.ElasticLoadBalancingV2.CfnLoadBalancer;
using static Amazon.CDK.AWS.ElasticLoadBalancingV2.CfnListener;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        public void CreateNetworkTargetGroup()
        {
            if (CdkOptions == null || CdkOptions.NetworkTargetGroups?.Any() != true) return;

            foreach (var targetGroup in CdkOptions.NetworkTargetGroups)
            {
                ValidateNetworkTargetGroupOptions(targetGroup);

                var vpc = LocateVpc(targetGroup.VpcId, "The VPC you are locating does not exist");

                StackResources.NetworkTargetGroups.Add(targetGroup.Id, AwsCdkHandler.AddNetworkTargetGroup(targetGroup.Id, targetGroup.Name, targetGroup.Port, vpc, targetGroup.HealthCheckCount));
            }
        }

        private static void ValidateNetworkTargetGroupOptions(NetworkTargetGroupOptions targetGroup)
        {
            if (string.IsNullOrWhiteSpace(targetGroup.Id))
            {
                throw new ArgumentException("There must be an id for the network target group");
            }
            if (string.IsNullOrWhiteSpace(targetGroup.Name))
            {
                throw new ArgumentException("There must be a name for the network target group");
            }
            if (targetGroup.Port == default)
            {
                throw new ArgumentException("There must be a port for the network target group");
            }
            if (targetGroup.HealthCheckCount > 10 || targetGroup.HealthCheckCount < 2)
            {
                throw new ArgumentException("The healthCheckCount must be between 2 and 10, both included.");
            }
        }

        public ISubnetMappingProperty CreateSubnetMappingProperty(string allocationId, string ipv6Address, string privateIpv4Address, string subnetId)
        {
            return new SubnetMappingProperty
            {
                AllocationId = allocationId,
                IPv6Address = ipv6Address,
                PrivateIPv4Address = privateIpv4Address,
                SubnetId = LocateSubnet(subnetId, $"The Subnet name { subnetId } in Network Load Balancer's Subnet Mapping does not exist").SubnetId
            };
        }
        private INetworkTargetGroup LocateNetworkTargetGroup(string targetGroupId, string exceptionMessageIfTGDoesNotExist, string exceptionMessageIfTGIsEmpty = null)
        {
            return StackResources.Locate<INetworkTargetGroup>(targetGroupId, exceptionMessageIfTGDoesNotExist, exceptionMessageIfTGIsEmpty);
        }
    }
}
