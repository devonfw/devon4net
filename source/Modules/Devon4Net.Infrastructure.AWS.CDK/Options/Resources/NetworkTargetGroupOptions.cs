using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class NetworkTargetGroupOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Port { get; set; }
        public string VpcId { get; set; }
        public int HealthCheckCount { get; set; }
    }
}
