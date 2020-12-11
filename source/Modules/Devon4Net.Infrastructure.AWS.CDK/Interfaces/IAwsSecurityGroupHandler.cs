using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Interfaces
{
    public interface IAwsSecurityGroupHandler
    {
        ISecurityGroup Create(string identification, string groupName, bool allowGroupAllOutbound, IVpc vpc, IPeer ingressPeer, Port ingressPort, IPeer egressPeer, Port egressPort);
        ISecurityGroup Create(string identification, string groupName, bool allowGroupAllOutbound, string vpcId, string vpcIdentification, IPeer ingressPeer, Port ingressPort, IPeer egressPeer, Port egressPort);
        ISecurityGroup Locate(string securityId, string securityGroupId);
    }
}