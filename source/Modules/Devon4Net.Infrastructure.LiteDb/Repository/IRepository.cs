using System.Collections.Generic;
using LiteDB;

namespace Devon4Net.Infrastructure.LiteDb.Repository
{
    public interface IRepository<T>
    {
        BsonValue Create(T entity);
        bool Update(T entity);
        int Delete(BsonExpression predicate, bool deleteAllCheck = false);
        IEnumerable<T> Get();
        IEnumerable<T> Get(BsonExpression predicate, int skip = 0, int limit = int.MaxValue);
        T GetFirstOrDefault(BsonExpression predicate);
        T GetFirstOrDefault(Query query);
    }
}