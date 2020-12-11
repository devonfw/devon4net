using Devon4Net.Infrastructure.SmaxHcm.Dto.Common;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants
{
    public class TenantEntityDto
    {
        public string entityType { get; set; }
        public string tenantType { get; set; }
        public EntityDto account { get; set; }
        public string tenantState { get; set; }
        public long lastActiveDate { get; set; }
        public string tenantEnvironment { get; set; }
        public bool mspTenant { get; set; }
        public bool managedTenant { get; set; }
        public object[] contacts { get; set; }
        public string url { get; set; }
        public string mspType { get; set; }
        public object[] licenses { get; set; }
        public bool accessControl { get; set; }
        public bool accessControlMergeResult { get; set; }
        public EntityDto tenantAdmin { get; set; }
        public bool smIntegrated { get; set; }
        public string backendType { get; set; }
        public bool samIntegrated { get; set; }
        public bool nativeSacmEnabled { get; set; }
        public string defaultLoginAuthType { get; set; }
        public string attachmentWhiteList { get; set; }
        public int attachmentMaxSize { get; set; }
        public bool rbacEnabled { get; set; }
        public string version { get; set; }
        public bool userSync { get; set; }
        public string name { get; set; }
        public object[] links { get; set; }
        public int id { get; set; }
        public EntityDto createdBy { get; set; }
        public long createdAt { get; set; }
        public EntityDto lastUpdatedBy { get; set; }
        public long lastUpdatedAt { get; set; }
        public bool deleted { get; set; }
        public bool sysData { get; set; }
    }
}