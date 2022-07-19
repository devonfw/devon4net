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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public class DynamoDbTableRepository : DynamoDbBaseRepository, IDynamoDbTableRepository
    {
        public DynamoDbTableRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, jsonHelper)
        {
        }

        public DynamoDbTableRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILogger logger, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, logger, jsonHelper)
        {
        }

        public DynamoDbTableRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILambdaLogger logger, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, logger, jsonHelper)
        {
        }

        public DynamoDbTableRepository(AmazonDynamoDBClient dynamoDBClient, ILogger logger, JsonHelper jsonHelper = null) : base(dynamoDBClient, logger, jsonHelper)
        {
        }

        public DynamoDbTableRepository(AmazonDynamoDBClient dynamoDBClient, ILambdaLogger lambdaLogger, JsonHelper jsonHelper = null) : base(dynamoDBClient, lambdaLogger, jsonHelper)
        {
        }

        public async Task<IList<T>> Get<T>(string tableName, QueryFilter queryFilter, string paginationToken = null, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class
        {
            var table = Table.LoadTable(AmazonDynamoDBClient, tableName);
            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            var config = new QueryOperationConfig
            {
                Filter = queryFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { AttributeValue },
                ConsistentRead = consistentRead
            };

            var results = table.Query(config);
            List<Document> data = await results.GetNextSetAsync(cancellationToken).ConfigureAwait(false);
            return TransformData<T>(data);
        }

        public async Task<IList<T>> Get<T>(string tableName, ScanFilter scanFilter, string paginationToken = null, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class
        {
            var table = Table.LoadTable(AmazonDynamoDBClient, tableName);
            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            var results = table.Scan(scanFilter);

            List<Document> data = await results.GetNextSetAsync(cancellationToken).ConfigureAwait(false);
            return TransformData<T>(data);
        }

        public async Task<IList<T>> Get<T>(string tableName, string paginationToken = null, CancellationToken cancellationToken = default) where T : class
        {
            var table = Table.LoadTable(AmazonDynamoDBClient, tableName);
            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            var results = table.Scan(scanOps);
            List<Document> data = await results.GetNextSetAsync(cancellationToken).ConfigureAwait(false);
            return TransformData<T>(data);
        }

        public async Task<IList<T>> Get<T>(List<ScanCondition> searchCriteria) where T : class
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

        public Task<IList<T>> Get<T>(DynamoSearchCriteria searchCriteria) where T : class
        {
            return Get<T>(searchCriteria.GetScanConditionList());
        }

        public async Task<T> GetByKey<T>(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                await InputChecksForGetOperation(tableName, key).ConfigureAwait(false);

                var attributes = new Dictionary<string, AttributeValue>
                {
                    [AttributeKey] = new AttributeValue { S = key }
                };

                var result = await AmazonDynamoDBClient.GetItemAsync(tableName, attributes, consistentRead, cancellationToken).ConfigureAwait(false);

                return result.Item.ContainsKey(AttributeValue) ? JsonHelper.Deserialize<T>(result.Item[AttributeValue].S) : default;
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task<PutItemResponse> Put(string tableName, string key, object objectValue, CancellationToken cancellationToken = default)
        {
            try
            {
                await InputChecksForPutOperation(tableName).ConfigureAwait(false);

                var attributes = new Dictionary<string, AttributeValue>
                {
                    [AttributeKey] = new AttributeValue { S = key },
                    [AttributeType] = new AttributeValue { S = objectValue.GetType().ToString() },
                    [AttributeValue] = new AttributeValue { S = await JsonHelper.Serialize(objectValue).ConfigureAwait(false) }
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

        public async Task<DeleteItemResponse> Delete(string tableName, string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleteItemRequest = new DeleteItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { AttributeKey, new AttributeValue { S = key } } }
                };

                return await AmazonDynamoDBClient.DeleteItemAsync(deleteItemRequest, cancellationToken).ConfigureAwait(false);
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

            var tableExists = await TableExists(tableName).ConfigureAwait(false);

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

        private async Task InputChecksForPutOperation(string tableName)
        {
            CheckTableNameAndKey(tableName, string.Empty);

            var tableExists = await TableExists(tableName).ConfigureAwait(false);

            if (!tableExists)
            {
                throw new InvalidOperationException("Cannot put the object on DynamoDb. The table does not exists. Set to true the create table param.");
            }
        }

        private static void CheckScanConditions(List<ScanCondition> searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria));
            }
        }
        #endregion
    }
}