namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer
{
    public class CreateDesignContainerResponseDto
    {
        public string self { get; set; }
        public string type { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public CreateDesignContainerResponseDtoExt ext { get; set; }
        public string container_type { get; set; }
        public CreateDesignContainerResponseDtoTag[] tags { get; set; }
    }

    public class CreateDesignContainerResponseDtoExt
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
        public string csa_artifact_container_type { get; set; }
    }

    public class CreateDesignContainerResponseDtoTag
    {
        public string self { get; set; }
        public string type { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public object icon { get; set; }
        public object color { get; set; }
        public object scopes { get; set; }
    }

}
