namespace Devon4Net.Infrastructure.Common.Options.JWT
{
    public class Security
    {
        public string SecretKeyEncryptionAlgorithm { get; set; }
        public string SecretKeyLengthAlgorithm { get; set; }
        public string SecretKey { get; set; }
        public string Certificate { get; set; }
        public string CertificatePassword { get; set; }
        public string CertificateEncryptionAlgorithm { get; set; }
    }
}