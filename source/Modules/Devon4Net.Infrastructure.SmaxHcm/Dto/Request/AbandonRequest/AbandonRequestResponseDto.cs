namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.AbandonRequest
{

    public class AbandonRequestResponseDto
    {
        public EntityResultList[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string completion_status { get; set; }
    }

    public class EntityResultList
    {
        public Entity entity { get; set; }
        public string completion_status { get; set; }
    }

    public class Entity
    {
        public string entity_type { get; set; }
        public Properties properties { get; set; }
        public RelatedProperties related_properties { get; set; }
    }

    public class Properties
    {
        public string Id { get; set; }
        public long LastUpdateTime { get; set; }
    }

    public class RelatedProperties
    {
    }

}
