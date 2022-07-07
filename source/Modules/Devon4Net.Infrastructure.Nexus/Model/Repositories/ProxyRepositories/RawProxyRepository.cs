using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class RawProxyRepository : NexusRepositoryProxy
    {
        [JsonPropertyName("raw")]
        public Raw Raw { get; set; }
    }
}
