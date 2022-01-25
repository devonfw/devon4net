using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IApiGatewayHandlerManager
    {
        public LambdaRestApi CreateLambdaRestApi(string identification, string restApiName, IFunction handler, string[] binaryMediaTypes = null, string[] allowedIps = null, string[] deniedIps = null, string stageName = "Prod", EndpointType endpointType = EndpointType.EDGE, bool enableAccessLogging = true, bool tracingEnabled = true, MethodLoggingLevel cloudWatchLoggingLevel = MethodLoggingLevel.OFF)
        {
            return HandlerResources.AwsCdkApiGatewayHandler.CreateLambdaRestApi(identification, restApiName, handler, binaryMediaTypes, allowedIps, deniedIps, stageName, endpointType, enableAccessLogging, tracingEnabled, cloudWatchLoggingLevel);
        }
    }
}
