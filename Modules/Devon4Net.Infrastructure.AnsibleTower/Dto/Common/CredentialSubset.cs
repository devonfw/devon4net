namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Common
{
    public class CredentialSubset
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string kind { get; set; }
        public bool cloud { get; set; }
    }
}