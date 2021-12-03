
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Grpc.Constants;
using Devon4Net.Infrastructure.Grpc.Helpers;
using Grpc.Net.Client;
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
            GrpcOptions = services.GetTypedOptions<GrpcOptions>(configuration, "Grpc");

            if (!GrpcOptions.EnableGrpc || string.IsNullOrEmpty(GrpcOptions.GrpcServer)) return;

            services.AddGrpc(options =>
            {
                options.MaxReceiveMessageSize = GrpcOptions.MaxReceiveMessageSize * 1024 * 1024; // 16 MB
            });

            if (GrpcOptions.UseDevCertificate)
            {
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = GetBypass
                };

                services.AddSingleton(GrpcChannel.ForAddress(GrpcOptions.GrpcServer, new GrpcChannelOptions { HttpHandler = httpHandler }));
            }
            else
            {
                services.AddSingleton(GrpcChannel.ForAddress(GrpcOptions.GrpcServer));
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

    }
}
