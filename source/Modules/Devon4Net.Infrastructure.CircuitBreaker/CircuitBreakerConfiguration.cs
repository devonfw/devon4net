using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Devon4Net.Infrastructure.CircuitBreaker.Common;
using Devon4Net.Infrastructure.CircuitBreaker.Handlers;
using Devon4Net.Infrastructure.Common.Common.Http;
using Devon4Net.Infrastructure.Common.Common.IO;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using HttpClientHandler = Devon4Net.Infrastructure.CircuitBreaker.Handlers.HttpClientHandler;

namespace Devon4Net.Infrastructure.CircuitBreaker
{
    public static class CircuitBreakerConfiguration
    {
        private static bool CheckCertificate { get; set; }

        public static void SetupCircuitBreaker(this IServiceCollection services, IConfiguration configuration)
        {
            var circuitBreakerOptions = services.GetTypedOptions<CircuitBreakerOptions>(configuration, "CircuitBreaker");

            if (circuitBreakerOptions?.Endpoints == null || circuitBreakerOptions.Endpoints.Count == 0)
            {
                return;
            }

            services.AddSingleton(typeof(IBuiltInTypes), typeof(BuiltInTypes));

            CheckCertificate = circuitBreakerOptions.CheckCertificate;
            services.AddHttpClient(circuitBreakerOptions.Endpoints);
            services.AddTransient<IHttpRequestFromContextHandler, HttpRequestFromContextHandler>();
            services.AddTransient<IHttpClientHandler, HttpClientHandler>();
        }

        private static void AddHttpClient(this IServiceCollection services, Endpoint endPointEntity)
        {
            if (endPointEntity == null) throw new ArgumentNullException(nameof(endPointEntity), "The end point provided is null");

            var waitAndSync = endPointEntity.GetWaitAndRetry();
            var waitAndSyncList = waitAndSync.ConvertAll(w => new TimeSpan(Convert.ToInt32(w)));

            services.AddHttpClient(endPointEntity.Name, client =>
            {
                client.BaseAddress = new Uri(endPointEntity.BaseAddress);

                foreach (var (key, value) in endPointEntity.GetHeaders())
                {
                    client.DefaultRequestHeaders.Add(key, value);
                }
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = GetHttpMessageHandler(endPointEntity.CompressionSupport, endPointEntity.AllowAutoRedirect);

                if (!endPointEntity.UseCertificate)
                {
                    return handler;
                }

                if (endPointEntity.Certificate == null)
                {
                    throw new ArgumentException($"The endpoint {endPointEntity.Name} has the flag 'UseCertificate=true' but there is no certificate defined");
                }

                var certificate = new X509Certificate2(FileOperations.GetFileFullPath(endPointEntity.Certificate), endPointEntity.CertificatePassword);
                handler.SslProtocols = string.IsNullOrEmpty(endPointEntity.SslProtocol) ? SslProtocols.Tls13 : ProtocolOperations.GetTlsProtocol(endPointEntity.SslProtocol);
                handler.ClientCertificates.Add(certificate);
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                return handler;
            })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(waitAndSyncList, (result, _, retryCount, _) =>
                {
                    if (waitAndSyncList.Count != retryCount) return;
                    throw new HttpRequestException($"Error getting {endPointEntity.Name} ({endPointEntity.BaseAddress})", result.Exception);
                }))
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                     waitAndSync.Count,
                     TimeSpan.FromMilliseconds(endPointEntity.DurationOfBreak))
                );
        }

        private static void AddHttpClient(this IServiceCollection services, IReadOnlyCollection<Endpoint> endPointEntityList)
        {
            if (endPointEntityList?.Any() != true) throw new ArgumentNullException(nameof(endPointEntityList), "The end point List provided does not have endpoints");

            foreach (var endPointEntity in endPointEntityList)
            {
                services.AddHttpClient(endPointEntity);
            }
        }

        private static System.Net.Http.HttpClientHandler GetHttpMessageHandler(bool compressionSupport, bool allowRedirect)
        {
            return new System.Net.Http.HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => !CheckCertificate,
                AutomaticDecompression = compressionSupport ? System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate : System.Net.DecompressionMethods.None,
                AllowAutoRedirect = allowRedirect
            };
        }
    }
}
