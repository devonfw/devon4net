using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;
using System;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials
{

    public class GetCredentialsResponseDto
    {
        public Dictionary<string,string> inputs { get; set; }
        public object kind { get; set; }
        public string description { get; set; }
        public bool? kubernetes { get; set; }
        public DateTime created { get; set; }
        public string url { get; set; }
        public SummaryFieldsCredential summary_fields { get; set; }
        public DateTime modified { get; set; }
        public RelatedCredential related { get; set; }
        public int? credential_type { get; set; }
        public int? organization { get; set; }
        public string type { get; set; }
        public int? id { get; set; }
        public bool? cloud { get; set; }
        public string name { get; set; }
    }

    public class SummaryFieldsCredential
    {
        public UserCapabilities user_capabilities { get; set; }
        public Credential_Type credential_type { get; set; }
        public Owner[] owners { get; set; }
        public Organization organization { get; set; }
        public Dictionary<string, RoleItems> object_roles { get; set; }
    }

    public class Credential_Type
    {
        public string description { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
    }

    public class Owner
    {
        public string url { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
    }

    public class RelatedCredential
    {
        public string input_sources { get; set; }
        public string object_roles { get; set; }
        public string access_list { get; set; }
        public string credential_type { get; set; }
        public string owner_users { get; set; }
        public string owner_teams { get; set; }
        public string organization { get; set; }
        public string copy { get; set; }
        public string activity_stream { get; set; }
    }
}
