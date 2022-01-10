using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using Amazon.Runtime;
using Amazon;
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AWS.DynamoDb.Extensions;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public class DynamoDbBaseRepository : IDynamoDbBaseRepository
    {
        protected IDynamoDBContext DynamoDBContext { get; }
        protected ILogger Logger { get; }
        protected ILambdaLogger LambdaLogger { get; }
        protected AmazonDynamoDBClient AmazonDynamoDBClient { get; }
        protected JsonHelper JsonHelper { get; }
        protected const string AttributeKey = "key";
        protected const string AttributeType = "type";
        protected const string AttributeValue = "value";

        public DynamoDbBaseRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, JsonHelper jsonHelper = null)
        {
            Logger = null;
            LambdaLogger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbBaseRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILogger logger, JsonHelper jsonHelper = null)
        {
            Logger = logger;
            LambdaLogger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbBaseRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILambdaLogger logger, JsonHelper jsonHelper = null)
        {
            LambdaLogger = logger;
            Logger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbBaseRepository(AmazonDynamoDBClient dynamoDBClient, ILogger logger, JsonHelper jsonHelper = null)
        {
            Logger = logger;
            LambdaLogger = null;
            DynamoDBContext = new DynamoDBContext(dynamoDBClient);
            AmazonDynamoDBClient = dynamoDBClient;
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbBaseRepository(AmazonDynamoDBClient dynamoDBClient, ILambdaLogger lambdaLogger, JsonHelper jsonHelper = null)
        {
            LambdaLogger = lambdaLogger;
            Logger = null;
            DynamoDBContext = new DynamoDBContext(dynamoDBClient);
            AmazonDynamoDBClient = dynamoDBClient;
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        protected void LogDynamoException(ref Exception exception)
        {
            var message = exception?.Message;
            var innerException = exception?.InnerException;
            Logger?.LogError("Error performing the DynamoDB action: \"{message}\" | InnerException: \"{innerException}\"", message, innerException);
            LambdaLogger?.Log($"Error performing the DynamoDB action:{message} {innerException}");
        }

        protected List<T> TransformData<T>(List<Document> documents)
        {
            var objToCast = documents.ConvertAll(d => d[AttributeValue].AsString());
            return JsonHelper.Deserialize<T>(objToCast);
        }

        #region tables
        public async Task<bool> TableExists(string tableName)
        {
            var request = new ListTablesRequest
            {
                Limit = 10, // Page size.
                ExclusiveStartTableName = null
            };

            var response = await AmazonDynamoDBClient.ListTablesAsync(request).ConfigureAwait(false);
            var result = response.TableNames;
            var tableExists = result.Find(x => x == tableName) != null;
            return tableExists;
        }

        public async Task<CreateTableResponse> CreateTable(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributes, long readCapacityUnits = 5, long writeCapacityUnits = 5, bool streamEnabled = true, StreamViewType streamViewType = default)
        {
            try
            {
                var request = new CreateTableRequest
                {
                    TableName = tableName,
                    KeySchema = keySchema,
                    AttributeDefinitions = attributes,
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = readCapacityUnits,
                        WriteCapacityUnits = writeCapacityUnits
                    },
                    StreamSpecification = new StreamSpecification
                    {
                        StreamEnabled = streamEnabled,
                        StreamViewType = streamViewType ?? StreamViewType.NEW_AND_OLD_IMAGES
                    }
                };

                return await AmazonDynamoDBClient.CreateTableAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        #endregion
    }
}
