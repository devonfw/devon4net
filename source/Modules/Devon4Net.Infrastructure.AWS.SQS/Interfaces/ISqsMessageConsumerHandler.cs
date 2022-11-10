using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.SQS.Interfaces
{
    public interface ISqsMessageConsumerHandler<T>
    {
        Task Start();
        void Stop();
    }
}