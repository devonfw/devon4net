namespace Devon4Net.Infrastructure.SmaxHcm.Common
{
    public static class SmaxHcmEndpointConst
    {
        #region AuthCookie
        public const string BoTenant = "/idm-service/idm/v0/api/public/tenant?id={0}";
        public const string BoUserTenant = "idm-service/idm/v0/api/public/tenant?id=sysbo";
        public const string BoLogin = "/bo/boLogin";
        public const string BoAuthenticate = "/idm-service/idm/v0/api/public/authenticate";
        public const string BoLoginTenant = "/idm-service/idm/v0/login?tenant={0}&local=true"; //tenantId
        public const string BoLoginToken = "/idm-service/idm/v0/api/public/token";
        public const string BoUserLogin = "/idm-service/idm/v0/api/public/authenticate/localUser";
        #endregion

        #region Auth
        public const string Logon = "/auth/authentication-endpoint/authenticate/login?TENANTID={0}"; // tenantId
        #endregion

        public const string AuthorizationHeaderTokenkey = "LWSSO_COOKIE_KEY";
        public const string Users = "/bo/rest/entities/user";
        public const string User = "/bo/rest/entities/user/{0}?timeStamp={1}"; //UserId, time stamp
        public const string UserTenants = "/bo/rest/entities/tenant?timeStamp={0}&filter=(user+eq+\"{1}\")";
        public const string Providers= "/{0}/dnd/api/resource/provider"; //tenant ID

        #region Design
        public const string GetDesign = "/{0}/dnd/api/service/design/{1}"; //tenantId, designId
        public const string GetIcons = "/{0}/dnd/api/blobstore?tag=library"; // tenantId
        public const string GetDesignTags = "/{0}/dnd/api/tag/filter"; // tenantId
        public const string CreateDesignContainer = "/{0}/dnd/api/container"; // tenantId
        public const string CreateDesignVersion = "/{0}/dnd/api/service/design"; // tenantId
        public const string DeleteDesignContainer = "/{0}/dnd/api/container/{1}"; // tenantId, versionId
        public const string DeleteDesignVersion = "/{0}/dnd/api/service/design/{1}"; // tenantId, versionId
        public const string PublishDesign = "{0}/dnd/api/service/design/{1}/publish"; // tenantId, designId

        // Service designer
        public const string GetServiceDesignerMetamodel = "{0}/dnd/api/designs/design/{1}/metamodel"; // tenantId, versionId
        public const string GetComponentTemplatesFromComponentType = "/{0}/dnd/api/node/type/{1}/template"; // tenantId, componentTypeId
        public const string CreateComponentsAndRelations = "/{0}/dnd/api/designs/graph/{1}"; // tenantId, versionId
        public const string ApplyComponentTemplateToComponent = "/{0}/dnd/api/designs/graph/{1}/component/{2}/applytemplate"; // tenantId, versionId, componentId
        public const string GetComponentsFromServiceDesigner = "/{0}/dnd/api/designs/graph/{1}"; // tenantId, versionId
        public const string GetPropertiesFromComponent = "/{0}/dnd/api/designs/graph/{1}/component/{2}/properties"; // tenantId, versionId, componentId
        public const string UpdatePropertyFromComponent = "{0}/dnd/api/property/{1}"; // tenantId, propertyId
        public const string UpdateComponent = "/{0}/dnd/api/components/{1}"; // tenantId, componentId
        #endregion

        #region Catalog
        public const string GetCatalogFeaturedProviders = "/rest/{0}/ess/catalog/category/featured-list"; // tenantId
        public const string GetServiceDefinitions = "/rest/{0}/ems/ServiceDefinition?&layout=Id,DisplayLabel,Category,Subtype,PhaseId,Category.DisplayLabel,Category.IsDeleted,Subtype&meta=totalCount&size=250&skip=0"; // tenantId
        public const string CreateNewOffering = "/rest/{0}/cloud-service/createOffering";
        public const string SwitchActivationOffering = "/rest/{0}/ems/bulk"; // tenantId
        #endregion

        #region Request
        public const string GetAllRequest= "/rest/{0}/ess/request/allRequestsWithBundles"; // tenantId
        public const string CreateRequest = "/rest/{0}/ems/bulk"; // tenantId
        #endregion

        #region Offering
        public const string Offerings = "/rest/{0}/ems/Offering/?layout=Id,DisplayLabel,OfferingType,Service,Status,Service.DisplayLabel,Service.IsDeleted&meta=totalCount&skip=0"; //tenant Id
        public const string OfferingDetail = "/rest/{0}/entity-page/initializationData/Offering/{1}"; //tenant ID, offering Id
        public const string CreateOffering = "/rest/{0}/ems/bulk"; // tenantId
        public const string AddAgregatedOffering = "/rest/{0}/cloud-service/createOffering?offeringDisplayName=&offeringId=&providerId=&service="; // tenantId
        #endregion
    }
}
