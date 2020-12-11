namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request
{
    public class GetUsersByUserNameResponse
    {
        public GetUsersByUserNameResponseEntity[] entities { get; set; }
        public GetUsersByUserNameResponseMeta meta { get; set; }
    }

    public class GetUsersByUserNameResponseMeta
    {
        public string completion_status { get; set; }
        public int total_count { get; set; }
        public object[] errorDetailsList { get; set; }
        public object[] errorDetailsMetaList { get; set; }
        public long query_time { get; set; }
    }

    public class GetUsersByUserNameResponseEntity
    {
        public string entity_type { get; set; }
        public GetUsersByUserNameResponseProperties properties { get; set; }
        public GetUsersByUserNameResponseRelatedProperties related_properties { get; set; }
    }

    public class GetUsersByUserNameResponseProperties
    {
        public string Upn { get; set; }
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetUsersByUserNameResponseRelatedProperties
    {
    }

}
