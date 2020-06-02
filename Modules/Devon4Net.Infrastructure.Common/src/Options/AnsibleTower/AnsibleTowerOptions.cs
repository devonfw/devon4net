using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.Common.Options.AnsibleTower
{
    public class AnsibleTowerOptions
    {
        public string Name { get; set; }
        public string CircuitBreakerName { get; set; }
        public string ApiUrlBase { get; set; }
        public string Version { get; set; }
    }
}
