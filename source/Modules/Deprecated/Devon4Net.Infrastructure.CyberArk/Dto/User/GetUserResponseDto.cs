namespace Devon4Net.Infrastructure.CyberArk.Dto.User
{
    public class GetUserResponseDto
    {
        public bool AgentUser { get; set; }
        public bool Disabled { get; set; }
        public string Email { get; set; }
        public bool Expired { get; set; }
        public object ExpiryDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Source { get; set; }
        public bool Suspended { get; set; }
        public string UserName { get; set; }
        public string UserTypeName { get; set; }
    }
}
