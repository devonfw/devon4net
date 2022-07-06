using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface ISecurityGroupsHandlerManager
    {
        ISecurityGroup AddSecurityGroup(string securityId, string securityGroupName, Vpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup AddSecurityGroup(string securityId, string securityGroupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup LocateSecurityGroupById(string securityId, string securityGroupId);
        ISecurityGroup LocateSecurityGroupByName(string securityId, string securityGroupName, IVpc securityGroupVpc);

    }
}
