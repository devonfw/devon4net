using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public interface IDynamoDbBaseRepository
    {
        Task<CreateTableResponse> CreateTable(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributes, long readCapacityUnits = 5, long writeCapacityUnits = 5, bool streamEnabled = true, StreamViewType streamViewType = null);
        Task<bool> TableExists(string tableName);
    }
}