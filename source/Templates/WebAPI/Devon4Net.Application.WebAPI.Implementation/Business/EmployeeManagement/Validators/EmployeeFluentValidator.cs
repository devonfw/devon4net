using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeFluentValidator : CustomFluentValidator<Employee>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="launchExceptionWhenError"></param>
        public EmployeeFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CustomValidate()
        {
            RuleFor(Employee => Employee.Name).NotNull();
            RuleFor(Employee => Employee.Name).NotEmpty();
            RuleFor(Employee => Employee.Name).NotNull();
            RuleFor(Employee => Employee.Surname).NotEmpty();
        }
    }
}
