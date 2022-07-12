using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IRolesHandlerManager
    {
        public IRole AddRole(string identification, IRoleProps roleProperties)
        {
            return HandlerResources.AwsCdkRoleHandler.Create(identification, roleProperties);
        }

        public IRole LocateRoleByArn(string id, string arn, IFromRoleArnOptions fromRoleArnOptions = null)
        {
            return HandlerResources.AwsCdkRoleHandler.LocateRoleByArn(id, arn, fromRoleArnOptions);
        }

        public IRole LocateRoleByName(string id, string name, IFromRoleNameOptions fromRoleNameOptions = null)
        {
            return HandlerResources.AwsCdkRoleHandler.LocateRoleByName(id, name, fromRoleNameOptions);
        }

        public IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, string policyName, string[] actions, string[] resources, IManagedPolicy[] managedPolicies = null, Dictionary<string, PolicyDocument> inlinePolicies = null, Effect effect = Effect.ALLOW)
        {
            return HandlerResources.AwsCdkRoleHandler.CreateRoleProperties(roleName, assumedBy, policyName, actions, resources, managedPolicies, effect, inlinePolicies);
        }

        public IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, IManagedPolicy[] managedPolicies, Dictionary<string, PolicyDocument> inlinePolicies = null)
        {
            return HandlerResources.AwsCdkRoleHandler.CreateRoleProperties(roleName, assumedBy, managedPolicies, inlinePolicies);
        }

        public void AddRolePolicyStatement(ref RoleProps roleProperty, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW)
        {
            HandlerResources.AwsCdkRoleHandler.AddRolePolicyStatement(ref roleProperty, policyName, actions, resources, effect);
        }

        public bool AddRolePolicyStatement(ref IRole role, string[] actions, string[] resources, Effect effect = Effect.ALLOW)
        {
            return HandlerResources.AwsCdkRoleHandler.AddRolePolicyStatement(ref role, actions, resources, effect);
        }
    }
}
