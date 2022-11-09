using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.Roles
{
    public interface IRolesHandlerManager
    {
        IRole AddRole(string identification, IRoleProps roleProperties);
        IRole LocateRoleByArn(string id, string arn, IFromRoleArnOptions fromRoleArnOptions = null);
        IRole LocateRoleByName(string id, string name, IFromRoleNameOptions fromRoleNameOptions = null);
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, string policyName, string[] actions, string[] resources, IManagedPolicy[] managedPolicies = null, Dictionary<string, PolicyDocument> inlinePolicies = null, Effect effect = Effect.ALLOW); //NOSONAR number of params
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, IManagedPolicy[] managedPolicies, Dictionary<string, PolicyDocument> inlinePolicies = null);
        void AddRolePolicyStatement(ref RoleProps roleProperty, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
        bool AddRolePolicyStatement(ref IRole role, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
    }
}