namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{
    public class CreateRequestResponse
    {
        public CreateRequestResponse_Entity_Result_List[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public CreateRequestResponse_Meta meta { get; set; }
    }

    public class CreateRequestResponse_Meta
    {
        public string completion_status { get; set; }
    }

    public class CreateRequestResponse_Entity_Result_List
    {
        public CreateRequestResponse_Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class CreateRequestResponse_Entity
    {
        public string entity_type { get; set; }
        public CreateRequestResponse_Properties properties { get; set; }
        public CreateRequestResponse_Related_Properties related_properties { get; set; }
    }

    public class CreateRequestResponse_Properties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class CreateRequestResponse_Related_Properties
    {
    }
}
