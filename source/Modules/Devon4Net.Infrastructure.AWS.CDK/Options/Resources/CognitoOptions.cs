using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class CognitoOptions
    {
        public List<UserPool> UserPools { get; set; }
        public string ParameterStoreName { get; set; }
    }

    public class UserPool
    {
        public string Id { get; set; }
        public string UserPoolName { get; set; }
        public List<UserPoolClient> UserPoolClients { get; set; }
        public List<UserPoolResourceServer> UserPoolResourceServers { get; set; }
    }

    public class UserPoolResourceServer
    {
        public string Id { get; set; } // CDK ID
        public string Identifier { get; set; } // Inside Cognito ID
        public string Name { get; set; }
        public List<ResourceServerOAuthScope> Scopes { get; set; }
    }

    public class UserPoolClient
    {
        public string Id { get; set; }
        public double? AccessTokenValidityMinutes { get; set; }
        public AuthFlows AuthFlows { get; set; }
        public bool? DisableOAuth { get; set; }
        public bool? EnableTokenRevocation { get; set; }
        public bool? GenerateSecret { get; set; }
        public double? IdTokenValidityMinutes { get; set; }
        public OAuthSettings OAuthSettings { get; set; }
        public double? RefreshTokenValidityDays { get; set; }
        public List<string> SupportedIdentityProviders { get; set; }
        public string UserPoolClientName { get; set; }
        public List<string> ResourceServersIds { get; set; }
    }

    public class AuthFlows
    {
        public bool? AdminUserPassword { get; set; }
        public bool? Custom { get; set; }
        public bool? UserPassword { get; set; }
        public bool? UserSrp { get; set; }
    }

    public class OAuthSettings
    {
        public List<string> CallBackUrls { get; set; }
        public OAuthFlows Flows { get; set; }
        public List<string> LogoutUrls { get; set; }
        public List<string> OAuthScopes { get; set; }
    }

    public class OAuthFlows
    {
        public bool? AuthorizationCodeGrant { get; set; }
        public bool? ClientCredentials { get; set; }
        public bool? ImplicitCodeGrant { get; set; }
    }

    public class ResourceServerOAuthScope
    {
        public string ScopeName { get; set; }
        public string ScopeDescription { get; set; }
    }
}
