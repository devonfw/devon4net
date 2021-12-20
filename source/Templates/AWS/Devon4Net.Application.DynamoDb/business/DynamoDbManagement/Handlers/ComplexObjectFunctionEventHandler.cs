using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Dto;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Entities;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using System;
using System.Threading.Tasks;

namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers
{
    public class ComplexObjectFunctionEventHandler : ILambdaEventHandler<string, ObjectTest>
    {
        private IDynamoDbRepository<DynamicObjectEntity> DynamoDbRepository { get; set; }
        public ComplexObjectFunctionEventHandler(IDynamoDbRepository<DynamicObjectEntity> dynamoDbRepository)
        {
            DynamoDbRepository = dynamoDbRepository;
        }

        public async Task<ObjectTest> FunctionHandler(string input, ILambdaContext context)
        {
            var objectTest = new ObjectTest {
                Values = new ObjectValues { MydateTime = DateTime.Now },
                EnableAws = true,
                UseSecrets = true,
                UseParameterStore = true
            };

            await DynamoDbRepository.Put("dynamic_object_storage", input, objectTest, true).ConfigureAwait(false);

            return await DynamoDbRepository.Get<ObjectTest>("dynamic_object_storage", input).ConfigureAwait(false);
        }
    }
}
