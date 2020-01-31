using Devon4Net.Infrastructure.Common.Options.Cors;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CorsConfiguration
    {
        public static void SetCorsAnyOriginAllowed(this IServiceCollection services)
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
        public static void SetupCorsOrigins(this IServiceCollection services, CorsOptions corsOptions)
        {
            if (corsOptions == null)
            {
                SetCorsAnyOriginAllowed(services);
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
