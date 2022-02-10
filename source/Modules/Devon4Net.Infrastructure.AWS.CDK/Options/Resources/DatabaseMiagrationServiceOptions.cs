using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DatabaseMiagrationServiceOptions
    {
        public List<DmsReplicationSubnetGroupOptions> DmsReplicationSubnetGroups { get; set; }
        public List<DmsReplicationInstanceOptions> DmsReplicationInstances { get; set; }
        public List<DmsMigrationTaskOptions> DmsMigrationTasks { get; set; }

    }
}
