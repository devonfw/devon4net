using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.JWT;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.JWT.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class JwtConfiguration
    {
        public static void SetupJwtConf(this IServiceCollection services, ref IConfiguration configuration)
        {
            var jwtOptions = services.GetTypedOptions<JwtOptions>(configuration, "JWT");

            if (jwtOptions == null) return;
            
            var jwtHandler = new JwtHandler(jwtOptions);
            services.AddSingleton<IJwtHandler>(jwtHandler);
            SetupJwtNetCore(services, jwtOptions, jwtHandler);
            
        }

        private static void SetupJwtNetCore(IServiceCollection services, JwtOptions jwtOptions, JwtHandler jwtHandler)
        {
            services.AddAuthentication(options => options.DefaultScheme = AuthConst.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                        ValidateLifetime = jwtOptions.ValidateLifetime,
                        RequireSignedTokens = true,
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
