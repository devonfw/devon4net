using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Consts
{
    public static class CognitoConsts
    {
        // Auth Scopes.
        public const string CognitoAdminAuthScopeName = "COGNITO_ADMIN";
        public const string EmailAuthScopeName = "EMAIL";
        public const string OpenIdAuthScopeName = "OPENID";
        public const string PhoneAuthScopeName = "PHONE";
        public const string ProfileAuthScopeName = "PROFILE";

        // Identity Providers.
        public const string AmazonIdentityProviderName = "AMAZON";
        public const string AppleIdentityProviderName = "APPLE";
        public const string CognitoIdentityProviderName = "COGNITO";
        public const string FacebookIdentityProviderName = "FACEBOOK";
        public const string GoogleIdentityProviderName = "GOOGLE";
    }
}
