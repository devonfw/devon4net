using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Authentication;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    public static class ProtocolOperationsHelper
    {
        public static SslProtocols GetTlsProtocol(string httpProtocol)
        {
            return (httpProtocol.ToLower()) switch
            {
                "tls12" => SslProtocols.Tls12,
                "tls13" => SslProtocols.Tls13,
                "none" => SslProtocols.None,
                _ => SslProtocols.Tls12,
            };
        }

        public static HttpProtocols GetHttProtocol(string httpProtocol)
        {
            if (httpProtocol == null) return HttpProtocols.Http1;

            return (httpProtocol.ToLower()) switch
            {
                "http1" => HttpProtocols.Http1,
                "http2" => HttpProtocols.Http2,
                "http1andhttp2" => HttpProtocols.Http1AndHttp2,
                "none" => HttpProtocols.None,
                _ => HttpProtocols.Http1,
            };
        }
    }
}
