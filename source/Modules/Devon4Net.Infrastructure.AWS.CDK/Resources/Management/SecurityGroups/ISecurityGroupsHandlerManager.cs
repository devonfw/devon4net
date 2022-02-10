using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface ISecurityGroupsHandlerManager
    {
        ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, Vpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup LocateSecurityGroupById(string identification, string securityGroupId);
    }
}
