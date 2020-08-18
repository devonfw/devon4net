namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class SwitchActivationOfferingResponse
    {
        public SwitchActivationOfferingResponse_Entity_Result_List[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public SwitchActivationOfferingResponse_Meta meta { get; set; }
    }

    public class SwitchActivationOfferingResponse_Meta
    {
        public string completion_status { get; set; }
    }

    public class SwitchActivationOfferingResponse_Entity_Result_List
    {
        public SwitchActivationOfferingResponse_Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class SwitchActivationOfferingResponse_Entity
    {
        public string entity_type { get; set; }
        public SwitchActivationOfferingResponse_Properties properties { get; set; }
        public SwitchActivationOfferingResponse_Related_Properties related_properties { get; set; }
    }

    public class SwitchActivationOfferingResponse_Properties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class SwitchActivationOfferingResponse_Related_Properties
    {
    }

}
