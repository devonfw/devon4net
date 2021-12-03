using Grpc.Net.Client;

namespace Devon4Net.Infrastructure.Grpc.Handlers
{
    public class GrpcHandler
    {
        private GrpcChannel GrpcChannel { get; set; }

        public GrpcHandler(GrpcChannel grpcChannel)
        {
            if (grpcChannel == null)
            {
                throw new ArgumentException("Please set up properly the gRPC component for devon");
            }

            GrpcChannel = grpcChannel;
        }
    }
}
