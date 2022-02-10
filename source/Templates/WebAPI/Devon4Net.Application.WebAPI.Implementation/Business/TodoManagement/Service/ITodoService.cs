using System.Linq.Expressions;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service
{
    /// <summary>
    /// TodoService interface
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// GetTodo
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TodoDto>> GetTodo(Expression<Func<Todos, bool>> predicate = null);

        /// <summary>
        /// GetTodoById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Todos> GetTodoById(long id);

        /// <summary>
        /// CreateTodo
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<Todos> CreateTodo(string description);

        /// <summary>
        /// DeleteTodoById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteTodoById(long id);

        /// <summary>
        /// ModifyTodoById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Todos> ModifyTodoById(long id);
    }
}