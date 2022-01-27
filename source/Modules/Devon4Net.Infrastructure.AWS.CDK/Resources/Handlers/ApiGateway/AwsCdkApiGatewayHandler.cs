using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers;

namespace ADC.PostNL.BuildingBlocks.AWSCDK.Handlers
{
    public class AwsCdkApiGatewayHandler : AwsCdkBaseHandler, IAwsCdkApiGatewayHandler
    {
        public AwsCdkApiGatewayHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public LambdaRestApi CreateLambdaRestApi(string identification, string restApiName, IFunction handler, string[] binaryMediaTypes = null, string[] allowedIps = null, string[] deniedIps = null, string stageName = "Prod", EndpointType endpointType = EndpointType.EDGE, bool enableAccessLogging = true, bool tracingEnabled = true, MethodLoggingLevel cloudWatchLoggingLevel = MethodLoggingLevel.OFF)
        {
            if (allowedIps?.Any() == true && deniedIps?.Any() == true)
            {
                throw new ArgumentException($"Specifying a list of {nameof(allowedIps)} and a list of {nameof(deniedIps)} is not allowed");
            }

            var accessLogDestination = enableAccessLogging ? new LogGroupLogDestination(logGroup: CreateLogGroup($"{identification}-logGroup", restApiName, RetentionDays.ONE_WEEK)) : null;

            return new LambdaRestApi(Scope, identification, new LambdaRestApiProps
            {
                RestApiName = restApiName,
                Handler = handler,
                DeployOptions = new StageOptions
                {
                    StageName = stageName,
                    AccessLogDestination = accessLogDestination,
                    LoggingLevel = cloudWatchLoggingLevel,
                    TracingEnabled = tracingEnabled
                },
                EndpointConfiguration = new EndpointConfiguration
                {
                    Types = new EndpointType[]
                    {
                        endpointType
                    }
                },
                Policy = GeneratePolicyDocument(allowedIps, deniedIps),
                Deploy = true,
                BinaryMediaTypes = binaryMediaTypes
            });
        }

        private static PolicyDocument GeneratePolicyDocument(string[] allowedIps, string[] deniedIps)
        {
            if (allowedIps?.Any() != true && deniedIps?.Any() != true) return null;

            return new PolicyDocument(new PolicyDocumentProps
            {
                Statements = new PolicyStatement[]
                {
                    GeneratePolicyStatement(allowedIps, deniedIps),
                }
            });
        }

        private static PolicyStatement GeneratePolicyStatement(string[] allowedIps, string[] deniedIps)
        {
            var policyStatement = new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new string[]
                {
                    "execute-api:Invoke"
                },
                Resources = new string[]
                {
                    "execute-api:/*/*/*"
                },
                Conditions = CreateIpList(allowedIps, deniedIps)
            });

            policyStatement.AddAnyPrincipal();

            return policyStatement;
        }

        private static Dictionary<string, object> CreateIpList(string[] allowedIps, string[] deniedIps)
        {
            string keyName;
            string[] ipList;

            if (allowedIps?.Any() == true)
            {
                keyName = "IpAddress";
                ipList = allowedIps;
            }
            else if (deniedIps?.Any() == true)
            {
                keyName = "NotIpAddress";
                ipList = deniedIps;
            }
            else
            { // If there is neither a whitelist nor a blacklist, then there are no conditions to apply
                return default;
            }

            return new Dictionary<string, object>
            {
                {
                    keyName, new Dictionary<string, string[]>
                    {
                        {
                            "aws:SourceIp", ipList
                        }
                    }
                }
            };
        }

        public ILogGroup CreateLogGroup(string id, string apiGatewayName, RetentionDays retentionTime)
        {
            return new LogGroup(Scope, id, new LogGroupProps
            {
                LogGroupName = $"/aws/apigateway/{apiGatewayName}",
                RemovalPolicy = RemovalPolicy.DESTROY,
                Retention = retentionTime
            });
        }
    }
}
