using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers;

namespace ADC.PostNL.BuildingBlocks.AWSCDK.Handlers
{
    public class AwsCdkDynamoDBHandler : AwsCdkBaseHandler, IAwsCdkDynamoDBHandler
    {
        public AwsCdkDynamoDBHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }
        public ITable CreateProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null)
        {
            GetPartitionKeyAndSortKey(partitionKeyName, partitionKeyType, sortKeyName, sortKeyType, out var partitionKey, out var sortKey);
            return CreateDynamoDB(new DynamoDBEntity
            {
                Id = id,
                TableName = tableName,
                PartitionKey = partitionKey,
                BillingMode = (BillingMode)billingModeInt,
                ContributorInsights = contributorInsights,
                PointInTimeRecovery = pointInTimeRecovery,
                ReadCapacity = readCapacity ?? default,
                WriteCapacity = writeCapacity ?? default,
                RemovalPolicy = (RemovalPolicy)removalPolicyInt,
                SortKey = sortKey,
                TimeToLiveAttribute = timeToLiveAttribute
            });
        }

        public ITable Create(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null)
        {
            GetPartitionKeyAndSortKey(partitionKeyName, partitionKeyType, sortKeyName, sortKeyType, out var partitionKey, out var sortKey);
            return CreateDynamoDB(new DynamoDBEntity
            {
                Id = id,
                TableName = tableName,
                PartitionKey = partitionKey,
                BillingMode = (BillingMode)billingModeInt,
                ContributorInsights = contributorInsights,
                PointInTimeRecovery = pointInTimeRecovery,
                RemovalPolicy = (RemovalPolicy)removalPolicyInt,
                SortKey = sortKey,
                TimeToLiveAttribute = timeToLiveAttribute
            });
        }
        private static void GetPartitionKeyAndSortKey(string partitionKeyName, int partitionKeyType, string sortKeyName, int? sortKeyType, out Attribute partitionKey, out Attribute sortKey)
        {
            sortKey = null;

            partitionKey = new Attribute
            {
                Name = partitionKeyName,
                Type = (AttributeType)partitionKeyType
            };

            if (!string.IsNullOrWhiteSpace(sortKeyName) && sortKeyType != null)
            {
                sortKey = new Attribute
                {
                    Name = sortKeyName,
                    Type = (AttributeType)sortKeyType
                };
            }
        }
        private ITable CreateDynamoDB(DynamoDBEntity dynamoDB)
        {
            return new Table(Scope, dynamoDB.Id, new TableProps
            {
                TableName = dynamoDB.TableName,
                PartitionKey = dynamoDB.PartitionKey,
                BillingMode = dynamoDB.BillingMode,
                ContributorInsightsEnabled = dynamoDB.ContributorInsights,
                PointInTimeRecovery = dynamoDB.PointInTimeRecovery,
                ReadCapacity = dynamoDB.ReadCapacity,
                WriteCapacity = dynamoDB.WriteCapacity,
                RemovalPolicy = dynamoDB.RemovalPolicy,
                SortKey = dynamoDB.SortKey,
                TimeToLiveAttribute = dynamoDB.TimeToLiveAttribute
            });
        }
    }
}
