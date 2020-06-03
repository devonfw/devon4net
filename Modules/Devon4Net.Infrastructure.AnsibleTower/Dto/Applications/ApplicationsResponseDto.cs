using System;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Applications
{
    public class ApplicationsResponseDto
    {
        public string redirect_uris { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public string url { get; set; }
        public Summary_Fields summary_fields { get; set; }
        public DateTime modified { get; set; }
        public RelatedApplicationsResponseDto related { get; set; }
        public string name { get; set; }
        public string client_type { get; set; }
        public string client_id { get; set; }
        public int organization { get; set; }
        public bool skip_authorization { get; set; }
        public string client_secret { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public string authorization_grant_type { get; set; }
    }

    public class Summary_Fields
    {
        public Tokens tokens { get; set; }
        public Organization organization { get; set; }
        public User_Capabilities user_capabilities { get; set; }
    }

    public class Tokens
    {
        public int count { get; set; }
        public object[] results { get; set; }
    }

    public class Organization
    {
        public string description { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class RelatedApplicationsResponseDto
    {
        public string tokens { get; set; }
        public string activity_stream { get; set; }
    }
}
