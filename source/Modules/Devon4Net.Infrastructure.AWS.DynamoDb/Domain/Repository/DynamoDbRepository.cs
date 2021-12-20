using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;
using Devon4Net.Infrastructure.AWS.DynamoDb.Extensions;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public class DynamoDbRepository<T> : IDynamoDbRepository<T> where T : class
    {
        private IDynamoDBContext DynamoDBContext { get; }
        private ILogger Logger { get; }
        private ILambdaLogger LambdaLogger { get; }
        private AmazonDynamoDBClient AmazonDynamoDBClient { get; }
        private JsonHelper JsonHelper { get; }
        private const string AttributeKey = "key";
        private const string AttributeObjectType = "objectType";
        private const string AttributeObjectValue = "objectValue";

        public DynamoDbRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, JsonHelper jsonHelper = null)
        {
            Logger = null;
            LambdaLogger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILogger logger, JsonHelper jsonHelper = null)
        {
            Logger = logger;
            LambdaLogger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILambdaLogger logger, JsonHelper jsonHelper = null)
        {
            LambdaLogger = logger;
            Logger = null;
            AmazonDynamoDBClient = new AmazonDynamoDBClient(awsCredentials, awsRegion);
            DynamoDBContext = new DynamoDBContext(AmazonDynamoDBClient);
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbRepository(AmazonDynamoDBClient dynamoDBClient, ILogger logger, JsonHelper jsonHelper = null)
        {
            Logger = logger;
            LambdaLogger = null;
            DynamoDBContext = new DynamoDBContext(dynamoDBClient);
            AmazonDynamoDBClient = dynamoDBClient;
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public DynamoDbRepository(AmazonDynamoDBClient dynamoDBClient, ILambdaLogger lambdaLogger, JsonHelper jsonHelper = null)
        {
            LambdaLogger = lambdaLogger;
            Logger = null;
            DynamoDBContext = new DynamoDBContext(dynamoDBClient);
            AmazonDynamoDBClient = dynamoDBClient;
            JsonHelper = jsonHelper ?? new JsonHelper();
        }

        public Task Create(T entity, CancellationToken cancellationToken = default)
        {
            return DynamoDBContext.SaveAsync(entity, cancellationToken);
        }

        public async Task<IList<T>> GetAll(string paginationToken = null, CancellationToken cancellationToken = default)
        {
            var table = DynamoDBContext.GetTargetTable<T>();
            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            var results = table.Scan(scanOps);
            List<Document> data = await results.GetNextSetAsync(cancellationToken).ConfigureAwait(false);

            return DynamoDBContext.FromDocuments<T>(data).ToList();
        }
        public async Task<IList<T>> Get(List<ScanCondition> searchCriteria)
        {
            try
            {
                CheckScanConditions(searchCriteria);

                return await DynamoDBContext.ScanAsync<T>(searchCriteria, null).GetRemainingAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public Task<IList<T>> Get(DynamoSearchCriteria searchCriteria)
        {
            return Get(searchCriteria.GetCriteriaScanConditions());
        }

        public async Task<S> Get<S>(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default) where S : class
        {
            try
            {
                var result = await Get(tableName, key, consistentRead, cancellationToken).ConfigureAwait(false);

                return JsonHelper.Deserialize<S>(result.Item[AttributeObjectValue].S);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<GetItemResponse> Get(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default)
        {
            try
            {
                await InputChecksForGetOperation(tableName, key).ConfigureAwait(false);

                var attributes = new Dictionary<string, AttributeValue>
                {
                    [AttributeKey] = new AttributeValue { S = key }
                };

                return await AmazonDynamoDBClient.GetItemAsync(tableName, attributes, consistentRead, cancellationToken).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public Task<T> GetById(int id, CancellationToken cancellationToken = default)
        {
            return Get(id, cancellationToken);
        }

        public Task<T> GetById(float id, CancellationToken cancellationToken = default)
        {
            return Get(id, cancellationToken);
        }

        public async Task<T> GetById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdString(id);

                return await Get(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdGuid(id);

                return await Get(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<T> GetById(DateTime id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdDateTime(id);

                return await Get(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<T> GetById(object id, CancellationToken cancellationToken)
        {
            try
            {
                CheckInputIdObject(id);

                return await Get(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public Task DeleteById(int id, CancellationToken cancellationToken = default)
        {
            return Delete(id, cancellationToken);
        }

        public Task DeleteById(float id, CancellationToken cancellationToken = default)
        {
            return Delete(id, cancellationToken);
        }

        public async Task DeleteById(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdGuid(id);

                await Delete(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task DeleteById(DateTime id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdDateTime(id);

                await Delete(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task DeleteById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdString(id);

                await Delete(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task DeleteById(object id, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputIdObject(id);

                await Delete(id, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                CheckInputGenericEntity(entity);
                await DynamoDBContext.SaveAsync(entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<PutItemResponse> Put(string tableName, string key, object objectValue, bool createTable = false, CancellationToken cancellationToken = default)
        {
            try
            {
                await InputChecksForPutOperation(tableName, createTable).ConfigureAwait(false);

                var attributes = new Dictionary<string, AttributeValue>
                {
                    [AttributeKey] = new AttributeValue { S = key },
                    [AttributeObjectType] = new AttributeValue { S = objectValue.GetType().ToString() },
                    [AttributeObjectValue] = new AttributeValue { S = await JsonHelper.Serialize(objectValue).ConfigureAwait(false) }
                };

                var request = new PutItemRequest
                {
                    TableName = tableName,
                    Item = attributes
                };

                return await AmazonDynamoDBClient.PutItemAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<bool> TableExists(string tableName, bool createTable = false)
        {
            var request = new ListTablesRequest
            {
                Limit = 10, // Page size.
                ExclusiveStartTableName = null
            };

            var response = await AmazonDynamoDBClient.ListTablesAsync(request).ConfigureAwait(false);
            var result = response.TableNames;
            var tableExists =  result.Find(x=>x == tableName)!=null;
            if (!tableExists && createTable) await CreateTable(tableName).ConfigureAwait(false);
            return tableExists;
        }

        public async Task<CreateTableResponse> CreateTable(string tableName, long readCapacityUnits = 5, long writeCapacityUnits = 5, bool streamEnabled = true, StreamViewType streamViewType = default)
        {
            try
            {
                var request = new CreateTableRequest
                {
                    TableName = tableName,
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = AttributeKey,
                            KeyType = "HASH"
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = AttributeKey,
                            AttributeType = "S"
                        }
                    },
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

        #region private methods

        private async Task InputChecksForGetOperation(string tableName, string key)
        {
            CheckTableNameAndKey(tableName, key, true);

            var tableExists = await TableExists(tableName, false).ConfigureAwait(false);

            if (!tableExists)
            {
                throw new ArgumentException($"The provided table does not exist. Cannot perform the 'Get' operation the key {key}");
            }
        }

        private static void CheckTableNameAndKey(string tableName, string key, bool checkKey = false)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("The DynamoDb table name cannot be null");
            }


            if (checkKey && string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The provided key cannot be null. Cannot perform the 'Get' operation");
            }
        }

        private async Task InputChecksForPutOperation(string tableName, bool createTable)
        {
            CheckTableNameAndKey(tableName, string.Empty);

            var tableExists = await TableExists(tableName).ConfigureAwait(false);

            if (!tableExists && !createTable)
            {
                throw new InvalidOperationException("Cannot put the object on DynamoDb. The table does not exists. Set to true the create table param.");
            }

            if (!tableExists)
            {
                await CreateTable(tableName).ConfigureAwait(false);
            }
        }

        private Task<T> Get(object id, CancellationToken cancellationToken)
        {
            return DynamoDBContext.LoadAsync<T>(id, cancellationToken);
        }

        private Task Delete(object id, CancellationToken cancellationToken)
        {
            return DynamoDBContext.DeleteAsync<T>(id, cancellationToken);
        }

        private void LogDynamoException(ref Exception exception)
        {
            var message = exception?.Message;
            var innerException = exception?.InnerException;
            Logger?.LogError("Error performing the DynamoDB action: \"{message}\" | InnerException: \"{innerException}\"", message, innerException);
            LambdaLogger?.Log($"Error performing the DynamoDB action:{message} {innerException}");
        }

        private static void CheckScanConditions(List<ScanCondition> searchCriteria)
        {
            if (searchCriteria == null || searchCriteria.Count == 0)
            {
                throw new ArgumentNullException(nameof(searchCriteria));
            }
        }
        private static void CheckInputIdString(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
        }
        private static void CheckInputIdGuid(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

        private static void CheckInputIdDateTime(DateTime id)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

        private static void CheckInputIdObject(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

        private static void CheckInputGenericEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
        #endregion
    }
}