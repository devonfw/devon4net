using Amazon.CDK.AWS.ECS;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcsTaskDefinitionOptions
    {
        public string Id { get; set; }
        public string Arn {get;set;}
        public string Family {get;set;}
        public string RoleId { get; set; }
        public Compatibility Compatibility { get; set; }
        public bool LocateInsteadCreate { get; set; }
        public List<EcsContainerDefinitionOptions> Containers { get; set; }
        public List<EcsDockerVolumeOptions> Volumes { get; set; }
        public List<EcsDockerMountPointOptions> MountPoints { get; set; }
    }
}
