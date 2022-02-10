namespace Devon4Net.Infrastructure.AWS.CDK.Options.Global
{
    public class ProvisionStackOptions
    {
        public string Id { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public string AwsAccount { get; set; }
        public string AwsRegion { get; set; }
    }
}
