using Amazon.CDK.AWS.DynamoDB;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Management.ParameterStore;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.DynamoDB
{
    public partial class AwsCdkHandlerManager : IParameterStoreHandlerManager
    {
        public ITable AddDynamoDBProvisioned(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int? readCapacity, int? writeCapacity, int removalPolicy, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null)
        {
            return HandlerResources.AwsCdkDynamoDBHandler.CreateProvisioned(id, tableName, partitionKeyName, partitionKeyType, billingMode, contributorInsights, pointInTimeRecovery, readCapacity, writeCapacity, removalPolicy, sortKeyName, sortKeyType, timeToLiveAttribute);
        }
        public ITable AddDynamoDBPayPerRequest(string id, string tableName, string partitionKeyName, int partitionKeyType, int? billingMode, bool contributorInsights, bool pointInTimeRecovery, int removalPolicy, string sortKeyName, int? sortKeyType, string timeToLiveAttribute = null)
        {
            return HandlerResources.AwsCdkDynamoDBHandler.Create(id, tableName, partitionKeyName, partitionKeyType, billingMode, contributorInsights, pointInTimeRecovery, removalPolicy, sortKeyName, sortKeyType, timeToLiveAttribute);
        }
    }
}
