using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class NugetProxyRepositoryDto : NexusRepositoryProxy
    {
        [JsonPropertyName("nugetProxy")]
        public NugetProxy NugetProxy { get; set; }
    }
}
