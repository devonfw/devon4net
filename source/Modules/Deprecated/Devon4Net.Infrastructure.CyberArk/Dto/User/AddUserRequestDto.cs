namespace Devon4Net.Infrastructure.CyberArk.Dto.User
{
    public class AddUserRequestDto
    {
        public string UserName { get; set; }
        public string InitialPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ChangePasswordOnTheNextLogon { get; set; }
        public string safeName { get; set; }
    }
}
