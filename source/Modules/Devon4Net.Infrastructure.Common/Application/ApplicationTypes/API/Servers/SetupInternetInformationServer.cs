using Devon4Net.Infrastructure.Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API.Servers
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