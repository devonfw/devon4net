using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class CreateOfferingRequest
    {
        public string providerId { get; set; }
        public string offeringId { get; set; }
        public string service { get; set; }
        public string offeringDisplayName { get; set; }

    }
}
