using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.WebAPI.Implementation.Configure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Application.WebAPI
{
    /// <summary>
    /// devonfw startup
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Configuration variable with all settings file loaded
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
 
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDevonFw(Configuration);
            services.SetupDevonDependencyInjection(Configuration);
            services.AddControllers();
            services.AddOptions();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app net param</param>
        /// <param name="env">environment param</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.ConfigureDevonFw();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}