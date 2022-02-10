namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class Enumerationdescriptor
    {
        public string name { get; set; }
        public string domain { get; set; }
        public bool system { get; set; }
        public string localized_label_key { get; set; }
        public List<EnumerationdescriptorValue> values { get; set; }
        public int maxNumberOfValues { get; set; }
        public bool volatileEnumeration { get; set; }
        public string orderBy { get; set; }
    }
}