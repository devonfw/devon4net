using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.Infrastructure.MediatR.Query;
namespace Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTodoQuery : QueryBase<TodoResultDto>
    {


        /// <summary>
        /// 
        /// </summary>
        public long TodoId{ get; set; }

        /// <summary>
        /// Constructor of the query 
        /// </summary>
        /// <param name="todoId"></param>
        public GetTodoQuery(long todoId)
        {
            TodoId = todoId;
        }
    }
}
