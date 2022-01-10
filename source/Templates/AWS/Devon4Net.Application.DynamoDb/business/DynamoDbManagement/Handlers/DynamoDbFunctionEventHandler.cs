using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Domain.Tables;
using Devon4Net.Infrastructure.AWS.DynamoDb.Constants;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers
{
    public class DynamoDbFunctionEventHandler : ILambdaEventHandler<string, DynamoTable>
    {
        private IDynamoDbEntityRepository<DynamoTable> DynamoDbRepository { get; set; }
        public DynamoDbFunctionEventHandler(IDynamoDbEntityRepository<DynamoTable> dynamoDbRepository)
        {
            DynamoDbRepository = dynamoDbRepository;
        }

        public async Task<DynamoTable> FunctionHandler(string input, ILambdaContext context)
        {

            if (!await DynamoDbRepository.TableExists("dynamo_table").ConfigureAwait(false))
            {
                await DynamoDbRepository.CreateTable("dynamo_table",
                    new List<KeySchemaElement> { new KeySchemaElement { AttributeName = DynamoDbGeneralObjectStorageAttributes.AttributeKey, KeyType = KeyType.HASH } },
                    new List<AttributeDefinition> { new AttributeDefinition { AttributeName = DynamoDbGeneralObjectStorageAttributes.AttributeKey, AttributeType = ScalarAttributeType.S } }).ConfigureAwait(false);
            }

            await DynamoDbRepository.Create(new DynamoTable { Key = input, CoverPage = $"Test cover page for {input}", ServiceList = new List<string> { $"Service for {input}"} }).ConfigureAwait(false);

            return await DynamoDbRepository.GetById(input).ConfigureAwait(false);
        }
    }
}
