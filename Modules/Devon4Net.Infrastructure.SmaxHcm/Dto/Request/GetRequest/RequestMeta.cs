using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest
{
    public class RequestMeta
    {
        public string completion_status { get; set; }
        public int total_count { get; set; }
        public List<object> errorDetailsList { get; set; }
        public List<object> errorDetailsMetaList { get; set; }
        public long query_time { get; set; }
    }
}