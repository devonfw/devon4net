using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class InlinePolicyOptions
    {
        public string Id { get; set; }
        public List<InlinePolicyStatementOptions> PolicyStatements { get; set; }
    }
}
