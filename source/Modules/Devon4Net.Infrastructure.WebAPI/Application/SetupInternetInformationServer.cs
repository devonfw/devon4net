using Devon4Net.Infrastructure.Common.Options.Devon;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration.Application
{
    public static class SetupInternetInformationServer
    {
        public static void ConfigureIIS(this IServiceCollection services, IisDevonOptions iISDevonOptions)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = iISDevonOptions.ForwardClientCertificate;
                options.AutomaticAuthentication = iISDevonOptions.AutomaticAuthentication;
                options.AuthenticationDisplayName = iISDevonOptions.AuthenticationDisplayName;
            });
        }
    }
}