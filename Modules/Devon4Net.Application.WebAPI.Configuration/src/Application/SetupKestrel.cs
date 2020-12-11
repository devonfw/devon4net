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
        public static void Configure(IWebHostBuilder webBuilder, IConfiguration configuration)
        {
            GetConfigurationValues(configuration, out var httpProtocol, out var sslProtocol, out var applicationPort, out var useHttps, out var maxConcurrentConnections, out var maxConcurrentUpgradedConnections, out var keepAliveTimeout, out var maxRequestBodySize, out var http2MaxStreamsPerConnection, out var http2InitialConnectionWindowSize, out var http2InitialStreamWindowSize, out var allowSynchronousIo);

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
                    if (http2InitialConnectionWindowSize >= 65535 && http2InitialConnectionWindowSize <= Math.Pow(2,31))
                    {
                        options.Limits.Http2.InitialConnectionWindowSize = http2InitialConnectionWindowSize;
                    }

                    if (http2InitialStreamWindowSize >= 65535 && http2InitialStreamWindowSize <= Math.Pow(2, 31))
                    {
                        options.Limits.Http2.InitialStreamWindowSize = http2InitialStreamWindowSize;
                    }

                    options.AllowSynchronousIO = allowSynchronousIo;


                    if (!useHttps) return;
                    
                    var httpsOptions = new HttpsConnectionAdapterOptions();
                    var kestrelCertificate = configuration["devonfw:Kestrel:ServerCertificate:Certificate"];
                    bool.TryParse(configuration["devonfw:Kestrel:ClientCertificate:RequireClientCertificate"], out bool requireClientCertificate);
                    bool.TryParse(configuration["devonfw:Kestrel:ClientCertificate:CheckCertificateRevocation"], out bool checkCertificateRevocation);

                    httpsOptions.SslProtocols = GetTlsProtocol(sslProtocol);

                    httpsOptions.ClientCertificateMode = requireClientCertificate ? ClientCertificateMode.RequireCertificate : ClientCertificateMode.NoCertificate;
                    httpsOptions.CheckCertificateRevocation = checkCertificateRevocation;

                    if (!string.IsNullOrEmpty(kestrelCertificate))
                    {
                        var kestrelCertificatePassword = configuration["devonfw:Kestrel:ServerCertificate:CertificatePassword"];
                        httpsOptions.ServerCertificate = LoadServerCertificate(kestrelCertificate, kestrelCertificatePassword);
                    }

                    listenOptions.UseHttps(httpsOptions);
                });
            });
        }

        private static void GetConfigurationValues(IConfiguration configuration, out string httpProtocol, out string sslProtocol,
            out int applicationPort, out bool useHttps, out long maxConcurrentConnections,
            out long maxConcurrentUpgradedConnections, out long keepAliveTimeout, out long maxRequestBodySize,
            out int http2MaxStreamsPerConnection, out int http2InitialConnectionWindowSize,
            out int http2InitialStreamWindowSize, out bool allowSynchronousIo)
        {
            httpProtocol = configuration["devonfw:Kestrel:HttpProtocol"];
            sslProtocol = configuration["devonfw:Kestrel:SslProtocol"];
            int.TryParse(configuration["devonfw:Kestrel:ApplicationPort"], out applicationPort);
            bool.TryParse(configuration["devonfw:Kestrel:UseHttps"], out useHttps);
            long.TryParse(configuration["devonfw:Kestrel:MaxConcurrentConnections"], out maxConcurrentConnections);
            long.TryParse(configuration["devonfw:Kestrel:MaxConcurrentUpgradedConnections"], out maxConcurrentUpgradedConnections);
            long.TryParse(configuration["devonfw:Kestrel:KeepAliveTimeout"], out keepAliveTimeout);
            long.TryParse(configuration["devonfw:Kestrel:MaxRequestBodySize"], out maxRequestBodySize);
            int.TryParse(configuration["devonfw:Kestrel:Http2MaxStreamsPerConnection"], out http2MaxStreamsPerConnection);
            int.TryParse(configuration["devonfw:Kestrel:Http2InitialConnectionWindowSize"], out http2InitialConnectionWindowSize);
            int.TryParse(configuration["devonfw:Kestrel:Http2InitialStreamWindowSize"], out http2InitialStreamWindowSize);
            bool.TryParse(configuration["devonfw:Kestrel:AllowSynchronousIO"], out allowSynchronousIo);
        }

        private static X509Certificate2 LoadServerCertificate(string kestrelCertificate, string kestrelCertificatePassword)
        {
            return new X509Certificate2(File.ReadAllBytes(FileOperations.GetFileFullPath(kestrelCertificate)), kestrelCertificatePassword, X509KeyStorageFlags.MachineKeySet);
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
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private static HttpProtocols GetHttProtocol(string httpProtocol)
        {
            if (httpProtocol == null) return HttpProtocols.Http1;

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