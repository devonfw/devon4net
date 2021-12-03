namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{

    public class CreateOfferingResponseDto
    {
        public List<CreateOfferingResponseEntityResultList> entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public CreateOfferingResponseEntityResultListMeta meta { get; set; }
    }

    public class CreateOfferingResponseEntityResultListMeta
    {
        public string completion_status { get; set; }
    }

    public class CreateOfferingResponseEntityResultList
    {
        public Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class Entity
    {
        public string entity_type { get; set; }
        public CreateOfferingResponseProperties properties { get; set; }
        public CreateOfferingResponseRelatedProperties related_properties { get; set; }
    }

    public class CreateOfferingResponseProperties
    {
        public long LastUpdateTime { get; set; }
        public string Id { get; set; }
    }

    public class CreateOfferingResponseRelatedProperties
    {
    }
}
