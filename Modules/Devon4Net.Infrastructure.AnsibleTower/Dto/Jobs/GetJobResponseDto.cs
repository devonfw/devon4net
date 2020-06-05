using System;
using System.Collections.Generic;
using System.Text;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Jobs
{

    public class GetJobResponseDto
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedJob related { get; set; }
        public SummaryFieldsJob summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? unified_job_template { get; set; }
        public string launch_type { get; set; }
        public string status { get; set; }
        public bool failed { get; set; }
        public DateTime started { get; set; }
        public object finished { get; set; }
        public object canceled_on { get; set; }
        public float? elapsed { get; set; }
        public string job_explanation { get; set; }
        public string execution_node { get; set; }
        public string controller_node { get; set; }
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
        public int? job_template { get; set; }
        public object[] passwords_needed_to_start { get; set; }
        public bool allow_simultaneous { get; set; }
        public object artifacts { get; set; }
        public string scm_revision { get; set; }
        public int? instance_group { get; set; }
        public bool diff_mode { get; set; }
        public int? job_slice_number { get; set; }
        public int? job_slice_count { get; set; }
        public string webhook_service { get; set; }
        public object webhook_credential { get; set; }
        public string webhook_guid { get; set; }
    }

    public class RelatedJob
    {
        public string created_by { get; set; }
        public string labels { get; set; }
        public string inventory { get; set; }
        public string project { get; set; }
        public string organization { get; set; }
        public string extra_credentials { get; set; }
        public string credentials { get; set; }
        public string unified_job_template { get; set; }
        public string stdout { get; set; }
        public string job_events { get; set; }
        public string job_host_summaries { get; set; }
        public string activity_stream { get; set; }
        public string notifications { get; set; }
        public string create_schedule { get; set; }
        public string job_template { get; set; }
        public string cancel { get; set; }
        public string relaunch { get; set; }
    }

    public class SummaryFieldsJob
    {
        public Organization organization { get; set; }
        public InventorySubset inventory { get; set; }
        public ProjectSubset project { get; set; }
        public JobTemplateSubset job_template { get; set; }
        public Unified_Job_Template unified_job_template { get; set; }
        public InstanceGroup instance_group { get; set; }
        public Created_By created_by { get; set; }
        public UserCapabilities user_capabilities { get; set; }
        public Labels labels { get; set; }
        public List<CredentialSubset> credentials { get; set; }
    }

    public class InventorySubset
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

    public class ProjectSubset
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string scm_type { get; set; }
    }

    public class JobTemplateSubset
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Unified_Job_Template
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string unified_job_type { get; set; }
    }

    public class InstanceGroup
    {
        public int? id { get; set; }
        public string name { get; set; }
        public bool is_containerized { get; set; }
    }
}
