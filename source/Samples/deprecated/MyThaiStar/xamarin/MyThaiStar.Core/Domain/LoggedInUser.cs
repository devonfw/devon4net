namespace MyThaiStar.Core.Domain
{
    public class LoggedInUser : User
    {
        public string AccessToken { get; set; }
        public string Role { get; set; }

        public long IdUser { get; set; }
    }
}