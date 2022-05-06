using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class YumProxyRepository : RepositoryProxy
    {
        [JsonPropertyName("yumSigning")]
        public YumSigning YumSigning { get; set; }
    }
}
