using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Domain.Tables;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using System.Threading.Tasks;

namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers
{
    public class DynamoDbFunctionEventHandler : ILambdaEventHandler<string, DynamoTable>
    {
        private IDynamoDbRepository<DynamoTable> DynamoDbRepository { get; set; }
        public DynamoDbFunctionEventHandler(IDynamoDbRepository<DynamoTable> dynamoDbRepository)
        {
            DynamoDbRepository = dynamoDbRepository;
        }

        public Task<DynamoTable> FunctionHandler(string input, ILambdaContext context)
        {
            return DynamoDbRepository.GetById(input);
        }
    }
}
