using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.Infrastructure.AWS.Serverless;

namespace Devon4Net.Application.WebAPI.AwsServerless
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDevonFw(Configuration);
            services.ConfigureDevonFwAws(Configuration);
            services.AddControllers().AddXmlSerializerFormatters();
            services.AddOptions();
            services.AddMvc(option => option.EnableEndpointRouting = false).AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });
            SetupServiceDatabase(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.ConfigureDevonFw();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

#pragma warning disable
        /// <summary>
        /// Setup here your database connections.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void SetupServiceDatabase(IServiceCollection services, IConfiguration configuration)
        {
            //services.SetupDatabase<PutACLRequest YOUR CONTEXT HERE>(configuration, "Default", DatabaseType.InMemory, ServiceLifetime.Transient, true);
        }
#pragma warning restore
    }
}
