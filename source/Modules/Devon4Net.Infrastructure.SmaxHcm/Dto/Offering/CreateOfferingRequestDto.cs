namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class CreateOfferingRequestDto
    {
        public int NumOfRequests { get; set; }
        public bool IsPopularity { get; set; }
        public string Service { get; set; }
        public string RequireAssetInfo { get; set; }
        public string DisplayLabel { get; set; }
        public bool IsBundle { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
    }
}
