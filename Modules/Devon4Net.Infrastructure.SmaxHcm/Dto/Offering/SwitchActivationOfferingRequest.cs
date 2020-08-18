using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class SwitchActivationOfferingRequest
    {
        public List<SwitchActivationOfferingRequest_Entity> entities { get; set; }
        public string operation { get; set; }
    }

    public class SwitchActivationOfferingRequest_Entity
    {
        public string entity_type { get; set; }
        public SwitchActivationOfferingRequest_Properties properties { get; set; }
    }

    public class SwitchActivationOfferingRequest_Properties
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
