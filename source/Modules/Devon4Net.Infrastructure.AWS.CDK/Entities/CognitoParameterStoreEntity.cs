using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class CognitoParameterStoreEntity
    {
        public List<UserPool> UserPools { get; set; }
    }

    public class UserPool
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AppClient> AppClients { get; set; }
    }

    public class AppClient
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
