namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Login
{

    public class AuthenticateRequestDto
    {
        public string tenantName { get; set; }
        public Passwordcredentials passwordCredentials { get; set; }
    }
}
