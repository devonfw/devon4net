using Devon4Net.Infrastructure.FluentValidation;
using FluentValidation;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Validators
{
    /// <summary>
    /// TodosFluentValidator implementation
    /// </summary>
    public class TodosFluentValidator : CustomFluentValidator<TodoDto>
    {
        /// <summary>
        /// TodosFluentValidator constructor
        /// </summary>
        /// <param name="launchExceptionWhenError">Please set to false to not launching an exception</param>
        public TodosFluentValidator(bool launchExceptionWhenError = false) : base(launchExceptionWhenError)
        {
        }

        /// <summary>
        /// Custom validation for TodoDto
        /// </summary>
        public override void CustomValidate()
        {
            RuleFor(todo => todo.Description).NotNull();
            RuleFor(todo => todo.Description).NotEmpty();
        }
    }
}
