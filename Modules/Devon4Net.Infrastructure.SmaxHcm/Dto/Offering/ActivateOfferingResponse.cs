namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class ActivateOfferingResponse
    {
        public ActivateOfferingResponse_Entity_Result_List[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public ActivateOfferingResponse_Meta meta { get; set; }
    }

    public class ActivateOfferingResponse_Meta
    {
        public string completion_status { get; set; }
    }

    public class ActivateOfferingResponse_Entity_Result_List
    {
        public ActivateOfferingResponse_Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class ActivateOfferingResponse_Entity
    {
        public string entity_type { get; set; }
        public ActivateOfferingResponse_Properties properties { get; set; }
        public ActivateOfferingResponse_Related_Properties related_properties { get; set; }
    }

    public class ActivateOfferingResponse_Properties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class ActivateOfferingResponse_Related_Properties
    {
    }

}
