using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Projects
{
    public class CreateProjectRequestDto
    {
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
        public int timeout { get; set; }
        public object organization { get; set; }
        public bool scm_update_on_launch { get; set; }
        public int scm_update_cache_timeout { get; set; }
        public bool allow_override { get; set; }
        public object custom_virtualenv { get; set; }
    }
}
