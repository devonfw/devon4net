using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcsDockerVolumeOptions
    {
        public string Name { get; set; }
        public bool AutoProvision { get; set; }
        public string Driver { get; set; }
        public string Scope { get; set; }
    }
}
