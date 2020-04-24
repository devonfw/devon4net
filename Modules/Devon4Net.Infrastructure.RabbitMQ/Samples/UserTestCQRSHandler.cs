using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Handlers;
using EasyNetQ;

namespace Devon4Net.Infrastructure.RabbitMQ.Samples
{

    public class UserTestCommand : Command
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }

    public class UserTestCQRSHandler : RabbitMqHandler<UserTestCommand>
    {
        public UserTestCQRSHandler(IBus serviceBus) : base(serviceBus)
        {
        }

        /// <summary>
        /// Sample class. Put  your async methods in logic
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
#pragma warning disable 1998
        public override async Task HandleCommand(UserTestCommand command)
#pragma warning restore 1998
        {
            Devon4NetLogger.Debug($"Handled!! {command.Name} {command.SurName}");
        }
    }
}
