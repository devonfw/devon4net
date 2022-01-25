namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class LogGroupOptions
    {
        public string Id { get; set; }
        public string FunctionId { get; set; }
        public string LogRetentionTime { get; set; }
        public string[] SubscribedLambdaIds { get; set; }
    }
}
