using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class SecurityGroupEntity
    {
        public string Identification { get; set; }
        public string GroupName { get; set; }
        public bool AllowGroupAllOutbound { get; set; }
        public IVpc Vpc { get; set; }
        public IPeer IngressPeer { get; set; }
        public Port IngressPort { get; set; }
        public IPeer EgressPeer { get; set; }
        public Port EgressPort { get; set; }
    }
}
