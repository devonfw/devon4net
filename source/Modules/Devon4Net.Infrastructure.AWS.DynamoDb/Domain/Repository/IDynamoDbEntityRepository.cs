using Amazon.DynamoDBv2.DataModel;
using Devon4Net.Infrastructure.AWS.DynamoDb.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository
{
    public interface IDynamoDbEntityRepository<T> : IDynamoDbBaseRepository where T : class
    {
        Task Create(T entity, CancellationToken cancellationToken = default);
        Task Update(T entity);
        Task<IList<T>> Get(string paginationToken = null, CancellationToken cancellationToken = default);
        Task<IList<T>> Get(List<ScanCondition> searchCriteria);
        Task<IList<T>> Get(DynamoSearchCriteria searchCriteria);
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
    }
}