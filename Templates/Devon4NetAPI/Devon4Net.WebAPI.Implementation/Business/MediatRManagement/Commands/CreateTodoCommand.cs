using System;
using System.Collections.Generic;
using System.Text;
using Devon4Net.Infrastructure.MediatR.Command;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Dto;

namespace Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Commands
{
    /// <summary>
    /// THe command to create a TO-DO
    /// </summary>
    public class CreateTodoCommand : CommandBase<TodoResultDto>
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor of the query 
        /// </summary>
        /// <param name="todoId"></param>
        public CreateTodoCommand( string description)
        {
            Description = description;
        }
    }
}
