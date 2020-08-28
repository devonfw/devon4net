namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{

    public class AddAgregatedOfferingResponseDto
    {
        public string entityType { get; set; }
        public AgregatedOfferingProperties properties { get; set; }
        public AgregatedOfferingRelatedpropertiesmap relatedPropertiesMap { get; set; }
    }
}
