using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class YumProxyRepository : NexusRepositoryProxy
    {
        [JsonPropertyName("yumSigning")]
        public YumSigning YumSigning { get; set; }
    }
}
