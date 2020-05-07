using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Devon4Net.Infrastructure.Log;
using LiteDB;

namespace Devon4Net.Infrastructure.LiteDb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private LiteDatabase LiteDb { get; set; }

        public Repository(ILiteDbContext liteDbContext)
        {
            LiteDb = liteDbContext?.Database ?? throw new ArgumentException("The context can not be null. Please check your DI container configuration and check if the container has declared the instance of the context");
        }

        public BsonValue Create(T entity)
        {
            var result = LiteDb.GetCollection<T>(typeof(T).Name).Insert(entity);
            return result;
        }

        public bool Update(T entity)
        {
            return LiteDb.GetCollection<T>(typeof(T).Name).Update(entity);
        }

        public int Delete(BsonExpression predicate, bool deleteAllCheck = false)
        {
            if (predicate != null && !deleteAllCheck)
            {
                return LiteDb.GetCollection<T>(typeof(T).Name).DeleteMany(predicate);
            }

            if (predicate == null && deleteAllCheck)
            {
                return LiteDb.GetCollection<T>(typeof(T).Name).DeleteAll();
            }

            var errorMessage = "Please check the predicate is null and the deleteAllCheck param as well. The provided predicate is null and the input param deleteAllCheck is to false. If you want to delete all the collection please set the deleteAllCheck param to true.";
            Devon4NetLogger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }

        public IEnumerable<T> Get()
        {
            return LiteDb.GetCollection<T>(typeof(T).Name).FindAll();
        }

        public IEnumerable<T> Get(BsonExpression predicate, int skip = 0, int limit = int.MaxValue)
        {
            return LiteDb.GetCollection<T>(typeof(T).Name).Find(predicate,skip, limit);
        }

        public T GetFirstOrDefault(BsonExpression predicate)
        {
            return LiteDb.GetCollection<T>(typeof(T).Name).FindOne(predicate);
        }
        public T GetFirstOrDefault(Query query)
        {
            return LiteDb.GetCollection<T>(typeof(T).Name).FindOne(query);
        }
    }
}
