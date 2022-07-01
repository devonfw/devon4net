using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SecurityGroupHandler
{
    public interface IAwsSecurityGroupHandler
    {
        ISecurityGroup Create(string identification, string groupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup Create(string identification, string groupName, string vpcId, string vpcIdentification, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup LocateById(string securityId, string securityGroupId);
        ISecurityGroup LocateByName(string securityId, string securityGroupName, IVpc securityGroupVpc);
    }
}