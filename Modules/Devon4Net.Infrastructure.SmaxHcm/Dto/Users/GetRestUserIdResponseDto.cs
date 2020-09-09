using System;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Users
{
    public class GetRestUserIdResponseDto
    {
        [JsonPropertyName("@type")]
        public GetRestUserIdResponseDtoType type { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public object[] tags { get; set; }
        public GetRestUserIdResponseDtoProperty1[] property { get; set; }
        public GetRestUserIdResponseDtoState state { get; set; }
        public GetRestUserIdResponseDtoOwnedby ownedBy { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
        public GetRestUserIdResponseDtoPartytype partyType { get; set; }
        public string emailAddress { get; set; }
        public GetRestUserIdResponseDtoRole[] role { get; set; }
        public GetRestUserIdResponseDtoPartypreference1[] partyPreference { get; set; }
        public string userName { get; set; }
        public string commonName { get; set; }
        public GetRestUserIdResponseDtoOrganization organization { get; set; }
    }

    public class GetRestUserIdResponseDtoType
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoState
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoOwnedby
    {
        public GetRestUserIdResponseDtoType1 type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public GetRestUserIdResponseDtoState1 state { get; set; }
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

    public class GetRestUserIdResponseDtoType1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoState1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoPartytype
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
        public GetRestUserIdResponseDtoCategorytype categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype
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

    public class GetRestUserIdResponseDtoOrganization
    {
        public GetRestUserIdResponseDtoType2 type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public GetRestUserIdResponseDtoState2 state { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
        public GetRestUserIdResponseDtoPartytype1 partyType { get; set; }
        public object[] role { get; set; }
        public GetRestUserIdResponseDtoPartypreference[] partyPreference { get; set; }
        public string accountNumber { get; set; }
        public object[] person { get; set; }
        public object[] contactPerson { get; set; }
        public object[] organization { get; set; }
        public GetRestUserIdResponseDtoBusinessrole businessRole { get; set; }
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

    public class GetRestUserIdResponseDtoType2
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoState2
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoPartytype1
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoBusinessrole
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public bool disabled { get; set; }
    }

    public class GetRestUserIdResponseDtoPartypreference
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserIdResponseDtoContenttype contentType { get; set; }
        public GetRestUserIdResponseDtoPreferredlocale preferredLocale { get; set; }
    }

    public class GetRestUserIdResponseDtoContenttype
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
        public GetRestUserIdResponseDtoCategorytype1 categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype1
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

    public class GetRestUserIdResponseDtoPreferredlocale
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
        public GetRestUserIdResponseDtoCategorytype2 categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype2
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

    public class GetRestUserIdResponseDtoProperty1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        [JsonPropertyName("@modified")]
        public DateTime modified { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserIdResponseDtoArtifact artifact { get; set; }
        public object[] propertyBindings { get; set; }
        public GetRestUserIdResponseDtoValuetype valueType { get; set; }
        public GetRestUserIdResponseDtoValue[] values { get; set; }
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

    public class GetRestUserIdResponseDtoArtifact
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

    public class GetRestUserIdResponseDtoValuetype
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
        public GetRestUserIdResponseDtoCategorytype3 categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype3
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

    public class GetRestUserIdResponseDtoValue
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        [JsonPropertyName("@modified")]
        public DateTime modified { get; set; }

        public string id { get; set; }
        public int orderIndex { get; set; }
    }

    public class GetRestUserIdResponseDtoRole
    {
        public string name { get; set; }
        public bool isCriticalSystemObject { get; set; }
    }

    public class GetRestUserIdResponseDtoPartypreference1
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        public string id { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public GetRestUserIdResponseDtoContenttype1 contentType { get; set; }
        public GetRestUserIdResponseDtoPreferredlocale1 preferredLocale { get; set; }
    }

    public class GetRestUserIdResponseDtoContenttype1
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
        public GetRestUserIdResponseDtoCategorytype4 categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype4
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

    public class GetRestUserIdResponseDtoPreferredlocale1
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
        public GetRestUserIdResponseDtoCategorytype5 categoryType { get; set; }
    }

    public class GetRestUserIdResponseDtoCategorytype5
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
