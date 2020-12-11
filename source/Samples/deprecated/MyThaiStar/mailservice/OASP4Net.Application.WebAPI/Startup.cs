using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OASP4Net.Infrastructure.AOP.Configuration;
using OASP4Net.Infrastructure.Cors.Configuration;
using Serilog;
using OASP4Net.Infrastructure.Cors;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using OASP4Net.Infrastructure.Middleware.Configuration;
using OASP4Net.Infrastructure.Middleware.Headers;
using OASP4Net.Application.Configuration;
using OASP4Net.Application.Configuration.Startup;
using OASP4Net.Infrastructure.Swagger.Configuration;
using OASP4Net.Infrastructure.Swagger;

namespace OASP4Net.Application.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ConfigurationManager ConfigurationManager { get; set; }

        public Startup(IConfiguration configuration)
        {
            ConfigurationManager = new ConfigurationManager();
            Configuration = ConfigurationManager.GetConfiguration();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            LoadDefinitions();

            services.ConfigurePermisiveIdentityPolicyService();
            services.AddAopAttributeService();
            services.ConfigureDependencyInjectionService();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.ConfigureSwaggerService();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.ConfigureCorsAnyOriginService();
            services.AddOptions();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCustomHeadersMiddleware();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.ConfigureUniversalCorsApplication();
            app.ConfigureSwaggerApplication();
            ConfigurationManager.ConfigureLog();            
            app.UseMvc();
        }

        public void LoadDefinitions()
        {
            Configuration.LoadCorsDefinition();
            Configuration.LoadMiddlewareDefinition();
            Configuration.LoadSwaggerDefinition();
        }
    }
}
