using Amazon.CDK.AWS.DynamoDB;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers
{
    public interface IAwsCdkDynamoDBHandler
    {
        ITable Create(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null); //NOSONAR number of params
        ITable CreateProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingModeInt, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicyInt, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null); //NOSONAR number of params
    }
}