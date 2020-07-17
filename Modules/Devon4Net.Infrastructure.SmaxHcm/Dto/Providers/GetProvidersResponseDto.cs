using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Common;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Providers
{

    public class GetProvidersResponseDto
    {
        public float total_results { get; set; }
        public string self { get; set; }
        public string type { get; set; }
        public List<ProviderMember> members { get; set; }
    }

    public class ProviderMember
    {
        public string self { get; set; }
        //public string type { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public bool enabled { get; set; }
        public ProviderType type { get; set; }
        public Access_Point access_point { get; set; }
        public Ext1 ext { get; set; }
    }

    public class ProviderType
    {
        public string self { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public Ext ext { get; set; }
    }

    public class Access_Point
    {
        public string uri { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }

    public class Ext1
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
    }
}
