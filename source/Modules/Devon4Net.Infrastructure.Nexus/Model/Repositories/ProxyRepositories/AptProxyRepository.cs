using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class AptProxyRepository : RepositoryProxy
    {
        [JsonPropertyName("apt")]
        public Apt Apt { get; set; }
        [JsonPropertyName("aptSigning")]
        public Aptsigning AptSigning { get; set; }
    }
}
