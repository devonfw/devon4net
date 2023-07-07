using FluentValidation;

namespace Devon4Net.Application.Features.Command.CreateEmployee
{
    public class InsertAuthorCommandValidation : AbstractValidator<CreateEmployeeCommand>
    {
        public InsertAuthorCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("FistName must be not empty");
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Mail).NotEmpty().EmailAddress();
        }
    }
}