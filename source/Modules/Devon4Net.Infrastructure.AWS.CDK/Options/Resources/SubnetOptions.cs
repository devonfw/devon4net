using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SubnetOptions
    {
        public List<SubnetGroupOptions> SubnetGroups { get; set; }
        public List<SubnetItemOptions> Subnets { get; set; }
    }
}
