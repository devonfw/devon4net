namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{

    public class CreateBulkOfferingRequestDto
    {
        public List<CreateOfferingEntity> entities { get; set; }
        public string operation { get; set; }
    }

    public class CreateOfferingEntity
    {
        public string entity_type { get; set; }
        public CreateOfferingProperties properties { get; set; }
    }

    public class CreateOfferingProperties
    {
        public int NumOfRequests { get; set; }
        public bool IsPopularity { get; set; }
        public string Service { get; set; }
        public string OfferingType { get; set; }
        public string Status { get; set; }
        public string SubscriptionActionType { get; set; }
        public string RequireAssetInfo { get; set; }
        public string DisplayLabel { get; set; }
        public bool IsBundle { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
    }
}
