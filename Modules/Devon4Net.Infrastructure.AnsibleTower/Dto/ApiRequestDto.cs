using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto
{
    public class ApiRequestDto
    {
        public string ping { get; set; }
        public string instances { get; set; }
        public string instance_groups { get; set; }
        public string config { get; set; }
        public string settings { get; set; }
        public string me { get; set; }
        public string dashboard { get; set; }
        public string organizations { get; set; }
        public string users { get; set; }
        public string projects { get; set; }
        public string project_updates { get; set; }
        public string teams { get; set; }
        public string credentials { get; set; }
        public string credential_types { get; set; }
        public string applications { get; set; }
        public string tokens { get; set; }
        public string inventory { get; set; }
        public string inventory_scripts { get; set; }
        public string inventory_sources { get; set; }
        public string inventory_updates { get; set; }
        public string groups { get; set; }
        public string hosts { get; set; }
        public string job_templates { get; set; }
        public string jobs { get; set; }
        public string job_events { get; set; }
        public string ad_hoc_commands { get; set; }
        public string system_job_templates { get; set; }
        public string system_jobs { get; set; }
        public string schedules { get; set; }
        public string roles { get; set; }
        public string notification_templates { get; set; }
        public string notifications { get; set; }
        public string labels { get; set; }
        public string unified_job_templates { get; set; }
        public string unified_jobs { get; set; }
        public string activity_stream { get; set; }
        public string workflow_job_templates { get; set; }
        public string workflow_jobs { get; set; }
        public string workflow_job_template_nodes { get; set; }
        public string workflow_job_nodes { get; set; }
    }
}
