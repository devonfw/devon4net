using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Validators
{
    public class TodosFluentValidator : CustomFluentValidator<Todos>
    {
        public TodosFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(todo => todo.Description).NotNull();
            RuleFor(todo => todo.Description).NotEmpty();
        }
    }
}
