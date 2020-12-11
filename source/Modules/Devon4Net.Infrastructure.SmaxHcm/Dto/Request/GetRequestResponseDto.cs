namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request
{
    public class GetRequestResponseDto
    {
        public GetRequestResponseDtoEntity[] entities { get; set; }
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

    public class GetRequestResponseDtoEntity
    {
        public string entity_type { get; set; }
        public GetRequestResponseDtoProperties properties { get; set; }
        public RelatedProperties related_properties { get; set; }
    }

    public class GetRequestResponseDtoProperties
    {
        public string PhaseId { get; set; }
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class RelatedProperties
    {
    }

}
