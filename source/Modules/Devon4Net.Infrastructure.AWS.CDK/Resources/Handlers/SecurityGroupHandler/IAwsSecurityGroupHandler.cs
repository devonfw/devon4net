using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SecurityGroupHandler
{
    public interface IAwsSecurityGroupHandler
    {
        ISecurityGroup Create(string identification, string groupName, IVpc vpc, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup Create(string identification, string groupName, string vpcId, string vpcIdentification, bool allowAllOutbound = false, bool disableInlineRules = false);
        ISecurityGroup Locate(string securityId, string securityGroupId);
    }
}