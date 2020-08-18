using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class DeactivateOfferingRequest
    {
        public List<DeactivateOfferingEntity> entities { get; set; }
        public string operation { get; set; }
    }
}
