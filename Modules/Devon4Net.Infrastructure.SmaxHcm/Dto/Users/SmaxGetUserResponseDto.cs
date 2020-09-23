namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Users
{
    public class SmaxGetUserResponseDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public object displayLabel { get; set; }
        public object[] links { get; set; }
        public int id { get; set; }
        public object createdBy { get; set; }
        public long createdAt { get; set; }
        public string lastUpdatedBy { get; set; }
        public long lastUpdatedAt { get; set; }
        public object owner { get; set; }
        public object code { get; set; }
        public bool deleted { get; set; }
        public bool sysData { get; set; }
        public string entityType { get; set; }
        public string customerUID { get; set; }
        public string externalID { get; set; }
        public string userType { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string location { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public bool isDeleted { get; set; }
        public object password { get; set; }
        public string idmOrganization { get; set; }
        public string homePhoneNumber { get; set; }
        public string officePhoneNumber { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string accountId { get; set; }
        public string zipCode { get; set; }
        public object[] groups { get; set; }
        public string[] roles { get; set; }
        public string language { get; set; }
        public object accessType { get; set; }
        public object primaryService { get; set; }
        public string authenticationType { get; set; }
        public object[] tenantList { get; set; }
        public bool locked { get; set; }
        public object[] mspTenantRelations { get; set; }
        public string role { get; set; }
    }
}
