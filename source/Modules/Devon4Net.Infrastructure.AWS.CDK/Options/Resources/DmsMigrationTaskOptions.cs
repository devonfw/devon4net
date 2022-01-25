namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DmsMigrationTaskOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SourceEndpointId { get; set; }
        public string TargetEndpointId { get; set; }
        public string ReplicationInstanceId { get; set; }
        public string MigrationType { get; set; }
        public string TableMappings { get; set; }
    }
}
