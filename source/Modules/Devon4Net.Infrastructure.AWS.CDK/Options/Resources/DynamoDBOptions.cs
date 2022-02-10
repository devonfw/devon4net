namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DynamoDBOptions
    {
        public string Id { get; set; }
        public string TableName { get; set; }
        public string PartitionKeyName { get; set; }
        public int PartitionKeyType { get; set; }
        public int? BillingMode { get; set; }
        public bool ContributorInsights { get; set; }
        public bool PointInTimeRecovery { get; set; }
        public int? ReadCapacity { get; set; }
        public int? WriteCapacity { get; set; }
        public int RemovalPolicy { get; set; }
        public string SortKeyName { get; set; }
        public int? SortKeyType { get; set; }
        public string TimeToLiveAttribute { get; set; }
    }
}
