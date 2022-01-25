namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DmsReplicationInstanceOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ReplicationInstanceClass { get; set; }
        public string SubnetGroupId { get; set; }
        public string[] SecurityGroupIds { get; set; }
        public bool? PubliclyAccessible { get; set; }
    }
}
