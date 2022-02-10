using System;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Devon4Net.Infrastructure.AWS.CDK.Interfaces;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsSecurityGroupHandler : AwsCdkDefaultHandler, IAwsSecurityGroupHandler
    {
        private AwsCdkVpcHandler AwsCdkVpcHandler { get; }
        public AwsSecurityGroupHandler(Construct scope, string applicationName, string environmentName, AwsCdkVpcHandler awsCdkVpcHandler) : base(scope, applicationName, environmentName)
        {
            AwsCdkVpcHandler = awsCdkVpcHandler;
        }

        public ISecurityGroup Locate(string securityId, string securityGroupId)
        {
            return SecurityGroup.FromLookup(Scope, securityId, securityGroupId);
        }

        public ISecurityGroup Create(string identification, string groupName, bool allowGroupAllOutbound, IVpc vpc, IPeer ingressPeer, Port ingressPort, IPeer egressPeer, Port egressPort)
        {

            var securityGroup = new SecurityGroup(Scope, identification, new SecurityGroupProps
            {
                AllowAllOutbound = allowGroupAllOutbound,
                SecurityGroupName = groupName,
                Vpc = vpc
            });

            securityGroup.AddIngressRule(ingressPeer, ingressPort);
            securityGroup.AddEgressRule(egressPeer, egressPort);
            return securityGroup;
        }

        public ISecurityGroup Create(string identification, string groupName, bool allowGroupAllOutbound, string vpcId, string vpcIdentification, IPeer ingressPeer, Port ingressPort, IPeer egressPeer, Port egressPort)
        {
            if (string.IsNullOrEmpty(vpcId))
            {
                throw new ArgumentException("The VPC id cannot be null ");
            }

            var vpc = AwsCdkVpcHandler.Locate(vpcIdentification, vpcId);

            if (vpc == null)
            {
                throw new ArgumentException($"The provided vpcId {vpcId} does not exists");
            }

            return Create(identification, groupName, allowGroupAllOutbound, vpc, ingressPeer, ingressPort, egressPeer, egressPort);
        }
    }
}
