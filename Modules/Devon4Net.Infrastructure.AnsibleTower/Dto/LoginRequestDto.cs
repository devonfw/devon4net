using System;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto
{
        public class LoginRequestDto
        {
            public long Id { get; set; }

            public string Type { get; set; }

            public string Url { get; set; }

            public Related Related { get; set; }

            public SummaryFields Summary_Fields { get; set; }

            public DateTime Created { get; set; }

            public DateTime Modified { get; set; }

            public string Description { get; set; }

            public long User { get; set; }

            public string Token { get; set; }

            public string Refresh_token { get; set; }

            public string Application { get; set; }

            public DateTime Expires { get; set; }

            public string Scope { get; set; }
        }

        public class Related
        {
            public string User { get; set; }

            public string Activity_stream { get; set; }
        }

        public class SummaryFields
        {
            public User User { get; set; }
        }

        public class User
        {
            public long Id { get; set; }

            public string Username { get; set; }

            public string First_name { get; set; }

            public string Last_name { get; set; }
        }
}
