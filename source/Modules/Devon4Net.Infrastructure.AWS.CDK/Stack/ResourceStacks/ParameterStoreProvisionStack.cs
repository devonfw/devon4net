using Amazon.CDK.AWS.SSM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateSsmParameters()
        {
            GenerateDatabaseSsmParameters();

            if (CdkOptions.SsmParameters?.Any() != true) return;

            foreach (var parameter in CdkOptions.SsmParameters)
            {
                if (parameter.LocateInsteadOfCreate)
                {
                    StackResources.SsmParameters.Add(parameter.Id, AwsCdkHandler.LocateParameterFromName(parameter.Id, parameter.Name));
                }
                else
                {
                    GetSsmParametersResources(parameter, out var parameterType, out var parameterTier);

                    StackResources.SsmParameters.Add(parameter.Id, AwsCdkHandler.AddParameter(parameter.Id, parameter.Name, parameter.Value, parameterType, parameterTier, parameter.Description));
                }
            }
        }

        private void GetSsmParametersResources(SsmParameterOptions ssmParameterOptions, out ParameterType parameterType, out ParameterTier parameterTier)
        {
            parameterType = ssmParameterOptions.IsStringList ? ParameterType.STRING_LIST : ParameterType.STRING;
            parameterTier = ssmParameterOptions.IsAdvancedTier ? ParameterTier.ADVANCED : ParameterTier.STANDARD;
        }

        private void GenerateDatabaseSsmParameters()
        {
            if (CdkOptions.Databases?.Any() != true) return;

            foreach (var databaseOption in CdkOptions.Databases)
            {
                if (databaseOption.SsmParameters == null) continue;

                var database = LocateDatabase(databaseOption.Id, $"The database with id {databaseOption.Id} was not created");

                foreach (var databaseSsmParameterKeyValuePair in databaseOption.SsmParameters)
                {
                    var databasePropertyValue = GetDatabaseProperty(databaseOption, database, databaseSsmParameterKeyValuePair.Key);

                    if (!string.IsNullOrWhiteSpace(databasePropertyValue))
                    {
                        StackResources.SsmParameters.Add(databaseSsmParameterKeyValuePair.Value, AwsCdkHandler.AddParameter(databaseSsmParameterKeyValuePair.Value, databaseSsmParameterKeyValuePair.Value, databasePropertyValue));
                    }
                }
            }
        }
    }
}
