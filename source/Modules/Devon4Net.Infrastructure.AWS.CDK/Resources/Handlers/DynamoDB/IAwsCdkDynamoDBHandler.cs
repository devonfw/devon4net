using Amazon.CDK.AWS.DynamoDB;

namespace ADC.PostNL.BuildingBlocks.AWSCDK.Handlers
{
    public interface IAwsCdkDynamoDBHandler
    {
        ITable Create(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null);
        ITable CreateProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null);
    }
}