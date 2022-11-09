using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Linq;
using static Amazon.CDK.AWS.ElasticLoadBalancingV2.CfnListener;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        public void CreateNetworkLoadBalancers()
        {
            if (CdkOptions.NetworkLoadBalancers == null || CdkOptions.NetworkLoadBalancers?.Any() != true) return;

            foreach (var networkLoadBalancerOptions in CdkOptions.NetworkLoadBalancers)
            {
                if (networkLoadBalancerOptions.Subnets?.Any() == false && networkLoadBalancerOptions.SubnetMappings?.Any() == false)
                {
                    throw new ArgumentException($"Subnets or SubnetMappings are missing in NetworkLoadBalancer {networkLoadBalancerOptions.Id}, define one  of them.");
                }

                if (networkLoadBalancerOptions.Subnets?.Any() == true && networkLoadBalancerOptions.SubnetMappings?.Any() == true)
                {
                    throw new ArgumentException($"Subnets and SubnetMappings are incompatible in NetworkLoadBalancer {networkLoadBalancerOptions.Id}, define only one  of them.");
                }

                var vpc = LocateVpc(networkLoadBalancerOptions.VpcId, $"The VPC name {networkLoadBalancerOptions.VpcId} in Network Load Balancer {networkLoadBalancerOptions.LoadBalancerName} does not exist");
                INetworkLoadBalancer networkLoadBalancer = default;
                if (networkLoadBalancerOptions.Subnets?.Any() == true)
                {
                    var subnets = networkLoadBalancerOptions.Subnets.Select(x => LocateSubnet(x, $"The Subnet name {x} in Network Load Balancer {networkLoadBalancerOptions.LoadBalancerName} does not exist")).ToArray();
                    networkLoadBalancer = AwsCdkHandler.CreateNetworkLoadBalancer(networkLoadBalancerOptions.Id, networkLoadBalancerOptions.CrossZoneEnabled, networkLoadBalancerOptions.DeletionProtection, networkLoadBalancerOptions.InternetFacing, networkLoadBalancerOptions.LoadBalancerName, vpc, subnets);
                }

                networkLoadBalancerOptions.Listeners.ForEach(x => AssignListenerToNetworkLoadBalancer(networkLoadBalancer, x));
                StackResources.NetworkLoadBalancers.Add(networkLoadBalancerOptions.Id, networkLoadBalancer);
            }
        }

        private void AssignListenerToNetworkLoadBalancer(INetworkLoadBalancer networkLoadBalancer, NetworkLoadBalancerListenerOptions listenerOptions)
        {
            if (listenerOptions.DefaultAction != null && listenerOptions.DefaultTargetGroups?.Any() == true)
            {
                throw new ArgumentException($"The Listener {listenerOptions.Id} can only have DefaultAction or DefaultTargerGroups but not both");
            }

            if (listenerOptions.DefaultAction is null && listenerOptions.DefaultTargetGroups?.Any() == false)
            {
                throw new ArgumentException($"The Listener {listenerOptions.Id} must have one DefaultAction or DefaultTargerGroups");
            }

            if (listenerOptions.DefaultAction != null)
            {
                networkLoadBalancer.AddListener(listenerOptions.Id, new BaseNetworkListenerProps
                {
                    DefaultAction = new NetworkListenerAction(new ActionProperty { Type = listenerOptions.DefaultAction.Type }),
                    Port = listenerOptions.Port,
                    Protocol = listenerOptions.Protocol
                });
            }
            else
            {
                var networkTargetGroups = listenerOptions.DefaultTargetGroups.Select(x => LocateNetworkTargetGroup(x, $"The NetworkTargetGroup name {x} in Listener {listenerOptions.Id} does not exist")).ToArray();
                networkLoadBalancer.AddListener(listenerOptions.Id, new BaseNetworkListenerProps
                {
                    DefaultTargetGroups = networkTargetGroups,
                    Port = listenerOptions.Port,
                    Protocol = listenerOptions.Protocol
                });
            }
        }
    }
}
