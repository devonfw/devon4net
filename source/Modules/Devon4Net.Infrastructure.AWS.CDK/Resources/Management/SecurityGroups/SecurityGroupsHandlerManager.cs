using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : ISecurityGroupsHandlerManager
    {
        public ISecurityGroup AddSecurityGroup(string securityId, string securityGroupName, Vpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false)
        {
            return HandlerResources.AwsSecurityGroupHandler.Create(securityId, securityGroupName, vpc, allowAllOutbound, disableInlineRules);
        }

        public ISecurityGroup AddSecurityGroup(string securityId, string securityGroupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false)
        {
            return HandlerResources.AwsSecurityGroupHandler.Create(securityId, securityGroupName, vpc, allowAllOutbound, disableInlineRules);
        }

        public ISecurityGroup LocateSecurityGroupById(string securityId, string securityGroupId)
        {
            return HandlerResources.AwsSecurityGroupHandler.LocateById(securityId, securityGroupId);
        }

        public ISecurityGroup LocateSecurityGroupByName(string securityId, string securityGroupName, IVpc securityGroupVpc)
        {
            return HandlerResources.AwsSecurityGroupHandler.LocateByName(securityId, securityGroupName, securityGroupVpc);
        }
    }
}
