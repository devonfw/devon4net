namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Login
{
    public class AuthenticateResponseDto
    {
        public string browserState { get; set; }
        public Returnuri returnUri { get; set; }
    }

    public class Returnuri
    {
        public string return_uri { get; set; }
    }
}
