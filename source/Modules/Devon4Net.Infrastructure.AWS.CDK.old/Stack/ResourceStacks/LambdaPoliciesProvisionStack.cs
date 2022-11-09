using Amazon.CDK.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void AddLambdaPolicies()
        {
            if (CdkOptions == null || CdkOptions.LambdaPolicies?.Any() != true) return;

            foreach (var lambdaPolicy in CdkOptions.LambdaPolicies)
            {
                GetLambdaPoliciesResources(lambdaPolicy, out var lambdaFunction, out var lambdaFunctionName);

                for (int i = 0; i < lambdaPolicy.PolicyStatements.Length; i++)
                {
                    var policyStatement = lambdaPolicy.PolicyStatements[i];

                    AwsCdkHandler.AddPermissionToLambda($"{lambdaFunctionName}-{i}", lambdaFunction, policyStatement.Principal, policyStatement.Action);
                }
            }
        }

        private void GetLambdaPoliciesResources(LambdaPolicyOptions lambdaPolicyOptions, out IFunction lambdaFunction, out string lambdaFunctionName)
        {
            // Find lambda function to apply the policies
            lambdaFunction = LocateLambda(lambdaPolicyOptions.FunctionId, $"The lambda function id {lambdaPolicyOptions.FunctionId} does not exist", $"The lambda function id {lambdaPolicyOptions.FunctionId} cannot be null");

            // Find lambda name associated with this log group
            lambdaFunctionName = CdkOptions?.Lambdas?.FirstOrDefault(x => x.Id == lambdaPolicyOptions.FunctionId)?.FunctionName;
            if (string.IsNullOrWhiteSpace(lambdaFunctionName))
            {
                throw new ArgumentException($"The lambda function id {lambdaPolicyOptions.FunctionId} in the policy {lambdaPolicyOptions.Id} was not found");
            }
        }
    }
}
