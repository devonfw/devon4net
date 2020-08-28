namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request
{
    public class GetUsersByUserNameResponse
    {
        public GetUsersByUserNameResponse_Entity[] entities { get; set; }
        public GetUsersByUserNameResponse_Meta meta { get; set; }
    }

    public class GetUsersByUserNameResponse_Meta
    {
        public string completion_status { get; set; }
        public int total_count { get; set; }
        public object[] errorDetailsList { get; set; }
        public object[] errorDetailsMetaList { get; set; }
        public long query_time { get; set; }
    }

    public class GetUsersByUserNameResponse_Entity
    {
        public string entity_type { get; set; }
        public GetUsersByUserNameResponse_Properties properties { get; set; }
        public GetUsersByUserNameResponse_Related_Properties related_properties { get; set; }
    }

    public class GetUsersByUserNameResponse_Properties
    {
        public string Upn { get; set; }
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetUsersByUserNameResponse_Related_Properties
    {
    }

}
