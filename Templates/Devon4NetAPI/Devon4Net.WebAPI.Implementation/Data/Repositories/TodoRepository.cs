using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the TODOS
    /// </summary>
    public class TodoRepository : Repository<Todos>, ITodoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public TodoRepository(TodoContext context) : base(context)
        {
        }

        /// <summary>
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<Todos>> GetTodo(Expression<Func<Todos, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository TodoService");
            return await Get(predicate).ConfigureAwait(false);
        }

        /// <summary>
        /// Geto the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todos> GetTodoById(long id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository TodoService with value : {id}");
            return await GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<Todos> Create(string description)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository TodoService with value : {description}");
            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The 'Description' field can not be null.");
            }

            return await Create(new Todos{Description = description}).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the TODO by id
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

            throw  new ApplicationException($"The Todo entity {id} has not been deleted.");
        }
    }
}
