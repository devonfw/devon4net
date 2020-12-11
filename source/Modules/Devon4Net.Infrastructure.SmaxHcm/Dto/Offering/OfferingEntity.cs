namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class OfferingEntity
    {
        public string entity_type { get; set; }
        public OfferingDetailProperties properties { get; set; }
        public OfferingRelatedProperties related_properties { get; set; }
    }
}