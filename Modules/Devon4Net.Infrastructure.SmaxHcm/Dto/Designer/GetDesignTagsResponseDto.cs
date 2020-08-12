namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
{
    public class GetDesignTagsResponseDto
    {
        public string type { get; set; }
        public string self { get; set; }
        public int total_results { get; set; }
        public int start_index { get; set; }
        public int items_per_page { get; set; }
        public GetDesignTagsResponseDto_Member[] members { get; set; }
    }

    public class GetDesignTagsResponseDto_Member
    {
        public string self { get; set; }
        public string type { get; set; }
        public int content_version { get; set; }
        public string name { get; set; }
        public GetDesignTagsResponseDto_Ext ext { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public string[] scopes { get; set; }
    }

    public class GetDesignTagsResponseDto_Ext
    {
        public string csa_name_key { get; set; }
    }
}
