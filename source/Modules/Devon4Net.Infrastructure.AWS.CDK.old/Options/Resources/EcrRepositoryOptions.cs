using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcrRepositoryOptions
    {
        public string Id { get; set; }
        public string RepositoryName { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public bool IsMutableImage { get; set; }
        public List<EcrExpireImageRule> ExpireImageRules { get; set; }
        public bool ImageScanOnPush { get; set; }
    }
}

