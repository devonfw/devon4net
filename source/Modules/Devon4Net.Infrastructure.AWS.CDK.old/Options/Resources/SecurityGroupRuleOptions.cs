namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SecurityGroupRuleOptions
    {
        public string SecurityGroupId { get; set; }
        public string IpAddress { get; set; }
        public int? Port { get; set; }
        public double? PortRangeStart { get; set; }
        public double? PortRangeEnd { get; set; }
        public string Description { get; set; }
    }
}
