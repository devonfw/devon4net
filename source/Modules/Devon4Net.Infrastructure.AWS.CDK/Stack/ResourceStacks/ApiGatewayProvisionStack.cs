using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateApiGateways()
        {
            if (CdkOptions == null || CdkOptions.ApiGateways?.Any() != true) return;

            foreach (var apiGatewayOption in CdkOptions.ApiGateways)
            {
                GetApiGatewayResources(apiGatewayOption, out var lambdaFunction, out var enableAccessLogging, out var tracingEnabled, out var cloudWatchLoggingLevel);

                var apiGateway = AwsCdkHandler.CreateLambdaRestApi(apiGatewayOption.RestApiName, apiGatewayOption.RestApiName, lambdaFunction, apiGatewayOption.BinaryMediaTypes, enableAccessLogging: enableAccessLogging, tracingEnabled: tracingEnabled, cloudWatchLoggingLevel: cloudWatchLoggingLevel);

                StackResources.ApiGateways.Add(apiGatewayOption.Id, apiGateway);
            }
        }

        private void GetApiGatewayResources(ApiGatewayOptions apiGatewayOption, out IFunction lambdaFunction, out bool enableAccessLogging, out bool tracingEnabled, out MethodLoggingLevel cloudWatchLoggingLevel)
        {
            // Locate lambda function
            lambdaFunction = LocateLambda(apiGatewayOption.LambdaName,
                $"The lambda name {apiGatewayOption.LambdaName} of the api gateway {apiGatewayOption.RestApiName} was not found",
                $"The api gateway {apiGatewayOption.RestApiName} must have a lambda to execute");

            enableAccessLogging = apiGatewayOption.EnableAccessLogging ?? true;
            tracingEnabled = apiGatewayOption.TracingEnabled ?? true;

            if (string.IsNullOrWhiteSpace(apiGatewayOption.CloudWatchLoggingLevel))
            {
                cloudWatchLoggingLevel = MethodLoggingLevel.OFF;
            }
            else
            {
                if (!Enum.TryParse(apiGatewayOption.CloudWatchLoggingLevel, out cloudWatchLoggingLevel))
                {
                    throw new ArgumentException($"The CloudWatchLoggingLevel option {apiGatewayOption.CloudWatchLoggingLevel} of the api gateway {apiGatewayOption.RestApiName} is not correct");
                }
            }
        }

        private LambdaRestApi LocateApiGateway(string id, string exceptionMessageIfApiGatewayDoesNotExist, string exceptionMessageIfApiGatewayIsEmpty = null)
        {
            return StackResources.Locate<LambdaRestApi>(id, exceptionMessageIfApiGatewayDoesNotExist, exceptionMessageIfApiGatewayIsEmpty);
        }
    }
}
