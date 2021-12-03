using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories
{
    public class ResultInventoryDto
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedInventories related { get; set; }
        public SummaryFieldsInventories summary_fields { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? organization { get; set; }
        public string kind { get; set; }
        public object host_filter { get; set; }
        public string variables { get; set; }
        public bool has_active_failures { get; set; }
        public int? total_hosts { get; set; }
        public int? hosts_with_active_failures { get; set; }
        public int? total_groups { get; set; }
        public bool has_inventory_sources { get; set; }
        public int? total_inventory_sources { get; set; }
        public int? inventory_sources_with_failures { get; set; }
        public object insights_credential { get; set; }
        public bool pending_deletion { get; set; }
    }

    public class RelatedInventories
    {
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string hosts { get; set; }
        public string groups { get; set; }
        public string root_groups { get; set; }
        public string variable_data { get; set; }
        public string script { get; set; }
        public string tree { get; set; }
        public string inventory_sources { get; set; }
        public string update_inventory_sources { get; set; }
        public string activity_stream { get; set; }
        public string job_templates { get; set; }
        public string ad_hoc_commands { get; set; }
        public string access_list { get; set; }
        public string object_roles { get; set; }
        public string instance_groups { get; set; }
        public string copy { get; set; }
        public string organization { get; set; }
    }

    public class SummaryFieldsInventories
    {
        public Organization organization { get; set; }
        public Created_By created_by { get; set; }
        public Modified_By modified_by { get; set; }
        public Dictionary<string, RoleItems> object_roles { get; set; }
        public UserCapabilities user_capabilities { get; set; }
    }
}
