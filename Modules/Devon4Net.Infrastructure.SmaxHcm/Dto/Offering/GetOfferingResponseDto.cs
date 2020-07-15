using System;
using System.Text;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{

    public class GetOfferingResponseDto
    {
        public OfferingEntity EntityData { get; set; }
        public Simulationresult SimulationResult { get; set; }
        public Useroptionsdata UserOptionsData { get; set; }
    }

    public class EnumerationdescriptorValue
    {
        public string name { get; set; }
        public string domain { get; set; }
        public bool system { get; set; }
        public string localized_label_key { get; set; }
        public int ordinal_number { get; set; }
        public string icon { get; set; }
        public bool is_disabled { get; set; }
        public string external_name { get; set; }
    }
}
