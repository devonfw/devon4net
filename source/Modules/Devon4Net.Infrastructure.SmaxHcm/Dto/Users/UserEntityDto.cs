using System.Collections.Generic;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Common;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Users
{
    public class UserEntityDto
    {
        public string name { get; set; }
        public List<object> links { get; set; }
        public int id { get; set; }
        public string createdBy { get; set; }
        public long createdAt { get; set; }
        public string lastUpdatedBy { get; set; }
        public long lastUpdatedAt { get; set; }
        public bool deleted { get; set; }
        public bool sysData { get; set; }
        public string entityType { get; set; }
        public string externalID { get; set; }
        public string userType { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public bool isDeleted { get; set; }
        public string idmOrganization { get; set; }
        public EntityDto accountId { get; set; }
        public List<string> roles { get; set; }
        public string role { get; set; }
        public string authenticationType { get; set; }
        public List<int?> tenantList { get; set; }
        public bool locked { get; set; }
        public string customerUID { get; set; }
        public string middleName { get; set; }
        public string homePhoneNumber { get; set; }
        public string officePhoneNumber { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string zipCode { get; set; }
        public string language { get; set; }
    }
}