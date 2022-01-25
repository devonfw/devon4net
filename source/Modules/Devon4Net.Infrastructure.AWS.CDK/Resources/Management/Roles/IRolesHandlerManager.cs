using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IRolesHandlerManager
    {
        IRole AddRole(string identification, IRoleProps roleProperties);
        IRole LocateRoleByArn(string id, string arn, IFromRoleArnOptions fromRoleArnOptions = null);
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, string policyName, string[] actions, string[] resources, IManagedPolicy[] managedPolicies = null, Dictionary<string, PolicyDocument> inlinePolicies = null, Effect effect = Effect.ALLOW);
        IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, IManagedPolicy[] managedPolicies, Dictionary<string, PolicyDocument> inlinePolicies = null);
        void AddRolePolicyStatement(ref RoleProps roleProperty, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
        bool AddRolePolicyStatement(ref IRole role, string[] actions, string[] resources, Effect effect = Effect.ALLOW);
        IManagedPolicy LocateAwsManagedPolicyByName(string policyName);
        IManagedPolicy LocateManagedPolicyByName(string policyName);
    }
}