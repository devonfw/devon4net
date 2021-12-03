namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{
    public class CreateRequestPropertiesDto
    {
        public string RequestedByPerson { get; set; }
        public string RequestsOffering { get; set; }
        public string DisplayLabel { get; set; }
        public string Description { get; set; }
        public string UserOptions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RequestAttachments { get; set; }
    }
}
