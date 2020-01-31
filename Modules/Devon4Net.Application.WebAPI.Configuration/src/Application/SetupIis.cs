using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration.Application
{
    public static class SetupIIS
    {
        public static void ConfigureIIS(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["devonfw:UseIIS"], out bool useIis);

            if (!useIis) return;

            bool.TryParse(configuration["devonfw:IIS:UseHttps"], out bool forwardClientCertificate);
            bool.TryParse(configuration["devonfw:IIS:UseHttps"], out bool automaticAuthentication);
            var authenticationDisplayName = configuration["devonfw:IIS:AuthenticationDisplayName"];

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = forwardClientCertificate;
                options.AutomaticAuthentication = automaticAuthentication;
                options.AuthenticationDisplayName = authenticationDisplayName;
            });
        }
    }
}