using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Devon4Net.Infrastructure.CircuitBreaker.Common;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using HttpClientHandler = Devon4Net.Infrastructure.CircuitBreaker.Handler.HttpClientHandler;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CircuitBreakerConfiguration
    {
        private static bool CheckCertificate { get; set; }

        public static void SetupCircuitBreaker(this IServiceCollection services, ref IConfiguration configuration)
        {
            var circuitBreakerOptions = services.GetTypedOptions<CircuitBreakerOptions>(configuration, "CircuitBreaker");

            if (circuitBreakerOptions?.Endpoints == null || !circuitBreakerOptions.Endpoints.Any())
            {
                return;
            }

            services.AddSingleton(typeof(IBuiltInTypes), typeof(BuiltInTypes));

            CheckCertificate = circuitBreakerOptions.CheckCertificate;
            services.AddHttpClient(circuitBreakerOptions.Endpoints);
            services.AddTransient<IHttpClientHandler, HttpClientHandler>();
        }

        private static void AddHttpClient(this IServiceCollection services, Endpoint endPointEntity)
        {
            if (endPointEntity == null) throw new ArgumentNullException("endPointEntity", "The end point provided is null");

            var waitAndSync = endPointEntity.GetWaitAndRetry();
            var waitAndSyncList = waitAndSync.Select(w => new TimeSpan(Convert.ToInt32(w))).ToList();

            services.AddHttpClient(endPointEntity.Name, client =>
                {
                    client.BaseAddress = new Uri(endPointEntity.BaseAddress);

                    foreach (var (key, value) in endPointEntity.GetHeaders())
                    {
                        client.DefaultRequestHeaders.Add(key, value);
                    }

                }).ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetHttpMessageHandler();

                    if (!endPointEntity.UseCertificate)
                    {
                        return handler;
                    }

                    if (endPointEntity.Certificate == null)
                    {
                        throw new ApplicationException($"The endpoint {endPointEntity.Name} has the flag 'UseCertificate=true' but there is no certificate defined");
                    }

                    var certificate = new X509Certificate2(FileOperations.GetFileFullPath(endPointEntity.Certificate), endPointEntity.CertificatePassword);
                    handler.SslProtocols = string.IsNullOrEmpty(endPointEntity.SslProtocol) ? SslProtocols.Tls12 : (SslProtocols)(Convert.ToInt32(endPointEntity.SslProtocol));
                    handler.ClientCertificates.Add(certificate);
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    return handler;

                })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(waitAndSyncList, (result, timeSpan, retryCount, context) =>
                {
                    if (waitAndSyncList.Count() != retryCount) return;
                    throw new HttpRequestException($"Error getting {endPointEntity.Name} ({endPointEntity.BaseAddress})", result.Exception);
                }))
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                     waitAndSync.Count,
                     TimeSpan.FromMilliseconds(endPointEntity.DurationOfBreak))
                );
        }

        private static System.Net.Http.HttpClientHandler GetHttpMessageHandler()
        {
            return new System.Net.Http.HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, a, e) => !CheckCertificate,
            };
        }

        private static void AddHttpClient(this IServiceCollection services, IReadOnlyCollection<Endpoint> endPointEntityList)
        {
            if (endPointEntityList == null || !endPointEntityList.Any()) throw new ArgumentNullException("endPointEntityList", "The end point List provided does not have endpoints");

            foreach (var endPointEntity in endPointEntityList)
            {
                services.AddHttpClient(endPointEntity);
            }
        }
    }
}

