using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class Fieldselectionrule
    {
        public string Field { get; set; }
        public List<OfferingFilter> Filters { get; set; }
        public string Title { get; set; }
    }
}