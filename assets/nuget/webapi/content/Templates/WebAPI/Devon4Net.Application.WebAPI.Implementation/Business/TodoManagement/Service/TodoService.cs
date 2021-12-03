using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Converters;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Domain.Database;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using Devon4Net.Application.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service
{
    /// <summary>
    /// TODO service implementation
    /// </summary>
    public class TodoService: Service<TodoContext>, ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public TodoService(IUnitOfWork<TodoContext> uoW) : base(uoW)
        {
            _todoRepository = uoW.Repository<ITodoRepository>();
        }

        /// <summary>
        /// Gets the TODO
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TodoDto>> GetTodo(Expression<Func<Todos, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from service TodoService");
            var result = await _todoRepository.GetTodo(predicate).ConfigureAwait(false);
            return result.Select(TodoConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Todos> GetTodoById(long id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from service TodoService with value : {id}");
            return _todoRepository.GetTodoById(id);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<Todos> CreateTodo(string description)
        {
            Devon4NetLogger.Debug($"SetTodo method from service TodoService with value : {description}");

            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The 'Description' field can not be null.");
            }

            return _todoRepository.Create(description);
        }
        
        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteTodoById(long id)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from service TodoService with value : {id}");
            var todo = await _todoRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (todo == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }

            return await _todoRepository.DeleteTodoById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todos> ModifyTodoById(long id)
        {
            Devon4NetLogger.Debug($"ModifyTodoById method from service TodoService with value : {id}");
            
            var todo = await _todoRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (todo == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }
            
            todo.Done = !todo.Done;
            
            return await _todoRepository.Update(todo).ConfigureAwait(false);
        }
    }
}
