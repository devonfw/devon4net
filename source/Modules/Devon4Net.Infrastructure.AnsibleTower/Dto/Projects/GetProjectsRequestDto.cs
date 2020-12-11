using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Projects
{
    public class GetProjectsRequestDto
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedProjects related { get; set; }
        public SummaryFieldsProjects summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string local_path { get; set; }
        public string scm_type { get; set; }
        public string scm_url { get; set; }
        public string scm_branch { get; set; }
        public string scm_refspec { get; set; }
        public bool scm_clean { get; set; }
        public bool scm_delete_on_update { get; set; }
        public object credential { get; set; }
        public int? timeout { get; set; }
        public string scm_revision { get; set; }
        public object last_job_run { get; set; }
        public bool last_job_failed { get; set; }
        public object next_job_run { get; set; }
        public string status { get; set; }
        public int? organization { get; set; }
        public bool scm_update_on_launch { get; set; }
        public int? scm_update_cache_timeout { get; set; }
        public bool allow_override { get; set; }
        public object custom_virtualenv { get; set; }
        public bool last_update_failed { get; set; }
        public object last_updated { get; set; }
    }

    public class RelatedProjects
    {
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string teams { get; set; }
        public string playbooks { get; set; }
        public string inventory_files { get; set; }
        public string update { get; set; }
        public string project_updates { get; set; }
        public string scm_inventory_sources { get; set; }
        public string schedules { get; set; }
        public string activity_stream { get; set; }
        public string notification_templates_started { get; set; }
        public string notification_templates_success { get; set; }
        public string notification_templates_error { get; set; }
        public string access_list { get; set; }
        public string object_roles { get; set; }
        public string copy { get; set; }
        public string organization { get; set; }
    }

    public class SummaryFieldsProjects
    {
        public Organization organization { get; set; }
        public Created_By created_by { get; set; }
        public Modified_By modified_by { get; set; }
        public Dictionary<string,RoleItems> object_roles { get; set; }
        public UserCapabilities user_capabilities { get; set; }
    }
}
