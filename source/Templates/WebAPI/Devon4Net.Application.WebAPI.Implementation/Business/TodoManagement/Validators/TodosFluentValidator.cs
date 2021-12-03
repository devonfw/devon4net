using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class TodosFluentValidator : CustomFluentValidator<Todos>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="launchExceptionWhenError"></param>
        public TodosFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CustomValidate()
        {
            RuleFor(todo => todo.Description).NotNull();
            RuleFor(todo => todo.Description).NotEmpty();
        }
    }
}
