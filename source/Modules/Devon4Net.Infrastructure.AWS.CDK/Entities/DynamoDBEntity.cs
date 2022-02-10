using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class DynamoDBEntity
    {
        public string Id { get; set; }
        public string TableName { get; set; }
        public Attribute PartitionKey { get; set; }
        public BillingMode BillingMode { get; set; }
        public bool ContributorInsights { get; set; }
        public bool PointInTimeRecovery { get; set; }
        public int? ReadCapacity { get; set; }
        public int? WriteCapacity { get; set; }
        public RemovalPolicy RemovalPolicy { get; set; }
        public Attribute SortKey { get; set; }
        public StreamViewType Stream { get; set; }
        public string TimeToLiveAttribute { get; set; }
    }
}
