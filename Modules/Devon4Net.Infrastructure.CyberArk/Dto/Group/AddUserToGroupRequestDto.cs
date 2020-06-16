namespace Devon4Net.Infrastructure.CyberArk.Dto.Group
{
    public class AddUserToGroupRequestDto
    {
        public string memberId { get; set; }
        public string memberType { get; set; }
        public string domainName { get; set; }
    }
}
