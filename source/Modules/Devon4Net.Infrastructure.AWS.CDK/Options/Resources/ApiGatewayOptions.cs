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
    }

}
