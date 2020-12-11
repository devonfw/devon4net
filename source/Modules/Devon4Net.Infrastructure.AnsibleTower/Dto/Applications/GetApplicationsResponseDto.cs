using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Applications
{
    public class RelatedApplication
    {
        public string tokens { get; set; }
        public string activity_stream { get; set; }
    }

    public class SummaryFieldsApplication
    {
        public Organization organization { get; set; }
        public UserCapabilities user_capabilities { get; set; }
        public Tokens tokens { get; set; }
    }

    public class ResultApplication
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public RelatedApplication related { get; set; }
        public SummaryFieldsApplication summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string client_type { get; set; }
        public string redirect_uris { get; set; }
        public string authorization_grant_type { get; set; }
        public bool? skip_authorization { get; set; }
        public int? organization { get; set; }
    }
}
