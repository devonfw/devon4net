using System;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto
{

    public class LoginRequestDto
    {
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public Related related { get; set; }
        public Summary_Fields summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string description { get; set; }
        public int user { get; set; }
        public string token { get; set; }
        public object refresh_token { get; set; }
        public object application { get; set; }
        public DateTime expires { get; set; }
        public string scope { get; set; }
    }

    public class Related
    {
        public string user { get; set; }
        public string activity_stream { get; set; }
    }

    public class Summary_Fields
    {
        public User user { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
