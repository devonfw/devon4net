using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{

    public class EcrExpireImageRule
    {
        public string Description { get; set; }
        public int MaxImageAgeDays { get; set; }
        public int MaxImageNumber { get; set; }
        public int PriorityOrder { get; set; }
        public List<string> TagPrefixList { get; set; }
        public string TagStatus { get; set; }
    }
}

