using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : ISecurityGroupsHandlerManager
    {
        public ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, Vpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false)
        {
            return HandlerResources.AwsSecurityGroupHandler.Create(identification, securityGroupName, vpc, allowAllOutbound, disableInlineRules);
        }

        public ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false)
        {
            return HandlerResources.AwsSecurityGroupHandler.Create(identification, securityGroupName, vpc, allowAllOutbound, disableInlineRules);
        }

        public ISecurityGroup LocateSecurityGroupById(string identification, string securityGroupId)
        {
            return HandlerResources.AwsSecurityGroupHandler.LocateById(identification, securityGroupId);
        }

        public ISecurityGroup LocateSecurityGroupByName(string identification, string securityGroupName, IVpc securityGroupVpc)
        {
            return HandlerResources.AwsSecurityGroupHandler.LocateByName(identification, securityGroupName, securityGroupVpc);
        }
    }
}
