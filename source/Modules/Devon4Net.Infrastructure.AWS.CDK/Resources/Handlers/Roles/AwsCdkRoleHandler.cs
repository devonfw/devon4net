using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.CDK.AWS.IAM;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers;

namespace ADC.PostNL.BuildingBlocks.AWSCDK.Handlers
{
    public class AwsCdkRoleHandler : AwsCdkBaseHandler, IAwsCdkRoleHandler
    {
        private TagHandler TagHandler { get; }

        public AwsCdkRoleHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
            TagHandler = new TagHandler();
        }

        /// <summary>
        /// Creates a role based on custom role properties
        /// </summary>
        /// <param name="identification">Example: RdsS3AccessRole</param>
        /// <param name="roleProperties"></param>
        /// <returns></returns>
        public IRole Create(string identification, IRoleProps roleProperties)
        {
            if (string.IsNullOrEmpty(identification))
            {
                throw new ArgumentException("The identification can not be null");
            }

            var result = new Role(Scope, identification, roleProperties);
            TagHandler.LogTag(identification, result);
            return result;
        }

        /// <summary>
        /// Returns a role in AWS by its ARN
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arn"></param>
        /// <param name="fromRoleArnOptions"></param>
        /// <returns></returns>
        public IRole LocateRoleByArn(string id, string arn, IFromRoleArnOptions fromRoleArnOptions = null)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(arn))
            {
                throw new ArgumentException("The identification or arn cannot be null");
            }
            return Role.FromRoleArn(Scope, id, arn, fromRoleArnOptions);
        }

        /// <summary>
        /// Creates the role properties to be used
        /// </summary>
        /// <param name="assumedBy">Example: rds.amazonaws.com</param>
        /// <param name="policyName">Example: RdsS3AccessPolicy</param>
        /// <param name="actions">Example: new string[] { "s3:*" }</param>
        /// <param name="resources">Example: new string[] { "*" }</param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, string policyName, string[] actions, string[] resources, IManagedPolicy[] managedPolicies = null, Effect effect = Effect.ALLOW, Dictionary<string, PolicyDocument> inlinePolicies = null)
        {
            var principals = assumedBy.Select(x => new ServicePrincipal(x)).ToArray();

            var result = new RoleProps
            {
                AssumedBy = new CompositePrincipal(principals),
                ManagedPolicies = managedPolicies,
                InlinePolicies = inlinePolicies ?? new Dictionary<string, PolicyDocument>(),
                RoleName = roleName
            };

            AddRolePolicyStatement(ref result, policyName, actions, resources, effect);

            return result;
        }

        /// <summary>
        /// Creates the role properties to be used
        /// </summary>
        /// <param name="assumedBy">Example: rds.amazonaws.com</param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public IRoleProps CreateRoleProperties(string roleName, string[] assumedBy, IManagedPolicy[] managedPolicies, Dictionary<string, PolicyDocument> inlinePolicies = null)
        {
            var principals = assumedBy.Select(x => new ServicePrincipal(x)).ToArray();

            var result = new RoleProps
            {
                AssumedBy = new CompositePrincipal(principals),
                ManagedPolicies = managedPolicies,
                InlinePolicies = inlinePolicies ?? new Dictionary<string, PolicyDocument>(),
                RoleName = roleName
            };

            return result;
        }

        public void AddRolePolicyStatement(ref RoleProps roleProperty, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW)
        {
            if (actions != null && actions.Any())
            {
                roleProperty.InlinePolicies.Add(policyName, new PolicyDocument(new PolicyDocumentProps
                {
                    Statements = new PolicyStatement[]
                    {
                        new PolicyStatement(
                        new PolicyStatementProps()
                        {
                            Effect = effect,
                            Actions = actions,
                            Resources = resources
                        })
                    }
                }));
            }
        }

        /// <summary>
        /// Returns true if the statement has been added
        /// </summary>
        /// <param name="role"></param>
        /// <param name="actions"></param>
        /// <param name="resources"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public bool AddRolePolicyStatement(ref IRole role, string[] actions, string[] resources, Effect effect = Effect.ALLOW)
        {
            return role.AddToPrincipalPolicy(
                new PolicyStatement(
                new PolicyStatementProps()
                {
                    Effect = effect,
                    Actions = actions,
                    Resources = resources
                })
            ).StatementAdded;
        }

        public IManagedPolicy LocateAwsManagedPolicyByName(string policyName)
        {
            return ManagedPolicy.FromAwsManagedPolicyName(policyName);
        }

        public IManagedPolicy LocateManagedPolicyByName(string policyName)
        {
            return ManagedPolicy.FromManagedPolicyName(Scope, policyName, policyName);
        }
    }
}
