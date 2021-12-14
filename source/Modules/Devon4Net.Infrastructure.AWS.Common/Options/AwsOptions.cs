namespace Devon4Net.Infrastructure.AWS.Common.Options
{
    public class AwsOptions
    {
        public bool EnableAws { get; set; }
        public bool UseSecrets { get; set; }
        public bool UseParameterStore { get; set; }
        public Credentials Credentials { get; set; }
    }
}
