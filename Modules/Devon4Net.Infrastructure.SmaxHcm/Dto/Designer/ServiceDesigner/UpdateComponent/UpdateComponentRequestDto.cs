namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdateComponent
{
    public class UpdateComponentRequestDto
    {
        public string global_id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public bool isConsumerVisible { get; set; }
        public bool isPattern { get; set; }
        public int processingOrder { get; set; }
    }
}
