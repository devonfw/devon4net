using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class RepositoryHosted : Repository
    {
        [JsonPropertyName("cleanup")]
        public Cleanup Cleanup { get; set; }

        [JsonPropertyName("component")]
        public RepoComponent Component { get; set; }
    }

    public class Cleanup
    {
        [JsonPropertyName("policyNames")]
        public List<string> PolicyNames { get; set; }
    }

    public class RepoComponent
    {
        [JsonPropertyName("proprietaryComponents")]
        public bool ProprietaryComponents { get; set; }
    }
}
