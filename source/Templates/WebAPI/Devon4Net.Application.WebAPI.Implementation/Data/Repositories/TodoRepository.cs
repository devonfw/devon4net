using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Validators;
using Devon4Net.Application.WebAPI.Implementation.Domain.Database;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using Devon4Net.Application.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.Application.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the TODOS
    /// </summary>
    public class TodoRepository : Repository<Todos>, ITodoRepository
    {
        /// <summary>
        /// TodosFluentValidator
        /// </summary>
        /// <param name="context"></param>
        public TodoRepository(TodoContext context) : base(context)
        {
        }

        /// <summary>
        /// Get object method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<Todos>> GetTodo(Expression<Func<Todos, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository TodoService");
            return Get(predicate);
        }

        /// <summary>
        /// Geto the object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Todos> GetTodoById(long id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository TodoService with value : {id}");
            return GetFirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Creates the object
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<Todos> Create(string description)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository TodoService with value : {description}");
            var todo = new Todos {Description = description};

            return Create(todo);
        }

        /// <summary>
        /// Deletes the object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteTodoById(long id)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository TodoService with value : {id}");
            var deleted = await Delete(t => t.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw  new ArgumentException($"The Todo entity {id} has not been deleted.");
        }
    }
}
