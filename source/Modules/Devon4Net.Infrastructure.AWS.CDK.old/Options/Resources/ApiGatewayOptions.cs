namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class ApiGatewayOptions
    {
        public string Id { get; set; }
        public string RestApiName { get; set; }
        public string LambdaName { get; set; }
        public string[] BinaryMediaTypes { get; set; }
        public bool? EnableAccessLogging { get; set; }
        public bool? TracingEnabled { get; set; }
        public string CloudWatchLoggingLevel { get; set; }
        public List<Resource> Resources { get; set; }
    }
    public class Resource
    {
        public string ResourceName { get; set; }
        public bool IsProxy { get; set; }
        public List<Resource> Resources { get; set; }
        public List<Method> Methods { get; set; }
    }

    public class Method
    {
        public string HttpMethod { get; set; }
        public string ApiLambdaName { get; set; }
        public IntegrationLambdaOptions IntegrationLambdaOptions { get; set; }
    }

    public class IntegrationLambdaOptions
    {
        public bool AllowTestInvoke { get; set; }
        public bool Proxy { get; set; }
    }
}
