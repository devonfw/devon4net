using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Devon4Net.Infrastructure.Common.Common.IO;
using Devon4Net.Infrastructure.Common.Common.Http;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Devon4Net.Application.WebAPI.Configuration.Application
{
    public static class SetupKestrel
    {
        public static void Configure(IWebHostBuilder webBuilder, IConfiguration configuration)
        {
            var httpProtocol = configuration["devonfw:Kestrel:HttpProtocol"];
            var sslProtocol = configuration["devonfw:Kestrel:SslProtocol"];
            _ = int.TryParse(configuration["devonfw:Kestrel:ApplicationPort"], out int applicationPort);
            _ = bool.TryParse(configuration["devonfw:Kestrel:UseHttps"], out bool useHttps);


            webBuilder.UseKestrel(options =>
            {
                options.AddServerHeader = false;
                options.Listen(IPAddress.Any, applicationPort, listenOptions =>
                {
                    listenOptions.Protocols = ProtocolOperations.GetHttProtocol(httpProtocol);
                    SetupCommonProperties(configuration, options);

                    if (!useHttps) return;

                    var httpsOptions = new HttpsConnectionAdapterOptions();
                    SetupHttpsProperties(configuration, sslProtocol, httpsOptions);
                    listenOptions.UseHttps(httpsOptions);
                });
            });
        }

        private static void SetupHttpsProperties(IConfiguration configuration, string sslProtocol, HttpsConnectionAdapterOptions httpsOptions)
        {
            var kestrelCertificate = configuration["Certificates:ServerCertificate:Certificate"];
            _ = bool.TryParse(configuration["Certificates:ClientCertificate:RequireClientCertificate"], out bool requireClientCertificate);
            _ = bool.TryParse(configuration["Certificates:ClientCertificate:CheckCertificateRevocation"], out bool checkCertificateRevocation);

            httpsOptions.SslProtocols = ProtocolOperations.GetTlsProtocol(sslProtocol);

            httpsOptions.ClientCertificateMode = requireClientCertificate ? ClientCertificateMode.RequireCertificate : ClientCertificateMode.NoCertificate;
            httpsOptions.CheckCertificateRevocation = checkCertificateRevocation;

            if (!string.IsNullOrEmpty(kestrelCertificate))
            {
                var kestrelCertificatePassword = configuration["Certificates:ServerCertificate:CertificatePassword"];
                httpsOptions.ServerCertificate = LoadServerCertificate(kestrelCertificate, kestrelCertificatePassword);
            }
        }

        private static void SetupCommonProperties(IConfiguration configuration, KestrelServerOptions options)
        {
            _ = long.TryParse(configuration["devonfw:Kestrel:ExtraSettings:MaxConcurrentConnections"], out long maxConcurrentConnections);
            _ = long.TryParse(configuration["devonfw:Kestrel:ExtraSettings:MaxConcurrentUpgradedConnections"], out long maxConcurrentUpgradedConnections);
            _ = long.TryParse(configuration["devonfw:Kestrel:ExtraSettings:KeepAliveTimeout"], out long keepAliveTimeout);
            _ = double.TryParse(configuration["devonfw:Kestrel:ExtraSettings:MaxRequestBodySize"], out double maxRequestBodySize);
            _ = int.TryParse(configuration["devonfw:Kestrel:ExtraSettings:Http2MaxStreamsPerConnection"], out int http2MaxStreamsPerConnection);
            _ = int.TryParse(configuration["devonfw:Kestrel:ExtraSettings:Http2InitialConnectionWindowSize"], out int Http2InitialConnectionWindowSize);
            _ = int.TryParse(configuration["devonfw:Kestrel:ExtraSettings:Http2InitialStreamWindowSize"], out int http2InitialStreamWindowSize);
            _ = bool.TryParse(configuration["devonfw:Kestrel:ExtraSettings:AllowSynchronousIO"], out bool allowSynchronousIO);

            if (maxConcurrentConnections > 0) options.Limits.MaxConcurrentConnections = maxConcurrentConnections;
            if (maxConcurrentUpgradedConnections > 0) options.Limits.MaxConcurrentUpgradedConnections = maxConcurrentUpgradedConnections;
            if (keepAliveTimeout > 0) options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(keepAliveTimeout);

            if (maxRequestBodySize >= 1 && maxRequestBodySize <= 28.6)
            {
                options.Limits.MaxRequestBodySize = (long?)(maxRequestBodySize * 1048576);
            }

            if (http2MaxStreamsPerConnection > 0) options.Limits.Http2.MaxStreamsPerConnection = http2MaxStreamsPerConnection;

            if (Http2InitialConnectionWindowSize >= 65535 && Http2InitialConnectionWindowSize <= Math.Pow(2, 31))
            {
                options.Limits.Http2.InitialConnectionWindowSize = Http2InitialConnectionWindowSize;
            }

            if (http2InitialStreamWindowSize >= 65535 && http2InitialStreamWindowSize <= Math.Pow(2, 31))
            {
                options.Limits.Http2.InitialStreamWindowSize = http2InitialStreamWindowSize;
            }

            if (allowSynchronousIO) options.AllowSynchronousIO = true;
        }

        private static X509Certificate2 LoadServerCertificate(string kestrelCertificate, string kestrelCertificatePassword)
        {
            try
            {
                return new X509Certificate2(File.ReadAllBytes(FileOperations.GetFileFullPath(kestrelCertificate)), kestrelCertificatePassword, X509KeyStorageFlags.MachineKeySet);
            }
            catch (Exception)
            {
                Devon4NetLogger.Error("Error loading the server certificate. Please check the certificate's path is not null and check the password is correct");
                throw;
            }
        }
    }
}