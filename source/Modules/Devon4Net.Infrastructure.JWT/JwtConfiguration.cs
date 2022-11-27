using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.JWT.Handlers;
using Devon4Net.Infrastructure.JWT.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class JwtConfiguration
    {
        public static void SetupJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = services.GetTypedOptions<JwtOptions>(configuration, OptionsDefinition.Jwt);

            if (jwtOptions == null) return;

            var jwtHandler = new JwtHandler(jwtOptions);
            services.AddSingleton<IJwtHandler>(jwtHandler);
            SetupJwtParameters(services, jwtOptions, jwtHandler);
        }

        private static void SetupJwtParameters(IServiceCollection services, JwtOptions jwtOptions, JwtHandler jwtHandler)
        {
            services.AddAuthentication(options => options.DefaultScheme = AuthConst.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = jwtOptions.ValidateIssuer,
                        ValidateAudience = jwtOptions.RequireAudience,
                        ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                        ValidateLifetime = jwtOptions.ValidateLifetime,
                        RequireSignedTokens = jwtOptions.RequireSignedTokens,
                        IssuerSigningKey = jwtHandler.GetIssuerSigningKey(),
                        TokenDecryptionKey = jwtHandler.GetIssuerSigningKey(),
                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issuer,
                        ClockSkew = TimeSpan.FromMinutes(jwtOptions.ClockSkew),
                        ValidIssuers = new List<string> {jwtOptions.Issuer}
                    };
                });
        }
    }
}
