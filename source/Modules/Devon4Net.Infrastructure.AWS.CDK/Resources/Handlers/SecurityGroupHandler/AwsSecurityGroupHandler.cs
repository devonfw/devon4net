using System;
using Amazon.CDK.AWS.EC2;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.VPC;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SecurityGroupHandler
{
    public class AwsSecurityGroupHandler : AwsCdkBaseHandler, IAwsSecurityGroupHandler
    {
        private AwsCdkVpcHandler AwsCdkVpcHandler { get; }
        private TagHandler TagHandler { get; }
        public AwsSecurityGroupHandler(Construct scope, string applicationName, string environmentName, AwsCdkVpcHandler awsCdkVpcHandler, string region) : base(scope, applicationName, environmentName, region)
        {
            AwsCdkVpcHandler = awsCdkVpcHandler;
            TagHandler = new TagHandler();
        }

        public ISecurityGroup Locate(string securityId, string securityGroupId)
        {
            return SecurityGroup.FromLookupById(Scope, securityId, securityGroupId);
        }

        public ISecurityGroup Create(string identification, string groupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false)
        {
            var securityGroup = new SecurityGroup(Scope, identification, new SecurityGroupProps
            {
                AllowAllOutbound = allowAllOutbound,
                SecurityGroupName = groupName,
                Vpc = vpc,
                DisableInlineRules = disableInlineRules
            });

            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{groupName}SecurityGroup", securityGroup);
            return securityGroup;
        }

        public ISecurityGroup Create(string identification, string groupName, string vpcId, string vpcIdentification, bool allowAllOutbound = false, bool disableInlineRules = false)
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

            return Create(identification, groupName, vpc, allowAllOutbound);
        }
    }
}
