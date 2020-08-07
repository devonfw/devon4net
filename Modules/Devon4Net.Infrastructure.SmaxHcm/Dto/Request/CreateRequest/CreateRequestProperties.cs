namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{
    public class CreateRequestProperties
    {
        public string ImpactScope { get; set; }
        public string Urgency { get; set; }
        public string RequestedByPerson { get; set; }
        public string RequestsOffering { get; set; }
        public string RegisteredForActualService { get; set; }
        public string DisplayLabel { get; set; }
        public string UserOptions { get; set; }
        public string[] DataDomains { get; set; }
        public long StartDate { get; set; }
        public string RequestAttachments { get; set; }
        public string Description { get; set; }
    }
}