using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Amazon.CDK.AWS.IAM;
using System.Linq;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using System;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateCodeBuildProjects()
        {
            if (CdkOptions == null || CdkOptions.CodeBuildProjects?.Any() != true) return;

            foreach (var codeBuildOption in CdkOptions.CodeBuildProjects)
            {
                GetCodeBuildProjectResources(codeBuildOption, out var role, out var enableIndependentTrigger);

                var dictObject = DeserializeToDictionary(File.ReadAllText(codeBuildOption.BuildSpecPath));

                var codeBuildProject = AwsCdkHandler.CreateCodeBuildProject(codeBuildOption.ProjectName, codeBuildOption.ProjectName, role, dictObject, enableIndependentTrigger: enableIndependentTrigger);

                StackResources.CodeBuildProjects.Add(codeBuildOption.Id, codeBuildProject);
            }
        }

        private void GetCodeBuildProjectResources(CodeBuildOptions codeBuildOption, out IRole role, out bool enableIndependentTrigger)
        {
            // Locate role
            role = LocateRole(codeBuildOption.Role, $"The role {codeBuildOption.Role} of the code build project {codeBuildOption.ProjectName} was not found");

            // Parse bool to trigger codebuild project without a pipeline
            enableIndependentTrigger = codeBuildOption.EnableIndependentTrigger ?? false;
        }

        private Dictionary<string, object> DeserializeToDictionary(string jo)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                if (d.Value is JObject)
                {
                    values2.Add(d.Key, DeserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }

        private void GetCodeBuildActionResources(PipelineActionCodeBuildOptions actionCodeBuildOptions, IDictionary<string, Artifact_> artifacts, out Artifact_ inputArtifact, out Artifact_ outputArtifact, out IProject project)
        {
            // Locate artifact
            if (string.IsNullOrWhiteSpace(actionCodeBuildOptions.InputArtifact))
            {
                throw new ArgumentException($"There is no input artifact in the pipeline action {actionCodeBuildOptions.Name}");
            }
            else
            {
                if (!artifacts.TryGetValue(actionCodeBuildOptions.InputArtifact, out inputArtifact))
                {
                    throw new ArgumentException($"The artifact {actionCodeBuildOptions.InputArtifact} of the pipeline action {actionCodeBuildOptions.Name} was not found");
                }
            }

            if (!string.IsNullOrEmpty(actionCodeBuildOptions.OutputArtifact))
            {
                if (artifacts.ContainsKey(actionCodeBuildOptions.OutputArtifact))
                {
                    throw new ArgumentException($"The artifact {actionCodeBuildOptions.OutputArtifact} of the pipeline action {actionCodeBuildOptions.Name} already exists");
                }
                else
                {
                    outputArtifact = new Artifact_(actionCodeBuildOptions.OutputArtifact);
                    artifacts.Add(actionCodeBuildOptions.OutputArtifact, outputArtifact);
                }
            }
            else
            {
                // We don't have an output artifact specified
                outputArtifact = null;
            }
            // Locate CodeBuild project
            project = LocateCodeBuildProject(actionCodeBuildOptions.CodeBuildProject, $"The CodeBuild project {actionCodeBuildOptions.CodeBuildProject} of the pipeline action {actionCodeBuildOptions.Name} was not found");
        }

        private IProject LocateCodeBuildProject(string codeBuildProjectId, string exceptionMessageIfCodeBuildProjectDoesNotExist, string exceptionMessageIfCodeBuildProjectIsEmpty = null)
        {
            return StackResources.Locate<IProject>(codeBuildProjectId, exceptionMessageIfCodeBuildProjectDoesNotExist, exceptionMessageIfCodeBuildProjectIsEmpty);
        }
    }
}
