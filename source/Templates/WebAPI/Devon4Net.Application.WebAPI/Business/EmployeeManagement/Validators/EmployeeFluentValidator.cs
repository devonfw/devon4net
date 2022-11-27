using Devon4Net.Infrastructure.FluentValidation;
using FluentValidation;
using Devon4Net.Application.WebAPI.Business.EmployeeManagement.Dto;

namespace Devon4Net.Application.WebAPI.Business.EmployeeManagement.Validators
{
    /// <summary>
    /// EmployeeFluentValidator implementation
    /// </summary>
    public class EmployeeFluentValidator : CustomFluentValidator<EmployeeDto>
    {
        /// <summary>
        /// EmployeeFluentValidator constructor
        /// </summary>
        /// <param name="launchExceptionWhenError">Please set to false to not launching an exception</param>
        public EmployeeFluentValidator(bool launchExceptionWhenError = false) : base(launchExceptionWhenError)
        {
        }

        /// <summary>
        /// Custom validation for EmployeeDto
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
