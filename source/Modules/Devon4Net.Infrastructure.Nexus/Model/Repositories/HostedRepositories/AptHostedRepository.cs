using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.HostedRepositories
{
    public class AptHostedRepository : RepositoryHosted
    {
        [JsonPropertyName("apt")]
        public Apt Apt { get; set; }
        [JsonPropertyName("aptSigning")]
        public Aptsigning AptSigning { get; set; }
    }
}
