using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// TodoRepository interface
    /// </summary>
    public interface ITodoRepository : IRepository<Todos>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<Todos>> GetTodo(Expression<Func<Todos, bool>> predicate = null);

        /// <summary>
        /// GetTodoById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Todos> GetTodoById(long id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<Todos> Create(string description);

        /// <summary>
        /// DeleteTodoById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteTodoById(long id);
    }
}
