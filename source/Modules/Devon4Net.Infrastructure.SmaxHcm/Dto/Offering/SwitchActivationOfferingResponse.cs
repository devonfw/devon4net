namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class SwitchActivationOfferingResponse
    {
        public SwitchActivationOfferingResponseEntityResultList[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public SwitchActivationOfferingResponseMeta meta { get; set; }
    }

    public class SwitchActivationOfferingResponseMeta
    {
        public string completion_status { get; set; }
    }

    public class SwitchActivationOfferingResponseEntityResultList
    {
        public SwitchActivationOfferingResponseEntity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class SwitchActivationOfferingResponseEntity
    {
        public string entity_type { get; set; }
        public SwitchActivationOfferingResponseProperties properties { get; set; }
        public SwitchActivationOfferingResponseRelatedProperties related_properties { get; set; }
    }

    public class SwitchActivationOfferingResponseProperties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class SwitchActivationOfferingResponseRelatedProperties
    {
    }

}
