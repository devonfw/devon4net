namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{
    public class CreateRequestResponse
    {
        public CreateRequestResponseEntityResultList[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public CreateRequestResponseMeta meta { get; set; }
    }

    public class CreateRequestResponseMeta
    {
        public string completion_status { get; set; }
    }

    public class CreateRequestResponseEntityResultList
    {
        public CreateRequestResponseEntity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class CreateRequestResponseEntity
    {
        public string entity_type { get; set; }
        public CreateRequestResponseProperties properties { get; set; }
        public CreateRequestResponseRelatedProperties related_properties { get; set; }
    }

    public class CreateRequestResponseProperties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class CreateRequestResponseRelatedProperties
    {
    }
}
