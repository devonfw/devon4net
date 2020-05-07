using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Devon4Net.Infrastructure.Common.Options.JWT;
using Devon4Net.Infrastructure.JWT.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Devon4Net.Infrastructure.JWT.Common.Const;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class JwtConfiguration
    {
        public static void SetupJwt(this IServiceCollection services, JwtOptions jwtOptions)
        {
            if (jwtOptions == null) return;
            
            var jwtHandler = new JwtHandler(jwtOptions);
            services.AddSingleton<IJwtHandler>(jwtHandler);
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
                    ValidIssuers = new List<string> { jwtOptions.Issuer }
                };
            });
        }
    }
}
