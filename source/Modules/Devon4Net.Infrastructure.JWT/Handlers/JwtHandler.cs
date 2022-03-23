using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Devon4Net.Infrastructure.Common.Common.IO;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.Common.Options.JWT;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Devon4Net.Infrastructure.JWT.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private SecurityKey IssuerSigningKey { get; set; }
        private SigningCredentials SigningCredentials { get; set; }
        private X509Certificate2 Certificate { get; set; }
        private static SecurityKey SecurityKey { get; set; }
        private JwtOptions JwtOptions { get; }

        public JwtHandler(JwtOptions jwtOptions)
        {
            JwtOptions = jwtOptions;

            if (JwtOptions != null)
            {
                SetupJwtSecurity();
            }
            else
            {
                throw new ArgumentException("Cannot create the JWT Handler. JWTOptions are null");
            }
        }

        public string CreateJwtToken(List<Claim> clientClaims)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = JwtOptions.Issuer,
                    Audience = JwtOptions.Audience,
                    Subject = new ClaimsIdentity(clientClaims), //NOSONAR false positive
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(JwtOptions.ClockSkew)),
                    IssuedAt = DateTime.UtcNow,
                    Claims = clientClaims.Where(c => c.Type != ClaimTypes.Role).ToDictionary(x => x.Type, x => x.Value as object),
                    SigningCredentials = SigningCredentials
                };
                return new JwtSecurityTokenHandler(). CreateEncodedJwt(tokenDescriptor);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public string CreateRefreshToken()
        {
            try
            {
                var rd = new Random();
                var ticks = DateTime.UtcNow.Ticks + rd.NextInt64();

                return Convert.ToBase64String(GetHashCodeFromString(ticks.ToString(), JwtOptions.Security.RefreshTokenEncryptionAlgorithm ?? SecurityAlgorithms.RsaSha512));
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public List<Claim> GetUserClaims(string jwtToken)
        {
            _ = ValidateToken(jwtToken,  out var claimsPrincipal, out _);
            return claimsPrincipal.Claims.ToList();
        }

        public string GetClaimValue(List<Claim> claimList, string claim)
        {
            if (claimList?.Any() != true) return string.Empty;
            return claimList.Find(x => x.Type == claim)?.Value;
        }

        public string GetClaimValue(string token, string claim)
        {
            if (string.IsNullOrEmpty(token)) return string.Empty;
            var claimList = GetUserClaims(token);
            return GetClaimValue(claimList, claim);
        }

        public SecurityKey GetIssuerSigningKey()
        {
            return IssuerSigningKey;
        }

        public bool ValidateToken(string jwtToken, out ClaimsPrincipal claimsPrincipal, out SecurityToken securityToken)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                claimsPrincipal = handler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidAudience = JwtOptions.Audience,
                    ValidIssuer = JwtOptions.Issuer,
                    RequireSignedTokens = JwtOptions.RequireSignedTokens,
                    RequireExpirationTime = JwtOptions.RequireExpirationTime,
                    RequireAudience = JwtOptions.RequireAudience,
                    TokenDecryptionKey = SecurityKey,
                    IssuerSigningKey = SecurityKey
                }, out securityToken);

                return true;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        #region private methods
        private void GetSigningCredentialsFromKey(string secretKey)
        {
            try
            {
                var userEncryptionAlgorithm = GetEncryptionAlgorithm(JwtOptions.Security.SecretKeyEncryptionAlgorithm);
                var key = new SymmetricSecurityKey(GetHashCodeFromString(secretKey, userEncryptionAlgorithm));
                SecurityKey = key;
                IssuerSigningKey = new SymmetricSecurityKey(key.Key);
                SigningCredentials = new SigningCredentials(key, userEncryptionAlgorithm);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private void GetSigningCredentialsFromCertificate(string certificate, string password)
        {
            try
            {
                var certificateEncryptionAlgorithm = GetEncryptionAlgorithm(JwtOptions.Security.CertificateEncryptionAlgorithm);

                Certificate = new X509Certificate2(File.ReadAllBytes(FileOperations.GetFileFullPath(certificate)), password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                SecurityKey = new X509SecurityKey(Certificate);
                IssuerSigningKey = new X509SecurityKey(Certificate);
                SigningCredentials = new SigningCredentials(IssuerSigningKey, certificateEncryptionAlgorithm);
            }
            catch (CryptographicException ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private static byte[] GetHashCodeFromString(string key, string algorithm)
        {
            var hash = HashAlgorithm.Create(GetHashAlgorithm(algorithm));
            return hash.ComputeHash(Encoding.Default.GetBytes(key));
        }

        private void SetupJwtSecurity()
        {
            if (JwtOptions?.Security == null) return;

            if (!string.IsNullOrEmpty(JwtOptions.Security.SecretKey))
            {
                GetSigningCredentialsFromKey(JwtOptions.Security.SecretKey);
            }
            else
            {
                if (!string.IsNullOrEmpty(JwtOptions.Security.Certificate) &&
                    !string.IsNullOrEmpty(JwtOptions.Security.CertificatePassword))
                {
                    GetSigningCredentialsFromCertificate(JwtOptions.Security.Certificate,
                        JwtOptions.Security.CertificatePassword);
                }
            }
        }

        private static string GetEncryptionAlgorithm(string encryptionAlgorithm)
        {
            try
            {
                var securityAlgorithm = StaticConstsHelper.GetValue(typeof(SecurityAlgorithms), encryptionAlgorithm);
                return securityAlgorithm ?? SecurityAlgorithms.RsaSha512;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private static string GetHashAlgorithm(string encryptionAlgorithm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptionAlgorithm))
                {
                    return HashAlgorithmConst.SHA512;
                }

                if (encryptionAlgorithm.Contains(HashSizeConst.S512))
                {
                    return HashAlgorithmConst.SHA512;
                }

                if (encryptionAlgorithm.Contains(HashSizeConst.S384))
                {
                    return HashAlgorithmConst.SHA384;
                }

                if (encryptionAlgorithm.Contains(HashSizeConst.S256))
                {
                    return HashAlgorithmConst.SHA256;
                }

                if (encryptionAlgorithm.Contains(HashSizeConst.SMD5, StringComparison.OrdinalIgnoreCase))
                {
                    return HashAlgorithmConst.MD5;
                }

                if (encryptionAlgorithm.Contains(HashSizeConst.SSHA, StringComparison.OrdinalIgnoreCase))
                {
                    return HashAlgorithmConst.SHA;
                }

                return HashAlgorithmConst.SHA512;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
        #endregion
    }
}