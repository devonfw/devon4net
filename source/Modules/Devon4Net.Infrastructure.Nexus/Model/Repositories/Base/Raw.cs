using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Raw
    {
        [JsonPropertyName("contentDisposition")]
        public string ContentDisposition { get; set; }
    }
}
