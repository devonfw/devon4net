using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class NexusRepositoryProxy : NexusRepository
    {
        [JsonPropertyName("proxy")]
        public Proxy Proxy { get; set; }
        [JsonPropertyName("negativeCache")]
        public Negativecache NegativeCache { get; set; }
        [JsonPropertyName("httpClient")]
        public Httpclient HttpClient { get; set; }
        [JsonPropertyName("routingRule")]
        public string RoutingRule { get; set; }
    }

    public class Httpclient
    {
        [JsonPropertyName("blocked")]
        public bool Blocked { get; set; }
        [JsonPropertyName("autoBlock")]
        public bool AutoBlock { get; set; }
        [JsonPropertyName("connection")]
        public Connection Connection { get; set; }
        [JsonPropertyName("authentication")]
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("ntlmHost")]
        public string NtlmHost { get; set; }
        [JsonPropertyName("ntlmDomain")]
        public string NtlmDomain { get; set; }
    }

    public class Connection
    {
        [JsonPropertyName("retries")]
        public int Retries { get; set; }
        [JsonPropertyName("userAgentSuffix")]
        public string UserAgentSuffix { get; set; }
        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }
        [JsonPropertyName("enableCircularRedirects")]
        public bool EnableCircularRedirects { get; set; }
        [JsonPropertyName("enableCookies")]
        public bool EnableCookies { get; set; }
        [JsonPropertyName("useTrustStore")]
        public bool UseTrustStore { get; set; }
    }

    public class Proxy
    {
        [JsonPropertyName("remoteUrl")]
        public string RemoteUrl { get; set; }
        [JsonPropertyName("contentMaxAge")]
        public int ContentMaxAge { get; set; }
        [JsonPropertyName("metadataMaxAge")]
        public int MetadataMaxAge { get; set; }
    }

    public class Negativecache
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        [JsonPropertyName("timeToLive")]
        public int TimeToLive { get; set; }
    }
}