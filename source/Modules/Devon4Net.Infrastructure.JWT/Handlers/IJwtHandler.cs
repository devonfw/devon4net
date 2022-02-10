using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Devon4Net.Infrastructure.JWT.Handlers
{
    public interface IJwtHandler
    {
        string CreateClientToken(List<Claim> clientClaims);
        List<Claim> GetUserClaims(string jwtToken);
        string GetClaimValue(List<Claim> claimList, string claim);
        string GetClaimValue(string token, string claim);
        SecurityKey GetIssuerSigningKey();
    }
}