using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DatabaseParameterGroupOptions
    {
        public string Id { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
