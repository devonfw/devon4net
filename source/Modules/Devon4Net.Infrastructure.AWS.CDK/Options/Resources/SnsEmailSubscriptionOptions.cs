using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SnsEmailSubscriptionOptions
    {
        public string Email { get; set; }
        public bool? Json { get; set; }
        public List<string> TopicIds { get; set; }
    }
}
