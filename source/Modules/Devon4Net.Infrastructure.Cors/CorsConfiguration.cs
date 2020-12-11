using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CorsConfiguration
    {
        public static void SetupCors(this IServiceCollection services, ref IConfiguration configuration)
        {
            var CorsOptions = services.GetTypedOptions<CorsOptions>(configuration, "devonfw");

            if (CorsOptions?.Origins == null)
            {
                SetCorsAnyOriginAllowed(ref services);
            }
            else
            {
                SetupCorsOrigins(ref services, ref configuration);
            }
        }

        private static void SetCorsAnyOriginAllowed(ref IServiceCollection services)
        {
            ////enables CORS and httpoptions
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
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
        private static void SetupCorsOrigins(ref IServiceCollection services, ref IConfiguration configuration)
        {
            var corsOptions = services.GetTypedOptions<CorsOptions>(configuration, "Cors");

            if (corsOptions == null)
            {
                SetCorsAnyOriginAllowed(ref services);
            }
            else
            {
                foreach (var definition in corsOptions.Origins)
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
}
