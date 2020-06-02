using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Devon4Net.Infrastructure.Common;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Devon4Net.Application.WebAPI.Configuration.Application
{
    public static class SetupKestrel
    {
        public static void Configure(ref IWebHostBuilder webBuilder, IConfiguration configuration)
        {
            var httpProtocol = configuration["devonfw:Kestrel:HttpProtocol"];
            var sslProtocol = configuration["devonfw:Kestrel:SslProtocol"];
            int.TryParse(configuration["devonfw:Kestrel:ApplicationPort"], out int applicationPort);
            bool.TryParse(configuration["devonfw:Kestrel:UseHttps"], out bool useHttps);
            long.TryParse(configuration["devonfw:Kestrel:MaxConcurrentConnections"], out long maxConcurrentConnections);
            long.TryParse(configuration["devonfw:Kestrel:MaxConcurrentUpgradedConnections"], out long maxConcurrentUpgradedConnections);
            long.TryParse(configuration["devonfw:Kestrel:KeepAliveTimeout"], out long keepAliveTimeout);
            long.TryParse(configuration["devonfw:Kestrel:MaxRequestBodySize"], out long maxRequestBodySize);
            int.TryParse(configuration["devonfw:Kestrel:Http2MaxStreamsPerConnection"], out int http2MaxStreamsPerConnection);
            int.TryParse(configuration["devonfw:Kestrel:Http2InitialConnectionWindowSize"], out int Http2InitialConnectionWindowSize);
            int.TryParse(configuration["devonfw:Kestrel:Http2InitialStreamWindowSize"], out int http2InitialStreamWindowSize);
            bool.TryParse(configuration["devonfw:Kestrel:AllowSynchronousIO"], out bool allowSynchronousIO);

            webBuilder.UseKestrel(options =>
            {
                options.AddServerHeader = false;
                options.Listen(IPAddress.Any, applicationPort, listenOptions =>
                {                    
                    listenOptions.Protocols = GetHttProtocol(httpProtocol);

                    options.Limits.MaxConcurrentConnections = maxConcurrentConnections;
                    options.Limits.MaxConcurrentUpgradedConnections = maxConcurrentUpgradedConnections;
                    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(keepAliveTimeout);

                    if (maxRequestBodySize >= 1 && maxRequestBodySize <= 28.6)
                    {
                        options.Limits.MaxRequestBodySize = maxRequestBodySize * 1048576;
                    }

                    options.Limits.Http2.MaxStreamsPerConnection = http2MaxStreamsPerConnection;
                    if (Http2InitialConnectionWindowSize >= 65535 && Http2InitialConnectionWindowSize <= Math.Pow(2,31))
                    {
                        options.Limits.Http2.InitialConnectionWindowSize = Http2InitialConnectionWindowSize;
                    }

                    if (http2InitialStreamWindowSize >= 65535 && http2InitialStreamWindowSize <= Math.Pow(2, 31))
                    {
                        options.Limits.Http2.InitialStreamWindowSize = http2InitialStreamWindowSize;
                    }

                    options.AllowSynchronousIO = allowSynchronousIO;


                    if (!useHttps) return;
                    
                    var httpsOptions = new HttpsConnectionAdapterOptions();
                    var kestrelCertificate = configuration["devonfw:Kestrel:ServerCertificate:Certificate"];
                    bool.TryParse(configuration["devonfw:Kestrel:ClientCertificate:RequireClientCertificate"], out bool requireClientCertificate);
                    bool.TryParse(configuration["devonfw:Kestrel:ClientCertificate:CheckCertificateRevocation"], out bool checkCertificateRevocation);

                    httpsOptions.SslProtocols = GetTlsProtocol(sslProtocol);

                    if (requireClientCertificate)
                    {                        
                        httpsOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                        httpsOptions.CheckCertificateRevocation = checkCertificateRevocation;
                    }
                    
                    if (!string.IsNullOrEmpty(kestrelCertificate))
                    {
                        var kestrelCertificatePassword = configuration["devonfw:Kestrel:ServerCertificate:CertificatePassword"];
                        httpsOptions.ServerCertificate = new X509Certificate2(File.ReadAllBytes(FileOperations.GetFileFullPath(kestrelCertificate)), kestrelCertificatePassword, X509KeyStorageFlags.MachineKeySet);
                    }

                    listenOptions.UseHttps(httpsOptions);
                });
            });
        }

        private static SslProtocols GetTlsProtocol(string httpProtocol)
        {
            return (httpProtocol.ToLower()) switch
            {
#pragma warning disable CS0618 // Type or member is obsolete
                "tls" => SslProtocols.Tls,
                "tls11" => SslProtocols.Tls11,
                "tls12" => SslProtocols.Tls12,
                "tls13" => SslProtocols.Tls13,
                "ssl2" => SslProtocols.Ssl2,
                "ssl3" => SslProtocols.Ssl3,
                "none" => SslProtocols.None,
                _ => SslProtocols.Tls12,
            };
            throw new NotImplementedException();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private static HttpProtocols GetHttProtocol(string httpProtocol)
        {
            return (httpProtocol.ToLower()) switch
            {
                "http1" => HttpProtocols.Http1,
                "http2" => HttpProtocols.Http2,
                "http1&2" => HttpProtocols.Http1AndHttp2,
                "none" => HttpProtocols.None,
                _ => HttpProtocols.Http1,
            };
        }
    }
}