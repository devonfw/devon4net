using System.Data;

namespace Devon4Net.Infrastructure.UnitOfWork.RawSqlRepository
{
    public interface IRawSqlRepository
    {
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
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null);

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
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null);

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
        Task<int> ExecuteAsync(string sql, object parameters = null, IDbTransaction transaction = null);
    }
}