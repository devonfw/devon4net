namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest
{
    public class RequestProperties
    {
        public string PhaseId { get; set; }
        public string AssignedToPerson { get; set; }
        public long LastUpdateTime { get; set; }
        public string Priority { get; set; }
        public string RequestedForPerson { get; set; }
        public string ChatStatus { get; set; }
        public string Id { get; set; }
        public string CurrentAssignment { get; set; }
        public string ProcessId { get; set; }
        public string AssignedToGroup { get; set; }
        public string DisplayLabel { get; set; }
    }
}