
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Grpc.Constants;
using Devon4Net.Infrastructure.Grpc.Helpers;
using Devon4Net.Infrastructure.Grpc.Options;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Devon4Net.Infrastructure.Grpc
{
    /// <summary>
    /// ASP.NET Core gRPC is not currently supported on Azure App Service or IIS. The HTTP/2 implementation of Http.Sys does not support HTTP response trailing headers which gRPC relies on. For more information, see this GitHub issue.
    /// https://github.com/dotnet/AspNetCore/issues/9020
    /// </summary>
    public static class GrpcConfiguration
    {
        private static GrpcOptions GrpcOptions { get; set; }

        public static void SetupGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            GrpcOptions = services.GetTypedOptions<GrpcOptions>(configuration, OptionsDefinition.Grpc);

            if (!GrpcOptions.EnableGrpc || string.IsNullOrEmpty(GrpcOptions.GrpcServer)) return;

            services.AddGrpc(options =>
            {
                options.MaxReceiveMessageSize = GrpcOptions.MaxReceiveMessageSize * 1024 * 1024; // 16 MB
            });

            var defaultMethodConfig = new MethodConfig
            {
                Names = { MethodName.Default },
                RetryPolicy = GetRetryPolicy(GrpcOptions.RetryPatternOptions)
            };

            if (GrpcOptions.UseDevCertificate)
            {
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = GetBypass
                };

                services.AddSingleton(GrpcChannel.ForAddress(GrpcOptions.GrpcServer, new GrpcChannelOptions 
                {
                    HttpHandler = httpHandler, MaxRetryAttempts = GrpcOptions.RetryPatternOptions.MaxAttempts,  
                    ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } }
                }));
            }
            else
            {
                services.AddSingleton(GrpcChannel.ForAddress(GrpcOptions.GrpcServer, new GrpcChannelOptions
                {
                    MaxRetryAttempts = GrpcOptions.RetryPatternOptions.MaxAttempts,
                    ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } }
                }));
            }
        }

        /// <summary>
        /// Auto registers the gRPC services marked with the attribute "GrpcDevonService"
        /// </summary>
        /// <param name="builder">The application builder</param>
        /// <param name="assemblyNames">List of assemblies to scan and find the gRPC services. The executting assembly will be used if not set</param>
        /// <param name="pattern">The routeendpoint patttern to add to the GET endpoint. The default value is "/"</param>
        public static void SetupGrpcServices(this IApplicationBuilder builder, List<string> assemblyNames = null, string pattern = "/", bool useRouting = true)
        {
            if (useRouting)
            {
                builder.UseRouting();
            }

            builder.UseEndpoints(endpoints =>
            {
                foreach (var assemblyName in assemblyNames)
                {
                    RegisterGrpcServices(endpoints, assemblyName);
                }

                endpoints.MapGet(pattern, async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909").ConfigureAwait(false);
                    await context.Response.WriteAsync("If you find any problem regarding certificate setup, please go to: https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1").ConfigureAwait(false);
                });
            });
        }
        private static void RegisterGrpcServices(IEndpointRouteBuilder builder, string assemblyName)
        {
            foreach (var item in GrpcServiceHelper.GetDevonGrpcServices(assemblyName))
            {
                var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod(GrpcConstants.RegisterServiceMethodName).MakeGenericMethod(item.Value);
                method.Invoke(null, new[] { builder });
            }
        }

        /// <summary>
        /// Returns if the certificate check should be bypassed. It works like setting ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <returns></returns>
        private static bool GetBypass(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return GrpcOptions.UseDevCertificate;
        }

        private static StatusCode GetRetryStatusCode(string statusCode)
        {
            if (string.IsNullOrWhiteSpace(statusCode)) return StatusCode.Unavailable;

            var parseOk = Enum.TryParse(statusCode, out StatusCode result);

            return parseOk ? result : StatusCode.Unavailable;
        }

        private static RetryPolicy GetRetryPolicy(GrpcRetrypatternOptions retryPatternOptions)
        {
            return new RetryPolicy
            {
                BackoffMultiplier = retryPatternOptions.BackoffMultiplier > 0 ? retryPatternOptions.BackoffMultiplier : 1.5,
                InitialBackoff = TimeSpan.FromSeconds(retryPatternOptions.InitialBackoffSeconds > 0 ? retryPatternOptions.InitialBackoffSeconds : 1),
                MaxBackoff = TimeSpan.FromSeconds(retryPatternOptions.MaxBackoffSeconds > 0 ? retryPatternOptions.MaxBackoffSeconds : 5),
                MaxAttempts = retryPatternOptions.MaxBackoffSeconds > 0 ? retryPatternOptions.MaxBackoffSeconds : 5,
                RetryableStatusCodes = { GetRetryStatusCode(retryPatternOptions.RetryableStatus) }
            };
        }
    }
}
