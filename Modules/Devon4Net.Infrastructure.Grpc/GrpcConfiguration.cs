using Devon4Net.Infrastructure.Common.Options;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Grpc
{
    /// <summary>
    /// ASP.NET Core gRPC is not currently supported on Azure App Service or IIS. The HTTP/2 implementation of Http.Sys does not support HTTP response trailing headers which gRPC relies on. For more information, see this GitHub issue.
    /// https://github.com/dotnet/AspNetCore/issues/9020
    /// </summary>
    public static class GrpcConfiguration
    {
        public static void SetupGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            var grpcOptions = services.GetTypedOptions<GrpcOptions>(configuration, "Grpc");

            if (grpcOptions == null || string.IsNullOrEmpty(grpcOptions.GrpcServer)) return;

#if NETCOREAPP
            services.AddGrpc(options =>
            {
                options.MaxReceiveMessageSize = grpcOptions.MaxReceiveMessageSize * 1024 * 1024; // 16 MB
            });

            services.AddSingleton(GrpcChannel.ForAddress(grpcOptions.GrpcServer));
#endif
        }
    }
}
