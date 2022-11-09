using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.CDK.Consts;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using Resource = Devon4Net.Infrastructure.AWS.CDK.Options.Resources.Resource;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
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

                if (apiGatewayOption.Resources.Count != 0)
                {
                    var apiRoot = apiGateway.Root;
                    ManageResources(apiGatewayOption.Resources, apiRoot, 0);
                }

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

        private static Amazon.CDK.AWS.APIGateway.Resource GetApiResource(IResource currentResource, Resource resource)
        {
            if (resource.IsProxy)
            {
                var proxyResourceOptions = new ProxyResourceOptions
                {
                    AnyMethod = false,
                };

                return currentResource.AddProxy(proxyResourceOptions);
            }

            return currentResource.AddResource(resource.ResourceName);
        }

        private void ManageMethods(List<Options.Resources.Method> methods, Amazon.CDK.AWS.APIGateway.Resource apiResource)
        {
            if (methods == null || methods.Count == 0)
            {
                return;
            }

            foreach (var method in methods)
            {
                GetLambdaForMethod(method.ApiLambdaName, out var apiLambdaFunction);
                AwsCdkHandler.AddApiGatewayResourceMethod(apiResource, method, apiLambdaFunction);
            }
        }

        private void ManageResources(List<Resource> resources, IResource currentResource, int currentResourcesNumber)
        {
            if (!CheckCreateResourcesInputParams(currentResourcesNumber, ref resources)) return;

            foreach (var resource in resources)
            {
                var apiResource = GetApiResource(currentResource, resource);
                ManageMethods(resource.Methods, apiResource);
                ManageResources(resource.Resources, apiResource, currentResourcesNumber + 1);
            }
        }

        private bool CheckCreateResourcesInputParams(int currentResourcesNumber, ref List<Resource> resources)
        {
            if (currentResourcesNumber >= ApiGatewayConts.MaxResourcesAllowed)
            {
                throw new ArgumentException($"Max Api-Gateway Resources Allowed exceeded (Current value: {ApiGatewayConts.MaxResourcesAllowed})");
            }

            return resources != null && resources.Count != 0;
        }

        private void GetLambdaForMethod(string apiLambdaName, out IFunction apiLambdaFunction)
        {
            // Locate lambda function
            apiLambdaFunction = LocateLambda(apiLambdaName, $"The lambda name {apiLambdaName} was not found");
        }

        private LambdaRestApi LocateApiGateway(string id, string exceptionMessageIfApiGatewayDoesNotExist, string exceptionMessageIfApiGatewayIsEmpty = null)
        {
            return StackResources.Locate<LambdaRestApi>(id, exceptionMessageIfApiGatewayDoesNotExist, exceptionMessageIfApiGatewayIsEmpty);
        }
    }
}
