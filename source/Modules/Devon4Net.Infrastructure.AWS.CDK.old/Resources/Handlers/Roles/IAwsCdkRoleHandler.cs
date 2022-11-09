using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Roles
{
    public interface IAwsCdkRoleHandler
    {
        bool AddRolePolicyStatement(ref IRole role, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
        void AddRolePolicyStatement(ref RoleProps roleProperty, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
        IRole Create(string identification, IRoleProps roleProperties);
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, IManagedPolicy[] managedPolicies, Dictionary<string, PolicyDocument> inlinePolicies = null);
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, string policyName, string[] actions, string[] resources, IManagedPolicy[] managedPolicies = null, Effect effect = Effect.ALLOW, Dictionary<string, PolicyDocument> inlinePolicies = null); //NOSONAR number of params
        IRole LocateRoleByArn(string id, string arn, IFromRoleArnOptions fromRoleArnOptions = null);
        IRole LocateRoleByName(string id, string name, IFromRoleNameOptions fromRoleNameOptions = null);
    }
}