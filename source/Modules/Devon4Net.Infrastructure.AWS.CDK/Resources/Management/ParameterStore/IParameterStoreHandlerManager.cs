using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.SSM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IParameterStoreHandlerManager
    {
        ITable AddDynamoDBProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicy, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null);
        ITable AddDynamoDBPayPerRequest(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int removalPolicy, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null);
    }
}