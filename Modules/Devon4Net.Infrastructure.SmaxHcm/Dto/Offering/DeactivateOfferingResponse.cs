namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{

    public class DeactivateOfferingResponse
    {
        public DeactivateOfferingResponse_Entity_Result_List[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public DeactivateOfferingResponse_Meta meta { get; set; }
    }

    public class DeactivateOfferingResponse_Meta
    {
        public string completion_status { get; set; }
    }

    public class DeactivateOfferingResponse_Entity_Result_List
    {
        public DeactivateOfferingResponse_Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class DeactivateOfferingResponse_Entity
    {
        public string entity_type { get; set; }
        public DeactivateOfferingResponse_Properties properties { get; set; }
        public DeactivateOfferingResponse_Related_Properties related_properties { get; set; }
    }

    public class DeactivateOfferingResponse_Properties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class DeactivateOfferingResponse_Related_Properties
    {
    }

}
