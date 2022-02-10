using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public interface IDynamoDbTableRepository: IDynamoDbBaseRepository
    {
        Task<IList<T>> Get<T>(string tableName, string paginationToken = null, CancellationToken cancellationToken = default) where T : class;
        Task<IList<T>> Get<T>(string tableName, QueryFilter queryFilter, string paginationToken = null, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class;
        Task<IList<T>> Get<T>(string tableName, ScanFilter scanFilter, string paginationToken = null, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class;
        Task<IList<T>> Get<T>(List<ScanCondition> searchCriteria) where T : class;
        Task<IList<T>> Get<T>(DynamoSearchCriteria searchCriteria) where T : class;
        Task<T> GetByKey<T>(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default) where T : class;
        Task<PutItemResponse> Put(string tableName, string key, object objectValue, CancellationToken cancellationToken = default);
        Task<DeleteItemResponse> Delete(string tableName, string key, CancellationToken cancellationToken = default);
    }
}