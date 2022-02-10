namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class Useroptionspropertydescriptor
    {
        public string visibility { get; set; }
        public string fieldSize { get; set; }
        public bool newLine { get; set; }
        public string editorType { get; set; }
        public string presentationId { get; set; }
        public string locallizedHelpTextKey { get; set; }
        public string dataSource { get; set; }
        public string externalPropertyName { get; set; }
        public bool updatable { get; set; }
        public bool isMultiSelect { get; set; }
        public string[] groupPath { get; set; }
        public string level { get; set; }
        public bool encrypted { get; set; }
        public string name { get; set; }
        public string domain { get; set; }
        public bool system { get; set; }
        public string localized_label_key { get; set; }
        public string logical_type { get; set; }
        public bool searchable { get; set; }
        public bool sortable { get; set; }
        public bool text_searchable { get; set; }
        public bool required { get; set; }
        public bool readOnly { get; set; }
        public bool hidden { get; set; }
        public bool unique { get; set; }
        public bool regexFilter { get; set; }
        public int length { get; set; }
        public object[] tags { get; set; }
        public object[] flavors { get; set; }
        public bool discoverable_attribute { get; set; }
        public bool discovered_editable { get; set; }
        public string icon { get; set; }
        public Externalparameters externalParameters { get; set; }
        public string reference_name { get; set; }
    }
}