using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcsPortMappingOptions
    {
        public int ContainerPort { get; set; }
        public int HostPort { get; set; }
    }
}
