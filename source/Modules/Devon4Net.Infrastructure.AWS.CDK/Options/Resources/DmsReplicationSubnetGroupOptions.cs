namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DmsReplicationSubnetGroupOptions
    {
        public string Id { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] ReplicationSubnetIds { get; set; }
    }
}
