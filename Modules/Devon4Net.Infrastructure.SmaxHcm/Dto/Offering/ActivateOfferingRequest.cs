using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class ActivateOfferingRequest
    {
        public List<ActivateOfferingEntity> entities { get; set; }
        public string operation { get; set; }
    }
}
