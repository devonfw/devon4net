namespace Devon4Net.Infrastructure.CyberArk.Dto.Group
{
    public class GroupDetail
    {
        public int id { get; set; }
        public string groupName { get; set; }
        public string groupType { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string directory { get; set; }
        public string dn { get; set; }
    }
}