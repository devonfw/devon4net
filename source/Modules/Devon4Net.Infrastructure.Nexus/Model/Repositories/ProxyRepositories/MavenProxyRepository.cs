using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class MavenProxyRepository : NexusRepositoryProxy
    {
        [JsonPropertyName("maven")]
        public Maven Maven { get; set; }
    }
}



