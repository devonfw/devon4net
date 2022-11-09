using Amazon.CDK.AWS.Logs;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateLogGroups()
        {
            if (CdkOptions == null || CdkOptions.LogGroups?.Any() != true) return;

            foreach (var logGroupOption in CdkOptions.LogGroups)
            {
                GetLogGroupResouces(logGroupOption, out var lambdaFunctionName, out var retentionTime);

                var logGroup = AwsCdkHandler.CreateLogGroup($"{lambdaFunctionName}-log-group", lambdaFunctionName, retentionTime);

                StackResources.LogGroups.Add(logGroupOption.Id, logGroup);

                if (logGroupOption.SubscribedLambdaIds?.Any() == true)
                {
                    foreach (var subscribedLambdaId in logGroupOption.SubscribedLambdaIds)
                    {
                        var lambdaFunction = LocateLambda(subscribedLambdaId, $"The lambda function id {logGroupOption.FunctionId} in the log group {logGroupOption.Id} was not found", $"The lambda function id in the log group {logGroupOption.Id} cannot be empty");
                        AwsCdkHandler.AddLogGroupTriggerToLambda($"{subscribedLambdaId}LogTrigger", lambdaFunction, logGroup);
                    }
                }
            }
        }

        private void GetLogGroupResouces(LogGroupOptions logGroup, out string lambdaFunctionName, out RetentionDays retentionTime)
        {
            // Find lambda name associated with this log group
            lambdaFunctionName = CdkOptions?.Lambdas?.FirstOrDefault(x => x.Id == logGroup.FunctionId)?.FunctionName;
            if (string.IsNullOrWhiteSpace(lambdaFunctionName))
            {
                throw new ArgumentException($"The lambda function id {logGroup.FunctionId} in the log group {logGroup.Id} was not found");
            }

            // Parse log retention time
            if (string.IsNullOrWhiteSpace(logGroup.LogRetentionTime))
            {
                retentionTime = RetentionDays.INFINITE;
            }
            else
            {
                if (!Enum.TryParse(logGroup.LogRetentionTime, out retentionTime))
                {
                    throw new ArgumentException($"The log retention time {logGroup.LogRetentionTime} of the log group {logGroup.Id} does not have a valid value");
                }
            }
        }
    }
}
