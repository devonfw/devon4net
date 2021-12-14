using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Domain.Tables
{
    [DynamoDBTable("dynamo_table")]
    public class DynamoTable
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("services")]
        public List<string> ServiceList{ get; set; }

        [DynamoDBIgnore]
        public string CoverPage { get; set; }
    }
}
