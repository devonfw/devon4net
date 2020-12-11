namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Login
{
    public class UserLoginRequestDto
    {
        public string tenantName { get; set; }
        public string token { get; set; }
        public Passwordcredentials passwordCredentials { get; set; }
    }
}
