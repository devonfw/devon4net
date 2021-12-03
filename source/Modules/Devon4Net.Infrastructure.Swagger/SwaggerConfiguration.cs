using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Devon4Net.Infrastructure.Swagger
{
    public static class SwaggerConfiguration
    {
        private static SwaggerOptions SwaggerOptions { get; set; }

        #region public methods
        public static void SetupSwagger(this IServiceCollection services, IConfiguration configuration, bool useSwagger = true)
        {
            SwaggerOptions = services.GetTypedOptions<SwaggerOptions>(configuration, "Swagger");

            if (!useSwagger) return;
            if (SwaggerOptions?.Endpoint == null) return;
            SetupSwaggerService(ref services);
        }

        private static void SetupSwaggerService(ref IServiceCollection services)
        {
            if (SwaggerOptions == null || SwaggerOptions.Contact == null || SwaggerOptions.License == null || SwaggerOptions.Terms == null) return;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerOptions.Version, new OpenApiInfo
                {
                    Version = SwaggerOptions.Version,
                    Title = SwaggerOptions.Title,
                    Description = SwaggerOptions.Description,
                    TermsOfService = new Uri(SwaggerOptions.Terms),
                    Contact = new OpenApiContact { Name = SwaggerOptions.Contact.Name, Email = SwaggerOptions.Contact.Email, Url = new Uri(string.IsNullOrEmpty(SwaggerOptions.Contact.Url) ? string.Empty : SwaggerOptions.Contact.Url) },
                    License = new OpenApiLicense { Name = SwaggerOptions.License.Name, Url =  new Uri(string.IsNullOrEmpty(SwaggerOptions.License.Url) ? string.Empty : SwaggerOptions.License.Url) }
                });

                foreach (var doc in GetXmlDocumentsForSwagger())
                    c.IncludeXmlComments(GetXmlCommentsPath(doc));
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(AuthConst.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Use input example: \"Bearer {token}\" without brakets.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = AuthConst.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = AuthConst.AuthenticationScheme },
                                Scheme = AuthConst.AuthenticationScheme,
                                Name = AuthConst.AuthenticationScheme,
                                In = ParameterLocation.Header,
                        }, new List<string>() }
                });
            });

            services.AddMvcCore().AddApiExplorer();
        }

        public static void ConfigureSwaggerEndPoint(this IApplicationBuilder app)
        {
            if (string.IsNullOrEmpty(SwaggerOptions?.Endpoint?.Url) || string.IsNullOrEmpty(SwaggerOptions?.Endpoint?.Name)) return;

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint(SwaggerOptions.Endpoint.Url, SwaggerOptions.Endpoint.Name); });
        }

        #endregion

        #region private methods
        private static string GetXmlCommentsPath(string assemblyName)
        {
            var basePath = AppContext.BaseDirectory;
            return Path.Combine(basePath, assemblyName);
        }

        private static List<string> GetXmlDocumentsForSwagger()
        {
            var basePath = AppContext.BaseDirectory;
            return Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
        }

        #endregion
    }
}
