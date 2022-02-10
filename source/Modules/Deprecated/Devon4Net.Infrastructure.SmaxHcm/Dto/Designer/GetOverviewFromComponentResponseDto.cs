using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
{
    public class GetOverviewFromComponentResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }

        [JsonPropertyName("@created")]
        public long created { get; set; }

        [JsonPropertyName("@modified")]
        public long modified { get; set; }

        public string global_id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public bool isConsumerVisible { get; set; }
        public bool isPattern { get; set; }
        public bool isSystemCriticalObject { get; set; }
        public bool isUpgradeLocked { get; set; }
        public int processingOrder { get; set; }
        public GetOverviewFromComponentResponseDtoBlueprint blueprint { get; set; }
        public GetOverviewFromComponentResponseDtoBasecomponenttype baseComponentType { get; set; }
        public GetOverviewFromComponentResponseDtoConfigurationItemType configuration_item_type { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoBlueprint
    {
        [JsonPropertyName("@self")]
        public object self { get; set; }

        [JsonPropertyName("@created")]
        public long created { get; set; }

        [JsonPropertyName("@modified")]
        public long modified { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public bool published { get; set; }
        public bool upgradable { get; set; }
        public object[] upgradesFrom { get; set; }
        public object[] upgradesTo { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoBasecomponenttype
    {
        [JsonPropertyName("@type")]
        public GetOverviewFromComponentResponseDtoType type { get; set; }

        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public GetOverviewFromComponentResponseDtoDerivedfrom derivedFrom { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoType
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoDerivedfrom
    {
        [JsonPropertyName("@type")]
        public GetOverviewFromComponentResponseDtoType1 type { get; set; }

        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public GetOverviewFromComponentResponseDtoDerivedfrom1 derivedFrom { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoType1
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoDerivedfrom1
    {
        [JsonPropertyName("@type")]
        public GetOverviewFromComponentResponseDtoType2 type { get; set; }

        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public GetOverviewFromComponentResponseDtoDerivedfrom2 derivedFrom { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoType2
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoDerivedfrom2
    {
        [JsonPropertyName("@type")]
        public GetOverviewFromComponentResponseDtoType3 type { get; set; }

        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public object[] tags { get; set; }
        public bool disabled { get; set; }
        public bool isOutOfSync { get; set; }
        public bool validUpgrade { get; set; }
        public object[] upgradeRelationsFrom { get; set; }
        public object[] upgradeRelationsTo { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoType3
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoConfigurationItemType
    {
        [JsonPropertyName("@created")]
        public DateTime created { get; set; }

        [JsonPropertyName("@modified")]
        public DateTime modified { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string objectId { get; set; }
        public bool disabled { get; set; }
        public GetOverviewFromComponentResponseDtoCategorytype categoryType { get; set; }
    }

    public class GetOverviewFromComponentResponseDtoCategorytype
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool isCriticalSystemObject { get; set; }
        public string objectId { get; set; }
        public bool extensible { get; set; }
        public object[] categories { get; set; }
    }

}
