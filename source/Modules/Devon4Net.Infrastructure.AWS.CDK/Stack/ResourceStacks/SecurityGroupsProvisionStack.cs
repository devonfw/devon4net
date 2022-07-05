using Amazon.CDK.AWS.EC2;
using Devon4Net.Infrastructure.AWS.CDK.Enums;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateOrLocateSecurityGroups()
        {
            if (CdkOptions == null || CdkOptions.SecurityGroups?.Any() != true) return;

            foreach (var securityGroupOptions in CdkOptions.SecurityGroups)
            {
                if (securityGroupOptions.LocateInsteadOfCreate)
                {
                    var securityGroup = LocateSecurityGroup(securityGroupOptions);

                    StackResources.SecurityGroups.Add(securityGroupOptions.Id, securityGroup);
                }
                else
                {
                    var vpcResource = StackResources.Vpcs.FirstOrDefault(v => v.Key == securityGroupOptions.VpcId);
                    var vpc = vpcResource.Value ?? AwsCdkHandler.LocateVpc(securityGroupOptions.VpcId, $"The Vpc id {securityGroupOptions.VpcId} of the security group {securityGroupOptions.SecurityGroupName} was not found");

                    var securityGroup = AwsCdkHandler.AddSecurityGroup(securityGroupOptions.SecurityGroupName, securityGroupOptions.SecurityGroupName, vpc, securityGroupOptions.AllowAllOutbound, securityGroupOptions.DisableInlineRules);
                    StackResources.SecurityGroups.Add(securityGroupOptions.Id, securityGroup);
                    AddSecurityGroupIngressRules(securityGroupOptions, securityGroup);
                    AddSecurityGroupEgressRules(securityGroupOptions, securityGroup);
                }
            }
        }

        private void AddSecurityGroupIngressRules(SecurityGroupOptions securityGroupOptions, ISecurityGroup securityGroup)
        {
            if (securityGroupOptions.IngressRules?.Any() != true)
            {
                return;
            }

            foreach (var ingressRuleOptions in securityGroupOptions.IngressRules)
            {
                var securityGroupIdIsEmpty = string.IsNullOrWhiteSpace(ingressRuleOptions.SecurityGroupId);
                var ipAddressIsEmpty = string.IsNullOrWhiteSpace(ingressRuleOptions.IpAddress);

                CheckSecurityGroupRuleParams(ingressRuleOptions, securityGroupIdIsEmpty, ipAddressIsEmpty, SecurityGroupRuleType.IngressRule);
                AddPortSecurityGroupRule(securityGroupOptions, securityGroup, ingressRuleOptions, ipAddressIsEmpty, SecurityGroupRuleType.IngressRule);
                AddPortRangeSecurityGroupRule(securityGroupOptions, securityGroup, ingressRuleOptions, ipAddressIsEmpty, SecurityGroupRuleType.IngressRule);
            }
        }

        private void AddSecurityGroupEgressRules(SecurityGroupOptions securityGroupOptions, ISecurityGroup securityGroup)
        {
            if (securityGroupOptions.EgressRules?.Any() != true)
            {
                return;
            }

            foreach (var egressRuleOptions in securityGroupOptions.EgressRules)
            {
                var securityGroupIdIsEmpty = string.IsNullOrWhiteSpace(egressRuleOptions.SecurityGroupId);
                var ipAddressIsEmpty = string.IsNullOrWhiteSpace(egressRuleOptions.IpAddress);

                CheckSecurityGroupRuleParams(egressRuleOptions, securityGroupIdIsEmpty, ipAddressIsEmpty, SecurityGroupRuleType.EgressRule);
                AddPortSecurityGroupRule(securityGroupOptions, securityGroup, egressRuleOptions, ipAddressIsEmpty, SecurityGroupRuleType.EgressRule);
                AddPortRangeSecurityGroupRule(securityGroupOptions, securityGroup, egressRuleOptions, ipAddressIsEmpty, SecurityGroupRuleType.EgressRule);
            }
        }

        private static void AddRuleWithIpAddress(ISecurityGroup securityGroup, SecurityGroupRuleOptions securitygroupRuleOptions, SecurityGroupRuleType securityGroupRuleType)
        {
            if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
            {
                securityGroup.AddIngressRule(Peer.Ipv4(securitygroupRuleOptions.IpAddress), Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
            else
            {
                securityGroup.AddEgressRule(Peer.Ipv4(securitygroupRuleOptions.IpAddress), Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
        }

        private void AddRuleWithEmptyIpAddress(SecurityGroupOptions securityGroupOptions, ISecurityGroup securityGroup, SecurityGroupRuleOptions securitygroupRuleOptions, SecurityGroupRuleType securityGroupRuleType)
        {
            var securityGroupAllowed = LocateSecurityGroup(securitygroupRuleOptions.SecurityGroupId,
                $"The security group id {securitygroupRuleOptions.SecurityGroupId} was found in the list of ingress rules of the security group {securityGroupOptions.SecurityGroupName}, that security group does not exist");

            if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
            {
                securityGroup.AddIngressRule(securityGroupAllowed, Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
            else
            {
                securityGroup.AddEgressRule(securityGroupAllowed, Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
        }

        private void AddPortRangeSecurityGroupRule(SecurityGroupOptions securityGroupOptions, ISecurityGroup securityGroup, SecurityGroupRuleOptions ruleOptions, bool ipAddressIsEmpty, SecurityGroupRuleType ruleType)
        {
            if ((ruleOptions.PortRangeStart != null || ruleOptions.PortRangeEnd != null) && (ruleOptions.PortRangeStart == null || ruleOptions.PortRangeEnd == null))
            {
                throw new ArgumentException("A Port Range must specify both a start port and an end port");
            }
            else
            {
                if (ruleOptions.PortRangeStart != null && ruleOptions.PortRangeEnd != null)
                {
                    if (ipAddressIsEmpty)
                    {
                        AddRuleWithEmptyIpAddress(securityGroupOptions, securityGroup, ruleOptions, ruleType);
                    }
                    else
                    {
                        AddRuleWithIpAddress(securityGroup, ruleOptions, ruleType);
                    }
                }
            }
        }

        private void AddPortSecurityGroupRule(SecurityGroupOptions securityGroupOptions, ISecurityGroup securityGroup, SecurityGroupRuleOptions ruleOptions, bool ipAddressIsEmpty, SecurityGroupRuleType securityGroupRuleType)
        {
            if (ruleOptions.Port != null)
            {
                if (ipAddressIsEmpty)
                {
                    var securityGroupAllowed = LocateSecurityGroup(ruleOptions.SecurityGroupId,
                        $"The security group id {ruleOptions.SecurityGroupId} was found in the list of ingress rules of the security group {securityGroupOptions.SecurityGroupName}, that security group does not exist");

                    if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
                    {
                        securityGroup.AddIngressRule(securityGroupAllowed, Port.Tcp(ruleOptions.Port.Value), ruleOptions.Description);
                    }
                    else
                    {
                        securityGroup.AddEgressRule(securityGroupAllowed, Port.Tcp(ruleOptions.Port.Value), ruleOptions.Description);
                    }
                }
                else
                {
                    if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
                    {
                        securityGroup.AddIngressRule(Peer.Ipv4(ruleOptions.IpAddress), Port.Tcp(ruleOptions.Port.Value), ruleOptions.Description);
                    }
                    else
                    {
                        securityGroup.AddEgressRule(Peer.Ipv4(ruleOptions.IpAddress), Port.Tcp(ruleOptions.Port.Value), ruleOptions.Description);
                    }
                }
            }
        }

        private static void CheckSecurityGroupRuleParams(SecurityGroupRuleOptions ruleOptions, bool securityGroupIdIsEmpty, bool ipAddressIsEmpty, SecurityGroupRuleType securityGroupRuleType)
        {
            var ruleType = securityGroupRuleType == SecurityGroupRuleType.IngressRule ? "ingress" : "egress";

            if (securityGroupIdIsEmpty && ipAddressIsEmpty)
            {
                throw new ArgumentException($"An {ruleType} rule must contain either a security group id or an IP address");
            }

            if (!securityGroupIdIsEmpty && !ipAddressIsEmpty)
            {
                throw new ArgumentException($"An {ruleType} rule cannot contain both a security group id and an IP address");
            }

            if (ruleOptions.Port == null && ruleOptions.PortRangeStart == null && ruleOptions.PortRangeEnd == null)
            {
                throw new ArgumentException($"An {ruleType} rule must contain either a Port or a Port Range");
            }
        }

        private ISecurityGroup LocateSecurityGroup(SecurityGroupOptions securityGroupOptions)
        {
            ISecurityGroup securityGroup;

            if (!string.IsNullOrWhiteSpace(securityGroupOptions.SecurityGroupId))
            {
                securityGroup = AwsCdkHandler.LocateSecurityGroupById(securityGroupOptions.SecurityGroupId, securityGroupOptions.SecurityGroupId);
            }
            else if (!string.IsNullOrWhiteSpace(securityGroupOptions.SecurityGroupName))
            {
                var securityGroupVpc = LocateVpc(securityGroupOptions.VpcId, $"The Vpc id {securityGroupOptions.VpcId} of the security group {securityGroupOptions.SecurityGroupName} was not found");
                securityGroup = AwsCdkHandler.LocateSecurityGroupByName(securityGroupOptions.Id, securityGroupOptions.SecurityGroupName, securityGroupVpc);
            }
            else
            {
                throw new ArgumentNullException($"Either SecurityGroupId or SecurityGroupName must be provided to locate a Security Group, Id: {securityGroupOptions.Id}");
            }

            return securityGroup;
        }

        private ISecurityGroup LocateSecurityGroup(string securityGroupId, string exceptionMessageIfSecurityGroupDoesNotExist, string exceptionMessageIfSecurityGroupIsEmpty = null)
        {
            return StackResources.Locate<ISecurityGroup>(securityGroupId, exceptionMessageIfSecurityGroupDoesNotExist, exceptionMessageIfSecurityGroupIsEmpty);
        }
    }
}
