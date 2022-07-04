using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Cognito
{
    public class AwsCdkCognitoHandler : AwsCdkBaseHandler, IAwsCdkCognitoHandler
    {
        public AwsCdkCognitoHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public (IUserPool userPool, List<UserPoolClient> userPoolClientsIds) CreateUserPool(string identification, string name, List<Options.Resources.UserPoolResourceServer> userPoolResourceServers, List<Options.Resources.UserPoolClient> userPoolClientOptions)
        {
            var userPool = new UserPool(Scope, identification, new UserPoolProps
            {
                UserPoolName = name,
            });

            var resourceServersScopes = AddResourceServers(ref userPool, userPoolResourceServers);
            var userPoolClients = AddUserPoolClients(ref userPool, userPoolClientOptions, resourceServersScopes);

            return (userPool, userPoolClients);
        }

        private List<UserPoolClient> AddUserPoolClients(ref UserPool userPool, List<Options.Resources.UserPoolClient> userPoolClientOptions, Dictionary<(string ResourceServerId, UserPoolResourceServer ResourceServer), List<ResourceServerScope>> resourceServersScopes)
        {
            var userPoolClients = new List<UserPoolClient>();

            if (userPoolClientOptions == null || !userPoolClientOptions.Any()) return userPoolClients;

            foreach (var userPoolClientOption in userPoolClientOptions)
            {
                var userPoolClientAwsOptions = new UserPoolClientOptions();

                SetTokensValidityDuration(userPoolClientOption, ref userPoolClientAwsOptions);
                SetUserPoolClientAuth(userPoolClientOption, ref userPoolClientAwsOptions, resourceServersScopes);

                userPoolClientAwsOptions.DisableOAuth = userPoolClientOption.DisableOAuth;
                userPoolClientAwsOptions.EnableTokenRevocation = userPoolClientOption.EnableTokenRevocation;
                userPoolClientAwsOptions.GenerateSecret = userPoolClientOption.GenerateSecret;
                userPoolClientAwsOptions.UserPoolClientName = userPoolClientOption.UserPoolClientName;
                userPoolClientAwsOptions.SupportedIdentityProviders = GetUserPoolClientIdentityProviders(userPoolClientOption.SupportedIdentityProviders);

                var userPoolClient = userPool.AddClient(userPoolClientOption.Id, userPoolClientAwsOptions);
                userPoolClients.Add(userPoolClient);
            }

            return userPoolClients;
        }

        private Dictionary<(string ResourceServerId, UserPoolResourceServer ResourceServer), List<ResourceServerScope>> AddResourceServers(ref UserPool userPool, List<Options.Resources.UserPoolResourceServer> userPoolResourceServers)
        {
            var resourceServers = new Dictionary<(string, UserPoolResourceServer), List<ResourceServerScope>>();

            if (userPoolResourceServers == null || !userPoolResourceServers.Any()) return resourceServers;

            foreach (var resourceServer in userPoolResourceServers)
            {
                var resourceServerScopes = resourceServer.Scopes.Select(scope => new ResourceServerScope(new ResourceServerScopeProps { ScopeName = scope.ScopeName, ScopeDescription = scope.ScopeDescription })).ToArray();

                UserPoolResourceServer awsResourceServer = userPool.AddResourceServer(resourceServer.Id, new UserPoolResourceServerOptions
                {
                    Identifier = resourceServer.Identifier,
                    Scopes = resourceServerScopes,
                });

                resourceServers.Add((resourceServer.Id, awsResourceServer), resourceServerScopes.ToList());
            }

            return resourceServers;
        }

        private void SetUserPoolClientAuth(Options.Resources.UserPoolClient userPoolClientOption, ref UserPoolClientOptions userPoolClientAwsOptions, Dictionary<(string ResourceServerId, UserPoolResourceServer ResourceServer), List<ResourceServerScope>> resourceServersScopes)
        {
            if (userPoolClientOption.AuthFlows != null)
            {
                userPoolClientAwsOptions.AuthFlows = new AuthFlow
                {
                    AdminUserPassword = userPoolClientOption.AuthFlows.AdminUserPassword,
                    Custom = userPoolClientOption.AuthFlows.Custom,
                    UserPassword = userPoolClientOption.AuthFlows.UserPassword,
                    UserSrp = userPoolClientOption.AuthFlows.UserSrp,
                };
            }

            if (userPoolClientOption.OAuthSettings != null)
            {
                userPoolClientAwsOptions.OAuth = new OAuthSettings
                {
                    CallbackUrls = userPoolClientOption.OAuthSettings.CallBackUrls?.ToArray(),
                    LogoutUrls = userPoolClientOption.OAuthSettings.LogoutUrls?.ToArray(),
                    Flows = new OAuthFlows
                    {
                        AuthorizationCodeGrant = userPoolClientOption.OAuthSettings.Flows.AuthorizationCodeGrant,
                        ClientCredentials = userPoolClientOption.OAuthSettings.Flows.ClientCredentials,
                        ImplicitCodeGrant = userPoolClientOption.OAuthSettings.Flows.ImplicitCodeGrant,
                    },
                    Scopes = GetOAuthScopes(userPoolClientOption.OAuthSettings.OAuthScopes, resourceServersScopes, userPoolClientOption.ResourceServersIds),
                };
            }
        }

        private void SetTokensValidityDuration(Options.Resources.UserPoolClient userPoolClientOption, ref UserPoolClientOptions userPoolClientAwsOptions)
        {
            if (userPoolClientOption.AccessTokenValidityMinutes != null)
            {
                userPoolClientAwsOptions.AccessTokenValidity = Duration.Minutes((double)userPoolClientOption.AccessTokenValidityMinutes);
            }

            if (userPoolClientOption.RefreshTokenValidityDays != null)
            {
                userPoolClientAwsOptions.RefreshTokenValidity = Duration.Days((double)userPoolClientOption.RefreshTokenValidityDays);
            }

            if (userPoolClientOption.IdTokenValidityMinutes != null)
            {
                userPoolClientAwsOptions.IdTokenValidity = Duration.Minutes((double)userPoolClientOption.IdTokenValidityMinutes);
            }
        }

        public IUserPool LocateFromId(string identification, string userPoolId)
        {
            return UserPool.FromUserPoolId(Scope, identification, userPoolId);
        }

        public IUserPool LocateFromArn(string identification, string arn)
        {
            return UserPool.FromUserPoolArn(Scope, identification, arn);
        }

        private OAuthScope[] GetOAuthScopes(List<string> scopeStrings, Dictionary<(string ResourceServerId, UserPoolResourceServer ResourceServer), List<ResourceServerScope>> resourceServersScopes, List<string> resourceServersIds)
        {

            var userPoolClientResourceServerScopes = GetUserPoolClientResourceServerScopes(resourceServersScopes, resourceServersIds);

            if (scopeStrings == null) return userPoolClientResourceServerScopes.ToArray();

            foreach (var scopeString in scopeStrings)
            {
                switch (scopeString)
                {
                    case CognitoConsts.CognitoAdminAuthScopeName:
                        userPoolClientResourceServerScopes.Add(OAuthScope.COGNITO_ADMIN);
                        break;
                    case CognitoConsts.EmailAuthScopeName:
                        userPoolClientResourceServerScopes.Add(OAuthScope.EMAIL);
                        break;
                    case CognitoConsts.OpenIdAuthScopeName:
                        userPoolClientResourceServerScopes.Add(OAuthScope.OPENID);
                        break;
                    case CognitoConsts.PhoneAuthScopeName:
                        userPoolClientResourceServerScopes.Add(OAuthScope.PHONE);
                        break;
                    case CognitoConsts.ProfileAuthScopeName:
                        userPoolClientResourceServerScopes.Add(OAuthScope.PROFILE);
                        break;
                    default:
                        throw new ArgumentException($"Invalid Cognito UserPoolClient OAuthScope value: {scopeString}");
                }
            }

            return userPoolClientResourceServerScopes.ToArray();
        }

        private List<OAuthScope> GetUserPoolClientResourceServerScopes(Dictionary<(string ResourceServerId, UserPoolResourceServer ResourceServer), List<ResourceServerScope>> resourceServersScopes, List<string> resourceServersIds)
        {
            var scopes = new List<OAuthScope>();

            if (resourceServersIds == null || !resourceServersIds.Any()) return scopes;

            foreach (var resourceServerId in resourceServersIds)
            {
                var resourceServer = resourceServersScopes.FirstOrDefault(x => x.Key.ResourceServerId == resourceServerId);

                foreach (var resourceServerScope in resourceServer.Value)
                {
                    scopes.Add(OAuthScope.ResourceServer(resourceServer.Key.ResourceServer, resourceServerScope));
                }
            }

            return scopes;
        }

        private UserPoolClientIdentityProvider[] GetUserPoolClientIdentityProviders(List<string> identityProviderStrings)
        {
            List<UserPoolClientIdentityProvider> userPoolClientIdentityProviders = new List<UserPoolClientIdentityProvider>();

            if (identityProviderStrings == null || !identityProviderStrings.Any()) return null;

            foreach (var identityProviderString in identityProviderStrings)
            {
                switch (identityProviderString)
                {
                    case CognitoConsts.AmazonIdentityProviderName:
                        userPoolClientIdentityProviders.Add(UserPoolClientIdentityProvider.AMAZON);
                        break;
                    case CognitoConsts.AppleIdentityProviderName:
                        userPoolClientIdentityProviders.Add(UserPoolClientIdentityProvider.APPLE);
                        break;
                    case CognitoConsts.CognitoIdentityProviderName:
                        userPoolClientIdentityProviders.Add(UserPoolClientIdentityProvider.COGNITO);
                        break;
                    case CognitoConsts.FacebookIdentityProviderName:
                        userPoolClientIdentityProviders.Add(UserPoolClientIdentityProvider.FACEBOOK);
                        break;
                    case CognitoConsts.GoogleIdentityProviderName:
                        userPoolClientIdentityProviders.Add(UserPoolClientIdentityProvider.GOOGLE);
                        break;
                    default:
                        throw new ArgumentException($"Invalid Cognito UserPoolClientIdentityProvider value: {identityProviderString}");
                }
            }

            return userPoolClientIdentityProviders.ToArray();
        }
    }
}
