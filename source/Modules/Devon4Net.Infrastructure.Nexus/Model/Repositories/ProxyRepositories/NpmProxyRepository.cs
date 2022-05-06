using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class NpmProxyRepository : RepositoryProxy
    {
        [JsonPropertyName("npm")]
        public Npm Npm { get; set; }
    }
}
