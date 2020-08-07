namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest
{
    public class EntityRequest
    {
        public string entity_type { get; set; }
        public RequestProperties properties { get; set; }
        public RequestRelatedProperties related_properties { get; set; }
    }
}