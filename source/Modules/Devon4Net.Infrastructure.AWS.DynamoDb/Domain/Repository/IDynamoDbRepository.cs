using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public interface IDynamoDbRepository<T> where T : class
    {
        Task Create(T entity, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAll(string paginationToken = null, CancellationToken cancellationToken = default);
        Task<IList<T>> Get(List<ScanCondition> searchCriteria);
        Task<IList<T>> Get(DynamoSearchCriteria searchCriteria);
        Task<S> Get<S>(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default) where S : class;
        Task<GetItemResponse> Get(string tableName, string key, bool consistentRead = true, CancellationToken cancellationToken = default);
        Task<T> GetById(int id, CancellationToken cancellationToken = default);
        Task<T> GetById(float id, CancellationToken cancellationToken = default);
        Task<T> GetById(string id, CancellationToken cancellationToken = default);
        Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<T> GetById(DateTime id, CancellationToken cancellationToken = default);
        Task<T> GetById(object id, CancellationToken cancellationToken);
        Task DeleteById(int id, CancellationToken cancellationToken = default);
        Task DeleteById(float id, CancellationToken cancellationToken = default);
        Task DeleteById(string id, CancellationToken cancellationToken = default);
        Task DeleteById(Guid id, CancellationToken cancellationToken = default);
        Task DeleteById(DateTime id, CancellationToken cancellationToken = default);
        Task DeleteById(object id, CancellationToken cancellationToken = default);
        Task Update(T entity);
        Task<PutItemResponse> Put(string tableName, string key, object objectValue, bool createTable = false, CancellationToken cancellationToken = default);
        Task<CreateTableResponse> CreateTable(string tableName, long readCapacityUnits = 5, long writeCapacityUnits = 5, bool streamEnabled = true, StreamViewType streamViewType = default);
        Task<bool> TableExists(string tableName, bool createTable = false);
    }
}