namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class WafOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AssociatedApiGatewayId { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public bool? CloudWatchMetricsEnabled { get; set; }
        public bool? SampledRequestsEnabled { get; set; }
    }
}
