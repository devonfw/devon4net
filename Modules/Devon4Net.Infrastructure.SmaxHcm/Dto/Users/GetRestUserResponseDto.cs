using System;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Users
{
    public class GetRestUserResponseDto
    {
        [JsonPropertyName("@type")]
        public GetRestUserResponseDtoType type { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public object[] tags { get; set; }
        public GetRestUserResponseDtoProperty1[] property { get; set; }
        public GetRestUserResponseDtoState state { get; set; }
        public GetRestUserResponseDtoOwnedby ownedBy { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
        public GetRestUserResponseDtoPartytype partyType { get; set; }
        public string emailAddress { get; set; }
        public GetRestUserResponseDtoRole[] role { get; set; }
        public GetRestUserResponseDtoPartypreference1[] partyPreference { get; set; }
        public string userName { get; set; }
        public string commonName { get; set; }
        public GetRestUserResponseDtoOrganization organization { get; set; }
    }

    public class GetRestUserResponseDtoType
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoState
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoOwnedby
    {
        public GetRestUserResponseDtoType1 type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public GetRestUserResponseDtoState1 state { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
        public object[] role { get; set; }
        public object[] person { get; set; }
        public object[] contactPerson { get; set; }
        public object[] organization { get; set; }
        public int portalEndDatePeriod { get; set; }
        public bool portalShowCustomPage { get; set; }
        public bool portalErrorDescFlag { get; set; }
        public bool portalShowConfirmDialog { get; set; }
        public bool showLegalNotice { get; set; }
        public bool portalEnforceEndDate { get; set; }
        public bool portalShowTermsOfUse { get; set; }
        public bool portalShowLearnMore { get; set; }
        public int portalTermSubscrOption { get; set; }
        public bool portalAllowAdvGrpOwnedSubscr { get; set; }
    }

    public class GetRestUserResponseDtoType1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoState1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoPartytype
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

    public class GetRestUserResponseDtoOrganization
    {
        public GetRestUserResponseDtoType2 type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public GetRestUserResponseDtoState2 state { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
        public GetRestUserResponseDtoPartytype1 partyType { get; set; }
        public object[] role { get; set; }
        public GetRestUserResponseDtoPartypreference[] partyPreference { get; set; }
        public string accountNumber { get; set; }
        public object[] person { get; set; }
        public object[] contactPerson { get; set; }
        public object[] organization { get; set; }
        public GetRestUserResponseDtoBusinessrole businessRole { get; set; }
        public string portalTitle { get; set; }
        public string portalWelcomeMsg { get; set; }
        public string portalHomePage { get; set; }
        public int portalEndDatePeriod { get; set; }
        public bool portalShowCustomPage { get; set; }
        public int portalLocalCacheSec { get; set; }
        public bool portalErrorDescFlag { get; set; }
        public bool portalShowConfirmDialog { get; set; }
        public bool portalShowLearnMore { get; set; }
        public int portalTermSubscrOption { get; set; }
        public bool portalAllowAdvGrpOwnedSubscr { get; set; }
        public string idmUUID { get; set; }
    }

    public class GetRestUserResponseDtoType2
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoState2
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoPartytype1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoBusinessrole
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserResponseDtoPartypreference
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserResponseDtoContenttype contentType { get; set; }
        public GetRestUserResponseDtoPreferredlocale preferredLocale { get; set; }
    }

    public class GetRestUserResponseDtoContenttype
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype1 categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

    public class GetRestUserResponseDtoPreferredlocale
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype2 categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype2
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

    public class GetRestUserResponseDtoProperty1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        [JsonPropertyName("@modified")]
        public DateTime modified { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserResponseDtoArtifact artifact { get; set; }
        public object[] propertyBindings { get; set; }
        public GetRestUserResponseDtoValuetype valueType { get; set; }
        public GetRestUserResponseDtoValue[] values { get; set; }
        public int maxOccurs { get; set; }
        public int minOccurs { get; set; }
        public int orderIndex { get; set; }
        public bool confidential { get; set; }
        public bool encrypted { get; set; }
        public bool consumerReadOnly { get; set; }
        public bool consumerVisible { get; set; }
        public bool dynamicValueEnabled { get; set; }
        public bool semanticValidationEnabled { get; set; }
        public bool measurable { get; set; }
        public bool multiplied { get; set; }
        public bool upgradeLocked { get; set; }
        public bool newPropertyOnUpgrade { get; set; }
        public bool visibleWhenDeployDesign { get; set; }
        public bool requiredWhenDeployDesign { get; set; }
        public object[] defaultValues { get; set; }
        public object[] availableValues { get; set; }
        public bool valueTypeString { get; set; }
    }

    public class GetRestUserResponseDtoArtifact
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public object[] tags { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
    }

    public class GetRestUserResponseDtoValuetype
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype3 categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype3
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

    public class GetRestUserResponseDtoValue
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        [JsonPropertyName("@modified")]
        public DateTime modified { get; set; }

        public string id { get; set; }
        public int orderIndex { get; set; }
    }

    public class GetRestUserResponseDtoRole
    {
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
    }

    public class GetRestUserResponseDtoPartypreference1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserResponseDtoContenttype1 contentType { get; set; }
        public GetRestUserResponseDtoPreferredlocale1 preferredLocale { get; set; }
    }

    public class GetRestUserResponseDtoContenttype1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype4 categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype4
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

    public class GetRestUserResponseDtoPreferredlocale1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public GetRestUserResponseDtoCategorytype5 categoryType { get; set; }
    }

    public class GetRestUserResponseDtoCategorytype5
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

}
