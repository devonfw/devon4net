namespace Devon4Net.Infrastructure.Common.Constants
{
    public static class AuthConst
    {
        /// <summary>
        /// Default value for AuthenticationScheme property in the JwtBearerAuthenticationOptions
        /// </summary>
        public const string AuthenticationScheme = "Bearer";
        public const string DevonSampleUserRole = "DevonSampleUserRole";
        public const string DevonSamplePolicy = "DevonSamplePolicy";
        public const string DefaultAlgorithm = "HmacSha512";
    }
}