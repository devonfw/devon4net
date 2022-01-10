using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Dto;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;
using Devon4Net.Infrastructure.AWS.DynamoDb.Constants;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Entities;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers
{
    public class ComplexObjectFunctionEventHandler : ILambdaEventHandler<string, ObjectTest>
    {
        private IDynamoDbTableRepository DynamoDbEntityRepository { get; set; }
        public ComplexObjectFunctionEventHandler(IDynamoDbTableRepository dynamoDbRepository)
        {
            DynamoDbEntityRepository = dynamoDbRepository;
        }

        public async Task<ObjectTest> FunctionHandler(string input, ILambdaContext context)
        {
            var objectTest = new ObjectTest {
                Key = input,
                Values = new ObjectValues { MydateTime = DateTime.Now },
                EnableAws = true,
                UseSecrets = true,
                UseParameterStore = true
            };

            if (!await DynamoDbEntityRepository.TableExists("dynamic_object_storage").ConfigureAwait(false))
            {
                await DynamoDbEntityRepository.CreateTable("dynamic_object_storage",
                    new List<KeySchemaElement> { new KeySchemaElement { AttributeName = DynamoDbGeneralObjectStorageAttributes.AttributeKey, KeyType = KeyType.HASH } },
                    new List<AttributeDefinition> { new AttributeDefinition { AttributeName = DynamoDbGeneralObjectStorageAttributes.AttributeKey, AttributeType = ScalarAttributeType.S } }).ConfigureAwait(false);
            }

            await DynamoDbEntityRepository.Put("dynamic_object_storage", input, objectTest).ConfigureAwait(false);
            var criteria = new DynamoSearchCriteria();
            criteria.AddQueryCriteria(DynamoDbGeneralObjectStorageAttributes.AttributeKey, input, QueryOperator.Equal); // For query method
            var result = await DynamoDbEntityRepository.Get<ObjectTest>("dynamic_object_storage", criteria.GetCriteriaScanFilterForSearchCriteria()).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
    }
}
