namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateDynamoDB()
        {
            if (CdkOptions == null || CdkOptions.DynamoDB?.Any() != true) return;

            foreach (var dynamoDBOption in CdkOptions.DynamoDB)
            {
                // Provisioned
                if (dynamoDBOption.BillingMode == null || dynamoDBOption.BillingMode == 1)
                {
                    AddProvisionedDynamoDBInstance(dynamoDBOption);
                }
                else // Pay per request
                {
                    AddPayPerRequestDynamoDBInstance(dynamoDBOption);
                }
            }
        }

        private void AddPayPerRequestDynamoDBInstance(Options.Resources.DynamoDBOptions dynamoDBOption)
        {
            // Pay per request option does not allow to define Write/Read capacity
            CheckRequestInstanceParams(dynamoDBOption);

            StackResources.DynamoDBs.Add(dynamoDBOption.Id, AwsCdkHandler.AddDynamoDBPayPerRequest(dynamoDBOption.Id, dynamoDBOption.TableName, dynamoDBOption.PartitionKeyName, dynamoDBOption.PartitionKeyType, dynamoDBOption.BillingMode, dynamoDBOption.ContributorInsights, dynamoDBOption.PointInTimeRecovery, dynamoDBOption.RemovalPolicy, dynamoDBOption.SortKeyName, dynamoDBOption.SortKeyType, dynamoDBOption.TimeToLiveAttribute));
        }
        private void AddProvisionedDynamoDBInstance(Options.Resources.DynamoDBOptions dynamoDBOption)
        {
            CheckProvisionedInstanceParams(dynamoDBOption);
            StackResources.DynamoDBs.Add(dynamoDBOption.Id, AwsCdkHandler.AddDynamoDBProvisioned(dynamoDBOption.Id, dynamoDBOption.TableName, dynamoDBOption.PartitionKeyName, dynamoDBOption.PartitionKeyType, dynamoDBOption.BillingMode, dynamoDBOption.ContributorInsights, dynamoDBOption.PointInTimeRecovery, dynamoDBOption.ReadCapacity, dynamoDBOption.WriteCapacity, dynamoDBOption.RemovalPolicy, dynamoDBOption.SortKeyName, dynamoDBOption.SortKeyType, dynamoDBOption.TimeToLiveAttribute));
        }

        private static void CheckRequestInstanceParams(Options.Resources.DynamoDBOptions dynamoDBOption)
        {
            if (dynamoDBOption.WriteCapacity != null && dynamoDBOption.ReadCapacity != null)
            {
                throw new ArgumentException("Can´t define write and read capacity if the billing mode is set to Pay per Request", nameof(dynamoDBOption));
            }

            if ((dynamoDBOption.SortKeyName == null && dynamoDBOption.SortKeyType != null) || (dynamoDBOption.SortKeyName != null && dynamoDBOption.SortKeyType == null))
            {
                throw new ArgumentException("A Sort key requires a name and type", nameof(dynamoDBOption));
            }
        }

        private static void CheckProvisionedInstanceParams(Options.Resources.DynamoDBOptions dynamoDBOption)
        {
            if ((dynamoDBOption.SortKeyName == null && dynamoDBOption.SortKeyType != null) || (dynamoDBOption.SortKeyName != null && dynamoDBOption.SortKeyType == null))
            {
                throw new ArgumentException("A Sort key requires a name and type");
            }
        }
    }
}
