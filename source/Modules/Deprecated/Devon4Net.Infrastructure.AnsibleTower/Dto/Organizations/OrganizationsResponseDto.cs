using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations
{
    public class ResultOrganizationDto
    {
        public string description { get; set; }
        public int? max_hosts { get; set; }
        public string url { get; set; }
        public OrganizationSummaryFields summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public Dictionary<string,string> related { get; set; }
        public object custom_virtualenv { get; set; }
        public string type { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class OrganizationSummaryFields
    {
        public UserCapabilities user_capabilities { get; set; }
        public Related_Field_Counts related_field_counts { get; set; }
        public Dictionary<string, ObjectRole> object_roles { get; set; }
    }

    public class Related_Field_Counts
    {
        public int? job_templates { get; set; }
        public int? users { get; set; }
        public int? teams { get; set; }
        public int? admins { get; set; }
        public int? inventories { get; set; }
        public int? projects { get; set; }
    }


    public class ObjectRole
    {
        public bool? user_only { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
    }
}

