using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates
{
    public class GetJobTemplatesResponseDto
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedTemplate related { get; set; }
        public SummaryFieldsJobTemplate summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string job_type { get; set; }
        public int? inventory { get; set; }
        public int? project { get; set; }
        public string playbook { get; set; }
        public string scm_branch { get; set; }
        public int? forks { get; set; }
        public string limit { get; set; }
        public int? verbosity { get; set; }
        public string extra_vars { get; set; }
        public string job_tags { get; set; }
        public bool force_handlers { get; set; }
        public string skip_tags { get; set; }
        public string start_at_task { get; set; }
        public int? timeout { get; set; }
        public bool use_fact_cache { get; set; }
        public int? organization { get; set; }
        public object last_job_run { get; set; }
        public bool last_job_failed { get; set; }
        public object next_job_run { get; set; }
        public string status { get; set; }
        public string host_config_key { get; set; }
        public bool ask_scm_branch_on_launch { get; set; }
        public bool ask_diff_mode_on_launch { get; set; }
        public bool ask_variables_on_launch { get; set; }
        public bool ask_limit_on_launch { get; set; }
        public bool ask_tags_on_launch { get; set; }
        public bool ask_skip_tags_on_launch { get; set; }
        public bool ask_job_type_on_launch { get; set; }
        public bool ask_verbosity_on_launch { get; set; }
        public bool ask_inventory_on_launch { get; set; }
        public bool ask_credential_on_launch { get; set; }
        public bool survey_enabled { get; set; }
        public bool become_enabled { get; set; }
        public bool diff_mode { get; set; }
        public bool allow_simultaneous { get; set; }
        public object custom_virtualenv { get; set; }
        public int? job_slice_count { get; set; }
        public string webhook_service { get; set; }
        public object webhook_credential { get; set; }
    }

    public class RelatedTemplate
    {
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string labels { get; set; }
        public string inventory { get; set; }
        public string project { get; set; }
        public string organization { get; set; }
        public string extra_credentials { get; set; }
        public string credentials { get; set; }
        public string jobs { get; set; }
        public string schedules { get; set; }
        public string activity_stream { get; set; }
        public string launch { get; set; }
        public string webhook_key { get; set; }
        public string webhook_receiver { get; set; }
        public string notification_templates_started { get; set; }
        public string notification_templates_success { get; set; }
        public string notification_templates_error { get; set; }
        public string access_list { get; set; }
        public string survey_spec { get; set; }
        public string object_roles { get; set; }
        public string instance_groups { get; set; }
        public string slice_workflow_jobs { get; set; }
        public string copy { get; set; }
    }

    public class SummaryFieldsJobTemplate
    {
        public Organization organization { get; set; }
        public Inventory inventory { get; set; }
        public Project project { get; set; }
        public Created_By created_by { get; set; }
        public Modified_By modified_by { get; set; }
        public Dictionary<string,RoleItems> object_roles { get; set; }
        public UserCapabilitiesJobTemplate user_capabilities { get; set; }
        public Labels labels { get; set; }
        public object[] recent_jobs { get; set; }
        public Credential[] credentials { get; set; }
    }

    public class Inventory
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool has_active_failures { get; set; }
        public int? total_hosts { get; set; }
        public int? hosts_with_active_failures { get; set; }
        public int? total_groups { get; set; }
        public bool has_inventory_sources { get; set; }
        public int? total_inventory_sources { get; set; }
        public int? inventory_sources_with_failures { get; set; }
        public int? organization_id { get; set; }
        public string kind { get; set; }
    }

    public class Project
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string scm_type { get; set; }
    }


    public class UserCapabilitiesJobTemplate
    {
        public bool edit { get; set; }
        public bool delete { get; set; }
        public bool start { get; set; }
        public bool schedule { get; set; }
        public bool copy { get; set; }
    }

    public class Labels
    {
        public int? count { get; set; }
        public object[] results { get; set; }
    }

    public class Credential
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string kind { get; set; }
        public bool cloud { get; set; }
    }

}
