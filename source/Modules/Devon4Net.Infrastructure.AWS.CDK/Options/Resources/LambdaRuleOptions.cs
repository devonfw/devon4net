namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class LambdaRuleOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TriggerHour { get; set; }
        public string TriggerMinute { get; set; }
        public string LambdaName { get; set; }
        public LambdaRuleS3Options S3 { get; set; }
    }

    public class LambdaRuleS3Options
    {
        public string[] Operations { get; set; }
        public string[] BucketIds { get; set; }
    }
}
