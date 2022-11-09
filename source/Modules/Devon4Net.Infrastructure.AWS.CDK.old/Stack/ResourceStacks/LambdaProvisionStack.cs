using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateLambdas()
        {
            if (CdkOptions == null || CdkOptions.Lambdas?.Any() != true) return;

            foreach (var lambda in CdkOptions.Lambdas)
            {
                CheckLambdaParams(lambda);

                GetLambdaResources(lambda, out var role, out var securityGroup, out var vpc, out var subnets, out var bucket, out var environmentVariables);

                var runtime = new Runtime(lambda.Runtime);
                IFunctionProps lambdaProperties;

                if (bucket == null)
                {
                    lambdaProperties = AwsCdkHandler.CreateLambdaProperties(lambda.SourceCode.CodeZipFilePath, lambda.FunctionHandler, role, runtime, lambda.FunctionName, vpc, securityGroup, subnets, environmentVariables: environmentVariables);
                }
                else
                {
                    lambdaProperties = AwsCdkHandler.CreateLambdaProperties(bucket, lambda.SourceCode.CodeBucket.FilePath, lambda.FunctionHandler, role, runtime, lambda.FunctionName, vpc, securityGroup, subnets, environmentVariables: environmentVariables);
                }

                var lambdaFunction = AwsCdkHandler.AddLambda(lambda.FunctionName, lambdaProperties);

                StackResources.Lambdas.Add(lambda.Id, lambdaFunction);
            }
        }

        private void CheckLambdaParams(LambdaOptions lambda)
        {
            if (string.IsNullOrWhiteSpace(lambda.Id))
            {
                throw new ArgumentException($"All lambdas must have a {nameof(lambda.Id)}");
            }

            if (string.IsNullOrWhiteSpace(lambda.FunctionName))
            {
                throw new ArgumentException($"All lambdas must have a {nameof(lambda.FunctionName)}");
            }

            if (string.IsNullOrWhiteSpace(lambda.Role))
            {
                throw new ArgumentException($"The lambda {lambda.FunctionName} must have a role");
            }

            if (string.IsNullOrWhiteSpace(lambda.FunctionHandler))
            {
                throw new ArgumentException($"The lambda {lambda.FunctionName} must have a handler");
            }

            if (string.IsNullOrWhiteSpace(lambda.Runtime))
            {
                throw new ArgumentException($"The lambda {lambda.FunctionName} must have a runtime");
            }

            CheckLambdaSourceCodeParams(lambda);
        }

        private void CheckLambdaSourceCodeParams(LambdaOptions lambda)
        {
            if (lambda.SourceCode == null)
            {
                throw new ArgumentException($"The source code options of the lambda {lambda.FunctionName} can not be null");
            }

            if (string.IsNullOrWhiteSpace(lambda.SourceCode.CodeZipFilePath)
                && (lambda.SourceCode.CodeBucket == null
                || (string.IsNullOrWhiteSpace(lambda.SourceCode.CodeBucket.BucketName) && string.IsNullOrWhiteSpace(lambda.SourceCode.CodeBucket.FilePath))))
            {
                throw new ArgumentException($"There must be at least one source code option in the code options of the lambda {lambda.SourceCode}");
            }

            if (!string.IsNullOrWhiteSpace(lambda.SourceCode.CodeZipFilePath)
                && lambda.SourceCode.CodeBucket != null
                && !string.IsNullOrWhiteSpace(lambda.SourceCode.CodeBucket.BucketName)
                && !string.IsNullOrWhiteSpace(lambda.SourceCode.CodeBucket.FilePath))
            {
                throw new ArgumentException($"There can not be two source code options in the code options of the lambda {lambda.SourceCode}");
            }
        }

        private void GetLambdaResources(LambdaOptions lambda, out IRole role, out ISecurityGroup securityGroup, out IVpc vpc, out ISubnet[] subnets, out IBucket bucket, out Dictionary<string, string> environmentVariables)
        {
            // Locate role
            role = LocateRole(lambda.Role, $"The role {lambda.Role} of the lambda {lambda.FunctionName} was not found");

            // Locate security group
            securityGroup = LocateSecurityGroup(lambda.SecurityGroupId, $"The security group {lambda.SecurityGroupId} of the lambda {lambda.FunctionName} was not found");

            // Locate vpc
            vpc = LocateVpc(lambda.VpcId, $"The VPC {lambda.VpcId} of the lambda {lambda.FunctionName} was not found");

            // Locate subnets
            if (lambda.SubnetIds?.Any() != true)
            {
                subnets = null;
            }
            else
            {
                var subnetList = new List<ISubnet>(lambda.SubnetIds.Length);
                foreach (var subnetId in lambda.SubnetIds)
                {
                    subnetList.Add(LocateSubnet(subnetId,
                        $"The subnet {subnetId} of the lambda {lambda.FunctionName} was not found",
                        $"The subnet ids of the lambda {lambda.FunctionName} can not be empty"));
                }
                subnets = subnetList.ToArray();
            }

            // Locate bucket
            bucket = LocateBucket(lambda.SourceCode?.CodeBucket?.BucketName, $"The bucket {lambda.SourceCode.CodeBucket.BucketName} of the lambda {lambda.FunctionName} was not found");

            if (lambda.LambdaEnvironmentVariables?.Any() == true)
            {
                environmentVariables = new Dictionary<string, string>();
                foreach (var environmentVariable in lambda.LambdaEnvironmentVariables)
                {
                    environmentVariables.Add(environmentVariable.EnvironmentVariableName, environmentVariable.EnvironmentVariableValue);
                }
            }
            else
            {
                environmentVariables = null;
            }
        }

        private IFunction LocateLambda(string lambdaId, string exceptionMessageIfLambdaDoesNotExist, string exceptionMessageIfLambdaIsEmpty = null)
        {
            return StackResources.Locate<IFunction>(lambdaId, exceptionMessageIfLambdaDoesNotExist, exceptionMessageIfLambdaIsEmpty);
        }
    }
}
