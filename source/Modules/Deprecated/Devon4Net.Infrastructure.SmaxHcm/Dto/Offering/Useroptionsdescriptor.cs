namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class Useroptionsdescriptor
    {
        public string name { get; set; }
        public string domain { get; set; }
        public bool system { get; set; }
        public string localized_label_key { get; set; }
        public Useroptionspropertydescriptor[] userOptionsPropertyDescriptors { get; set; }
        public bool volatileComplexType { get; set; }
    }
}