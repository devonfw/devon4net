namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials
{
    public class CreateCredentialRequestDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public int? organization { get; set; }
        public int? credential_type { get; set; }
        public IDictionary<string, string> inputs { get; set; }
        public int? user { get; set; }
    }
}
