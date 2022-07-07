namespace Devon4Net.Infrastructure.Grpc.Options
{
    public class GrpcOptions
    {
        public bool EnableGrpc { get; set; }
        public bool UseDevCertificate { get; set; }
        public string GrpcServer { get; set; }
        public int MaxReceiveMessageSize { get; set; }
        public GrpcRetrypatternOptions RetryPatternOptions { get; set; }
    }

}
