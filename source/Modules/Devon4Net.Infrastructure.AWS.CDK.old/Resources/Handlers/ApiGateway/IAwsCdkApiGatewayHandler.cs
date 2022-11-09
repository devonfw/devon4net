using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ApiGateway
{
    public interface IAwsCdkApiGatewayHandler
    {
        LambdaRestApi CreateLambdaRestApi(string identification, string restApiName, IFunction handler, string[] binaryMediaTypes = null, string[] allowedIps = null, string[] deniedIps = null, string stageName = "Prod", EndpointType endpointType = EndpointType.EDGE, bool enableAccessLogging = true, bool tracingEnabled = true, MethodLoggingLevel cloudWatchLoggingLevel = MethodLoggingLevel.OFF); //NOSONAR number of params
        ILogGroup CreateLogGroup(string id, string apiGatewayName, RetentionDays retentionTime);
    }
}