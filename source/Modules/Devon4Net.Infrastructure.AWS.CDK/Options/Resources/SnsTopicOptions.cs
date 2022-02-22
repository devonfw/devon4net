using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SnsTopicOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool? Fifo { get; set; }
        public bool? ContentBasedDeduplication { get; set; }
    }
}
