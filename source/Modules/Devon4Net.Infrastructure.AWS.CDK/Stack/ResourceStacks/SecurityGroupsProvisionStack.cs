using Amazon.CDK.AWS.EC2;
using Devon4Net.Infrastructure.AWS.CDK.Enums;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateSecurityGroups()
        {
            if (CdkOptions == null || CdkOptions.SecurityGroups?.Any() != true) return;

            foreach (var securityGroupOption in CdkOptions.SecurityGroups)
            {
                if (securityGroupOption.LocateInsteadOfCreate)
                {
                    var securityGroup = AwsCdkHandler.LocateSecurityGroupById(securityGroupOption.SecurityGroupId, securityGroupOption.SecurityGroupId);
                    StackResources.SecurityGroups.Add(securityGroupOption.Id, securityGroup);
                }
                else
                {
                    var vpcResource = StackResources.Vpcs.FirstOrDefault(v => v.Key == securityGroupOption.VpcId);
                    var vpc = vpcResource.Value ?? AwsCdkHandler.LocateVpc(securityGroupOption.VpcId, $"The Vpc id {securityGroupOption.VpcId} of the security group {securityGroupOption.SecurityGroupName} was not found");

                    var securityGroup = AwsCdkHandler.AddSecurityGroup(securityGroupOption.SecurityGroupName, securityGroupOption.SecurityGroupName, vpc, securityGroupOption.AllowAllOutbound, securityGroupOption.DisableInlineRules);
                    AddSecurityGroupRules(securityGroupOption, securityGroup, SecurityGroupRuleType.IngressRule);
                    AddSecurityGroupRules(securityGroupOption, securityGroup, SecurityGroupRuleType.EgressRule);
                    StackResources.SecurityGroups.Add(securityGroupOption.Id, securityGroup);
                }
            }
        }

        private void AddSecurityGroupRules(SecurityGroupOptions securityGroup, ISecurityGroup sg, SecurityGroupRuleType securityGroupRuleType)
        {
            if (securityGroup.IngressRules?.Any() != true)
            {
                return;
            }

            foreach (var ingressRule in securityGroup.IngressRules)
            {
                var securityGroupIdIsEmpty = string.IsNullOrWhiteSpace(ingressRule.SecurityGroupId);
                var ipAddressIsEmpty = string.IsNullOrWhiteSpace(ingressRule.IpAddress);

                CheckSecurityGroupIngressRulesParams(ingressRule, securityGroupIdIsEmpty, ipAddressIsEmpty, securityGroupRuleType);

                CheckIngresPortAndIPAdress(securityGroup, sg, ingressRule, ipAddressIsEmpty, securityGroupRuleType);

                CheckPortRangeStart(securityGroup, sg, ingressRule, ipAddressIsEmpty, securityGroupRuleType);
            }
        }

        private void CheckPortRangeStart(SecurityGroupOptions securityGroup, ISecurityGroup sg, SecurityGroupRuleOptions securitygroupRuleOptions, bool ipAddressIsEmpty, SecurityGroupRuleType securityGroupRuleType)
        {
            if ((securitygroupRuleOptions.PortRangeStart != null || securitygroupRuleOptions.PortRangeEnd != null) && (securitygroupRuleOptions.PortRangeStart == null || securitygroupRuleOptions.PortRangeEnd == null))
            {
                throw new ArgumentException("A Port Range must specify both a start port and an end port");
            }
            else
            {
                if (securitygroupRuleOptions.PortRangeStart != null && securitygroupRuleOptions.PortRangeEnd != null)
                {
                    if (ipAddressIsEmpty)
                    {
                        AddRuleWithEMptyIPAddress(securityGroup, sg, securitygroupRuleOptions, securityGroupRuleType);
                    }
                    else
                    {
                        AddRuleWithIPAddress(sg, securitygroupRuleOptions, securityGroupRuleType);
                    }
                }
            }
        }

        private static void AddRuleWithIPAddress(ISecurityGroup sg, SecurityGroupRuleOptions securitygroupRuleOptions, SecurityGroupRuleType securityGroupRuleType)
        {
            if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
            {
                sg.AddIngressRule(Peer.Ipv4(securitygroupRuleOptions.IpAddress), Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
            else
            {
                sg.AddEgressRule(Peer.Ipv4(securitygroupRuleOptions.IpAddress), Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
        }

        private void AddRuleWithEMptyIPAddress(SecurityGroupOptions securityGroup, ISecurityGroup sg, SecurityGroupRuleOptions securitygroupRuleOptions, SecurityGroupRuleType securityGroupRuleType)
        {
            var securityGroupAllowed = LocateSecurityGroup(securitygroupRuleOptions.SecurityGroupId,
                $"The security group id {securitygroupRuleOptions.SecurityGroupId} was found in the list of ingress rules of the security group {securityGroup.SecurityGroupName}, that security group does not exist");

            if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
            {
                sg.AddIngressRule(securityGroupAllowed, Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
            else
            {
                sg.AddEgressRule(securityGroupAllowed, Port.TcpRange(securitygroupRuleOptions.PortRangeStart.Value, securitygroupRuleOptions.PortRangeEnd.Value), securitygroupRuleOptions.Description);
            }
        }

        private void CheckIngresPortAndIPAdress(SecurityGroupOptions securityGroup, ISecurityGroup sg, SecurityGroupRuleOptions ingressRule, bool ipAddressIsEmpty, SecurityGroupRuleType securityGroupRuleType)
        {
            if (ingressRule.Port != null)
            {
                if (ipAddressIsEmpty)
                {
                    var securityGroupAllowed = LocateSecurityGroup(ingressRule.SecurityGroupId,
                        $"The security group id {ingressRule.SecurityGroupId} was found in the list of ingress rules of the security group {securityGroup.SecurityGroupName}, that security group does not exist");

                    if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
                    {
                        sg.AddIngressRule(securityGroupAllowed, Port.Tcp(ingressRule.Port.Value), ingressRule.Description);
                    }
                    else
                    {
                        sg.AddEgressRule(securityGroupAllowed, Port.Tcp(ingressRule.Port.Value), ingressRule.Description);
                    }
                }
                else
                {
                    if (securityGroupRuleType == SecurityGroupRuleType.IngressRule)
                    {
                        sg.AddIngressRule(Peer.Ipv4(ingressRule.IpAddress), Port.Tcp(ingressRule.Port.Value), ingressRule.Description);
                    }
                    else
                    {
                        sg.AddEgressRule(Peer.Ipv4(ingressRule.IpAddress), Port.Tcp(ingressRule.Port.Value), ingressRule.Description);
                    }
                }
            }
        }

        private static void CheckSecurityGroupIngressRulesParams(SecurityGroupRuleOptions ingressRule, bool securityGroupIdIsEmpty, bool ipAddressIsEmpty, SecurityGroupRuleType securityGroupRuleType)
        {
            var typeRule = securityGroupRuleType == SecurityGroupRuleType.IngressRule ? "ingress" : "egress";

            if (securityGroupIdIsEmpty && ipAddressIsEmpty)
            {
                throw new ArgumentException($"An {typeRule} rule must contain either a security group id or an IP address");
            }

            if (!securityGroupIdIsEmpty && !ipAddressIsEmpty)
            {
                throw new ArgumentException($"An {typeRule} rule cannot contain both a security group id and an IP address");
            }

            if (ingressRule.Port == null && ingressRule.PortRangeStart == null && ingressRule.PortRangeEnd == null)
            {
                throw new ArgumentException($"An {typeRule} rule must contain either a Port or a Port Range");
            }
        }

        private ISecurityGroup LocateSecurityGroup(string securityGroupId, string exceptionMessageIfSecurityGroupDoesNotExist, string exceptionMessageIfSecurityGroupIsEmpty = null)
        {
            return StackResources.Locate<ISecurityGroup>(securityGroupId, exceptionMessageIfSecurityGroupDoesNotExist, exceptionMessageIfSecurityGroupIsEmpty);
        }
    }
}
