namespace Devon4Net.Infrastructure.Common.Options
{
    public class GrpcOptions
    {
        public bool EnableGrpc { get; set; }
        public bool UseDevCertificate { get; set; }
        public string GrpcServer { get; set; }
        public int MaxReceiveMessageSize { get; set; }
    }
}
