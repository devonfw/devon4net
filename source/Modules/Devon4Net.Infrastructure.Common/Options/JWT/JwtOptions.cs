namespace Devon4Net.Infrastructure.Common.Options.JWT
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int TokenExpirationTime { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
        public int ClockSkew { get; set; }
        public Security Security { get; set; }
        public bool RequireSignedTokens { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool RequireAudience { get; set; }
    }
}