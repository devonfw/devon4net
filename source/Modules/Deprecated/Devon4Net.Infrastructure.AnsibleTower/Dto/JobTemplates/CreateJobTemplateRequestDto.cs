namespace Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates
{
    public class CreateJobTemplateRequestDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string job_type { get; set; }
        public int? inventory { get; set; }
        public int? project { get; set; }
        public string playbook { get; set; }
        public string limit { get; set; }
        public int? verbosity { get; set; }
        public string job_tags { get; set; }
        public string skip_tags { get; set; }
        public object custom_virtualenv { get; set; }
        public List<InstanceGroupsCreateJob> instance_groups { get; set; }
        public int? job_slice_count { get; set; }
        public int? timeout { get; set; }
        public bool diff_mode { get; set; }
        public bool become_enabled { get; set; }
        public bool allow_callbacks { get; set; }
        public bool enable_webhook { get; set; }
        public bool allow_simultaneous { get; set; }
        public bool use_fact_cache { get; set; }
        public string host_config_key { get; set; }
        public string webhook_service { get; set; }
        public object webhook_credential { get; set; }
        public int? forks { get; set; }
        public bool ask_diff_mode_on_launch { get; set; }
        public bool ask_scm_branch_on_launch { get; set; }
        public bool ask_tags_on_launch { get; set; }
        public bool ask_skip_tags_on_launch { get; set; }
        public bool ask_limit_on_launch { get; set; }
        public bool ask_job_type_on_launch { get; set; }
        public bool ask_verbosity_on_launch { get; set; }
        public bool ask_inventory_on_launch { get; set; }
        public bool ask_variables_on_launch { get; set; }
        public bool ask_credential_on_launch { get; set; }
        public string extra_vars { get; set; }
        public bool survey_enabled { get; set; }
    }

    public class InstanceGroupsCreateJob
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedCreateJob related { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public int? capacity { get; set; }
        public int? committed_capacity { get; set; }
        public int? consumed_capacity { get; set; }
        public int? percent_capacity_remaining { get; set; }
        public int? jobs_running { get; set; }
        public int? jobs_total { get; set; }
        public int? instances { get; set; }
        public object controller { get; set; }
        public bool is_controller { get; set; }
        public bool is_isolated { get; set; }
        public bool is_containerized { get; set; }
        public object credential { get; set; }
        public int? policy_instance_percentage { get; set; }
        public int? policy_instance_minimum { get; set; }
        public List<object> policy_instance_list { get; set; }
        public string pod_spec_override { get; set; }
        public object summary_fields { get; set; }
        public bool isSelected { get; set; }
    }

    public class RelatedCreateJob
    {
        public string jobs { get; set; }
        public string instances { get; set; }
    }
}
