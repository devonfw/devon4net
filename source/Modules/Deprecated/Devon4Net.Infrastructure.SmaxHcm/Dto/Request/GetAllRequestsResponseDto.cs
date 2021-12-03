namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request
{
    public class GetAllRequestsResponseDto
    {
        public GetAllRequestsResponseDtoRequest[] requests { get; set; }
        public int totalCount { get; set; }
    }

    public class GetAllRequestsResponseDtoRequest
    {
        public GetAllRequestsResponseDtoProperties properties { get; set; }
        public GetAllRequestsResponseDtoRelatedpropertiesmap relatedPropertiesMap { get; set; }
        public object translations { get; set; }
        public string entityType { get; set; }
        public string id { get; set; }
        public string entityDescriptorName { get; set; }
        public string[] propertyNamesIterator { get; set; }
        public string[] relatedPropertyNamesIterator { get; set; }
    }

    public class GetAllRequestsResponseDtoProperties
    {
        public string PhaseId { get; set; }
        public string Description { get; set; }
        public long LastUpdateTime { get; set; }
        public long CreateTime { get; set; }
        public string RequestedForPerson { get; set; }
        public string ProcessId { get; set; }
        public string Id { get; set; }
        public string DisplayLabel { get; set; }
        public string RequestType { get; set; }
        public string RequestsOffering { get; set; }
    }

    public class GetAllRequestsResponseDtoRelatedpropertiesmap
    {
        public GetAllRequestsResponseDtoRequestedforperson RequestedForPerson { get; set; }
        public GetAllRequestsResponseDtoRequestsoffering RequestsOffering { get; set; }
    }

    public class GetAllRequestsResponseDtoRequestedforperson
    {
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }

    public class GetAllRequestsResponseDtoRequestsoffering
    {
        public string Id { get; set; }
        public string Image { get; set; }
    }

}
