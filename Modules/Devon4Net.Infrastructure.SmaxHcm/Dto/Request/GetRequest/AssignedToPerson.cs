namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest
{
    public class AssignedToPerson
    {
        public string Upn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVIP { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}