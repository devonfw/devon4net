using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkRoleHandler : AwsCdkDefaultHandler
    {
        private TagHandler TagHandler { get; }

        public AwsCdkRoleHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
            TagHandler = new TagHandler();
        }

        /// <summary>
        /// 
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

            var result = new Role(Scope, $"{ApplicationName}{EnvironmentName}{identification}", roleProperties);
            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{identification}", result);
            return result;  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assumedBy">Example: rds.amazonaws.com</param>
        /// <param name="policyName">Example: RdsS3AccessPolicy</param>
        /// <param name="actions">Example: new string[] { "s3:*" }</param>
        /// <param name="resources">Example: new string[] { "*" }</param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public IRoleProps CreateRoleProperties(string assumedBy, string policyName, string[] actions, string[] resources, Effect effect = Effect.ALLOW)
        {
            return new RoleProps
            {
                AssumedBy = new ServicePrincipal(assumedBy),
                InlinePolicies = new Dictionary<string, PolicyDocument>
                {
                    { $"{ApplicationName}{EnvironmentName}{policyName}",
                        new PolicyDocument(new PolicyDocumentProps
                        {
                            Statements = new PolicyStatement[]
                            {
                                new PolicyStatement(new PolicyStatementProps
                                {
                                    Effect = effect,
                                    Actions = actions,
                                    Resources = resources
                                })
                            }
                        })
                    }
                }
            };
        }
    }
}
