using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.UnitOfWork.RawSqlRepository
{
    public class RawSqlRepository : IRawSqlRepository
    {
        /// <summary>
        ///     Dapper Base Repository
        /// </summary>
        /// <param name="context">The data base context to work with</param>
        /// </param>
        protected RawSqlRepository(DbContext context)
        {
            DbContext = context;
        }

        private DbContext DbContext { get; set; }

        /// <summary>
        /// This method executes a SQL query against the database using the Dapper library.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns>
        /// It returns an IEnumerable of the provided object containing the results of the query.
        /// </returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await DbContext.Database.GetDbConnection().QueryAsync<T>(sql, parameters, transaction);
        }

        /// <summary>
        /// This method executes a SQL query against the database using the Dapper library.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns>
        /// Returns the first result, or null if the query returns no results.
        /// </returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await DbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<T>(sql, parameters, transaction);
        }

        /// <summary>
        /// This method executes a SQL query against the database using the Dapper library.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns>
        /// It returns the number of affected rows.
        /// </returns>
        public async Task<int> ExecuteAsync(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await DbContext.Database.GetDbConnection().ExecuteAsync(sql, parameters, transaction);
        }
    }
}