using Amazon.CDK.AWS.DynamoDB;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IDynamoDBHandlerManager
    {
        ITable AddDynamoDBProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicy, string sortKeyName, int? sortKeyType);
        ITable AddDynamoDBPayPerRequest(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int removalPolicy, string sortKeyName, int? sortKeyType);
    }
}