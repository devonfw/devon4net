using Amazon.CDK.AWS.RDS;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateSecrets()
        {
            GenerateDatabaseSecrets();

            if (CdkOptions == null || CdkOptions.Secrets?.Any() != true) return;

            foreach (var secret in CdkOptions.Secrets)
            {
                StackResources.Secrets.Add(secret.Id, AwsCdkHandler.CreateSecret(secret.Key, secret.Value));
            }
        }

        private void GenerateDatabaseSecrets()
        {
            if (CdkOptions.Databases?.Any() != true) return;
            foreach (var databaseOption in from databaseOption in CdkOptions.Databases
                                           where string.IsNullOrWhiteSpace(databaseOption.Password)
                                           select databaseOption)
            {
                MangeDatabaseAndSecrets(databaseOption);
            }
        }

        private void MangeDatabaseAndSecrets(DatabaseOptions databaseOption)
        {
            var database = LocateDatabase(databaseOption.Id, $"The database with id {databaseOption.Id} was not created");

            foreach (var databaseSecretKeyValuePair in databaseOption.Secrets)
            {
                AddSecret(databaseOption, database, databaseSecretKeyValuePair);
            }
        }

        private void AddSecret(DatabaseOptions databaseOption, IDatabaseInstance database, KeyValuePair<string, string> databaseSecretKeyValuePair)
        {
            var databasePropertyValue = GetDatabaseProperty(databaseOption, database, databaseSecretKeyValuePair.Key);

            if (string.IsNullOrWhiteSpace(databasePropertyValue)) return;
            var secret = AwsCdkHandler.CreateSecret(databaseSecretKeyValuePair.Value, databasePropertyValue);
            StackResources.Secrets.Add(databaseSecretKeyValuePair.Value, secret);
        }
    }
}
