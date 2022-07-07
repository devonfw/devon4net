namespace Devon4Net.Infrastructure.JWT.Options
{
    public class Security
    {
        public string SecretKeyEncryptionAlgorithm { get; set; }
        public string SecretKey { get; set; }
        public string Certificate { get; set; }
        public string CertificatePassword { get; set; }
        public string CertificateEncryptionAlgorithm { get; set; }
        public string RefreshTokenEncryptionAlgorithm { get; set; }
    }
}