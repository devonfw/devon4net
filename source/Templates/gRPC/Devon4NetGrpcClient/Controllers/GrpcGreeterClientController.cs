using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Devon4Net.Application.GrpcService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.GrpcClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrpcGreeterClientController : ControllerBase
    {
        private GrpcChannel GrpcChannel { get; set; }

        public GrpcGreeterClientController(GrpcChannel grpcChannel)
        {
            GrpcChannel = grpcChannel;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HelloReply), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<HelloReply> Get(string name)
        {
            var client = new Greeter.GreeterClient(GrpcChannel);
            return client.SayHelloAsync(new HelloRequest { Name = name }).ResponseAsync;
        }
    }
}
