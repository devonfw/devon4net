using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Cors.Options;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CorsConfiguration
    {
        private static List<Origin> CorsOptions { get; set; }
        /// <summary>
        /// Sets up the CORS policy. Please be aware to put your CORS domains to be allowed to avoid security hot spots
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOptions = services.GetTypedOptions<List<Origin>>(configuration, "Cors");

            if (CorsOptions == null || CorsOptions.Count == 0)
            {
                SetCorsAnyOriginAllowed(ref services);
            }
            else
            {
                SetupCorsOrigins(ref services);
            }
        }

        public static void SetupCors(this IApplicationBuilder builder)
        {
            if (CorsOptions == null || CorsOptions.Count == 0)
            {
                builder.UseCors("CorsPolicy");
                return;
            }

            foreach (var policy in CorsOptions.Select(c => c.CorsPolicy))
            {
                builder.UseCors(policy);
            }
        }

        /// <summary>
        /// Disables the check of the CORS origins. Be aware of this.
        /// Sonar exclussion added to allow development purposes.
        /// </summary>
        /// <param name="services"></param>
        private static void SetCorsAnyOriginAllowed(ref IServiceCollection services)
        {
            Devon4NetLogger.Warning("CORS Options set to allow any origin!. Please review it in production environments.");
            //enables CORS and httpoptions
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin(); // NOSONAR
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Allow different cors origins defined on appsettings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="corsOptions"></param>
        private static void SetupCorsOrigins(ref IServiceCollection services)
        {
            foreach (var definition in CorsOptions)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(definition.CorsPolicy, builder =>
                    {
                        builder.WithOrigins(definition.GetOriginsList().ToArray());
                        builder.WithHeaders(definition.GetHeadersList().ToArray());
                        builder.WithMethods(definition.GetMethodsList().ToArray());
                        if (definition.AllowCredentials) builder.AllowCredentials();
                    });
                });
            }
        }
    }
}
