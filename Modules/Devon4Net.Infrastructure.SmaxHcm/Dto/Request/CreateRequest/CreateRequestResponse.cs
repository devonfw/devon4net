namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{

    public class CreateRequestResponse
    {
        public CreateRequestResponseEntity_Result_List[] entity_result_list { get; set; }
        public object[] relationship_result_list { get; set; }
        public object[] translation_result_list { get; set; }
        public CreateRequestResponseMeta meta { get; set; }
    }

    public class CreateRequestResponseMeta
    {
        public string completion_status { get; set; }
    }

    public class CreateRequestResponseEntity_Result_List
    {
        public CreateRequestResponseEntity entity { get; set; }
        public string completion_status { get; set; }
        public Errordetails errorDetails { get; set; }
    }

    public class CreateRequestResponseEntity
    {
        public string entity_type { get; set; }
        public Properties properties { get; set; }
        public Related_Properties related_properties { get; set; }
    }

    public class Properties
    {
        public string UserOptions { get; set; }
        public string Description { get; set; }
        public string UserOptionsName { get; set; }
        public string RequestedForPerson { get; set; }
        public string DisplayLabel { get; set; }
        public long StartDate { get; set; }
        public bool FirstTouch { get; set; }
        public string RequestedByPerson { get; set; }
        public string PublicScope { get; set; }
        public string OwnedByPerson { get; set; }
        public string AssignedToPerson { get; set; }
        public string ChatStatus { get; set; }
        public int SLT { get; set; }
        public string PreferredContactMethod { get; set; }
        public string AssignedToGroup { get; set; }
        public string ServiceDeskGroup { get; set; }
        public string PhaseId { get; set; }
        public string[] DataDomains { get; set; }
        public string RequestAttachments { get; set; }
        public string Priority { get; set; }
        public string Comments { get; set; }
        public long CreateTime { get; set; }
        public long EndDate { get; set; }
        public bool ServiceImpacted { get; set; }
        public bool Active { get; set; }
        public string SubscriptionActionType { get; set; }
        public string OfferingWorkflow { get; set; }
        public string ImpactScope { get; set; }
        public bool FirstLine { get; set; }
        public string CreationSource { get; set; }
        public string ProcessId { get; set; }
        public string CurrentAssignment { get; set; }
        public string Urgency { get; set; }
        public string RequestsOffering { get; set; }
        public string RequestType { get; set; }
    }

    public class Related_Properties
    {
    }

    public class Errordetails
    {
        public int httpStatus { get; set; }
        public string message { get; set; }
        public string developer_message { get; set; }
        public string message_key { get; set; }
        public string message_rb { get; set; }
        public object[] message_arguments { get; set; }
        public string exceptionType { get; set; }
    }

}
