using System.Collections.Generic;
using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class VpcEntity
    {
        public string Identification { get; set; }
        public string Cidr { get; set; }
        public double? MaxAzs { get; set; }
        public DefaultInstanceTenancy DefaultInstanceTenancy { get; set; }
        public bool EnableDnsSupport { get; set; }
        public bool EnableDnsHostnames { get; set; }
        public List<ISubnetConfiguration> SubnetConfigurations { get; set; }
        public Dictionary<string,string> Tags { get; set; }
    }
}
