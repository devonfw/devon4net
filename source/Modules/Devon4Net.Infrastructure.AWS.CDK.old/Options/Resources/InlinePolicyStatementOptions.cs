using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class InlinePolicyStatementOptions
    {
        public Effect Effect { get; set; }
        public List<string> Action { get; set; }
        public List<string> Resource { get; set; }
    }
}
