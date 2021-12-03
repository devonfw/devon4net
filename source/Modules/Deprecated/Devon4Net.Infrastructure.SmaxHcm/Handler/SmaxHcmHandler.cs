﻿using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handlers;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.SmaxHcm.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Catalog;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignVersion;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.GetDesignTags;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.ApplyComponentTemplateToComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdateComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Login;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.AbandonRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SmaxHcm.Exceptions;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public class SmaxHcmHandler : ISmaxHcmHandler
    {
        private IHttpClientHandler HttpClientHandler { get; }
        private SmaxHcmOptions SmaxHcmOptions { get; }
        private string AuthToken { get; set; }
        private string BasicAuthToken { get; set; }

        public SmaxHcmHandler(IHttpClientHandler httpClientHandler, IOptions<SmaxHcmOptions> smaxHcmOptions)
        {
            HttpClientHandler = httpClientHandler;
            SmaxHcmOptions = smaxHcmOptions?.Value ?? throw new ArgumentException("No SmaxHcm options provided");
        }

        #region Designer

        public Task<GetDesignResponseDto> GetDesign(string designId)
        {
            if (string.IsNullOrEmpty(designId))
            {
                throw new ArgumentException("The designId can not be null");
            }

            return SendSmaxHcm<GetDesignResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetDesign, SmaxHcmOptions.TenantId, designId), null, false, true);
        }

        public async Task<byte[]> ExportDesign(string designId, string restUserId)
        {
            if (string.IsNullOrEmpty(designId))
            {
                throw new ArgumentException($"The {nameof(designId)} can not be null");
            }

            using (var designResponse = await SendSmaxHcm(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.ExportDesign, SmaxHcmOptions.TenantId, designId, restUserId), null, false, true))
            {
                return await designResponse.Content.ReadAsByteArrayAsync();
            }
        }

        public Task<GetIconsResponseDto> GetIcons()
        {
            return SendSmaxHcm<GetIconsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetIcons, SmaxHcmOptions.TenantId), null, false, true);
        }

        public Task<GetDesignTagsResponseDto> GetDesignTags()
        {
            try
            {
                var data = new GetDesignTagsRequestDto
            {
                scopes = new string[]
                {
                    DesignConst.Sequence_Artifact_Container
                }
            };

            return SendSmaxHcm<GetDesignTagsResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.GetDesignTags, SmaxHcmOptions.TenantId), data, false, true);
            }
            catch(Exception ex)
            {

                throw new SmaxHcmGenericException($"Error on getting the design tags - {ex.Message}");
            }
        }

        public Task<CreateDesignContainerResponseDto> CreateDesignContainer(CreateDesignContainerDto createDesignContainerDto)
        {
            try
            {
                var tags = new CreateDesignContainerRequestDtoTag[createDesignContainerDto.Tags.Length];
                for (var i = 0; i < createDesignContainerDto.Tags.Length; i++)
                {
                    tags[i] = new CreateDesignContainerRequestDtoTag
                    {
                        self = createDesignContainerDto.Tags[i]
                    };
                }

                var data = new CreateDesignContainerRequestDto
                {
                    container_type = DesignConst.Sequence_Artifact_Container,
                    description = createDesignContainerDto.Description,
                    name = createDesignContainerDto.Name,
                    tags = tags,
                    icon = createDesignContainerDto.Icon
                };

                return SendSmaxHcm<CreateDesignContainerResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateDesignContainer, SmaxHcmOptions.TenantId), data, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on creating the design container - {ex.Message}");
            }
        }

        public Task<CreateDesignVersionResponseDto> CreateDesignVersion(CreateDesignVersionDto createVersionDto)
        {
            try
            {
                var data = new CreateDesignVersionRequestDto
                {
                    containerId = createVersionDto.containerId,
                    description = createVersionDto.description,
                    icon = createVersionDto.icon,
                    name = createVersionDto.name,
                    published = createVersionDto.published,
                    type = DesignConst.Version_Type,
                    upgrades_from = new string[0],
                    upgrades_to = new string[0],
                    url = createVersionDto.url,
                    version = createVersionDto.version
                };

                return SendSmaxHcm<CreateDesignVersionResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateDesignVersion, SmaxHcmOptions.TenantId), data, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting the design version - {ex.Message}");
            }
        }

        public Task DeleteDesignContainer(string containerId)
        {
            return SendSmaxHcm<object>(HttpMethod.Delete, string.Format(SmaxHcmEndpointConst.DeleteDesignContainer, SmaxHcmOptions.TenantId, containerId), null, false, true);
        }

        public Task DeleteDesignVersion(string versionId)
        {
            return SendSmaxHcm<object>(HttpMethod.Delete, string.Format(SmaxHcmEndpointConst.DeleteDesignVersion, SmaxHcmOptions.TenantId, versionId), null, false, true);
        }

        public Task<PublishDesignResponseDto> PublishDesignVersion(string versionId)
        {
            try
            {
                return SendSmaxHcm<PublishDesignResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.PublishDesign, SmaxHcmOptions.TenantId, versionId), null, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on publishing the design version - {ex.Message}");
            }
        }

        // Service designer

        public Task<GetServiceDesignerMetamodelResponseDto> GetServiceDesignerMetamodel(string versionId)
        {
            try
            {
                return SendSmaxHcm<GetServiceDesignerMetamodelResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetServiceDesignerMetamodel, SmaxHcmOptions.TenantId, versionId), null, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting service designer metamodel - {ex.Message}");
            }
        }

        public Task<GetComponentTemplatesFromComponentTypeResponseDto> GetComponentTemplatesFromComponentType(string componentTypeId)
        {
            try
            {
                return SendSmaxHcm<GetComponentTemplatesFromComponentTypeResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetComponentTemplatesFromComponentType, SmaxHcmOptions.TenantId, componentTypeId), null, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting the component templates from component type - {ex.Message}");
            }
        }

        public Task<CreateComponentsAndRelationsResponseDto> CreateComponentsAndRelations(string versionId, CreateComponentsAndRelationsDto createComponentsAndRelationsDto)
        {
            try
            {
                var nodes = new List<CreateComponentsAndRelationsRequestDtoNode>();
                var relationships = new List<CreateComponentsAndRelationsRequestDtoRelationship>();

                foreach (var node in createComponentsAndRelationsDto.nodes)
                {
                    nodes.Add(new CreateComponentsAndRelationsRequestDtoNode
                    {
                        name = node.name,
                        description = node.description,
                        displayName = node.displayName,
                        icon = node.icon,
                        orderIndex = node.orderIndex,
                        statusMessages = new object[0],
                        tags = new string[]
                        {
                            DesignConst.Create_Component_Tag_Consumer_Visible
                        },
                        typeId = node.typeId,
                        x = node.x,
                        y = node.y
                    });
                }

                foreach (var relationship in createComponentsAndRelationsDto.relationships)
                {
                    relationships.Add(new CreateComponentsAndRelationsRequestDtoRelationship
                    {
                        name = relationship.name,
                        displayName = relationship.displayName,
                        relationshipTypeId = relationship.relationshipTypeId,
                        source = new CreateComponentsAndRelationsRequestDtoSource
                        {
                            name = relationship.sourceName
                        },
                        target = new CreateComponentsAndRelationsRequestDtoTarget
                        {
                            name = relationship.targetName
                        }
                    });
                }

                var data = new CreateComponentsAndRelationsRequestDto
                {
                    groups = new object[0],
                    nodes = nodes.ToArray(),
                    relationships = relationships.ToArray()
                };

                return SendSmaxHcm<CreateComponentsAndRelationsResponseDto>(HttpMethod.Put, string.Format(SmaxHcmEndpointConst.CreateComponentsAndRelations, SmaxHcmOptions.TenantId, versionId), data, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on creting components and relations - {ex.Message}");
            }
        }

        public Task<GetOverviewFromComponentResponseDto> GetOverviewFromComponent(string componentId)
        {
            return SendSmaxHcm<GetOverviewFromComponentResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetOverviewFromComponent, SmaxHcmOptions.TenantId, componentId), null, false, true);
        }

        public Task UpdateComponent(UpdateComponentDto updateComponentDto)
        {
            var data = new UpdateComponentRequestDto
            {
                global_id = updateComponentDto.component_id,
                name = updateComponentDto.name,
                displayName = updateComponentDto.displayName,
                description = updateComponentDto.description,
                icon = updateComponentDto.icon,
                processingOrder = updateComponentDto.processingOrder,
                isConsumerVisible = updateComponentDto.isConsumerVisible,
                isPattern = updateComponentDto.isPattern
            };

            return SendSmaxHcm<object>(HttpMethod.Put, string.Format(SmaxHcmEndpointConst.UpdateComponent, SmaxHcmOptions.TenantId, updateComponentDto.component_id), data, false, true);
        }

        public Task<ApplyComponentTemplateToComponentResponseDto> ApplyComponentTemplateToComponent(ApplyComponentTemplateToComponentDto applyComponentTemplateToComponentDto)
        {
            try
            {
                var data = new ApplyComponentTemplateToComponentRequestDto
                {
                    componentTemplateId = applyComponentTemplateToComponentDto.componentTemplateId
                };

                return SendSmaxHcm<ApplyComponentTemplateToComponentResponseDto>(HttpMethod.Put, string.Format(SmaxHcmEndpointConst.ApplyComponentTemplateToComponent, SmaxHcmOptions.TenantId, applyComponentTemplateToComponentDto.versionId, applyComponentTemplateToComponentDto.componentId), data, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on applying component template to component - {ex.Message}");
            }
        }

        public Task<GetComponentsResponseDto> GetComponentsFromServiceDesigner(string versionId)
        {
            return SendSmaxHcm<GetComponentsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetComponentsFromServiceDesigner, SmaxHcmOptions.TenantId, versionId), null, false, true);
        }

        public Task<GetPropertiesFromComponentResponseDto> GetPropertiesFromComponent(string versionId, string componentId)
        {
            try
            {
                return SendSmaxHcm<GetPropertiesFromComponentResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetPropertiesFromComponent, SmaxHcmOptions.TenantId, versionId, componentId), null, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting the properties from component - {ex.Message}");
            }
        }

        public Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, string value)
        {
            try
            {
                return UpdatePropertyFromComponent(propertyId, ComponentPropertyTypesConst.STRING, value);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on updating property from component - {ex.Message}");
            }
        }

        public Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, int value)
        {
            return UpdatePropertyFromComponent(propertyId, ComponentPropertyTypesConst.NUMBER, value);
        }

        public Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, bool value)
        {
            return UpdatePropertyFromComponent(propertyId, ComponentPropertyTypesConst.BOOLEAN, value);
        }

        public Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, List<UpdateListPropertyFromComponentDto> value)
        {
            var data = new List<UpdatePropertyListDto>();

            foreach (var property in value)
            {
                data.Add(new UpdatePropertyListDto
                {
                    name = property.name,
                    description = property.description,
                    value = property.value,
                    value_type = ComponentPropertyTypesConst.STRING.ToString()
                });
            }

            return UpdatePropertyFromComponent(propertyId, ComponentPropertyTypesConst.LIST, data);
        }

        private Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, ComponentPropertyTypesConst propertyType, object value)
        {
            var data = new UpdatePropertyFromComponentRequestDto
            {
                property_type = propertyType.ToString(),
                property_value = value
            };

            return SendSmaxHcm<UpdatePropertyFromComponentResponseDto>(HttpMethod.Put, string.Format(SmaxHcmEndpointConst.UpdatePropertyFromComponent, SmaxHcmOptions.TenantId, propertyId), data, false, true);
        }

        #endregion

        #region Tenants

        public Task<GetUserTenantsResponseDto> GetUserTenants(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<GetUserTenantsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.UserTenants, DateTime.Now.Ticks.ToString(), userId));
        }

        #endregion

        #region Offerings

        public Task<GetOfferingsResponseDto> GetOfferings()
        {
            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Offerings, SmaxHcmOptions.TenantId));
        }

        public Task<GetOfferingResponseDto> GetOffering(string offeringId)
        {
            if (string.IsNullOrEmpty(offeringId))
            {
                throw new ArgumentException("The offeringId can not be null");
            }

            return SendSmaxHcm<GetOfferingResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.OfferingDetail, SmaxHcmOptions.TenantId, offeringId));
        }

        public Task<CreateOfferingResponseDto> CreateOffering(CreateOfferingRequestDto createOfferingRequestDto)
        {
            var data = new CreateOfferingEntity
            {
                entity_type = BulkEntityConst.Offering,
                properties = new CreateOfferingProperties
                {
                    Description = createOfferingRequestDto.Description,
                    DisplayLabel = createOfferingRequestDto.DisplayLabel,
                    IsBundle = createOfferingRequestDto.IsBundle,
                    IsDefault = createOfferingRequestDto.IsDefault,
                    IsPopularity = createOfferingRequestDto.IsPopularity,
                    NumOfRequests = createOfferingRequestDto.NumOfRequests,
                    OfferingType = OfferingTypeConst.ServiceOffering,
                    RequireAssetInfo = createOfferingRequestDto.RequireAssetInfo,
                    Service = createOfferingRequestDto.Service,
                    Status = OfferingStatusConst.Active,
                    SubscriptionActionType = SubscriptionActionTypeConst.All
                }
            };

            var request = new CreateBulkOfferingRequestDto
            {
                operation = BulkOperationConst.Create,
                entities = new List<CreateOfferingEntity> { data }
            };

            return SendSmaxHcm<CreateOfferingResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateOffering, SmaxHcmOptions.TenantId), request);
        }

        public Task<GetOfferingProvidersResponseDto> GetOfferingProviders(string searchText = null, string[] tags = null)
        {
            try
            {
                var data = new GetOfferingProvidersRequestDto
                {
                    providerId = SmaxHcmOptions.ProviderId,
                    tags = tags,
                    text = searchText
                };

                return SendSmaxHcm<GetOfferingProvidersResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.GetOfferingProviders, SmaxHcmOptions.TenantId), data, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting offering providers - {ex.Message}");
            }
        }

        public Task<AddAgregatedOfferingResponseDto> AddAggregatedOffering(AddAgregatedOfferingDto addAgregatedOfferingDto)
        {
            try
            {
                if (addAgregatedOfferingDto == null || string.IsNullOrEmpty(addAgregatedOfferingDto.offeringId) || string.IsNullOrEmpty(addAgregatedOfferingDto.offeringDisplayName))
                {
                    throw new ArgumentException("The offeringId or the display name can not be null");
                }

                var data = new AddAgregatedOfferingRequestDto
                {
                    offeringDisplayName = addAgregatedOfferingDto.offeringDisplayName,
                    offeringId = addAgregatedOfferingDto.offeringId,
                    providerId = SmaxHcmOptions.ProviderId,
                    service = addAgregatedOfferingDto.service
                };

                return SendSmaxHcm<AddAgregatedOfferingResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.AddAgregatedOffering, SmaxHcmOptions.TenantId), data);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on adding aggregated offering - {ex.Message}");
            }
        }

        public Task<object> UpdateOffering(UpdateOfferingDto updateOfferingDto)
        {

            if (updateOfferingDto == null || string.IsNullOrEmpty(updateOfferingDto.offeringId))
            {
                throw new ArgumentException(
                    "Please check the offering object properties. Object can not be null or empty");
            }

            var data = new UpdateOfferingRequest
            {
                providerId = string.IsNullOrEmpty(updateOfferingDto.providerId) ? SmaxHcmOptions.ProviderId : updateOfferingDto.providerId,
                service = updateOfferingDto.service,
                offeringDisplayName = updateOfferingDto.offeringDisplayName,
                offeringId = updateOfferingDto.offeringId
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateNewOffering, SmaxHcmOptions.TenantId), data);
        }

        public Task<SwitchActivationOfferingResponse> SwitchActivationOffering(string offeringId, bool activate = true)
        {
            try
            {
                var data = new SwitchActivationOfferingRequest
                {
                    entities = new List<SwitchActivationOfferingRequestEntity>
                    {
                        new SwitchActivationOfferingRequestEntity
                        {
                            entity_type = BulkEntityConst.Offering,
                            properties = new SwitchActivationOfferingRequestProperties()
                            {
                                Id = offeringId,
                                Status = activate ? OfferingStatusConst.Active : OfferingStatusConst.Inactive
                            }
                        }
                    },
                    operation = BulkOperationConst.Update
                };

                return SendSmaxHcm<SwitchActivationOfferingResponse>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.SwitchActivationOffering, SmaxHcmOptions.TenantId), data);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on switching activation offering - {ex.Message}");
            }
        }

        #endregion

        #region Providers

        public Task<GetProvidersResponseDto> GetProviders()
        {
            return SendSmaxHcm<GetProvidersResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Providers, SmaxHcmOptions.TenantId), null, false, true);
        }
        #endregion

        #region Users
        public Task<GetUsersResponseDto> GetUsers()
        {
            return SendSmaxHcm<GetUsersResponseDto>(HttpMethod.Get, SmaxHcmEndpointConst.Users);
        }

        public Task<SmaxGetUserResponseDto> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<SmaxGetUserResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.User, userId, DateTime.Now.Ticks.ToString()));
        }

        public Task<GetRestUserResponseDto> GetRestUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"The {nameof(userName)} can not be null");
            }

            return SendSmaxHcm<GetRestUserResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.RestUserId, SmaxHcmOptions.TenantId, userName), null, false, true, true);
        }
        #endregion

        #region Security

        public async Task<string> Login(string userName, string password)
        {
            BasicAuthToken = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
            AuthToken = await HttpClientHandler.Send<string>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, string.Format(SmaxHcmEndpointConst.Logon, SmaxHcmOptions.TenantId), new LoginRequestDto { Login = userName, Password = password }, MediaType.ApplicationJson);
            return AuthToken;
        }
        #endregion

        #region HttpMethods and security
        private async Task<T> SendSmaxHcm<T>(HttpMethod httpMethod, string endpoint, object content = null,  bool getUrlWithTenant = false, bool addBasicAuth = false, bool addAcceptJsonHeader = false, bool useCamelCase = false)
        {
            await PerformLogin();
            return await HttpClientHandler.Send<T>(httpMethod, SmaxHcmOptions.CircuitBreakerName, getUrlWithTenant ? GetUrlWithTenant(endpoint) : endpoint, content, MediaType.ApplicationJson, GetAuthorizationHeaders(addBasicAuth, addAcceptJsonHeader), true, useCamelCase);
        }

        private async Task<HttpResponseMessage> SendSmaxHcm(HttpMethod httpMethod, string endpoint, object content = null, bool getUrlWithTenant = false, bool addBasicAuth = false, bool addAcceptJsonHeader = false, bool useCamelCase = false)
        {
            await PerformLogin();
            return await HttpClientHandler.Send(httpMethod, SmaxHcmOptions.CircuitBreakerName, getUrlWithTenant ? GetUrlWithTenant(endpoint) : endpoint, content, MediaType.ApplicationJson, false, false, GetAuthorizationHeaders(addBasicAuth, addAcceptJsonHeader));
        }

        private string GetUrlWithTenant(string originalUrl)
        {
            return string.IsNullOrEmpty(SmaxHcmOptions.TenantId) ? originalUrl : $"{originalUrl}&TENANTID={SmaxHcmOptions.TenantId}";
        }

        /// <summary>
        /// Please check that some SMAX endpoint sometimes need the basic Athentication
        /// </summary>
        /// <param name="addBasicAuthentication">Flag to set the basic authentication in the header's request</param>
        /// <returns></returns>
        private Dictionary<string, string> GetAuthorizationHeaders(bool addBasicAuthentication, bool addAcceptJsonHeader)
        {
            var result = new Dictionary<string, string>
            {
                {"Cookie", $"{SmaxHcmEndpointConst.AuthorizationHeaderTokenkey}={AuthToken};TENANTID={SmaxHcmOptions.TenantId}"}
            };

            if (addBasicAuthentication)
            {
                result.Add("Authorization", $"Basic {BasicAuthToken}");
            }

            if (addAcceptJsonHeader)
            {
                result.Add("Accept", MediaType.ApplicationJson);
            }

            return result;
        }

        private async Task PerformLogin()
        {
            if (SmaxHcmOptions == null || string.IsNullOrEmpty(SmaxHcmOptions.UserName) || string.IsNullOrEmpty(SmaxHcmOptions.Password))
            {
                throw new SmaxHcmUnauthorizedException("No Smax authorization credentials provided");
            }

            AuthToken = await Login(SmaxHcmOptions.UserName, SmaxHcmOptions.Password).ConfigureAwait(false);
        }
        #endregion

        #region Catalog

        public Task<object> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query)
        {
            var data = new QueryCatalogRequest
            {
                categoryId = category,
                includeArticles = includeArticles,
                includeOfferings = includeOfferings,
                searchQuery = query ?? string.Empty
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.GetCatalogFeaturedProviders, SmaxHcmOptions.TenantId), data);
        }

        public Task<GetOfferingsResponseDto> GetServiceDefinitions()
        {
            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetServiceDefinitions, SmaxHcmOptions.TenantId));
        }
        #endregion

        #region Request

        public Task<GetAllRequestsResponseDto> GetAllRequest()
        {
            return SendSmaxHcm<GetAllRequestsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetAllRequest, SmaxHcmOptions.TenantId));
        }

        /// <summary>
        /// sample: { "entity": { "entity_type": "Request", "properties": { "ImpactScope": "Enterprise", "Urgency": "NoDisruption", "RequestedByPerson": "10016", "RequestsOffering": "11428", "DisplayLabel": "Request from Postman", "Description": "<p>This is a request created from Postman</p>", "UserOptions": "{"complexTypeProperties":[{"properties":{"OptionSet5FCCC5624DDDBA17DA397224C5D4ED0B_c":{"Option55F063755603608CB8287224C5D426DE_c":true},"OptionSetB9C2FB0D304A86418C651492D37A5E72_c":{"Option90AB77B8232585150B951492D37AE403_c":true},"changedUserOptionsForSimulation":"EndDate&","PropertyamazonResourceProvider55F063755603608CB8287224C5D426DE_c":"8a809efe73146d590173147b4a954cfc","Propertyregion55F063755603608CB8287224C5D426DE_c":"eu-west-1","PropertyPlatformType55F063755603608CB8287224C5D426DE_c":"Amazon Linux#eu-west-1","Propertykeypair55F063755603608CB8287224C5D426DE_c":"FARM-1-0d7bdb2a","PropertyvpcId55F063755603608CB8287224C5D426DE_c":"eu-west-1#vpc-017f010990ed442ce","Propertyimage55F063755603608CB8287224C5D426DE_c":"ami-0093757e056f6fe96","PropertysubnetId55F063755603608CB8287224C5D426DE_c":"subnet-0dcd276d74eb84059"}}]}", "DataDomains": [ "Public" ], "StartDate": 1596664800000, "EndDate": 1599170400000, "RequestAttachments": "{"complexTypeProperties":[]}" } }, "operation": "CREATE" } 
        /// </summary>
        /// <param name="createNewRequestDto">The response of the API please check the status provided at "completion_status" node</param>
        /// <returns></returns>
        public Task<CreateRequestResponse> CreateRequest(CreateRequestPropertiesDto createNewRequestDto)
        {
            try
            {
                if (createNewRequestDto == null)
                {
                    throw new ArgumentException(
                        "Please check the create NewRequest Dto object properties. Object can not be null or empty");
                }

                var data = new CreateRequestEntity
                {
                    entity_type = BulkEntityConst.Request,
                    properties = new CreateRequestProperties
                    {
                        Description = createNewRequestDto.Description,
                        DisplayLabel = createNewRequestDto.DisplayLabel,
                        StartDate = GetTotalMillisecondsFromDateTime(createNewRequestDto.StartDate),
                        EndDate = createNewRequestDto.EndDate.HasValue ? GetTotalMillisecondsFromDateTime(createNewRequestDto.EndDate.Value) : null as long?,
                        RequestedByPerson = createNewRequestDto.RequestedByPerson,
                        RequestsOffering = createNewRequestDto.RequestsOffering,
                        ImpactScope = BulkImpactScopeConst.Enterprise,
                        Urgency = BulkUrgencyConst.NoDisruption,
                        //UserOptions = createNewRequestDto.entity.properties.UserOptions != null ? createNewRequestDto.entity.properties.UserOptions : new UserOptionsDto{complexTypeProperties = new List<Complextypeproperty>()},
                        UserOptions = createNewRequestDto.UserOptions,
                        DataDomains = new List<string> { BulkDataDomainsConst.Public },
                        RequestAttachments = createNewRequestDto.RequestAttachments
                    }
                };

                var request = new CreateRequestDto
                {
                    operation = BulkOperationConst.Create,
                    entities = new List<CreateRequestEntity> { data }
                };

                return SendSmaxHcm<CreateRequestResponse>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateRequest, SmaxHcmOptions.TenantId), request);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on creating request - {ex.Message}");
            }
        }

        public Task<AbandonRequestResponseDto> AbandonRequest(string idRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(idRequest))
                {
                    throw new ArgumentException(
                        "Please request ID can not be null or empty");
                }

                var data = new AbandonRequestEntity
                {
                    entity_type = BulkEntityConst.Request,
                    properties = new AbandonRequestProperties
                    {
                        Id = idRequest,
                        PhaseId = PhaseIdValuesConst.Abandon
                    }
                };

                var request = new AbandonRequestDto
                {
                    operation = BulkOperationConst.Update,
                    entities = new List<AbandonRequestEntity> { data }
                };

                return SendSmaxHcm<AbandonRequestResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateRequest, SmaxHcmOptions.TenantId), request);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on abandoning request - {ex.Message}");
            }
        }

        public Task<GetUsersByUserNameResponse> GetUsersByUserName(string username)
        {
            try
            {
                return SendSmaxHcm<GetUsersByUserNameResponse>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetUsersByName, SmaxHcmOptions.TenantId, username), null, false, true);
            }
            catch (Exception ex)
            {
                throw new SmaxHcmGenericException($"Error on getting users by user name - {ex.Message}");
            }
        }

        public Task<GetRequestResponseDto> GetRequestById(string requestId)
        {
            return SendSmaxHcm<GetRequestResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetRequestById, SmaxHcmOptions.TenantId, requestId), null, false, true);
        }

        private long GetTotalMillisecondsFromDateTime(DateTime dateTime)
        {
            if (dateTime == default || dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                throw new ArgumentException($"The {nameof(dateTime)} provided can not have the value {dateTime}");
            }

            var offSet = new DateTimeOffset(dateTime);
            return offSet.ToUnixTimeMilliseconds();
        }

        #endregion
    }
}
