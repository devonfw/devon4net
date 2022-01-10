using Amazon.DynamoDBv2.DataModel;
using System;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Entities
{
    [DynamoDBTable("dynamic_object_storage")]
    public class DynamicObjectEntity
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("key")]
        public string Key { get; set; }

        [DynamoDBProperty("value")]
        public string Value { get; set; }

        [DynamoDBProperty("type")]
        public Type ObjectType { get; set; }
    }
}
