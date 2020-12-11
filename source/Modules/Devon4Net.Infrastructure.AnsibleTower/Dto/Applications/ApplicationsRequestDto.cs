namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Applications
{
    public class ApplicationsRequestDto
    {
        public int? organization { get; set; }
        public string client_type { get; set; }
        public string name { get; set; }
        public string authorization_grant_type { get; set; }
    }

}