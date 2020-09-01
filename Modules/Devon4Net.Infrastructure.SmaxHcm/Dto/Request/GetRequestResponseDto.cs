namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request
{
    public class GetRequestResponseDto
    {
        public GetRequestResponseDto_Entity[] entities { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string completion_status { get; set; }
        public int total_count { get; set; }
        public object[] errorDetailsList { get; set; }
        public object[] errorDetailsMetaList { get; set; }
        public long query_time { get; set; }
    }

    public class GetRequestResponseDto_Entity
    {
        public string entity_type { get; set; }
        public GetRequestResponseDto_Properties properties { get; set; }
        public Related_Properties related_properties { get; set; }
    }

    public class GetRequestResponseDto_Properties
    {
        public string PhaseId { get; set; }
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class Related_Properties
    {
    }

}
