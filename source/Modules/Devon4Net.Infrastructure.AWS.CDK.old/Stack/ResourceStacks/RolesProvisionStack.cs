using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateRoles()
        {
            if (CdkOptions == null || CdkOptions.Roles?.Any() != true) return;

            foreach (var roleOption in CdkOptions.Roles)
            {
                IRole role;
                if (roleOption.LocateInsteadOfCreate)
                {
                    role = AwsCdkHandler.LocateRoleByArn(roleOption.Id, roleOption.RoleArn);
                }
                else
                {
                    role = CreateNewRole(roleOption);
                }
                StackResources.Roles.Add(roleOption.Id, role);
            }
        }

        private IRole CreateNewRole(RoleOptions roleOption)
        {
            if (roleOption.AwsPolicies?.Any() == true || roleOption.CustomPolicies?.Any() == true || roleOption.InlinePolicies?.Any() == true)
            {
                var policies = new List<IManagedPolicy>();
                var awsPolicies = roleOption.AwsPolicies?.Select(x => AwsCdkHandler.LocateAwsManagedPolicyByName(x)).ToList();
                var inlinePolicies = roleOption.InlinePolicies?.ToDictionary(policyId => policyId, policyId => LocatePolicyDocument(policyId, $"The PolicyDocument {policyId} of the role {roleOption.Name} was not found"));

                if (awsPolicies?.Any() == true)
                {
                    policies.AddRange(awsPolicies);
                }

                if (roleOption.CustomPolicies?.Any() == true)
                {
                    IManagedPolicy managedPolicy;
                    try
                    {
                        foreach (var managedPolicyOption in roleOption.CustomPolicies)
                        {
                            managedPolicy = LocateManagedPolicy(managedPolicyOption, "Could not locate managed policy");
                            policies.Add(managedPolicy);
                        }
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Cannot add the managed policy");
                    }
                }

                return AwsCdkHandler.AddRole(roleOption.Name, AwsCdkHandler.CreateRoleProperties(roleOption.Name, roleOption.AssumedBy.ToArray(), policies.ToArray(), inlinePolicies));
            }
            else if (roleOption.AwsActions?.Any() == true)
            {
                return AwsCdkHandler.AddRole(roleOption.Name, AwsCdkHandler.CreateRoleProperties(roleOption.Name, roleOption.AssumedBy.ToArray(), roleOption.Name, roleOption.AwsActions.ToArray(), new string[] { "*" }));
            }
            else
            {
                throw new ArgumentException($"The role {roleOption.Id} must have either a list of AwsPolicies, a list of CustomPolicies or a list of AwsActions");
            }
        }

        private IRole LocateRole(string roleId, string exceptionMessageIfRoleDoesNotExist, string exceptionMessageIfRoleIsEmpty = null)
        {
            return StackResources.Locate<IRole>(roleId, exceptionMessageIfRoleDoesNotExist, exceptionMessageIfRoleIsEmpty);
        }
    }
}
