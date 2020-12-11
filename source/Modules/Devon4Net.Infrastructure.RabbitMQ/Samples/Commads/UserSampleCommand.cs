using Devon4Net.Infrastructure.RabbitMQ.Commands;

namespace Devon4Net.Infrastructure.RabbitMQ.Samples.Commads
{
    public class UserSampleCommand : Command
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}