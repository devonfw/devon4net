using Devon4Net.Application.GrpcClient.Protos;
using Devon4Net.Infrastructure.Logger.Logging;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.GrpcClient.Controllers
{
    /// <summary>
    /// GrpcGreeterClientController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GrpcGreeterClientController : ControllerBase
    {
        private GrpcChannel GrpcChannel { get; }

        /// <summary>
        /// GrpcGreeterClientController
        /// </summary>
        /// <param name="grpcChannel"></param>
        public GrpcGreeterClientController(GrpcChannel grpcChannel)
        {
            GrpcChannel = grpcChannel;
        }

        /// <summary>
        /// Says hello
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(HelloReply), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<HelloReply> Get(string name)
        {
            try
            {
                var client = new Greeter.GreeterClient(GrpcChannel);
                return await client.SayHelloAsync(new HelloRequest { Name = name }).ResponseAsync.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
