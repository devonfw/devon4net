namespace Devon4Net.Infrastructure.CyberArk.Dto.User
{
    public class UpdateUserRequestDto
    {
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ChangePasswordOnTheNextLogon { get; set; }
        public string ExpiryDate { get; set; }
        public string UserTypeName { get; set; }
        public bool Disabled { get; set; }
        public string Location { get; set; }
    }
}
