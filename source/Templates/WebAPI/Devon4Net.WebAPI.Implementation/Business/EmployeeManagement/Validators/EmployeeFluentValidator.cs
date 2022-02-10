using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Validators
{
    public class EmployeeFluentValidator : CustomFluentValidator<Employee>
    {
        public EmployeeFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(Employee => Employee.Name).NotNull();
            RuleFor(Employee => Employee.Name).NotEmpty();
            RuleFor(Employee => Employee.Name).NotNull();
            RuleFor(Employee => Employee.Surname).NotEmpty();
        }
    }
}
