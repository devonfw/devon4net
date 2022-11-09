namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SecurityGroupOptions
    {
        public string Id { get; set; }
        public string SecurityGroupName { get; set; }
        public string VpcId { get; set; }
        public bool AllowAllOutbound { get; set; }
        public bool DisableInlineRules { get; set; }
        public SecurityGroupRuleOptions[] IngressRules { get; set; }
        public SecurityGroupRuleOptions[] EgressRules { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string SecurityGroupId { get; set; }
    }
}
