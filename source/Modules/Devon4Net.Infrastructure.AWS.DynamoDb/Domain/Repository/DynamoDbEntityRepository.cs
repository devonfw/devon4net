using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Amazon.Runtime;
using Amazon;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Devon4Net.Infrastructure.AWS.DynamoDb.Extensions;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public class DynamoDbEntityRepository<T> : DynamoDbBaseRepository, IDynamoDbEntityRepository<T> where T : class
    {
        public DynamoDbEntityRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, jsonHelper)
        {
        }

        public DynamoDbEntityRepository(AmazonDynamoDBClient dynamoDBClient, ILogger logger, JsonHelper jsonHelper = null) : base(dynamoDBClient, logger, jsonHelper)
        {
        }

        public DynamoDbEntityRepository(AmazonDynamoDBClient dynamoDBClient, ILambdaLogger lambdaLogger, JsonHelper jsonHelper = null) : base(dynamoDBClient, lambdaLogger, jsonHelper)
        {
        }

        public DynamoDbEntityRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILogger logger, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, logger, jsonHelper)
        {
        }

        public DynamoDbEntityRepository(AWSCredentials awsCredentials, RegionEndpoint awsRegion, ILambdaLogger logger, JsonHelper jsonHelper = null) : base(awsCredentials, awsRegion, logger, jsonHelper)
        {
        }

        public async Task Create(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckInputGenericEntity(entity);
                await DynamoDBContext.SaveAsync(entity, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task Create(List<T> entityList, CancellationToken cancellationToken = default)
        {
            try
            {
                var batch = DynamoDBContext.CreateBatchWrite<T>();
                batch.AddPutItems(entityList);
                await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
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

        public async Task<IList<T>> Get(string paginationToken = null, CancellationToken cancellationToken = default)
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
            return Get(searchCriteria.GetScanConditionList());
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

                return await Get((object)id, cancellationToken).ConfigureAwait(false);
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

        public async Task Delete(List<T> entityList, CancellationToken cancellationToken = default)
        {
            try
            {
                var batch = DynamoDBContext.CreateBatchWrite<T>();
                batch.AddDeleteItems(entityList);
                await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }

        public async Task Put(List<T> entityList, CancellationToken cancellationToken = default)
        {
            try
            {
                var batch = DynamoDBContext.CreateBatchWrite<T>();
                batch.AddPutItems(entityList);
                await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogDynamoException(ref ex);
                throw;
            }
        }
        #region private methods

        private Task<T> Get(object id, CancellationToken cancellationToken)
        {
            return DynamoDBContext.LoadAsync<T>(id, cancellationToken);
        }

        private Task Delete(object id, CancellationToken cancellationToken)
        {
            return DynamoDBContext.DeleteAsync<T>(id, cancellationToken);
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
