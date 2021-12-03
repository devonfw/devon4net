using Devon4Net.Application.GrpcServer.Protos;
using Devon4Net.Infrastructure.Grpc.Attributes;
using Grpc.Core;

namespace Devon4Net.Application.GrpcService
{
    /// <summary>
    /// GreeterService with protobuf
    /// </summary>
    [GrpcDevonServiceAttribute]
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// SayHello to user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Saying hello to user!");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
