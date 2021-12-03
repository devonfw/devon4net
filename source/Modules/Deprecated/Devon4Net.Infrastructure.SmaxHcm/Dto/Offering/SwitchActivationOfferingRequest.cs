namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class SwitchActivationOfferingRequest
    {
        public List<SwitchActivationOfferingRequestEntity> entities { get; set; }
        public string operation { get; set; }
    }

    public class SwitchActivationOfferingRequestEntity
    {
        public string entity_type { get; set; }
        public SwitchActivationOfferingRequestProperties properties { get; set; }
    }

    public class SwitchActivationOfferingRequestProperties
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
