using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Amazon;
using Devon4Net.Infrastructure.AWS.Common.Constants;
using Devon4Net.Infrastructure.Common;

namespace Devon4Net.Infrastructure.AWS.Common.Helpers
{
    public class AwsCredentialsHelper
    {
        private AwsOptions AwsOptions { get; set; }
        public AwsCredentialsHelper(AwsOptions awsOptions) 
        {
            if (awsOptions == null)
            {
                throw new ArgumentNullException(nameof(awsOptions));
            }

            AwsOptions = awsOptions;
        }

        public AWSCredentials LoadAwsCredentials()
        {
            try
            {
                AWSCredentials credentials;
                if (!string.IsNullOrEmpty(AwsOptions?.Credentials?.AccessKeyId) && !string.IsNullOrEmpty(AwsOptions?.Credentials?.SecretAccessKey))
                {
                    credentials = new BasicAWSCredentials(AwsOptions?.Credentials?.AccessKeyId, AwsOptions?.Credentials?.SecretAccessKey);
                }
                else
                {
                    var profileName = string.IsNullOrWhiteSpace(AwsOptions?.Credentials?.Profile) ? Environment.GetEnvironmentVariable(AwsConstants.AWS_PROFILE) : AwsOptions.Credentials.Profile;
                    GetCredentialsFromProfileName(profileName, out credentials);
                }

                return credentials;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private static void GetCredentialsFromProfileName(string profileName, out AWSCredentials credentials)
        {
            CheckProfileName(profileName);

            var sharedFile = new SharedCredentialsFile();
            sharedFile.TryGetProfile(profileName, out var profile);
            AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedFile, out credentials);
        }

        private static void CheckProfileName(string profileName)
        {
            if (!string.IsNullOrEmpty(profileName)) return; 

            const string profileErrorMessage = "There is no defined AWS profile to be loaded";
            Devon4NetLogger.Error(profileErrorMessage);
            throw new ArgumentException(profileErrorMessage, nameof(profileName));
        }

        public RegionEndpoint LoadAwsRegionEndpoint()
        {
            try
            {
                var region = !string.IsNullOrEmpty(AwsOptions?.Credentials?.Region) ? AwsOptions.Credentials.Region : Environment.GetEnvironmentVariable(AwsConstants.AWS_REGION);
                return RegionEndpoint.GetBySystemName(region);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
